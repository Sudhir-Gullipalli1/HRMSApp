using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMSApp.Data.Models
{
    public partial class DeptEmp
    {
        [Required]
        [Display(Name = "Department Number")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public string DeptNo { get; set; }

        [Required]
        [Display(Name = "Employee Number")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int? EmpNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "From Date")]
        public DateTime? FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateTime? ToDate { get; set; }

        public virtual Department DeptNoNavigation { get; set; }
        public virtual Employee EmpNoNavigation { get; set; }
    }
}
