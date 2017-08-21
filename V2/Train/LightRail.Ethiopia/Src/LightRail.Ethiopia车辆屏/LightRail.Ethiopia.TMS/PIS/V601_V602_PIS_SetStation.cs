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
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using LightRail.Ethiopia.TMS.Common;
using LightRail.Ethiopia.TMS.Control;
using LightRail.Ethiopia.TMS.Control.Common;

namespace LightRail.Ethiopia.TMS.PIS
{
    /// <summary>
    /// 功能描述：视图104-运行-站点设置-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-3
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V601_V602_PIS_SetStation : baseClass
    {
        #region 稀有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        //private Int32 _readInfoId = 0;//读取文本标志
        private List<Button> _btns = new List<Button>();//功能按钮列表
        //private List<List<String>> _list_Stations = new List<List<String>>();//站点列表
        private List<Button> _btn_Stations = new List<Button>();//站点按钮列表
        private Boolean _isFirstSet = true;//是否第一次设置
        private ViewState _currentViewState;//当前视图
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            String[] strs = new[] { "OK", "QUIT" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    strs[i],
                    new RectangleF(588 + 101 * i, 555, 89, 38),
                    i,
                     new ButtonStyle()
                     {
                         FontStyle = new FontStyle_ES()
                         {
                             Font = new Font("Verdana", 11),
                             TextBrush = new SolidBrush(Color.Yellow),
                             StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                         },
                         Background = _resource_Image[2],
                         DownImage = _resource_Image[3]
                     }
                );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            return true;
        }

        ///// <summary>
        ///// 获取到当前运行视图：根据当前视图设置按钮状态
        ///// </summary>
        ///// <param name="nParaA"></param>
        ///// <param name="nParaB"></param>
        ///// <param name="nParaC"></param>
        //public override void setRunValue(int nParaA, int nParaB, float nParaC)
        //{
        //    if (nParaA == 2)
        //    {
        //        _currentViewState = (ViewState)nParaB;
        //    }
        //}

        //public override bool canSetStringList(int nPara)
        //{
        //    if (nPara == 3)//线路信息
        //    {
        //        _readInfoId = 3;
        //        return true;
        //    }
        //    else return false;
        //}

        ///// <summary>
        ///// 读取站点信息
        ///// </summary>
        ///// <param name="nIndex"></param>
        ///// <param name="cStr"></param>
        //public override void addStringByLine(int nIndex, string cStr)
        //{
        //    string[] split = cStr.Split('\t');
        //    switch (_readInfoId)
        //    {
        //        case 3:
        //            if (split.Length == 2)
        //            {
        //                string tmpStations = split[1];
        //                string tmpStr = string.Empty;
        //                string outTmp = string.Empty;
        //                if (StringHelper.findStringByKey(tmpStations, "[", "]", ref tmpStr))
        //                {
        //                    outTmp = tmpStr.Trim();
        //                }

        //                if (outTmp != string.Empty)
        //                {
        //                    this._list_Stations.Add((outTmp.Split('-')).ToList());
        //                }
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}

        public override bool mouseDown(Point nPoint)
        {
            _btn_Stations.ForEach(a => a.MouseDown(nPoint));
            _btns.ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btn_Stations.ForEach(a => a.MouseUp(nPoint));
            _btns.ForEach(a => a.MouseUp(nPoint));

            return base.mouseUp(nPoint);
        }

        public override void paint(Graphics dcGs)
        {
            if (_isFirstSet)
            {
                for (int i = 0; i < V6_PIS_MianView._list_Stations[(int)V6_PIS_MianView._currentUpDown].Count; i++)
                {
                    Button btn_ = new Button(
                        V6_PIS_MianView._list_Stations[(int)V6_PIS_MianView._currentUpDown][i],
                        new RectangleF(49 + 251 * (i % 3), 129 + 54 * (i / 3), 202, 38),
                        i,
                        new ButtonStyle()
                        {
                            FontStyle = new FontStyle_ES()
                            {
                                Font = new Font("Verdana", 11),
                                TextBrush = new SolidBrush(Color.Yellow),
                                StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                            },
                            Background = _resource_Image[0],
                            DownImage = _resource_Image[1]
                        },
                        false
                        );
                    btn_.MouseDownEvent += btn_Station_MouseDownEvent;
                    btn_.ClickEvent += btn_Station_ClickEvent;
                    _btn_Stations.Add(btn_);
                }

                _isFirstSet = false;
            }

            paint_StationLine(dcGs);
            _btn_Stations.ForEach(a => a.Paint(dcGs));
            _btns.ForEach(a => a.Paint(dcGs));
            paint_Frame(dcGs);

            base.paint(dcGs);
        }

        #endregion
        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)_btns[e.Message].Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
        }

        /// <summary>
        /// 站点按钮点击事件响应函数：站点选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)_btns[e.Message].Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
            switch (e.Message)
            {
                case 0:
                    Button btn = _btn_Stations.Find(a => !a.IsReplication);
                    if (btn != null)
                    {
                        if (V6_PIS_MianView.S_T_State == 0)
                        {
                            V6_PIS_MianView.StartingStation = btn.ID;
                        }
                        else if(V6_PIS_MianView.S_T_State == 1)
                        {
                            V6_PIS_MianView.TerminalStation = btn.ID;
                        }
                    }
                    break;
                case 1:
                    append_postCmd(CmdType.ChangePage, 6,0,0);
                    break;
            }
        }

        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Station_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)_btn_Stations[e.Message].Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
        }

        /// <summary>
        /// 站点按钮点击事件响应函数：站点选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Station_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            _btn_Stations.FindAll(a => a.ID != e.Message).ForEach(b =>
            {
                b.IsReplication = true;
                ((ButtonStyle)b.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);

            });
            _btn_Stations[e.Message].IsReplication = false;
        }

        /// <summary>
        /// 绘制框架
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.FillRectangle(Brushes.Yellow, new RectangleF(0, 78, 800, 37));
            dcGs.DrawString(
                _currentViewState == ViewState.PIS_StartStation ? "Starting Station Set" : "Terminal Station Set",
               new Font("Verdana", 11),
               Brushes.Black,
               new RectangleF(0, 78, 800, 37),
               new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
               );

            dcGs.DrawString(
               "Please select a atation and press the 'OK' button.",
               new Font("Verdana", 11),
               Brushes.Yellow,
               new RectangleF(0, 555, 566, 45),
               new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
               );
        }

        /// <summary>
        /// 绘制站台之间的连接线
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_StationLine(Graphics dcGs)
        {
            for (int i = 0; i < _btn_Stations.Count - 1; i++)
            {
                dcGs.FillRectangle(
                    new SolidBrush(Color.Yellow),
                    new Rectangle(49 + 202 + 251 * (i % 3), 129 + 13 + 54 * (i / 3), 49, 12)
                    );
            }
        }
    }
}
