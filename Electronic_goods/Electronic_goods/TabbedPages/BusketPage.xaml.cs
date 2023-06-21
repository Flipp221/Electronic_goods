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
        Busket busket;
        List<Tovars> aa = new List<Tovars>();
        int price = 0;
        public BusketPage()
        {
            InitializeComponent();
            BusketLst.RefreshCommand = new Command(() =>
            {
                BusketLst.IsRefreshing = false;
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateList();
        }
        void UpdateList()
        {
            aa = new List<Tovars>();
            price = 0;
            BusketLst.ItemsSource = null;
            var a = App.Db.GetBasket();
            foreach (var item in a)
            {
                if (App.client.Id == item.ClientId)
                {
                    aa.Add(App.Db.GetProjectItem(item.TovarsId));
                    price += int.Parse(App.Db.GetProjectItem(item.TovarsId).Price);
                }
            }
            BusketLst.ItemsSource = aa;
            GoodAmountLbl.Text = $"Товара ({aa.Count}):";
            GoodPriceLbl.Text = $"{price} Pуб.";
        }

        private void SwipeItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var id = ((SwipeItem)sender).CommandParameter.ToString();
                var fre = App.Db.GetBasket();

                foreach (var item in fre)
                {
                    if (item.TovarsId == int.Parse(id))
                    {
                        App.Db.DeleteTovarsInBasket(item.Id);
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

            if (App.client.RoleId == 2)
            {
                await DisplayAlert("Ошибка", "Вы не можете совершать покупки, пожалуйста войдите или зарегестрируйтесь", "Ok");
            }
            else
            {
                await Navigation.PushAsync(new CheckoutPage(aa.Count, price));
            }
        }
    }
}