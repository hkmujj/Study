using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HXFaultCommom : baseClass
    {
        public static SortedList<int, Fault> m_StaticFaults = new SortedList<int, Fault>();//记事本中存储的所有种类的故障信息
        public static SortedList<int, Fault> m_CurrentFaults = new SortedList<int, Fault>();//当前活动故障
        public static SortedList<int, Fault> m_SortedFaults = new SortedList<int, Fault>();//排序后的当前活动故障
        public static SortedList<int, Fault> m_TotalFaults = new SortedList<int, Fault>();//当前所有故障


        public static int m_Num = 10;

        public override string GetInfo()
        {
            return "故障辅助信息";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadFaultInfo();
            nErrorObjectIndex = -1;
            return true;
        }

        public override void paint(System.Drawing.Graphics g)
        {
            GetValue();

            base.paint(g);
        }

        private void LoadFaultInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "故障信息.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                if (cStr.Trim() != "")
                {
                    string[] str = cStr.Split(';');
                    if (str.Length == 9)
                    {
                        Fault fauly = new Fault();
                        fauly.LogicalBit = int.Parse(str[0]);
                        if (str[1].Equals("A"))
                        {
                            fauly.Level = Fault.FaultLevel.A;
                        }
                        else if (str[1].Equals("B"))
                        {
                            fauly.Level = Fault.FaultLevel.B;
                        }
                        else if (str[1].Equals("C"))
                        {
                            fauly.Level = Fault.FaultLevel.C;
                        }
                        else
                        {
                            fauly.Level = Fault.FaultLevel.D;
                        }

                        fauly.FaultCategory = str[2];
                        fauly.FaultID = int.Parse(str[3]);
                        fauly.FaultText = str[4];
                        fauly.CaNum = Int32.Parse(str[5]);
                        fauly.HelpinfoV0 = str[6].Trim().Replace("n", "\n");
                        fauly.HelpinfoV = str[7].Trim().Replace("n", "\n");
                        fauly.ProcedInfo = str[8].Trim().Replace("n", "\n");

                        m_StaticFaults.Add(m_StaticFaults.Count, fauly);
                    }
                }
            }
        }

        private void GetValue()
        {
            //添加新的
            for (int i = 0; i < m_StaticFaults.Count; i++)
            {
                if (BoolList[m_StaticFaults[i].LogicalBit])
                {
                    if (!m_CurrentFaults.ContainsKey(m_StaticFaults[i].LogicalBit))
                    {
                        Fault fault = new Fault();
                        fault.LogicalBit = m_StaticFaults[i].LogicalBit;
                        fault.FaultID = m_StaticFaults[i].FaultID;
                        fault.FaultText = m_StaticFaults[i].FaultText;
                        fault.Level = m_StaticFaults[i].Level;
                        fault.FaultCategory = m_StaticFaults[i].FaultCategory;
                        fault.HappenedTime = DateTime.Now;
                        fault.CaNum = m_StaticFaults[i].CaNum;
                        fault.HelpinfoV = m_StaticFaults[i].HelpinfoV;
                        fault.HelpinfoV0 = m_StaticFaults[i].HelpinfoV0;
                        fault.ProcedInfo = m_StaticFaults[i].ProcedInfo;
                        m_CurrentFaults.Add(fault.LogicalBit, fault);
                    }
                }
            }

            /*----------------------------------------------------------------------------
             * 
             * 为了方便索引 将currentFaults 中的存储的故障信息 添加序号索引
             * 
             *----------------------------------------------------------------------------*/

            m_SortedFaults.Clear();
            int indexs = 0;
            foreach (Fault item in m_CurrentFaults.Values)
            {
                m_SortedFaults.Add(indexs, item);
                indexs++;
            }

            for (int i = 0; i < m_SortedFaults.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (m_SortedFaults[i].HappenedTime.CompareTo(m_SortedFaults[j].HappenedTime) < 0)
                    {
                        Fault temp = m_SortedFaults[i];
                        m_SortedFaults[i] = m_SortedFaults[j];
                        m_SortedFaults[j] = temp;
                    }
                }
            }
            //删除已经消失的
            foreach (int key in m_CurrentFaults.Keys)
            {
                if (!BoolList[key])
                {
                    m_CurrentFaults[key].EndedTime = DateTime.Now;
                    m_TotalFaults.Add(m_TotalFaults.Count, m_CurrentFaults[key]);
                    m_CurrentFaults.Remove(key);
                    //OnFaultRemove(CurrentFaults[key]);//触发事件
                    break;
                }
            }
        }
    }
}
