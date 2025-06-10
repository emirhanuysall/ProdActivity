using Microsoft.AspNetCore.Mvc;
using ProdActivity.DataAccess.Repositories; // Veritabanı erişim katmanını ekleyin
using ProdActivity.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProdActivity.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DetectionRepository _detectionRepository;

        public DashboardController()
        {
            _detectionRepository = new DetectionRepository();
        }

        public IActionResult Index()
        {
            var detections = _detectionRepository.GetAllDetections();
            var viewModel = new DashboardViewModel
            {
                Detections = detections.Select(d => new DetectionViewModel
                {
                    Label = d.Label,
                    Confidence = d.Confidence,
                    CreatedAt = d.CreatedAt
                }).ToList(),
                AverageConfidence = detections.Any() ? detections.Average(d => d.Confidence) : 0,
                MostDetectedProducts = detections.GroupBy(d => d.Label)
                                                  .OrderByDescending(g => g.Count())
                                                  .Take(5)
                                                  .Select(g => new DetectionViewModel
                                                  {
                                                      Label = g.Key,
                                                      Confidence = g.Count()
                                                  }).ToList()
            };

            return View(viewModel);
        }
    }
}