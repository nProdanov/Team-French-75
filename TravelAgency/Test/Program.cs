using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.ParseModels;
using Ionic.Zip;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private string zipPath = "../../../../Reports-Departures";
        private string folderPath = "../../../../Reports-Departures";

        static void Unzip()
        {
            // unzip
        }

        static void ReadExcel(string path, IEnumerable<TouroperatorMongoDbModel> touroperators)
        {

            DirectoryInfo root = null;

            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            files = root.GetFiles("*.*");

            if (files != null)
            {
                foreach (FileInfo fi in files)
                {
                    
                }
            }

            subDirs = root.GetDirectories();

            foreach (DirectoryInfo dirInfo in subDirs)
            {
  
            }

        }




    }
}
