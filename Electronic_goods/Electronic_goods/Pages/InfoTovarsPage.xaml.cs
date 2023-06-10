using Electronic_goods.Models;
using GemBox.Spreadsheet;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var data = App.Db.GetReportBasket();
                    var fileName = "export.xlsx";
                    var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);
                    var file = new FileInfo(filePath);
                    if (file.Exists)
                    {
                        file.Delete();
                        file = new FileInfo(filePath);
                    }
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
                            BasketReport rep = new BasketReport(item.Name, item.Count,DateTime.Now,item.Price, $"{App.client.Surname} {App.client.Name} {App.client.Patronymic}");
                            App.Db.SaveReportBasket(rep);
                            using (var package = new ExcelPackage(file))
                            {
                                var worksheet = package.Workbook.Worksheets.Add("Data");
                                worksheet.Cells[1, 1].Value = "Название";
                                worksheet.Cells[1, 2].Value = "Количество";
                                worksheet.Cells[1, 3].Value = "Дата покупки";
                                worksheet.Cells[1, 4].Value = "Цена";
                                worksheet.Cells[1, 5].Value = "ФИО покупателя";
                                using (var range = worksheet.Cells[1, 1, 1, 5])
                                {
                                    range.Style.Font.Bold = true; range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                }
                                var row = 2;
                                foreach (var acc in data)
                                {
                                    worksheet.Cells[row, 1].Value = acc.Name.ToString();
                                    worksheet.Cells[row, 2].Value = acc.Count.ToString();
                                    worksheet.Cells[row, 3].Value = acc.Date.ToString();
                                    worksheet.Cells[row, 4].Value = acc.Price.ToString();
                                    worksheet.Cells[row, 5].Value = acc.FIO.ToString();
                                    row++;
                                }
                                worksheet.Cells.AutoFitColumns();
                                file = new FileInfo("/storage/emulated/0/Download/ReportBusket.xlsx");
                                package.SaveAs(file);
                            }
                            await DisplayAlert("Done", "Товар успешно добавлен", "Ok");
                            await Navigation.PopAsync();
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