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
    class IncidentRecord : baseClass 
    {
        /// <summary>
        /// 设置按钮
        /// </summary>
        private Button[] btn = new Button[4];
        private List<Button> _menu_Nr = new List<Button>();
        private List<Image> _images = new List<Image>();
        private string[] line;
        private List<int> id = new List<int>();
        public override string GetInfo()
        {
            return "事件记录";
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
            string[] ButtonText = new string[] { "主页", "查询", "返回", "主界面" };
            for (int i = 0; i < btn.Length; i++)
            {
                if (i == 0)
                {
                    btn[i] = new Button(
                     ButtonText[i],
                     new Rectangle(100, 540, 90, 50),
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
                else
                {
                    btn[i] = new Button(
                    ButtonText[i],
                    new Rectangle(500 + (i - 1) * 100, 540, 90, 50),
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
                    break;
                case 1:
                    break;
                case 2:
                    //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[2], 0, 0);
                    append_postCmd(CmdType.ChangePage, 110, 0, 0);
                    break;
                case 3:
                   // append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[3], 0, 0);
                    append_postCmd(CmdType.ChangePage, 104, 0, 0);
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    
                    break;
                case 7:
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
            if (BoolList[UIObj.InBoolList[4]])
            {
                append_postCmd(CmdType.ChangePage, 104, 0, 0);
            }
            if (!BoolList[UIObj.InBoolList[5]])
            {
                append_postCmd(CmdType.ChangePage, 103, 0, 0);
            }
            if (BoolList[UIObj.InBoolList[6]])
            {
                append_postCmd(CmdType.ChangePage, 104, 0, 0);
            }
                _menu_Nr.ForEach(a => a.Paint(g));       
            base.paint(g);
        }
    }
}
