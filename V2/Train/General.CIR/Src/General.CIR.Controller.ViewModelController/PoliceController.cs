using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using CommonUtil.Util;
using General.CIR.CIRData;
using General.CIR.Events;
using General.CIR.Resource;
using General.CIR.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace General.CIR.Controller.ViewModelController
{
    [Export]
    public class PoliceController : ControllerBase<PoliceViewModel>
    {
        //[CompilerGenerated]
        //[Serializable]
        //private sealed class <>c
        //{
        //	public static readonly PoliceController.<>c <>9 = new PoliceController.<>c();

        //	public static Action<object> <>9__2_0;

        //	internal void <NetWorkDataEventResponse>b__2_0(object s)
        //	{
        //		Thread.Sleep(5000);
        //		ServiceLocator.Current.GetInstance<CIRController>().NavigatorToKey(BtnItemKeys.报警默认);
        //		ServiceLocator.Current.GetInstance<CIRController>().NavigatorToKey(BtnItemKeys.主界面GSMR);
        //	}
        //}

        public PoliceController()
        {
            EventAggregator.GetEvent<NetWorkDataEvent>().Subscribe(NetWorkDataEventResponse);
            DispatcherTimer dispatcherTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 5)
            };
            dispatcherTimer.Tick += delegate (object sener, EventArgs args)
            {
                this.Request800M();
            };
            dispatcherTimer.Start();
        }

        private void Request800M()
        {
            CIRPacket cIRPacket = default(CIRPacket);
            cIRPacket.Init();
            cIRPacket.SetHeadInfo(2, 19, 11, 3);
            CIRCommAgent.SendCIRData(CIRCommAgent.StructToBytes(cIRPacket), cIRPacket.GetDataLen(), false, 0);
        }

        private void NetWorkDataEventResponse(NetWorkEventArgs obj)
        {
            bool flag = obj.BusinessType == BusinessType.Model800M;
            if (flag)
            {
                bool flag2 = obj.Data.Cbcmd == 4;
                if (flag2)
                {
                    Debug.WriteLine("800M状态信息命令：");
                    LbjStatusInfo lbjStatusInfo = (LbjStatusInfo)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(LbjStatusInfo), 0);
                    Debug.WriteLine("报警状态：{0:X}，工作状态：{1:X}，列尾状态：{2:X}，机车号：{3}，列尾ID：{4}，列尾风压：{5}", new object[]
                    {
                        lbjStatusInfo.alarmStatus,
                        lbjStatusInfo.workStatus,
                        lbjStatusInfo.tailStatus,
                        lbjStatusInfo.tailInfo.TrainID,
                        lbjStatusInfo.tailInfo.TailID,
                        lbjStatusInfo.TailAirPressure
                    });
                    CIRViewModel instance = ServiceLocator.Current.GetInstance<CIRViewModel>();
                    instance.MainContentViewModel.ColumnEndViewModel.IsConnect = (lbjStatusInfo.tailStatus == 1);
                    instance.MainContentViewModel.PoliceViewModel.IsEmergency = true;
                    instance.MainContentViewModel.ColumnEndViewModel.ID = lbjStatusInfo.tailInfo.TailID;
                    instance.MainContentViewModel.ColumnEndViewModel.FanPress = lbjStatusInfo.TailAirPressure.ToString();
                    instance.MainContentViewModel.PoliceViewModel.CanPolice = true;
                    Debug.WriteLine("");
                }
                else
                {
                    bool flag3 = obj.Data.Cbcmd == 1;
                    if (flag3)
                    {
                        StatusInfo800M statusInfo800M = (StatusInfo800M)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(StatusInfo800M), 0);
                        CIRViewModel instance2 = ServiceLocator.Current.GetInstance<CIRViewModel>();
                        instance2.MainContentViewModel.PoliceViewModel.IsEmergency = false;
                        AppLog.Info("信息类型：{0}", new object[]
                        {
                            statusInfo800M.infoType
                        });
                        bool flag4 = statusInfo800M.infoType == 1;
                        if (flag4)
                        {
                            AppLog.Info("列车防护报警回复命令：");
                            TrainProtectionAlarmRequest trainProtectionAlarmRequest = (TrainProtectionAlarmRequest)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(TrainProtectionAlarmRequest), 1);
                            ServiceLocator.Current.GetInstance<CIRController>().NavigatorToKey(BtnItemKeys.报警紧急确认);
                            AppLog.Info(string.Format("机车号：{0}", trainProtectionAlarmRequest.TrainID));
                            AppLog.Info(string.Format("车次号：{0}", trainProtectionAlarmRequest.TrainNum));
                            AppLog.Info("信息发送日期时间：{0}", new object[]
                            {
                                trainProtectionAlarmRequest.InfoSendDateTime
                            });
                            AppLog.Info("公里标：{0}", new object[]
                            {
                                trainProtectionAlarmRequest.KMMark
                            });
                            AppLog.Info("启动报警原因：{0}", new object[]
                            {
                                trainProtectionAlarmRequest.StartStopReason
                            });
                        }
                        else
                        {
                            bool flag5 = statusInfo800M.infoType == 2;
                            if (flag5)
                            {
                                AppLog.Info("列车防护报警解除回复命令：");
                                TrainProtectionAlarmRequest trainProtectionAlarmRequest2 = (TrainProtectionAlarmRequest)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(TrainProtectionAlarmRequest), 1);
                                ServiceLocator.Current.GetInstance<CIRController>().NavigatorToKey(BtnItemKeys.报警报警解除);
                                Task.Factory.StartNew(() =>
                                {
                                    Thread.Sleep(5000);
                                    ServiceLocator.Current.GetInstance<CIRController>().NavigatorToKey(BtnItemKeys.报警默认);
                                    ServiceLocator.Current.GetInstance<CIRController>().NavigatorToKey(BtnItemKeys.主界面GSMR);
                                });
                                AppLog.Info(string.Format("机车号：{0}", trainProtectionAlarmRequest2.TrainID));
                                AppLog.Info(string.Format("车次号：{0}", trainProtectionAlarmRequest2.TrainNum));
                                AppLog.Info("信息发送日期时间：{0}", new object[]
                                {
                                    trainProtectionAlarmRequest2.InfoSendDateTime
                                });
                                AppLog.Info("公里标：{0}", new object[]
                                {
                                    trainProtectionAlarmRequest2.KMMark
                                });
                                AppLog.Info("解除报警原因：{0}", new object[]
                                {
                                    trainProtectionAlarmRequest2.StartStopReason
                                });
                            }
                            else
                            {
                                bool flag6 = statusInfo800M.infoType == 3;
                                if (flag6)
                                {
                                    AppLog.Info("道口事故报警回复命令：");
                                    CrossAccientAlarmRequest crossAccientAlarmRequest = (CrossAccientAlarmRequest)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(CrossAccientAlarmRequest), 1);
                                    AppLog.Info("信息发送日期时间：{0}", new object[]
                                    {
                                        crossAccientAlarmRequest.InfoSendDateTime
                                    });
                                    AppLog.Info("公里标：{0}", new object[]
                                    {
                                        crossAccientAlarmRequest.KMMark
                                    });
                                }
                                else
                                {
                                    bool flag7 = statusInfo800M.infoType == 4;
                                    if (flag7)
                                    {
                                        AppLog.Info("道口事故报警解除回复命令：");
                                        CrossAccientAlarmRequest crossAccientAlarmRequest2 = (CrossAccientAlarmRequest)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(CrossAccientAlarmRequest), 1);
                                        AppLog.Info("信息发送日期时间：{0}", new object[]
                                        {
                                            crossAccientAlarmRequest2.InfoSendDateTime
                                        });
                                        AppLog.Info("公里标：{0}", new object[]
                                        {
                                            crossAccientAlarmRequest2.KMMark
                                        });
                                    }
                                    else
                                    {
                                        bool flag8 = statusInfo800M.infoType == 5;
                                        if (flag8)
                                        {
                                            AppLog.Info("施工防护报警回复命令：");
                                            ConstructProtectionAlarmRequest constructProtectionAlarmRequest = (ConstructProtectionAlarmRequest)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(ConstructProtectionAlarmRequest), 1);
                                            AppLog.Info("信息发送日期时间：{0}", new object[]
                                            {
                                                constructProtectionAlarmRequest.InfoSendDateTime
                                            });
                                            AppLog.Info("公里标：{0}", new object[]
                                            {
                                                constructProtectionAlarmRequest.KMMark
                                            });
                                        }
                                        else
                                        {
                                            bool flag9 = statusInfo800M.infoType == 6;
                                            if (flag9)
                                            {
                                                AppLog.Info("施工防护报警解除回复命令：");
                                                ConstructProtectionAlarmRequest constructProtectionAlarmRequest2 = (ConstructProtectionAlarmRequest)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(ConstructProtectionAlarmRequest), 1);
                                                AppLog.Info("信息发送日期时间：{0}", new object[]
                                                {
                                                    constructProtectionAlarmRequest2.InfoSendDateTime
                                                });
                                                AppLog.Info("公里标：{0}", new object[]
                                                {
                                                    constructProtectionAlarmRequest2.KMMark
                                                });
                                            }
                                            else
                                            {
                                                bool flag10 = statusInfo800M.infoType == 7;
                                                if (flag10)
                                                {
                                                    AppLog.Info("出入库检测回复命令：");
                                                }
                                                else
                                                {
                                                    bool flag11 = statusInfo800M.infoType == 8;
                                                    if (flag11)
                                                    {
                                                        AppLog.Info("报警试验回复命令：");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
