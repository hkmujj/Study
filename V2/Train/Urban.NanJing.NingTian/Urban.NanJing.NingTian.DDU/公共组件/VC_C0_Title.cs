#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-21
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-第一个组件-标题
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Urban.NanJing.NingTian.DDU.Common;
using Urban.NanJing.NingTian.DDU.PIS;
using Urban.NanJing.NingTian.DDU.公共组件;

// ReSharper disable once CheckNamespace
namespace Urban.NanJing.NingTian.DDU
{
    /// <summary>
    /// 功能描述：公共组件-第一个组件-标题
    /// 创建人：唐林
    /// 创建时间：2014-7-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class VC_C0_Title : baseClass
    {
        #region 私有变量
        private int readTxtID = 0;                                     //读取文本标志
        private List<Image> _resource_Image = new List<Image>();               //图片资源
        private SortedDictionary<int, string> _stationList = new SortedDictionary<int, string>();//车站列表
        private List<Label> _labels = new List<Label>();//组件上的文本框列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-标题栏";
        }

        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            LoadStationInfo();
            

            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            //
            for (int i = 0; i < 4; i++)
            {
                Label label = new Label(
                    "ATC",
                    new RectangleF(5 + 102 * (i % 2), 9 + 46 * (i / 2), 100, 44),
                    new LabelStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("Calibri", 16), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC },
                        BorderPen = new Pen(new SolidBrush(Color.Black))
                    },
                    i
                    );
                _labels.Add(label);
            }

            //EB文本框，下标4
            Label label_EB = new Label(
                    "EB",
                    new RectangleF(340 + 54, 11, 54, 38),
                    new LabelStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("Calibri", 20, FontStyle.Bold), TextBrush = new SolidBrush(Color.FromArgb(0, 33, 125)), StringFormat = FontInfo.SF_CC },
                        BorderPen = new Pen(Color.Black)
                    },
                    4
                    );
            _labels.Add(label_EB);

            //故障模块4个文本框下标5-8
            string[] strs = new string[] { "故障等级", "", "故障数目", "" };
            for (int i = 0; i < 4; i++)
            {
                Label label = new Label(
                    strs[i],
                    new RectangleF(461 + 68 * (i % 2), 9 + 46 * (i / 2), 62, 44),
                    new LabelStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("宋体", 11, FontStyle.Bold), TextBrush = new SolidBrush(Color.FromArgb(23, 56, 139)), StringFormat = FontInfo.SF_CC },
                        BackgroundBrush = new SolidBrush(Color.White),
                        BorderPen = new Pen(new SolidBrush(Color.Black))
                    },
                    i + 5
                    );
                _labels.Add(label);
            }

            //站点模块文本框 下标9-12
            string[] strs_ = new string[] { "下一站", "终点站", "", "" };
            for (int i = 0; i < 4; i++)
            {
                Label label = new Label(
                    strs_[i],
                    new RectangleF(598 + 100 * (i % 2), 35 + 35 * (i / 2), 99, 34),
                    new LabelStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("宋体", 11, FontStyle.Bold), TextBrush = new SolidBrush(Color.FromArgb(23, 56, 139)), StringFormat = FontInfo.SF_CC },
                        BackgroundBrush = new SolidBrush(Color.White),
                        BorderPen = new Pen(new SolidBrush(Color.Black))
                    },
                    i + 9
                    );
                _labels.Add(label);
            }

            //时间文本框 下标13
            Label label_Time = new Label(
                    "2000-00-00 00:00:00",
                    new RectangleF(596, 1, 204, 34),
                    new LabelStyle() { FontStyle = new FontStyle_ES() { Font = new Font("Arial", 15, FontStyle.Bold), TextBrush = Brushs.BlackBrush, StringFormat = FontInfo.SF_CC } },
                    13
                    );
            _labels.Add(label_Time);

            return true;
        }

    

        private void LoadStationInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "车站信息.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] split = cStr.Split(new char[] { '\t' });
                int i;
                string[] tmp;
                {
                    tmp = new string[2];
                    i = 0;
                    foreach (string s in split)
                    {
                        if (s.Trim() != "")
                        {
                            if (i < 2)
                                tmp[i] = s;
                            i++;
                        }
                        if (i == 2)
                        {
                            _stationList.Add(int.Parse(tmp[0]), tmp[1]);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取当期视图值
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                string viewName = ((ViewState)nParaB).ToString();
            }
        }

        #endregion

        #region 绘制界面
        public override void paint(Graphics g)
        {
            paint_Frame(g);

            paint_ATC(g);
            paint_Mode(g);
            paint_InfoPointOut(g);
            paint_Temrature(g);
            paint_Speed_Pressure(g);
            paint_EB(g);
            paint_Handler(g);
            paint_Fault(g);

            //下一站文本框
            _labels[11].Text = getStation((int)FloatList[UIObj.InFloatList[0]]) ?? "----";
            //终点站文本框
            _labels[12].Text = getStation((int)FloatList[UIObj.InFloatList[1]]) ?? "----";
            //下标13 时间文本框
            _labels[13].Text = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString();
            _labels.ForEach(a => a.Paint(g));//文本框列表绘制


            switch (V5_PIS_C0_MainView._currentMode)
            {
                case V5_PIS_C0_MainView.PISMode.Hand:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4800, 1, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1] + 4800, 0, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2] + 4800, 0, 0);
                    break;
                case V5_PIS_C0_MainView.PISMode.HalfAuto:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4800, 0, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1] + 4800, 1, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2] + 4800, 0, 0);
                    break;
                case V5_PIS_C0_MainView.PISMode.Auto:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[0] + 4800, 0, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[1] + 4800, 0, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[2] + 4800, 1, 0);
                    break;
            }

            switch (V5_PIS_C0_MainView._currentInterval)
            {
                case V5_PIS_C0_MainView.PISInterval.Front:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[3] + 4800, 1, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[4] + 4800, 0, 0);
                    break;
                case V5_PIS_C0_MainView.PISInterval.Back:
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[3] + 4800, 0, 0);
                    append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[4] + 4800, 1, 0);
                    break;
            }

            base.paint(g);
        }

        /// <summary>
        /// 线框绘制
        /// </summary>
        /// <param name="g"></param>
        private void paint_Frame(Graphics g)
        {
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 2), new Point(0, 1), new Point(800, 1));
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 2), new Point(212, 1), new Point(212, 108));
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 2), new Point(324, 1), new Point(324, 108));
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 2), new Point(456, 1), new Point(456, 108));
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 2), new Point(596, 1), new Point(596, 108));
        }

        /// <summary>
        /// EB绘制
        /// </summary>
        /// <param name="g"></param>
        private void paint_EB(Graphics g)
        {
            Brush[] brushs = new SolidBrush[] {
                new SolidBrush(Color.FromArgb(221, 221, 221)), //紧急制动未知状态，灰色
                new SolidBrush(Color.FromArgb(255, 255, 255)),//未激活紧急制动，白色
                new SolidBrush(Color.FromArgb(255, 0, 0))//激活紧急制动，红色
            };
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    if (_labels[4] != null)
                    {
                        ((LabelStyle)_labels[4].Style).BackgroundBrush = brushs[i];
                        return;
                    }
                }
            }

            ((LabelStyle)_labels[4].Style).BackgroundBrush = null;
        }

        /// <summary>
        /// ATC绘制
        /// </summary>
        /// <param name="g"></param>
        private void paint_ATC(Graphics g)
        {
            Brush[] brushs = new Brush[] { 
                new SolidBrush(Color.FromArgb(0, 255, 0)),
                new SolidBrush(Color.FromArgb(253,153,7)), 
                new SolidBrush(Color.FromArgb(255,0,5)),
                new SolidBrush(Color.FromArgb(178,178,178)) 
            };

            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    ((LabelStyle)_labels[0].Style).BackgroundBrush = brushs[i];
                    return;
                }
            }
            ((LabelStyle)_labels[0].Style).BackgroundBrush = null;
        }

        /// <summary>
        /// 模式绘制
        /// </summary>
        /// <param name="g"></param>
        private void paint_Mode(Graphics g)
        {
            Brush[] brushs = new Brush[] { 
                new SolidBrush(Color.Transparent),
                new SolidBrush(Color.FromArgb(255,255,7)), 
                new SolidBrush(Color.FromArgb(255,255,7)), 
                new SolidBrush(Color.FromArgb(255,255,7)), 
                new SolidBrush(Color.FromArgb(0,255,5)),
                new SolidBrush(Color.FromArgb(0,255,5)),
                Brushes.Orange 
            };
            string[] strs = new string[] { "断开", "洗车", "限速向后", "限速向前", "手动", "ATO", "紧急牵引" };

            for (int i = 0; i < 7; i++)
            {
                if (BoolList[UIObj.InBoolList[3] + i])
                {
                    ((LabelStyle)_labels[1].Style).BackgroundBrush = brushs[i];
                    _labels[1].Text = strs[i];
                    return;
                }
            }
            ((LabelStyle)_labels[1].Style).BackgroundBrush = null;
            _labels[1].Text = strs[0];
        }

        /// <summary>
        /// 信息提示绘制
        /// </summary>
        /// <param name="g"></param>
        private void paint_InfoPointOut(Graphics g)
        {
            ((LabelStyle) _labels[2].Style).FontStyle.Font = new Font("宋体", 13);
            ((LabelStyle) _labels[2].Style).BackgroundBrush = BoolList[UIObj.InBoolList[5]]
                ? null
                : new SolidBrush(Color.FromArgb(255, 255, 7));
            _labels[2].Text = BoolList[UIObj.InBoolList[5]]
                ? "不限速"
                : "<" + FloatList[UIObj.InFloatList[4]].ToString("0.0") + "km/h";
        }

        /// <summary>
        /// 温度绘制
        /// </summary>
        /// <param name="g"></param>
        public void paint_Temrature(Graphics g)
        {
            _labels[3].Text = FloatList[UIObj.InFloatList[5]].ToString("0.0℃");
        }

        /// <summary>
        /// 手柄绘制
        /// </summary>
        /// <param name="g"></param>
        public void paint_Handler(Graphics g)
        {
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    g.DrawImage(_resource_Image[i], new RectangleF(332, 11, 54, 38));
                }
            }
        }

        /// <summary>
        /// 绘制：速度与网压
        /// </summary>
        /// <param name="g"></param>
        private void paint_Speed_Pressure(Graphics g)
        {
            //网压
            g.DrawString(FloatList[UIObj.InFloatList[2]].ToString("0V"), new Font("Arial", 25, FontStyle.Bold),
                new SolidBrush(Color.Black), new Rectangle(325, 55, 130, 44), FontInfo.SF_CC);

            //速度
            g.DrawString(FloatList[UIObj.InFloatList[3]].ToString("0.0"), new Font("Arial", 28, FontStyle.Bold),
                new SolidBrush(Color.Black), new Rectangle(212, 1, 112, 54), FontInfo.SF_CB);
            g.DrawString("km/h", new Font("Arial", 20, FontStyle.Bold), new SolidBrush(Color.Black),
                new Rectangle(212, 55, 112, 44), FontInfo.SF_RC);
        }

        /// <summary>
        /// 故障绘制
        /// </summary>
        /// <param name="g"></param>
        private void paint_Fault(Graphics g)
        {
            //故障数目
            if (VC_C4_GetFault.CurrentFaults != null)
                _labels[8].Text = VC_C4_GetFault.CurrentFaults.Count.ToString();

            //故障等级
            Brush[] brushs = new Brush[] {  
                new SolidBrush(Color.FromArgb(0,255,0)),
                new SolidBrush(Color.FromArgb(255,255,5)),
                new SolidBrush(Color.FromArgb(255, 0, 0))
            };
            string[] strs = new string[] { "轻微", "中等", "严重" };

            //for (int i = 0; i < 3; i++)
            //{
            //if (BoolList[UIObj.InBoolList[4] + i])
            //{
            if (VC_C4_GetFault.CurrentFault != null)
            {
                _labels[6].Text = strs[VC_C4_GetFault.CurrentFault.Grade];
                ((LabelStyle)_labels[6].Style).BackgroundBrush = brushs[VC_C4_GetFault.CurrentFault.Grade];
                return;
            }
            //}
            //}
            _labels[6].Text = null;
            ((LabelStyle)_labels[6].Style).BackgroundBrush = null;
        }

        /// <summary>
        /// 获取站台信息
        /// </summary>
        /// <param name="stationNum"></param>
        /// <returns></returns>
        private string getStation(int stationNum)
        {
            string stationName = string.Empty;
            _stationList.TryGetValue(stationNum, out stationName);

            return stationName;
        }
        #endregion
    }
}
