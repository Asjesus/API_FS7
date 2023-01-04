
using ChapterAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace ChapterAPI.Contexts
{
    public class ChapterContext : DbContext
    {
       
            public ChapterContext()
            {
            }
            public ChapterContext(DbContextOptions<ChapterContext> options) : base(options)
            {
            }
            // vamos utilizar esse método para configurar o banco de dados
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    // cada provedor tem sua sintaxe para especificação

                    optionsBuilder.UseSqlServer("Data Source = ASJESUS-2815\\SQLEXPRESS; initial catalog = Chapterpfs7;Integrated Security = true"); // caso seja com senha : id User = o usuario ; pwd= coloca a senha
                }
            }
            // dbset representa as entidades que serão utilizadas nas operações de leitura, criação, atualização e deleção
            public DbSet<Livro> Livros { get; set; }
            public DbSet<Usuario> Usuarios { get; set; }
        
    }
}
