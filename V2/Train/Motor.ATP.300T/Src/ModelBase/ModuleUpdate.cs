using System.Collections.Generic;

namespace Motor.ATP._300T.ModelBase
{
    /// <summary>
    /// 模块数据更新类
    /// </summary>
    public class ModuleUpdate
    {
        /// <summary>
        /// ToDo：
        /// </summary>
        public ModuleUpdate()
        {
            AllBoolsIdList = new List<int>();
            AllFloatsIdList = new List<int>();
            m_LastCycleBoolsDict = new Dictionary<int, bool>();
            m_LastCycleFloatsDict = new Dictionary<int, float>();
        }

        /// <summary>
        /// 初始化内部id列表
        /// </summary>
        /// <param name="theDataType">需要添加的数据id类型</param>
        /// <param name="id">需要添加的数据id</param>
        public void AddObjectToList(DataType theDataType, int id)
        {
            switch (theDataType)
            {
                case DataType.BoolType:
                    if (!AllBoolsIdList.Contains(id))
                        AllBoolsIdList.Add(id);
                    break;
                case DataType.FloatType:
                    if (!AllFloatsIdList.Contains(id))
                        AllFloatsIdList.Add(id);
                    break;
            }
        }

        /// <summary>
        /// bool量数据更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public void BoolsUpdate(int id, bool value)
        {
            if (!m_LastCycleBoolsDict.ContainsKey(id)) //所有bool集合中不存在需要更新的值
            {
                m_LastCycleBoolsDict.Add(id, value);
                ChangingBoolsDict.Add(id, value);
            }
            else
            {
                //本周期与上周期的值相同
                if (m_LastCycleBoolsDict[id].Equals(value) && ChangingBoolsDict.ContainsKey(id))
                {
                    ChangingBoolsDict.Remove(id);
                }
                else if (!m_LastCycleBoolsDict[id].Equals(value)) //本周期与上周期的值不同
                {
                    if (ChangingBoolsDict.ContainsKey(id))
                        ChangingBoolsDict[id] = value;
                    else
                        ChangingBoolsDict.Add(id, value);
                }
            }
        }

        /// <summary>
        /// float量数据更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public void FloatsUpdate(int id, float value)
        {
            if (!m_LastCycleFloatsDict.ContainsKey(id)) //所有bool集合中不存在需要更新的值
            {
                m_LastCycleFloatsDict.Add(id, value);
                ChangingFloatsDict.Add(id, value);
            }
            else
            {
                //本周期与上周期的值相同
                if (m_LastCycleFloatsDict[id].Equals(value) && ChangingFloatsDict.ContainsKey(id))
                {
                    ChangingFloatsDict.Remove(id);
                }
                else if (!m_LastCycleFloatsDict[id].Equals(value)) //本周期与上周期的值不同
                {
                    if (ChangingFloatsDict.ContainsKey(id))
                        ChangingFloatsDict[id] = value;
                    else
                        ChangingFloatsDict.Add(id, value);
                }
            }
        }

        /// <summary>
        /// 课程结束
        /// </summary>
        public void ClassOver()
        {
            m_LastCycleBoolsDict.Clear();
            m_LastCycleFloatsDict.Clear();
            ChangingBoolsDict.Clear();
            ChangingFloatsDict.Clear();
        }

        /// <summary>
        /// 模块数据对象需要的所有bool型数据在BoolList中的编号
        /// </summary>
        public List<int> AllBoolsIdList { get; private set; }

        /// <summary>
        /// 模块数据对象需要的所有float型数据在FloatList中的编号
        /// </summary>
        public List<int> AllFloatsIdList { get; private set; }

        private readonly Dictionary<int, bool> m_LastCycleBoolsDict;

        /// <summary>
        /// 当前周期下模块数据对象中bool值改变的集合
        /// </summary>
        public Dictionary<int, bool> ChangingBoolsDict { get; private set; }

        private readonly Dictionary<int, float> m_LastCycleFloatsDict;

        /// <summary>
        /// 当前周期下模块数据对象中float值改变的集合
        /// </summary>
        public Dictionary<int, float> ChangingFloatsDict { get; private set; }

        public enum DataType
        {
            BoolType,
            FloatType,
        }
    }
}
