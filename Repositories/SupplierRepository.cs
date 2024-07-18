using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class SupplierRepository :BaseRepository <SupplierCompany>
    {
        private readonly AirConditionerShop2024DbContext _db;
        public SupplierRepository(AirConditionerShop2024DbContext db) : base(db)
        {
            _db = db;
        }
    }
}
