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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Electronic_goods.TabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReportPage : ContentPage
    {
        int countPl = 0;
        int prices = 0;
        List<Report> aa = new List<Report>();
        public ReportPage()
        {
            InitializeComponent();
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
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
            ReportLst.ItemsSource = App.Db.GetReport();
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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var data = App.Db.GetReportBasket();
                countPl = 0;
                prices = 0;
                var fileName = "export.xlsx";
                var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), fileName);
                var file = new FileInfo(filePath);
                if (file.Exists)
                {
                    file.Delete();
                    file = new FileInfo(filePath);
                }
                using (var package = new ExcelPackage(file))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Data");
                    worksheet.Cells[1, 1].Value = "Наименование товара";
                    worksheet.Cells[1, 2].Value = "Количество товара";
                    worksheet.Cells[1, 3].Value = "Дата покупки";
                    worksheet.Cells[1, 4].Value = "Цена";
                    worksheet.Cells[1, 5].Value = "ФИО покупателя";
                    worksheet.Cells[20, 1].Value = "Итого:";
                    worksheet.Cells[20, 3].Value = "Шт";
                    worksheet.Cells[20, 5].Value = "Руб.";
                    using (var range = worksheet.Cells[1, 1, 1, 5])
                    {
                        range.Style.Font.Bold = true; range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                    var row = 2;
                    var sla = 20;
                    foreach (var acc in data)
                    {
                        worksheet.Cells[row, 1].Value = acc.Name.ToString();
                        worksheet.Cells[row, 2].Value = (int.Parse(acc.Count));
                        worksheet.Cells[row, 3].Value = acc.Date.ToString();
                        worksheet.Cells[row, 4].Value = (int.Parse(acc.Price));
                        worksheet.Cells[row, 5].Value = acc.FIO.ToString();
                        countPl += int.Parse(acc.Count);
                        worksheet.Cells[sla, 2].Value = countPl.ToString();
                        prices += int.Parse(acc.Price);
                        worksheet.Cells[sla, 4].Value = prices.ToString();
                        row++;
                    }
                    worksheet.Cells.AutoFitColumns();
                    file = new FileInfo("/storage/emulated/0/Download/ReportBusket.xlsx");
                    package.SaveAs(file);
                    await DisplayAlert("Уведомление", "Отчёт успешно скачан!", "ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message.ToString(), "ok");

            }
        }
        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReportLst.ItemsSource = App.Db.GetReport().Select(f => f).Where(x => x.FIO.Contains(searchBar.Text));
        }
    }
}