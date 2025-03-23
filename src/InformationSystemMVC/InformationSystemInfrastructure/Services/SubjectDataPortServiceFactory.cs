using InformationSystemDomain.Model;
namespace InformationSystemInfrastructure.Services
{
    public class SubjectDataPortServiceFactory : DataPortServiceFactory<Subject>
    {
        private readonly ProjectCsContext _context;
        public SubjectDataPortServiceFactory(ProjectCsContext context)
        {
            _context = context;
        }
        public ImportService<Subject> GetImportService(string contentType)
        {
            if (contentType is "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new SubjectImportService(_context);
            }
            throw new NotImplementedException($"Import service for {contentType} is not implemented");
        }
        public ExportService<Subject> GetExportService(string contentType)
        {
            if (contentType is "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new SubjectExportService(_context);
            }
            throw new NotImplementedException($"Export service for {contentType} is not implemented");
        }
    }
}
