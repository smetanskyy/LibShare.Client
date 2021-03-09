namespace LibShare.Client.Data.ApiModels
{
    public class EmailApiModel
    {
        /// <summary>
        /// User email
        /// </summary>     
        /// <example>example@gmail.com</example>
        public string Email { get; set; }


        /// <summary>
        /// Google Recaptcha Token
        /// </summary>
        /// <example>Recaptcha</example>
        public string RecaptchaToken { get; set; }
    }
}
