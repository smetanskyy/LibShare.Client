﻿namespace LibShare.Client.Data.ApiModels
{
    public class ChangePasswordApiModel
    {
        /// <summary>
        /// User old password
        /// </summary>
        /// <example>Qwerty1-</example>
        public string OldPassword { get; set; }

        /// <summary>
        /// User new password
        /// </summary>
        /// <example>Qwerty1-</example>
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirm new password
        /// </summary>
        /// <example>Qwerty1-</example>
        public string ConfirmNewPassword { get; set; }

        /// <summary>
        /// Google Recaptcha Token
        /// </summary>
        /// <example>Recaptcha</example>
        public string RecaptchaToken { get; set; }
    }
}
