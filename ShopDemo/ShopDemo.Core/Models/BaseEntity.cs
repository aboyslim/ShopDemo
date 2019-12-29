using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDemo.Core.Models
{
    public abstract class BaseEntity
    {
        public string id { get; set; }
        public DateTimeOffset createdAt { get; set; }

        public BaseEntity()
        {
            this.id = Guid.NewGuid().ToString();
            this.createdAt = DateTime.Now;
        }
    }
}
