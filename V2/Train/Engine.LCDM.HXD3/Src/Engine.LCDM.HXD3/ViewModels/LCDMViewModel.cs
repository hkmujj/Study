
using System.Windows;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.LCDM.HXD3.Config;
using Engine.LCDM.HXD3.Interfaces;
using MMI.Facility.Interface.Data;
using Engine.LCDM.HXD3.Controller;
using Engine.LCDM.HXD3.Enums;
using Engine.LCDM.HXD3.Resource;
using Engine.LCDM.HXD3.Views.BottomButton;
using MMI.Facility.Interface.Service;
using System.Collections.Generic;
using System;
using Engine.LCDM.HXD3.Common;

namespace Engine.LCDM.HXD3.ViewModels
{
    [Export(typeof(LCDMViewModel))]
    public class LCDMViewModel : ViewModelBase, IDataListener
    {
        private Buttons m_Buttons;
        private Visibility m_IsShowMain;
        [ImportingConstructor]
        public LCDMViewModel(LCDMController controler, BrakeSetting brake, InfoSet info)
        {
            Controller = controler;
            controler.ViewModel = this;
            BrakeData = brake;
            brake.ViewModel = this;
            InfoData = info;
            info.ViewModel = this;
            GlobalParam.Instance.InitPara.DataPackage.ServiceManager.GetService<ICourseService>().CourseStateChanged += BrakeSet_CurseStateChanged;
        }

        private void BrakeSet_CurseStateChanged(object sender, CourseStateChangedArgs e)
        {
            switch (e.CourseService.CurrentCourseState)
            {
                case CourseState.Unknown:
                    break;
                case CourseState.Started:
                    AllViewModel.ForEach(f => f.Value.Start());
                    break;
                case CourseState.Stoped:
                    AllViewModel.ForEach(f => f.Value.Stop());
                    break;
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Buttons Buttons
        {
            get { return m_Buttons; }
            set
            {
                m_Buttons = value;
                Controller.ResponseButton.Execute(null);
            }
        }
        public Visibility IsShowMain
        {
            set
            {
                if (value == m_IsShowMain)
                {
                    return;
                }
                m_IsShowMain = value;
                RaisePropertyChanged(() => IsShowMain);
            }
            get { return m_IsShowMain; }
        }

        public LCDMController Controller { get; private set; }
        [ImportMany]
        public List<Lazy<ICourceStatusChanged>> AllViewModel { get; private set; }

        [Import]
        public MainViewModel Main { get; private set; }

        [Import]
        public InfoSet InfoData { get; private set; }

        public BrakeSetting BrakeData { get; private set; }

        public override void DataChanged(object sender, CommunicationDataChangedArgs<bool> args)
        {
            foreach (var tmpValue in typeof(LCDMViewModel).GetProperties().Select(info => info.GetValue(this, null) as IDataChanged))
            {
                if (tmpValue != null)
                {
                    tmpValue.DataChanged(sender, args);
                }
                //tmpValue?.DataChanged(sender, args);
            }
            var boolIndex1 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.EPCU失电];
            var boolIndex2 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.MIPM失电];
            var boolIndex3 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.亮屏标志];
            var index1 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.F1];
            var index2 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.F2];
            var index3 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.F3];
            var index4 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.F4];
            var index5 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.F5];
            var index6 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.F6];
            var index7 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.F7];
            var index8 = GlobalParam.Instance.IndexConfig.InBoolDescriptionDictionary[InBoolKeys.F8];
            if (args.NewValue.ContainsKey(index1) && args.NewValue[index1])
            {
                Buttons = Buttons.F1;
            }
            else if (args.NewValue.ContainsKey(index2) && args.NewValue[index2])
            {
                Buttons = Buttons.F2;
            }
            else if (args.NewValue.ContainsKey(index3) && args.NewValue[index3])
            {
                Buttons = Buttons.F3;
            }
            else if (args.NewValue.ContainsKey(index8) && args.NewValue[index8])
            {
                Buttons = Buttons.F8;
            }
            else if (args.NewValue.ContainsKey(index4) && args.NewValue[index4])
            {
                Buttons = Buttons.F4;
            }
            else if (args.NewValue.ContainsKey(index5) && args.NewValue[index5])
            {
                Buttons = Buttons.F5;
            }
            else if (args.NewValue.ContainsKey(index6) && args.NewValue[index6])
            {
                Buttons = Buttons.F6;
            }
            else if (args.NewValue.ContainsKey(index7) && args.NewValue[index7])
            {
                Buttons = Buttons.F7;
            }
            if (args.NewValue.ContainsKey(boolIndex1))
            {

                if (args.NewValue[boolIndex1])
                {
                    Main.EpcuVisibility = Visibility.Visible;
                    Main.EpcuNotMipmVisibility = Main.MipuVisibility == Visibility.Hidden
                        ? Visibility.Visible
                        : Visibility.Hidden;
                    Main.Flow = 0;
                    Controller.Navigator.Execute(typeof(MainBottomButton).FullName);
                }
                else
                {
                    Main.EpcuVisibility = Visibility.Hidden;
                    Main.EpcuNotMipmVisibility = Visibility.Hidden;
                }
                if (Main.EpcuVisibility == Visibility.Hidden && Main.MipuVisibility == Visibility.Hidden)
                    Main.NorEmVisibility = Visibility.Visible;
                else
                {
                    Main.NorEmVisibility = Visibility.Hidden;
                }

            }
            if (args.NewValue.ContainsKey(boolIndex2))
            {
                if (args.NewValue[boolIndex2])
                {
                    Main.MipuVisibility = Visibility.Visible;
                    Main.EpcuNotMipmVisibility = Visibility.Hidden;
                    Main.Flow = 0;
                    Controller.Navigator.Execute(typeof(MainBottomButton).FullName);
                }
                else
                {
                    Main.MipuVisibility = Visibility.Hidden;
                    if (Main.EpcuVisibility == Visibility.Visible)
                        Main.EpcuNotMipmVisibility = Visibility.Visible;
                }
                if (Main.EpcuNotMipmVisibility == Visibility.Hidden && Main.MipuVisibility == Visibility.Hidden)
                    Main.NorEmVisibility = Visibility.Visible;
                else
                {
                    Main.NorEmVisibility = Visibility.Hidden;
                }
            }
            if (args.NewValue.ContainsKey(boolIndex3))
            {
                //Controller.Navigator.Execute(args.NewValue[boolIndex3] ? ViewNames.MainShell : ViewNames.BlackScreenView);
                IsShowMain = args.NewValue[boolIndex3] ? Visibility.Visible : Visibility.Hidden;
            }
            base.DataChanged(sender, args);
        }

        public override void DataChanged(object sender, CommunicationDataChangedArgs<float> args)
        {
            foreach (var tmpValue in typeof(LCDMViewModel).GetProperties().Select(info => info.GetValue(this, null) as IDataChanged))
            {
                if (tmpValue != null)
                {
                    tmpValue.DataChanged(sender, args);
                }
            }
            base.DataChanged(sender, args);
        }

        public override void Clear()
        {
            foreach (var tmpValue in typeof(LCDMViewModel).GetProperties().Select(info => info.GetValue(this, null) as IClear))
            {
                if (tmpValue != null)
                {
                    tmpValue.Clear();
                }
            }
            base.Clear();
        }

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
            DataChanged(sender, dataChangedArgs);
        }

        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {

        }
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
            DataChanged(sender, dataChangedArgs);
        }
    }
}