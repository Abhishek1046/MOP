using DrugMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugMicroservice.Repositories
{
    public interface IDrugRepository
    {
        Drug searchDrugsByID(int drugId);
        Drug searchDrugsByName(string name);
        int getLocationQty(string name, string location);
        bool updateQuantity(string name, string location, int qty);
    }
}
