using ShopDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ShopDemo.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default; //create and setup cache
        List<ProductCategory> productCategories; //create internal list of Product

        public ProductCategoryRepository() //contructor for standard initialization
        {
            productCategories = cache["productCategories"] as List<ProductCategory>; //when launched, will look into cache to find list of products

            //if list not found in cache, then create new list
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void Commit() //when products are added, they won't save right away
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p) //Add product to Product list
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory categoryToUpdate = productCategories.Find(p => p.id == productCategory.id); //look into database to find product to update

            if (categoryToUpdate != null) //if found then update, otherwise throw exception
            {
                categoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Not Found!");
            }
        }

        public ProductCategory Find(string id)
        {
            ProductCategory productCategory = productCategories.Find(p => p.id == id);

            if (productCategory != null) //if find product then return it
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Not Found!");
            }
        }

        public IQueryable<ProductCategory> Collection() //List that can be queried
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string id)
        {
            ProductCategory categoryToDelete = productCategories.Find(p => p.id == id);

            if (categoryToDelete != null) //if product found, then remove from list, otherwise throw exception
            {
                productCategories.Remove(categoryToDelete);
            }
            else
            {
                throw new Exception("Product Not Found!");
            }
        }
    }
}
