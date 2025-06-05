namespace LabApp.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DiagnosticianId { get; set; }
        public int DeviceId { get; set; }
        public DateTime OrderedAt { get; set; }
        
    }
    public class CreateOrderDto
    {
        public int PatientId { get; set; }
        public int DiagnosticianId { get; set; }
        public int DeviceId { get; set; }
    }
    public class UpdateOrderDto
    {
        public int DeviceId { get; set; }
    }

}
