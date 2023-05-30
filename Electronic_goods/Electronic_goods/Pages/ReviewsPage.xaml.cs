using Electronic_goods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Electronic_goods.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Electronic_goods.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReviewsPage : ContentPage
    {
        List<Review> aa = new List<Review>();
        public ReviewsPage()
        {
            InitializeComponent();
            Update();
            ReviewsLst.RefreshCommand = new Command(() =>
            {
                Update();
                ReviewsLst.IsRefreshing = false; //выключить анимацию обновления 
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Update();
        }
        private void Update()
        {
            aa = new List<Review>();
            ReviewsLst.ItemsSource = null;
            List<REviewSevices> res = new List<REviewSevices>();
            foreach (var item in App.Db.GetReviews())
            {

                Color color1 = Color.Gray;
                Color color2 = Color.Gray;
                Color color3 = Color.Gray;
                Color color4 = Color.Gray;
                Color color5 = Color.Gray;

                switch (item.Count)
                {
                    case 1:
                        color1 = Color.Yellow;
                        break;
                    case 2:
                        color1 = Color.Yellow;
                        color2 = Color.Yellow;
                        break;
                    case 3:
                        color1 = Color.Yellow;
                        color2 = Color.Yellow;
                        color3 = Color.Yellow;
                        break;
                    case 4:
                        color1 = Color.Yellow;
                        color2 = Color.Yellow;
                        color3 = Color.Yellow;
                        color4 = Color.Yellow;
                        break;
                    case 5:
                        color1 = Color.Yellow;
                        color2 = Color.Yellow;
                        color3 = Color.Yellow;
                        color4 = Color.Yellow;
                        color5 = Color.Yellow;
                        break;
                }
                res.Add(new REviewSevices(item.FIO, item.Path, item.ReviewText, color1, color2, color3, color4, color5));
            }
            ReviewsLst.ItemsSource = res;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void SwipeItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var id = ((SwipeItem)sender).CommandParameter.ToString();
                var fre = App.Db.GetReviews();

                foreach (var item in fre)
                {
                    if (item.ReviewId == int.Parse(id))
                    {
                        App.Db.DeleteRewiws(item.Id);
                        break;
                        
                    }
                }
                DisplayAlert("Done!", "Отзыв удалён!", "ok");
                Update();
            }
            catch (Exception ex)
            {
                DisplayAlert("", ex.Message, "ok");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReviewsFormPage());
        }
    }
}