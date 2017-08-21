#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-3
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图3-空调-No.2-空调控制
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using NC_TMS.Common;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：视图3-空调-No.2-空调控制
    /// 创建人：唐林
    /// 创建时间：2014-7-3
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V3_AirCondition_C2_AirConditionControl : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private List<Button> _btn_AirConditionState = new List<Button>();//空调状态按钮列表
        private List<Button> _btn_AirConditionMode = new List<Button>();//空调模式按钮列表
        private Group _group;//“组”控件
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "空调状态视图-空调控制";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath ,a), FileMode.Open))
                {
                    this._resource_Image.Add(Image.FromStream(fs));
                }
            });

            //空调的5种状态按钮和停机按钮
            String[] str_State = new String[] { "退出预冷", "全冷", "半冷", "自动冷", "通风", "停机" };
            for (int i = 0; i < 6; i++)
            {
                Button btn_State = null;
                if (i < 5) btn_State = new Button(
                    str_State[i], 
                    new RectangleF(57 + 85 * i - 15, 402, 77, 42),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]},
                    false
                    );
                else if (i == 5) btn_State = new Button(
                    str_State[i], 
                    new RectangleF(57 - 15, 457, 77, 42),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]},
                    false
                    );

                btn_State.ClickEvent += btn_State_ClickEvent;
                this._btn_AirConditionState.Add(btn_State);
            }

            //空调的3重模式
            String[] str_Mode = new String[] { "紧急通风测试", "紧急通风停止", "火灾模式" };
            for (int i = 0; i < 3; i++)
            {
                Button btn_Mode = null;
                if (i < 2) btn_Mode = new Button(
                    str_Mode[i],
                    new RectangleF(482 + 111 * i - 15, 402, 103, 42),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                    );
                else if (i == 2) btn_Mode = new Button(
                    str_Mode[i],
                    new RectangleF(57 + 85 - 15, 457, 77, 42),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1]}
                    );
                btn_Mode.ClickEvent += btn_Mode_ClickEvent;
                this._btn_AirConditionMode.Add(btn_Mode);
            }

            this._group = new Group("空调控制", new Font("宋体", 11), new Rectangle(12, 377, 688, 147));

            return true;
        }
        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            this._btn_AirConditionState.ForEach(a => a.MouseDown(nPoint));
            this._btn_AirConditionMode.ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            this._btn_AirConditionState.ForEach(a => a.MouseUp(nPoint));
            this._btn_AirConditionMode.ForEach(a => a.MouseUp(nPoint));
            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 空调状态按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_State_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            this._btn_AirConditionState.FindAll(a => a.ID != e.Message).ForEach(b => b.IsReplication = true);
        }

        /// <summary>
        /// 空调模式按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_Mode_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            //
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            this._btn_AirConditionState.ForEach(a => a.Paint(dcGs));
            this._btn_AirConditionMode.ForEach(a => a.Paint(dcGs));
            this._group.Paint(dcGs);

            base.paint(dcGs);
        }
        #endregion
    }
}
