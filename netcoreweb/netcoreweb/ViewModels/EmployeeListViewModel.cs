using System.Collections.Generic;

namespace netcoreweb.ViewModels
{
	public class EmployeeListViewModel : BaseViewModel
	{
		public List<EmployeeViewModel> Employees { get; set; }
	}
}