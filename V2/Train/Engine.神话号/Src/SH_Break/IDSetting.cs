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
    class IDSetting:baseClass 
    {
        /// <summary>
        /// 设置按钮
        /// </summary>
        private Button[] btn = new Button[5];
        private List<Button> _menu_Nr = new List<Button>();
        /// <summary>
        /// 设置文本框
        /// </summary>
        private TextBox[] tb = new TextBox[5];
        private List<TextBox> _ID_setting = new List<TextBox>();
        private List<Image> _images1 = new List<Image>();
        private List<Image> _images2 = new List<Image>();
        private string[] line;
        private List<int> id = new List<int>();
        public override string GetInfo()
        {
            return "机车号设置";
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
                      _images1.Add(Image.FromStream(fs));
                  }
              }
              );
            string[] IDButtonText = new string[] { "递增", "递减", "左移", "右移", "主界面" };
            for (int i = 0; i < btn.Length; i++)
            {
                btn[i] = new Button(
                IDButtonText[i],
                new Rectangle(0 + 100 * i, 540, 90, 50),
                i,
                new ButtonStyle()
                {
                    Background = _images1[0],
                    DownImage = _images1[1],
                    FontStyle = new FontStyle_ES()
                    {
                        Font = Global.Font_Arial_15_B,
                        TextBrush = (SolidBrush)Brushes.Blue,
                        StringFormat = Global.SF_CC
                    }
                });
                if (i == 4)
                {
                    btn[4] = new Button(
                IDButtonText[i],
                new Rectangle(710, 540, 90, 50),
                4,
                new ButtonStyle()
                {
                    Background = _images1[0],
                    DownImage = _images1[1],
                    FontStyle = new FontStyle_ES()
                    {
                        Font = Global.Font_Arial_15_B,
                        TextBrush = (SolidBrush)Brushes.Blue,
                        StringFormat = Global.SF_CC
                    }
                });
                }
                _menu_Nr.Add(btn[i]);
                btn[i].ClickEvent += btn_ClickEvent;
            }                       
             UIObj.ParaList.ForEach(
              a =>
              {
                  using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                  {
                      _images2.Add(Image.FromStream(fs));
                  }
              }
              );
             for (int i = 0; i < tb.Length; i++)
             {
                 tb[i] = new TextBox(
                           new Rectangle(260 + 30 * i, 490, 15, 15),
                           new TextBoxStyle()
                           {
                               Background = _images2[0],
                               BackgroundFocus = _images2[1],
                               FontStyle = new FontStyle_ES()
                               {
                                   Font = Global.Font_Arial_08_B,
                                   TextBrush = (SolidBrush)Brushes.Red,
                                   StringFormat = Global.SF_CC
                               }
                           }, i);
                 _ID_setting.Add(tb[i]);
             }
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
                     break;
                 case 1:           
                     break;
                 case 2:                   
                     break;
                 case 3:
                     break;
                 case 4:
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[4], 0, 0);
                     append_postCmd(CmdType.ChangePage, 104, 0, 0);
                     break;
                 case 5:
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
        /// 设置机车号文本信息
        /// </summary>
        /// <param name="g"></param>
 
        public override void paint(Graphics g)
         {
             for (int i = 0; i < id.Count; i++)
             {
                 if (BoolList[id[i]])
                 {
                     append_postCmd(CmdType.ChangePage, 118, 0, 0);
                 }
             }
             for (int i = 0; i < btn.Length; i++)
             {
                 if (BoolList[UIObj.InBoolList[i]] && !BoolOldList[UIObj.InBoolList[i]])
                 {
                     btn_ClickEvent(null, new ClickEventArgs<int>(i));
                 }
             }
             if (BoolList[UIObj.InBoolList[5]])
             {
                 append_postCmd(CmdType.ChangePage, 104, 0, 0);
             }
             if (!BoolList[UIObj.InBoolList[6]])
             {
                 append_postCmd(CmdType.ChangePage, 103, 0, 0);
             }
             if (BoolList[UIObj.InBoolList[7]])
             {
                 append_postCmd(CmdType.ChangePage, 104, 0, 0);
             }
             g.DrawString("设置机车号", Global.Font_Arial_08_B, Global.brush1, new Rectangle(180, 460, 80, 80), Global.SF_CC);
             _ID_setting.ForEach(a => a.Paint(g));
             _menu_Nr.ForEach(a => a.Paint(g));
             base.paint(g);

         }
    }
}
