using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using TravelAgency.Readers;

namespace TravelAgency.ReportGenerators
{
    public class ExcelGenerator
    {
        public void GenerateReport()
        {
            string relativePath = @"..\..\..\..\Generated-Reports\Excel-report.xlsx";
            var fullPath = Path.GetFullPath(relativePath);

            var tripsWithContinent = ReadDataFromSqlite();

            // TODO: add MySQL data and include it into excel

            try
            {
                var excelApplication = new Application();
                excelApplication.Visible = true;
                var workbook = (_Workbook)(excelApplication.Workbooks.Add(""));
                var sheet = (_Worksheet)workbook.ActiveSheet;

                sheet.Cells[1, 1] = "Trips Excel Report";
                sheet.get_Range("A1", "B1").MergeCells = true;
                sheet.get_Range("A1", "B1").Font.Bold = true;
                sheet.get_Range("A1", "B1").ColumnWidth = 25;

                var row = 2;
                var col = 1;
                sheet.Cells[row, col] = "Trip Name";
                sheet.Cells[row, col + 1] = "Country";
                sheet.get_Range("A2", "B2").Font.Bold = true;
                sheet.get_Range("A2", "B2").Interior.Color = XlRgbColor.rgbCadetBlue;

                foreach (var trip in tripsWithContinent)
                {
                    row++;
                    sheet.Cells[row, col] = trip.Key;
                    sheet.Cells[row, col + 1] = trip.Value;
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

        private IDictionary<string, string> ReadDataFromSqlite()
        {
            var sqliteReader = new SqliteReader();
            var tripsWithContinent = sqliteReader.ReadSqlite();

            return tripsWithContinent;
        }
    }
}
