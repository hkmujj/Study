using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Service;

// ReSharper disable All

namespace MMI.Facility.Interface
{
    /// <summary>
    /// 基础对象类
    /// </summary>
    [GksDataType(Interface.Attribute.DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    public class baseClass : TypeBase, IObjectBase
    {
        private IProjectIndexDescriptionConfig m_IndexDescriptionConfig;

        /// <summary>
        /// 配置参数
        /// </summary>
        public IUIObject UIObj { get; set; }

        /// <summary>
        /// 主分类
        /// </summary>
        public override string MainIndex
        {
            get { return UIObj.MainIndex.ToString(CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// 
        /// </summary>
        public override List<int> ViewList
        {
            get { return UIObj.ViewList; }
            set { throw new InvalidOperationException(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public IProjectIndexDescriptionConfig IndexDescriptionConfig
        {
            get
            {
                return m_IndexDescriptionConfig ??
                       (m_IndexDescriptionConfig =
                           IConfig.IndexDescriptionConfig.GetProjectIndexDescriptionConfig(
                               new CommunicationDataKey(AppConfig)));
            }
        }

        public bool GetInBoolValue(string name)
        {
            return BoolList[GetInBoolIndex(name)];
        }

        public bool GetOutBoolValue(string name)
        {
            return OutBoolList[GetOutBoolIndex(name)];
        }

        public float GetInFloatValue(string name)
        {
            return FloatList[GetInFloatIndex(name)];
        }

        public float GetOutFloatValue(string name)
        {
            return OutFloatList[GetOutFloatIndex(name)];
        }

        public int GetInBoolIndex(string name)
        {
            if (IndexDescriptionConfig != null && IndexDescriptionConfig.InBoolDescriptionDictionary.ContainsKey(name))
            {
                return IndexDescriptionConfig.InBoolDescriptionDictionary[name];
            }
            throw new KeyNotFoundException(string.Format("Can not found in bool index where name={0}", name));
            return int.MaxValue;
        }

        public int GetOutBoolIndex(string name)
        {
            if (IndexDescriptionConfig != null && IndexDescriptionConfig.OutBoolDescriptionDictionary.ContainsKey(name))
            {
                return IndexDescriptionConfig.OutBoolDescriptionDictionary[name];
            }
            throw new KeyNotFoundException(string.Format("Can not found out bool index where name={0}", name));
            return int.MaxValue;
        }

        public int GetInFloatIndex(string name)
        {
            if (IndexDescriptionConfig != null && IndexDescriptionConfig.InFloatDescriptionDictionary.ContainsKey(name))
            {
                return IndexDescriptionConfig.InFloatDescriptionDictionary[name];
            }
            throw new KeyNotFoundException(string.Format("Can not found in float index where name={0}", name));
            return int.MaxValue;
        }

        public int GetOutFloatIndex(string name)
        {
            if (IndexDescriptionConfig != null && IndexDescriptionConfig.OutFloatDescriptionDictionary.ContainsKey(name))
            {
                return IndexDescriptionConfig.OutFloatDescriptionDictionary[name];
            }
            throw new KeyNotFoundException(string.Format("Can not found out float index where name={0}", name));
            return int.MaxValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public baseClass()
        {
            UIObj = new UIObject();
        }

        /// <summary>
        /// 设置动态数据
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public virtual void setRunValue(int nParaA, int nParaB, float nParaC)
        {
        }

        /// <summary>
        /// 绘制图像
        /// </summary>
        /// <param name="g"></param>
        public virtual void paint(Graphics g)
        {
        }

        /// <summary>
        /// 鼠标单点下
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool mouseDown(Point point)
        {
            return true;
        }

        /// <summary>
        /// 鼠标弹起
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public virtual bool mouseUp(Point point)
        {
            return false;
        }

        /// <summary>
        /// 设置各列表指定位置的值
        /// </summary>
        /// <param name="listType"></param>
        /// <param name="nindex"></param>
        /// <param name="objValue"></param>
        public virtual void setListValue(ParaType listType, int nindex, object objValue)
        {
            int tmpIntValue;

            switch (listType)
            {
                case ParaType.inBool:
                    if (nindex < 0) return;

                    if (int.TryParse(objValue.ToString(), out tmpIntValue))
                    {
                        //如果没有发现该key，则先添加该key
                        if (nindex >= UIObj.InBoolList.Count)
                        {
                            for (var i = UIObj.InBoolList.Count; i < nindex; i++)
                            {
                                UIObj.InBoolList.Add(0);
                            }
                        }

                        UIObj.InBoolList[nindex] = tmpIntValue;
                    }
                    break;
                case ParaType.inFloat:
                    if (nindex < 0) return;

                    if (int.TryParse(objValue.ToString(), out tmpIntValue))
                    {
                        //如果没有发现该key，则先添加该key
                        if (nindex >= UIObj.InFloatList.Count)
                        {
                            for (var i = UIObj.InFloatList.Count; i <= nindex; i++)
                            {
                                UIObj.InFloatList.Add(0);
                            }
                        }

                        UIObj.InFloatList[nindex] = tmpIntValue;
                    }
                    break;
                case ParaType.uiPara:
                    if (nindex < 0) return;

                    string tmpStrValue = objValue.ToString().Trim();

                    //如果没有发现该key，则先添加该key
                    if (nindex >= UIObj.ParaList.Count)
                    {
                        for (var i = UIObj.ParaList.Count; i <= nindex; i++)
                        {
                            UIObj.ParaList.Add("0");
                        }
                    }

                    UIObj.ParaList[nindex] = tmpStrValue;
                    break;
                case ParaType.view:
                    bool tmpBoolValue;
                    if (!bool.TryParse(objValue.ToString(), out tmpBoolValue)) return;

                    if (tmpBoolValue)
                    {
                        if (!UIObj.ViewList.Contains(nindex))
                        {
                            UIObj.ViewList.Add(nindex);
                        }
                    }
                    else
                    {
                        if (UIObj.ViewList.Contains(nindex))
                        {
                            UIObj.ViewList.Remove(nindex);
                        }
                    }
                    break;
            }
        }
    }
}
