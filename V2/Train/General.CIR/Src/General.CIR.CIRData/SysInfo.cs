using System;
using System.Runtime.InteropServices;
using System.Text;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct SysInfo
	{
		public byte netStatus;

		public byte state;

		public byte gsmR;

		public byte gprS;

		public byte singalMode;

		public byte supplyOrder;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
		public byte[] hostIP;

		public byte defData;

		public byte lkjStatus;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
		public byte[] kmMark;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
		public byte[] speed;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
		public byte[] trainNum;

		public byte finishChar1;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
		public byte[] trainID;

		public byte finishChar2;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32, ArraySubType = UnmanagedType.I1)]
		public byte[] lineName;

		public byte finishChar3;

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
				finishChar1 = 59;
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
				finishChar2 = 59;
			}
		}

		public string LineName
		{
			get
			{
				return Encoding.Default.GetString(lineName);
			}
			set
			{
				string text = value;
				bool flag = text.Length > 32;
				if (flag)
				{
					text = text.Remove(32);
				}
				else
				{
					bool flag2 = text.Length < 32;
					if (flag2)
					{
						text = text.PadRight(32, ' ');
					}
				}
				lineName = Encoding.Default.GetBytes(text);
				finishChar3 = 59;
			}
		}

		public string HostIPAddr
		{
			get
			{
				return string.Format("{0:D}.{0:D}.{0:D}.{0:D}", new object[]
				{
					hostIP[0],
					hostIP[1],
					hostIP[2],
					hostIP[3]
				});
			}
		}

		public double KMMark
		{
			get
			{
				return (double)(BitConverter.ToInt16(kmMark, 0) / 1000);
			}
		}

		public double Speed
		{
			get
			{
				return (double)BitConverter.ToInt16(speed, 0);
			}
		}

		public bool IsGetIPAddr
		{
			get
			{
				return (state & 32) == 32;
			}
		}

		public bool IsRegistTrainIDSucceed
		{
			get
			{
				return (state & 16) == 16;
			}
		}

		public bool IsRegistTrainNumSucceed
		{
			get
			{
				return (state & 8) == 8;
			}
		}

		public bool IsHostOrder
		{
			get
			{
				return (state & 4) == 4;
			}
		}

		public bool IsHandWorkMode
		{
			get
			{
				return (state & 2) == 2;
			}
		}

		public bool IsReduceLevel
		{
			get
			{
				return (state & 1) == 1;
			}
		}

		public bool IsShuntTrain
		{
			get
			{
				return (state & 4) == 4;
			}
		}
	}
}
