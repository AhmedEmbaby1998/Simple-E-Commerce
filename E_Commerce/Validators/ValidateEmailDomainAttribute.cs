using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Validators
{
    public class ValidateEmailDomainAttribute:ValidationAttribute
    {
        private string _allowedDomain;

        public ValidateEmailDomainAttribute(string allowedDomain)
        {
            _allowedDomain = allowedDomain;
        }
        public override bool IsValid(object value)
        {
            return value.ToString().Split("@")[1].ToLower() == _allowedDomain;
        }
    }
}