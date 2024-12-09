using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyContacts.Domain.Core.Models
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; }
    }
}
