using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FastFood.PayStream.Infra.Persistence;

namespace FastFood.PayStream.Migrator;

/// <summary>
/// Programa que executa migrations do Entity Framework Core automaticamente.
/// Segue o padrão do projeto auth-lambda, lendo a connection string do appsettings.json
/// e aplicando migrations pendentes.
/// </summary>
class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Iniciando processo de migração do banco de dados...");

            // Carregar configuração de appsettings.json e appsettings.Development.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables() // Priorizar variáveis de ambiente
                .Build();

            // Obter connection string, priorizando variável de ambiente se existir
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") 
                ?? configuration.GetConnectionString("DefaultConnection");

            // Validar que connection string não é nula/vazia
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                Console.Error.WriteLine("ERRO: Connection string 'DefaultConnection' não foi encontrada.");
                Console.Error.WriteLine("Configure a connection string em appsettings.json ou na variável de ambiente 'ConnectionStrings__DefaultConnection'.");
                Environment.Exit(1);
                return;
            }

            Console.WriteLine("Connection string configurada.");

            // Criar DbContextOptionsBuilder e configurar com PostgreSQL
            var optionsBuilder = new DbContextOptionsBuilder<PayStreamDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            // Criar instância de PayStreamDbContext com as opções
            using var context = new PayStreamDbContext(optionsBuilder.Options);

            // Verificar migrations pendentes
            Console.WriteLine("Verificando migrations pendentes...");
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                Console.WriteLine($"Encontradas {pendingMigrations.Count()} migration(s) pendente(s):");
                foreach (var migration in pendingMigrations)
                {
                    Console.WriteLine($"  - {migration}");
                }

                // Aplicar migrations pendentes
                Console.WriteLine("Aplicando migrations...");
                await context.Database.MigrateAsync();
                Console.WriteLine("Migrations aplicadas com sucesso!");
            }
            else
            {
                Console.WriteLine("Nenhuma migration pendente. Banco de dados está atualizado.");
            }

            Console.WriteLine("Processo de migração concluído com sucesso.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"ERRO ao executar migrations: {ex.Message}");
            Console.Error.WriteLine($"Detalhes: {ex}");
            Environment.Exit(1);
        }
    }
}
