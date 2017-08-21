using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Tram.CBTC.Casco.ViewModel;
using Tram.CBTC.Infrasturcture.Events;
using Tram.CBTC.Infrasturcture.Model.Constant;
using Tram.CBTC.Infrasturcture.Model.Send;

namespace Tram.CBTC.Casco.Model.TempModel
{
    [Export]
    public class PopModel : NotificationObject
    {
        [Import]
        public DriverIDInput DriverIDInput { get; private set; }
        [Import]
        public VehicleOperationSelectionModel VehicleOperationSelectionModel { get; private set; }
        [Import]
        public EndStationModel EndStationModel { get; private set; }
        [Import]
        public SelectPlanTrainAndOneWay SelectPlanTrainAndOneWay { get; private set; }
    }
    [Export]
    public class DriverIDInput : NotificationObject
    {
        private string m_DriverID;

        public DriverIDInput()
        {
            DriverID = string.Empty;
            Login = new DelegateCommand(() =>
                {
                    DomainViewModel.Value.CBTC.SendInterface.InputDriverId(new SendModel<string>(DriverID));
                    DomainViewModel.Value.Controller.ClosePop.Execute(null);
                });
            Cancel = new DelegateCommand(() =>
            {
                DomainViewModel.Value.Controller.ClosePop.Execute(null);
            });
            InputChar = new DelegateCommand<string>(UpdateDriverID);
        }

        private void UpdateDriverID(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return;
            }

            switch (obj)
            {
                case "0":
                    Plus(obj);
                    break;
                case "1":
                    Plus(obj);
                    break;
                case "2":
                    Plus(obj);
                    break;
                case "3":
                    Plus(obj);
                    break;
                case "4":
                    Plus(obj);
                    break;
                case "5":
                    Plus(obj);
                    break;
                case "6":
                    Plus(obj);
                    break;
                case "7":
                    Plus(obj);
                    break;
                case "8":
                    Plus(obj);
                    break;
                case "9":
                    Plus(obj);
                    break;
                case ".":
                    break;
                case "C":
                    DriverID = string.Empty;
                    break;
                case "←":
                    Subtract();
                    break;
                case "确定":
                    Login.Execute(null);
                    break;
            }
        }

        private void Plus(string str)
        {
            if (str == "0" && string.IsNullOrEmpty(DriverID))
            {
                return;
            }
            DriverID += str;
        }

        private void Subtract()
        {
            if (DriverID.Length > 1)
            {
                DriverID = DriverID.Substring(0, DriverID.Length - 1);
            }
            else
            {
                DriverID = string.Empty;
            }
        }
        public string DriverID
        {
            get { return m_DriverID; }
            set
            {
                if (value == m_DriverID)
                    return;

                m_DriverID = value;
                RaisePropertyChanged(() => DriverID);
            }
        }
        [Browsable(false)]
        [Import]
        public Lazy<DomainViewModel> DomainViewModel { get; private set; }
        public ICommand Login { get; private set; }
        public ICommand Cancel { get; private set; }
        public ICommand InputChar { get; private set; }
    }

    [Export]
    public class VehicleOperationSelectionModel : NotificationObject
    {
        private bool m_IsOnBoardOnline;
        private bool m_IsVehicleIndependent;
        private bool m_IsManualControl;

        public VehicleOperationSelectionModel()
        {
            IsOnBoardOnline = true;
            Confirm = new DelegateCommand(() =>
            {
                DomainViewModel.Value.CBTC.SendInterface.SelectVehicleRunningModel(
                    new SendModel<VehicleRunningModel>(m_RunningModel));
                Cancel.Execute(null);
            });
            Cancel = new DelegateCommand(() => { DomainViewModel.Value.Controller.ClosePop.Execute(null); });
        }

        private VehicleRunningModel m_RunningModel;
        private void Changed()
        {
            if (IsVehicleIndependent)
            {
                m_RunningModel = VehicleRunningModel.VehicleIndependent;
            }
            if (IsManualControl)
            {
                m_RunningModel = VehicleRunningModel.ManualControl;
            }
            if (IsOnBoardOnline)
            {
                m_RunningModel = VehicleRunningModel.OnBoardOnline;
            }
        }
        /// <summary>
        /// 手工控制
        /// </summary>
        public bool IsManualControl
        {
            get { return m_IsManualControl; }
            set
            {
                if (value == m_IsManualControl)
                    return;

                m_IsManualControl = value;
                Changed();
                RaisePropertyChanged(() => IsManualControl);
            }
        }

        /// <summary>
        /// 车载独立
        /// </summary>
        public bool IsVehicleIndependent
        {
            get { return m_IsVehicleIndependent; }
            set
            {
                if (value == m_IsVehicleIndependent)
                    return;

                m_IsVehicleIndependent = value;
                Changed();
                RaisePropertyChanged(() => IsVehicleIndependent);
            }
        }

        /// <summary>
        /// 车载联机
        /// </summary>
        public bool IsOnBoardOnline
        {
            get { return m_IsOnBoardOnline; }
            set
            {
                if (value == m_IsOnBoardOnline)
                    return;

                m_IsOnBoardOnline = value;
                Changed();
                RaisePropertyChanged(() => IsOnBoardOnline);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        public ICommand Confirm { get; set; }
        /// <summary>
        /// 取消
        /// </summary>
        public ICommand Cancel { get; set; }
        [Import]
        public Lazy<DomainViewModel> DomainViewModel { get; private set; }
    }

    [Export]
    public class EndStationModel : NotificationObject
    {
        private ComboxSelectItemModel m_SelectItem;
        private List<ComboxSelectItemModel> m_ComboxSelectItemModels;

        public EndStationModel()
        {
            ComboxSelectItemModels = new List<ComboxSelectItemModel>();
            var model1 = new ComboxSelectItemModel();
            model1.Content = "终点站";
            model1.Items = new List<ComboxSelectItemModel>();
            model1.Items.Add(new ComboxSelectItemModel() { Content = "郫县西站" });
            model1.Items.Add(new ComboxSelectItemModel() { Content = "郫县东站" });
            ComboxSelectItemModels.Add(model1);
            var model2 = new ComboxSelectItemModel();
            model2.Content = "路径号";
            model2.Items = new List<ComboxSelectItemModel>();
            model2.Items.Add(new ComboxSelectItemModel() { Content = "1" });
            model2.Items.Add(new ComboxSelectItemModel() { Content = "2" });
            ComboxSelectItemModels.Add(model2);
            SelectItem = ComboxSelectItemModels.FirstOrDefault();
            Confirm = new DelegateCommand(() =>
            {
                if (SelectItem == ComboxSelectItemModels[0])
                {
                    DoMainViewModel.Value.CBTC.SendInterface.SendEndStation(new SendModel<string>(SelectItem.Select.Content));
                }
                else
                {
                    DoMainViewModel.Value.CBTC.SendInterface.SendLineID(new SendModel<string>(SelectItem.Select.Content));
                }
                DoMainViewModel.Value.Controller.ClosePop.Execute(null);
            });
            Cancel = new DelegateCommand(() => { DoMainViewModel.Value.Controller.ClosePop.Execute(null); });
        }
        [Import]
        public Lazy<DomainViewModel> DoMainViewModel { get; private set; }
        public ICommand Confirm { get; private set; }
        public ICommand Cancel { get; private set; }
        public ComboxSelectItemModel SelectItem
        {
            get { return m_SelectItem; }
            set
            {
                if (Equals(value, m_SelectItem))
                    return;

                m_SelectItem = value;
                if (m_SelectItem.Select == null)
                {
                    m_SelectItem.Select = m_SelectItem.Items.FirstOrDefault();
                }
                RaisePropertyChanged(() => SelectItem);
            }
        }

        public List<ComboxSelectItemModel> ComboxSelectItemModels
        {
            get { return m_ComboxSelectItemModels; }
            set
            {
                if (Equals(value, m_ComboxSelectItemModels))
                    return;

                m_ComboxSelectItemModels = value;
                RaisePropertyChanged(() => ComboxSelectItemModels);
            }
        }
    }
    [Export]
    public class SelectPlanTrainAndOneWay : NotificationObject
    {
        private ComboxSelectItemModel2 m_SelectItem;
        private List<ComboxSelectItemModel2> m_SelectItemModels;

        public SelectPlanTrainAndOneWay()
        {
      
            Confirm = new DelegateCommand(() =>
            {
                DoMainViewModel.Value.CBTC.SendInterface.SendPlanInfo(new SendModel<PlanInfo>(new PlanInfo()
                {
                    Plan = SelectItem.Content,
                    Train = SelectItem.Select1.Content,
                    OneWay = SelectItem.Select2.Content,
                }));
            });
            Cancel = new DelegateCommand(() => { DoMainViewModel.Value.Controller.ClosePop.Execute(null); });
        }

        [Import]
        public Lazy<DomainViewModel> DoMainViewModel { get; private set; }
        public ICommand Confirm { get; private set; }
        public ICommand Cancel { get; private set; }
        public ComboxSelectItemModel2 SelectItem
        {
            get { return m_SelectItem; }
            set
            {
                if (Equals(value, m_SelectItem))
                    return;

                m_SelectItem = value;
                if (m_SelectItem.Select1 == null)
                {
                    m_SelectItem.Select1 = m_SelectItem.Items1.FirstOrDefault();
                }
                if (m_SelectItem.Select2 == null)
                {
                    m_SelectItem.Select2 = m_SelectItem.Items2.FirstOrDefault();
                }
                RaisePropertyChanged(() => SelectItem);
            }
        }

        public List<ComboxSelectItemModel2> SelectItemModels
        {
            get { return m_SelectItemModels; }
            set
            {
                if (Equals(value, m_SelectItemModels))
                    return;

                m_SelectItemModels = value;
                RaisePropertyChanged(() => SelectItemModels);
            }
        }
    }
    public class ComboxSelectItemModel : NotificationObject
    {
        private List<ComboxSelectItemModel> m_Items;
        private ComboxSelectItemModel m_Select;
        private string m_Content;
        private int m_ID;

        public int ID
        {
            get { return m_ID; }
            set
            {
                if (value == m_ID)
                    return;

                m_ID = value;
                RaisePropertyChanged(() => ID);
            }
        }

        public string Content
        {
            get { return m_Content; }
            set
            {
                if (value == m_Content)
                    return;

                m_Content = value;
                RaisePropertyChanged(() => Content);
            }
        }

        public ComboxSelectItemModel Select
        {
            get { return m_Select; }
            set
            {
                if (Equals(value, m_Select))
                    return;

                m_Select = value;
                RaisePropertyChanged(() => Select);
            }
        }

        public List<ComboxSelectItemModel> Items
        {
            get { return m_Items; }
            set
            {
                if (Equals(value, m_Items))
                    return;

                m_Items = value;
                RaisePropertyChanged(() => Items);
            }
        }
    }

    public class ComboxSelectItemModel2 : NotificationObject
    {
        private List<ComboxSelectItemModel2> m_Items2;
        private List<ComboxSelectItemModel2> m_Items1;
        private ComboxSelectItemModel2 m_Select2;
        private ComboxSelectItemModel2 m_Select1;
        private string m_Content;
        private int m_ID;

        public int ID
        {
            get { return m_ID; }
            set
            {
                if (value == m_ID)
                    return;

                m_ID = value;
                RaisePropertyChanged(() => ID);
            }
        }

        public string Content
        {
            get { return m_Content; }
            set
            {
                if (value == m_Content)
                    return;

                m_Content = value;
                RaisePropertyChanged(() => Content);
            }
        }

        public ComboxSelectItemModel2 Select1
        {
            get { return m_Select1; }
            set
            {
                if (Equals(value, m_Select1))
                    return;

                m_Select1 = value;
                RaisePropertyChanged(() => Select1);
            }
        }

        public ComboxSelectItemModel2 Select2
        {
            get { return m_Select2; }
            set
            {
                if (Equals(value, m_Select2))
                    return;

                m_Select2 = value;
                RaisePropertyChanged(() => Select2);
            }
        }

        public List<ComboxSelectItemModel2> Items1
        {
            get { return m_Items1; }
            set
            {
                if (Equals(value, m_Items1))
                    return;

                m_Items1 = value;
                RaisePropertyChanged(() => Items1);
            }
        }

        public List<ComboxSelectItemModel2> Items2
        {
            get { return m_Items2; }
            set
            {
                if (Equals(value, m_Items2))
                    return;

                m_Items2 = value;
                RaisePropertyChanged(() => Items2);
            }
        }
    }
}
