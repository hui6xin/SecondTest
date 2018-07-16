using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SQLitePCL;
using SQLite;

namespace WpfApp1
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		private SQLiteConnection sqLiteConnection;
		public MainWindow()
		{
			InitializeComponent();
			//SQLiteConnection.CreateFile(AppDomain.CurrentDomain.BaseDirectory+ "db\\third.db");
			sqLiteConnection = new SQLiteConnection(AppDomain.CurrentDomain.BaseDirectory + "db\\third.db;"+ "Version = 3; Password = 999999;");
			//sqLiteConnection = new SQLiteConnection(AppDomain.CurrentDomain.BaseDirectory + "db\\third.db");


			//sqLiteConnection.SetPassword("999999");


			//sqLiteConnection.ChangePassword("999999");


		}
	}
}
