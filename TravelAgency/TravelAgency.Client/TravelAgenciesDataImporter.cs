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
                //System.Console.WriteLine(touroperator.Name);
                //System.Console.WriteLine("trips:");
                //foreach (var trip in touroperator.Trips)
                //{
                //    System.Console.WriteLine($"    {trip.Name}");
                //    foreach (var dest in trip.Destinations)
                //    {
                //        System.Console.WriteLine($"        {dest.Name}");
                //    }
                //}
                //System.Console.WriteLine();
                this.travelAgencyDbContext.Touroperators.Add(touroperator);
            }
        }

        private IEnumerable<Touroperator> MergeData()
        {
            var mongoTourOperators = this.mongoExtractor.ExtractMongoDbTourOperators();


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

            // TODO: here will collect and merge data from excel and xml

            return touroperators; // just da ne pishi
        }
    }
}
