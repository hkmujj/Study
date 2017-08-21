using System;
using System.Runtime.InteropServices;
using System.Text;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct DispatchInfoSignConfirm
	{
		public byte InfoName;

		public byte FunCode;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
		public byte[] date;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
		public byte[] time;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7, ArraySubType = UnmanagedType.I1)]
		public byte[] trainNum;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
		public byte[] trainID;

		public byte cmdForm;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
		public byte[] cmdNum;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
		public byte[] kmMark;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
		public byte[] SignLongitude;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
		public byte[] SignLatitude;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
		public byte[] PreLeave;

		public byte packNum;

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

		public double KMMark
		{
			get
			{
				return (double)(BitConverter.ToInt16(kmMark, 0) / 1000);
			}
		}

		public void Init()
		{
			bool flag = date == null;
			if (flag)
			{
				date = new byte[3];
			}
			bool flag2 = time == null;
			if (flag2)
			{
				time = new byte[3];
			}
			bool flag3 = cmdNum == null;
			if (flag3)
			{
				cmdNum = new byte[6];
			}
			bool flag4 = kmMark == null;
			if (flag4)
			{
				kmMark = new byte[3];
			}
			bool flag5 = SignLongitude == null;
			if (flag5)
			{
				SignLongitude = new byte[5];
			}
			bool flag6 = SignLatitude == null;
			if (flag6)
			{
				SignLatitude = new byte[4];
			}
			bool flag7 = PreLeave == null;
			if (flag7)
			{
				PreLeave = new byte[5];
			}
		}
	}
}
