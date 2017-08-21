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
    class MaintenanceSetting : baseClass 
    {
        /// <summary>
        /// 设置按钮
        /// </summary>
        private Button[] btn = new Button[7];
         private List<Button> _menu_Nr = new List<Button>();
        private List<Image> _images = new List<Image>();
        private string[] line;
        private List<int> id = new List<int>();
        public override string GetInfo()
        {
            return "维护设置";
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
            string[] MaintenanceButtonText = new string[] { "单机"+System.Environment.NewLine+"自检", "事件"+System.Environment.NewLine+"记录", "泄露"+System.Environment.NewLine+"试验",
            "列车"+System.Environment.NewLine+"贯通", "传感器"+System.Environment.NewLine+"校准", "返回", "主界面" };

            for (int i = 0; i < btn.Length; i++)
            {
                if (i < 5)
                {
                    btn[i] = new Button(
               MaintenanceButtonText[i],
               new Rectangle(0 + 100 * i, 540, 90, 50),
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
                    if (i == 4)
                    {
                        btn[i] = new Button(
               MaintenanceButtonText[i],
               new Rectangle(0 + 100 * i, 540, 90, 50),
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

                }
                else
                {
                    btn[i] = new Button(
               MaintenanceButtonText[i],
               new Rectangle(600 + 100 * (i - 5), 540, 90, 50),
               i,
               new ButtonStyle()
               {
                   Background = _images[0],
                   DownImage = _images[1],
                   FontStyle = new FontStyle_ES()
                   {
                       Font = Global.Font_Arial_13_B,
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
                     append_postCmd(CmdType.ChangePage, 112, 0, 0);
                     break;
                 case 1:
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[1], 0, 0);
                     append_postCmd(CmdType.ChangePage, 113, 0, 0);
                     break;
                 case 2:
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[2], 0, 0);
                     append_postCmd(CmdType.ChangePage, 114, 0, 0);
                     break;
                 case 3:
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[3], 0, 0);
                     append_postCmd(CmdType.ChangePage, 115, 0, 0);
                     break;
                 case 4:
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[4], 0, 0);
                     append_postCmd(CmdType.ChangePage, 116, 0, 0);
                     break;
                 case 5:
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[5], 0, 0);
                     append_postCmd(CmdType.ChangePage, 109, 0, 0);
                     break;
                 case 6:
                     //append_postCmd(CmdType.SetBoolValue, UIObj.InBoolList[6], 0, 0);
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
        /// 功能提示
        /// </summary>
        /// <param name="g"></param>
         public void DrawText(Graphics g)
         {
             string[] ButtonText = new string[] {"单机自检","事件记录","泄露试验","列车贯通","传感器校准" };
             for (int i = 0; i < ButtonText.Length; i++)
             {
                 g.DrawString(ButtonText[i], Global.font, Global.brush2, new Rectangle(100, 200+50*i, 100, 30), Global.SF_NC);
             }
             string[] FuncText = new string[] {"维护菜单","执行单机自检测试","显示事件/故障记录","执行泄露试验","执行列车贯通试验","执行传感器校准" };
             for (int i = 0; i<FuncText.Length; i++)
             {
                 g.DrawString(FuncText[i], Global.font, Global.brush1, new Rectangle(300, 150+50*i, 200, 30), Global.SF_NC);
             }
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
             if (BoolList[UIObj.InBoolList[7]])
             {
                 append_postCmd(CmdType.ChangePage, 104, 0, 0);
             }
             if (!BoolList[UIObj.InBoolList[8]])
             {
                 append_postCmd(CmdType.ChangePage, 103, 0, 0);
             }
             if (BoolList[UIObj.InBoolList[9]])
             {
                 append_postCmd(CmdType.ChangePage, 104, 0, 0);
             }
               _menu_Nr.ForEach(a => a.Paint(g));
             DrawText(g);
             base.paint(g);

         }
    }
}
