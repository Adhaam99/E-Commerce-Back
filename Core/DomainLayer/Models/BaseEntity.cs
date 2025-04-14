using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        //public DateTime CreatedDate { get; set; } = DateTime.Now;
        //public DateTime UpdatedDate { get; set; } = DateTime.Now;
        //public bool IsDeleted { get; set; } = false;
    }
}
