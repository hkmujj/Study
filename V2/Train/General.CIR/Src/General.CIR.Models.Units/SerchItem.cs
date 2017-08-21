using System;
using Excel.Interface;
using General.CIR.Commands;

namespace General.CIR.Models.Units
{
	[ExcelLocation("查询界面内容.xls", "Sheet1")]
	public class SerchItem : ISetValueProvider
	{
		[ExcelField("编号", false)]
		public int Index
		{
			get;
			set;
		}

		[ExcelField("内容", false)]
		public string Content
		{
			get;
			set;
		}

		[ExcelField("命令", false)]
		public CustomCommandBase Response
		{
			get;
			set;
		}

		[ExcelField("参数", false)]
		public string Parmers
		{
			get;
			set;
		}

		public void SetValue(string propertyOrFieldName, string value)
		{
			bool flag = propertyOrFieldName.Equals("Response") && !string.IsNullOrEmpty(value);
			if (flag)
			{
				string typeName = string.Format("General.CIR.Commands.SerchItemResponse.{0}", value);
				Type expr_32 = Type.GetType(typeName);
				Response = (((expr_32 != null) ? expr_32.Assembly.CreateInstance(typeName) : null) as CustomCommandBase);
			}
		}
	}
}
