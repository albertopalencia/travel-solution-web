using System.Collections.Generic;

namespace TravelWeb.Client.Models
{
    public class BookModel
    {
        /// <summary>
        /// Book Key 
        /// </summary>
        public int Isbn { get; set; }

        /// <summary>
        /// Book's name
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Book's Sypnosis
        /// </summary>
        public string Sypnosis { get; set; }

        /// <summary>
        /// Book's number pages
        /// </summary>
        public string NumberPages { get; set; }

        /// <summary>
        /// Book's authors
        /// </summary>
        public List<AuthorModel> Authors { get; set; } = new();

        /// <summary>
        /// Book's editorial
        /// </summary>
        public string EditorialName { get; set; }
    }
}
