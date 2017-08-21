using System;
using System.Runtime.InteropServices;
using System.Text;

namespace General.CIR.CIRData
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GpsInfo
	{
		public byte protocolFlag;

		public byte funCode;

		public byte infoStatus;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
		public byte[] lineName;

		public ushort lineCode;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 21, ArraySubType = UnmanagedType.I1)]
		public byte[] sectionName;

		public byte singalMode;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.I1)]
		public byte[] longitude;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
		public byte[] latitude;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
		public byte[] dateTime;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
		public byte[] dispatchPhoneNumber;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I1)]
		public StationInfo[] stationInfo;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
		public byte[] PreLeave;

		public bool IsPositionInfoAvailable
		{
			get
			{
				return infoStatus == 65;
			}
		}

		public string SectionName
		{
			get
			{
				return Encoding.Default.GetString(sectionName);
			}
			set
			{
				string text = value;
				bool flag = text.Length > 21;
				if (flag)
				{
					text = text.Remove(21);
				}
				else
				{
					bool flag2 = text.Length < 21;
					if (flag2)
					{
						text = text.PadRight(21, ' ');
					}
				}
				sectionName = Encoding.Default.GetBytes(text);
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
						text = text.PadRight(8, ' ');
					}
				}
				lineName = Encoding.Default.GetBytes(text);
			}
		}

		public DateTime DateTime
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(6);
				StringBuilder stringBuilder2 = new StringBuilder(Encoding.Default.GetString(dateTime));
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

		public string DispatchPhoneNumber
		{
			get
			{
				return Encoding.Default.GetString(dispatchPhoneNumber);
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
						text = text.PadRight(8, ' ');
					}
				}
				dispatchPhoneNumber = Encoding.Default.GetBytes(text);
			}
		}
	}
}
