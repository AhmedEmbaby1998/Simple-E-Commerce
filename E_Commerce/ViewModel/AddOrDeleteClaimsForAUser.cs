using System.Collections.Generic;
using System.Security.Claims;

namespace E_Commerce.Models.FormsData
{
    public class AddOrDeleteClaimsForAUser
    {
        public string UserId { set; get; }
        public List<UserClaim> Claims {  set; get; }
        public AddOrDeleteClaimsForAUser()
        {
            Claims=new List<UserClaim>();
        }
    }

    public class UserClaim
    {
        public string ClaimType { set; get; }
        public bool Checked { set; get; }
    }
}