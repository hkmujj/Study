#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-23
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图3-空调-No.0-主界面
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

namespace Urban.NanJing.NingTian.DDU.空调状态
{
    /// <summary>
    /// 功能描述：视图3-空调-No.0-主界面
    /// 创建人：唐林
    /// 创建时间：2014-7-23
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V3_AirCondition_C0_MainView : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();//图片资源
        private List<Button> _btns = new List<Button>();//按钮列表
        private List<Button> _btns_ = new List<Button>();
        private int _temprature = 18;//温度
        private string _modeString = "未知";
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "空调状态视图-主界面";
        }

        /// <summary>
        /// 组件初始化函数：初始化处理
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            //导入图片资源
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            //模式按钮
            string[] strs = new string[] { "自动", "手动", "通风", "停止" };
            for (int i = 0; i < 4; i++)
            {
                Button btn = new Button(
                    strs[i],
                    new RectangleF(224 + 120 * i, 411, 118, 54),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("黑体", 11), TextBrush = Brushs.WhiteBrush, StringFormat = FontInfo.SF_CC },
                        Background = _resource_Image[5],
                        DownImage = _resource_Image[6]
                    },
                    false
                    );
                btn.ClickEvent += btn_ClickEvent;
                _btns.Add(btn);
            }

            //上下调节温度按钮
            for (int i = 0; i < 2; i++)
            {
                Button btn = new Button(
                    "",
                    new RectangleF(463 + 120 * i, 471, 118, 54),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES() { Font = new Font("黑体", 11), TextBrush = Brushs.WhiteBrush, StringFormat = FontInfo.SF_CC },
                        Background = _resource_Image[7 + i * 3],
                        DownImage = _resource_Image[8 + i * 3],
                        DisableImage = _resource_Image[9 + i * 3]
                    }
                    );
                btn.ClickEvent += btn_ClickEvent_;
                _btns_.Add(btn);
            }

            return true;
        }
        #endregion

        #region 鼠标事件
        /// <summary>
        /// 鼠标按下监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseDown(Point nPoint)
        {
            _btns.ForEach(a => a.MouseDown(nPoint));
            _btns_.ForEach(a => a.MouseDown(nPoint));

            return base.mouseDown(nPoint);
        }

        /// <summary>
        /// 鼠标弹起监测函数
        /// </summary>
        /// <param name="nPoint"></param>
        /// <returns></returns>
        public override bool mouseUp(Point nPoint)
        {
            _btns.ForEach(a => a.MouseUp(nPoint));
            _btns_.ForEach(a => a.MouseUp(nPoint));

            return base.mouseUp(nPoint);
        }

        private int _currentFaultState = -1;
        /// <summary>
        /// 按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            if (_currentFaultState == e.Message)
            {
                _btns[e.Message].IsReplication = false;
                return;
            }

            if(_currentFaultState!=-1)
            _btns[_currentFaultState].IsReplication = true;
            _btns[e.Message].IsReplication = false;
            _currentFaultState = e.Message;

            //按钮点击事件
            switch (e.Message)
            {
                case 0:
                    _modeString = "自动";
                    _btns_[0].Enable = false;
                    _btns_[1].Enable = false;
                    break;
                case 1:
                    _modeString = "手动";
                    _btns_[0].Enable = true;
                    _btns_[1].Enable = true;
                    break;
                case 2:
                    _modeString = "通风";
                    _btns_[0].Enable = false;
                    _btns_[1].Enable = false;
                    break;
                case 3:
                    _modeString = "停止";
                    _btns_[0].Enable = false;
                    _btns_[1].Enable = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 按钮点击事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent_(object sender, ClickEventArgs<int> e)
        {
            //按钮点击事件
            switch (e.Message)
            {
                case 0://向上调节按钮
                    if (_temprature < 30)
                        _temprature++;
                    break;
                case 1://向下调节按钮
                    if (_temprature > 18)
                        _temprature--;
                    break;
            }
        }

        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制函数
        /// </summary>
        /// <param name="g"></param>
        public override void paint(Graphics g)
        {
            paint_Frame(g);
            paint_TrainTemperature(g);
            paint_TrainOutTemperature(g);
            paint_TrainTargetTemperature(g);
            paint_PowerConsume(g);
            paint_CompressorState(g);
            paint_NewWindState(g);
            paint_BackWindState(g);
            paint_AirConditionMode(g);

            _btns.ForEach(a => a.Paint(g));
            _btns_.ForEach(a => a.Paint(g));
            if (_modeString == "未知")
            {
                _btns_[0].Enable = false;
                _btns_[1].Enable = false;
            }

            base.paint(g);
        }

        /// <summary>
        /// 状态框绘制
        /// </summary>
        /// <param name="g"></param>
        private void paint_Frame(Graphics g)
        {
            g.DrawLine(new Pen(Brushs.BlackBrush, 2), new Point(0, 108), new Point(800, 108));//上线框
            g.DrawImage(_resource_Image[0], new Point(0, 108));//背景

            g.DrawString(
                _temprature.ToString(),
                new Font("DigifaceWide", 32), Brushes.White, new Rectangle(250, 473, 100, 49), FontInfo.SF_CC);
        }

        /// <summary>
        /// 室内温度
        /// </summary>
        /// <param name="g"></param>
        private void paint_TrainTemperature(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                g.DrawString(
                    FloatList[UIObj.InFloatList[0] + i].ToString("0"),
                    new Font("Arial", 11),
                    new SolidBrush(Color.White),
                    new RectangleF(103 + 173 * i, 143 + 4 + 26 * 0, 165, 24),
                    FontInfo.SF_CC
                    );
            }
        }

        /// <summary>
        /// 室外温度
        /// </summary>
        /// <param name="g"></param>
        private void paint_TrainOutTemperature(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                g.DrawString(
                    FloatList[UIObj.InFloatList[1] + i].ToString("0"),
                    new Font("Arial", 11),
                    new SolidBrush(Color.White),
                    new RectangleF(103 + 173 * i, 143 + 4 + 26 * 1, 165, 24),
                    FontInfo.SF_CC
                    );
            }
        }
        
        /// <summary>
        /// 车厢目标温度
        /// </summary>
        /// <param name="g"></param>
        private void paint_TrainTargetTemperature(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                g.DrawString(
                    _temprature.ToString(),//FloatList[UIObj.InFloatList[2] + i].ToString("0"),
                    new Font("Arial", 11),
                    new SolidBrush(Color.White),
                    new RectangleF(103 + 173 * i, 143 + 4 + 26 * 2 - 2, 165, 24),//向上调整2个像素
                    FontInfo.SF_CC
                    );
            }
        }

        /// <summary>
        /// 功率消耗
        /// </summary>
        /// <param name="g"></param>
        private void paint_PowerConsume(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                g.DrawString(
                    FloatList[UIObj.InFloatList[3] + i].ToString("0"),
                    new Font("Arial", 11),
                    new SolidBrush(Color.White),
                    new RectangleF(103 + 173 * i, 143 + 4 + 26 * 3 - 2, 165, 24),//向上调整2个像素
                    FontInfo.SF_CC
                    );
            }
        }

        /// <summary>
        /// 空调模式
        /// </summary>
        /// <param name="g"></param>
        private void paint_AirConditionMode(Graphics g)
        {
            //String[] str = new String[] { "本地控制", "集中控制", "紧急通风测试", "紧急通风", "未知" };
            for (int i = 0; i < 4; i++)
            {
                //if (BoolList[UIObj.InBoolList[i + 6] + j])
                //{
                //    g.DrawString(
                //        str[j],
                //        new Font("宋体", 10.5f),
                //        new SolidBrush(Color.White),
                //        new RectangleF(15.5f - 4 + 114.899f + 92.934f * i, 182 + 2 + 31.5f * 3, 92.934f, 31.5f),
                //        FontInfo.SF_CC
                //        );
                //}
                g.DrawString(
                    _modeString,
                    new Font("宋体", 10.5f),
                    new SolidBrush(Color.White),
                    new RectangleF(103 + 173 * i, 143 + 4 + 26 * 4, 165, 24),
                    FontInfo.SF_CC
                    );
            }
        }

        /// <summary>
        /// 压缩机状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_CompressorState(Graphics g)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[0] + j + i * 4])
                    {
                        g.DrawImage(
                           _resource_Image[j + 1],
                           new Point(121 + 173 * (i / 4) + 35 * (i % 4), 276 + 2)
                           );
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 新风阀状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_NewWindState(Graphics g)
        {
            Image[] images = new Image[] { _resource_Image[2], _resource_Image[2], _resource_Image[2], _resource_Image[3], _resource_Image[1], _resource_Image[4] };
            string[] strs = new string[] { "1/3", "2/3", "", "", "", "" };
            //A1
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (BoolList[UIObj.InBoolList[1] + j + i * 6])
                    {
                        g.DrawImage(
                           images[j],
                           new Rectangle(121 + 173 * 0 + 35 * (i % 4), 303 + 2 + 22 * (i / 4), 21, 18)
                           );
                        g.DrawString(
                            strs[j],
                            new Font("Arial", 8),
                            new SolidBrush(Color.Black),
                            new RectangleF(121 + 173 * 0 + 35 * (i % 4), 303 + 2 + 22 * (i / 4), 21, 18),//向上调整2个像素
                            FontInfo.SF_CC
                            );
                        break;
                    }
                }
            }
            //B1
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (BoolList[UIObj.InBoolList[2] + j + i * 6])
                    {
                        g.DrawImage(
                           images[j],
                           new Rectangle(121 + 173 * 1 + 35 * (i % 4), 303 + 2 + 22 * (i / 4), 21, 18)
                           );
                        g.DrawString(
                            strs[j],
                            new Font("Arial", 8),
                            new SolidBrush(Color.Black),
                            new RectangleF(121 + 173 * 1 + 35 * (i % 4), 303 + 2 + 22 * (i / 4), 21, 18),//向上调整2个像素
                            FontInfo.SF_CC
                            );
                        break;
                    }
                }
            }
            //B2
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (BoolList[UIObj.InBoolList[3] + j + i * 6])
                    {
                        g.DrawImage(
                           images[j],
                           new Rectangle(121 + 173 * 2 + 35 * (i % 4), 303 + 2 + 22 * (i / 4) + 2, 21, 18)
                           );
                        g.DrawString(
                            strs[j],
                            new Font("Arial", 8),
                            new SolidBrush(Color.Black),
                            new RectangleF(121 + 173 * 2 + 35 * (i % 4), 303 + 2 + 22 * (i / 4) + 2, 21, 18),//向上调整2个像素
                            FontInfo.SF_CC
                            );
                        break;
                    }
                }
            }
            //A2
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (BoolList[UIObj.InBoolList[4] + j + i * 6])
                    {
                        g.DrawImage(
                           images[j],
                           new Rectangle(121 + 173 * 3 + 35 * (i % 4), 303 + 2 + 22 * (i / 4) + 2, 21, 18)
                           );
                        g.DrawString(
                            strs[j],
                            new Font("Arial", 8),
                            new SolidBrush(Color.Black),
                            new RectangleF(121 + 173 * 3 + 35 * (i % 4), 303 + 2 + 22 * (i / 4) + 2, 21, 18),//向上调整2个像素
                            FontInfo.SF_CC
                            );
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 回风阀状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_BackWindState(Graphics g)
        {
            Image[] images = new Image[] { _resource_Image[2], _resource_Image[1], _resource_Image[3], _resource_Image[4] };
            //A1
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[5] + j + i * 4])
                    {
                        g.DrawImage(
                           images[j],
                           new Rectangle(121 + 173 * 0 + 35 * (i % 4), 354 + 2 + 22 * (i / 4), 21, 18)
                           );
                        break;
                    }
                }
            }
            //B1
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[6] + j + i * 4])
                    {
                        g.DrawImage(
                           images[j],
                           new Rectangle(121 + 173 * 1 + 35 * (i % 4), 354 + 2 + 22 * (i / 4), 21, 18)
                           );
                        break;
                    }
                }
            }
            //B2
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[7] + j + i * 4])
                    {
                        g.DrawImage(
                           images[j],
                           new Rectangle(121 + 173 * 2 + 35 * (i % 4), 354 + 2 + 22 * (i / 4) + 2, 21, 18)
                           );
                        break;
                    }
                }
            }
            //A2
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[8] + j + i * 4])
                    {
                        g.DrawImage(
                           images[j],
                           new Rectangle(121 + 173 * 3 + 35 * (i % 4), 354 + 2 + 22 * (i / 4) + 2, 21, 18)
                           );
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
