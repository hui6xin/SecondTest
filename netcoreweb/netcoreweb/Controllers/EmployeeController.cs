using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using netcoreweb.DataBaseLayer;
using netcoreweb.Filters;
using netcoreweb.Models;
using netcoreweb.ViewModels;
using SqlSugar;

namespace netcoreweb.Controllers
{
	[Authorize]
	[HeaderFooterFilter]
	public class EmployeeController : Controller
	{
		// GET: Test/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Test/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Test/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				// TODO: Add insert logic here

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View("Index");
			}
		}

		// GET: Test/Edit/5
		public ActionResult Edit(int id)
		{
			return View("Index");
		}

		// POST: Test/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View("Index");
			}
		}

		// GET: Test/Delete/5
		public ActionResult Delete(int id)
		{
			return View("Index");
		}

		// POST: Test/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View("Index");
			}
		}

		public ActionResult Index()
		{
			var employeeListViewModel = new EmployeeListViewModel();
			employeeListViewModel.UserName = User.Identity.Name; //New Line

			employeeListViewModel.FooterData = new FooterViewModel();
			employeeListViewModel.FooterData.CompanyName = "ssadasdasdasdasd"; //Can be set to dynamic value
			employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();

			var empBal = new EmployeeBusinessLayer();
			var employees = empBal.GetEmployees();

			var empViewModels = new List<EmployeeViewModel>();

			foreach (var emp in employees)
			{
				var empViewModel = new EmployeeViewModel();
				empViewModel.EmployeeName = emp.FirstName + " " + emp.LastName;
				empViewModel.Salary = emp.Salary?.ToString("C", new CultureInfo("en-US"));
				if (emp.Salary > 15000)
					empViewModel.SalaryColor = "yellow";
				else
					empViewModel.SalaryColor = "green";

				empViewModels.Add(empViewModel);
			}

			employeeListViewModel.Employees = empViewModels;
			//employeeListViewModel.UserName = "Admin";
			return View("Index", employeeListViewModel);
		}

		[AdminFilter]
		public ActionResult AddNew()
		{
			var employeeListViewModel = new CreateEmployeeViewModel();
			//employeeListViewModel.FooterData = new FooterViewModel();
			//employeeListViewModel.FooterData.CompanyName = "StepByStepSchools";//Can be set to dynamic value
			//employeeListViewModel.FooterData.Year = DateTime.Now.Year.ToString();
			//employeeListViewModel.UserName = User.Identity.Name; //New Line
			return View("CreateEmployee", employeeListViewModel);
		}

		[AdminFilter]
		public ActionResult GetAddNewLink()
		{
			if (Convert.ToBoolean(HttpContext.Session.GetString("IsAdmin")))
				return PartialView("AddNewLink");
			return new EmptyResult();
		}

		[HttpPost]
		[AdminFilter]
		public ActionResult SaveEmployee(Employee e, string BtnSubmit)
		{
			switch (BtnSubmit)
			{
				case "Save Employee":
					if (ModelState.IsValid)
					{
						var db = SqlHelper.GetInstance();
						db.Insertable(e).With(SqlWith.UpdLock).ExecuteReturnBigIdentity();
						return RedirectToAction("Index");
					}
					else
					{
						var vm = new CreateEmployeeViewModel();
						vm.FirstName = e.FirstName;
						vm.LastName = e.LastName;
						//vm.FooterData = new FooterViewModel();
						//vm.FooterData.CompanyName = "StepByStepSchools";//Can be set to dynamic value
						//vm.FooterData.Year = DateTime.Now.Year.ToString();
						vm.UserName = User.Identity.Name; //New Line
						if (e.Salary.HasValue)
							vm.Salary = e.Salary.ToString();
						else
							vm.Salary = ModelState["Salary"].AttemptedValue;
						return View("CreateEmployee", vm); // Day 4 Change - Passing e here
					}
				//return Content(e.FirstName + "|" + e.LastName + "|" + e.Salary);
				case "Cancel":
					return RedirectToAction("Index");
			}

			return new EmptyResult();
		}
	}
}