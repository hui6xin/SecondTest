using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			var conn = new SQLiteAsyncConnection("db.sqlite");
			//AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
			base.OnStartup(e);
		}


		//Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		//{
		//	//系统位数
		//	var type = Environment.Is64BitOperatingSystem;
		//	AssemblyName assemblyName = new AssemblyName(args.Name);
		//	return Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DLL"));
		//}
	}
}
