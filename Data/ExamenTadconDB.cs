
using APILIBROS.Models;
using Microsoft.EntityFrameworkCore;

namespace APILIBROS.Data
{
    public class ExamenTadconDB : DbContext
    {
        public ExamenTadconDB(DbContextOptions<ExamenTadconDB> options) : base(options)
        {

        }

        public DbSet<Libro> Libros => Set<Libro>();
    }
}
