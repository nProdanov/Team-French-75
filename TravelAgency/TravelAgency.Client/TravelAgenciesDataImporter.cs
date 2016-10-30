using System.Collections.Generic;
using System.Linq;

using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.Readers.Contracts;

namespace TravelAgency.Client
{
    public class TravelAgenciesDataImporter
    {
        private ITravelAgencyDbContext travelAgencyDbContext;
        private IMongoReader mongoExtractor;
        private IExcelReader excelReader;

        public TravelAgenciesDataImporter(ITravelAgencyDbContext travelAgencyDbContext, IMongoReader mongoReader, IExcelReader excelReader)
        {
            this.travelAgencyDbContext = travelAgencyDbContext;
            this.mongoExtractor = mongoReader;
            this.excelReader = excelReader;
        }

        public void ImportData()
        {
            var touroperatorsReadyForImport = this.MergeData();

            foreach (var touroperator in touroperatorsReadyForImport)
            {
                // TODO: Add logic for often will call SaveChanges()
                this.travelAgencyDbContext.Touroperators.Add(touroperator);
            }
        }

        private IEnumerable<Touroperator> MergeData()
        {
            var mongoTourOperators = this.mongoExtractor.ReadMongo();
            // TODO: Exctract customers and discounts

            // read xml - save to mongo

            this.excelReader.ReadExcel(mongoTourOperators);
             
            var touroperators = mongoTourOperators
                .Select(mongoTourop => new Touroperator()
                {
                    Name = mongoTourop.Name,
                    Trips = mongoTourop
                            .Trips
                            .Select(mongoTr => new Trip()
                            {
                                Name = mongoTr.Name,
                                Price = mongoTr.Price,
                                ArrivalDate = mongoTr.ArrivalDate,
                                DeparterDate = System.DateTime.Now,
                                Destinations = mongoTr
                                    .Destinations
                                    .Select(mongoDest => new Destination() { Name = mongoDest.Name })
                                    .ToList(),
                                Customers = mongoTr
                                    .Customers
                                    .Select(customer => new Customer()
                                        {
                                            FirstName = customer.FirstName,
                                            LastName = customer.LastName,
                                            HasDiscount = customer.HasDiscount
                                        })
                                    .ToList()
                            })
                            .ToList()
                    });

            return touroperators;
        }
    }
}
