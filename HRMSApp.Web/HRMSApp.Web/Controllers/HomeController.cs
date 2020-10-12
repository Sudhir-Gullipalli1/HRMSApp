using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HRMSApp.Data.Models;
using FizzWare.NBuilder;
using HRMSApp.Business.Services;
using HRMSApp.Web.Models;
using System.Linq.Expressions;

namespace HRMSApp.Web.Controllers
{
    public enum Gender
    {
        M,
        F
    };

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IDeptEmpService _deptEmpService;
        private readonly IDeptManagerService _deptManagerService;
        private readonly ISalaryService _salaryService;
        private readonly ITitleService _titleService;

        public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService, IDepartmentService departmentService,
            IDeptEmpService deptEmpService, IDeptManagerService deptManagerService, ISalaryService salaryService, ITitleService titleService
            )
        {
            _logger = logger;
            _employeeService = employeeService;
            _departmentService = departmentService;
            _deptEmpService = deptEmpService;
            _deptManagerService = deptManagerService;
            _salaryService = salaryService;
            _titleService = titleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Generate()
        {
            var existingEmployess = await _employeeService.GetAll();
            if (existingEmployess.Count > 0)
                return View();

            var daysGenerator = new RandomGenerator();
            string[] gender = { "M", "F" };
            var employees = Builder<Employee>.CreateListOfSize(10000)
                       .All()
                       .With(c => c.Ssn = Faker.Identification.SocialSecurityNumber())
                       .With(c => c.FirstName = Faker.Name.First())
                       .With(c => c.MiddleName = Faker.Name.Middle())
                       .With(c => c.LastName = Faker.Name.Last())
                       .With(c => c.Gender = Faker.Extensions.ArrayExtensions.Random(gender))
                       .With(c => c.HireDate = DateTime.Now.AddDays(-daysGenerator.Next(1, 3650))) // HireDate within last 10 years
                       .With(c => c.BirthDate = DateTime.Now.AddDays(-daysGenerator.Next(6750, 21170)))// Age between 18 to 58 Years
                       .With(c => c.Address1 = Faker.Address.StreetAddress())
                       .With(c => c.Address2 = Faker.Address.StreetName())
                       .With(c => c.City = Faker.Address.City())
                       .With(c => c.State = Faker.Address.UsStateAbbr())
                       .With(c => c.Zipcode = Faker.Address.ZipCode())
                       .With(c => c.HomePhone = Faker.Phone.Number())
                       .With(c => c.MobilePhone = Faker.Phone.Number())
                       .With(c => c.Email = Faker.Internet.Email())

                       .Build();



            foreach (var employee in employees)
            {
                try
                {
                    employee.HomePhone = employee.HomePhone.Replace("(", "").Replace(")", "").Replace("-", "");
                    if (employee.HomePhone.Length > 12)
                        employee.HomePhone = employee.HomePhone.Substring(0, 12);
                    else
                        employee.HomePhone = employee.HomePhone.Substring(0, employee.HomePhone.Length);
                    employee.MobilePhone = employee.MobilePhone.Replace("(", "").Replace(")", "").Replace("-", "");

                    if (employee.MobilePhone.Length > 12)
                        employee.MobilePhone = employee.MobilePhone.Substring(0, 12);
                    else
                        employee.MobilePhone = employee.MobilePhone.Substring(0, employee.MobilePhone.Length);
                    await _employeeService.AddEmployee(employee);

                }
                catch (Exception ex)
                {
                }
            }

            string[] departments = { "Transport",
            "Security",
            "Research and development",
            "Product development",
            "Learning and development",
            "IT services",
            "Infrastructures",
            "Engineering",
            "Business development",
            "Admin",
            "Accounts and Finance"
            };

            string[] titles = { "Vice-President",
            "Senior Account Manager",
            "Account Manager",
            "Senior Project Manager",
            "Project Manager",
            "Senior Technical Lead",
            "Technical Lead",
            "Project Lead",
            "Senior Team Lead",
            "Team Lead",
            "Module Lead",
            "Senior Software Engineer",
            "Software Engineer",
            "Associate Software Engineer",
            "Intern"
            };

            for (int i = 0; i < departments.Length; i++)
            {

                Department dept = new Department();
                dept.DeptNo = "D" + i;
                dept.Name = departments[i];
                await _departmentService.AddDepartment(dept);

                var flag = false;
                while (!flag)
                {
                    try
                    {
                        var index = daysGenerator.Next(1, employees.Count);
                        var emp = employees[index - 1];

                        DeptManager deptManager = new DeptManager();
                        deptManager.DeptNo = dept.DeptNo;
                        deptManager.EmpNo = emp.EmpNo;
                        deptManager.FromDate = DateTime.Now.AddDays(-daysGenerator.Next(1, 3650));
                        deptManager.ToDate = deptManager.FromDate.Value.AddDays(daysGenerator.Next(1, 3650));

                        await _deptManagerService.AddDeptManager(deptManager);
                        flag = true;
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                }
            }

            foreach (var employee in employees)
            {
                try
                {
                    var index = daysGenerator.Next(1, departments.Length);
                    var dept = departments[index - 1];
                    DeptEmp deptEmp = new DeptEmp();
                    deptEmp.DeptNo = "D" + (index - 1).ToString();
                    deptEmp.EmpNo = employee.EmpNo;
                    deptEmp.FromDate = DateTime.Now.AddDays(-daysGenerator.Next(1, 3650));
                    deptEmp.ToDate = deptEmp.FromDate.Value.AddDays(daysGenerator.Next(1, 3650));
                    Expression<Func<DeptEmp, bool>> deptEmpPred = d => (d.EmpNo == employee.EmpNo && d.DeptNo == deptEmp.DeptNo);
                    var dept_emp = _deptEmpService.FindDeptEmp(deptEmpPred).Result.FirstOrDefault();
                    if (dept_emp == null)
                    {
                        await _deptEmpService.AddDeptEmp(deptEmp);
                    }

                    Salary salary = new Salary();
                    salary.EmpNo = employee.EmpNo;
                    salary.Salary1 = daysGenerator.Next(25000, 200000);
                    salary.FromDate = DateTime.Now.AddDays(-daysGenerator.Next(1, 3650));
                    salary.ToDate = salary.FromDate.Value.AddDays(daysGenerator.Next(1, 3650));
                    await _salaryService.AddSalary(salary);

                    index = daysGenerator.Next(1, titles.Length);
                    var sTitle = titles[index - 1];
                    Title title = new Title();
                    title.EmpNo = employee.EmpNo;
                    title.Title1 = sTitle;
                    title.FromDate = DateTime.Now.AddDays(-daysGenerator.Next(1, 3650));
                    title.ToDate = title.FromDate.Value.AddDays(daysGenerator.Next(1, 3650));
                    await _titleService.AddTitle(title);
                }
                catch (Exception ex)
                {
                }
            }


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
