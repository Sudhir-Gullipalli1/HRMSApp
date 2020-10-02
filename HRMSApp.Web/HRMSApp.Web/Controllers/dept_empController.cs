using HRMSApp.Business.Services;
using HRMSApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HRMSApp.Web.Helpers;
using System.Linq.Expressions;
using System;

namespace HRMSApp1.Web.Controllers
{
    public class dept_empController : Controller
    {
        private readonly IDeptEmpService _deptEmpService;
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;

        public dept_empController(
           IDeptEmpService deptEmpService,
           IDepartmentService departmentService,
           IEmployeeService employeeService
        )
        {
            _deptEmpService = deptEmpService;
            _departmentService = departmentService;
            _employeeService = employeeService;
        }

        // GET: dept_emp
        public async Task<ActionResult> Index()
        {
            var items = await _deptEmpService.GetAll();
            return View(items);
        }

        // GET: dept_emp/Details/5
        public ActionResult Details(int? empNo, string deptNo)
        {
            Expression<Func<DeptEmp, bool>> deptEmpPred = d => (d.EmpNo == empNo && d.DeptNo == deptNo);
            var dept_emp = _deptEmpService.FindDeptEmp(deptEmpPred).Result.FirstOrDefault();
            if (dept_emp == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var departments = Common.GetDepartments(_departmentService);
            ViewBag.DepartmentList = departments;

            var employees = Common.GetEmployees(_employeeService);
            ViewBag.EmployeeList = employees;
            return View(dept_emp);
        }

        // GET: dept_emp/Create
        public ActionResult Create()
        {
            var departments = Common.GetDepartments(_departmentService);
            ViewBag.DepartmentList = departments;

            var employees = Common.GetEmployees(_employeeService);
            ViewBag.EmployeeList = employees;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(DeptEmp dept_emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _deptEmpService.AddDeptEmp(dept_emp);
                    return RedirectToAction("Index");
                }
                var departments = Common.GetDepartments(_departmentService);
                ViewBag.DepartmentList = departments;

                var employees = Common.GetEmployees(_employeeService);
                ViewBag.EmployeeList = employees;
                return View(dept_emp);
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

                var departments = Common.GetDepartments(_departmentService);
                ViewBag.DepartmentList = departments;

                var employees = Common.GetEmployees(_employeeService);
                ViewBag.EmployeeList = employees;
                return View(dept_emp);
            }
        }

        // GET: dept_emp/Edit/5
        public ActionResult Edit(int? empNo, string deptNo)
        {
            Expression<Func<DeptEmp, bool>> deptEmpPred = d => (d.EmpNo == empNo && d.DeptNo == deptNo);
            var dept_emp = _deptEmpService.FindDeptEmp(deptEmpPred).Result.FirstOrDefault();
            if (dept_emp == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var departments = Common.GetDepartments(_departmentService);
            ViewBag.DepartmentList = departments;

            var employees = Common.GetEmployees(_employeeService);
            ViewBag.EmployeeList = employees;
            return View(dept_emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DeptEmp dept_emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _deptEmpService.UpdateDeptEmp(dept_emp);
                    return RedirectToAction("Index");
                }

                var departments = Common.GetDepartments(_departmentService);
                ViewBag.DepartmentList = departments;

                var employees = Common.GetEmployees(_employeeService);
                ViewBag.EmployeeList = employees;
                return View(dept_emp);
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

                var departments = Common.GetDepartments(_departmentService);
                ViewBag.DepartmentList = departments;

                var employees = Common.GetEmployees(_employeeService);
                ViewBag.EmployeeList = employees;
                return View(dept_emp);
            }
        }


        public ActionResult Delete(int? empNo, string deptNo)
        {
            Expression<Func<DeptEmp, bool>> deptEmpPred = d => (d.EmpNo == empNo && d.DeptNo == deptNo);
            var dept_emp = _deptEmpService.FindDeptEmp(deptEmpPred).Result.FirstOrDefault();
            if (dept_emp == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(dept_emp);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int? empNo, string deptNo)
        {
            Expression<Func<DeptEmp, bool>> deptEmpPred = d => (d.EmpNo == empNo && d.DeptNo == deptNo);
            var dept_emp = _deptEmpService.FindDeptEmp(deptEmpPred).Result.FirstOrDefault();
            try
            {
                if (dept_emp == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                await _deptEmpService.DeleteDeptEmp(dept_emp);
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

                return View(dept_emp);
            }
        }
    }
}
