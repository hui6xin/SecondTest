using System.Collections.Generic;
using System.Net.Http;
using netcoreweb.DataBaseLayer;
using SqlSugar;

namespace netcoreweb.Models
{
	public class EmployeeBusinessLayer
	{
		public List<Employee> GetEmployees()
		{
			SqlHelper.createTable();
			var db = SqlHelper.GetInstance();
			var employees = db.Queryable<Employee>().ToList();
			return employees;
		}

		public bool IsValidUser(UserDetails u)
		{
			if (u.UserName == "Admin" && u.Password == "Admin")
				return true;
			return false;
		}

		public UserStatus GetUserValidity(UserDetails u)
		{
			if (u.UserName == "Admin" && u.Password == "Admin")
				return UserStatus.AuthenticatedAdmin;
			if (u.UserName == "Sukesh" && u.Password == "Sukesh")
				return UserStatus.AuthenticatedUser;
			return UserStatus.NonAuthenticatedUser;
		}

		public void UploadEmployees(List<Employee> employees)
		{
			var db = SqlHelper.GetInstance();
			if (employees != null)
				db.Insertable(employees).With(SqlWith.UpdLock).ExecuteReturnBigIdentity();
		}
	}
}