using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Model;
using Engine.TCMS.HXD3D.Fault.Ensure.Detail;
using Engine.TCMS.HXD3D.底层共用;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3D.Fault.Ensure
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class FaultEnsure : HXD3BaseClass
    {
        private int m_LastViewID = -100;

        private CommonUtil.Model.IReadOnlyDictionary<EnsureType, FaultEnsureView> m_EnsureViewDictionary;

        private FaultEnsureView m_CurrentEnsureView;

        private FaultMsgHXD3D[] m_ToBeEnsureing;

        public override string GetInfo()
        {
            return "故障确认";
        }

        protected override void Initalize()
        {
            m_EnsureViewDictionary =
                new ReadOnlyDictionary<EnsureType, FaultEnsureView>(new Dictionary<EnsureType, FaultEnsureView>()
                {
                    {
                        EnsureType.EnsureItem, new EnsureItemView(this)
                        {
                            EnsureItem = it =>
                            {
                                EnsureFault(it);
                                append_postCmd(CmdType.ChangePage, m_LastViewID > 0 ? m_LastViewID : 1, 0, 0f);
                            },
                            ReturnAction = () =>
                            {
                                append_postCmd(CmdType.ChangePage, m_LastViewID > 0 ? m_LastViewID : 1, 0, 0f);
                            }
                        }
                    },
                    {
                        EnsureType.EnsureAll, new EnsureAllView(this)
                        {
                            EnsureItem = d =>
                            {
                                UpdateToEnsure(d);
                                NavigateTo(EnsureType.EnsureItem);
                            },
                            //ReturnAction = () =>
                            //{
                            //    append_postCmd(CmdType.ChangePage, m_LastViewID > 0 ? m_LastViewID : 1, 0, 0f);
                            //},
                            EnsureAllAction = () =>
                            {
                                foreach (var it in FaultReceive.MsgInf.CurrentMsgList)
                                {
                                    EnsureFault(it);
                                }
                            }
                        }
                    },
                });

            foreach (var kvp in m_EnsureViewDictionary)
            {
                kvp.Value.Init();
            }
        }

        private void EnsureFault(FaultMsgHXD3D it)
        {
            it.MsgOverTime = DateTime.Now;
            FaultReceive.MsgInf.MsgBeRead(FaultReceive.MsgInf.UnFlagCurrentMsgList.IndexOf(it));
            //FaultReceive.MsgInf.MsgOver(it.MsgID);
        }

        public void UpdateToEnsure(params FaultMsgHXD3D[] msgs)
        {
            m_ToBeEnsureing = msgs;
        }

        public override bool mouseDown(Point point)
        {
            m_CurrentEnsureView.OnMouseDown(point);

            return true;
        }

        public override bool mouseUp(Point point)
        {
            m_CurrentEnsureView.OnMouseUp(point);

            return true;
        }

        public override void paint(Graphics g)
        {
            m_CurrentEnsureView.OnPaint(g);
        }

        public void NavigateTo(EnsureType ensureType)
        {
            m_CurrentEnsureView = m_EnsureViewDictionary[ensureType];
            m_CurrentEnsureView.NavigateToThis(m_ToBeEnsureing);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                m_LastViewID = (int) nParaC;

                //switch (m_LastViewID)
                //{
                //    case 81:
                //    case 82:
                //        NavigateTo(EnsureType.EnsureItem);
                //        break;
                //    default:
                //        NavigateTo(EnsureType.EnsureAll);
                //        break;
                //}
            }
        }
    }
}