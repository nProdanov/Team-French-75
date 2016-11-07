using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Office.Interop.Excel;

using TravelAgency.MySqlData;
using TravelAgency.Readers;
using TravelAgency.ReportGenerators.Contracts;

namespace TravelAgency.ReportGenerators
{
    public class ExcelGenerator
    {
        public void GenerateReport()
        {
            string pathToGenerate = Common.Constants.GeneratedReportsPath;
            string relativePath = $"{pathToGenerate}/Excel-report.xlsx";
            var fullPath = Path.GetFullPath(relativePath);

            var tripsWithContinent = ReadDataFromSqlite();
            var trips = ReadDataFromMySQL();

            try
            {
                var excelApplication = new Application();
                excelApplication.Visible = true;
                var workbook = (_Workbook)(excelApplication.Workbooks.Add(""));
                var sheet = (_Worksheet)workbook.ActiveSheet;

                sheet.Cells[1, 1] = "Trips Excel Report";
                var cellWidth = 25;
                SetFondsToTableTitle(sheet, "A1", "E1", cellWidth);

                var row = 2;
                var col = 1;
                sheet.Cells[row, col] = "Trip Name (from SQLite)";
                sheet.Cells[row, col + 1] = "Country (from SQLite)";
                sheet.Cells[row, col + 2] = "Touroperator (from MySQL)";
                sheet.Cells[row, col + 3] = "Trips Sold (from MySQL)";
                sheet.Cells[row, col + 4] = "Income (from MySQL)";

                var bacgroundColor = XlRgbColor.rgbCadetBlue;
                SetFondsToHeaderCells(sheet, "A2", "E2", bacgroundColor);

                foreach (var trip in tripsWithContinent)
                {
                    foreach (var item in trips)
                    {
                        if (item.TripName == trip.Key)
                        {
                            row++;
                            sheet.Cells[row, col] = trip.Key;
                            sheet.Cells[row, col + 1] = trip.Value;

                            sheet.Cells[row, col + 2] = item.TouroperatorName;
                            sheet.Cells[row, col + 3] = item.TotalTripsSold;
                            sheet.Cells[row, col + 4] = item.TotalIncomes;
                        }
                    }
                }

                workbook.SaveAs($"{fullPath}", XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                workbook.Close();

                Console.WriteLine($"The Excel report has been successfully generated.");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Excel Error occured: " + exception);
            }
        }

        private ICollection<TripReportMySqlModel> ReadDataFromMySQL()
        {
            var mySQLReader = new MySqlReader();
            var trips = mySQLReader.ReadMySql();

            return trips;
        }

        private IDictionary<string, string> ReadDataFromSqlite()
        {
            var sqliteReader = new SqliteReader();
            var tripsWithContinent = sqliteReader.ReadSqlite();

            return tripsWithContinent;
        }

        private void SetFondsToTableTitle(_Worksheet sheet, string cellRangeStart, string cellRangeEnd, int cellWidth)
        {
            sheet.get_Range(cellRangeStart, cellRangeEnd).Font.Bold = true;
            sheet.get_Range(cellRangeStart, cellRangeEnd).Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            sheet.get_Range(cellRangeStart, cellRangeEnd).MergeCells = true;
            sheet.get_Range(cellRangeStart, cellRangeEnd).ColumnWidth = cellWidth;
        }

        private void SetFondsToHeaderCells(_Worksheet sheet, string cellRangeStart, string cellRangeEnd, XlRgbColor bacgroundColor)
        {
            sheet.get_Range(cellRangeStart, cellRangeEnd).Font.Bold = true;
            sheet.get_Range(cellRangeStart, cellRangeEnd).Interior.Color = bacgroundColor;
            sheet.get_Range(cellRangeStart, cellRangeEnd).Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }
    }
}
