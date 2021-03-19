using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using LibShare.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Providers
{
    public class JWTAuthenticationStateProvider : AuthenticationStateProvider, IAuthService
    {
        private readonly IJSRuntime _jSRuntime;
        private readonly HttpClient _httpClient;
        private readonly IAccountService _accountService;
        private readonly NavigationManager _navigationManager;

        private AuthenticationState Anonymous => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        private readonly string jwtKey = "jwt";
        private readonly string tokenRefreshKey = "tokenRefresh";
        public JWTAuthenticationStateProvider(IJSRuntime jSRuntime, HttpClient httpClient, IAccountService accountService, NavigationManager navigationManager)
        {
            _jSRuntime = jSRuntime;
            _httpClient = httpClient;
            _accountService = accountService;
            _navigationManager = navigationManager;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            Console.WriteLine("Anonymous");
            var token = await _jSRuntime.GetFromLocalStorage(jwtKey);
            var tokenRefresh = await _jSRuntime.GetFromLocalStorage(tokenRefreshKey);
            if (string.IsNullOrWhiteSpace(token))
            {
                return Anonymous;
            }

            if (string.IsNullOrWhiteSpace(tokenRefresh))
            {
                return Anonymous;
            }

            var decodeToken = ReadJwtToken(token);
            if(decodeToken.ValidTo.ToLocalTime() < DateTime.Now)
            {
                try
                {
                    var tokens = await _accountService.RefreshTokenAsync(new TokenApiModel
                    {
                        Token = token,
                        RefreshToken = tokenRefresh
                    });   

                    await _jSRuntime.SetInLocalStorage(jwtKey, tokens.Token);
                    await _jSRuntime.SetInLocalStorage(tokenRefreshKey, tokens.RefreshToken);

                    token = tokens.Token;
                    tokenRefresh = tokens.RefreshToken;
                    Console.WriteLine("Refreshing ... ");

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error from server: " + ex.Message);
                    return Anonymous;
                }
            }

            return BuildAuthenticationState(token);
        }

        public AuthenticationState BuildAuthenticationState(string jwt)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwt);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ReadJwtToken(jwt).Claims, "jwt")));
        }

        public JwtSecurityToken ReadJwtToken(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(jwt);
        }

        public async Task Login(LoginApiModel model)
        {
            var tokens = await _accountService.LoginUserAsync(model);

            await _jSRuntime.SetInLocalStorage(jwtKey, tokens.Token);
            await _jSRuntime.SetInLocalStorage(tokenRefreshKey, tokens.RefreshToken);

            var authState = BuildAuthenticationState(tokens.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task Register(RegisterApiModel model)
        {
            var tokens = await _accountService.RegisterUserAsync(model);

            await _jSRuntime.SetInLocalStorage(jwtKey, tokens.Token);
            await _jSRuntime.SetInLocalStorage(tokenRefreshKey, tokens.RefreshToken);

            var authState = BuildAuthenticationState(tokens.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task RefreshToken()
        {
            var token = await _jSRuntime.GetFromLocalStorage(jwtKey);
            var refreshToken = await _jSRuntime.GetFromLocalStorage(tokenRefreshKey);
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(refreshToken))
                await Logout();
            var decodeToken = ReadJwtToken(token);
            if (decodeToken.ValidTo.ToLocalTime() < DateTime.Now)
            {
                try
                {
                    var tokens = await _accountService.RefreshTokenAsync(new TokenApiModel
                    {
                        Token = token,
                        RefreshToken = refreshToken
                    });

                    await _jSRuntime.SetInLocalStorage(jwtKey, tokens.Token);
                    await _jSRuntime.SetInLocalStorage(tokenRefreshKey, tokens.RefreshToken);

                    var authState = BuildAuthenticationState(tokens.Token);
                    NotifyAuthenticationStateChanged(Task.FromResult(authState));
                    Console.WriteLine("Refreshing ... ");

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error from server: " + ex.Message);
                    await Logout();
                }
            }
        }

        public async Task Logout()
        {
            await _jSRuntime.RemoveFromLocalStorage(jwtKey);
            await _jSRuntime.RemoveFromLocalStorage(tokenRefreshKey);
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
            await _accountService.LogoutAsync();
            _navigationManager.NavigateTo("/login");
        }

        public async Task UpdateToken(TokenApiModel tokens)
        {
            await _jSRuntime.SetInLocalStorage(jwtKey, tokens.Token);
            await _jSRuntime.SetInLocalStorage(tokenRefreshKey, tokens.RefreshToken);

            var authState = BuildAuthenticationState(tokens.Token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }
    }
}
