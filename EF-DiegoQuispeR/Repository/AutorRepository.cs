using EF_DiegoQuispeR.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Repository
{
    public class AutorRepository
    {
        private readonly db_bibliotecaContext ctx;
        public AutorRepository(db_bibliotecaContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<List<Autor>> FindAll()
        {
            return await ctx.Autors.ToListAsync();
        }

        public async Task<Autor?> FindById(int id)
        {
            return await ctx.Autors
                            .FirstOrDefaultAsync(a => a.IdAutor == id);
        }

        public async Task<List<string>> FindAllNationalities()
        {
            return await ctx.Autors.Select(a => a.Nacionalidad).Distinct().ToListAsync();
        }

        public async Task<Autor?> FindByNationality(string nationality)
        {
            return await ctx.Autors.FirstOrDefaultAsync(a => a.Nacionalidad == nationality);
        }
    }
}
