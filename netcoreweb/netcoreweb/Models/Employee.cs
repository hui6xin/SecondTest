using System.ComponentModel.DataAnnotations;
using SqlSugar;

namespace netcoreweb.Models
{
	[SugarTable("Employee")]
	public class Employee
	{
		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnDataType = "BigInt", ColumnDescription = "ID",
			ColumnName = "ID")]
		public int EmployeeId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(Length = 20)]
		[Required(ErrorMessage = "Enter First Name")]
		[FirstNameValidation]
		public string FirstName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[SugarColumn(Length = 20)]
		[StringLength(8, MinimumLength = 5, ErrorMessage = "Last Name length should not be greater than 8")]
		public string LastName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Range(500, 5000, ErrorMessage = "500-5000")]
		public int? Salary { get; set; }
	}
	/// <summary>
	/// 
	/// </summary>
	public class FirstNameValidation : ValidationAttribute
	{/// <summary>
	 /// 
	 /// </summary>
	 /// <param name="value"></param>
	 /// <param name="validationContext"></param>
	 /// <returns></returns>
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null) // Checking for Empty Value
			{
				return new ValidationResult("Please Provide First Name");
			}

			if (value.ToString().Contains("@")) return new ValidationResult("First Name should contain @");
			return ValidationResult.Success;
		}
	}
}