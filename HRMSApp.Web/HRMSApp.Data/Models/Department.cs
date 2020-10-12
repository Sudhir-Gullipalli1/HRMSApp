using HRMSApp.Data.Models;
using System;
using System.Collections.Generic;

namespace HRMSApp.Data.Models
{
    public partial class Department
    {
        public Department()
        {
            DeptEmp = new HashSet<DeptEmp>();
            DeptManager = new HashSet<DeptManager>();
        }

        public string DeptNo { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DeptEmp> DeptEmp { get; set; }
        public virtual ICollection<DeptManager> DeptManager { get; set; }
    }
}
