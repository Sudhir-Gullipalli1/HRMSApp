using HRMSApp.Business.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMSApp.Web.Helpers
{
    public static class Common
    {

        public static DateTime GetCurrentTime()
        {
            DateTime currentDateTime = DateTime.Now;

            TimeZone localZone = TimeZone.CurrentTimeZone;

            return currentDateTime;
        }
        public static List<SelectListItem> GetDepartments(IDepartmentService departmentService)
        {
            var items = departmentService.GetAll().Result;

            var list = new List<SelectListItem>();

            foreach (var item in items)
            {
                list.Add(new SelectListItem { Value = item.DeptNo, Text = item.Name });
            }
            list = list.OrderBy(o => o.Text).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = " -- Please Select -- " });

            return list;
        }

        public static List<SelectListItem> GetEmployees(IEmployeeService employeeService)
        {
            var items = employeeService.GetAll().Result;

            var list = new List<SelectListItem>();

            foreach (var item in items)
            {
                list.Add(new SelectListItem { Value = item.EmpNo.ToString(), Text = item.FirstName + " " + item.LastName });
            }
            list = list.OrderBy(o => o.Text).ToList();
            list.Insert(0, new SelectListItem { Value = "", Text = " -- Please Select -- " });

            return list;
        }

        public static long ConvertToTimestamp(DateTime value)
        {
            long epoch = (value.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return epoch;
        }

    }
}


