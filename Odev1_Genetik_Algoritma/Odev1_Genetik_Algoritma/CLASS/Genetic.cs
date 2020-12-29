using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev1_Genetik_Algoritma.CLASS
{
    public class Genetic
    {
        public int Population_Count { get; set; }
        public int Modul_Count { get; set; }
        public int NodeCount { get; set; }
        public int edgeList_Count { get; set; }
        public List<ChromosomeInfo> Population { get; set; }
    }
}
