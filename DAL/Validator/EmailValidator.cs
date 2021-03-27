using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL.Validator
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EmailValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (value.ToString().Any(c => c == '@' && c == '.') && Regex.Matches(value.ToString(), @"\P{IsCyrillic}").Count == 0)
            {
                return true;
            };
            return false;
        }
    }
}
