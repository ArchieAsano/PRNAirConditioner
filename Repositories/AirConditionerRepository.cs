using Microsoft.IdentityModel.Tokens;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AirConditionerRepository : BaseRepository<AirConditioner>
    {
        private readonly AirConditionerShop2024DbContext _db;
        public AirConditionerRepository(AirConditionerShop2024DbContext db): base(db)
        {
             _db = db;
        }
        public AirConditioner Findbyid(AirConditioner airConditioner)
        {
            return _db.AirConditioners.FirstOrDefault(ac => ac.AirConditionerId == airConditioner.AirConditionerId);
        }
        public AirConditioner Update (AirConditioner input)
        {
            var update = Findbyid(input);
            if (update != null)
            {
                update.AirConditionerName = input.AirConditionerName;
                update.SoundPressureLevel = input.SoundPressureLevel;
                update.Warranty = input.Warranty;
                update.FeatureFunction = input.FeatureFunction; 
                update.DollarPrice = input.DollarPrice;
                update.Quantity = input.Quantity;   
                update.SupplierId = input.SupplierId;
            }
            return update;
        }
        public IEnumerable<AirConditioner> Search(string function, int quantity)
        {
            var result = _db.AirConditioners.Where(ac=> (string.IsNullOrEmpty(function) || ac.FeatureFunction.ToLower().Contains(function.ToLower()))
            && (string.IsNullOrEmpty(quantity.ToString()) || ac.Quantity == quantity)).ToList();
            return result;
        }
        public bool checkValidate (AirConditioner airConditioner, out string error)
        {
            if (airConditioner.AirConditionerId == 0
                || airConditioner.AirConditionerName.IsNullOrEmpty()
                || airConditioner.Warranty.IsNullOrEmpty()
                || airConditioner.SoundPressureLevel.IsNullOrEmpty()
                || airConditioner.FeatureFunction.IsNullOrEmpty()
                || airConditioner.SupplierId.IsNullOrEmpty()
                || airConditioner.DollarPrice == 0
                || airConditioner.Quantity == 0)
            {
                error = "Please Input Missed Information";
                return false;
            }
            if(airConditioner.AirConditionerName.Length < 5 || airConditioner.AirConditionerName.Length > 90)
            {
                error = "Wrong Name Length Range";
                return false;
            }
            if(airConditioner.Quantity < 0 || airConditioner.Quantity > 4000000)
            {
                error = "Invalid Quantity Value";
                return false;
            }
            if (airConditioner.DollarPrice < 0 || airConditioner.DollarPrice > 4000000)
            {
                error = "Invalid Price Value";
                return false;
            }
            if(checkString(airConditioner.AirConditionerName) == false)
            {
                error = "Wrong Name Format";
                return false;
            }
            error = string.Empty;
            return true;
        }
        private bool checkString(string input)
        {
            string[] words = input.Split(' ');
            foreach (var item in words)
            {
                if (!char.IsUpper(item[0]) || !char.IsDigit(item[0]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
