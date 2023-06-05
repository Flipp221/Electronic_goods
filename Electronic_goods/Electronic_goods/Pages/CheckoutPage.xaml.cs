using Electronic_goods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Electronic_goods.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutPage : ContentPage
    {
        List<Tovars> aa = new List<Tovars>();
        public CheckoutPage(int a, int b)
        {
            InitializeComponent();
            AmountLbl.Text = $"Товары ({a}):";
            SumLbl.Text = $"{b} P";
            SumLbl1.Text = $"{b} P";
            FIOEntry.Text = $"{App.client.Surname} {App.client.Name} {App.client.Patronymic}";
            BindingContext = App.client;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void OrderBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                var id = ((Button)sender).CommandParameter.ToString();
                var fre = App.Db.GetTovars();

                foreach (var item in fre)
                {
                    if (item.Id == int.Parse(id))
                    {
                        item.Count = (Convert.ToInt32(item.Count.ToString()) - 1).ToString();
                        App.Db.SaveTovars(item);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("", ex.Message, "ok");
            }
            await DisplayAlert("Уведомление", "Заказ успешно оформлен", "ОК");
            await Navigation.PopAsync();
        }
    }
}