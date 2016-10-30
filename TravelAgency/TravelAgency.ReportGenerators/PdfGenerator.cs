using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Linq;
using TravelAgency.Data;
using TravelAgency.ReportGenerators.Contracts;
using static iTextSharp.text.Font;

namespace TravelAgency.ReportGenerators
{
    public class PdfGenerator : IReportGenerator
    {
        private const string DateTimeFormat = "dd.MM.yyyy";
        private const int TableColumnsNumber = 6;

        public void GenerateReport(ITravelAgencyDbContext travelAgencyDbContext)
        {
            string filePath = "../../../../Generated-Reports/";
            string fileName = "Profit-report-per-touroperators.pdf";

            ExportProfitsToPdf(travelAgencyDbContext, filePath, fileName);
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
                PdfPTable table = new PdfPTable(TableColumnsNumber);
                table.SpacingBefore = 25f;
                table.TotalWidth = 550f;
                table.LockedWidth = true;
                float[] widths = new float[] { 140f, 90, 80f, 70f, 90f, 80f };
                table.SetWidths(widths);

                PdfPCell touroperatorName = new PdfPCell(new Phrase("Touroperator: " + touroperator.Name));
                touroperatorName.Colspan = TableColumnsNumber;
                touroperatorName.BackgroundColor = new BaseColor(51, 153, 153);
                touroperatorName.HorizontalAlignment = 1;
                touroperatorName.PaddingBottom = 5f;
                table.AddCell(touroperatorName);

                GetColums(table);

                var trips = touroperator.Trips.Where(y => y.DeparterDate > reportStartDate || y.DeparterDate < reportEndDate).ToList();
                
                foreach (var trip in trips)
                {
                    tripName = trip.Name.Replace(' ', '-');
                    departerDate = trip.DeparterDate;
                    arrivalDate = trip.ArrivalDate;

                    table.AddCell(tripName);
                    var departerDateCell = new PdfPCell(new Phrase(departerDate.ToString(DateTimeFormat)));
                    departerDateCell.HorizontalAlignment = 1;
                    table.AddCell(departerDateCell);

                    var arrivalDateCell = new PdfPCell(new Phrase(arrivalDate.ToString(DateTimeFormat)));
                    arrivalDateCell.HorizontalAlignment = 1;
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

                    var customers = new PdfPCell(new Phrase(customersCount.ToString()));
                    customers.HorizontalAlignment = 1;
                    table.AddCell(customers);

                    var totalDiscountCell = new PdfPCell(new Phrase(totalDiscountAmount.ToString("N")));
                    totalDiscountCell.HorizontalAlignment = 2;
                    table.AddCell(totalDiscountCell);

                    var tripProfitCell = new PdfPCell(new Phrase(tripProfit.ToString("N")));
                    tripProfitCell.HorizontalAlignment = 2;
                    table.AddCell(tripProfitCell);
                }

                var cellTotal = new PdfPCell(new Phrase("Total Profit: $ " + total.ToString("N")));
                cellTotal.Colspan = TableColumnsNumber;
                cellTotal.HorizontalAlignment = 2;
                cellTotal.BackgroundColor = new BaseColor(241, 241, 241);
                cellTotal.PaddingBottom = 5f;
                cellTotal.PaddingRight = 30f;
                table.AddCell(cellTotal);

                doc.Add(table);
            }

            doc.Close();
        }

        private void GetColums(PdfPTable table)
        {
            table.AddCell(CreateColumn("Trip name"));
            table.AddCell(CreateColumn("Departure Date"));
            table.AddCell(CreateColumn("Arrival Date"));
            table.AddCell(CreateColumn("Customers"));
            table.AddCell(CreateColumn("Discount amont"));
            table.AddCell(CreateColumn("Total profit"));
        }

        private PdfPCell CreateColumn(string columnName)
        {
            PdfPCell column = new PdfPCell(new Phrase(columnName));
            column.HorizontalAlignment = 1;

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
