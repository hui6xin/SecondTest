using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using netcoreweb.Filters;
using netcoreweb.Models;
using netcoreweb.ViewModels;

namespace netcoreweb.Controllers
{
	/// <summary>
	/// </summary>
	[HeaderFooterFilter]
	public class BulkUploadController : Controller
	{
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			return View(new BaseViewModel());
		}

		/// <summary>
		/// </summary>
		/// <param name="files"></param>
		/// <returns></returns>
		public async Task<ActionResult> Upload(List<IFormFile> files)
		{
			var t1 = Thread.CurrentThread.ManagedThreadId;
			var formFiles = Request.Form.Files;
			var employees = await Task.Factory.StartNew(() => GetEmployees(formFiles.ToList()));
			var bal = new EmployeeBusinessLayer();
			var t2 = Thread.CurrentThread.ManagedThreadId;
			Console.WriteLine(t2);
			if (employees != null && employees.Count > 0)
			{
				bal.UploadEmployees(employees);
				return RedirectToAction("Index", "Employee");
			}

			return RedirectToAction("AddNew", "Employee");
		}

		/// <summary>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="files"></param>
		/// <returns></returns>
		private List<Employee> GetEmployees<T>(List<T> files)
		{
			var employees = new List<Employee>();
			long size = files.Count;
			var filePath = Path.GetTempFileName();
			if (size > 0)
			{
				var firstFile = files[0];
				var csvreader = StreamReader.Null;
				if (firstFile is IFormFile) csvreader = new StreamReader((firstFile as IFormFile).OpenReadStream());
				csvreader.ReadLine(); // Assuming first line is header
				while (!csvreader.EndOfStream)
				{
					var line = csvreader.ReadLine();
					var values = line.Split(','); //Values are comma separated
					//if (values.Length > 2)
					{
						var e = new Employee();
						e.FirstName = values[0];
						e.LastName = values[1];
						e.Salary = int.Parse(values[2]);
						employees.Add(e);
					}
				}
			}

			return employees;
		}
	}
}