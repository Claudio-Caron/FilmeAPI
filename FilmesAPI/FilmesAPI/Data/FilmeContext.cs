using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmeContext: DbContext
    {
        private string conecctionString = "";

        public DbSet<Filme> Filmes { get; set; }
        public FilmeContext(DbContextOptions opts):base(opts) { }


    }

}
