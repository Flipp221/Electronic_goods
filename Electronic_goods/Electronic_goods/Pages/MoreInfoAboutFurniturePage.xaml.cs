using Electronic_goods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Electronic_goods.TabbedPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Electronic_goods.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreInfoAboutFurniturePage : ContentPage
    {
        Tovars tovars;
        public MoreInfoAboutFurniturePage(Tovars tov)
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

        private void Btn1_Clicked(object sender, EventArgs e)
        {
            Clear();
            Btn1.BorderWidth = 2;
            Btn1.BorderColor = Color.Black;
            tovars.Color = App.colors[0];
        }

        private void Btn2_Clicked(object sender, EventArgs e)
        {
            Clear();
            Btn2.BorderWidth = 2;
            Btn2.BorderColor = Color.Black;
            tovars.Color = App.colors[1];
        }

        private void Btn3_Clicked(object sender, EventArgs e)
        {
            Clear();
            Btn3.BorderWidth = 2;
            Btn3.BorderColor = Color.Black;
            tovars.Color = App.colors[2];
        }

        private void Btn4_Clicked(object sender, EventArgs e)
        {
            Clear();
            Btn4.BorderWidth = 2;
            tovars.Color = App.colors[3];
            Btn4.BorderColor = Color.Black;
        }

        private void Clear()
        {
            Btn1.BorderWidth = 0;
            Btn1.BorderColor = Color.Black;
            Btn2.BorderWidth = 0;
            Btn2.BorderColor = Color.Black;
            Btn3.BorderWidth = 0;
            Btn3.BorderColor = Color.Black;
            Btn4.BorderWidth = 0;
            Btn4.BorderColor = Color.Black;
        }

        private async void ForBasketBtn_Clicked(object sender, EventArgs e)
        {
            string material = "";
            if (RBtn1.IsChecked)
                material = RBtn1.Content.ToString();
            else if (RBtn2.IsChecked)
                material = RBtn2.Content.ToString();
            else if (RBtn3.IsChecked)
                material = RBtn3.Content.ToString();
            else if (RBtn4.IsChecked)
                material = RBtn4.Content.ToString();
            tovars.Material = material;
            Busket bk = new Busket(1, App.client.Id, tovars.Id);
            App.Db.SaveBasket(bk);
            await DisplayAlert("Done", "Товар успешно добавлен", "Ok");
            await Navigation.PopAsync();
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
            App.Db.DeleteFurniture(tovars.Id);
            await DisplayAlert("Done", "Товар удалён", "Ok");
            await Navigation.PopAsync();
        }
    }
}