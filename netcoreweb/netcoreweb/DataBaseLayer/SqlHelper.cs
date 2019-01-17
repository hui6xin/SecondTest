using netcoreweb.Models;
using SqlSugar;

namespace netcoreweb.DataBaseLayer
{
	public class SqlHelper
	{
		public static SqlSugarClient GetInstance()
		{
			var db = new SqlSugarClient(new ConnectionConfig
			{
				ConnectionString =
					"Database=test;Data Source=www.leonis.site;Uid=root;Pwd=666666;pooling=false;CharSet=utf8;port=9010", //必填, 数据库连接字符串
				DbType = DbType.MySql, //必填, 数据库类型
				//IsAutoCloseConnection = true,       //默认false, 时候知道关闭数据库连接, 设置为true无需使用using或者Close操作
				InitKeyType = InitKeyType.Attribute, //默认SystemTable, 字段信息读取, 如：该属性是不是主键，是不是标识列等等信息
				IsShardSameThread = true
			});
			return db;
		}

		public static void createTable()
		{
			//todo net to move to project init
			var db = GetInstance();
			if (!db.DbMaintenance.IsAnyTable("Employee")) db.CodeFirst.InitTables(typeof(Employee));
		}
	}
}