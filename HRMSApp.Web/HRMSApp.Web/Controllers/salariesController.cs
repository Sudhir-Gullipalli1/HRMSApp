using HRMSApp.Business.Services;
using HRMSApp.Data.Models;
using HRMSApp.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRMSApp1.Web.Controllers
{
    public class salariesController : Controller
    {
        private readonly ISalaryService _salaryService;
        private readonly IEmployeeService _employeeService;

        public salariesController(
           ISalaryService salaryService,
           IEmployeeService employeeService
        )
        {
            _salaryService = salaryService;
            _employeeService = employeeService;
        }

        // GET: salaries
        public async Task<ActionResult> Index()
        {
            var items = await _salaryService.GetAll();
            return View(items);
        }

        // GET: salaries/Details/5
        public ActionResult Details(int? empNo, int? salary1)
        {
            Expression<Func<Salary, bool>> salaryPred = s => (s.EmpNo == empNo && s.Salary1 == salary1);
            var salary = _salaryService.FindSalary(salaryPred).Result.FirstOrDefault();
            if (salary == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var employees = Common.GetEmployees(_employeeService);
            ViewBag.EmployeeList = employees;
            return View(salary);
        }

        // GET: salaries/Create
        public ActionResult Create()
        {
            var employees = Common.GetEmployees(_employeeService);
            ViewBag.EmployeeList = employees;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Salary salary)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _salaryService.AddSalary(salary);
                    return RedirectToAction("Index");
                }

                var employees = Common.GetEmployees(_employeeService);
                ViewBag.EmployeeList = employees;
                return View(salary);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    int index = ex.InnerException.Message.IndexOf("UC_");
                    string message = ex.InnerException.Message;
                    if (index > 0)
                        message = message.Substring(0, index - 1);
                    ModelState.AddModelError("Error", "There was an error during: " + message);
                }
                else
                    ModelState.AddModelError("Error", "There was an error during: " + ex.ToString());

                var employees = Common.GetEmployees(_employeeService);
                ViewBag.EmployeeList = employees;
                return View(salary);
            }
        }

        // GET: salaries/Edit/5
        public ActionResult Edit(int? empNo, int? salary1)
        {
            Expression<Func<Salary, bool>> salaryPred = s => (s.EmpNo == empNo && s.Salary1 == salary1);
            var salary = _salaryService.FindSalary(salaryPred).Result.FirstOrDefault();
            if (salary == null)
            {
                return RedirectToAction("Error", "Home");
            }
            
            var employees = Common.GetEmployees(_employeeService);
            ViewBag.EmployeeList = employees;
            return View(salary);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Salary salary)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _salaryService.UpdateSalary(salary);
                    return RedirectToAction("Index");
                }

                var employees = Common.GetEmployees(_employeeService);
                ViewBag.EmployeeList = employees;
                return View(salary);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    int index = ex.InnerException.Message.IndexOf("UC_");
                    string message = ex.InnerException.Message;
                    if (index > 0)
                        message = message.Substring(0, index - 1);
                    ModelState.AddModelError("Error", "There was an error during: " + message);
                }
                else
                    ModelState.AddModelError("Error", "There was an error during: " + ex.ToString());

                var employees = Common.GetEmployees(_employeeService);
                ViewBag.EmployeeList = employees;
                return View(salary);
            }
        }


        public ActionResult Delete(int? empNo, int? salary1)
        {
            Expression<Func<Salary, bool>> salaryPred = s => (s.EmpNo == empNo && s.Salary1 == salary1);
            var salary = _salaryService.FindSalary(salaryPred).Result.FirstOrDefault();
            if (salary == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(salary);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int? empNo, int? salary1)
        {
            Expression<Func<Salary, bool>> salaryPred = s => (s.EmpNo == empNo && s.Salary1 == salary1);
            var salary = _salaryService.FindSalary(salaryPred).Result.FirstOrDefault();
            try
            {
                if (salary == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                await _salaryService.DeleteSalary(salary);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    int index = ex.InnerException.Message.IndexOf("fails");
                    string message = ex.InnerException.Message;
                    if (index > 0)
                        message = message.Substring(0, index - 1);
                    ModelState.AddModelError("Error", "There was an error during: " + message);
                }
                else
                    ModelState.AddModelError("Error", "There was an error during: " + ex.ToString());

                return View(salary);
            }
        }
    }
}
