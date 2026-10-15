using System.Collections.Generic;
using System.Linq;

namespace PR32
{
    public class CartItem
    {
        public string Article { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; } // %
        public int Quantity { get; set; }

        public decimal FinalPrice => Price - (Price * Discount / 100m);
        public decimal Sum => FinalPrice * Quantity;
        public decimal OldSum => Price * Quantity;
    }

    public static class Cart
    {
        public static List<CartItem> Items = new List<CartItem>();

        public static bool IsEmpty => Items.Count == 0;

        public static void Add(string art, string name, decimal price, decimal disc)
        {
            var item = Items.FirstOrDefault(i => i.Article == art);
            if (item != null) { item.Quantity++; return; }

            Items.Add(new CartItem
            {
                Article = art,
                Name = name,
                Price = price,
                Discount = disc,
                Quantity = 1
            });
        }

        public static void Remove(string art) => Items.RemoveAll(i => i.Article == art);

        public static void SetQty(string art, int qty)
        {
            var item = Items.FirstOrDefault(i => i.Article == art);
            if (item == null) return;
            if (qty <= 0) Items.Remove(item);
            else item.Quantity = qty;
        }

        public static decimal Total => Items.Sum(i => i.Sum);
        public static decimal DiscountSum => Items.Sum(i => i.OldSum - i.Sum);

        public static void Clear() => Items.Clear();
    }
}