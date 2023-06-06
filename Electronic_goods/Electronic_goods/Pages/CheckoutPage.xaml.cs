using Android.Net;
using Electronic_goods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Electronic_goods.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutPage : ContentPage
    {
        Tovars tovars;
        public CheckoutPage(int a, int b)
        {
            InitializeComponent();
            AmountLbl.Text = $"Товары ({a}):";
            SumLbl.Text = $"{b} P";
            SumLbl1.Text = $"{b} P";
            FIOEntry.Text = $"{App.client.Surname} {App.client.Name} {App.client.Patronymic}";
            BindingContext = App.client;
            
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void OrderBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                MailAddress fromAdress = new MailAddress($"bannov.2002@bk.ru", "Electronick_Goods");
                MailAddress toAdress = new MailAddress(mailEnt.Text, Login.Text);
                MailMessage message = new MailMessage(fromAdress, toAdress);
                message.Body = "Магазин Electronick_Goods приветствует вас," + Environment.NewLine + $"{FIOEntry.Text}" + Environment.NewLine + "Спасибо вам за покупку в нашем магазине, мы сообщим когда ваш товар прибудет!!!";
                message.Subject = "Администрация магазина - Electronick_Goods";

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.mail.ru";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromAdress.Address, "CbET0LjDiduaeLPpzYgZ");

                smtp.Send(message);

                await DisplayAlert("Уведомление", "Заказ успешно оформлен", "ОК");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {

                await DisplayAlert("Уведомление", ex.Message.ToString(), "ОК");
            }
        }
    }
}