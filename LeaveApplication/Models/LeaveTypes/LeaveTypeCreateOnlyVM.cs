using System.ComponentModel.DataAnnotations;

namespace LeaveApplication.Models.LeaveTypes
{
    public class LeaveTypeCreateOnlyVM
    {
        [Required]
        [Length(4,150, ErrorMessage = "You have violated the length requiremets")]
        public string Name { get; set; } = string.Empty;
        
        
        [Required]
        [Range(1,90)]
        [Display(Name = "Maximum Allocation of Days")]
        public int NoOfDays { get; set; }
    }
}
