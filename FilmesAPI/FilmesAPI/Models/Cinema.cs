using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FilmesAPI.Models
{
    public class Cinema
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="O campo de nome eh obrigatohrio")]
        public string Nome { get; set; }
        
        public int EnderecoId {  get; set; }

        public virtual Endereco Endereco { get; set; }
    }
}
