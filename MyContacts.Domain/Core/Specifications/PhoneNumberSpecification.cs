using MyContacts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContacts.Domain.Core.Specifications
{
    public class PhoneNumberSpecification
    {
        public static BaseSpecification<PhoneNumber> GetPhoneNumberById(string phoneNumber)
        {
            return new BaseSpecification<PhoneNumber>(c => c.Number == phoneNumber) { };
        }
    }
}
