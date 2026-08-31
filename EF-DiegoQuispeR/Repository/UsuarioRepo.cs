using EF_DiegoQuispeR.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_DiegoQuispeR.Repository
{
    public class UsuarioRepo
    {
        private readonly db_bibliotecaContext ctx;
        public UsuarioRepo(db_bibliotecaContext _ctx)
        {
            ctx = _ctx;
        }
        public async Task<List<Usuario>> findAll()
        {
            return await ctx.Usuarios.ToListAsync(); 
        }

        public async Task<Usuario?> findByUsername(string username)
        {
            return await ctx.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<ProtectedUsuarioResponse?> findById(int id)
        {
            return await ctx.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new ProtectedUsuarioResponse
                {
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Edad = u.Edad,
                    Username = u.Username,
                    Rol = u.Rol
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> isUsernameUsed(string username)
        {
            return await ctx.Usuarios.AnyAsync(u => u.Username == username);
        }

        public async Task save(Usuario usuario)
        {
            await ctx.Usuarios.AddAsync(usuario);
            await ctx.SaveChangesAsync();
        }
    }
}
