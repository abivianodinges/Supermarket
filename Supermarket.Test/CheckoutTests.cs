using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Supermarket.Test
{
    [TestClass]
    public class CheckoutTests
    {
        IEnumerable<Product> products = new[]
        {
            new Product { SKU = "A", Price = 50 },
            new Product { SKU = "B", Price = 30 },
            new Product { SKU = "C", Price = 20 },
            new Product { SKU = "D", Price = 15 }
        };

        IEnumerable<Discount> discounts = new[]
        {
            new Discount { SKU = "A", Price = 130, Quantity = 3},
            new Discount { SKU= "B", Price = 45, Quantity = 2 }
        };



        [TestMethod]
        public void NoDiscounts()
        {
            IEnumerable<Discount> discount = new[] { new Discount() };
            Checkout checkout = new Checkout(products, discount);

            var itemsToPurchase = new List<string>()
            {
                "A", "B", "C", "D"
            };

            foreach (string item in itemsToPurchase)
            {
                checkout.Scan(item);
            }

            Assert.AreEqual(115, checkout.GetTotalPrice());
        }

        [TestMethod]
        public void GetTotalPrice_AllDiscounts()
        {
            Checkout checkout = new Checkout(products, discounts);
            // 7 A's, 5 B's, 1 C, 2 D's
            var itemsToPurchase = new List<string>()
            {
                "A", "C", "A", "A", "A", "A", "D", "A", "A", "B", "D", "B", "B", "B", "B"
            };

            foreach (string item in itemsToPurchase)
            {
                checkout.Scan(item);
            }

            Assert.AreEqual(480, checkout.GetTotalPrice());
        }
    }
}