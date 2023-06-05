using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Electronic_goods
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : TabbedPage
    {
        public Page1()
        {
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
    }
}