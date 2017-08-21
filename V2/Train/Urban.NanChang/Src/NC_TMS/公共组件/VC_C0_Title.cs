#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-1
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
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using ES.Facility.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using NC_TMS.Common;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：公共组件-第一个组件-标题
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class VC_C0_Title : baseClass
    {
        #region 私有变量
        private Int32 readTxtID = 0;                                     //读取文本标志
        private String _title = String.Empty;                             //标题
        private Button _btn_Fault;                                        //故障按钮
        private List<Image> _resource_Image = new List<Image>();               //图片资源
        private SortedDictionary<Int32, String> _stationList = new SortedDictionary<int, string>();//车站列表
        #endregion

        public static String BoardMode = "----";

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
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath ,a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            string[] strs = File.ReadAllLines(Path.Combine(RecPath, "..\\config\\车站信息.txt"), System.Text.Encoding.Default);
            for (int i = 0; i < strs.Length; i++)
            {
                String[] strs_ = strs[i].Split(':');
                _stationList.Add(Convert.ToInt32(strs_[0]), strs_[1]);
            }

            this._btn_Fault = new Button(
                "",
                new RectangleF(730, 30, 63, 63),
                0,
                new ButtonStyle()
                {
                    FontStyle = FontStyles.FS_Song_11_CC_B,
                    Background = _resource_Image[0],
                    DownImage = _resource_Image[1]
                }
                );
            this._btn_Fault.ClickEvent += _btn_Fault_ClickEvent;
            return true;
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
                String viewName = ((ViewState)nParaB).ToString();
                this._title = viewName.Split(new Char[] { '_' })[0];
            }
        }

        ///// <summary>
        ///// 设置读取文本标志
        ///// </summary>
        ///// <param name="nPara"></param>
        ///// <returns></returns>
        //public override bool canSetStringList(int nPara)
        //{
        //    if (nPara == 1)
        //    {
        //        readTxtID = 1;
        //        return true;
        //    }
        //    else if (nPara == 2)
        //    {
        //        readTxtID = 2;
        //        return true;
        //    }
        //    else
        //    {
        //        readTxtID = 0;
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 获取文本信息：车站信息
        ///// </summary>
        ///// <param name="nIndex"></param>
        ///// <param name="cStr"></param>
        //public override void addStringByLine(int nIndex, string cStr)
        //{
        //    String[] split = cStr.Split(new char[] { '\t' });
        //    int i;
        //    String[] tmp;
        //    if (readTxtID == 1)
        //    {
        //        tmp = new String[2];
        //        i = 0;
        //        foreach (string s in split)
        //        {
        //            if (s.Trim() != "")
        //            {
        //                if (i < 2)
        //                    tmp[i] = s;
        //                i++;
        //            }
        //            if (i == 2)
        //            {
        //                _stationList.Add(int.Parse(tmp[0]), tmp[1]);
        //                return;
        //            }
        //        }
        //    }
        //}
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            this._btn_Fault.MouseDown(nPoint);
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            this._btn_Fault.MouseUp(nPoint);
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 故障按钮点击事件：切换到故障处理视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _btn_Fault_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (VC_C4_GetFault.CurrentFault == null)
                return;

            append_postCmd(CmdType.ChangePage, 101, 0, 0);
        }
        #endregion

        #region 绘制界面
        public override void paint(Graphics dcGs)
        {
            this.paint_Up_SystemInfo(dcGs);
            this.paint_Fault(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制：系统信息（主界面上部分）
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Up_SystemInfo(Graphics dcGs)
        {
            //文本垂直居中和水平居中设置
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            //编组
            dcGs.DrawString("编组：", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(15, 0, 60, 35), sf);
            dcGs.DrawString(FloatList[UIObj.InFloatList[0]].ToString(), new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(71, 9, 50, 30));

            //标题
            dcGs.DrawString(this._title, new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(0, 0, 800, 35), sf);

            //广播模式
            //String[] str_ = new String[] { "全自动模式", "半自动模式", "手动模式", "----", "----" };
            dcGs.DrawString("广播模式：", new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(610, 0, 90, 35), FontInfo.SF_RC);
            //for (int i = 0; i < 5; i++)
            //{
            //    if (BoolList[UIObj.InBoolList[0] + i])
            //    {
            //        dcGs.DrawString(str_[i], new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(700, 0, 100, 35), FontInfo.SF_LC);
            //    }
            //}
            dcGs.DrawString(BoardMode, new Font("宋体", 11), new SolidBrush(Color.White), new RectangleF(700, 0, 100, 35), FontInfo.SF_LC);

            //左上6个模块
            Rectangle[] rects = new Rectangle[6];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rects[i * 3 + j] = new Rectangle(5 + j * 135, 30 + i * 33, 130, 30);
                }
            }
            for (int i = 0; i < 6; i++)
            {
                dcGs.FillRectangle(new SolidBrush(Color.FromArgb(92, 92, 104)), rects[i]);
            }

            //日期与时间
            dcGs.DrawString(DateTime.Now.ToString("yyyy-MM-dd"), new Font("宋体", 10), new SolidBrush(Color.White), rects[0], sf);
            dcGs.DrawString(DateTime.Now.ToLongTimeString(), new Font("宋体", 10), new SolidBrush(Color.White), rects[3], sf);

            //当前站
            dcGs.DrawString("当前站", new Font("宋体", 11, FontStyle.Bold), new SolidBrush(Color.White), rects[1], sf);
            String station = String.Empty;
            dcGs.DrawString(
                getStation((Int32)FloatList[UIObj.InFloatList[1]], station) ?? "----",
                new Font("宋体", 11, FontStyle.Bold), new SolidBrush(Color.White), rects[4], sf
                );

            //终点站
            dcGs.DrawString("终点站", new Font("宋体", 11, FontStyle.Bold), new SolidBrush(Color.White), rects[2], sf);
            dcGs.DrawString(
                getStation((Int32)FloatList[UIObj.InFloatList[2]], station) ?? "----",
                new Font("宋体", 11, FontStyle.Bold), new SolidBrush(Color.White), rects[5], sf
                );

            //网压
            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(92, 92, 104)), new Rectangle(410, 30, 130, 63));
            dcGs.DrawString(FloatList[UIObj.InFloatList[3]].ToString(), new Font("宋体", 15, FontStyle.Bold),
                new SolidBrush(Color.FromArgb(28, 205, 32)), new Rectangle(410, 30, 130, 63), sf);
            dcGs.DrawString("V", new Font("宋体", 15), new SolidBrush(Color.White), new Rectangle(540, 63, 30, 30), sf);

            //速度
            dcGs.FillRectangle(new SolidBrush(Color.FromArgb(92, 92, 104)), new Rectangle(570, 30, 105, 63));
            dcGs.DrawString(FloatList[UIObj.InFloatList[4]].ToString("0.0"), new Font("宋体", 15, FontStyle.Bold),
                new SolidBrush(Color.FromArgb(28, 205, 32)), new Rectangle(570, 30, 105, 63), sf);
            dcGs.DrawString("km/h", new Font("宋体", 15), new SolidBrush(Color.White),
                new Rectangle(670, 63, 60, 30), sf);
        }

        /// <summary>
        /// 绘制故障按钮
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Fault(Graphics dcGs)
        {
            this._btn_Fault.Paint(dcGs);

            //无故障
            if (VC_C4_GetFault.CurrentFault == null)
            {
                this._btn_Fault.Text = String.Empty;
                this._btn_Fault.Style.Background = _resource_Image[0];
                ((ButtonStyle)this._btn_Fault.Style).DownImage = _resource_Image[0];
                return;
            }

            //根据最高优先级故障设置按钮的背景图片与文本属性
            switch (VC_C4_GetFault.CurrentFault.Grade)
            {
                case 0:
                    this._btn_Fault.Text = "轻微";
                    this._btn_Fault.Style.Background = _resource_Image[2];
                    ((ButtonStyle)this._btn_Fault.Style).DownImage = _resource_Image[1];
                    break;
                case 1:
                    this._btn_Fault.Text = "中级";
                    this._btn_Fault.Style.Background = _resource_Image[2];
                    ((ButtonStyle)this._btn_Fault.Style).DownImage = _resource_Image[1];
                    break;
                case 2:
                    this._btn_Fault.Text = "严重";
                    this._btn_Fault.Style.Background = _resource_Image[2];
                    ((ButtonStyle)this._btn_Fault.Style).DownImage = _resource_Image[1];
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取站台信息
        /// </summary>
        /// <param name="stationNum"></param>
        /// <param name="stationName"></param>
        /// <returns></returns>
        private String getStation(int stationNum, String stationName)
        {
            if (stationNum < 0)
                return null;

            _stationList.TryGetValue(stationNum, out stationName);

            return stationName;
        }
        #endregion
    }
}
