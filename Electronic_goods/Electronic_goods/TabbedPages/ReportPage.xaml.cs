using Electronic_goods.Models;
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
    public partial class ReportPage : ContentPage
    {
        List<Report> aa = new List<Report>();
        public ReportPage()
        {
            InitializeComponent();
            ReportLst.RefreshCommand = new Command(() =>
            {
                ReportLst.IsRefreshing = false;
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateList();
        }
        void UpdateList()
        {
            aa = new List<Report>();
            ReportLst.ItemsSource = null;
            ReportLst.ItemsSource =  App.Db.GetReport();
        }
        private void SwipeItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var id = ((SwipeItem)sender).CommandParameter.ToString();
                var fre = App.Db.GetReport();

                foreach (var item in fre)
                {
                    if (item.Id == int.Parse(id))
                    {
                        App.Db.DeleteReports(item.Id);
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
    }
}