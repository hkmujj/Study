using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Controls.Grid.GridLine;
using CommonUtil.Controls.Grid.Items;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Config;
using Motor.HMI.CRH1A.Common.Global;
using Motor.HMI.CRH1A.Common.View;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Motor.HMI.CRH1A.SystemMenu
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class GT_SystemMenu : CRH1BaseClass
    {
        private readonly GT_MenuTitle m_Title = new GT_MenuTitle("系统");//菜单标题       
        private Rectangle m_Recposition = new Rectangle(0, 185, 70, 40);

        private SystemButtonRegion m_SystemButtonRegion;

        private List<CRH1AButton> m_ControlBtns;

        public override string GetInfo()
        {
            return "系统概况菜单";
        }

        public override bool Initialize()
        {
            m_SystemButtonRegion = new SystemButtonRegion(GlobalInfo.Instance.CRH1ADetailConfig.SystemMenuConfig.Region, this);

            InitData();

            return true;
        }
        public override void paint(Graphics g)
        {
            m_Title.OnDraw(g);
            m_SystemButtonRegion.OnDraw(g);
            m_ControlBtns.ForEach(e => e.OnDraw(g));
        }
        public void InitData()
        {

            m_ControlBtns = new List<CRH1AButton>();
            var btn = new CRH1Button()
                      {
                          ButtonUpEvent = (sender, args) => OnPost(CmdType.ChangePage, (int)ViewConfig.MainView, 0, 0),
                      };
            btn.SetButtonRect(m_Recposition.X + 624, m_Recposition.Y + 326, 80, 50);
            btn.SetButtonColor(192, 192, 192);
            btn.SetButtonText("主菜单");
            m_ControlBtns.Add(btn);

            btn = new CRH1Button()
                  {
                      ButtonUpEvent = (sender, args) =>
                      {
                          if (GlobalParam.Instance.TrainInfo.Speed >= 3)//速度高于3km时跳转到运行界面
                              OnPost(CmdType.ChangePage, (int)ViewConfig.Running, 0, 0);
                          else
                          {
                              OnPost(CmdType.ChangePage, (int)ViewConfig.Station, 0, 0);
                          }
                      }
                  };
            btn.SetButtonColor(192, 192, 192);
            btn.SetButtonRect(m_Recposition.X + 710, m_Recposition.Y + 326, 80, 50);
            btn.SetButtonText("运行/车站");
            m_ControlBtns.Add(btn);
            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                m_ControlBtns.ForEach(f => f.IsEnable = GlobalInfo.Instance.ButtonEnable);
            };

        }

        protected override bool OnMouseDown(Point point)
        {
            return m_SystemButtonRegion.OnMouseDown(point) || m_ControlBtns.Any(a => a.OnMouseDown(point));
        }

        protected override bool OnMouseUp(Point point)
        {
            return m_SystemButtonRegion.OnMouseUp(point) || m_ControlBtns.Any(a => a.OnMouseUp(point));
        }
    }
}
