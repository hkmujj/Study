using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Motor.HMI.CRH1A.Alarm.Fault;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using CommonUtil.Controls;
using Motor.HMI.CRH1A.Common.Global;
using Timer = System.Threading.Timer;

namespace Motor.HMI.CRH1A.Startup
{
    /// <summary>
    /// 启动界面
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class StartUpView : CRH1BaseClass
    {
        private Font m_Font;

        private string m_StartInfo;


        public override string GetInfo()
        {
            return "启动视图";
        }

        public override bool Initialize()
        {
            m_Font = new Font(CommonResouce.DefalutFont.FontFamily, 20, FontStyle.Bold);

            return true;
        }

        protected override void NavigateTo(ViewConfig to)
        {
            //if (nParaA == ParaADefine.SwitchFromOhter)
            {
                m_StartInfo = "启动中";
                var timerModel = new StartUpTimerModel(this);
                var timer = new Timer(state =>
                {
                    var su = (StartUpTimerModel)state;
                    ++su.TimeCount;
                    if (su.TimeCount <= 6)
                    {
                        su.Content.m_StartInfo += ".";
                    }
                    else
                    {
                        su.CurretnTimer.Dispose();
                        GT_GalobalFaultManage.Instance.CanResponsePopupFault = true;
                        OnPost(CmdType.ChangePage, (int)ViewConfig.Login, 0, 0);
                        GlobalEvent.Instance.OnRestartCompleted();
                    }

                }, timerModel, 300, 1000);
                timerModel.CurretnTimer = timer;
            }
        }

        public override void paint(Graphics g)
        {
            g.DrawString(m_StartInfo, m_Font, Brushes.White, new PointF(300, 300));
        }
    }
}
