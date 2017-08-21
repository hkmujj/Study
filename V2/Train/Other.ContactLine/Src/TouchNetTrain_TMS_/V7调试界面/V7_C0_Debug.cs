using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace TouchNetTrain_TMS_.V7调试界面
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V7_C0_Debug : baseClass
    {
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private Button _btn;

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "调试界面-主界面";
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

            _btn = new Button(
                    "累计信息清零",
                    new RectangleF(15, 98, 157, 43),
                    0,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = Global.Font_Verdana_12_B,
                            TextBrush = new SolidBrush(Color.Black),
                            StringFormat = Global.SF_CC
                        },
                        Background = _resource_Image[1],
                        DownImage = _resource_Image[2]
                    }
                    );
            _btn.MouseDownEvent += btn_MouseDownEvent;
            _btn.ClickEvent += btn_ClickEvent;

            return true;
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btn.MouseDown(nPoint);
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btn.MouseUp(nPoint);
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
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(_resource_Image[0], new RectangleF(216,230,384,181));
            _btn.Paint(dcGs);

            base.paint(dcGs);
        }
        #endregion
    }
}
