using EF_DiegoQuispeR.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Repository
{
    public class LibroRepository
    {
        private readonly db_bibliotecaContext ctx;
        public LibroRepository(db_bibliotecaContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<List<Libro>> FindAll()
        {
            return await ctx.Libros.ToListAsync();
        }

        public async Task<Libro> FindById(int id)
        {
            return await ctx.Libros
                            .FirstOrDefaultAsync(l => l.IdLibro == id);
        }

        public async Task<List<Libro>> FindByGenre(string genre)
        {
            return await ctx.Libros
                    .Where(l => l.Categoria == genre)
                    .ToListAsync();
        }

        public async Task<List<string>> FindAllUniqueGenres()
        {
            return await ctx.Libros
                    .Select(l => l.Categoria)
                    .Distinct()
                    .ToListAsync();
        }

        public async Task<List<Libro>> FindByAuthor(int autorId)
        {
            return await ctx.Libros
                    .Where(l => l.IdAutor == autorId)
                    .ToListAsync();
        }


    }
}
