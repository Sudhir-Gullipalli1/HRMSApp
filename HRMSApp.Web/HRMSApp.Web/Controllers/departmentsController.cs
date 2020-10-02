using HRMSApp.Business.Services;
using HRMSApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRMSApp1.Web.Controllers
{

    public class departmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public departmentsController(
           IDepartmentService departmentService
        )
        {
            _departmentService = departmentService;
        }

        // GET: departments
        public async Task<ActionResult> Index()
        {
            var items = await _departmentService.GetAll();
            return View(items);
        }

        // GET: departments/Details/5
        public ActionResult Details(string deptNo)
        {
            Expression<Func<Department, bool>> departmentPred = d => (d.DeptNo == deptNo);
            var department = _departmentService.FindDepartment(departmentPred).Result.FirstOrDefault();

            if (department == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(department);
        }

        // GET: departments/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _departmentService.AddDepartment(department);
                    return RedirectToAction("Index");
                }

                return View(department);
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

                return View(department);
            }
        }

        // GET: departments/Edit/5
        public ActionResult Edit(string deptNo)
        {
            Expression<Func<Department, bool>> departmentPred = d => (d.DeptNo == deptNo);
            var department = _departmentService.FindDepartment(departmentPred).Result.FirstOrDefault();

            if (department == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(department);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _departmentService.UpdateDepartment(department);
                    return RedirectToAction("Index");
                }

                return View(department);
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

                return View(department);
            }
        }

        // GET: departments/Delete/5
        public async Task<ActionResult> Delete(string deptNo)
        {
            Expression<Func<Department, bool>> departmentPred = d => (d.DeptNo == deptNo);
            var department = _departmentService.FindDepartment(departmentPred).Result.FirstOrDefault();
            if (department == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(string deptNo)
        {
            Expression<Func<Department, bool>> departmentPred = d => (d.DeptNo == deptNo);
            var department = _departmentService.FindDepartment(departmentPred).Result.FirstOrDefault();
            try
            {
                if (department == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                await _departmentService.DeleteDepartment(department);
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

                return View(department);
            }
        }
    }
}
