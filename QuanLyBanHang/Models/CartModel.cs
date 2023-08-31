using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyBanHang.Models
{
    public class CartModel
    {
        NorthwindDataContext da = new NorthwindDataContext("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal? Total { get { return UnitPrice * Quantity; } }
        public CartModel(int productID)
        {
            Product p = da.Products.FirstOrDefault(s => s.ProductID == productID);
            this.ProductID = p.ProductID;
            this.ProductName = p.ProductName;
            this.UnitPrice = p.UnitPrice;
            this.Quantity = 1;
        }
    }
}