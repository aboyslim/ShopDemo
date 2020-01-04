﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopDemo.Core.Contracts;
using ShopDemo.Core.Models;
using ShopDemo.Core.ViewModels;
using ShopDemo.DataAccess.InMemory;

namespace ShopDemo.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> context; //create instance of ProductRepository found in DataAccess.InMemory
        IRepository<ProductCategory> productCategories;
        
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> categoryContext) //constructor for initiazlizing repository
        {
            context = productContext;
            productCategories = categoryContext;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            //Return list of products
            List<Product> products = context.Collection().ToList(); //get from collection and convert to list

            return View(products);
        }

        //Create product
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.product = new Product();
            viewModel.productCategories = productCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        //Create product
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if(file != null)
                {
                    product.image = product.id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.image);
                }

                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        //Edit product
        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);

            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.product = product;
                viewModel.productCategories = productCategories.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        //Edit product
        public ActionResult Edit(Product product, string id, HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(id);

            if(productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                if (file != null)
                {
                    productToEdit.image = product.id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.image);
                }

                productToEdit.category = product.category;
                productToEdit.description = product.description;
                productToEdit.name = product.name;

                context.Commit();

                return RedirectToAction("Index");
            }           
        }

        public ActionResult Delete(string id)
        {
            Product productToDelete = context.Find(id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product productToDelete = context.Find(id);

            if (productToDelete == null)
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