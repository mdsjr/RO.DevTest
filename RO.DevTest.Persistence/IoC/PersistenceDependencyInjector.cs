using Microsoft.Extensions.DependencyInjection;

namespace RO.DevTest.Persistence.IoC
{
    public static class PersistenceDependencyInjector
    {
        public static IServiceCollection InjectPersistenceDependencies(this IServiceCollection services)
        {
            // Configurações de persistência (ex.: repositórios)
            // Como o DbContext agora é configurado no Program.cs, não precisamos de AddDbContext aqui
            return services;
        }
    }
}