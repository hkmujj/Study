#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-3
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图104-运行-站点设置-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using ES.Facility.PublicModule.Memo;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using NC_TMS.Common;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图104-运行-站点设置-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-3
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V104_Run_StationSet_C0_MainView : baseClass
    {
        #region 稀有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        //private Int32 _readInfoId = 0;//读取文本标志
        private List<Button> _btns_BroadSet = new List<Button>();//功能按钮列表
        private List<Button> _btns_Mode = new List<Button>();
        //private List<List<String>> _list_Stations = new List<List<String>>();//站点列表
        private List<Button> _btn_Stations = new List<Button>();//站点按钮列表
        private Boolean _isFirstSet = true;//是否第一次设置
        private List<TextBox> _textBoxs_Station_F_T = new List<TextBox>();//文本输入框列表
        private Button _btn_Set;//设置按钮
        private Dictionary<Int32, String> _stations = new Dictionary<int, string>();
        private String BoradMode = "----";
        #endregion

        #region 属性
        /// <summary>
        /// 设置当前线路属性（用于根据线路绘制站点按钮）
        /// </summary>
        public Int32 CurrentRoadID
        {
            set
            {
                //if (this._currentRoadID == value)
                //    return;

                //this._currentRoadID = value;
                //this._btn_Stations.Clear();

                //for (int i = 0; i < _list_Stations[value].Count; i++)
                //{
                //    Button btn_ = new Button(
                //        this._list_Stations[value][i],
                //        new RectangleF(28 + 96 * (i % 7), 172 + 52 * (i / 7), 89, 47),
                //        i,
                //        new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] },
                //        false
                //        );
                //    btn_.ClickEvent += btn__ClickEvent;
                //    this._btn_Stations.Add(btn_);
                //}
            }
        }
        //private Int32 _currentRoadID = -1;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行（站点设置）试图-站点设置";
        }

        /// <summary>
        /// 初始化函数：导入图片、创建控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            string[] strs = File.ReadAllLines(Path.Combine(RecPath, "..\\config\\车站信息.txt"),System.Text.Encoding.Default);
            for (int i = 0; i < strs.Length; i++)
            {
                String[] strs_=strs[i].Split(':');
                _stations.Add(Convert.ToInt32(strs_[0]), strs_[1]);
            }

            String[] str_BroadSet = new String[] { "半自动\n模式", "手动\n模式", "", "", "", "", "", "返回" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    str_BroadSet[i],
                    new RectangleF(715, 100 + 56.25F * i, 83, 54.25F),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[6], DownImage = _resource_Image[5]},
                    false
                    );
                
                btn.ClickEvent+=btnMode_ClickEvent;
                _btns_Mode.Add(btn);
                _btns_Mode[0].IsReplication = false;
                //this._btns_BroadSet.Add(btn);
            }
            
            for (int i = 2; i < 8; i++)
            {
                Button btn = new Button(
                    str_BroadSet[i],
                    new RectangleF(715, 100 + 56.25F * i, 83, 54.25F),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[i < 2 ? 5 : 0], DownImage = _resource_Image[i < 2 ? 6 : 1] }
                    );
                //if (i == 0 || i == 1) btn.Enable = false;
                btn.ClickEvent += btn_ClickEvent;
                this._btns_BroadSet.Add(btn);
            }

            for (int i = 0; i < 3; i++)
            {
                TextBox textBox = new TextBox(
                    new RectangleF(110 + 210 * i, 108, 130, 45),
                    new TextBoxStyle(){ FontStyle=FontStyles.FS__Song_105_LC_W, Background=_resource_Image[3], BackgroundFocus=_resource_Image[4]},
                    i
                    );
                if (i == 1) textBox.Enable = false;
                textBox.ClickEvent += textBox_ClickEvent;
                this._textBoxs_Station_F_T.Add(textBox);
            }

            this._btn_Set = new Button(
                "设定",
                new RectangleF(601, 490, 89, 47),
                0,
                new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]}
                );
            //this._btn_Set.Enable = false;
            this._btn_Set.ClickEvent += _btn_Set_ClickEvent;

            foreach (var item in _stations)
            {
                Button btn_ = new Button(
                    item.Value,
                    new RectangleF(28 + 96 * (_btn_Stations.Count % 7), 172 + 52 * (_btn_Stations.Count / 7), 89, 47),
                    item.Key,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]},
                    false
                    );
                btn_.ClickEvent += btn__ClickEvent;
                this._btn_Stations.Add(btn_);
            }

            //for (int i = 0; i < _stations.Count; i++)
            //{
            //    Button btn_ = new Button(
            //        this._list_Stations[value][i],
            //        new RectangleF(28 + 96 * (i % 7), 172 + 52 * (i / 7), 89, 47),
            //        i,
            //        new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] },
            //        false
            //        );
            //    btn_.ClickEvent += btn__ClickEvent;
            //    this._btn_Stations.Add(btn_);
            //}

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (nParaB == 104)
                {
                    BoradMode = VC_C0_Title.BoardMode;
                }
            }
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            this._btns_BroadSet.ForEach(a => a.MouseDown(nPoint));
            this._btn_Stations.ForEach(a => a.MouseDown(nPoint));
            this._textBoxs_Station_F_T.ForEach(a => a.MouseDown(nPoint));
            this._btn_Set.MouseDown(nPoint);
            _btns_Mode.ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            this._btns_BroadSet.ForEach(a => a.MouseUp(nPoint));
            this._btn_Stations.ForEach(a => a.MouseUp(nPoint));
            this._btn_Set.MouseUp(nPoint);
            _btns_Mode.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// TextBox空间获取焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void textBox_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            this._textBoxs_Station_F_T.FindAll(a => a.ID != e.Message).ForEach(b => b.IsFocus = false);
        }

        /// <summary>
        /// 站点按钮点击事件响应函数：站点选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn__ClickEvent(object sender, ClickEventArgs<int> e)
        {
            TextBox textBox = this._textBoxs_Station_F_T.Find(a => a.IsFocus);

            if (textBox != null)
            {
                Button btn = this._btn_Stations.Find(a => a.Text == textBox.Text);
                if (btn != null)
                    btn.IsReplication = true;

                textBox.Text = _stations[e.Message];
                if (textBox.ID == 0)
                {
                    this._textBoxs_Station_F_T[1].Enable = true;
                    this._textBoxs_Station_F_T[1].IsFocus = true;
                }
                else if (textBox.ID == 1)
                {
                    this._textBoxs_Station_F_T[2].Enable = true;
                    this._textBoxs_Station_F_T[2].IsFocus = true;
                }
                else
                {
                    //this._btn_Set.Enable = true;
                }
                textBox.IsFocus = false;
            }
            else
            {
                foreach (var item in this._textBoxs_Station_F_T)
                {
                    if (this._btn_Stations.Find(a => a.ID == e.Message).Text == item.Text)
                    {
                        this._btn_Stations.Find(a => a.ID == e.Message).IsReplication = false;
                        return;
                    }
                }
                this._btn_Stations.Find(a => a.ID == e.Message).IsReplication = true;
            }
        }

        /// <summary>
        /// 右侧菜单按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnMode_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            _btns_Mode.ForEach(a => a.IsReplication = true);
            _btns_Mode[e.Message].IsReplication = false;
            switch (e.Message)
            {
                case 0://半自动模式按钮
                    BoradMode = "半自动模式";
                    break;
                case 1://手动模式按钮
                    BoradMode = "手动模式";
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 右侧菜单按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 7://返回按钮
                    if (VC_C2_GetValue.IsHandleBroadMode)
                    {
                        _btns_Mode[0].IsReplication = true;
                        _btns_Mode[1].IsReplication = false;
                    }
                    else
                    {
                        _btns_Mode[1].IsReplication = true;
                        _btns_Mode[0].IsReplication = false;
                    }
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (Int32)ViewState.运行, 0, 0);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 设定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _btn_Set_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            VC_C2_GetValue.IsHandleBroadMode = _btns_Mode[0].IsReplication;
            VC_C0_Title.BoardMode = BoradMode;

            if (_btn_Stations.Find(a => a.Text == this._textBoxs_Station_F_T[0].Text) != null)
                VC_C2_GetValue.StartStationID = this._btn_Stations.Find(a => a.Text == this._textBoxs_Station_F_T[0].Text).ID + 1;
            if (_btn_Stations.Find(a => a.Text == this._textBoxs_Station_F_T[1].Text) != null)
                VC_C2_GetValue.CurrentStationID = this._btn_Stations.Find(a => a.Text == this._textBoxs_Station_F_T[1].Text).ID + 1;
            if (_btn_Stations.Find(a => a.Text == this._textBoxs_Station_F_T[2].Text) != null)
                VC_C2_GetValue.EndStationID = this._btn_Stations.Find(a => a.Text == this._textBoxs_Station_F_T[2].Text).ID + 1;

        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            if (this._isFirstSet)
            {
                //this.CurrentRoadID = 0;
                this._isFirstSet = false;
            }

            this._btns_BroadSet.ForEach(a => a.Paint(dcGs));
            this._btn_Stations.ForEach(a => a.Paint(dcGs));
            this._textBoxs_Station_F_T.ForEach(a => a.Paint(dcGs));
            this._btn_Set.Paint(dcGs);
            _btns_Mode.ForEach(a => a.Paint(dcGs));

            this.paint_Frame(dcGs);
            this.paint_Station_F_T(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制线框
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(5, 96, 707, 454));
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(709, 96, 91, 454));
            dcGs.DrawRectangle(new Pen(Brushs.WhiteBrush), new Rectangle(14, 167, 688, 379));
        }

        /// <summary>
        /// 绘制始发站与终点站
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Station_F_T(Graphics dcGs)
        {
            dcGs.DrawString(
                "始发站：",
                new Font("宋体", 10.5f),
                Brushs.WhiteBrush,
                new RectangleF(57, 108, 160, 45),
                FontInfo.SF_LC
                );

            dcGs.DrawString(
                "当前站：",
                new Font("宋体", 10.5f),
                Brushs.WhiteBrush,
                new RectangleF(267, 108, 160, 45),
                FontInfo.SF_LC
                );

            dcGs.DrawString(
                "终点站：",
                new Font("宋体", 10.5f),
                Brushs.WhiteBrush,
                new RectangleF(477, 108, 160, 45),
                FontInfo.SF_LC
                );
        }
        #endregion
    }
}
