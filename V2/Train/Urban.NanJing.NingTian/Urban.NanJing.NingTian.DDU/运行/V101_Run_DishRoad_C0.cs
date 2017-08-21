#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-21
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图101-运行-盘路信息-No.0
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using Urban.NanJing.NingTian.DDU.Common;

namespace Urban.NanJing.NingTian.DDU.运行
{
    /// <summary>
    /// 功能描述：视图101-运行-盘路信息-No.0
    /// 创建人：唐林
    /// 创建时间：2014-7-21
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V101_Run_DishRoad_C0 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
        private string[] _str_DishRoad;//盘路信息文本列表
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "运行视图-盘路信息";
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
            paint_Frame(g);
            paint_Value(g);

            base.paint(g);
        }

        /// <summary>
        /// 绘制线框与标题
        /// </summary>
        /// <param name="g"></param>
        private void paint_Frame(Graphics g)
        {
            g.DrawImage(_resource_Images[0], new Point(0, 108));
            g.DrawLine(new Pen(Brushs.BlackBrush, 2), new Point(0, 108), new Point(800, 108));
        }

        /// <summary>
        /// 绘制状态
        /// </summary>
        /// <param name="g"></param>
        private void paint_Value(Graphics g)
        {
            //A1
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[0] + j + i * 3])
                    {
                        g.DrawImage(_resource_Images[1 + j], new PointF(199, 159 + 22.5f * i));
                        break;
                    }
                }
            }

            //A2
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[0] + j + (i + 16) * 3])
                    {
                        g.DrawImage(_resource_Images[1 + j], new PointF(537, 159 + 22.5f * i));
                        break;
                    }
                }
            }
        }
        #endregion
    }
}
