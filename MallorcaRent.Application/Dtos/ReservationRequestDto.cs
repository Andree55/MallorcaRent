namespace MallorcaRent.Application.Dtos
{
    public class ReservationRequestDto
    {
        public int CarId { get; set; }
        public int PickupLocationId { get; set; }
        public int ReturnLocationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
