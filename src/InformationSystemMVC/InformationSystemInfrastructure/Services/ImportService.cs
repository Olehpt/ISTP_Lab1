using InformationSystemDomain.Model;
namespace InformationSystemInfrastructure.Services
{
    public interface ImportService<TEntity>
    {
        Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken);
    }
}
