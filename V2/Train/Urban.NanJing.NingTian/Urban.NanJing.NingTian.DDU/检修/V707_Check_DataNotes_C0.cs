#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-26
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图707-检修-数据记录-No.0-主界面
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using Urban.NanJing.NingTian.DDU.Common;

namespace Urban.NanJing.NingTian.DDU.检修
{
    /// <summary>
    /// 功能描述：视图707-检修-数据记录-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-26
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V707_Check_DataNotes_C0 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片列表
        private List<Button> _btns = new List<Button>();//按钮列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "检修试图-数据记录";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            string[] str = new string[] { "牵引能耗\n清零", "再生能耗\n清零", "电阻能耗\n清零", "辅助能耗\n清零", "返回" };
            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button(
                    str[i],
                    new RectangleF(671, 112 + 83 * i, 127, 76),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            string[] str1 = new string[] { "里程\n清零", "总里程\n清零", "A1\n压缩机工作时间\n清零", "A2\n压缩机工作时间\n清零" };
            for (int i = 0; i < 4; i++)
            {
                Button btn = new Button(
                    str1[i],
                    new RectangleF(68+150*i, 450, 127, 76),
                    i + 5,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_11_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            return true;
        }
        #endregion

        #region 鼠标消息
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
        /// 按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 0://数据重置按钮
                    break;
                case 4://返回按钮
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.检修, 0, 0);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics g)
        {
            _btns.ForEach(a => a.Paint(g));
            paint_Frame(g);
            paint_DataValue(g);

            base.paint(g);
        }

        /// <summary>
        /// 绘制界面上的线框与标题等
        /// </summary>
        /// <param name="g"></param>
        private void paint_Frame(Graphics g)
        {
            //上线框
            g.DrawLine(new Pen(Brushs.BlackBrush, 2), new Point(0, 108), new Point(800, 108));
            g.DrawImage(_resource_Image[2], new Point(0, 108));//背景
        }

        /// <summary>
        /// 绘制数据（后续添加真实数据）
        /// </summary>
        /// <param name="g"></param>
        private void paint_DataValue(Graphics g)
        {
            //for (int i = 0; i < 5; i++)
            //{
            //    g.DrawString("0", new Font("宋体", 10), Brushs.WhiteBrush, new RectangleF(170 + (i + 1) % 2 * 317, 173 + (i + 1) / 2 * 51, 111, 39), FontInfo.SF_CC);
            //}
        }
        #endregion
    }
}
