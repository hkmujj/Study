using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using HD_HXD2_TMS.Common;
using MMI.Facility.Interface.Data;

namespace HD_HXD2_TMS.V2控制
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_V3_FunctionBtns : baseClass
    {
        public static List<Button> Btns = new List<Button>();
        private static Int32[] _viewIDs = null;
        private List<Image> _images = new List<Image>(); //图片资源

        public override string GetInfo()
        {
            return "机车信息界面-功能按钮";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    _images.Add(Image.FromStream(fs));
                }
            });

            String[] strs = {"隔离", "受电弓", "运行测试", "距离计数"};
            _viewIDs = new []
            {
                (Int32) ViewType.ControlInsulateA, (Int32) ViewType.ControlBowA,
                (Int32) ViewType.ControlRunTest, (Int32) ViewType.ControlDistance
            };
            for (int i = 0; i < 4; i++)
            {
                Button btn = new Button(
                    strs[i],
                    new RectangleF(8 + 99*i, 44, 98, 42),
                    _viewIDs[i],
                    new ButtonStyle()
                    {
                        Background = _images[0],
                        DownImage = _images[1],
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("宋体", 13),
                            TextBrush = (SolidBrush) Brushes.White,
                            StringFormat = new StringFormat()
                            {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            }
                        }
                    },
                    false
                    );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                Btns.Add(btn);
            }
            Btns[0].IsReplication = false;

            return true;
        }

        public override bool mouseDown(Point nPoint)
        {
            if (!HD_V6_FunctionBtns.IsShow)
            Btns.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            if (!HD_V6_FunctionBtns.IsShow)
            Btns.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                Btns.FindAll(a => a.ID != nParaB).ForEach(b => b.IsReplication = true);
                Button btn = Btns.Find(a => a.ID == nParaB);
                if (btn != null)
                {

                    btn.ID = nParaB;
                    btn.IsReplication = false;
                }
                else
                {
                    if (nParaB == (Int32) ViewType.ControlInsulateB)
                    {
                        Btns[0].ID = (Int32) ViewType.ControlInsulateB;
                        Btns[0].IsReplication = false;
                    }
                    else if (nParaB == (Int32)ViewType.ControlInsulateA)
                    {
                        Btns[0].ID = (Int32)ViewType.ControlInsulateA;
                        Btns[0].IsReplication = false;
                    }
                    else if (nParaB == (Int32) ViewType.ControlBowB)
                    {
                        Btns[1].ID = (Int32) ViewType.ControlBowB;
                        Btns[1].IsReplication = false;
                    }
                    else if (nParaB == (Int32)ViewType.ControlBowA)
                    {
                        Btns[1].ID = (Int32)ViewType.ControlBowA;
                        Btns[1].IsReplication = false;
                    }
                }
            }
        }

        public override void paint(Graphics dcGs)
        {
            Btns.ForEach(a => a.Paint(dcGs));

            for (int i = 4; i < 8; i++)
            {
                dcGs.DrawRectangle(Pens.White, new Rectangle(8 + 99*i, 44, 98, 42));
            }

            base.paint(dcGs);
        }

        private void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            Btns.ForEach(a => a.IsReplication = true);
        }

        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            VC公共组件.HD_VC_FalutManager.IsShowCurrentView = false;
            VC公共组件.HD_VC_FalutManager.IsShowCurrentAView = false;
            VC公共组件.HD_VC_FalutManager.IsShowCurrentBView = false;

            append_postCmd(CmdType.ChangePage, e.Message, 0, 0);
        }

        public static void Reset()
        {
            if (Btns.Count == 0||_viewIDs==null||_viewIDs.Length==0) return;

            for (int i = 0; i < Btns.Count; i++)
            {
                Btns[i].ID = _viewIDs[i];
            }
        }
    }
}
