using System.Collections.Generic;
using System.Drawing;
using System.IO;

using MMI.Facility.Interface;

namespace Urban.QingDao3Line.MMI
{
    public class NewQBaseclass : baseClass
    {
        /// <summary>
        /// 配置文件取出来的图片列表
        /// </summary>
        public List<Image> m_Imgs = new List<Image>();
        /// <summary>
        /// 配置文件取出来的逻辑变量列表
        /// </summary>
        public List<int> m_BoolIds = new List<int>();
        /// <summary>
        /// 配置文件取出来的数字变量列表
        /// </summary>
        public List<int> m_FoolatIds = new List<int>();

       public override bool init(ref int nErrorObjectIndex)
        {

            return Initalize();
        }

        public virtual bool Initalize()
        {
            return true;
        }

        public void IntiData()
        {
            #region 从配置表里取出数据
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                m_Imgs.Add(Image.FromFile(Path.Combine(RecPath, UIObj.ParaList[i])));
            }
            //取出配置表Boolids编号

            for (int index = 0; index < UIObj.InBoolList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InBoolList[index + 1]; i++)
                {
                    m_BoolIds.Add(UIObj.InBoolList[index] + i);
                }
            }
            //取出配置表Floatids编号

            for (int index = 0; index < UIObj.InFloatList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InFloatList[index + 1]; i++)
                {
                    m_FoolatIds.Add(UIObj.InFloatList[index] + i);
                }
            }
            #endregion
        }
    }

}
