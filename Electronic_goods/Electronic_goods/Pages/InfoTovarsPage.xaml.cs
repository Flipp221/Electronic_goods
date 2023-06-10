using Electronic_goods.Models;
using GemBox.Spreadsheet;
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
    public partial class InfoTovarsPage : ContentPage
    {
        Tovars tovars;
        public InfoTovarsPage(Tovars tov)
        {
            tovars = tov;
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
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


        private async void ForBasketBtn_Clicked(object sender, EventArgs e)
        {
            if (tovars.Count == 0.ToString())
            {
                await DisplayAlert("Error", "В данный момент товар отсутствует в магазине", "Ok");
            }
            if (App.client.RoleId == 2)
            {
                await DisplayAlert("Error", "Вы не можете совершать покупки, пожалуйста войдите или зарегестрируйтесь", "Ok");
            }
            else
            {
                try
                {
                    var workbook = new ExcelFile();
                    var worksheet = workbook.Worksheets.Add("Report");
                    var fileName = "Report.xlsx";
                    var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
                    var file = new FileInfo(filePath);
                    if (file.Exists)
                    {
                        file.Delete();
                        file = new FileInfo(filePath);
                    }
                    worksheet.Cells[1, 1].Value = "Наименование товара";
                    worksheet.Cells[1, 2].Value = "Количество купленного товара";
                    worksheet.Cells[1, 3].Value = "Дата покупки";
                    worksheet.Cells[1, 4].Value = "Стоимость товара";
                    worksheet.Cells[1, 5].Value = "ФИО покупателя";
                    var row = 2;
                    var id = ((Button)sender).CommandParameter.ToString();
                    var fre = App.Db.GetTovars();

                    foreach (var item in fre)
                    {
                        if (item.Id == int.Parse(id))
                        {
                            item.Count = (Convert.ToInt32(item.Count.ToString()) - 1).ToString();
                            App.Db.SaveTovars(item);
                            Report report = new Report(item.Name, item.Count = "1", $"{App.client.Surname} {App.client.Name} {App.client.Patronymic}");
                            App.Db.SaveReport(report);
                            string material = "";
                            tovars.Material = material;
                            Busket bk = new Busket(1, App.client.Id, tovars.Id);
                            App.Db.SaveBasket(bk);
                            await DisplayAlert("Done", "Товар успешно добавлен", "Ok");
                            await Navigation.PopAsync();
                            if (item.Id == int.Parse(id))
                            {
                                worksheet.Cells[row, 1].Value = item.Name.ToString();
                                worksheet.Cells[row, 2].Value = item.Count.ToString();
                                worksheet.Cells[row, 3].Value = DateTime.Now;
                                worksheet.Cells[row, 4].Value = item.Price.ToString();
                                worksheet.Cells[row, 5].Value = $"{App.client.Surname} {App.client.Name} {App.client.Patronymic}    ";
                                row++;
                                worksheet.Cells.AutoFitColumnWidth();

                                
                                file = new FileInfo("/storage/emulated/0/Download/Report.xlsx");
                                workbook.Save(file.ToString());                           
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("", ex.Message, "ok");
                }
            }
        }

        private async void BackBtn_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void EditBtn_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTovarsPage(tovars));
        }

        private async void deleteBtn_Tapped(object sender, EventArgs e)
        {
            App.Db.DeleteTovars(tovars.Id);
            await DisplayAlert("Done", "Товар удалён", "Ok");
            await Navigation.PopAsync();
        }
    }
}