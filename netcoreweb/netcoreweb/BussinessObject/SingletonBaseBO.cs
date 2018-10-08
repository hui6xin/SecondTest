using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcoreweb.BussinessObject
{
	public class SingletonBaseBo<T>
	{
		private static SingletonBaseBo<T> _instance = null;
		private static readonly object Padlock = new object();

		SingletonBaseBo()
		{
		}

		public static SingletonBaseBo<T> Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (Padlock)
					{
						if (_instance == null)
						{
							_instance = new SingletonBaseBo<T>();
						}
					}
				}
				return _instance;
			}
		}
	}
}
