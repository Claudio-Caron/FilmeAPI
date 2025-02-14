using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmeContext: DbContext
    {
        private string conecctionString = "";

        public FilmeContext(DbContextOptions opts) : base(opts) { }
        public DbSet<Filme> Filmes { get; set; }
        
        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        

    }

}
