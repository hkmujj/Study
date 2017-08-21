using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
//using System.Windows.Forms;

using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;


namespace NJ_MMI
{

    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    class ShowVersion: baseClass 
    {
        /// <summary>
        /// 设置按钮
        /// </summary>
        private Button[] btn = new Button[1];
        private List<Button> _menu_Nr = new List<Button>();
        /// <summary>
        /// 设置文本框
        /// </summary>
        private TextBox[] tb;
        private List<TextBox> _ID_setting = new List<TextBox>();
        private List<Image> _images = new List<Image>();
        private string[] line;
        private List<int> id = new List<int>();
        public override string GetInfo()
        {
            return "版本显示";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            System.Text.Encoding.GetEncoding("gb2312");
            line = File.ReadAllLines(@"1D\config\显示内容配置.txt", System.Text.Encoding.Default);
            for (int i = 0; i < line.Length; i++)
            {
                string text = line[i].Substring(line[i].IndexOf("["));
                int a = line[i].IndexOf("[");
                string num = line[i].Substring(0, a);
                id.Add(Convert.ToInt32(num));
            }
            UIObj.ParaList.ForEach(
             a =>
             {
                 using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                 {
                     _images.Add(Image.FromStream(fs));
                 }
             }
             );
            btn[0] = new Button(
                             "主界面",
                             new Rectangle(700, 540, 90, 50),
                             0,
                             new ButtonStyle()
                             {
                                 Background = _images[0],
                                 DownImage = _images[1],
                                 FontStyle = new FontStyle_ES()
                                 {
                                     Font = Global.Font_Arial_14_B,
                                     TextBrush = (SolidBrush)Brushes.Blue,
                                     StringFormat = Global.SF_CC
                                 }
                             });
            _menu_Nr.Add(btn[0]);
            btn[0].ClickEvent += btn_ClickEvent;          
            return true;
        }
         /// <summary>
         /// 按钮事件
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
         private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
         {
             switch (e.Message)
             {
                 case 0:
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[0], 0, 0);
                     append_postCmd(CmdType.ChangePage, 104, 0, 0);
                     break;              
                 default:
                     break;
             }
         }
         public override bool mouseUp(Point nPoint)
         {
             _menu_Nr.ForEach(a => a.MouseUp(nPoint));

             return base.mouseUp(nPoint);
         }
         public override bool mouseDown(Point nPoint)
         {

             _menu_Nr.ForEach(a => a.MouseDown(nPoint));

             return base.mouseDown(nPoint);
         }
        /// <summary>
        /// 显示版本信息
        /// </summary>
        /// <param name="g"></param>
         public void showVersion(Graphics g)
         {
            g. DrawString("显示屏   VERSION:  V1.01   2013/9/25", Global.font, Global.brush1, new Rectangle(5, 460, 770, 35), Global.SF_CN);
            g.DrawString("   BCU      VERSION:  V1.01   2014/10/25", Global.font, Global.brush1, new Rectangle(5, 495, 770, 35), Global.SF_CN);
         }
        public override void paint(Graphics g)
        {
            for (int i = 0; i < id.Count; i++)
            {
                if (BoolList[id[i]])
                {
                    append_postCmd(CmdType.ChangePage, 118, 0, 0);
                }
            }
            if (BoolList[UIObj.InBoolList[0]])
            {
                btn_ClickEvent(null, new ClickEventArgs<int>(0));
            }
            if (BoolList[UIObj.InBoolList[1]])
            {
                append_postCmd(CmdType.ChangePage, 104, 0, 0);
            }
            if (!BoolList[UIObj.InBoolList[2]])
            {
                append_postCmd(CmdType.ChangePage, 103, 0, 0);
            }
            if (BoolList[UIObj.InBoolList[3]])
            {
                append_postCmd(CmdType.ChangePage, 104, 0, 0);
            }
            _menu_Nr.ForEach(a => a.Paint(g));
            showVersion(g);
            base.paint(g);
        }
    }
}
