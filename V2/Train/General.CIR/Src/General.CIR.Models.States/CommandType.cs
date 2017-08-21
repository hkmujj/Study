using System.ComponentModel;

namespace General.CIR.Models.States
{
	public enum CommandType
	{
		[Description("调度命令")]
		DispatchCommand = 1,
		[Description("路票")]
		RoadTicket,
		[Description("绿色许可证")]
		GreenPermit,
		[Description("红色许可证")]
		RedPermit,
		[Description("出站跟踪调车通知书")]
		OutStation,
		[Description("列车进路预告信息")]
		InRoad = 7,
		[Description("调车作业通知单")]
		DispatchCarNotify = 17,
		[Description("调车请求确认")]
		DispatchCarConfirm,
		[Description("其他信息")]
		OtherInfo = 24,
		[Description("出入库检测")]
		InOutLibrary = 32
	}
}
