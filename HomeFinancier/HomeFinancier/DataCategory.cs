using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinancier
{
    public class DataCategory
    {
        
        public string Name { set; get; }
        public float Summ { set; get; }

        public DataCategory(string name, float summ)
        {
            Name = name;            
            Summ = summ;
        }

      
    }
}
