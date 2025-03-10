using Microsoft.EntityFrameworkCore;
using Models.Entidades;



namespace TODO.ContextDB;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> option): base(option) { }

    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Tareas> Tareas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
                    .HasMany(u => u.Tareas)
                    .WithOne(t => t.Usuario)
                    .HasForeignKey(t => t.IdUsuario);

        base.OnModelCreating(modelBuilder);
    }
}
