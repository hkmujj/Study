#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-26
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图705-检修-车号设置-No.0-主界面
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
    /// 功能描述：视图705-检修-车号设置-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-26
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V705_Check_TrainIDSetting_C0 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片列表
        private List<Button> _btns = new List<Button>();//按钮列表
        private string _newTrainID = string.Empty;//新车号
        private string _oldTrainID = "0";//旧车号
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "检修试图-车号设置";
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

            for (int i = 0; i < 10; i++)
            {
                Button btn = new Button(
                    ((i + 1) % 10).ToString(),
                    new RectangleF(403 + (i % 3 * 102), 187 + (i / 3 * 48), 100, 44),
                    i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_13_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            Button btn_DEL = new Button(
                "DEL",
                new RectangleF(403 + (10 % 3 * 102), 187 + (10 / 3 * 48), 202, 44),
                10,
                new ButtonStyle() { FontStyle = FontStyles.FS_Song_13_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                );
            btn_DEL.ClickEvent += btn_ClickEvent;
            _btns.Add(btn_DEL);

            string[] str_ = new string[] { "确定", "取消" };
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    str_[i],
                    new RectangleF(403 + (i * 204), 393, 100, 44),
                    11 + i,
                    new ButtonStyle() { FontStyle = FontStyles.FS_Song_13_CC_B, Background = _resource_Image[0], DownImage = _resource_Image[1] }
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            return true;
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
        /// 数字模块按钮响应点击事件
        /// </summary>
        /// <param name="obj"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            switch (e.Message)
            {
                case 10://DEL按钮
                    if (_newTrainID.Length == 0)
                        break;
                    _newTrainID = _newTrainID.Substring(0, _newTrainID.Length - 1);
                    break;
                case 11://确定按钮
                    break;
                case 12://取消按钮
                    append_postCmd(MMI.Facility.Interface.Data.CmdType.ChangePage, (int)ViewState.检修, 0, 0);
                    break;
                default://0-9数字按钮
                    if (_newTrainID.Length == 6)
                        break;
                    _newTrainID += ((e.Message + 1) % 10).ToString();
                    break;
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics g)
        {
            _btns.ForEach(a => a.Paint(g));
            paint_SetValue(g);

            base.paint(g);
        }

        /// <summary>
        /// 绘制车号输入
        /// </summary>
        /// <param name="g"></param>
        private void paint_SetValue(Graphics g)
        {
            string[] str = new string[] { "原车号：", "新车号：" };
            string[] str_ = new string[] { _oldTrainID, _newTrainID };
            for (int i = 0; i < 2; i++)
            {
                g.DrawString(str[i], new Font("宋体", 10.5f), Brushs.WhiteBrush, new RectangleF(185, 245 + 43 * i, 100, 36), FontInfo.SF_LC);
                g.FillRectangle(Brushs.WhiteBrush, new Rectangle(229, 245 + 43 * i, 91, 36));
                g.DrawString(str_[i], new Font("宋体", 12f), Brushs.BlackBrush, new RectangleF(229, 245 + 43 * i, 91, 36), FontInfo.SF_LC);
            }
        }
        #endregion
    }
}
