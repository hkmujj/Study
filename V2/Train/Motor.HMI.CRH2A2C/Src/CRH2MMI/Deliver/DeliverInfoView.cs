using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CommonUtil.Controls.Roundness;
using CommonUtil.Util.Extension;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View.Train;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI.Deliver
{
    /// <summary>
    /// 出库信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class DeliverInfoView : CRH2BaseClass
    {
        private TrainView m_TrainView;

        private List<GDIRoundness> m_DeliverCarInfoList;

        private List<CommonInnerControlBase> m_ConstInfoList;

        private GDIRectText m_ResultTypeText;
        private DeliverResultType m_DeliverResultType;

        // ReSharper disable once InconsistentNaming
        private static readonly Rectangle m_InfoRectangle = new Rectangle(2, 268, 794, 248);

        // ReSharper disable once InconsistentNaming
        private static readonly Rectangle m_WhiteRectangle = new Rectangle(m_InfoRectangle.X, m_InfoRectangle.Y, m_InfoRectangle.Width, 20);

        public DeliverResultType DeliverResultType
        {
            set
            {
                m_DeliverResultType = value;
                switch (m_DeliverResultType)
                {
                    case DeliverResultType.Normal:
                        m_ResultTypeText.Text = "正常";
                        m_ResultTypeText.TextColor = Color.White;
                        break;
                    case DeliverResultType.Abnormal:
                        m_ResultTypeText.Text = "异常";
                        m_ResultTypeText.TextColor = Color.Red;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            get { return m_DeliverResultType; }
        }

        public override bool Init()
        {

            m_TrainView = TrainView.Instance;

            InitConstInfos();

            InitTypeInfo();

            InitStateRoundness();

            return true;

        }

        private void InitStateRoundness()
        {
            ModifyTrainLocation();


            m_DeliverCarInfoList = GlobalInfo.CurrentCRH2Config.DeliverConfig.DeliverCarConfigs
                                             .Select(s => new GDIRoundness()
                                                          {
                                                              Center =
                                                                  m_TrainView.Cars[s.CarNo].Location.Translate(Car.DefaultSize.Width / 2,
                                                                      Car.DefaultSize.Height + 30),
                                                              R = 10,
                                                              NeedDrawContent = true,
                                                              NeedDrawArc = false,
                                                              RefreshAction = o =>
                                                              {
                                                                  var round = (GDIRoundness) o;
                                                                  round.ContentColor =
                                                                      BoolList[this.GetInBoolIndex(s.InBoolColoumNames[0])]
                                                                          ? s.Colors[1]
                                                                          : s.Colors[0];
                                                              },
                                                          }).ToList();
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                ModifyTrainLocation();
            }
        }

        private void ModifyTrainLocation()
        {
            m_TrainView.Location = GlobalInfo.CurrentCRH2Config.DeliverConfig.TrainViewLocation.Rectangle.Location;
        }

        public override string GetInfo()
        {
            return "出库信息";
        }



        private void InitTypeInfo()
        {
            var typeRect = new Rectangle(255, 230, 70, 30);

            m_ResultTypeText = new GDIRectText()
                               {
                                   BkColor = Color.Black,
                                   OutLineRectangle = typeRect,
                                   NeedDarwOutline = false,
                                   DrawFont = new Font(SystemFonts.DefaultFont.FontFamily, 20, FontStyle.Bold),
                               };

            DeliverResultType = GlobalInfo.CurrentCRH2Config.DeliverConfig.DeliverResultType;
        }

        private void InitConstInfos()
        {
            InitInterpreter();

            InitConstTexts();
        }

        private void InitConstTexts()
        {
            var x = 137;
            var y = 230;

            m_ConstInfoList.Add(new GDIRectText()
            {
                BkColor = Color.Black,
                OutLineRectangle = new Rectangle(new Point(x, y), new Size(300, 30)),
                TextColor = Color.White,
                Text = "出库检查结果                  位",
                NeedDarwOutline = false,
                Bold = true,
            });

            var txtSize = new Size(80, m_WhiteRectangle.Height);
            x = 25;
            m_ConstInfoList.Add(new GDIRectText()
            {
                BkColor = Color.White,
                OutLineRectangle = new Rectangle(new Point(x, m_WhiteRectangle.Y), txtSize),
                TextColor = Color.Black,
                Text = "(车厢)",
                NeedDarwOutline = false,
                Bold = true,
            });

            x = 170;
            m_ConstInfoList.Add(new GDIRectText()
            {
                BkColor = Color.White,
                OutLineRectangle = new Rectangle(new Point(x, m_WhiteRectangle.Y), txtSize),
                TextColor = Color.Black,
                Text = "(故障码)",
                NeedDarwOutline = false,
                Bold = true,
            });
        }

        private void InitInterpreter()
        {
            var x = 450;
            var y = 230;
            var textSize = new Size(65, 20);
            const int r = 8;
            const int interval = 10;
            m_ConstInfoList = new List<CommonInnerControlBase>
                              {
                                  new GDIRectText()
                                  {
                                      BkColor = Color.Black,
                                      OutLineRectangle = new Rectangle(new Point(x, y), new Size(20, textSize.Height)),
                                      TextColor = Color.White,
                                      Text = "( ",
                                      NeedDarwOutline = false,
                                      Bold = true,
                                  }
                              };

            m_ConstInfoList.Add(new GDIRoundness()
                                {
                                    ContentColor = Color.White,
                                    NeedDrawArc = false,
                                    NeedDrawContent = true,
                                    Center = new Point(m_ConstInfoList.Last().OutLineRectangle.Right + interval, y + textSize.Height / 2),
                                    R = r,
                                });

            m_ConstInfoList.Add(new GDIRectText()
                                {
                                    BkColor = Color.Black,
                                    OutLineRectangle =
                                        new Rectangle(new Point(m_ConstInfoList.Last().OutLineRectangle.Right + interval, y),
                                        new Size(textSize.Width, textSize.Height)),
                                    TextColor = Color.White,
                                    Text = ": 正常",
                                    NeedDarwOutline = false,
                                    Bold = true,
                                });
            m_ConstInfoList.Add(new GDIRoundness()
                                {
                                    ContentColor = Color.Red,
                                    NeedDrawArc = false,
                                    NeedDrawContent = true,
                                    Center = new Point(m_ConstInfoList.Last().OutLineRectangle.Right + interval, y + textSize.Height / 2),
                                    R = r,
                                });

            m_ConstInfoList.Add(new GDIRectText()
                                {
                                    BkColor = Color.Black,
                                    OutLineRectangle =
                                        new Rectangle(new Point(m_ConstInfoList.Last().OutLineRectangle.Right + interval, y),
                                        new Size(textSize.Width + 10, textSize.Height)),
                                    TextColor = Color.White,
                                    Text = ": 异常  )",
                                    NeedDarwOutline = false,
                                    Bold = true,
                                });
        }

        public override void paint(Graphics g)
        {
            g.FillRectangle(Brushes.White, m_WhiteRectangle);
            g.DrawRectangle(Pens.White, m_InfoRectangle);

            m_ConstInfoList.ForEach(e => e.OnDraw(g));

            m_ResultTypeText.OnDraw(g);

            m_DeliverCarInfoList.ForEach(e => e.OnPaint(g));
        }
    }
}
