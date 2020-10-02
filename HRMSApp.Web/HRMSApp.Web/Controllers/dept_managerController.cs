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
    public class dept_managerController : Controller
    {
        private readonly IDeptManagerService _deptManagerService;
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;

        public dept_managerController(
           IDeptManagerService deptManagerService,
           IDepartmentService departmentService,
           IEmployeeService employeeService
        )
        {
            _deptManagerService = deptManagerService;
            _departmentService = departmentService;
            _employeeService = employeeService;
        }


        // GET: dept_manager
        public async Task<ActionResult> Index()
        {
            var items = await _deptManagerService.GetAll();
            return View(items);
        }

        // GET: dept_manager/Details/5
        public ActionResult Details(int? empNo, string deptNo)
        {
            Expression<Func<DeptManager, bool>> deptManagerPred = d => (d.EmpNo == empNo && d.DeptNo == deptNo);
            var dept_manager = _deptManagerService.FindDeptManager(deptManagerPred).Result.FirstOrDefault();
            if (dept_manager == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var departments = Common.GetDepartments(_departmentService);
            ViewBag.DepartmentList = departments;

            var employees = Common.GetEmployees(_employeeService);
            ViewBag.EmployeeList = employees;
            return View(dept_manager);
        }

        // GET: dept_manager/Create
        public ActionResult Create()
        {
            var departments = Common.GetDepartments(_departmentService);
            ViewBag.DepartmentList = departments;

            var employees = Common.GetEmployees(_employeeService);
            ViewBag.EmployeeList = employees;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(DeptManager dept_manager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _deptManagerService.AddDeptManager(dept_manager);
                    return RedirectToAction("Index");
                }
                var departments = Common.GetDepartments(_departmentService);
                ViewBag.DepartmentList = departments;

                var employees = Common.GetEmployees(_employeeService);
                ViewBag.EmployeeList = employees;
                return View(dept_manager);
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
                return View(dept_manager);
            }
        }

        // GET: dept_manager/Edit/5
        public ActionResult Edit(int? empNo, string deptNo)
        {
            Expression<Func<DeptManager, bool>> deptManagerPred = d => (d.EmpNo == empNo && d.DeptNo == deptNo);
            var dept_manager = _deptManagerService.FindDeptManager(deptManagerPred).Result.FirstOrDefault();
            if (dept_manager == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var departments = Common.GetDepartments(_departmentService);
            ViewBag.DepartmentList = departments;

            var employees = Common.GetEmployees(_employeeService);
            ViewBag.EmployeeList = employees;
            return View(dept_manager);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DeptManager dept_manager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _deptManagerService.UpdateDeptManager(dept_manager);
                    return RedirectToAction("Index");
                }

                var departments = Common.GetDepartments(_departmentService);
                ViewBag.DepartmentList = departments;

                var employees = Common.GetEmployees(_employeeService);
                ViewBag.EmployeeList = employees;
                return View(dept_manager);
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
                return View(dept_manager);
            }
        }

        public ActionResult Delete(int? empNo, string deptNo)
        {
            Expression<Func<DeptManager, bool>> deptManagerPred = d => (d.EmpNo == empNo && d.DeptNo == deptNo);
            var dept_manager = _deptManagerService.FindDeptManager(deptManagerPred).Result.FirstOrDefault();
            if (dept_manager == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(dept_manager);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int? empNo, string deptNo)
        {
            Expression<Func<DeptManager, bool>> deptManagerPred = d => (d.EmpNo == empNo && d.DeptNo == deptNo);
            var dept_manager = _deptManagerService.FindDeptManager(deptManagerPred).Result.FirstOrDefault();
            try
            {
                if (dept_manager == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                await _deptManagerService.DeleteDeptManager(dept_manager);
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

                return View(dept_manager);
            }
        }
    }
}
