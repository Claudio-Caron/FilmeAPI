﻿using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.DTO;

public class CreateFilmeDto
{

    [Required(ErrorMessage = "O titulo do filme eh obrigatorio")]
    public string? Titulo { get; set; }

    [Required(ErrorMessage = "O genero do filme eh obrigatorio")]
    [StringLength(50, ErrorMessage = "Genero deve ter menos de 50 caracteres")]
    public string? Genero { get; set; }

    [Required(ErrorMessage = "A duracao do filme eh obrigatorio")]
    [Range(70, 600, ErrorMessage = "A duracao deve ser entre 70 e 600 minutos")]
    public int Duracao { get; set; }
}
