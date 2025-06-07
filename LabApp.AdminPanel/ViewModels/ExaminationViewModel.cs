using System.ComponentModel.DataAnnotations;

namespace LabApp.AdminPanel.ViewModels
{
    public class ExaminationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

     
        public string Description { get; set; }
    
        public string Unit { get; set; }
        public string LowerRange { get; set; }
        public string UpperRange { get; set; }
     
        public int Price { get; set; }
    }
}
