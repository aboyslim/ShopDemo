using ShopDemo.Core.Contracts;
using ShopDemo.Core.Models;
using ShopDemo.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDemo.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context; //create instance of ProductRepository found in DataAccess.InMemory

        public ProductCategoryManagerController(IRepository<ProductCategory> context) //constructor for initiazlizing repository
        {
            this.context = context;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            //Return list of products
            List<ProductCategory> productCategories = context.Collection().ToList(); //get from collection and convert to list

            return View(productCategories);
        }

        //Create product
        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        //Create product
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        //Edit product
        public ActionResult Edit(string id)
        {
            ProductCategory productCategory = context.Find(id);

            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]
        //Edit product
        public ActionResult Edit(ProductCategory productCategory, string id)
        {
            ProductCategory categoryToEdit = context.Find(id);

            if (categoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }

                categoryToEdit.category = productCategory.category;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string id)
        {
            ProductCategory categoryToDelete = context.Find(id);

            if (categoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(categoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory categoryToDelete = context.Find(id);

            if (categoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}