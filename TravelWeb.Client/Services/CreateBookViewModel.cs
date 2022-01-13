using System.ComponentModel.DataAnnotations;

namespace TravelWeb.Client.Services
{
    public class CreateBookViewModel
    {
        [Required(ErrorMessage = "Titulo es requerido.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Isbn es requerido.")] 
        public string Isbn { get; set; }

        [Required(ErrorMessage = "Sipnosis es requerido.")]
        public string Sypnosis { get; set; }

        [Required(ErrorMessage = "Número de páginas es requerido.")]
        public string NumberPages { get; set; }

        [Required(ErrorMessage = "Editorial es requerido.")]
        public int EditorialId { get; set; }

        [Required(ErrorMessage = "Autor es requerido.")]
        public int AuthorId { get; set; }

    }
}
