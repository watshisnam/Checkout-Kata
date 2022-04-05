using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout_Kata.ViewModels;

namespace Checkout_Kata
{
    public class AppVM
    {

        public CheckOutBasket Basket { get; set; }
        public AppVM()
        {
            Basket = new CheckOutBasket();
        }
    }
}
