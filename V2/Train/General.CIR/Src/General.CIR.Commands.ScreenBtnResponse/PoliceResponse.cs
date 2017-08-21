using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonUtil.Util;
using General.CIR.CIRData;
using General.CIR.Controller;
using General.CIR.Models.Units;
using General.CIR.Resource;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class PoliceResponse : BtnResponseBase
	{
		//[CompilerGenerated]
		//[Serializable]
		//private sealed class <>c
		//{
		//	public static readonly PoliceResponse.<>c <>9 = new PoliceResponse.<>c();

		//	public static Action<object> <>9__3_0;

		//	public static Func<Type, bool> <>9__5_0;

		//	internal void <ClickUp>b__3_0(object s)
		//	{
		//		Thread.Sleep(1000);
		//		ViewModel.MainContentViewModel.PoliceViewModel.Trips = InFoResource.正在发送报警信息;
		//		Thread.Sleep(3000);
		//		CIRPacket cIRPacket = default(CIRPacket);
		//		cIRPacket.Init();
		//		cIRPacket.SetHeadInfo(2, 19, 11, 2);
		//		StartStopLbjAlarmRequest startStopLbjAlarmRequest = new StartStopLbjAlarmRequest
		//		{
		//			StartStop = 1
		//		};
		//		cIRPacket.SetData(CIRCommAgent.StructToBytes(startStopLbjAlarmRequest));
		//		AppLog.Info("请求启动LBJ报警Mpack.GetPackLen()：{0}", new object[]
		//		{
		//			cIRPacket.GetPackLen()
		//		});
		//		CIRCommAgent.SendCIRData(CIRCommAgent.StructToBytes(cIRPacket), cIRPacket.GetDataLen(), false, 0);
		//		ViewModel.MainContentViewModel.PoliceViewModel.Trips = InFoResource.启动报警命令已发送;
		//	}

		//	internal bool <ClickDown>b__5_0(Type w)
		//	{
		//		return w.GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault<object>() is ViewExportAttribute;
		//	}
		//}

		public static DateTime LastOperationTime = default(DateTime);

		private bool m_IsUp;

		private DateTime m_DownTime;

		private string m_FullName;

		public override void ClickUp()
		{
			bool canPolice = ViewModel.MainContentViewModel.PoliceViewModel.CanPolice;
			if (canPolice)
			{
				m_IsUp = true;
				TimeSpan timeSpan = DateTime.Now - m_DownTime;
				CIRController controller = ViewModel.Controller;
				controller.NavigatorToKey(BtnItemKeys.报警报警确认);
				bool flag = timeSpan.TotalSeconds > 5.0;
				if (flag)
				{
					controller.NavigatorToKey(BtnItemKeys.报警发送报警命令);
				    Task.Factory.StartNew(() =>
				    {
                        Thread.Sleep(1000);
                        ViewModel.MainContentViewModel.PoliceViewModel.Trips = InFoResource.正在发送报警信息;
                        Thread.Sleep(3000);
                        CIRPacket cIRPacket = default(CIRPacket);
                        cIRPacket.Init();
                        cIRPacket.SetHeadInfo(2, 19, 11, 2);
                        StartStopLbjAlarmRequest startStopLbjAlarmRequest = new StartStopLbjAlarmRequest
                        {
                            StartStop = 1
                        };
                        cIRPacket.SetData(CIRCommAgent.StructToBytes(startStopLbjAlarmRequest));
                        AppLog.Info("请求启动LBJ报警Mpack.GetPackLen()：{0}", new object[]
                        {
                                    cIRPacket.GetPackLen()
                        });
                        CIRCommAgent.SendCIRData(CIRCommAgent.StructToBytes(cIRPacket), cIRPacket.GetDataLen(), false, 0);
                        ViewModel.MainContentViewModel.PoliceViewModel.Trips = InFoResource.启动报警命令已发送;
                    });
				}
			}
		}

		public override void ClickDown()
		{
			bool canPolice = ViewModel.MainContentViewModel.PoliceViewModel.CanPolice;
			if (canPolice)
			{
				m_IsUp = false;
				m_DownTime = DateTime.Now;
				CIRController con = ViewModel.Controller;
				string view = BtnItemsFactory.Instance.GetOrCreateBtn(BtnItemKeys.报警报警确认).Views;
				bool flag = string.IsNullOrEmpty(m_FullName);
				if (flag)
				{
				
					m_FullName = GetType().Assembly.GetExportedTypes().Where(w => w.GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault<object>() is ViewExportAttribute).FirstOrDefault((Type w) => w.Name.Equals(view))?.FullName;
				}
				con.NavigatorTo(m_FullName);
				Task.Factory.StartNew(delegate(object s)
				{
					Thread.Sleep(5000);
					bool flag2 = !ViewModel.MainContentViewModel.PoliceViewModel.IsEmergency && this.m_IsUp;
					if (flag2)
					{
						con.NavigatorToKey(BtnItemKeys.报警默认);
						con.NavigatorToKey(BtnItemKeys.主界面GSMR);
					}
				}, null);
			}
		}
	}
}
