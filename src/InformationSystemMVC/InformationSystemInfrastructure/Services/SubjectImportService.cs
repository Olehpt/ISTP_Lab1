using ClosedXML.Excel;
using InformationSystemDomain.Model;
using Microsoft.EntityFrameworkCore;
namespace InformationSystemInfrastructure.Services
{
    public class SubjectImportService:ImportService<Subject>
    {
        private readonly ProjectCsContext _context;
        public SubjectImportService(ProjectCsContext context)
        {
            _context = context;
        }
        public async Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken)
        {
           if (!stream.CanRead)
            {
                throw new ArgumentException("Stream is not readable", nameof(stream));
            }
           using (XLWorkbook workBook = new XLWorkbook(stream))
            {
                foreach(IXLWorksheet worksheet in workBook.Worksheets)
                {
                    var subjectname = worksheet.Name;
                    var subject = await _context.Subjects.FirstOrDefaultAsync(s => EF.Functions.Like(s.Name, subjectname));
                    if (subject == null)
                    {
                        subject = new Subject();
                        subject.Name = subjectname;
                        _context.Subjects.Add(subject);
                    }
                    foreach(var row in worksheet.RowsUsed().Skip(1))
                    {
                        await AddArticleAsync(row, cancellationToken, subject); //
                    }
                }
            }
        }
        private async Task AddArticleAsync(IXLRow row, CancellationToken cancellationToken, Subject subject)
        {
            Article a = new Article();
            a.Name = row.Cell(1).Value.ToString();
            a.Topic = row.Cell(2).Value.ToString();
            a.Content = row.Cell(3).Value.ToString();
            a.PublicationDate = DateOnly.FromDateTime(row.Cell(4).GetValue<DateTime>());
            a.TypeId = row.Cell(5).GetValue<int>();
            a.SubjectId = row.Cell(6).GetValue<int>();
            _context.Articles.Add(a);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
