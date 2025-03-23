using InformationSystemDomain.Model;
namespace InformationSystemInfrastructure.Services
{
    public interface DataPortServiceFactory<TEntity>
    {
        ImportService<TEntity> GetImportService(string contentType);
        ExportService<TEntity> GetExportService(string contentType);
    }
}
