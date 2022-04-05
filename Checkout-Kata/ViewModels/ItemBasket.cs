using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Checkout_Kata.ViewModels
{
    public class CheckOutBasket:INotifyPropertyChanged
    {
        public CheckOutBasket()
        {
            _factory = new ItemFactory();
            Basket = new ObservableCollection<BasketItem>();
            Basket.CollectionChanged += Basket_CollectionChanged;
            AvailableItems = new List<AvailableItem>()
            {
                new AvailableItem()
                {
                    Item = ItemEnum.ItemA,
                    Name = ItemEnum.ItemA.ToString()
                },new AvailableItem()
                {
                    Item = ItemEnum.ItemB,
                    Name = ItemEnum.ItemB.ToString()
                },new AvailableItem()
                {
                    Item = ItemEnum.ItemC,
                    Name = ItemEnum.ItemC.ToString()
                },new AvailableItem()
                {
                    Item = ItemEnum.ItemD,
                    Name = ItemEnum.ItemD.ToString()
                }
            };
        }

        private void Basket_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ReCalc();
        }

        private void ReCalc()
        {
            var sumItemAAndItemC = Basket.Where(b => b.Item is ItemA || b.Item is ItemC)
                .Sum(b => b.Item.UnitPrice * b.Quantity);
            //For Item A and C its a simple calculation 


            var bBasket = Basket.Where(b => b.Item is ItemB).ToArray();
            var sumItemB = (bBasket.Sum(b => b.Quantity) >= 3) ? 
                (bBasket.Sum(b => b.Quantity) / 3) * 40 + (bBasket.Sum(b => b.Quantity) % 3) * 15 
                : bBasket.Sum(b => b.Quantity * b.Item.UnitPrice);
            //For Item B it gets slightly more complicated, We first divide the total quantity by 3 , and multiply this by 4 , This is the Promo applied 
            // we then mod the total quantity by 3 , and this will be the remainder and for this we do a straight calculation.

            var dBasket = Basket.Where(b => b.Item is ItemD).ToArray();
            decimal sumItemD = 0;
            foreach (var basketItem in dBasket)
            {
                if (basketItem.Quantity % 2 == 0)
                {
                    var standardPrice = (basketItem.Quantity * basketItem.Item.UnitPrice);
                    sumItemD += (decimal)(standardPrice - standardPrice * 0.25);
                }
                else
                {
                    if (basketItem.Quantity > 1)
                    {
                        var standardPrice = (basketItem.Quantity * basketItem.Item.UnitPrice);
                        var standardPriceMinusOne = ((basketItem.Quantity-1) * basketItem.Item.UnitPrice);
                        sumItemD += (decimal)(standardPrice - standardPriceMinusOne * 0.25);
                    }
                    else
                    {
                        sumItemD += (basketItem.Quantity * basketItem.Item.UnitPrice);
                    }
                }
            }

            Total = sumItemAAndItemC + sumItemD + sumItemB;
            OnPropertyChanged(nameof(BPromo));
            OnPropertyChanged(nameof(DPromo));


        }

        public List<AvailableItem> AvailableItems { get; set; } 
        public AvailableItem SelectedItem { get; set; }
        private int _quantity;
        public int Quantity { 
            get=>_quantity;
            set
            {
                if (value == _quantity) return;
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        private decimal _total;
        public decimal Total
        {
            get => _total;
            set
            {
                if (value == _total) return;
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        public Boolean BPromo
        {
            get => Basket.Where(b=>b.Item is ItemB).Sum(b=>b.Quantity) >= 3;
        }
        public Boolean DPromo
        {
            get => Basket.Where(b => b.Item is ItemD).Sum(b => b.Quantity) >= 2;
        }
        public ObservableCollection<BasketItem> Basket { get; set; }
    
        private ItemFactory _factory;
        private BaseCommand _addItemCommand;
        public BaseCommand AddItemCommand => _addItemCommand ?? (_addItemCommand = new BaseCommand(AddItemToBasket));


        public void AddItemToBasket()
        {
            if (SelectedItem == null) return;
            var itemToAdd = _factory.GetItem(SelectedItem.Item);
            if (itemToAdd != null && Quantity > 0)
            {
                Basket.Add(new BasketItem(itemToAdd, Quantity));
                MessageBox.Show($"{itemToAdd.Name} Added to Basket","Checkout Kata",MessageBoxButton.OK,MessageBoxImage.None);
                Quantity = 0;
            }
            else
            {
                MessageBox.Show($"An error occurred when adding {itemToAdd.Name} to basket", "Checkout Kata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class BasketItem
    {
        public BasketItem(IItemBase item, int quantity)
        {
            Quantity = quantity;
            Item = item;
        }

        public IItemBase Item { get; private set; }
        public int Quantity { get; private set; }
    }

    public class AvailableItem
    {
        public ItemEnum Item { get; set; }
        public String Name { get; set; }
    }
}
