using System;
using System.Collections.Generic;

namespace ProdActivity.Web.Models
{
    public class DetectionViewModel
    {
        public string Label { get; set; }
        public double Confidence { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class DashboardViewModel
    {
        public double AverageConfidence { get; set; }
        public List<DetectionViewModel> MostDetectedProducts { get; set; }
        public List<DetectionViewModel> Detections { get; set; }
    }
}
