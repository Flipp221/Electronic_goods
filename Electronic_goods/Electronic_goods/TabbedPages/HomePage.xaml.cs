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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            Update();
            GoodsLstViw.RefreshCommand = new Command(() =>
            {
                Update();
                GoodsLstViw.IsRefreshing = false;

            });
        }
        private void Update()
        {
            GoodsLstViw.ItemsSource = null;

            if (toolsBoxVw.IsVisible)
                GoodsLstViw.ItemsSource = App.Db.GetTovars().Select(f => f).Where(x => x.Type == App.types[0]);
            else if (gadgetsBoxVw.IsVisible)
                GoodsLstViw.ItemsSource = App.Db.GetTovars().Select(f => f).Where(x => x.Type == App.types[1]);
            else if (appliancesBoxVw.IsVisible)
                GoodsLstViw.ItemsSource = App.Db.GetTovars().Select(f => f).Where(x => x.Type == App.types[2]);
            else if (restBoxVw.IsVisible)
                GoodsLstViw.ItemsSource = App.Db.GetTovars().Select(f => f).Where(x => x.Type == App.types[3]);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.client.RoleId == 0)
                AdminPanel.IsVisible = false;
            else if (App.client.RoleId == 1)
                AdminPanel.IsVisible = true;
        }

        private void toolsBtn_Clicked(object sender, EventArgs e)
        {
            CleanAllBtnBoxViews();
            toolsBoxVw.IsVisible = true;
            Update();
        }

        private void gadgetsBtn_Clicked(object sender, EventArgs e)
        {
            CleanAllBtnBoxViews();
            gadgetsBoxVw.IsVisible = true;
            Update();
        }

        private void appliancesBtn_Clicked(object sender, EventArgs e)
        {
            CleanAllBtnBoxViews();
            appliancesBoxVw.IsVisible = true;
            Update();
        }

        private void restBtn_Clicked(object sender, EventArgs e)
        {
            CleanAllBtnBoxViews();
            restBoxVw.IsVisible = true;
            Update();
        }

        private void CleanAllBtnBoxViews()
        {
            toolsBoxVw.IsVisible = false;
            gadgetsBoxVw.IsVisible = false;
            appliancesBoxVw.IsVisible = false;
            restBoxVw.IsVisible = false;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTovarsPage());
        }

        private async void GoodsLstViw_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var d = (Tovars)e.Item;
            var a = new InfoTovarsPage(d);
            a.BindingContext = d;
            await Navigation.PushAsync(a);
        }
    }
}