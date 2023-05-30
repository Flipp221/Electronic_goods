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
    public partial class OfferPage : ContentPage
    {
        public OfferPage()
        {
            InitializeComponent();
            SuggestionsLst.RefreshCommand = new Command(() =>
            {
                SuggestionsLst.ItemsSource = App.Db.GetImproveOffers();
                SuggestionsLst.IsRefreshing = false; //выключить анимацию обновления 
            });
            SuggestionsLst.ItemsSource = App.Db.GetImproveOffers();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OfferFormPage());
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateList();
        }
        void UpdateList()
        {
            SuggestionsLst.ItemsSource = null;
            var a = App.Db.GetImproveOffers();
        }
        private void SwipeItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var id = ((SwipeItem)sender).CommandParameter.ToString();
                var fre = App.Db.GetImproveOffers();

                foreach (var item in fre)
                {
                    if (item.Id  == int.Parse(id))
                    {
                        App.Db.DeleteOffers(item.Id);
                        break;
                    }
                }
                DisplayAlert("Done!", "Предложение удалено!", "ok");
                UpdateList();
            }
            catch(Exception ex)
            {
                DisplayAlert("", ex.Message, "ok");
            }
        }
    }
}