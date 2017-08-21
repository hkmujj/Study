#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-9
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图4-通讯信息-No.3-B2模块
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using Urban.NanJing.NingTian.DDU.Common;

namespace Urban.NanJing.NingTian.DDU.通讯状态
{
    /// <summary>
    /// 功能描述：视图4-通讯信息-No.3-B2模块
    /// 创建人：唐林
    /// 创建时间：2014-7-24
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V4_Message_C3_B2 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
        private List<MessageModeInfo> _modesInfo = new List<MessageModeInfo>();//M车模块信息列表
        private int[] _values;//模块状态值
        private int readTxtID = 0;//文本读取标志
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "通讯状态视图-B2模块";
        }

        /// <summary>
        /// 初始化函数：初始化操作
        /// </summary>
        /// <param name="nErrorObjectIndex">错误索性</param>
        /// <returns>初始化是否成功</returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Images.Add(Image.FromStream(fs));
                }
            });

            _values = new int[UIObj.InBoolList.Count];

            string[] tmpFile0 = File.ReadAllLines(
                Path.Combine(AppPaths.ConfigDirectory, "B2模块信息.txt"),
                Encoding.Default
                );
            foreach (string str0 in tmpFile0)
            {
                //tmpObject.addStringByLine(tmpStringLine++, str0);
                string[] split = str0.Split(new char[] { ':' });

                if (split.Length != 2)
                    continue;
                _modesInfo.Add(new MessageModeInfo(Convert.ToInt32(split[0]), split[1]));

            }

            return true;
        }

        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="g"></param>
        public override void paint(Graphics g)
        {
            getValue();//获取数据
            paint_MpMode(g);

            base.paint(g);
        }

        /// <summary>
        /// 获取Mp模块组件网络值
        /// </summary>
        private void getValue()
        {
            for (int dataID = 0; dataID < UIObj.InBoolList.Count; dataID++)
            {
                int index = 2;
                for (int valueID = 0; valueID < 3; valueID++)
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
        /// 绘制Mp模块
        /// </summary>
        /// <param name="g"></param>
        private void paint_MpMode(Graphics g)
        {
            float axis1 = 401;
            for (int i = 0; i < UIObj.InBoolList.Count; i++)
            {
                if (_modesInfo[i].ID <= 7)
                {
                    g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(170, 169, 152)), 3), new PointF(axis1 - 79, 217 + 31 / 2 + ((_modesInfo[i].ID - 1) * 33)), new PointF(axis1, 217 + 31 / 2 + ((_modesInfo[i].ID - 1) * 33)));
                    g.DrawImage(_resource_Images[_values[i]], new RectangleF(axis1 - 24 - 62, 217 + ((_modesInfo[i].ID - 1) * 33), 62, 31));
                    g.DrawString(_modesInfo[i].Name, new Font("宋体", 11f, FontStyle.Bold), Brushs.BlackBrush, new RectangleF(axis1 - 24 - 62, 217 + ((_modesInfo[i].ID - 1) * 33), 62, 31), FontInfo.SF_CC);
                }
                else
                {
                    g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(170, 169, 152)), 3), new PointF(axis1, 217 + 31 / 2 + ((_modesInfo[i].ID - 1 - 7) * 33)), new PointF(axis1 + 79, 217 + 31 / 2 + ((_modesInfo[i].ID - 1 - 7) * 33)));
                    g.DrawImage(_resource_Images[_values[i]], new RectangleF(axis1 + 24, 217 + ((_modesInfo[i].ID - 1 - 7) * 33), 62, 31));
                    g.DrawString(_modesInfo[i].Name, new Font("宋体", 11f, FontStyle.Bold), Brushs.BlackBrush, new RectangleF(axis1 + 24, 217 + ((_modesInfo[i].ID - 1 - 7) * 33), 62, 31), FontInfo.SF_CC);
                }
            }
        }
        #endregion
    }
}
