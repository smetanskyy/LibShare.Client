using System.ComponentModel.DataAnnotations;

namespace LibShare.Client.Data.ApiModels
{
    public class MessageApiModel
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Message { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Message2 { get; set; }
    }
}