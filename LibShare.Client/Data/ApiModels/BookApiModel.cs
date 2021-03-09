﻿using System;

namespace LibShare.Client.Data.ApiModels
{
    public class BookApiModel
    {
        /// <summary>
        /// Id of book 
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Title of book 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Author of book 
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Publisher of book 
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// When book was published 
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// Lanquage of book 
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Description of book 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Picture of book 
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Is e-book 
        /// </summary>
        public bool IsEbook { get; set; }

        // public string File { get; set; }

        /// <summary>
        /// Category id
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Date of creation
        /// </summary>
        public DateTime DateCreate { get; set; }
    }
}
