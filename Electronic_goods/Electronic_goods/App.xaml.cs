using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Electronic_goods.Db;
using Electronic_goods.Models;
using System.IO;

[assembly: ExportFont("MaterialIcons-Regular.ttf", Alias = "MaterialIconsFont")]
namespace Electronic_goods
{
    public partial class App : Application
    {
        public const string DB_NAME = "ElectrStore.db";
        public static CRUDOperation db;
        public static Client client;
        public static string[] colors = new string[4] { "Белый", "Чёрный", "Синий", "Красный" };
        public static string[] types = new string[4] { "Инструменты", "Гаджеты", "Бытовая техника", "Отдых" };
        public static string[] materials = new string[4] { "Алюминий", "Пластик", "Антипригарное покрытие", "Металл" };
        public static CRUDOperation Db
        {
            get
            {
                if (db == null)
                {
                    db = new CRUDOperation(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DB_NAME));
                }
                return db;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
