using netcoreweb.ViewModels;

namespace netcoreweb.Models
{
	/// <summary>
	/// </summary>
	public class CreateEmployeeViewModel : BaseViewModel
	{
		/// <summary>
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		/// </summary>
		public string Salary { get; set; }
	}
}