﻿namespace LibShare.Client.Data.ApiModels
{
    public class LoginApiModel
    {
        /// <summary>
        /// User email
        /// </summary>     
        /// <example>example@gmail.com</example>
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        /// <example>Qwerty1-</example>
        public string Password { get; set; }

        /// <summary>
        /// Google Recaptcha Token
        /// </summary>
        /// <example>Recaptcha</example>
        public string RecaptchaToken { get; set; }
    }
}
