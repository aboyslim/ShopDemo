using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching; //calling to store temp/in memory data
using ShopDemo.Core.Models; //need to call Products class from Core Models

namespace ShopDemo.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default; //create and setup cache
        List<Product> products; //create internal list of Product

        public ProductRepository() //contructor for standard initialization
        {
            products = cache["products"] as List<Product>; //when launched, will look into cache to find list of products

            //if list not found in cache, then create new list
            if(products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit() //when products are added, they won't save right away
        {
            cache["products"] = products;
        }

        public void Insert(Product p) //Add product to Product list
        {
            products.Add(p);
        }

        public void Update(Product product) 
        {
            Product productToUpdate = products.Find(p => p.id == product.id); //look into database to find product to update

            if(productToUpdate != null) //if found then update, otherwise throw exception
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product Not Found!");
            }
        }

        public Product Find(string id)
        {
            Product product = products.Find(p => p.id == id);

            if (product != null) //if find product then return it
            {
                return product;
            }
            else
            {
                throw new Exception("Product Not Found!");
            }
        }

        public IQueryable<Product> Collection() //List that can be queried
        {
            return products.AsQueryable();
        }

        public void Delete(string id)
        {
            Product productToDelete = products.Find(p => p.id == id);

            if (productToDelete != null) //if product found, then remove from list, otherwise throw exception
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Not Found!");
            }
        }

    }
}
