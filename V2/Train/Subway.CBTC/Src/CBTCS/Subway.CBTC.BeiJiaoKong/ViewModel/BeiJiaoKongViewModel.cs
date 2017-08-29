using System.ComponentModel.Composition;
using System.Windows;
using CBTC.Infrasturcture.Model.Send;
using CBTC.Infrasturcture.ViewModel.Monitor;
using Subway.CBTC.BeiJiaoKong.Controller;
using MMI.Facility.Interface.Service;
using Subway.CBTC.BeiJiaoKong.Models;
using Subway.CBTC.BeiJiaoKong.Models.Domain;

namespace Subway.CBTC.BeiJiaoKong.ViewModel
{
    /// <summary>
    /// 创 建 者：谭栋炜
    /// 创建时间：2017/1/16
    /// 修 改 者：谭栋炜
    /// 修改时间：2017/1/16
    /// </summary>
    [Export]
    public class BeiJiaoKongViewModel : ViewModelBase, ICBTCProvider
    {
        private InputKeyBoardModel m_InputKeyBoard;
        private InputScreenModel m_InputScreen;
        private TCTType m_TCTType;
        private int m_ScreenSaverDelay;
        private bool m_IsScreenSaver;

        public ICommunicationDataService DataService { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controller">控制器</param>
        [ImportingConstructor]
        public BeiJiaoKongViewModel(BeiJiaoKongController controller)
        {
            controller.ViewModel = this;
            Controller = controller;
            InputKeyBoard = new InputKeyBoardModel(this);
            InputScreen = new InputScreenModel(this);
            Domain = new Domain(GlobalParams.Instance.InitParam);
            Domain.SendInterface = new EmptySendInterface();
            controller.Initalize();
            DoMainViewModel = this;
        }

        public static BeiJiaoKongViewModel DoMainViewModel { get; private set; }

        /// <summary>
        /// 控制类
        /// </summary>
        public BeiJiaoKongController Controller { get; private set; }

        public InputScreenModel InputScreen
        {
            get { return m_InputScreen; }
            set
            {
                if (Equals(value, m_InputScreen))
                {
                    return;
                }
                m_InputScreen = value;
                RaisePropertyChanged(() => InputScreen);
            }
        }

        public InputKeyBoardModel InputKeyBoard
        {
            get { return m_InputKeyBoard; }
            set
            {
                if (Equals(value, m_InputKeyBoard))
                {
                    return;
                }
                m_InputKeyBoard = value;
                RaisePropertyChanged(() => InputKeyBoard);
            }
        }

        public TCTType TCTType
        {
            get { return m_TCTType; }
            set
            {
                if (Equals(value, m_TCTType))
                {
                    return;
                }
                m_TCTType = value;
                RaisePropertyChanged(() => TCTType);
            }
        }

        /// <summary>
        /// 未操作屏延时进入屏保
        /// </summary>
        public int ScreenSaverDelay
        {
            get { return m_ScreenSaverDelay; }
            set
            {
                if (value == m_ScreenSaverDelay)
                {
                    return;
                }
                m_ScreenSaverDelay = value;
                RaisePropertyChanged(() => ScreenSaverDelay);
            }
        }

        /// <summary>
        /// 是否进入屏保状态 true=亮屏  false=黑屏
        /// </summary>
        public bool IsScreenSaver
        {
            get { return m_IsScreenSaver; }
            set
            {
                if (value == m_IsScreenSaver)
                {
                    return;
                }
                m_IsScreenSaver = value;
                RaisePropertyChanged(() => IsScreenSaver);
            }
        }

        [Import]
        public static Domain Domain { get; set; }

        public global::CBTC.Infrasturcture.Model.CBTC CBTC { get { return Domain; } }

    }
}