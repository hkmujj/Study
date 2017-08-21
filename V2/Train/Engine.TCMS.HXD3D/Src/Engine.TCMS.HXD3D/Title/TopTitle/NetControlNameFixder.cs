using System;
using System.Collections.Generic;
using CommonUtil.Model;
using Engine.TCMS.HXD3D.Constant;
using Engine.TCMS.HXD3D.过程数据.NetControl;

namespace Engine.TCMS.HXD3D.Title.TopTitle
{
    public class NetControlNameFixder : ITopTitleNameFixder
    {
        private readonly ProcessNetControl m_ProcessNetControl;

        private readonly Lazy<IReadOnlyDictionary<NetControlChildType, string>> m_ChildTitleNameDictionary;

        public NetControlNameFixder(ProcessNetControl processNetControl)
        {
            m_ProcessNetControl = processNetControl;

            m_ChildTitleNameDictionary =
                new Lazy<IReadOnlyDictionary<NetControlChildType, string>>(
                    () =>
                        new ReadOnlyDictionary<NetControlChildType, string>(new Dictionary<NetControlChildType, string>()
                        {
                            {NetControlChildType.NetControlRoot, "网络控制"},
                            {NetControlChildType.SoftVersion, "软件版本"},
                            {NetControlChildType.SignalInfo, "网络控制-信号信息"},
                            {NetControlChildType.TrainsInfo, "网络控制-传送信息"},
                        }));
        }

        public string UpdateTitleName(int nParaA, int nParaB, float nParaC, string currentName = null)
        {
            if (nParaB == (int) ViewId.ProcessDataNetControl)
            {
                return m_ChildTitleNameDictionary.Value[m_ProcessNetControl.CurrentChildType];
            }

            return currentName;
        }
    }
}