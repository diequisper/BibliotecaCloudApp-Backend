using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EF_DiegoQuispeR.Models
{
    public partial class db_bibliotecaContext : DbContext
    {
        public db_bibliotecaContext()
        {
        }

        public db_bibliotecaContext(DbContextOptions<db_bibliotecaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Autor> Autors { get; set; }
        public virtual DbSet<AutorBookmark> AutorBookmarks { get; set; }
        public virtual DbSet<Editorial> Editorials { get; set; }
        public virtual DbSet<EditorialBookmark> EditorialBookmarks { get; set; }
        public virtual DbSet<Libro> Libros { get; set; }
        public virtual DbSet<LibroBookmark> LibroBookmarks { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.;database=db_biblioteca;integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Autor>(entity =>
            {
                entity.HasKey(e => e.IdAutor)
                    .HasName("PK__autor__5FC3872D2B5808D6");

                entity.ToTable("autor");

                entity.Property(e => e.IdAutor).HasColumnName("id_autor");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.BreveBio)
                    .HasMaxLength(2000)
                    .HasColumnName("breve_bio");

                entity.Property(e => e.FechaDeceso)
                    .HasColumnType("date")
                    .HasColumnName("fecha_deceso");

                entity.Property(e => e.FechaNac)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nac");

                entity.Property(e => e.ImagenUrl)
                    .HasMaxLength(200)
                    .HasColumnName("imagen_url");

                entity.Property(e => e.Nacionalidad)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nacionalidad");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<AutorBookmark>(entity =>
            {
                entity.ToTable("autor_bookmark");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Autor).HasColumnName("autor");

                entity.Property(e => e.Usuario).HasColumnName("usuario");

                entity.HasOne(d => d.AutorNavigation)
                    .WithMany(p => p.AutorBookmarks)
                    .HasForeignKey(d => d.Autor)
                    .HasConstraintName("FK__autor_boo__autor__4F7CD00D");

                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.AutorBookmarks)
                    .HasForeignKey(d => d.Usuario)
                    .HasConstraintName("FK__autor_boo__autor__4E88ABD4");
            });

            modelBuilder.Entity<Editorial>(entity =>
            {
                entity.HasKey(e => e.IdEditorial)
                    .HasName("PK__editoria__10C1DD02DA018ECE");

                entity.ToTable("editorial");

                entity.Property(e => e.IdEditorial).HasColumnName("id_editorial");

                entity.Property(e => e.ImagenUrl)
                    .HasMaxLength(200)
                    .HasColumnName("imagen_url");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.PaisOrigen)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pais_origen");

                entity.Property(e => e.SitioWeb)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("sitio_web");
            });

            modelBuilder.Entity<EditorialBookmark>(entity =>
            {
                entity.ToTable("editorial_bookmark");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Editorial).HasColumnName("editorial");

                entity.Property(e => e.Usuario).HasColumnName("usuario");

                entity.HasOne(d => d.EditorialNavigation)
                    .WithMany(p => p.EditorialBookmarks)
                    .HasForeignKey(d => d.Editorial)
                    .HasConstraintName("FK__editorial__edito__571DF1D5");

                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.EditorialBookmarks)
                    .HasForeignKey(d => d.Usuario)
                    .HasConstraintName("FK__editorial__edito__5629CD9C");
            });

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.HasKey(e => e.IdLibro)
                    .HasName("PK__libro__EC09C24E74EC9A0B");

                entity.ToTable("libro");

                entity.Property(e => e.IdLibro).HasColumnName("id_libro");

                entity.Property(e => e.AnioOrgPub).HasColumnName("anio_org_pub");

                entity.Property(e => e.AnioPub).HasColumnName("anio_pub");

                entity.Property(e => e.IdAutor).HasColumnName("id_autor");

                entity.Property(e => e.IdEditorial).HasColumnName("id_editorial");

                entity.Property(e => e.Idioma)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("idioma");

                entity.Property(e => e.ImagenUrl)
                    .HasMaxLength(200)
                    .HasColumnName("imagen_url");

                entity.Property(e => e.Sinopsis)
                    .HasMaxLength(2000)
                    .HasColumnName("sinopsis");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("titulo");

                entity.HasOne(d => d.IdAutorNavigation)
                    .WithMany(p => p.Libros)
                    .HasForeignKey(d => d.IdAutor)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__libro__id_autor__145C0A3F");

                entity.HasOne(d => d.IdEditorialNavigation)
                    .WithMany(p => p.Libros)
                    .HasForeignKey(d => d.IdEditorial)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__libro__id_editor__15502E78");
            });

            modelBuilder.Entity<LibroBookmark>(entity =>
            {
                entity.ToTable("libro_bookmark");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Libro).HasColumnName("libro");

                entity.Property(e => e.Usuario).HasColumnName("usuario");

                entity.HasOne(d => d.LibroNavigation)
                    .WithMany(p => p.LibroBookmarks)
                    .HasForeignKey(d => d.Libro)
                    .HasConstraintName("FK__libro_boo__libro__534D60F1");

                entity.HasOne(d => d.UsuarioNavigation)
                    .WithMany(p => p.LibroBookmarks)
                    .HasForeignKey(d => d.Usuario)
                    .HasConstraintName("FK__libro_boo__libro__52593CB8");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.HasIndex(e => e.Username, "UQ__usuario__F3DBC57203851CA0")
                    .IsUnique();

                entity.HasIndex(e => new { e.Nombre, e.Apellido }, "uq_nom_ap")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("clave");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Rol)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("rol");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
