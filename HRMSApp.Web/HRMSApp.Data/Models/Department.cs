using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMSApp.Data.Models
{
    public partial class Department
    {
        public Department()
        {
            DeptManager = new HashSet<DeptManager>();
        }

        [Required]
        [Display(Name = "Department Number")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        [StringLength(4)]
        public string DeptNo { get; set; }

        [Required]
        [StringLength(45)]
        public string Name { get; set; }

        public virtual DeptEmp DeptEmp { get; set; }
        public virtual ICollection<DeptManager> DeptManager { get; set; }
    }
}
