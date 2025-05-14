namespace MallorcaRent.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        public int CarId { get; set; }
        public Car? Car { get; set; }

        public int PickupLocationId { get; set; }
        public Location? PickupLocation { get; set; }

        public int ReturnLocationId { get; set; }
        public Location? ReturnLocation { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal TotalCost { get; set; }

    }
}
