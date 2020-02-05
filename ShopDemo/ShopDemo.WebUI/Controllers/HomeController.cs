﻿using ShopDemo.Core.Contracts;
using ShopDemo.Core.Models;
using ShopDemo.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopDemo.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> context; //create instance of ProductRepository found in DataAccess.InMemory
        IRepository<ProductCategory> productCategories;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> categoryContext) //constructor for initiazlizing repository
        {
            context = productContext;
            productCategories = categoryContext;
        }

        public ActionResult Index(string Category=null)
        {
            List<Product> products;
            List<ProductCategory> categories = productCategories.Collection().ToList();

            if (Category == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p=>p.category==Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.products = products;
            model.productCategories = categories;


            return View(model);
        }

        public ActionResult Details(string id)
        {
            Product product = context.Find(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}