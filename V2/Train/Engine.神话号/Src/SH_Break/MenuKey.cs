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
    class MenuKey:baseClass 
    {
        /// <summary>
        /// 设置按钮
        /// </summary>
        private Button[] btn = new Button[6];
        private List<TextBox> _mima = new List<TextBox>();
         private List<Button> _menu_Nr = new List<Button>();
        private List<Image> _images = new List<Image>();
        private string[] line;
        private List<int> id = new List<int>();
        public override string GetInfo()
        {
            return "一级菜单";
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
            string[] ButtonText = new string[] { "数据"+System .Environment .NewLine +"监视", "机车号", "时间"+System.Environment .NewLine+"日期", "语言", "软件"+System.Environment.NewLine+"版本", "主界面" };
            for (int i = 0; i < btn.Length; i++)
            {
                btn[i] = new Button(
            ButtonText[i],
            new Rectangle(100 + 100 * i, 540, 90, 50),
            i,
            new ButtonStyle()
            {
                Background = _images[0],
                DownImage = _images[1],
                FontStyle = new FontStyle_ES()
                {
                    Font = Global.Font_Arial_15_B,
                    TextBrush = (SolidBrush)Brushes.Blue,
                    StringFormat = Global.SF_CC
                }
            });
                if (i == 5)
                {
                    btn[i] = new Button(
           ButtonText[i],
           new Rectangle(700, 540, 90, 50),
           i,
           new ButtonStyle()
           {
               Background = _images[0],
               DownImage = _images[1],
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
                     append_postCmd(CmdType.ChangePage, 108, 0, 0);
                     break;
                 case 1:
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[1], 0, 0);
                     append_postCmd(CmdType.ChangePage, 107, 0, 0);
                     break;
                 case 2:
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[2], 0, 0);
                     append_postCmd(CmdType.ChangePage, 106, 0, 0);
                     break;
                 case 3:
                     break;
                 case 4:
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[4], 0, 0);
                     append_postCmd(CmdType.ChangePage, 111, 0, 0);
                     break;
                 case 5:
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[5], 0, 0);
                     append_postCmd(CmdType.ChangePage, 104, 0, 0);
                     break;
                 default:
                     break;
             }
         }
         public override bool mouseUp(Point nPoint)
         {
             _menu_Nr . ForEach(a => a.MouseUp(nPoint));

             return base.mouseUp(nPoint);
         }

         public override bool mouseDown(Point nPoint)
         {
            
             _menu_Nr.ForEach(a => a.MouseDown(nPoint));

             return base.mouseDown(nPoint);
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
            for (int i = 0; i < btn.Length; i++)
            {
                if (BoolList[UIObj.InBoolList[i]] && !BoolOldList[UIObj.InBoolList[i]])
                {
                    btn_ClickEvent(null, new ClickEventArgs<int>(i));
                }

            }
            if (BoolList[UIObj.InBoolList[6]])
            {
                append_postCmd(CmdType.ChangePage, 104, 0, 0);
            }
            if (!BoolList[UIObj.InBoolList[7]])
            {
                append_postCmd(CmdType.ChangePage, 103, 0, 0);
            }
            if (BoolList[UIObj.InBoolList[8]])
            {
                append_postCmd(CmdType.ChangePage, 104, 0, 0);
            }
            _menu_Nr.ForEach(a => a.Paint(g));
            base.paint(g);

        }
    }
}
