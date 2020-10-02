using HRMSApp.Business.Services;
using HRMSApp.Data.Models;
using HRMSApp.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace HRMSApp1.Web.Controllers
{
    public class titlesController : Controller
    {
        private readonly ITitleService _titleService;
        private readonly IEmployeeService _employeeService;

        public titlesController(
           ITitleService titleService,
           IEmployeeService employeeService
        )
        {
            _titleService = titleService;
            _employeeService = employeeService;
        }

        // GET: titles
        public async Task<ActionResult> Index()
        {
            var items = await _titleService.GetAll();
            return View(items);
        }

        // GET: titles/Details/5
        public ActionResult Details(int? empNo, string title1)
        {
            Expression<Func<Title, bool>> titlePred = t => (t.EmpNo == empNo && t.Title1 == title1);
            var title = _titleService.FindTitle(titlePred).Result.FirstOrDefault();

            if (title == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var employees = Common.GetEmployees(_employeeService);
            ViewBag.EmployeeList = employees;
            return View(title);
        }

        // GET: titles/Create
        public ActionResult Create()
        {
            var employees = Common.GetEmployees(_employeeService);
            ViewBag.EmployeeList = employees;
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Title title)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _titleService.AddTitle(title);
                    return RedirectToAction("Index");
                }

                var employees = Common.GetEmployees(_employeeService);
                ViewBag.EmployeeList = employees;
                return View(title);
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
                return View(title);
            }
        }

        // GET: titles/Edit/5
        public ActionResult Edit(int? empNo, string title1)
        {
            Expression<Func<Title, bool>> titlePred = t => (t.EmpNo == empNo && t.Title1 == title1);
            var title = _titleService.FindTitle(titlePred).Result.FirstOrDefault();
            
            if (title == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var employees = Common.GetEmployees(_employeeService);
            ViewBag.EmployeeList = employees;
            return View(title);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Title title)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _titleService.UpdateTitle(title);
                    return RedirectToAction("Index");
                }

                var employees = Common.GetEmployees(_employeeService);
                ViewBag.EmployeeList = employees;
                return View(title);
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
                return View(title);
            }
        }

        public ActionResult Delete(int? empNo, string title1)
        {
            Expression<Func<Title, bool>> titlePred = t => (t.EmpNo == empNo && t.Title1 == title1);
            var title = _titleService.FindTitle(titlePred).Result.FirstOrDefault();

            if (title == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(title);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int? empNo, string title1)
        {
            Expression<Func<Title, bool>> titlePred = t => (t.EmpNo == empNo && t.Title1 == title1);
            var title = _titleService.FindTitle(titlePred).Result.FirstOrDefault();
            try
            {
                if (title == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                await _titleService.DeleteTitle(title);
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

                return View(title);
            }
        }
    }
}
