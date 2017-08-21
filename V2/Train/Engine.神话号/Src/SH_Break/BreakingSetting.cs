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
    class BreakingSetting:baseClass 
    {
        /// <summary>
        /// 设置按钮
        /// </summary>
        private Button[] btn = new Button[2];
        private List<Button> _menu_Nr = new List<Button>();
        private List<Image> _images = new List<Image>();
        private string[] line;
        private List<int> id = new List<int>();
        public override string GetInfo()
        {
            return "电空制动设置";
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
            string[] BreakSettingButtonText = new string[] { "维护", "主界面" };
            for (int i = 0; i < btn.Length; i++)
            {               
                btn[i] = new Button(
               BreakSettingButtonText[i],
               new Rectangle(600 + 100 * i, 540, 90, 50),
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
                _menu_Nr.Add(btn[i]);
                btn[i].ClickEvent += btn_ClickEvent;
            }
            return true;
        }
        private int _currentViewID = 0;
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if(nParaA ==2)
            {
                _currentViewID = nParaB;
            }
        }
/// <summary>
/// 设置按钮信息
/// </summary>
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
                     append_postCmd(CmdType.ChangePage, 110, 0, 0);
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[0], 0, 0);
                     break;  
                 case 1:
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[1], 0, 0);
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
        /// <summary>
        /// 电空制动提示
        /// </summary>
        /// <param name="g"></param>
         public void DrawSettingInterface(Graphics g)
         {
            g.FillRectangle(Global.brush2,new Rectangle (4,343,792,174));
            g.DrawString("电空制动设置", Global.font, Global.brush1, new Rectangle(4, 358, 772, 87), Global.SF_CC);
            g.DrawString("请确认BCU面板上钮子开关设置！", Global.font, Global.brush3, new Rectangle(4, 400, 772, 87), Global.SF_CC);
         }
        public override void paint(Graphics g)
        {
            for (int i = 0; i < id.Count; i++)
            {
                if (BoolList[id [i]])
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
            if (BoolList[UIObj.InBoolList[2]])
            {
                append_postCmd(CmdType.ChangePage, 104, 0, 0);
            }
            if (!BoolList[UIObj.InBoolList[3]])
            {
                append_postCmd(CmdType.ChangePage, 103, 0, 0);
            }
            if (BoolList[UIObj.InBoolList[4]])
            {
                append_postCmd(CmdType.ChangePage, 104, 0, 0);
            }
            _menu_Nr.ForEach(a => a.Paint(g));
            DrawSettingInterface(g);
            base.paint(g);

        }
    }
}
