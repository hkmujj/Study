using System;
using System.Collections.Generic;
using System.Drawing;

namespace Urban.Wuxi.TMS.故障事件
{
    /// <summary>
    /// 故障结构体
    /// </summary>
    public struct DefaultInfo
    {
        /// <summary>
        /// 故障图标类型
        /// </summary>
        public string m_PicType;

        /// <summary>
        /// 故障代码
        /// </summary>
        public string m_Code;

        /// <summary>
        /// 故障开始时间
        /// </summary>
        public string m_StartTime;

        /// <summary>
        /// 故障结束时间
        /// </summary>
        public string m_OverTime;

        /// <summary>
        /// 故障名称
        /// </summary>
        public string m_FauleName;

        /// <summary>
        /// 故障解决方法
        /// </summary>
        public string m_SolveName;

        /// <summary>
        /// 故障解决方法的图片
        /// </summary>
        public Image m_SolvePic;

        /// <summary>
        /// 发送确认位
        /// </summary>
        public int m_SendCmd;

        /// <summary>
        /// 故障是否结束
        /// </summary>
        public bool m_EventOver;

        /// <summary>
        /// 是否已按确认
        /// </summary>
        public bool m_IsReceiveCmd;
    }

    /// <summary>
    /// 故障类
    /// </summary>
    public class DefaultItemInfo : TMSBaseClass
    {
        /// <summary>
        /// 故障解决方法
        /// </summary>
        /// <param name="e"></param>
        public void DrawTest(Graphics e, Rectangle rect)
        {
            if (m_DefaultSort.Count > 0)
            {
                if (m_DefaultSort[0].m_SolvePic != null)
                {
                    e.DrawImage(m_DefaultSort[0].m_SolvePic, rect);
                }
            }
        }

        /// <summary>
        /// 故障名称
        /// </summary>
        /// <param name="e"></param>
        /// <param name="rect"></param>
        public void DrawTitle(Graphics e, Rectangle rectPic, Font font,
            Brush brush, Rectangle rect, StringFormat strFor)
        {
            if (m_DefaultSort.Count > 0)
            {
                e.DrawImage(DefaultLevelPic(m_DefaultSort[0].m_PicType), rectPic);
                e.DrawString(m_DefaultSort[0].m_FauleName, font, brush, rect, strFor);
                e.DrawString("机车1A端", new Font("宋体", 12, FontStyle.Bold), brush, new Rectangle(640, 70, 100, 30), strFor);
            }
        }

        #region ::::::::::::::::::::::::::::::::: Fun :::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 故障等级相应图片显示
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private Image DefaultLevelPic(string level)
        {
            switch (level)
            {
                case "1":
                    return m_LevelImgs[0];
                case "2":
                    return m_LevelImgs[1];
                case "3":
                    return m_LevelImgs[2];
                default:
                    return m_LevelImgs[2];
            }
        }

        /// <summary>
        /// 记录历史事件（事件发生）[未用]
        /// </summary>
        /// <param name="item"></param>
        private void AddEvent(DefaultInfo item)
        {
            if (m_GradeAllDefault.Count <= 0) m_GradeAllDefault.Add(item);
            else
            {
                bool flag = false;
                for (int index = m_GradeAllDefault.Count - 1; index >= 0; index--)
                {
                    if (m_GradeAllDefault[index].m_Code == item.m_Code)
                    {
                        flag = true;
                        if (m_GradeAllDefault[index].m_EventOver)
                        {
                            m_GradeAllDefault.Add(item);
                        }
                        return;
                    }
                }
                if (!flag)
                {
                    m_GradeAllDefault.Add(item);
                }
            }
        }

        /// <summary>
        /// 记录时间（事件结束）
        /// </summary>
        /// <param name="item"></param>
        private void AddEventOver(DefaultInfo item)
        {
            if (m_GradeAllDefault.Count < 0) return;
            for (int index = m_GradeAllDefault.Count - 1; index >= 0; index--)
            {
                if (m_GradeAllDefault[index].m_Code == item.m_Code && m_GradeAllDefault[index].m_EventOver == false)
                {
                    DefaultInfo tmp = new DefaultInfo();
                    tmp = m_GradeAllDefault[index];
                    tmp.m_OverTime = item.m_OverTime;
                    tmp.m_EventOver = true;
                    return;
                }
            }
        }

        /// <summary>
        /// 临时添加
        /// 判断是否接受到确认
        /// </summary>
        /// <param name="nItemInfo"></param>
        /// <param name="nCode"></param>
        /// <returns></returns>
        private int ReceiveTheItem(List<DefaultInfo> nItemInfo, string nCode)
        {
            for (int tmpIndex = 0; tmpIndex < nItemInfo.Count; tmpIndex++)
            {
                if (nItemInfo[tmpIndex].m_Code.Trim() == nCode.Trim())
                {
                    return tmpIndex;
                }
            }
            return -1;
        }


        /// <summary>
        /// 校验方法，判断是否相应的故障结束
        /// </summary>
        /// <param name="nItemInfo"></param>
        /// <param name="nCode"></param>
        /// <returns></returns>
        private int HasTheItem(List<DefaultInfo> nItemInfo, string nCode)
        {
            for (int tmpIndex = 0; tmpIndex < nItemInfo.Count; tmpIndex++)
            {
                if (nItemInfo[tmpIndex].m_Code.Trim() == nCode.Trim() && nItemInfo[tmpIndex].m_OverTime == null)
                {
                    return tmpIndex;
                }
            }
            return -1;
        }

        /// <summary>
        /// 故障排序
        /// 排过序的当前故障列表清空，重新添加
        /// </summary>
        private void DefaultSort()
        {
            m_TheNoOverDefault1.Clear();
            m_TheNoOverDefault2.Clear();
            m_TheNoOverDefault3.Clear();
            foreach (DefaultInfo def in m_TheCurrentDefault)
            {
                if (!def.m_IsReceiveCmd && def.m_PicType == "1")
                {
                    m_TheNoOverDefault1.Add(def);
                }
                if (!def.m_IsReceiveCmd && def.m_PicType == "2")
                {
                    m_TheNoOverDefault2.Add(def);
                }
                if (!def.m_IsReceiveCmd && def.m_PicType == "3")
                {
                    m_TheNoOverDefault3.Add(def);
                }
            }

            m_DefaultSort.Clear();
            if (m_TheNoOverDefault1.Count > 0)
            {
                for (int i = 0; i < m_TheNoOverDefault1.Count; i++)
                {
                    m_DefaultSort.Add(m_TheNoOverDefault1[i]);
                }
            }
            if (m_TheNoOverDefault2.Count > 0)
            {
                for (int i = 0; i < m_TheNoOverDefault2.Count; i++)
                {
                    m_DefaultSort.Add(m_TheNoOverDefault2[i]);
                }
            }
            if (m_TheNoOverDefault3.Count > 0)
            {
                for (int i = 0; i < m_TheNoOverDefault3.Count; i++)
                {
                    m_DefaultSort.Add(m_TheNoOverDefault3[i]);
                }
            }
        }

        /// <summary>
        /// 故障获取
        /// </summary>
        public void GetDefault()
        {
            if (m_DefValue.Length == m_OldDefValue.Length)
            {
                for (int index = 0; index < m_DefValue.Length; index++)
                {
                    #region :::::::::::::::::::::::::::::: 故障发生 :::::::::::::::::::::::::::::::::::::::
                    if (m_DefValue[index] && !m_OldDefValue[index])
                    {
                        if (m_FaultList.ContainsKey(m_MenmoryAddress[index]))
                        {
                            m_DefaultTmp = new DefaultInfo();
                            string[] tmp = m_FaultList[m_MenmoryAddress[index]];
                            if (m_DefType == 1)
                            {
                                m_DefaultTmp.m_Code = tmp[1];
                                m_DefaultTmp.m_PicType = tmp[2];
                                m_DefaultTmp.m_FauleName = tmp[3];
                                m_DefaultTmp.m_SolveName = tmp[4];
                                m_DefaultTmp.m_SolvePic = m_GetImgs[index];
                                m_DefaultTmp.m_EventOver = false;
                                m_DefaultTmp.m_IsReceiveCmd = false;
                                m_DefaultTmp.m_StartTime = DateTime.Now.ToLongTimeString();
                                m_DefaultTmp.m_SendCmd = Convert.ToInt32(tmp[5]);
                            }
                            else if (m_DefType == 2)
                            {
                            }
                            else if (m_DefType == 3)
                            {
                            }
                            //故障加入所有故障
                            m_GradeAllDefault.Add(m_DefaultTmp);

                            //故障加入未排除故障、未根据等级排序
                            m_TheNoOverDefault.Add(m_DefaultTmp);

                            //故障加入当前故障、未根据等级排序
                            m_TheCurrentDefault.Add(m_DefaultTmp);

                            DefaultSort();
                        }
                    }
                    #endregion

                    #region ::::::::::::::::::::::::::::::: 故障结束 :::::::::::::::::::::::::::::::::::::
                    else if (!m_DefValue[index] && m_OldDefValue[index])
                    {
                        if (!m_DefValue[index] && m_OldDefValue[index])
                        {
                            if (m_FaultList.ContainsKey(m_MenmoryAddress[index]))
                            {
                                string[] tmp = m_FaultList[m_MenmoryAddress[index]];
                                m_DefaultTmp.m_Code = tmp[1];
                                //故障结束时间添加进去
                                m_DefaultTmp.m_OverTime = DateTime.Now.ToLongTimeString();
                                m_DefaultTmp.m_IsReceiveCmd = true;

                                AddEventOver(m_DefaultTmp);

                                //判断故障是否结束，然后从当前故障中删掉
                                int tmpIndex = HasTheItem(m_TheNoOverDefault, m_DefaultTmp.m_Code);
                                if (tmpIndex > -1)
                                {
                                    m_TheNoOverDefault.RemoveAt(tmpIndex);
                                }
                                tmpIndex = HasTheItem(m_TheCurrentDefault, m_DefaultTmp.m_Code);
                                if (tmpIndex > -1)
                                {
                                    m_TheCurrentDefault.RemoveAt(tmpIndex);
                                }

                                //判断故障是否结束，然后从所有故障中找出相应故障
                                //把结束时间填进去
                                //删除当前故障并把带有结束时间的故障添加到那个故障的位置
                                tmpIndex = HasTheItem(m_GradeAllDefault, m_DefaultTmp.m_Code);
                                if (tmpIndex > -1)
                                {
                                    DefaultInfo tmpItemInfo = m_GradeAllDefault[tmpIndex];
                                    tmpItemInfo.m_OverTime = m_DefaultTmp.m_OverTime;
                                    tmpItemInfo.m_IsReceiveCmd = true;
                                    tmpItemInfo.m_EventOver = true;
                                    m_GradeAllDefault.RemoveAt(tmpIndex);
                                    m_GradeAllDefault.Insert(tmpIndex, tmpItemInfo);
                                }

                                DefaultSort();                         
                            }
                        }
                    }
                    #endregion

                    DefaultSort();
                }

                //有序当前故障个数不为0则要在相应位置画故障名称
                if (m_DefaultSort.Count != 0)
                    m_BeDefault = true;
                else
                    m_BeDefault = false;
            }
        }

        /// <summary>
        /// 按确认后
        /// 返回给逻辑当前故障确认
        /// </summary>
        public void SendConfirmation()
        {
            //发送当前按得确认位
            if (m_DefaultSort.Count != 0)
            {
                int tmpIndex = ReceiveTheItem(m_GradeAllDefault, m_DefaultSort[0].m_Code);
                if (tmpIndex > -1)
                {
                    DefaultInfo tmpItemInfo = m_GradeAllDefault[tmpIndex];
                    tmpItemInfo.m_IsReceiveCmd = true;
                    m_GradeAllDefault.RemoveAt(tmpIndex);
                    m_GradeAllDefault.Insert(tmpIndex, tmpItemInfo);
                }

                tmpIndex = ReceiveTheItem(m_TheNoOverDefault, m_DefaultSort[0].m_Code);
                if (tmpIndex > -1)
                {
                    DefaultInfo tmpItemInfo = m_TheNoOverDefault[tmpIndex];
                    tmpItemInfo.m_IsReceiveCmd = true;
                    m_TheNoOverDefault.RemoveAt(tmpIndex);
                    m_TheNoOverDefault.Insert(tmpIndex, tmpItemInfo);
                }

                tmpIndex = ReceiveTheItem(m_TheCurrentDefault, m_DefaultSort[0].m_Code);
                if (tmpIndex > -1)
                {
                    DefaultInfo tmpItemInfo = m_TheCurrentDefault[tmpIndex];
                    tmpItemInfo.m_IsReceiveCmd = true;
                    m_TheCurrentDefault.RemoveAt(tmpIndex);
                    DefaultSort();
                }
            }
        }
        #endregion

        #region ::::::::::::::::::::::::::::::::: init ::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 构造函数
        /// 和谐2机车用
        /// </summary>
        /// <param name="defBValue">故障量</param>
        /// <param name="oldDefBValue">上一周期的故障量</param>
        /// <param name="memoryIndex">故障所在的内存地址</param>
        /// <param name="_FaultList">故障列表</param>
        /// <param name="imgs">故障操作提示用图片</param>
        /// <param name="levelImgs">故障等级图片</param>
        public DefaultItemInfo(ref bool[] defBValue, ref bool[] oldDefBValue,
            ref int[] memoryIndex, SortedList<int, string[]> faultList, Image[] imgs, Image[] levelImgs)
        {
            m_DefValue = defBValue;
            m_OldDefValue = oldDefBValue;
            m_MenmoryAddress = memoryIndex;
            m_FaultList = faultList;
            m_GetImgs = imgs;
            m_LevelImgs = levelImgs;
            m_DefType = 1;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="defBValue">故障量</param>
        /// <param name="oldDefBValue">上一周期的故障量</param>
        /// <param name="memoryIndex">故障所在的内存地址</param>
        /// <param name="_FaultList">故障列表</param>
        /// <param name="imgs">故障操作提示用图片</param>
        public DefaultItemInfo(ref bool[] defBValue, ref bool[] oldDefBValue,
            ref int[] memoryIndex, SortedList<int, string[]> faultList,
            Image[] imgs)
        {
            m_DefValue = defBValue;
            m_OldDefValue = oldDefBValue;
            m_MenmoryAddress = memoryIndex;
            m_FaultList = faultList;
            m_GetImgs = imgs;
            m_DefType = 2;

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="defBValue">故障亮</param>
        /// <param name="oldDefBValue">上一周期的故障量</param>
        /// <param name="memoryIndex">故障所在的内存地址</param>
        /// <param name="_FaultList">故障列表</param>
        /// <param name="_Solution">读文本故障操作提示</param>
        public DefaultItemInfo(ref bool[] defBValue, ref bool[] oldDefBValue,
            ref int[] memoryIndex, SortedList<int, string[]> faultList,
            SortedList<int, string[]> solution)
        {
            m_DefValue = defBValue;
            m_OldDefValue = oldDefBValue;
            m_MenmoryAddress = memoryIndex;
            m_FaultList = faultList;
            m_DefType = 3;
        }

        /// <summary>
        /// 故障类型
        /// 根据构造函数不同而不同
        /// </summary>
        readonly int m_DefType = 0;

        /// <summary>
        /// 当前循环故障状态
        /// </summary>
        readonly bool[] m_DefValue;

        /// <summary>
        /// 前一循环故障状态
        /// </summary>
        readonly bool[] m_OldDefValue;

        /// <summary>
        /// 逻辑号
        /// </summary>
        readonly int[] m_MenmoryAddress;

        /// <summary>
        /// 故障解决方法图片集
        /// </summary>
        public Image[] m_GetImgs;

        /// <summary>
        /// 故障等级图片
        /// </summary>
        public Image[] m_LevelImgs; 

        /// <summary>
        /// 故障实例
        /// </summary>
        DefaultInfo m_DefaultTmp;

        /// <summary>
        /// 当前是否存在故障
        /// </summary>
        public bool m_BeDefault;

        /// <summary>
        /// 故障键值列表
        /// </summary>
        readonly SortedList<int, string[]> m_FaultList = new SortedList<int, string[]>();


        /// <summary>
        /// 所有故障
        /// </summary>
        public List<DefaultInfo> m_GradeAllDefault = new List<DefaultInfo>();

        /// <summary>
        /// 未排除故障
        /// </summary>
        public List<DefaultInfo> m_TheNoOverDefault = new List<DefaultInfo>();

        /// <summary>
        /// 当前故障
        /// </summary>
        public List<DefaultInfo> m_TheCurrentDefault = new List<DefaultInfo>();

        /// <summary>
        /// 未排除1级故障
        /// </summary>
        public List<DefaultInfo> m_TheNoOverDefault1 = new List<DefaultInfo>();

        /// <summary>
        /// 未排除2级故障
        /// </summary>
        public List<DefaultInfo> m_TheNoOverDefault2 = new List<DefaultInfo>();

        /// <summary>
        /// 未排除3级故障
        /// </summary>
        public List<DefaultInfo> m_TheNoOverDefault3 = new List<DefaultInfo>();

        /// <summary>
        /// 排完序的当前故障
        /// </summary>
        public List<DefaultInfo> m_DefaultSort = new List<DefaultInfo>();
        #endregion
    }
}
