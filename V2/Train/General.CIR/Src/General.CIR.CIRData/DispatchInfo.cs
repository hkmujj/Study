using System;
using System.Runtime.InteropServices;
using System.Text;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct DispatchInfo
	{
		public byte cmdType;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
		public byte[] dispatchDate;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
		public byte[] sendTime;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
		public byte[] trainNum;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
		public byte[] trainID;

		public byte cmdForm;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
		public byte[] cmdNum;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
		public byte[] dispatcher;

		public byte cmdStatus;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
		public byte[] defdate;

		public byte totalPack;

		public byte packNum;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 600)]
		public byte[] buffer;

		public string TrainID
		{
			get
			{
				return Encoding.Default.GetString(trainID);
			}
			set
			{
				string text = value;
				bool flag = text.Length > 8;
				if (flag)
				{
					text = text.Remove(8);
				}
				else
				{
					bool flag2 = text.Length < 8;
					if (flag2)
					{
						text = text.PadRight(8);
					}
				}
				trainID = Encoding.Default.GetBytes(text);
			}
		}

		public string TrainNum
		{
			get
			{
				return Encoding.Default.GetString(trainNum);
			}
			set
			{
				string text = value;
				bool flag = text.Length > 7;
				if (flag)
				{
					text = text.Remove(7);
				}
				else
				{
					bool flag2 = text.Length < 7;
					if (flag2)
					{
						text = text.PadRight(7);
					}
				}
				trainNum = Encoding.Default.GetBytes(text);
			}
		}

		public string Dispatcher
		{
			get
			{
				return Encoding.Default.GetString(dispatcher);
			}
			set
			{
				string text = value;
				bool flag = text.Length > 8;
				if (flag)
				{
					text = text.Remove(8);
				}
				else
				{
					bool flag2 = text.Length < 8;
					if (flag2)
					{
						text = text.PadRight(8);
					}
				}
				dispatcher = Encoding.Default.GetBytes(text);
			}
		}

		public string CmdNum
		{
			get
			{
				return Encoding.Default.GetString(cmdNum);
			}
			set
			{
				string text = value;
				bool flag = text.Length > 6;
				if (flag)
				{
					text = text.Remove(6);
				}
				else
				{
					bool flag2 = text.Length < 6;
					if (flag2)
					{
						text = text.PadRight(6);
					}
				}
				cmdNum = Encoding.Default.GetBytes(text);
			}
		}

		public DateTime DispatchDateTime
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(6);
				StringBuilder stringBuilder2 = new StringBuilder(Encoding.Default.GetString(dispatchDate));
				CIRCommAgent.BCD2Str(stringBuilder2, stringBuilder2.Length, stringBuilder);
				string text = stringBuilder.ToString();
				CIRDate cIRDate = default(CIRDate);
				cIRDate.year = (byte)int.Parse(text.Substring(0, 2));
				cIRDate.month = (byte)int.Parse(text.Substring(2, 2));
				cIRDate.day = (byte)int.Parse(text.Substring(4, 2));
				CIRTime cIRTime = default(CIRTime);
				cIRTime.hour = (byte)int.Parse(text.Substring(6, 2));
				cIRTime.minute = (byte)int.Parse(text.Substring(8, 2));
				cIRTime.second = (byte)int.Parse(text.Substring(10, 2));
				DateTime result = new DateTime((int)cIRDate.year, (int)cIRDate.month, (int)cIRDate.day, (int)cIRTime.hour, (int)cIRTime.minute, (int)cIRTime.second);
				return result;
			}
		}

		public DateTime SendTime
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(6);
				StringBuilder stringBuilder2 = new StringBuilder(Encoding.Default.GetString(sendTime));
				CIRCommAgent.BCD2Str(stringBuilder2, stringBuilder2.Length, stringBuilder);
				string text = stringBuilder.ToString();
				CIRTime cIRTime = default(CIRTime);
				cIRTime.hour = (byte)int.Parse(text.Substring(0, 2));
				cIRTime.minute = (byte)int.Parse(text.Substring(2, 2));
				cIRTime.second = (byte)int.Parse(text.Substring(4, 2));
				DateTime result = new DateTime(1, 1, 1, (int)cIRTime.hour, (int)cIRTime.minute, (int)cIRTime.second);
				return result;
			}
		}

		public string DispatchCmdName
		{
			get
			{
				string @string = Encoding.Default.GetString(buffer);
				string[] array = @string.Split(new char[]
				{
					'\r',
					'\n'
				});
				return array[0];
			}
		}

		public string SendDispatchCmdName
		{
			get
			{
				string @string = Encoding.Default.GetString(buffer);
				string[] array = @string.Split(new char[]
				{
					'\r',
					'\n'
				});
				return array[2];
			}
		}

		public string DispatchCmdContext
		{
			get
			{
				string @string = Encoding.Default.GetString(buffer);
				string[] array = @string.Split(new char[]
				{
					'\r',
					'\n'
				});
				return array[4];
			}
		}
	}
}
