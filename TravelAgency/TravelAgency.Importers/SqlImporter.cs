using System.Collections.Generic;
using System.Linq;
using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.Readers.Contracts;

namespace TravelAgency.Importers
{
    public class SqlImporter
    {
        //private ITravelAgencyDbContext travelAgencyDbContext;
        private DataProvider sqlData;
        private IMongoReader mongoExtractor;
        private IExcelReader excelReader;
        private IXmlReader xmlReader;
        private ITravelAgencyDbContext context;

        public SqlImporter(
            DataProvider sqlData,
            //ITravelAgencyDbContext travelAgencyDbContext, 
            IMongoReader mongoReader, 
            IExcelReader excelReader, 
            IXmlReader xmlReader)
        {
            this.sqlData = sqlData;
            this.context = sqlData.Context;
            //this.travelAgencyDbContext = travelAgencyDbContext;
            this.mongoExtractor = mongoReader;
            this.excelReader = excelReader;
            this.xmlReader = xmlReader;
        }

        public void ImportGeneralData()
        {
            var touroperatorsReadyForImport = this.MergeData().ToList();

            for (int i = 0; i < touroperatorsReadyForImport.Count; i++)
            {
                var touroperator = touroperatorsReadyForImport[i];
                //this.travelAgencyDbContext.Touroperators.Add(touroperator);
                this.sqlData.ToureoperatorRepo.Add(touroperator);

                if (i % 20 == 0)
                {
                    this.sqlData.Context.SaveChanges();
                    //this.travelAgencyDbContext = new TravelAgencyDbContext();
                }
            }

            this.sqlData.Context.SaveChanges();
        }

        public void ImportAdditionalData()
        {
            var discounts = this.xmlReader.ReadXml();
            var tripIds = this.sqlData.TripRepo
                .GetAll()
                .OrderBy(tr => tr.Id)
                .Select(tr => tr.Id)
                .ToList();

            var savesCount = tripIds.Count / 100;

            if (savesCount < 1)
            {
                savesCount = 1;
            }

            var skip = 0;
            var take = 100;
            for (int i = 0; i < savesCount; i++)
            {
                var currTripIds = tripIds.Skip(skip).Take(take).ToList();
                var currTrips = 
                    this.sqlData.TripRepo
                    .GetAll()
                    .Where(tr => currTripIds.Contains(tr.Id))
                    .ToList();

                foreach (var trip in currTrips)
                {
                    trip.Discount = discounts[trip.Name];
                }

                this.sqlData.Context.SaveChanges();
                //this.travelAgencyDbContext = new TravelAgencyDbContext();
                skip += 100;
            }
        }

        private IEnumerable<Touroperator> MergeData()
        {
            var mongoTourOperators = this.mongoExtractor.ReadMongo();

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
                                DeparterDate = mongoTr.ArrivalDate.AddDays(-15), 
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
