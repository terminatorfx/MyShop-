﻿using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
   public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;

            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
                cache["productCategories"] = productCategories;
            }
        }

        public void Committ()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToEdit = productCategories.Find(x=>x.Id==productCategory.Id);

            if(productCategoryToEdit!=null)
            {
                productCategoryToEdit.Category = productCategory.Category;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productCategories.Find(x => x.Id == Id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete (string Id)
        {
            ProductCategory productCategoryToDelete = productCategories.Find(x => x.Id == Id);

            if (productCategoryToDelete != null)
            {
                productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product Category Not Found");
            }
        }
    }
}
