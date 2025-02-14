using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO
{
    public class UpdateCinemaDto
    {
        [Required(ErrorMessage ="O campo nome é obrigatório")]
        public string Nome { get; set; }
    }
}
