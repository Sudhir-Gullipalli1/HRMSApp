using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMSApp.Data.Models
{
    public partial class Salary
    {
        [Required]
        [Display(Name = "Employee Number")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int? EmpNo { get; set; }

        [Required]
        [Display(Name = "Salary")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int? Salary1 { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "From Date")]
        public DateTime? FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateTime? ToDate { get; set; }

        public virtual Employee EmpNoNavigation { get; set; }
    }
}
