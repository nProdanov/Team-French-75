using System;
using System.Linq;
using System.Xml.Linq;
using TravelAgency.Data;
using TravelAgency.ReportGenerators.Contracts;
using TravelAgency.Models;

namespace TravelAgency.ReportGenerators
{
    public class XmlGenerator : IReportGenerator
    {
        public void GenerateReport(ITravelAgencyDbContext travelAgencyDbContext)
        {
            string filePath = "../../../../Generated-Reports/profit-report.xml";

            XElement root = new XElement("profits-report");


            var touroperators = travelAgencyDbContext.Touroperators.ToList();
            foreach(var touroperator in touroperators)
            {
                
                var touroperatorName = touroperator.Name.Replace(' ', '-');
                var touroperatorElement = new XElement("touroperator",
                        new XAttribute("name", touroperatorName));

                foreach (Trip trip in touroperator.Trips)
                {
                    var tripName = trip.Name.Replace(' ', '-');
                    var tripElement = new XElement("trip",
                            new XAttribute("name", tripName));

                    decimal profit = 0;

                    foreach (var customer in trip.Customers)
                    {
                        profit += trip.Price;

                        if (customer.HasDiscount)
                        {
                            // calculate discount percentage
                            profit -= trip.Price * (decimal)trip.Discount / 100;
                        }
                    }

                    tripElement.Add(new XElement("profit", profit));
                    touroperatorElement.Add(tripElement);
                }

                root.Add(touroperatorElement);
            }

            root.Save(filePath);
        }
    }
}
