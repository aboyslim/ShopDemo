using ShopDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDemo.Core.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> products { get; set; }

        public IEnumerable<ProductCategory> productCategories { get; set; }
    }
}
