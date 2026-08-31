using EF_DiegoQuispeR.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Repository
{
    public class EditorialRepository
    {
        private readonly db_bibliotecaContext ctx;
        public EditorialRepository(db_bibliotecaContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<List<Editorial>> FindAll()
        {
            return await ctx.Editorials.ToListAsync();
        }

        public async Task<Editorial?> FindById(int id)
        {
            return await ctx.Editorials
                            .FirstOrDefaultAsync(e => e.IdEditorial == id);
        }

        public async Task<List<string>> FindAllNationalities()
        {
            return await ctx.Editorials.Select(e => e.PaisOrigen).Distinct().ToListAsync();
        }

        public async Task<Editorial?> FindByNationality(string nationality)
        {
            return await ctx.Editorials.FirstOrDefaultAsync(e => e.PaisOrigen == nationality);
        }
    }
}
