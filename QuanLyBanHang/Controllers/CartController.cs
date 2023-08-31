using QuanLyBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace QuanLyBanHang.Controllers
{
    public class CartController : Controller
    {
        NorthwindDataContext da = new NorthwindDataContext("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");        

        //lay danh sach cac sp trong gio hang
        private List<CartModel> GetListCarts()
        {
            List<CartModel> carts = Session["CartModel"] as List<CartModel>; //danh sach sp trong gio hang
            if (carts == null)//chua co sp nao trong gio hang
            {
                carts = new List<CartModel>();
                Session["CartModel"] = carts;
            }
            return carts;
        }

        //them sp vao gio hang
        public ActionResult AddCart(int id)
        {
            //tao moi 1 sp trong gio hang
            CartModel c = new CartModel(id);
            //lay danh sach gio hang da co
            List<CartModel> carts = GetListCarts();
            //add sp do vao danh sach
            carts.Add(c);
            return RedirectToAction("ListCarts");
        }

        public ActionResult ListCarts()
        {
            //danh sach sp trong gio hang
            List<CartModel> carts = GetListCarts();
            ViewBag.CountProduct = carts.Sum(s => s.Quantity);
            ViewBag.Total = carts.Sum(s => s.Total);
            return View(carts);
        }

        public ActionResult OrderProduct()
        {
            try
            {
                {
                    Order order = new Order();
                    order.EmployeeID = 1;
                    order.OrderDate = DateTime.Now;
                    da.Orders.InsertOnSubmit(order);
                    da.SubmitChanges();
                    foreach (CartModel item in GetListCarts())
                    {
                        Order_Detail od = new Order_Detail();
                        od.OrderID = order.OrderID;
                        od.ProductID = item.ProductID;
                        od.UnitPrice = decimal.Parse(item.UnitPrice.ToString());
                        od.Quantity = short.Parse(item.Quantity.ToString());
                        od.Discount = 0;
                        da.Order_Details.InsertOnSubmit(od);
                    }
                    da.SubmitChanges();
                    Session["CarModel"] = null;
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("ListOrder");
        }

        public ActionResult ListOrder()
        {
            var ds = da.Orders.OrderByDescending(s => s.OrderDate).ToList();
            return View(ds);
        }

        public ActionResult DemDonHangDangQuanLy(int id)
        {
            Employee nhanVien = da.Employees.FirstOrDefault(q => q.EmployeeID == id);
            ViewBag.MaNhanVien = nhanVien.EmployeeID;
            ViewBag.HoTenNhanVien = nhanVien.FirstName + " " + nhanVien.LastName;
            ViewBag.SoDonHangDangQuanLy = da.Orders.Where(o => o.EmployeeID == id).Count();
            return View();
        }
        
        public ActionResult DemDonHangDaLap(string id)
        {
            Customer khachHang = da.Customers.FirstOrDefault(o => o.CustomerID == id);
            ViewBag.MaKhachHang = khachHang.CustomerID;
            ViewBag.HoTenKhachHang = khachHang.ContactName;
            ViewBag.SoDonHangDaLap = da.Orders.Count(p => p.CustomerID == id);
            return View();
        }
    }
}
