using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Config.ConfigModel;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;

namespace CRH2MMI.WorkState
{
    /// <summary>
    ///   //公共工况等
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class WorkStateView : CRH2BaseClass
    {
        private List<GDIRectText> m_ConstInfos;

        private List<GDIRectText> m_BrakeInfos;

        private WorkStateResource m_Resource;

        public override bool Init()
        {

            m_Resource = new WorkStateResource(this);

            var models =GlobalInfo.CurrentCRH2Config.WorkStateConfig.ParkConfigs.Cast<CRH2CommunicationPortModel>().ToList();
            models.AddRange(GlobalInfo.CurrentCRH2Config.WorkStateConfig.LevenConfigs.Configs.Cast<CRH2CommunicationPortModel>());
            InitUIObj(models);

            InitConstInfo();

            InitBrakeInfo();

            return true;
        }

        private void InitBrakeInfo()
        {
            var workStateCongfig = GlobalInfo.CurrentCRH2Config.WorkStateConfig;

            m_BrakeInfos = workStateCongfig.ParkConfigs.Select(s => new GDIRectText()
            {
                TextColor = s.TextColor,
                BkColor = s.BkColor,
                Text = s.Name,
                OutLineRectangle = s.Rectangle,
                RefreshAction = o =>
                {
                    var txt = (GDIRectText) o;
                    txt.Visible = BoolList[GetInBoolIndex(s.InBoolColoumNames.First())];
                }
            }).ToList();

            m_BrakeInfos.Add(new GDIRectText()
            {
                OutLineRectangle = workStateCongfig.LevenConfigs.Rectangle,
                BkColor = Color.Black,
                TextColor = Color.Yellow,
                RefreshAction = o =>
                {
                    var txt = (GDIRectText)o;
                    txt.Text = m_Resource.GetLevel();
                }
            });

        }

        private void InitConstInfo()
        {

            var txtSize = new Size(60, 20);
            m_ConstInfos = new List<GDIRectText>()
            {

                new GDIRectText()
                {
                    Text = "制    动",
                    OutLineRectangle = new Rectangle(312, 111, txtSize.Width, txtSize.Height),
                    BkColor = Color.Black,
                    TextColor = Color.White,
                },

                new GDIRectText()
                {
                    Text = "级",
                    OutLineRectangle = new Rectangle(50, 111, txtSize.Width, txtSize.Height),
                    BkColor = Color.Black,
                    TextColor = Color.White,
                },
            };

        }

        public override void paint(Graphics g)
        {
            m_ConstInfos.ForEach(e => e.OnDraw(g));

            m_BrakeInfos.ForEach(e => e.OnPaint(g));

        }

        public override string GetInfo()
        {
            return "公共工况等";
        }
    }
}