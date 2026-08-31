using EF_DiegoQuispeR.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Repository
{
    public class EditorialBookmarkRepository
    {
        private readonly DbBibliotecaContext ctx;
        public EditorialBookmarkRepository(DbBibliotecaContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<Editorial?> FindBookmarkById(int id, int userId)
        {
            Editorial? editorial = await ctx.EditorialBookmarks
                                    .Where(eb => eb.Id == id && eb.Usuario == userId)
                                    .Join(ctx.Editorials, eb => eb.Editorial, e => e.IdEditorial, (eb, e) => e)
                                    .FirstOrDefaultAsync();
            return editorial;
        }

        public async Task<List<Editorial>> FindBookmarkByUsuario(int usuarioId)
        {
            return await ctx.EditorialBookmarks
                    .Where(eb => eb.Usuario == usuarioId)
                    .Join(
                            ctx.Editorials,
                            eb => eb.Editorial,
                            e => e.IdEditorial,
                            (eb, e) => e
                         )
                    .ToListAsync();
        }

        public async Task<bool> BookmarkExists(EditorialBookmark eb)
        {
            return await ctx.EditorialBookmarks.AnyAsync(eba =>
                eba.Usuario == eb.Usuario &&
                eba.Editorial == eb.Editorial);
        }

        public async Task<bool> EditorialExists(int id)
        {
            return await ctx.Editorials.AnyAsync(e =>
                e.IdEditorial == id);
        }

        public async Task Save(EditorialBookmark eb)
        {
            await ctx.EditorialBookmarks.AddAsync(eb);
            await ctx.SaveChangesAsync();
        }

        public async Task<bool> Delete(int usuarioId, int editorialId)
        {
            var bookmark = await ctx.EditorialBookmarks
                                    .FirstOrDefaultAsync(eb => eb.Usuario == usuarioId && eb.Editorial == editorialId);

            if (bookmark == null)
                return false;

            ctx.EditorialBookmarks.Remove(bookmark);

            await ctx.SaveChangesAsync();

            return true;
        }
    }
}
