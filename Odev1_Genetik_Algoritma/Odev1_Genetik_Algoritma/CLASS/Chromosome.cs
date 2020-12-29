using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev1_Genetik_Algoritma.CLASS
{
    public class ChromosomeInfo
    {
        public List<int> Chromosome { get; set; }
        public double Q { get; set; }


        public ChromosomeInfo()
        {
            List<int> s = new List<int>();
            this.Chromosome = s;
        }
    }
}
