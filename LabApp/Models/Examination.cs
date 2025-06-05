using System.ComponentModel.DataAnnotations;

namespace LabApp.Models
{
    public class Examination
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public string Unit { get; set; }
        public string LowerRange { get; set; }
        public string UpperRange { get; set; }
        [Required]
        public decimal Price { get; set; }

        

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public Result Result { get; set; }
    }
}
