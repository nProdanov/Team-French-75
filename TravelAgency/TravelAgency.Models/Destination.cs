using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TravelAgency.Models
{
    public class Destination
    {
        private const int MinNameLength = 2;
        private const int MaxNameLength = 50;
        private const string MinNameLengthErrorMessage = "The {0} must be at least {1} characters long.";
        private const string MaxNameLengthErrorMessage = "The {0} must be no more than {1} characters long.";

        private ICollection<Trip> trips;

        public Destination()
        {
            this.trips = new HashSet<Trip>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(MinNameLength, ErrorMessage = MinNameLengthErrorMessage)]
        [MaxLength(MaxNameLength, ErrorMessage = MaxNameLengthErrorMessage)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Trip> Trips
        {
            get { return this.trips; }
            set { this.trips = value; }
        }
    }
}
