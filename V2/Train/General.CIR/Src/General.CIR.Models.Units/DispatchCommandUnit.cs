using System;
using General.CIR.CIRData;
using General.CIR.Models.States;
using Microsoft.Practices.Prism.ViewModel;

namespace General.CIR.Models.Units
{
	public class DispatchCommandUnit : NotificationObject
	{
		private bool m_IsSign;

		private string m_Content;

		public DispatchInfo Info
		{
			get;
			private set;
		}

		public CommandType CommandType
		{
			get;
			set;
		}

		public string CommandName
		{
			get;
			set;
		}

		public string Number
		{
			get;
			set;
		}

		public string ReleaseName
		{
			get;
			set;
		}

		public string ReleasePlace
		{
			get;
			set;
		}

		public DateTime ReleaseTime
		{
			get;
			set;
		}

		public string TrainNumber
		{
			get;
			set;
		}

		public string Current
		{
			get;
			set;
		}

		public string EngineNumber
		{
			get;
			set;
		}

		public string Content
		{
			get
			{
				return m_Content;
			}
			set
			{
				bool flag = value == m_Content;
				if (!flag)
				{
					m_Content = value;
					RaisePropertyChanged<string>(() => Content);
				}
			}
		}

		public bool IsSign
		{
			get
			{
				return m_IsSign;
			}
			set
			{
				bool flag = value == m_IsSign;
				if (!flag)
				{
					m_IsSign = value;
					RaisePropertyChanged<bool>(() => IsSign);
				}
			}
		}

		public DispatchCommandUnit(DispatchInfo info)
		{
			Info = info;
		}
	}
}
