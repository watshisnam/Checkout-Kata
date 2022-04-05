using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout_Kata.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Checkout_Kata
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void NoPromoApplied()
        {
            CheckOutBasket checkOutBasket = new CheckOutBasket();
            ItemFactory factory = new ItemFactory();
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemA),1));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemB),1));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemC),1));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemD),1));
            Assert.AreEqual(120,checkOutBasket.Total);
        }
        [TestMethod]
        public void BPromoApplied()
        {
            CheckOutBasket checkOutBasket = new CheckOutBasket();
            ItemFactory factory = new ItemFactory();
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemB), 4));
            Assert.AreEqual(55, checkOutBasket.Total);
        }
        [TestMethod]
        public void DPromoApplied()
        {
            CheckOutBasket checkOutBasket = new CheckOutBasket();
            ItemFactory factory = new ItemFactory();
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemD), 3));
            Assert.AreEqual(137.05, checkOutBasket.Total);
        }
        [TestMethod]
        public void AllPromoApplied()
        {
            CheckOutBasket checkOutBasket = new CheckOutBasket();
            ItemFactory factory = new ItemFactory();
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemA), 50));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemB), 50));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemC), 50));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemD), 50));
            Assert.AreEqual(5232.5, checkOutBasket.Total);
        }
        [TestMethod]
        public void ExtremePromoApplied()
        {
            CheckOutBasket checkOutBasket = new CheckOutBasket();
            ItemFactory factory = new ItemFactory();
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemA), 20));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemB), 20));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemB), 3));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemB), 3));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemB), 3));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemB), 3));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemB), 3));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemC), 20));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemD), 20));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemD), 40));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemD), 50));
            checkOutBasket.Basket.Add(new BasketItem(factory.GetItem(ItemEnum.ItemD), 60));
            Assert.AreEqual(8482.5, checkOutBasket.Total);
        }
    }
}
