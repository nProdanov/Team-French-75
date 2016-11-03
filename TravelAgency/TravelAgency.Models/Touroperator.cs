using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public class Touroperator
    {
        private const int MinNameLength = 3;
        private const int MaxNameLength = 20;
        private const string MinNameLengthErrorMessage = "The {0} must be at least {1} characters long.";
        private const string MaxNameLengthErrorMessage = "The {0} must be no more than {1} characters long.";

        private ICollection<Trip> trips;

        public Touroperator()
        {
            this.trips = new HashSet<Trip>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(MinNameLength, ErrorMessage = MinNameLengthErrorMessage)]
        [MaxLength(MaxNameLength, ErrorMessage = MaxNameLengthErrorMessage)]
        public string Name { get; set; }

        public virtual ICollection<Trip> Trips
        {
            get { return this.trips; }
            set { this.trips = value; }
        }
    }
}
