using System.Collections.Generic;
using System.Linq;
using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.MongoDbExtractor;

namespace TravelAgency.Client
{
    public class TravelAgenciesDataImporter
    {
        private ITravelAgencyDbContext travelAgencyDbContext;
        private IMongoDbExtractor mongoExtractor;

        public TravelAgenciesDataImporter(ITravelAgencyDbContext travelAgencyDbContext, IMongoDbExtractor mongoExtractor)
        {
            this.travelAgencyDbContext = travelAgencyDbContext;
            this.mongoExtractor = mongoExtractor;
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
            var mongoTourOperators = this.mongoExtractor.ExtractMongoDbTourOperators();
            // TODO: Exctract customers and discounts

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
                                    .ToList()
                            })
                            .ToList()
                    });

            return touroperators;
        }
    }
}
