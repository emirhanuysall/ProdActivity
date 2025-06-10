using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProdActivity.Entities
{
    public class Root
    {
        public int Id { get; set; }  // Veritabanı için
        public string Type { get; set; }
        public DateTime Timestamp { get; set; }
        public List<Detection> Detections { get; set; }
    }
}
