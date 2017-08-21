using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using General.CIR.CIRData;
using General.CIR.Contance;
using General.CIR.Events;
using General.CIR.Extentions;
using General.CIR.Models;
using General.CIR.Models.Units;
using General.CIR.Resource;
using General.CIR.ViewModels;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;

namespace General.CIR.Controller
{
    [Export]
    public class CIRController : MMI.Facility.WPFInfrastructure.Interfaces.ControllerBase<CIRViewModel>
    {
        //[CompilerGenerated]
        //[Serializable]
        //private sealed class <>c
        //{
        //	public static readonly CIRController.<>c <>9 = new CIRController.<>c();

        //	public static Action<Lazy<ICIRStatusChanged>> <>9__9_1;

        //	public static Action<Lazy<ICIRStatusChanged>> <>9__9_3;

        //	internal void <UpdateState>b__9_1(Lazy<ICIRStatusChanged> f)
        //	{
        //		f.Value.Initaliation();
        //	}

        //	internal void <UpdateState>b__9_3(Lazy<ICIRStatusChanged> f)
        //	{
        //		f.Value.Clear();
        //	}
        //}

        protected IRegionManager RegionManager { get; }

        protected IEventAggregator EventAggregator { get; }

        public ICommand NavigatorToKeyCommand
        {
            get;
            private set;
        }

        [ImportingConstructor]
        public CIRController(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            RegionManager = regionManager;
            EventAggregator = eventAggregator;
            EventAggregator.GetEvent<NavigatorEvent>().Subscribe(NavigatorTo, ThreadOption.UIThread);
            EventAggregator.GetEvent<NetWorkDataEvent>().Subscribe(NetWorkAction, ThreadOption.UIThread);
            NavigatorToKeyCommand = new DelegateCommand<string>(NavigatorToKey);
        }

        private void NetWorkAction(NetWorkEventArgs obj)
        {
            bool flag = obj.BusinessType == BusinessType.Location;
            if (flag)
            {
                bool flag2 = obj.Data.Cbcmd == 255;
                if (flag2)
                {
                    Debug.WriteLine("GIS命令：");
                }
            }
            else
            {
                bool flag3 = obj.BusinessType == BusinessType.MMI;
                if (flag3)
                {
                    bool flag4 = obj.Data.Cbcmd == 70;
                    if (flag4)
                    {
                        byte b = obj.Data.Buff[0];
                        ViewModel.IsStart = (b == 1);
                        Debug.WriteLine(string.Format("CIR电源控制命令：{0}", b));
                    }
                    else
                    {
                        bool flag5 = obj.Data.Cbcmd == 89;
                        if (flag5)
                        {
                            Debug.WriteLine("机车号注册注销命令！");
                            TrainIDRegistResponse trainIDRegistResponse = (TrainIDRegistResponse)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(TrainIDRegistResponse), 0);
                            Debug.WriteLine(string.Format("机车号：{0}", trainIDRegistResponse.TrainID));
                            bool isRegist = trainIDRegistResponse.IsRegist;
                            if (isRegist)
                            {
                                Debug.WriteLine(trainIDRegistResponse.IsSucceed ? "注册成功！" : "注册失败！");
                            }
                            else
                            {
                                Debug.WriteLine(trainIDRegistResponse.IsSucceed ? "注销成功！" : "注销失败！");
                            }
                        }
                        else
                        {
                            bool flag6 = obj.Data.Cbcmd == 88;
                            if (flag6)
                            {
                                Debug.WriteLine("车次号注册注销命令！");
                                TrainNumRegistResponse trainNumRegistResponse = (TrainNumRegistResponse)CIRCommAgent.BytesToStruct(obj.Data.Buff, typeof(TrainNumRegistResponse), 0);
                                Debug.WriteLine(string.Format("车次号：{0}", trainNumRegistResponse.TrainNum));
                                bool isRegist2 = trainNumRegistResponse.IsRegist;
                                if (isRegist2)
                                {
                                    Debug.WriteLine(trainNumRegistResponse.IsSucceed ? "注册成功！" : "注册失败！");
                                }
                                else
                                {
                                    Debug.WriteLine(trainNumRegistResponse.IsSucceed ? "注销成功！" : "注销失败！");
                                }
                            }
                            else
                            {
                                bool flag7 = obj.Data.Cbcmd == 85;
                                if (flag7)
                                {
                                    Debug.WriteLine("综合信息回复命令");
                                }
                            }
                        }
                    }
                }
            }
        }

        public void NavigatorTo(string fullName)
        {
            bool flag = string.IsNullOrEmpty(fullName);
            if (!flag)
            {
                Type expr_14 = Type.GetType(fullName);
                ViewExportAttribute viewExportAttribute = ((expr_14 != null) ? expr_14.GetCustomAttributes(typeof(ViewExportAttribute), false).FirstOrDefault<object>() : null) as ViewExportAttribute;
                bool flag2 = viewExportAttribute != null;
                if (flag2)
                {
                    RegionManager.RequestNavigate(viewExportAttribute.RegionName, fullName);
                }
                bool flag3 = fullName == ViewNames.MainContent || fullName == ViewNames.SettingView;
                if (flag3)
                {
                    ViewModel.CurrentViewName = fullName;
                }
            }
        }

        public void UpdateState()
        {
            bool isStart = ViewModel.IsStart;
            if (isStart)
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(delegate
                {
                    ViewModel.AllViewModel.ForEach(f=>f.Value.Initaliation());
                }));
                NavigatorToKey(BtnItemKeys.启动);
                EventAggregator.GetEvent<NavigatorEvent>().Publish(ViewNames.StartingView);
                ThreadPool.QueueUserWorkItem(delegate (object state)
                {
                    Thread.Sleep(5000);
                    this.NavigatorToKey(BtnItemKeys.主界面GSMR);
                    this.NavigatorToKey(BtnItemKeys.主界面信息区域空白区域);
                    this.NavigatorToKey(BtnItemKeys.主界面GSMR);
                });
                ViewModel.MainContentViewModel.TitleViewModel.EngineNumber = GlobalParams.Instance.SystemConfig.EngineNumber.ToStrNumber();
                ViewModel.MainContentViewModel.TitleViewModel.TrainNumber = GlobalParams.Instance.SystemConfig.TrainNumber;
                ViewModel.MainContentViewModel.ColumnEndViewModel.ID = GlobalParams.Instance.SystemConfig.ColumnEndID;
                GlobalParams.Instance.RequstResgisterEngineNumber();
            }
            else
            {
                NavigatorToKey(BtnItemKeys.黑屏);
                ViewModel.AllViewModel.ForEach(f => f.Value.Clear());
            }
        }

        public void NavigatorToKey(string key)
        {
            ViewModel.UpdateCurrentView(BtnItemsFactory.Instance.GetOrCreateBtn(key));
        }
    }
}
