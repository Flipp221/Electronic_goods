﻿using Electronic_goods.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Electronic_goods.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilPage : ContentPage
    {
        public ProfilPage()
        {
            InitializeComponent();
            AccountInfoLbl.Text = $"{App.client.Surname} {App.client.Name} {App.client.Patronymic} {App.client.TelephoneNumber}";
            if (img.Source == null)
                img.Source = "profiljpg.jpg";
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReviewsPage());
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
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

                    App.client.AvatarPath = photo.FullPath.ToString();
                    img.Source = photo.FullPath;
                    App.Db.SaveClient(App.client);
                    if (img.Source == null)
                        img.Source = "profiljpg.jpg";
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
                }
            }
            else if (result == "Фото из камеры")
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
                    App.client.AvatarPath = photo.FullPath;
                    img.Source = photo.FullPath;
                    App.Db.SaveClient(App.client);
                    if (img.Source == null)
                        img.Source = "profiljpg.jpg";
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
                }
            }

        }
    }
}