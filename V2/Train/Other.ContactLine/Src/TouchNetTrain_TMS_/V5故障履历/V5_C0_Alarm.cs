using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace TouchNetTrain_TMS_.故障履历
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V5_C0_Alarm : baseClass
    {
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private List<Button> _btns = new List<Button>();//按钮列表
        private List<FaultInfo> _faluts = new List<FaultInfo>();
        private int _currentPage = 0;
        private int _currentFalutID = 0;
        private Boolean _isCurrent = true;

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "故障履历-主界面";
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Button btn = new Button(
                        "",
                        new RectangleF(710, 121 + j * 54 + i * 158, 40, 41),
                        i * 2 + j,
                        new ButtonStyle()
                        {
                            FontStyle = new FontStyle_ES()
                            {
                                Font = Global.Font_Verdana_12_B,
                                TextBrush = new SolidBrush(Color.Black),
                                StringFormat =
                                    new StringFormat()
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Center
                                    }
                            },
                            Background = _resource_Image[(i * 2 + j) * 2],
                            DownImage = _resource_Image[(i * 2 + j) * 2 + 1]
                        }
                        );
                    btn.MouseDownEvent += btn_MouseDownEvent;
                    btn.ClickEvent += btn_ClickEvent;
                    _btns.Add(btn);
                }
            }

            String[] strs = { "确定", "历史" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                        strs[i],
                        new RectangleF(684, 405 + i * 65, 87, 40),
                        4 + i,
                        new ButtonStyle()
                        {
                            FontStyle = new FontStyle_ES()
                            {
                                Font = Global.Font_Verdana_12_B,
                                TextBrush = new SolidBrush(Color.Black),
                                StringFormat =
                                    new StringFormat()
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Center
                                    }
                            },
                            Background = _resource_Image[8],
                            DownImage = _resource_Image[9]
                        }
                        );
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            return true;
        }

        /// <summary>
        /// 获取到当前运行视图：根据当前视图设置按钮状态
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (nParaB == 4)//故障界面
                {
                    _currentPage = 0;
                    _currentFalutID = 0;
                    _isCurrent = true;
                }
            }
        }

        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btns.ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns.ForEach(a => a.MouseUp(nPoint));

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://上一条
                    if (_currentFalutID > 0) _currentFalutID--;
                    break;
                case 1://上一页
                    if (_currentPage > 0)
                    {
                        _currentPage--;
                        _currentFalutID = 0;
                    }
                    break;
                case 2://下一页
                    if (_faluts != null &&
                        (_currentPage + 1) * 11 < _faluts.Count - 1)
                    {
                        _currentPage++;
                        _currentFalutID = 0;
                    }
                    break;
                case 3://下一条
                    if (getShowFalutList() != null && _currentFalutID < getShowFalutList().Count - 1) _currentFalutID++;
                    break;
                case 4://确定
                    break;
                case 5://历史
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            _faluts = _isCurrent ? VC_C2_GetAlarm.CurrentFaults : VC_C2_GetAlarm.AllFaults;

            _btns.ForEach(a => a.Paint(dcGs));
            paint_Frame(dcGs);
            paint_Alarms(dcGs, getShowFalutList());
            paint_SelectFalut(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 绘制线框
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Frame(Graphics dcGs)
        {
            dcGs.DrawRectangle(Global.Pen_White_2, new Rectangle(11, 95, 653, 440));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<FaultInfo> getShowFalutList()
        {
            if (_faluts == null || _faluts.Count == 0)
                return null;
            if (_currentPage * 11 >= _faluts.Count)
                return null;

            List<FaultInfo> faults = new List<FaultInfo>();
            for (int i = _currentPage * 11; i < _currentPage * 11 + 11; i++)
            {
                if (i >= _faluts.Count) break;

                faults.Add(_faluts[i]);
            }

            return faults;
        }

        /// <summary>
        /// 绘制故障
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_Alarms(Graphics dcGs, List<FaultInfo> faluts)
        {
            if (faluts == null || faluts.Count == 0)
                return;

            for (int i = 0; i < faluts.Count; i++)
            {
                dcGs.DrawString(
                    faluts[i].DateTime + "   " + faluts[i].Name,
                    new Font("Verdana", 11),
                    Brushes.White,
                    new RectangleF(13, 103 + 38 * i, 653, 38),
                    Global.SF_NC
                    );
            }
        }

        /// <summary>
        /// 绘制选中故障效果
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_SelectFalut(Graphics dcGs)
        {
            dcGs.DrawRectangle(
                Global.Pen_White_2,
                new Rectangle(15, 103 + 38 * _currentFalutID, 643, 38)
                );
        }

        #endregion
    }
}
