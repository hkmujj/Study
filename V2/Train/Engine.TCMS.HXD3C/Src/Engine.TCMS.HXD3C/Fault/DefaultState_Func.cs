using System;
using System.Collections.Generic;

namespace Engine.TCMS.HXD3C.Fault
{
    public partial class DefaultState
    {
        /// <summary>
        /// 记录时间（事件结束）
        /// </summary>
        /// <param name="item"></param>
        private void AddEventOver(ItemInfo item)
        {
            if (m_TheAllDefault.Count < 0) return;
            for (int index = m_TheAllDefault.Count - 1; index >= 0; index--)
            {
                if (m_TheAllDefault[index].m_Code == item.m_Code && m_TheAllDefault[index].m_EventOver == false)
                {
                    ItemInfo tmp = new ItemInfo();
                    tmp = m_TheAllDefault[index];
                    tmp.m_OverTime = item.m_OverTime;
                    tmp.m_EventOver = true;
                    return;
                }
            }
        }

        /// <summary>
        /// 校验方法，判断是否相应的故障结束
        /// </summary>
        /// <param name="nItemInfo"></param>
        /// <param name="nCode"></param>
        /// <returns></returns>
        private int HasTheItem(List<ItemInfo> nItemInfo, string nCode)
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
        /// </summary>
        private void DefaultSort()
        {
            if (m_TheAllDefault.Count > 0)
            {
                m_TheDefaultLevel1.Clear();
                m_TheDefaultLevel2.Clear();
                m_TheDefaultLevel3.Clear();
                foreach (ItemInfo def in m_TheAllDefault)
                {
                    if (def.m_PicType == "1")
                        m_TheDefaultLevel1.Add(def);
                    else if (def.m_PicType == "2")
                        m_TheDefaultLevel2.Add(def);
                    else if (def.m_PicType == "3")
                        m_TheDefaultLevel3.Add(def);
                }

                m_DefaultSort.Clear();
                if (m_TheDefaultLevel1.Count > 0)
                {
                    for (int i = 0; i < m_TheDefaultLevel1.Count; i++)
                    {
                        m_DefaultSort.Add(m_TheDefaultLevel1[i]);
                    }
                }
                if (m_TheDefaultLevel2.Count > 0)
                {
                    for (int i = 0; i < m_TheDefaultLevel2.Count; i++)
                    {
                        m_DefaultSort.Add(m_TheDefaultLevel2[i]);
                    }
                }
                if (m_TheDefaultLevel3.Count > 0)
                {
                    for (int i = 0; i < m_TheDefaultLevel3.Count; i++)
                    {
                        m_DefaultSort.Add(m_TheDefaultLevel3[i]);
                    }
                }
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        private void GetValue()
        {
            if (UIObj.InBoolList.Count == 2)
            {
                int max = UIObj.InBoolList[1] - UIObj.InBoolList[0];
                for (int i = 0; i <= max; i++)
                {
                    m_MemoryIndex[i] = UIObj.InBoolList[0] + i;
                    m_BValue[i] = BoolList[m_MemoryIndex[i]];
                    m_OldbValue[i] = BoolOldList[m_MemoryIndex[i]];

                    //是否有故障
                    if (m_BValue[i] && !m_OldbValue[i])
                    {
                        if (m_FaultList.ContainsKey(m_MemoryIndex[i]))
                        {
                            m_DefaultTmp = new ItemInfo();
                            string[] tmp = m_FaultList[m_MemoryIndex[i]];
                            m_DefaultTmp.m_Code = tmp[1];
                            m_DefaultTmp.m_PicType = tmp[2];
                            m_DefaultTmp.m_FaultName = tmp[3];
                            m_DefaultTmp.m_SolveName = tmp[4];
                            m_DefaultTmp.m_StartTime = m_StartTime;
                            m_DefaultTmp.m_WangYa = m_FValue[0].ToString("0.0") + "kV/";
                            m_DefaultTmp.m_DianLiu = Convert.ToInt32(m_FValue[1]).ToString() + "A";
                            m_DefaultTmp.m_TrainState = Convert.ToInt32(m_FValue[2]).ToString();
                            m_DefaultTmp.m_JiWei = Convert.ToInt32(m_FValue[3]).ToString("0.0") + "级";
                            m_DefaultTmp.m_TrainSpeed = Convert.ToInt32(m_FValue[4]).ToString() + "km/h";

                            m_TheNoOverDefault.Add(m_DefaultTmp);
                            m_TheAllDefault.Add(m_DefaultTmp);
                        }
                    }
                    else if (!m_BValue[i] && m_OldbValue[i])
                    {
                        if (m_FaultList.ContainsKey(m_MemoryIndex[i]))
                        {
                            string[] tmp = m_FaultList[m_MemoryIndex[i]];
                            m_DefaultTmp.m_Code = tmp[1];
                            m_DefaultTmp.m_OverTime = m_OverTime;

                            AddEventOver(m_DefaultTmp);

                            int tmpIndex = HasTheItem(m_TheNoOverDefault, m_DefaultTmp.m_Code);
                            if (tmpIndex > -1)
                            {
                                m_TheNoOverDefault.RemoveAt(tmpIndex);
                            }

                            tmpIndex = HasTheItem(m_TheAllDefault, m_DefaultTmp.m_Code);
                            if (tmpIndex > -1)
                            {
                                ItemInfo tmpItemInfo = m_TheAllDefault[tmpIndex];
                                tmpItemInfo.m_OverTime = m_DefaultTmp.m_OverTime;
                                tmpItemInfo.m_EventOver = true;
                                m_TheAllDefault.RemoveAt(tmpIndex);
                                m_TheAllDefault.Insert(tmpIndex, tmpItemInfo);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < UIObj.InFloatList.Count; i++)
            {
                m_FValue[i] = FloatList[UIObj.InFloatList[i]];
            }

            if (m_TheNoOverDefault.Count > 0) m_IsDefaultOccur = true;
            else m_IsDefaultOccur = false;

            if (m_NeedSort)
                DefaultSort();
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        private void RefreshDate()
        {
            DateTime currenttime = DateTime.Now;
            string hour;
            string min;
            string sec;
            string year;
            string mon;
            string day;

            sec = currenttime.Second.ToString("00") + "秒";
            min = currenttime.Minute.ToString("00");
            hour = currenttime.Hour.ToString("00") + ":";

            year = currenttime.Year.ToString("").Remove(0, 2) + "/";
            mon = currenttime.Month.ToString("00") + "/";
            day = currenttime.Day.ToString("00") + "  ";

            m_StartTime = year + mon + day + hour + min;
            m_OverTime = mon + day + hour + min;
        }
    }
}
