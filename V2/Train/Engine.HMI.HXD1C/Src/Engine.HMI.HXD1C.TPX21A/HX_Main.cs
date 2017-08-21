using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.HMI.HXD1C.TPX21A.Button;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class HX_Main : baseClass, IButtonEventListener
    {
        public static string m_TrainID = "HXDIC0001";
        private Image m_Img;
        private readonly Pen m_LinePen = new Pen(Color.FromArgb(202, 202, 202));
        private readonly SolidBrush m_DarkBrush = new SolidBrush(Color.FromArgb(148, 148, 148));
        private Rectangle m_C2Rect;
        private Rectangle m_TitleRect;
        private Rectangle m_InfoRect;
        private Rectangle m_DateTimeRect;
        private bool m_IseventInfo;
        private bool m_IshelpInfo;
        private bool m_Isunalert;

        private List<int> m_BelongViewIds;

        public override string GetInfo()
        {
            return "主界面";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            m_BelongViewIds = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 25 };
            GlobalParam.Instance.AddButtonEventListener(this);
            HxCommon hc = new HxCommon();//调用HX_Common的构造函数初始化HX_Common中的元素
            
            InitData();
            //加载图片
            if (UIObj.ParaList.Count>0)
            {
                m_Img = Image.FromFile(RecPath + "\\" + UIObj.ParaList[0]);
            }
           
            nErrorObjectIndex = -1;

            return true;
        }

        public void InitData()
        {
            m_C2Rect = new Rectangle(HxCommon.Recposition.X + 2, HxCommon.Recposition.Y + 2, 40, 27);
            m_TitleRect = new Rectangle(m_C2Rect.Right+ 2, HxCommon.Recposition.Y + 2, 445, 27);
            m_InfoRect = new Rectangle(m_TitleRect.Right + 2, HxCommon.Recposition.Y + 2, 140, 27);
            m_DateTimeRect = new Rectangle(m_InfoRect.Right + 2, HxCommon.Recposition.Y + 2, 160, 27);
        }

        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
            ButtonDownEvent();
        }

        public void DrawOn(Graphics g)
        {
           //g.DrawRectangle(new Pen(Color.FromArgb(255,0,0),2),HX_Common.recposition);//定一个800*600的矩形框 作为参考大小
            g.DrawRectangle(m_LinePen, m_C2Rect);
            g.DrawRectangle(m_LinePen, m_TitleRect);
            g.DrawRectangle(m_LinePen, m_InfoRect);
            g.DrawRectangle(m_LinePen, m_DateTimeRect);
           // g.DrawRectangle(linePen, HX_Common.Rect_Public);
            
            HxCommon.HTitle.OnDraw(g);
            g.DrawString("机车编号:", HxCommon.Font12,m_DarkBrush,HxCommon.RectPublic.X+300,HxCommon.RectPublic.Y+5);
            g.DrawString("C2", HxCommon.Font12B, HxCommon.WhiteBrush, m_C2Rect.X + 15, m_C2Rect.Y + 3);
            g.DrawString(m_TrainID, HxCommon.Font12B, HxCommon.WhiteBrush, HxCommon.RectPublic.X + 370, HxCommon.RectPublic.Y + 5);
            g.DrawString("蓄电池高于88V", HxCommon.Font12B, HxCommon.WhiteBrush, m_InfoRect.X + 8, m_InfoRect.Y+3);
            g.DrawString(DateTime.Now.ToString(), HxCommon.Font12, HxCommon.WhiteBrush, m_DateTimeRect.X + 3, m_DateTimeRect.Y+3);

            #region 测试代码
            g.DrawRectangle(new Pen(Color.White, 5), 0, 0, 800, 600);
            g.DrawRectangle(Pens.Red, HxCommon.Recposition);
            #endregion 
            for (int i = 0; i < 4;i++ )
            {
                HxCommon.HDefault[i].OnDraw(g);
            }

            ////绘制键盘区图标
            g.DrawImage(m_Img, HxCommon.ButtonInfo);
            g.DrawRectangle(HxCommon.LinePen2, HxCommon.ButtonInfo);
        }

        public void GetValue()
        {
            m_IseventInfo=BoolList[UIObj.InBoolList[0]];
            m_IshelpInfo = BoolList[UIObj.InBoolList[1]];
            m_Isunalert = BoolList[UIObj.InBoolList[2]];
           
        }


        public void ButtonDownEvent()
        {
           if(m_IseventInfo)
           {
                append_postCmd(CmdType.ChangePage, 13, 0, 0);
           }

           if(m_IshelpInfo)
           {
               append_postCmd(CmdType.ChangePage, 25, 0, 0);
           }
        }

        public bool ResponseMouseDown(ButtonName btn)
        {
            if (m_BelongViewIds.Contains(GlobalParam.Instance.CurrentViewId))
            {
                m_IshelpInfo = false;
                m_IseventInfo = false;
                switch (btn)
                {
                    case ButtonName.Help:
                        m_IshelpInfo = true;
                        break;
                    case ButtonName.Fault:
                        m_IseventInfo = true;
                        break;
                }
                ButtonDownEvent();
                return true;
            }

            return false;
        }

        public bool ResponseMouseUp(ButtonName btn)

        {
            if (m_BelongViewIds.Contains(GlobalParam.Instance.CurrentViewId))
            {
                m_IshelpInfo = false;
                m_IseventInfo = false;
                return true;
            }
            return false;
        }
    }
}
