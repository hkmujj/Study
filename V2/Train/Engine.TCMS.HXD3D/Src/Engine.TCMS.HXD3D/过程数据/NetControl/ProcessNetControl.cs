using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Model;
using Engine.TCMS.HXD3D.底层共用;
using Engine.TCMS.HXD3D.过程数据.NetControl.Child;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.过程数据.NetControl
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ProcessNetControl : HXD3BaseClass
    {
        private bool m_HasInitChild;

        public NetControlChildType CurrentChildType { get { return m_CurrentChildView.ChildType; } }

        private Lazy<CommonUtil.Model.IReadOnlyDictionary<NetControlChildType, NetControlChildView>> m_ChildDictionary;

        private NetControlChildView m_CurrentChildView;

        public override string GetInfo()
        {
            return "网络控制";
        }

        protected override void Initalize()
        {
            m_ChildDictionary =
                new Lazy<CommonUtil.Model.IReadOnlyDictionary<NetControlChildType, NetControlChildView>>(
                    () =>
                        new ReadOnlyDictionary<NetControlChildType, NetControlChildView>(
                            new Dictionary<NetControlChildType, NetControlChildView>()
                            {
                                {NetControlChildType.NetControlRoot, new NetControlRootView(this)},
                                {NetControlChildType.SoftVersion, new NetSoftVersionView(this)},
                                {NetControlChildType.TrainsInfo, new NetTrainsInfoView(this)},
                                {NetControlChildType.SignalInfo, new NetSiganlInfoView(this)},
                            }));
        }

        public void RequestNavigateTo(NetControlChildType childType)
        {
            var value = m_ChildDictionary.Value[childType];
            if (value != m_CurrentChildView && value != null)
            {
                value.NavigateToThis();
            }
            m_CurrentChildView = value;
        }

        /// <summary>设置动态数据</summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (!m_HasInitChild)
            {
                foreach (var view in m_ChildDictionary.Value.Values)
                {
                    view.Init();
                }

                RequestNavigateTo(NetControlChildType.NetControlRoot);

                m_HasInitChild = true;
            }

            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                RequestNavigateTo(NetControlChildType.NetControlRoot);
            }
        }

        /// <summary>绘制图像</summary>
        /// <param name="g"></param>
        public override void paint(Graphics g)
        {
            m_CurrentChildView.OnPaint(g);
        }

        /// <summary>鼠标单点下</summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool mouseDown(Point point)
        {
            return m_CurrentChildView.OnMouseDown(point);
        }

        /// <summary>鼠标弹起</summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public override bool mouseUp(Point point)
        {
            return m_CurrentChildView.OnMouseUp(point);
        }
    }
}