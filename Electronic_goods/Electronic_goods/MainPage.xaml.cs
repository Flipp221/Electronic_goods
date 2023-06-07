using Electronic_goods.Models;
using Electronic_goods.TabbedPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Electronic_goods
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void LoginPageBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void RegistrationPageBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        private async void GuestAuthBtn_Clicked(object sender, EventArgs e)
        {
            var cl = new Client("Гость", "", "", "", "", "1", "1", 2);
            App.client = cl;
            await Navigation.PushModalAsync(new NavigationPage(new Page1()));
        }
    }
}
