using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace TouchNetTrain_TMS_.手动控制
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V3_C0_HandlerControl : baseClass
    {
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private List<Button> _btns_Tab = new List<Button>();

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行信息-切换按钮";
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

            String[] strs_Tab = { "离合齿", "转向架支撑", "保护解除" };
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button(
                    strs_Tab[i],
                    new RectangleF(19, 225 + 84 * i, 124, 37),
                    i,
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
                        Background = _resource_Image[0],
                        DownImage = _resource_Image[1]
                    },
                    false
                    );
                btn.MouseDownEvent += btn_Tab_MouseDownEvent;
                btn.ClickEvent += btn_Tab_ClickEvent;
                _btns_Tab.Add(btn);
            }
            _btns_Tab[0].IsReplication = false;

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
                switch (nParaB)
                {
                    case 301:
                        _btns_Tab[0].IsReplication = false;
                        break;
                    case 302:
                        _btns_Tab[1].IsReplication = false;
                        break;
                    case 303:
                        _btns_Tab[2].IsReplication = false;
                        break;
                }
            }
        }

        #endregion

        #region 鼠标事件
        public override
        bool mouseDown(Point nPoint)
        {
            _btns_Tab.ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns_Tab.ForEach(a => a.MouseUp(nPoint));

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Tab_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            _btns_Tab.ForEach(a =>
            {
                if (!a.IsReplication) a.IsReplication = true;
            });
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Tab_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0:
                    append_postCmd(CmdType.ChangePage, 301, 0, 0);
                    break;
                case 1:
                    append_postCmd(CmdType.ChangePage, 302, 0, 0);
                    break;
                case 2:
                    append_postCmd(CmdType.ChangePage, 303, 0, 0);
                    break;
            }
        }
        #endregion

        public override void paint(Graphics dcGs)
        {
            _btns_Tab.ForEach(a => a.Paint(dcGs));

            base.paint(dcGs);
        }
    }
}
