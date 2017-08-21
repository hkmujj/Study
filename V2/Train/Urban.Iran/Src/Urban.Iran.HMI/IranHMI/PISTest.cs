using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.Iran.HMI.Common;
namespace Urban.Iran.HMI
{
    //[GksDataType(DataType.isMMIObjectClass)]
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    //public class PISTest : HMIBase
    //{
    public class PISTest : baseClass
    {
        //private  FjButtonEx[] m_BtnArr;
        //private  Rectangle[] m_TxtRect;
        //private string str = "Activeate Test";
        //private void BtnMouseDown(FjButtonEx btnSender, Point pt)
        //{

        //}

        //public override string GetInfo()
        //{
        //    return "PISTest";
        //}

        //protected override bool Initalize()
        //{

        //    m_TxtRect = new[]
        //              {
        //                  new Rectangle(218, 225, 170, 18),
        //                  new Rectangle(415, 225, 170, 18)
        //              };
        //    m_BtnArr = new[]
        //             {
        //                 new FjButtonEx(1, str, new Rectangle(251, 260, 97, 62)),
        //                 new FjButtonEx(1, str, new Rectangle(448, 260, 97, 62))
        //             };
        //    foreach (var btn in m_BtnArr)
        //    {
        //        btn.MouseDown += BtnMouseDown;

        //    }

        //    return true;
        //}

        //public override void paint(Graphics g)
        //{

        //    m_BtnArr[0].OnDraw(g);
        //    m_BtnArr[1].OnDraw(g);
        //    g.DrawString("LED Test", GdiCommon.Txt14Font, GdiCommon.MediumGreyBrush, m_TxtRect[0], GdiCommon.CenterFormat);
        //    g.DrawString("Cycle broadcasting", GdiCommon.Txt14Font, GdiCommon.MediumGreyBrush, m_TxtRect[1], GdiCommon.CenterFormat);
        //}

        //public override bool mouseDown(Point point)
        //{
        //    foreach (var btn in m_BtnArr)
        //    {
        //        if (btn.IsVisible(point))
        //            btn.OnMouseDown(point);
        //    }
        //    return base.mouseDown(point);
        //}
        #region 私有变量
        private List<Button> _btns = new List<Button>();//按钮列表
        private Rectangle[] m_TxtRect;
        private Image[] m_Img;//图片资源
        public static FontStyle_ES Txt12FontStyle = new FontStyle_ES()
        {
            Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center },
            TextBrush = GdiCommon.MediumGreyBrush
        };
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns></returns>
        public override string GetInfo()
        {
            return "PISTest";
        }

        /// <summary>
        /// 初始化函数：导入图片、创建组件控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_Img = new Image[2];
            m_Img[0] = Image.FromFile(Path.Combine(RecPath, "frame\\btnBkNormal.jpg"));
            m_Img[1] = Image.FromFile(Path.Combine(RecPath, "frame\\btnWarning.jpg"));
            m_TxtRect = new[]
                          {
                              new Rectangle(218, 225, 170, 18),
                              new Rectangle(415, 225, 170, 18)
                          };

            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                     "Activeate Test",
                     new Rectangle(251 + i * 197, 260, 97, 62),
                     i,
                     new ButtonStyle() { FontStyle = Txt12FontStyle, Background = m_Img[0], DownImage = m_Img[1] },
                     false
                     );

                btn.ClickEvent += btn_ClickEvent;
                this._btns.Add(btn);
            }
            return true;
        }
        #endregion
        #region 鼠标事件
        /// <summary>
        /// 组件鼠标按下事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {

            this._btns.ForEach(a => a.MouseDown(nPoint));
            return true;
        }

        /// <summary>
        /// 组件鼠标弹起事件监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {

            this._btns.ForEach(a => a.MouseUp(nPoint));
            return true;
        }

        /// <summary>
        /// 按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {

        }
        #endregion
        #region 界面绘制
        /// <summary>
        /// 界面绘制：按钮绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawString("LED Test", GdiCommon.Txt14Font, GdiCommon.MediumGreyBrush, m_TxtRect[0], GdiCommon.CenterFormat);
            dcGs.DrawString("Cycle broadcasting", GdiCommon.Txt14Font, GdiCommon.MediumGreyBrush, m_TxtRect[1], GdiCommon.CenterFormat);
            foreach (Button btn in _btns)
            {
                if (btn.IsReplication)
                {
                    btn.Text = "Activeate Test";
                }
                else
                {
                    btn.Text = "Test active";
                }
            }
            this._btns.ForEach(a => a.Paint(dcGs));
            if (!_btns[1].IsReplication)
            { append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 1, 0); }
            else
            { append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0], 0, 0); }


            base.paint(dcGs);
        }
        #endregion
    }
}