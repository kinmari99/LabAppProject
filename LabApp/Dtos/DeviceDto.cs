namespace LabApp.Dtos
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string Model { get; set; }       
        public string SerialNumber { get; set; }
        public bool IsOperational { get; set; }
    }
    public class CreateDeviceDto
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
    }
    public class UpdateDeviceDto
    {
        public bool IsOperational { get; set; }
    }
}
