using HRMSApp.Business.Services;
using HRMSApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRMSApp1.Web.Controllers
{
    public class employeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public employeesController(
           IEmployeeService employeeService
        )
        {
            _employeeService = employeeService;
        }


        // GET: employees
        public async Task<ActionResult> Index()
        {
            var items = await _employeeService.GetAll();
            return View(items);
        }

        // GET: employees/Details/5
        public ActionResult Details(int? empNo)
        {
            Expression<Func<Employee, bool>> employeePred = d => (d.EmpNo == empNo);
            var employee = _employeeService.FindEmployee(employeePred).Result.FirstOrDefault();

            if (employee == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(employee);
        }

        // GET: employees/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _employeeService.AddEmployee(employee);
                    return RedirectToAction("Index");
                }

                return View(employee);
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

                return View(employee);
            }
        }

        // GET: employees/Edit/5
        public ActionResult Edit(int? empNo)
        {
            Expression<Func<Employee, bool>> employeePred = d => (d.EmpNo == empNo);
            var employee = _employeeService.FindEmployee(employeePred).Result.FirstOrDefault();
           
            if (employee == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(employee);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _employeeService.UpdateEmployee(employee);
                    return RedirectToAction("Index");
                }

                return View(employee);
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

                return View(employee);
            }
        }

        public async Task<ActionResult> Delete(int? empNo)
        {
            Expression<Func<Employee, bool>> employeePred = d => (d.EmpNo == empNo);
            var employee = _employeeService.FindEmployee(employeePred).Result.FirstOrDefault();

            if (employee == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int? empNo)
        {
            Expression<Func<Employee, bool>> employeePred = d => (d.EmpNo == empNo);
            var employee = _employeeService.FindEmployee(employeePred).Result.FirstOrDefault();
            try
            {
                if (employee == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                await _employeeService.DeleteEmployee(employee);
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

                return View(employee);
            }
        }
    }
}
