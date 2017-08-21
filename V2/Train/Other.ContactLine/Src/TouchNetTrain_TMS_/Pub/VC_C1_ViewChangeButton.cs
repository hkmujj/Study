#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-1
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-No.1-切换视图按钮
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
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using NC_TMS.Common;

namespace TouchNetTrain_TMS_
{
    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class VC_C1_ViewChangeButton : baseClass
    {
        #region 私有变量
        private ViewState _currentViewState;                      //当前视图
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private List<Button> _btns_Down_TabView = new List<Button>();//按钮列表
        private Int32 _currentViewID = 0;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共试图-视图切换按钮";
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
                List<String> strList = new List<string>() { "牵引界面", "运行信息", "手动控制", "开关状态", "故障履历", "累计信息", "调试界面", "辅助功能" };
                _currentViewID = Convert.ToInt32(nParaB.ToString().ElementAt(0).ToString()) - 1;

                if (_currentViewID > 7) return;

                strList.RemoveAt(_currentViewID);
                for (int i = 0; i < 7; i++)
                {
                    Button btn = new Button(
                        strList[i],
                        new RectangleF(16 + i * 111.1F, 545, 101.1F, 34),
                        i,
                        new ButtonStyle()
                        {
                            FontStyle = new FontStyle_ES()
                            {
                                Font = Global.Font_Verdana_12_B,
                                TextBrush = new SolidBrush(Color.Yellow),
                                StringFormat = Global.SF_CC
                            },
                            Background = _resource_Image[0],
                            DownImage = _resource_Image[1]
                        }
                        );
                    btn.MouseDownEvent += btn_MouseDownEvent;
                    _btns_Down_TabView.Add(btn);
                }
            }
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btns_Down_TabView.ToList().ForEach(a => a.MouseDown(nPoint));
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns_Down_TabView.ToList().ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            Int32 index = e.Message >= _currentViewID ? e.Message + 1 : e.Message;

            index++;

            switch (index)
            {
                case 3:
                    append_postCmd(CmdType.ChangePage, 301, 0, 0);
                    break;
                default:
                    append_postCmd(CmdType.ChangePage, index, 0, 0);
                    break;
            }
        }
        #endregion

        public SolidBrush SB_1 = new SolidBrush(Color.FromArgb(160, 183, 206));
        public SolidBrush SB_2 = new SolidBrush(Color.FromArgb(77, 134, 243));

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            dcGs.FillRectangle(SB_1, new Rectangle(8, 8, 784, 584));
            dcGs.FillRectangle(SB_2, new Rectangle(11, 538, 778, 48));
            dcGs.DrawRectangle(Global.Pen_White_2, new Rectangle(11, 538, 778, 48));
            _btns_Down_TabView.ToList().ForEach(a => a.Paint(dcGs));

            base.paint(dcGs);
        }
        #endregion
    }
}
