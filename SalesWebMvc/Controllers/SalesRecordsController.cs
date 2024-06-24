using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GroupingSearch(DateTime? initialDate, DateTime? endDate)
        {
            if (!initialDate.HasValue)
            {
                initialDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!endDate.HasValue)
            {
                endDate = DateTime.Now;
            }
            ViewData["minDate"] = initialDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = endDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordService.GetSalesByDateGroupingAsync(initialDate, endDate);
            return View(result);
        }
        public async Task<IActionResult> SimpleSearch(DateTime? initialDate, DateTime? endDate)
        {
            if (!initialDate.HasValue)
            {
                initialDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!endDate.HasValue)
            {
                endDate = DateTime.Now;
            }
            ViewData["minDate"] = initialDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = endDate.Value.ToString("yyyy-MM-dd");
            var result = await _salesRecordService.GetSalesByDateAsync(initialDate, endDate);
            return View(result);
        }
    }
}
