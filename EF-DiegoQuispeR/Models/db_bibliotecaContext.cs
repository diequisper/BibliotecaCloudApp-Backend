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
        public virtual DbSet<Editorial> Editorials { get; set; }
        public virtual DbSet<Libro> Libros { get; set; }

        public virtual DbSet<spGetLibro> spGetLibro {  get; set; }

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
            modelBuilder.Entity<spGetLibro>().HasNoKey();

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

                entity.Property(e => e.FechaNac)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nac");

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

            modelBuilder.Entity<Editorial>(entity =>
            {
                entity.HasKey(e => e.IdEditorial)
                    .HasName("PK__editoria__10C1DD02DA018ECE");

                entity.ToTable("editorial");

                entity.Property(e => e.IdEditorial).HasColumnName("id_editorial");

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

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.HasKey(e => e.IdLibro)
                    .HasName("PK__libro__EC09C24E74EC9A0B");

                entity.ToTable("libro");

                entity.Property(e => e.IdLibro).HasColumnName("id_libro");

                entity.Property(e => e.AnioOrgPub).HasColumnName("anio_org_pub");

                entity.Property(e => e.IdAutor).HasColumnName("id_autor");

                entity.Property(e => e.IdEditorial).HasColumnName("id_editorial");

                entity.Property(e => e.Idioma)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("idioma");

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
