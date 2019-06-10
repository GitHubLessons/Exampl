using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinancier
{
  public  class Spending
    {
        int id;
        public int ID { get { return id; } }
        string category;//категория расхода
        public string Value { set; get; }//наименование расхода
        public string Category { get { return category; } }
        public float Summ { set; get; }
        public float Quantity { set; get; }
        public DateTime Date { set; get; }


        public void SetCategory(string category)
        {
         if(IsCategoryExist(category))
            {
                this.category = category;
            }   
        }
        
        bool IsCategoryExist(string category)
        {
            bool isexist = false;

            for(int i=0;i<Financier.Categories.Count;i++)
            {
                if(Financier.Categories[i] == category)
                {
                    return true;
                }
            }
            return isexist;
        }

        public void SetId(int ID)
        {
            id = ID;
        }
    }
}
