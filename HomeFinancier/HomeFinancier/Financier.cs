using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinancier
{
   static class Financier
    {
        static  public List<string> Categories = new List<string>();
        static public List<Spending> spendingList = new List<Spending>();
        static public Spending CurrentSpending { set; get; }
        static public string CurrentCategory { set; get; }
        static public void DeleteSpending(int ID)
        {
            for(int i=0;i<spendingList.Count;i++)
            {
                if(spendingList[i].ID==ID)
                {
                    spendingList.Remove(spendingList[i]);
                    break;
                }
            }
        }

        static public Spending FinfSpending(int id)
        {
            Spending find = new Spending();
            for(int i = 0;i< spendingList.Count;i++)
            {
                if(spendingList[i].ID ==id)
                {
                    Console.WriteLine(spendingList[i].Value + " расход ");
                    Console.WriteLine(spendingList[i].Quantity + " количество ");
                    return spendingList[i];
                }
            }
            return find;
        } 

        static public bool IsCategoryExist(string category)
        {
            bool find = false;
            for(int i=0;i<Categories.Count;i++)
            {
                if(Categories[i] == category)
                {
                    return true;
                }
            }
            return find;
        }
    }
}
