using Microsoft.EntityFrameworkCore;
using MinimalApi.Dominio.Entidades;

namespace MinimalApi.Infraestrutura.Db;

public class DbContexto : DbContext
{
    private readonly IConfiguration _configuracaoAppSettings;
    public DbContexto(DbContextOptions<DbContexto> options) : base(options)
    {
    }

    public DbSet <Administrador> Administradores { get; set; } = default!;
    public DbSet <Veiculo> Veiculos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>().HasData
        (
            new Administrador {
                Id = 1,
                Email = "admin@test.com",
                Senha = "123456",
                Perfil = "Adm"
            }
        );
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var stringConexao = _configuracaoAppSettings.GetConnectionString("MySlq")?.ToString(); //==> O "?" antes do ponto, é uma validação, se ele não achar nada, vem vazio!

            if(!string.IsNullOrEmpty(stringConexao))
            {
                optionsBuilder.UseMySql
                (
                    stringConexao, 
                    ServerVersion.AutoDetect(stringConexao)
                );
            }
        }
    }
}