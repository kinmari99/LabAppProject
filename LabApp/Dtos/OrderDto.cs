namespace LabApp.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime OrderedAt { get; set; }
        public List<string> Tests { get; set; } = new();
    }
    public class CreateOrderDto
    {
        public int PatientId { get; set; }
        public List<string> Tests { get; set; } = new();
    }
    public class UpdateOrderDto
    {
        public List<string> Tests { get; set; } = new();
    }

}
