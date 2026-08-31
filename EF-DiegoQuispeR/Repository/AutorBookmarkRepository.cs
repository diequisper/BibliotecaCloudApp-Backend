using EF_DiegoQuispeR.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Repository
{
    public class AutorBookmarkRepository
    {
        private readonly DbBibliotecaContext ctx;
        public AutorBookmarkRepository(DbBibliotecaContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<Autor?> FindBookmarkById(int id, int userId)
        {
            Autor? autor = await ctx.AutorBookmarks
                                    .Where(ab => ab.Id == id && ab.Usuario == userId)
                                    .Join(ctx.Autors, ab => ab.Autor, a => a.IdAutor, (ab, a) => a)
                                    .FirstOrDefaultAsync();
            return autor;
        }

        public async Task<List<Autor>> FindBookmarkByUsuario(int usuarioId)
        {
            return await ctx.AutorBookmarks
                    .Where(ab => ab.Usuario == usuarioId)
                    .Join(
                            ctx.Autors,
                            ab => ab.Autor,
                            a => a.IdAutor,
                            (ab, a) => a
                         )
                    .ToListAsync();
        }

        public async Task<bool> BookmarkExists(AutorBookmark ab)
        {
            return await ctx.AutorBookmarks.AnyAsync(aba =>
                aba.Usuario == ab.Usuario &&
                aba.Autor == ab.Autor);
        }

        public async Task<bool> AutorExists(int id)
        {
            return await ctx.Autors.AnyAsync(a =>
                a.IdAutor == id);
        }

        public async Task Save(AutorBookmark ab)
        {
            await ctx.AutorBookmarks.AddAsync(ab);
            await ctx.SaveChangesAsync();
        }

        public async Task<bool> Delete(int usuarioId, int autorId)
        {
            var bookmark = await ctx.AutorBookmarks
                                    .FirstOrDefaultAsync(ab => ab.Usuario == usuarioId && ab.Autor == autorId);

            if (bookmark == null)
                return false;

            ctx.AutorBookmarks.Remove(bookmark);

            await ctx.SaveChangesAsync();

            return true;
        }
    }
}
