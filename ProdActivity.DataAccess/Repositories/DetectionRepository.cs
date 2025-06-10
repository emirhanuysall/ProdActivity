using ProdActivity.Entities;
using Microsoft.EntityFrameworkCore;

namespace ProdActivity.DataAccess.Repositories
{
    public class DetectionRepository
    {
        public static event Action OnNewDetectionSaved;

        public void SaveDetectionData(Root root)
        {
            using var context = new AppDbContext();

            foreach (var detection in root.Detections)
            {
                detection.CreatedAt = DateTime.Now;
                context.Detections.Add(detection);
            }

            context.SaveChanges();

            // Event'i tetikle
            OnNewDetectionSaved?.Invoke();
        }

        public List<Detection> GetAllDetections()
        {
            using var context = new AppDbContext();
            return context.Detections.OrderByDescending(d => d.CreatedAt).ToList();
        }

        public List<Detection> GetDetectionsBetween(DateTime start, DateTime end)
        {
            using var context = new AppDbContext();
            return context.Detections
                         .Where(d => d.CreatedAt >= start && d.CreatedAt <= end)
                         .OrderByDescending(d => d.CreatedAt)
                         .ToList();
        }
    }
}
