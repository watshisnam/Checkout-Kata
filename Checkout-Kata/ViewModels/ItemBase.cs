using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout_Kata.ViewModels
{
    public interface IItemBase
    {
        String Name { get; }
        int UnitPrice { get; }
    }

    public class ItemA:IItemBase
    {

        public string Name => "A";

        public int UnitPrice => 10;
    }
    public class ItemB :IItemBase
    {
        public string Name => "B";
        public int UnitPrice => 15;
    }
    public class ItemC : IItemBase
    {
        public string Name => "C";
        public int UnitPrice => 40;
    }
    public class ItemD : IItemBase
    {
        public string Name=>"D";
        public int UnitPrice => 55;
    }

    public class ItemFactory
    {
        public IItemBase GetItem(ItemEnum itemToMake)
        {
            switch (itemToMake)
            {
                case ItemEnum.ItemA:
                    return GetItemA();
                case ItemEnum.ItemB:
                    return GetItemB();
                case ItemEnum.ItemC:
                    return GetItemC();
                case ItemEnum.ItemD:
                    return GetItemD();
            }

            return null;
        }
        private ItemA GetItemA()
        {
            return new ItemA();
        }
        private ItemB GetItemB()
        {
            return new ItemB();
        }
        private ItemC GetItemC()
        {
            return new ItemC();
        }
        private ItemD GetItemD()
        {
            return new ItemD();
        }
    }

    public enum ItemEnum
    {
        ItemA,ItemB,ItemC,ItemD
    }
}
