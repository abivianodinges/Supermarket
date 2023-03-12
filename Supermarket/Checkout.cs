namespace Supermarket
{
    public class Checkout : ICheckout
    {
        private readonly IEnumerable<Product> allProducts;
        private readonly IEnumerable<Discount> allDiscounts;

        List<string> scannedItems = new List<string>();

        public Checkout(IEnumerable<Product> allProducts,
            IEnumerable<Discount> allDiscounts)
        {
            this.allProducts = allProducts;   
            this.allDiscounts = allDiscounts;
        }
        public void Scan(string item)
        {
            scannedItems.Add(item);   
        }

        public int GetTotalPrice()
        {
            int totalPrice = 0;

            foreach(string item in scannedItems)
            {
                totalPrice += allProducts.FirstOrDefault(x => x.SKU == item).Price;
            }

            foreach(Discount discount in allDiscounts)
            {
                totalPrice -= calculateDiscount(discount);
            }

            return totalPrice;
        }

        private int calculateDiscount(Discount discount)
        {
            var scannedSkus = scannedItems.Where(x => x == discount.SKU);

            if (scannedSkus.Any() && scannedSkus.Count() > discount.Quantity)
            {
                int skuDiscount = allProducts.FirstOrDefault(x => x.SKU == discount.SKU).Price * discount.Quantity - discount.Price;
                if (skuDiscount > 0)
                {
                    int skuTotalDiscount = (scannedSkus.Count() / discount.Quantity) * skuDiscount;

                    return skuTotalDiscount;
                }
            }

            return 0;
        }
    }
}