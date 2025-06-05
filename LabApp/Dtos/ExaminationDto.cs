namespace LabApp.Dtos
{
    public class ExaminationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }         
        public string Description { get; set; }  
        public string Unit { get; set; }   
        public string LowerRange {  get; set; }
        public string UpperRange { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateExaminationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string LowerRange { get; set; }
        public string UpperRange { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateExaminationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string LowerRange { get; set; }
        public string UpperRange { get; set; }
        public decimal Price { get; set; }
    }

}
