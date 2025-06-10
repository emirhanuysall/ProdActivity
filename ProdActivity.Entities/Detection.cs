using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProdActivity.Entities
{
    public class Detection
    {
        public int Id { get; set; }  // optional, for database
        public string Label { get; set; }
        public double Confidence { get; set; }
        public string BBox { get; set; } // string olarak kaydedilecek
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
