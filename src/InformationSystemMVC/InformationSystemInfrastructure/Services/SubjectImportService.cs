using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Vml.Office;
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
                //
                await _context.Database.ExecuteSqlRawAsync("DELETE FROM dbo.Articles");
                //await _context.Database.ExecuteSqlRawAsync("DELETE FROM dbo.Subjects");
                await _context.SaveChangesAsync();
                //
                foreach (IXLWorksheet worksheet in workBook.Worksheets)
                {
                    var subjectname = worksheet.Name;
                    var subject = await _context.Subjects.FirstOrDefaultAsync(s => EF.Functions.Like(s.Name, subjectname));
                    if (subject == null)
                    {
                        int newsubjectid = 1;
                        var maxId = _context.Subjects.Max(e => (int?)e.SubjectId);
                        if (maxId.HasValue)
                        {
                            newsubjectid = maxId.Value + 1;
                        }
                        subject = new Subject();
                        subject.Name = subjectname;
                        //subject.SubjectId = newsubjectid;
                        _context.Subjects.Add(subject);
                        await _context.SaveChangesAsync(cancellationToken);
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
            //
            var pubtypesIDs = await _context.PublicationTypes.Select(x => x.TypeId).ToListAsync(cancellationToken);
            //
            Article a = new Article();
            a.Name = row.Cell(1).Value.ToString();
            a.Topic = row.Cell(2).Value.ToString();
            a.Content = row.Cell(3).Value.ToString();
            //
            try { a.PublicationDate = DateOnly.FromDateTime(row.Cell(4).GetValue<DateTime>()); }
            catch (Exception Ex){
                return;
            }
            //
            
            //
            try { a.TypeId = row.Cell(5).GetValue<int>(); }
            catch (Exception Ex) { return;}
            //
            if (!pubtypesIDs.Contains(a.TypeId))
            {
                return;
            }
            //
            a.SubjectId = subject.SubjectId;
            _context.Articles.Add(a);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
