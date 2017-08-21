using System;
using Excel.Interface;
using General.CIR.Commands;
using Microsoft.Practices.Prism.ViewModel;

namespace General.CIR.Models.Units
{
	[ExcelLocation("设置界面响应.xls", "Sheet1")]
	public class SettingItem : NotificationObject, ISetValueProvider
	{
		private string m_Names;

		private ItemType m_Type;

		[ExcelField("编号", false)]
		public int Index
		{
			get;
			set;
		}

		[ExcelField("显示文字", false)]
		public string Names
		{
			get
			{
				return m_Names;
			}
			set
			{
				bool flag = value == m_Names;
				if (!flag)
				{
					m_Names = value;
					RaisePropertyChanged<string>(() => Names);
				}
			}
		}

		[ExcelField("响应", false)]
		public CustomCommandBase Response
		{
			get;
			set;
		}

		[ExcelField("类型", false)]
		public ItemType Type
		{
			get
			{
				return m_Type;
			}
			set
			{
				m_Type = value;
			}
		}

		public void SetValue(string propertyOrFieldName, string value)
		{
			if (!(propertyOrFieldName == "Response"))
			{
				if (propertyOrFieldName == "Type")
				{
					Enum.TryParse<ItemType>(value, false, out m_Type);
				}
			}
			else
			{
				string typeName = string.Format("General.CIR.Commands.SettingItemResponse.{0}", value);
				Type expr_32 = System.Type.GetType(typeName);
				Response = (((expr_32 != null) ? expr_32.Assembly.CreateInstance(typeName) : null) as CustomCommandBase);
			}
		}
	}
}
