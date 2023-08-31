using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Controllers
{
    public class ProductController : Controller
    {
        NorthwindDataContext da = new NorthwindDataContext("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
        // GET: ListProduct
        public ActionResult ListProduct()
        {
            List<Product> ListProduct = da.Products.ToList();
            return View(ListProduct);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            Product p = da.Products.Where(q => q.ProductID == id).FirstOrDefault();
            return View(p);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product, FormCollection collection)
        {
            try
            {
                Product p = product;
                da.Products.InsertOnSubmit(p);
                da.SubmitChanges();              
                return RedirectToAction("ListProduct");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                Product p = da.Products.First(s => s.ProductID == id);
                p.ProductName = product.ProductName;
                p.SupplierID = product.SupplierID;
                p.CategoryID = product.CategoryID;
                p.QuantityPerUnit = product.QuantityPerUnit;
                p.UnitPrice = product.UnitPrice;
                p.UnitsInStock = product.UnitsInStock;
                p.UnitsOnOrder = product.UnitsOnOrder;
                p.ReorderLevel = product.ReorderLevel;
                p.Discontinued = product.Discontinued;
                da.SubmitChanges();
                return RedirectToAction("ListProduct");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Product p = da.Products.First(s => s.ProductID == id);
                da.Products.DeleteOnSubmit(p);
                da.SubmitChanges();
                return RedirectToAction("ListProduct");
            }
            catch
            {
                return View();
            }
        }
    }
}
