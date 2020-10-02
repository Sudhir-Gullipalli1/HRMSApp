using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMSApp.Data.Models
{
    public partial class Employee
    {
        public Employee()
        {
            DeptEmp = new HashSet<DeptEmp>();
            DeptManager = new HashSet<DeptManager>();
            Salary = new HashSet<Salary>();
            Title = new HashSet<Title>();
        }

        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int? EmpNo { get; set; }

        [Required]
        [StringLength(45)]
        public string Ssn { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Required]
        [StringLength(24)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(24)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(24)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime? HireDate { get; set; }

        [Required]
        [StringLength(45)]
        public string Address1 { get; set; }

        [StringLength(45)]
        public string Address2 { get; set; }

        [Required]
        [StringLength(45)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Zip Code")]
        public string Zipcode { get; set; }

        [StringLength(12)]
        [Display(Name = "Home Phone")]
        public string HomePhone { get; set; }

        [Required]
        [StringLength(12)]
        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "The email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [StringLength(45)]
        public string Email { get; set; }

        public virtual ICollection<DeptEmp> DeptEmp { get; set; }
        public virtual ICollection<DeptManager> DeptManager { get; set; }
        public virtual ICollection<Salary> Salary { get; set; }
        public virtual ICollection<Title> Title { get; set; }
    }
}
