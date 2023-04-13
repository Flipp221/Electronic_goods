using Electronic_goods.Models;
using Electronic_goods.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Electronic_goods.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusketPage : ContentPage
    {
        List<Furniture> aa = new List<Furniture>();
        int price = 0;
        public BusketPage()
        {
            InitializeComponent();
            BusketLst.RefreshCommand = new Command(() =>
            {
                BusketLst.IsRefreshing = false; //выключить анимацию обновления 
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateList();
        }
        void UpdateList()
        {
            aa = new List<Furniture>();
            price = 0;
            BusketLst.ItemsSource = null;
            var a = App.Db.GetBasket();
            foreach (var item in a)
            {
                if (App.client.Id == item.ClientId)
                {
                    aa.Add(App.Db.GetProjectItem(item.FurnitureId));
                    price += int.Parse(App.Db.GetProjectItem(item.FurnitureId).Price);
                }
            }
            BusketLst.ItemsSource = aa;
            GoodAmountLbl.Text = $"Товара ({aa.Count}):";
            GoodPriceLbl.Text = $"{price} P";
        }

        private void SwipeItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var id = ((SwipeItem)sender).CommandParameter.ToString();
                var fre = App.Db.GetBasket();

                foreach (var item in fre)
                {
                    if (item.FurnitureId == int.Parse(id))
                    {
                        App.Db.DeleteFurnitureInBusket(item.Id);
                        break;
                    }
                }
                UpdateList();
            }
            catch (Exception ex)
            {
                DisplayAlert("", ex.Message, "ok");
            }
        }

        private async void OrderBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CheckoutPage(aa.Count, price));
        }
    }
}