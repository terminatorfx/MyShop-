using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;


        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }
        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories= context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if(!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.Committ();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = context.Find(Id);

            if(productCategory==null)
            {
                throw new Exception("Product Category Not Found");
            }
            else
            {
                return View("Edit", productCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);

            if (productCategoryToEdit == null)
            {
                throw new Exception("Product Category Not Found");
            }
            else
            {
                productCategoryToEdit.Category = productCategory.Category;
                context.Committ();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);

            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View("Delete", productCategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);

            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
               // context.Committ();
                return RedirectToAction("Index");
            }
        }
    }
}