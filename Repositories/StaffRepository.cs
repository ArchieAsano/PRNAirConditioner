using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class StaffRepository: BaseRepository<StaffMember>
    {
        private readonly AirConditionerShop2024DbContext _db;
        public StaffRepository(AirConditionerShop2024DbContext db) : base(db)
        {
            _db = db;
        }
        public StaffMember checkLogin (string username, string password)
        {
            var user = _db.StaffMembers.FirstOrDefault(st=>st.EmailAddress.Equals(username) && st.Password.Equals(password));
            return user;
        }

    }
    public static class CurrentUser
    {
        public static StaffMember LoggedUser {  get; set; }
    }
}
