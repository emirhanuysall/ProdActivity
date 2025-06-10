using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdActivity.Entities
{
  public  class RawDetection
    {
        public string label { get; set; }
        public double confidence { get; set; }
        public int[] bbox { get; set; }
    }
}
