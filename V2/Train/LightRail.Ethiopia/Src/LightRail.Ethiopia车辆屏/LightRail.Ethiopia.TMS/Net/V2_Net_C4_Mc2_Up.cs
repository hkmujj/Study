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
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using LightRail.Ethiopia.TMS.Common;

namespace LightRail.Ethiopia.TMS.Net
{
    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V2_Net_C4_Mc2_Up : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private List<NetInfo> _nets = new List<NetInfo>();//网络通讯状态列表 
        private Int32 readTxtID = 0;//文本读取标志
        private Int32[] _values;//模块状态值
        #endregion

        #region 框架函数
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

            _values = new Int32[UIObj.InBoolList.Count];

            string[] tmpFile0 = File.ReadAllLines(
                Path.Combine(AppConfig.AppPaths.ConfigDirectory,"Mc2Up模块信息.txt"),
                Encoding.Default
                );
            foreach (string str0 in tmpFile0)
            {
                //tmpObject.addStringByLine(tmpStringLine++, str0);
                String[] split = str0.Split(new char[] { ':' });

                if (split.Length != 2)
                    continue;
                _nets.Add(new NetInfo(Convert.ToInt32(split[0]), split[1]));

            }

            return true;
        }


        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            getValue();
            paint_Mc1(dcGs, 43, 84, 5, new Rectangle(458, 190, 57, 25));

            base.paint(dcGs);
        }

        #endregion
        /// <summary>
        /// 获取Tc模块网络值
        /// </summary>
        private void getValue()
        {
            for (int dataID = 0; dataID < UIObj.InBoolList.Count; dataID++)
            {
                Int32 index = 1;
                for (int valueID = 0; valueID < 2; valueID++)
                {
                    if (BoolList[UIObj.InBoolList[dataID] + valueID])
                    {
                        index = valueID;
                        break;
                    }
                }
                _values[dataID] = index;
            }
        }

        /// <summary>
        /// 绘制Mc1模块
        /// </summary>
        /// <param name="dcGs"></param>
        /// <param name="rowDistance"></param>
        /// <param name="columnDistance"></param>
        /// <param name="rowCount"></param>
        /// <param name="rect"></param>
        private void paint_Mc1(Graphics dcGs, float rowDistance, float columnDistance, Int32 rowCount, Rectangle rect)
        {
            RectangleF[] rects = new RectangleF[_nets.Count];
            for (int i = 0; i < _nets.Count; i++)
            {
                float temp = (i / rowCount % 2 == 0) ? i % rowCount : (rowCount - 1 - i % rowCount);

                rects[i] = new RectangleF(rect.Left + columnDistance * (i / rowCount), rect.Top + rowDistance * temp, rect.Width, rect.Height);

                if (i > 0)//绘制连接线
                {
                    //单数列连接线为红色
                    Brush brush = new SolidBrush(Color.FromArgb(0, 255, 0));
                    if (i / (rowCount + 1) == 1 && i % rowCount == 0)
                        brush = Brushes.Red;

                    dcGs.DrawLine(
                        new Pen(brush, 2),
                        new PointF(rects[i - 1].Left + rects[i - 1].Width / 2, rects[i - 1].Top + rects[i - 1].Height / 2),
                        new PointF(rects[i].Left + rects[i].Width / 2, rects[i].Top + rects[i].Height / 2)
                        );
                }
            }

            Brush[] brushes=new Brush[]
            {
                new SolidBrush(Color.FromArgb(0,255,0)), 
                new SolidBrush(Color.FromArgb(215,245,255))
                //,new SolidBrush(Color.FromArgb(239,245,71))
            };
            for (int i = 0; i < _nets.Count; i++)//绘制矩形框
            {
                dcGs.FillRectangle(
                    brushes[_values[i]],
                    rects[i]
                    );
                dcGs.DrawString(
                    _nets[i].Name,
                    new Font("Verdana", 9),
                    Brushes.Black,
                    rects[i],
                    new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                    );
            }
        }
    }
}
