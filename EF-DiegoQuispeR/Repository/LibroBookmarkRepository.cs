using EF_DiegoQuispeR.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Repository
{
    public class LibroBookmarkRepository
    {
        private readonly DbBibliotecaContext ctx;
        public LibroBookmarkRepository(DbBibliotecaContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<Libro?> FindBookmarkById(int id, int userId)
        {
            Libro? libro = await ctx.LibroBookmarks
                                    .Where(lb => lb.Id == id && lb.Usuario == userId)
                                    .Join(ctx.Libros, lb => lb.Libro, l => l.IdLibro, (lb, l) => l)
                                    .FirstOrDefaultAsync();
            return libro;
        }

        public async Task<List<Libro>> FindBookmarkByUsuario(int usuarioId)
        {
            return await ctx.LibroBookmarks
                    .Where(lb => lb.Usuario == usuarioId)
                    .Join(
                            ctx.Libros,
                            lb => lb.Libro,
                            l => l.IdLibro,
                            (lb, l) => l
                         )
                    .ToListAsync();
        }

        public async Task<bool> BookmarkExists(LibroBookmark lb)
        {
            return await ctx.LibroBookmarks.AnyAsync(lba =>
                lba.Usuario == lb.Usuario &&
                lba.Libro == lb.Libro);
        }

        public async Task<bool> LibroExists(int id)
        {
            return await ctx.Libros.AnyAsync(l =>
                l.IdLibro == id);
        }

        public async Task Save(LibroBookmark lb)
        {
            await ctx.LibroBookmarks.AddAsync(lb);
            await ctx.SaveChangesAsync();
        }

        public async Task<bool> Delete(int usuarioId, int libroId)
        {
            var bookmark = await ctx.LibroBookmarks
                                    .FirstOrDefaultAsync(lb => lb.Usuario == usuarioId && lb.Libro == libroId);

            if (bookmark == null)
                return false;

            ctx.LibroBookmarks.Remove(bookmark);

            await ctx.SaveChangesAsync();

            return true;
        }
    }
}
