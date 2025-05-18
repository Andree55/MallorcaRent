namespace MallorcaRent.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
    }
}
