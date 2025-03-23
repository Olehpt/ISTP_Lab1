using InformationSystemDomain.Model;
namespace InformationSystemInfrastructure.Services
{
    public interface ExportService<TEntity>
    {
        Task WriteToAsync(Stream stream, CancellationToken cancellationToken);
    }
}
