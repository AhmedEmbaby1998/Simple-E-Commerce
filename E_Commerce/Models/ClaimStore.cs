using System.Collections.Generic;
using System.Security.Claims;

namespace E_Commerce.Models
{
    public static class ClaimStore
    {
        public static List<Claim> Claims { set; get; } = new List<Claim>
        {
            new Claim("Add Role", "Add Role"),
            new Claim("Edit Role", "Edit Role"),
            new Claim("Delete Role", "Delete Role"),
        };
    }
}