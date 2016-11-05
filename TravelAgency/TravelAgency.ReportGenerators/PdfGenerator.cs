using System;
using System.IO;
using System.Linq;

using iTextSharp.text;
using iTextSharp.text.pdf;
using static iTextSharp.text.Font;

using TravelAgency.Data;
using TravelAgency.ReportGenerators.Contracts;

namespace TravelAgency.ReportGenerators
{
    public class PdfGenerator : IReportGenerator
    {
        private const string DateTimeFormat = "dd.MM.yyyy";
        private const int TableColumnsNumber = 6;

        public void GenerateReport(ITravelAgencyDbContext travelAgencyDbContext)
        {
            string pathToGenerate = Common.Constants.GeneratedReportsPath;
            string fileName = "Profit-report-per-touroperators.pdf";

            ExportProfitsToPdf(travelAgencyDbContext, pathToGenerate, fileName);
        }

        public void ExportProfitsToPdf(ITravelAgencyDbContext travelAgencyDbContext, string pathToSave, string pdfReportName)
        {
            if (!string.IsNullOrEmpty(pdfReportName))
            {
                CreateDirectoryIfNotExists(pathToSave);
            }

            DateTime reportStartDate = new DateTime(2016, 10, 1);
            DateTime reportEndDate = new DateTime(2016, 10, 31);

            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(pathToSave + pdfReportName, FileMode.Create));
            doc.Open();

            FontFamily fontFamily = new FontFamily();
            Paragraph heading = new Paragraph("Profits per touroperators", new Font(fontFamily, 28f, Font.BOLD));
            heading.SpacingAfter = 18f;
            doc.Add(heading);

            Paragraph date = new Paragraph("Departure period:" + reportStartDate.ToString(DateTimeFormat) + "-" + reportEndDate.ToString(DateTimeFormat),
                                           new Font(fontFamily, 14f, Font.BOLD));
            heading.SpacingAfter = 20f;
            doc.Add(date);

            var touroperators = travelAgencyDbContext.Touroperators.ToList();

            decimal total = 0;
            var tripName = string.Empty;
            var departerDate = new DateTime();
            var arrivalDate = new DateTime();

            foreach (var touroperator in touroperators)
            {
                PdfPTable table = CreateTable(TableColumnsNumber);
               
                var touroperatorName = CreateColumn("Touroperator: " + touroperator.Name, 1);
                touroperatorName.Colspan = TableColumnsNumber;
                touroperatorName.BackgroundColor = new BaseColor(51, 153, 153);
                touroperatorName.PaddingBottom = 5f;
                table.AddCell(touroperatorName);

                GetHeaders(table);

                var trips = touroperator.Trips.Where(y => y.DeparterDate > reportStartDate || y.DeparterDate < reportEndDate).ToList();
                
                foreach (var trip in trips)
                {
                    tripName = trip.Name.Replace(' ', '-');
                    departerDate = trip.DeparterDate;
                    arrivalDate = trip.ArrivalDate;

                    table.AddCell(tripName);
                    var departerDateCell = CreateColumn(departerDate.ToString(DateTimeFormat), 1);
                    table.AddCell(departerDateCell);

                    var arrivalDateCell = CreateColumn(arrivalDate.ToString(DateTimeFormat), 1);
                    table.AddCell(arrivalDateCell);

                    decimal tripProfit = 0;
                    decimal discountAmount = 0;
                    decimal totalDiscountAmount = 0;
                    var customersCount = 0;

                    foreach (var customer in trip.Customers)
                    {
                        customersCount++;

                        tripProfit += trip.Price;

                        if (customer.HasDiscount)
                        {
                            discountAmount = trip.Price * (decimal)trip.Discount / 100;
                            totalDiscountAmount += discountAmount;
                            tripProfit -= discountAmount;
                            total += tripProfit;
                        }
                    }

                    var customers = CreateColumn(customersCount.ToString(), 1);
                    table.AddCell(customers);

                    var totalDiscountCell = CreateColumn(totalDiscountAmount.ToString("N"), 2);
                    table.AddCell(totalDiscountCell);

                    var tripProfitCell = CreateColumn(tripProfit.ToString("N"), 2);
                    table.AddCell(tripProfitCell);
                }

                var cellTotal = CreateColumn("Total Profit: $ " + total.ToString("N"), 2);
                cellTotal.BackgroundColor = new BaseColor(241, 241, 241);
                cellTotal.PaddingBottom = 5f;
                cellTotal.PaddingRight = 30f;
                table.AddCell(cellTotal);

                doc.Add(table);
            }

            doc.Close();
        }

        private PdfPTable CreateTable(int tableColumnsNumber)
        {
            PdfPTable table = new PdfPTable(TableColumnsNumber);
            table.SpacingBefore = 25f;
            table.TotalWidth = 550f;
            table.LockedWidth = true;
            float[] widths = new float[] { 140f, 90, 80f, 70f, 90f, 80f };
            table.SetWidths(widths);

            return table;
        }

        private void GetHeaders(PdfPTable table)
        {
            table.AddCell(CreateColumn("Trip name", 1));
            table.AddCell(CreateColumn("Departure Date", 1));
            table.AddCell(CreateColumn("Arrival Date", 1));
            table.AddCell(CreateColumn("Customers", 1));
            table.AddCell(CreateColumn("Discount amont", 1));
            table.AddCell(CreateColumn("Total profit", 1));
        }

        private PdfPCell CreateColumn(string columnName, int alignment)
        {
            PdfPCell column = new PdfPCell(new Phrase(columnName));
            column.HorizontalAlignment = alignment;

            return column;
        }

        public static void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
