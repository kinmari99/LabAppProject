namespace LabApp.Dtos
{
    public class ResultDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public int ExaminationId { get; set; }
        public string ExaminationName { get; set; }
        public string Value { get; set; }         
        public string Unit { get; set; }      
        public string LowerRange { get; set; }
        public string UpperRange { get; set; }  
        public DateTime PerformedAt { get; set; }
    }
    public class CreateResultDto
    {
        public int OrderId { get; set; }

        public int ExaminationId { get; set; }
        public string ExaminationName { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
        public string LowerRange { get;set; }
        public string UpperRange { get; set;}

        public DateTime PerformedAt { get; set; }
    }
    public class UpdateResultDto
    {
        public string Value { get; set; }
        public string Unit { get; set; }

    }
}
