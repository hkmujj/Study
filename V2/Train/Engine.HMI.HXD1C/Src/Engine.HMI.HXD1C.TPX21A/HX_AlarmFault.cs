using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Engine.HMI.HXD1C.TPX21A
{
    [GksDataType(DataType.isMMIObjectClass)]
    // ReSharper disable once InconsistentNaming
    class HX_AlarmFault:baseClass
    {
        private int m_StarBit;//故障开始位  从配置文件中读取
        private int m_FaultCount;//故障数量 从配置文件中读取
        private readonly SortedList<int, Fault> m_CurrentFaults = new SortedList<int, Fault>();//当前活动故障
        private readonly SortedList<int, Fault> m_SortedFaults = new SortedList<int, Fault>();//排序后的当前活动故障
        private readonly SortedList<int, Image> m_ImageLists = new SortedList<int, Image>();//界面中需要的界面元素
        private Fault m_CurrentFault;//故障警示区当前显示的故障

        private bool m_IsStartScreen;//是否进入开机界面
        private bool m_IsBlackScreen;//是否进入黑屏界面
        private readonly SortedDictionary<int, BrakeInfo> m_BrakeInfoList = new SortedDictionary<int, BrakeInfo>();//制动提示信息
        private readonly SortedList<int, HelpInfo> m_HelpInfoList = new SortedList<int, HelpInfo>();//提示信息区

        private int m_InfoStartBit;
        private int m_InfoCount;

        public override string GetInfo()
        {
            return "活动故障警示信息";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();

            //加载图片
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Image image = Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]);
                m_ImageLists.Add(i, image);
            }

            LoadBrakeInfo();

            LoadInfos();

            nErrorObjectIndex = -1;

            return true;
        }

        private void LoadInfos()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "信息提示区.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all.Skip(1))
            {
                string[] str = cStr.Split(';');
                HelpInfo helpInfo = new HelpInfo
                {
                    m_LogicalBit = Convert.ToInt32(str[1]),
                    m_Info = str[2]
                };
                m_HelpInfoList.Add(m_HelpInfoList.Count, helpInfo);
            }
        }

        private void LoadBrakeInfo()
        {
            var file = Path.Combine(AppPaths.ConfigDirectory, "制动提示信息.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] str = cStr.Split(';');
                BrakeInfo brakeinfo = new BrakeInfo
                {
                    m_LogicalBit = Convert.ToInt32(str[0]),
                    m_Info = str[1]
                };
                m_BrakeInfoList.Add(m_BrakeInfoList.Count, brakeinfo);
            }
        }

        public override void paint(Graphics g)
        {
            GetValue();
            DrawOn(g);
            base.paint(g);
        }

        public void InitData()
        {
            m_StarBit = UIObj.InBoolList[0];
            m_FaultCount = UIObj.InBoolList[1];
        }

        public void GetValue()
        {
            #region 刷新活动故障

            //添加新的
            for (int i = 0; i < HXFaultCommom.m_StaticFaults.Count; i++)
            {
                if (BoolList[HXFaultCommom.m_StaticFaults[i].LogicalBit])
                {
                    if (!m_CurrentFaults.ContainsKey(HXFaultCommom.m_StaticFaults[i].LogicalBit))
                    {
                        Fault fault = new Fault();
                        fault.LogicalBit = HXFaultCommom.m_StaticFaults[i].LogicalBit;
                        fault.FaultID = HXFaultCommom.m_StaticFaults[i].FaultID;
                        fault.FaultText = HXFaultCommom.m_StaticFaults[i].FaultText;
                        fault.Level = HXFaultCommom.m_StaticFaults[i].Level;
                        fault.FaultCategory = HXFaultCommom.m_StaticFaults[i].FaultCategory;
                        fault.HappenedTime = DateTime.Now;
                        m_CurrentFaults.Add(fault.LogicalBit, fault);
                    }
                }
            }

            //删除已经消失的
            foreach (int key in m_CurrentFaults.Keys)
            {
                if (!BoolList[m_StarBit + key])
                {
                    m_CurrentFaults.Remove(key);
                    break;
                }
            }

            /*----------------------------------------------------------------------------
             * 
             * 为了方便索引 将currentFaults 中的存储的故障信息 添加序号索引 并按时间的先后顺序排列；
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
            #endregion

            
            if (m_SortedFaults.Count > 0)
            {
                m_CurrentFault = new Fault();
                m_CurrentFault.FaultCategory = m_SortedFaults[0].FaultCategory;
                m_CurrentFault.Level = m_SortedFaults[0].Level;
            }
            else
            {
                m_CurrentFault = null;
            }

            m_InfoStartBit = UIObj.InBoolList[2];
            m_InfoCount = UIObj.InBoolList[3];

            m_IsStartScreen=BoolList[UIObj.InBoolList[4]];
            m_IsBlackScreen = BoolList[UIObj.InBoolList[5]];
            if (m_IsStartScreen)
            {
                append_postCmd(CmdType.ChangePage, 101, 0, 0);
            }
        }

        public void DrawOn(Graphics g)
        {
            if (m_CurrentFault != null)
            {
                if (DateTime.Now.Second%2==0)
                 {
                      HxCommon.HDefault[2].SetBkColor(255, 0, 0); 
                 }
                else
                 {
                     HxCommon.HDefault[2].SetBkColor(0, 0, 0); 
                 }
               HxCommon.HDefault[2].SetTextColor(255, 255, 255);
               HxCommon.HDefault[2].SetText(m_CurrentFault.FaultCategory);

                switch ((int)m_CurrentFault.Level)
                {
                    case 0:
                        g.DrawImage(m_ImageLists[0], HxCommon.HDefault[3].m_RectPosition.X + 3, HxCommon.HDefault[3].m_RectPosition.Y + 3,
                            HxCommon.HDefault[3].m_RectPosition.Width - 6, HxCommon.HDefault[3].m_RectPosition.Height - 6);
                        break;
                    case 1:
                        g.DrawImage(m_ImageLists[1], HxCommon.HDefault[3].m_RectPosition.X + 3, HxCommon.HDefault[3].m_RectPosition.Y + 3,
                            HxCommon.HDefault[3].m_RectPosition.Width - 6, HxCommon.HDefault[3].m_RectPosition.Height - 6);
                        break;
                    case 2:
                        g.DrawImage(m_ImageLists[2], HxCommon.HDefault[3].m_RectPosition.X + 3, HxCommon.HDefault[3].m_RectPosition.Y + 3,
                            HxCommon.HDefault[3].m_RectPosition.Width - 6, HxCommon.HDefault[3].m_RectPosition.Height - 6);
                        break;
                    case 3:
                        g.DrawImage(m_ImageLists[3], HxCommon.HDefault[3].m_RectPosition.X + 3, HxCommon.HDefault[3].m_RectPosition.Y+3,
                            HxCommon.HDefault[3].m_RectPosition.Width - 6, HxCommon.HDefault[3].m_RectPosition.Height-6);
                        break;
                    default:
                        break;
                }
            }
            else
            {
              HxCommon.HDefault[2].SetText(" ");
              HxCommon.HDefault[2].SetBkColor(0, 0, 0);
            }


            for (int index = 0; index < m_BrakeInfoList.Count; index++)
            {
                if (BoolList[m_BrakeInfoList[index].m_LogicalBit])
                {
                    HxCommon.HDefault[0].SetBkColor(218, 204, 128);
                    HxCommon.HDefault[0].SetText(m_BrakeInfoList[index].m_Info);
                    break;
                }

                if (index == m_BrakeInfoList.Count - 1 && BoolList[m_BrakeInfoList[index].m_LogicalBit] == false)
                {
                    HxCommon.HDefault[0].SetText(" ");
                    HxCommon.HDefault[0].SetBkColor(0, 0, 0);
                }
            }

            for (int index = 0; index < m_HelpInfoList.Count; index++)
            {
                if (BoolList[m_HelpInfoList[index].m_LogicalBit])
                {
                    HxCommon.HDefault[1].SetBkColor(233, 233, 233);
                   
                    HxCommon.HDefault[1].SetText(m_HelpInfoList[index].m_Info);
                    break;
                }

                if (index == m_HelpInfoList.Count - 1 && BoolList[m_HelpInfoList[index].m_LogicalBit]==false)
                {
                    HxCommon.HDefault[1].SetText(" ");
                    HxCommon.HDefault[1].SetBkColor(0, 0, 0);
                }

            }
        }

    }
}