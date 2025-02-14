using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO;

public class UpdateEnderecoDto
{ 
    public string Logradouro { get; set; }
    public int Numero { get; set; }
}
