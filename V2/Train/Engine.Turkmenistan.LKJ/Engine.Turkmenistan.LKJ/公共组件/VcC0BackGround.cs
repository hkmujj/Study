using System.Collections.Generic;
using System.Drawing;
using System.IO;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Util;

namespace Engine.Turkmenistan.LKJ.公共组件
{

    [GksDataType(DataType.isMMIObjectClass)]
    public class VcC0BackGround : baseClass
    {
        #region 私有变量
        public static bool isShow = false;
        private readonly List<Image> m_ResourceImage = new List<Image>();//图片资源
        private Rectangle rec = new Rectangle(0, 0, 800, 600);
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-标题栏";
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
                using (FileStream fs = new FileStream(Path.Combine(RecPath ,a), FileMode.Open))
                {
                    m_ResourceImage.Add(Image.FromStream(fs));
                }
            });

            return true;
        }
        #endregion


        #region 绘制界面
        public override void paint(Graphics dcGs)
        {
            dcGs.DrawImage(m_ResourceImage[0], rec);
            if (BoolList[UIObj.InBoolList[0]])//LKJ得电
            {
                isShow = true;
            }
            else
            {
                isShow = false;
            }
            base.paint(dcGs);
        }
        #endregion
    }
}
