using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Electronic_goods.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Electronic_goods
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private async void BackLblTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            if (LoginLbl.Text == "admin" && PassLbl.Text == "adm")
            {
                var cl = new Client("Баннов", "Филипп", "Германович", "89047176539", "bannov.2002@bk.ru", "admin", "adm", 1);
                App.client = cl;
                await Navigation.PushModalAsync(new NavigationPage(new Page1()));
            }
            else
                if (!await CheckLogin())
                await DisplayAlert("Ошибка", "Неправильный пароль или логин", "Продолжить");
        }
        private async Task<bool> CheckLogin()
        {
            foreach (var item in App.Db.GetClients())
            {
                if (item.Login == LoginLbl.Text)
                {
                    if (item.Password == PassLbl.Text)
                    {
                        App.client = item;
                        await Navigation.PushModalAsync(new NavigationPage(new Page1()));
                        return true;

                    }
                }
            }
            return false;
        }
    }
}