using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdActivity.Entities
{
    public class RawRoot
    {
        public string type { get; set; }
        public double timestamp { get; set; }
        public List<RawDetection> detections { get; set; }
    }
}
