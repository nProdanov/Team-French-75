using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public class Customer
    {
        private const int MinNameLength = 3;
        private const int MaxNameLength = 50;
        private const string MinNameLengthErrorMessage = "The {0} must be at least {1} characters long.";
        private const string MaxNameLengthErrorMessage = "The {0} must be no more than {1} characters long.";

        private ICollection<Trip> trips;

        public Customer()
        {
            this.trips = new HashSet<Trip>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(MinNameLength, ErrorMessage = MinNameLengthErrorMessage)]
        [MaxLength(MaxNameLength, ErrorMessage = MaxNameLengthErrorMessage)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(MinNameLength, ErrorMessage = MinNameLengthErrorMessage)]
        [MaxLength(MaxNameLength, ErrorMessage = MaxNameLengthErrorMessage)]
        public string LastName { get; set; }

        public bool HasDiscount { get; set; }

        public virtual ICollection<Trip> Trips
        {
            get { return this.trips; }
            set { this.trips = value; }
        }
    }
}
