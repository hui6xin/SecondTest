using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace netcoreweb.Controllers
{
    public class LoginController : Controller
    {
	    public IActionResult OnGet()
	    {
		    //登录授权直接跳转index界面
		  
		    return View("Index");
	    }

		public IActionResult Index()
        {
            return View("Index");
        }

	    public ActionResult StartView()
	    {
		    return View("BackgroudControlStartView");
	    }
    }
}