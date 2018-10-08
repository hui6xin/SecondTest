using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using netcoreweb.Models;

namespace netcoreweb.Controllers
{
	/// <summary>
	/// </summary>
	public class AuthenticationController : Controller
	{
		/// <summary>
		/// </summary>
		/// <returns></returns>
		public IActionResult OnGet()
		{
			//登录授权直接跳转index界面
			if (HttpContext.User.Identity.IsAuthenticated) return RedirectToPage("Index");
			return View("Login0");
		}

		/// <summary>
		/// </summary>
		/// <param name="u"></param>
		/// <param name="ReturnUrl"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Login(UserDetails u, string ReturnUrl)
		{
			if (ModelState.IsValid)
			{
				var bal = new EmployeeBusinessLayer();
				if (bal.IsValidUser(u))
				{
					//New Code Start
					var status = bal.GetUserValidity(u);
					var IsAdmin = false;
					if (status == UserStatus.AuthenticatedAdmin)
					{
						IsAdmin = true;
					}
					else if (status == UserStatus.AuthenticatedUser)
					{
						IsAdmin = false;
					}
					else
					{
						ModelState.AddModelError("CredentialError", "Invalid Username or Password");
						return View("Login0");
					}

					var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
					identity.AddClaim(new Claim(ClaimTypes.Sid, u.UserName));
					identity.AddClaim(new Claim(ClaimTypes.Name, u.UserName));
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
					HttpContext.Session.SetString("IsAdmin", IsAdmin.ToString());
					//HttpContext.Session.Set("IsAdmin", new byte[]{Convert.ToByte(IsAdmin)} );
					if (string.IsNullOrEmpty(ReturnUrl)) return RedirectToAction("Index", "Employee");

					return Redirect(ReturnUrl);

					//FormsAuthentication.SetAuthCookie(u.UserName, false);
					//Session["IsAdmin"] = IsAdmin;
					//登陆授权
				}

				ModelState.AddModelError("CredentialError", "Invalid Username or Password");
				return View("Login0");
			}

			return View("Login0");
		}

		/// <summary>
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> Logout()
		{
			if (HttpContext.User.Identity.IsAuthenticated)
				await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return View("Login0");
		}

		/// <summary>
		///     退出
		/// </summary>
		/// <returns></returns>
		public async Task<IActionResult> OnGetLoginOutAsync()
		{
			if (HttpContext.User.Identity.IsAuthenticated)
				await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login");
		}

		/// <summary>
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			return View("Login0");
		}

		public IActionResult RegistView()
		{
			return View("Register0");
		}
	}
}