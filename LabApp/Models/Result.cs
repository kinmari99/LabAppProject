using System.ComponentModel.DataAnnotations;

namespace LabApp.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }

        [Required]
        public string ExaminationName { get; set; }
        
        public string Value { get; set; }
        public string Unit { get; set; }
        public string LowerRange { get; set; }
        public string UpperRange { get; set; }
        public DateTime PerformedAt { get; set; }

        public int ExaminationId { get; set; }
        public Examination Examination { get; set; }
    }
}
