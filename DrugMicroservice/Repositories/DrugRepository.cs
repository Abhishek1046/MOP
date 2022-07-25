using DrugMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugMicroservice.Repositories
{
    public class DrugRepository : IDrugRepository
    {
        public static List<Drug> drugList = new List<Drug>()
        {
             new Drug
             {
                DrugId = 1,
                Name = "Paracip-500",
                Manufacturer = "Mankind",
                ManufacturedDate = new DateTime(2020, 09, 21),
                ExpiryDate = new DateTime(2023, 09, 20),
                UnitCost = 5.00,
                LocQty = new Dictionary<string, int>()
                {
                    {"Pune",50 },
                    {"Bangalore",80 },
                    {"Chennai",60 }
                }
             },
              new Drug
             {
                DrugId = 2,
                Name = "Septilin",
                Manufacturer = "Himalaya",
                ManufacturedDate = new DateTime(2018, 05, 15),
                ExpiryDate = new DateTime(2023, 05, 15),
                UnitCost = 10.00,
                LocQty = new Dictionary<string, int>()
                {
                    {"Kolkatta",55 },
                    {"Bangalore",60 },
                    {"Kochi",80 }
                }
             },
               new Drug
             {
                DrugId = 3,
                Name = "Ativan",
                Manufacturer = "Sun Pharma",
                ManufacturedDate = new DateTime(2018, 02, 10),
                ExpiryDate = new DateTime(2025, 01, 05),
                UnitCost = 10.00,
                LocQty = new Dictionary<string, int>()
                {
                    {"Mumbai",55 },
                    {"Bangalore",60 },
                    {"Gujrat",80 }
                }
             }
        };

        public virtual int getLocationQty(string name, string location)
        {
            int qty = 0;
            Drug item = drugList.Where(x => x.Name == name).FirstOrDefault();
            foreach (var temp in item.LocQty)
            {
                if (temp.Key == location)
                {
                    qty = temp.Value;
                }
            }
            return qty;
        }

        public virtual Drug searchDrugsByID(int drugId)
        {
            var item = drugList.Where(x => x.DrugId == drugId).FirstOrDefault();
            if (item == null)
                return null;
            return item;
        }
            
        public virtual Drug searchDrugsByName(string name)
        {
            var item = drugList.Where(x => x.Name == name).FirstOrDefault();
            if (item == null)
                return null;
            return item;
        }

        public bool updateQuantity(string name, string location, int qty)
        {
            var item = drugList.Where(x => x.Name == name).FirstOrDefault();
            try
            {
                item.LocQty[location] -= qty;
                return true;
            }
            catch (Exception) { return false; }

        }
    }
}
