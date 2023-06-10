using Electronic_goods.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Electronic_goods.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTovarsPage : ContentPage
    {
        int tovarsid;
        string path;
        bool state;
        bool state1 = false;
        Tovars tovars;
        public AddTovarsPage()
        {
            InitializeComponent();
            BarLbl.Text = "Добавление товаров";
            TypeTovar.ItemsSource = App.types;
            ColorTovar.ItemsSource = App.colors;
            MaterialTovar.ItemsSource = App.materials;
            state = true;
        }
        public AddTovarsPage(Tovars tov)
        {
            tovars = tov;
            InitializeComponent();
            TypeTovar.ItemsSource = App.types;
            ColorTovar.ItemsSource = App.colors;
            MaterialTovar.ItemsSource = App.materials;
            NameTovar.Text = tovars.Name;
            DescriptionTovar.Text = tovars.Description;
            path = tovars.ImagePath;
            PriceTovar.Text = tovars.Price;
            TypeTovar.SelectedItem = tovars.Type;
            ColorTovar.SelectedItem = tovars.Color;
            MaterialTovar.SelectedItem = tovars.Material;
            CountTovar.Text = tovars.Count;
            BarLbl.Text = "Редактирование";
            state1 = true;
        }

        private async void RegistrateBtn_Clicked(object sender, EventArgs e)
        {

            string result = await DisplayActionSheet("Выберите:", null, null, "Фото из галереи", "Фото из камеры");

            if (result == "Фото из галереи")
            {
                try
                {
                    // выбираем фото
                    var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                    {
                        Title = $"Xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                    });

                    var newFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), photo.FileName);

                    // загружаем в ImageView

                    path = photo.FullPath;
                    state1 = true;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
                }
            }
            else
            {
                try
                {
                    var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                    {
                        Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                    });
                    var newFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), photo.FileName);
                    using (var stream = await photo.OpenReadAsync())
                    using (var newStream = File.OpenWrite(newFile))
                    {
                        await stream.CopyToAsync(newStream);
                    }
                    path = photo.FullPath;
                    state1 = true;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
                }

            }
        }

        private async void SaveBtn_Clicked(object sender, EventArgs e)
        {
            if (state)
                App.Db.SaveTovars(new Tovars(NameTovar.Text, DescriptionTovar.Text, PriceTovar.Text, ColorTovar.SelectedItem.ToString(), TypeTovar.SelectedItem.ToString(), MaterialTovar.SelectedItem.ToString(), path, CountTovar.Text, DateTime.Now));
            else
            {
                if (state1)
                tovars.Name = NameTovar.Text.ToString();
                tovars.Description = DescriptionTovar.Text.ToString();
                tovars.Price = PriceTovar.Text.ToString();
                tovars.ImagePath = path;
                tovars.Type = TypeTovar.SelectedItem.ToString();
                tovars.Color = ColorTovar.SelectedItem.ToString();
                tovars.Material = MaterialTovar.SelectedItem.ToString();
                tovars.Count = CountTovar.Text.ToString();
                App.Db.SaveTovars(tovars);
            }
            await Navigation.PopAsync();
        }

        private async void BackLbl_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}