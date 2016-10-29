using System.Collections.Generic;
using TravelAgency.ParseModels;

namespace TravelAgency.Readers.Contracts
{
    public interface IExcelReader
    {
        void ReadExcel(IEnumerable<TouroperatorMongoDbModel> touroperators);
    }
}