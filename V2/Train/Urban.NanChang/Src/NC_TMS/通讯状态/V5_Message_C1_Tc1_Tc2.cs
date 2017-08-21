#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-9
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：视图5-通讯信息-No.1-TC模块
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace NC_TMS
{
    /// <summary>
    /// 功能描述：通讯信息模块info
    /// 创建人：唐林
    /// 创建时间：2014-07-09
    /// </summary>
    public class MessageModeInfo
    {
        /// <summary>
        /// 读取或设置编号属性
        /// </summary>
        public Int32 ID { get; set; }
        /// <summary>
        /// 读取或设置名称属性
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public MessageModeInfo(Int32 id, String name)
        {
            this.ID = id;
            this.Name = name;
        }
    }

    /// <summary>
    /// 功能描述：视图5-通讯信息-No.1-TC模块
    /// 创建人：唐林
    /// 创建时间：2014-7-9
    /// </summary>
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V5_Message_C1_Tc1_Tc2 : baseClass
    {
        #region 私有变量
        private List<Image> _resource_Images = new List<Image>();//图片资源
        private List<MessageModeInfo> _modesInfo = new List<MessageModeInfo>();//M车模块信息列表
        private Int32[] _values;//模块状态值
        private Int32 readTxtID = 0;//文本读取标志
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "通讯状态视图-Tc模块";
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    this._resource_Images.Add(Image.FromStream(fs));
                }
            });

            string[] strs = File.ReadAllLines(Path.Combine(RecPath, "..\\config\\Tc模块信息.txt"), System.Text.Encoding.Default);
            for (int i = 0; i < strs.Length; i++)
            {
                String[] strs_ = strs[i].Split(':');
                this._modesInfo.Add(new MessageModeInfo(Convert.ToInt32(strs_[0]), strs_[1]));
            }

            this._values = new Int32[UIObj.InBoolList.Count];

            return true;
        }

        ///// <summary>
        ///// 设置Tc模块文本读取标志
        ///// </summary>
        ///// <param name="nPara"></param>
        ///// <returns></returns>
        //public override bool canSetStringList(int nPara)
        //{
        //    if (nPara == 3)
        //    {
        //        readTxtID = 3;
        //        return true;
        //    }
        //    else
        //    {
        //        readTxtID = 0;
        //        return false;
        //    }
        //}

        ///// <summary>
        ///// 读取Tc模块配置信息
        ///// </summary>
        ///// <param name="nIndex"></param>
        ///// <param name="cStr"></param>
        //public override void addStringByLine(int nIndex, string cStr)
        //{
        //    String[] split = cStr.Split(new char[] { ':' });
        //    if (readTxtID == 3)
        //    {
        //        if (split.Length != 2)
        //            return;
        //        this._modesInfo.Add(new MessageModeInfo(Convert.ToInt32(split[0]), split[1]));
        //    }
        //}
        #endregion

        #region 界面绘制
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            this.getValue();//获取数据
            this.paint_TcMode(dcGs);

            base.paint(dcGs);
        }

        /// <summary>
        /// 获取Tc模块网络值
        /// </summary>
        private void getValue()
        {
            for (int dataID = 0; dataID < UIObj.InBoolList.Count; dataID++)
            {
                Int32 index = 2;
                for (int valueID = 0; valueID < 3; valueID++)
                {
                    if (BoolList[UIObj.InBoolList[dataID] + valueID])
                    {
                        index = valueID;
                        break;
                    }
                }
                this._values[dataID] = index;
            }
        }

        /// <summary>
        /// 绘制Tc模块
        /// </summary>
        /// <param name="dcGs"></param>
        private void paint_TcMode(Graphics dcGs)
        {
            for (int i = 0; i < UIObj.InBoolList.Count / 2; i++)
            {
                if (this._modesInfo[i].ID <= 8)
                {
                    dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(95 - 17, 240 + 29 / 2 + ((this._modesInfo[i].ID - 1) * 33)), new PointF(95, 240 + 29 / 2 + ((this._modesInfo[i].ID - 1) * 33)));
                    dcGs.DrawImage(this._resource_Images[this._values[i]], new RectangleF(20, 240 + ((this._modesInfo[i].ID - 1) * 33), 58, 29));
                    dcGs.DrawString(this._modesInfo[i].Name, new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(20, 240 + ((this._modesInfo[i].ID - 1) * 33), 58, 29), FontInfo.SF_CC);

                    dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(704, 240 + 29 / 2 + ((this._modesInfo[i].ID - 1) * 33)), new PointF(704 + 17, 240 + 29 / 2 + ((this._modesInfo[i].ID - 1) * 33)));
                    dcGs.DrawImage(this._resource_Images[this._values[i + UIObj.InBoolList.Count / 2]], new RectangleF(704 + 17, 240 + ((this._modesInfo[i].ID - 1) * 33), 58, 29));
                    dcGs.DrawString(this._modesInfo[i].Name, new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(20 + 702, 240 + ((this._modesInfo[i].ID - 1) * 33), 58, 29), FontInfo.SF_CC);
                }
                else
                {
                    dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(95, 240 + 29 / 2 + ((this._modesInfo[i].ID - 8 - 1) * 33)), new PointF(95 + 17, 240 + 29 / 2 + ((this._modesInfo[i].ID - 8 - 1) * 33)));
                    dcGs.DrawImage(this._resource_Images[this._values[i]], new RectangleF(20 + 93, 240 + ((this._modesInfo[i].ID - 8 - 1) * 33), 58, 29));
                    dcGs.DrawString(this._modesInfo[i].Name, new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(20 + 93, 240 + ((this._modesInfo[i].ID - 8 - 1) * 33), 58, 29), FontInfo.SF_CC);


                    dcGs.DrawLine(new Pen(Brushs.WhiteBrush), new PointF(704 - 17, 240 + 29 / 2 + ((this._modesInfo[i].ID - 8 - 1) * 33)), new PointF(704, 240 + 29 / 2 + ((this._modesInfo[i].ID - 8 - 1) * 33)));
                    dcGs.DrawImage(this._resource_Images[this._values[i + UIObj.InBoolList.Count / 2]], new RectangleF(20 + 702 - 93, 240 + ((this._modesInfo[i].ID - 8 - 1) * 33), 58, 29));
                    dcGs.DrawString(this._modesInfo[i].Name, new Font("宋体", 10f), Brushs.BlackBrush, new RectangleF(20 + 702 - 93, 240 + ((this._modesInfo[i].ID - 8 - 1) * 33), 58, 29), FontInfo.SF_CC);
                }
            }
        }
        #endregion
    }
}
