using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace InformationSystemInfrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController: ControllerBase
    {
        private record Chart1(string year, int count);
        private record Chart2(string subject, int count);
        private readonly ProjectCsContext context;
        public ChartsController(ProjectCsContext context)
        {
            this.context = context;
        }
        [HttpGet("chart1")]
        public async Task<JsonResult> GetChart1(CancellationToken token)
        {
            var responseItems = await context.Articles.GroupBy(article => article.PublicationDate.Year)
                .Select(group => new Chart1(group.Key.ToString(), group.Count())).ToListAsync(token);
            return new JsonResult(responseItems);
        }
        [HttpGet("chart2")]
        public async Task<JsonResult> GetChart2(CancellationToken token)
        {
            var allArticles = await context.Articles
            .Select(article => new { article.Subject.Name })
            .ToListAsync(token);
            var responseItems = allArticles.GroupBy(article => article.Name)
                .Select(group => new Chart2(group.Key, group.Count())).ToList(); 
            return new JsonResult(responseItems);
        }
    }

}
