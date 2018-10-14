﻿using System;

using System.Data.Entity;
using System.Linq;

using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using TelerikMvc.Models;

namespace TelerikMvc.Controllers
{
    public class EmployeeController : Controller
    {
        private HarpreetEntities db = new HarpreetEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Employees_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Employee> employees = db.Employees;
            DataSourceResult result = employees.ToDataSourceResult(request, employee => new {
                EmpId = employee.EmpId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Phone = employee.Phone,
                Salary = employee.Salary,
                Email = employee.Email,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Employees_Create([DataSourceRequest]DataSourceRequest request, Employee employee)
        {
            if (ModelState.IsValid)
            {
                var entity = new Employee
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    MiddleName = employee.MiddleName,
                    Phone = employee.Phone,
                    Salary = employee.Salary,
                    Email = employee.Email,
                };

                db.Employees.Add(entity);
                db.SaveChanges();
                employee.EmpId = entity.EmpId;
            }

            return Json(new[] { employee }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Employees_Update([DataSourceRequest]DataSourceRequest request, Employee employee)
        {
            if (ModelState.IsValid)
            {
                var entity = new Employee
                {
                    EmpId = employee.EmpId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    MiddleName = employee.MiddleName,
                    Phone = employee.Phone,
                    Salary = employee.Salary,
                    Email = employee.Email,
                };

                db.Employees.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { employee }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Employees_Destroy([DataSourceRequest]DataSourceRequest request, Employee employee)
        {
            if (ModelState.IsValid)
            {
                var entity = new Employee
                {
                    EmpId = employee.EmpId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    MiddleName = employee.MiddleName,
                    Phone = employee.Phone,
                    Salary = employee.Salary,
                    Email = employee.Email,
                };

                db.Employees.Attach(entity);
                db.Employees.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { employee }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    
        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
