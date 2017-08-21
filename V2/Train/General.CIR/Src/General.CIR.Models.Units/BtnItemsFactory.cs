using System.Collections.Generic;
using System.Linq;
using Excel.Interface;
using General.CIR.Interfaces;

namespace General.CIR.Models.Units
{
	public class BtnItemsFactory : IBtnItemsFactory
	{
		protected IDictionary<string, IBtnItems> BtnItemSource
		{
			get; }

		public static BtnItemsFactory Instance
		{
			get;
			private set;
		}

		static BtnItemsFactory()
		{
			Instance = new BtnItemsFactory();
		}

		public BtnItemsFactory()
		{
			BtnItemSource = ExcelParser.Parser<BtnItems>(GlobalParams.Instance.SystemConfig.AppConfigPath).ToDictionary(t=>t.Keys, t=>(IBtnItems)t);
		}

		public IBtnItems GetOrCreateBtn(string key)
		{
			bool flag = BtnItemSource.ContainsKey(key);
			IBtnItems result;
			if (flag)
			{
				result = BtnItemSource[key];
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
