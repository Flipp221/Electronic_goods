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
    public partial class InfoTovarsPage : ContentPage
    {
        Tovars tovars;
        public InfoTovarsPage(Tovars tov)
        {
            tovars = tov;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.client.RoleId == 0)
                AdminPanel.IsVisible = false;
            else if (App.client.RoleId == 1)
                AdminPanel.IsVisible = true;
        }


        private async void ForBasketBtn_Clicked(object sender, EventArgs e)
        {
            if (tovars.Count == 0.ToString())
            {
                await DisplayAlert("Error", "В данный момент товар отсутствует в магазине", "Ok");
            }
            else
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
                            Report report = new Report(item.Name, item.Count = "1", $"{App.client.Surname} {App.client.Name} {App.client.Patronymic}");
                            App.Db.SaveReport(report);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("", ex.Message, "ok");
                }
                string material = "";
                tovars.Material = material;
                Busket bk = new Busket(1, App.client.Id, tovars.Id);
                App.Db.SaveBasket(bk);
                await DisplayAlert("Done", "Товар успешно добавлен", "Ok");
                await Navigation.PopAsync();
            }
        }

        private async void BackBtn_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void EditBtn_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTovarsPage(tovars));
        }

        private async void deleteBtn_Tapped(object sender, EventArgs e)
        {
            App.Db.DeleteTovars(tovars.Id);
            await DisplayAlert("Done", "Товар удалён", "Ok");
            await Navigation.PopAsync();
        }
    }
}