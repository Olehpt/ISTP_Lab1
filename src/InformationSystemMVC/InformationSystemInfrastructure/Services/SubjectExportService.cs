using Microsoft.EntityFrameworkCore;   
using ClosedXML.Excel;
using InformationSystemDomain.Model;
namespace InformationSystemInfrastructure.Services
{
    public class SubjectExportService : ExportService<Subject>
    {
        private static readonly IReadOnlyList<string> HeaderNames =
            new string[]
            {
                "Name",
                "Topic",
                "Content",
                "PublicationDate",
                "TypeId",
                "SubjectId",
            };
        private readonly ProjectCsContext _context;
        public SubjectExportService(ProjectCsContext context)
        {
            _context = context;
        }
        private static void WriteHeaders(IXLWorksheet worksheet)
        {
            for (int i = 0; i < HeaderNames.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = HeaderNames[i];
            }
            worksheet.Row(1).Style.Font.Bold = true;
        }
        private void WriteArticle(IXLWorksheet worksheet, Article article, int rowIndex)
        {
            worksheet.Cell(rowIndex, 1).Value = article.Name;
            worksheet.Cell(rowIndex, 2).Value = article.Topic;
            worksheet.Cell(rowIndex, 3).Value = article.Content;
            worksheet.Cell(rowIndex, 4).Value = article.PublicationDate.ToString();
            worksheet.Cell(rowIndex, 5).Value = article.TypeId;
            worksheet.Cell(rowIndex, 6).Value = article.SubjectId;
        }
        private void WriteSubjects(XLWorkbook workbook, ICollection<Subject> subjects)
        {
            foreach(var subject in subjects)
            {
                var worksheet = workbook.Worksheets.Add(subject.Name);
                WriteHeaders(worksheet);
                int rowIndex = 2;
                foreach (var article in subject.Articles)
                {
                    WriteArticle(worksheet, article, rowIndex);
                    rowIndex++;
                }
            }
        }
        public async Task WriteToAsync(Stream stream, CancellationToken cancellationToken)
        {
            if (!stream.CanWrite)
            {
                throw new ArgumentException("Stream is not writable", nameof(stream));
            }
            var workbook = new XLWorkbook();
            WriteSubjects(workbook, await _context.Subjects.Include(s => s.Articles).ToListAsync(cancellationToken));
            workbook.SaveAs(stream);
        }
    }   
}
