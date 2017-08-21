using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.Control.Data;
using MMI.Facility.DataType.Config;
using MMI.Facility.DataType.Config.Net;
using MMI.Facility.Interface.Data.Config.Net;
using MMITool.Addin.MMIConfiguration.Constant;
using MMITool.Addin.MMIConfiguration.Infrastructure;
using MMITool.Addin.MMIConfiguration.View.ConfigureContent.NetDetail;
using MMITool.Addin.MMIConfiguration.View.ConfigureContent.NetDetail.DataProtocol;
using MMITool.Addin.MMIConfiguration.ViewModel;
using ANetDetailConfigView = MMITool.Addin.MMIConfiguration.View.ConfigureContent.NetDetail.Channel.ANetDetailConfigView;
using BNetDetailConfigView = MMITool.Addin.MMIConfiguration.View.ConfigureContent.NetDetail.Channel.BNetDetailConfigView;
using CNetDetailConfigView = MMITool.Addin.MMIConfiguration.View.ConfigureContent.NetDetail.Channel.CNetDetailConfigView;
using DNetDetailConfigView = MMITool.Addin.MMIConfiguration.View.ConfigureContent.NetDetail.Channel.DNetDetailConfigView;
using PTT2DNetDetailConfigView = MMITool.Addin.MMIConfiguration.View.ConfigureContent.NetDetail.Channel.PTT2DNetDetailConfigView;

namespace MMITool.Addin.MMIConfiguration.Controller
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NetConfigController : ConfigContentControllerBase<NetConfigViewModel>
    {
        public ICommand GetLocalIpCellectionCommand { get; private set; }

        public ICommand UpdateProtocolContentCommand { private set; get; }

        public ICommand SelectTypeCommand { private set; get; }

        private readonly IRegionManager m_RegionManager;

        public const string UpdateSelfChild = "UpdateSelfChild";

        public override NetConfigViewModel ViewModel { get; set; }

        private void UpdateDetailView()
        {
            string viewName;
            switch (ViewModel.Model.NetConfig.NetChannelConfig.NetType)
            {
                case NetType.None:
                    viewName = typeof(ANetDetailConfigView).Name;
                    break;
                case NetType.B:
                    viewName = typeof(BNetDetailConfigView).Name;
                    break;
                case NetType.C:
                    viewName = typeof(CNetDetailConfigView).Name;
                    break;
                case NetType.PTT2D:
                    viewName = typeof(PTT2DNetDetailConfigView).Name;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var uriQuery = new UriQuery {{UpdateSelfChild, UpdateSelfChild}};
            m_RegionManager.RequestNavigate(ConfigurationRegionNames.NetDetailConfigRegion,
                new Uri(viewName + uriQuery, UriKind.Relative));
        }

        [ImportingConstructor]
        public NetConfigController(IRegionManager regionManager)
        {
            m_RegionManager = regionManager;
            GetLocalIpCellectionCommand = new DelegateCommand(GetLocIpNumList);
            SelectTypeCommand = new DelegateCommand(UpdateDetailView);
            UpdateProtocolContentCommand = new DelegateCommand(OnUpdateProtocolContent);
        }

        private void OnUpdateProtocolContent()
        {
            string content;
            switch (ViewModel.Model.NetConfig.NetDataProtocolConfig.ProtocolType)
            {
                case NetDataProtocolType.PackageIdOnly:
                    content = typeof(PackageIndexOnlyView).FullName;
                    break;
                case NetDataProtocolType.BussnessIdAndPackageId:
                    content = typeof(BussnessIdAndPackageIdConfigView).FullName;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var uriQuery = new UriQuery { { UpdateSelfChild, UpdateSelfChild } };
            m_RegionManager.RequestNavigate(ConfigurationRegionNames.NetDetailProtocolConfigRegion, new Uri(content + uriQuery, UriKind.Relative));
        }

        /// <summary>
        /// 获取本地IP List
        /// </summary>
        private void GetLocIpNumList()
        {
            var ips = Dns.GetHostAddresses(Dns.GetHostName()).Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToList();
            List<string> locIpNum = ips.Select(t => t.ToString()).ToList();
            ViewModel.Model.LocIpNameCollection = locIpNum;
        }

        protected override bool HasInitalzied
        {
            get { return ViewModel.Model.NetConfig != null; }
        }

        public override void ResetConfig()
        {
            ViewModel.Model.NetConfig = XmlModelDeepCopy.Copy((NetConfig) ConfigManager.Instance.Config.NetConfig);
            UpdateDetailView();
        }

        public override void SaveConfig()
        {
            ConcreateRootConfig.NetConfig = ViewModel.Model.NetConfig;
            SaveConfig(ViewModel.Model.NetConfig);
        }
    }
}
