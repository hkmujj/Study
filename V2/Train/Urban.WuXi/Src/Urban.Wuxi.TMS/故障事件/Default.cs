using System;
using System.Collections.Generic;
using System.Drawing;

namespace Urban.Wuxi.TMS.�����¼�
{
    /// <summary>
    /// ���Ͻṹ��
    /// </summary>
    public struct DefaultInfo
    {
        /// <summary>
        /// ����ͼ������
        /// </summary>
        public string m_PicType;

        /// <summary>
        /// ���ϴ���
        /// </summary>
        public string m_Code;

        /// <summary>
        /// ���Ͽ�ʼʱ��
        /// </summary>
        public string m_StartTime;

        /// <summary>
        /// ���Ͻ���ʱ��
        /// </summary>
        public string m_OverTime;

        /// <summary>
        /// ��������
        /// </summary>
        public string m_FauleName;

        /// <summary>
        /// ���Ͻ������
        /// </summary>
        public string m_SolveName;

        /// <summary>
        /// ���Ͻ��������ͼƬ
        /// </summary>
        public Image m_SolvePic;

        /// <summary>
        /// ����ȷ��λ
        /// </summary>
        public int m_SendCmd;

        /// <summary>
        /// �����Ƿ����
        /// </summary>
        public bool m_EventOver;

        /// <summary>
        /// �Ƿ��Ѱ�ȷ��
        /// </summary>
        public bool m_IsReceiveCmd;
    }

    /// <summary>
    /// ������
    /// </summary>
    public class DefaultItemInfo : TMSBaseClass
    {
        /// <summary>
        /// ���Ͻ������
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
        /// ��������
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
                e.DrawString("����1A��", new Font("����", 12, FontStyle.Bold), brush, new Rectangle(640, 70, 100, 30), strFor);
            }
        }

        #region ::::::::::::::::::::::::::::::::: Fun :::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// ���ϵȼ���ӦͼƬ��ʾ
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
        /// ��¼��ʷ�¼����¼�������[δ��]
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
        /// ��¼ʱ�䣨�¼�������
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
        /// ��ʱ���
        /// �ж��Ƿ���ܵ�ȷ��
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
        /// У�鷽�����ж��Ƿ���Ӧ�Ĺ��Ͻ���
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
        /// ��������
        /// �Ź���ĵ�ǰ�����б���գ��������
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
        /// ���ϻ�ȡ
        /// </summary>
        public void GetDefault()
        {
            if (m_DefValue.Length == m_OldDefValue.Length)
            {
                for (int index = 0; index < m_DefValue.Length; index++)
                {
                    #region :::::::::::::::::::::::::::::: ���Ϸ��� :::::::::::::::::::::::::::::::::::::::
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
                            //���ϼ������й���
                            m_GradeAllDefault.Add(m_DefaultTmp);

                            //���ϼ���δ�ų����ϡ�δ���ݵȼ�����
                            m_TheNoOverDefault.Add(m_DefaultTmp);

                            //���ϼ��뵱ǰ���ϡ�δ���ݵȼ�����
                            m_TheCurrentDefault.Add(m_DefaultTmp);

                            DefaultSort();
                        }
                    }
                    #endregion

                    #region ::::::::::::::::::::::::::::::: ���Ͻ��� :::::::::::::::::::::::::::::::::::::
                    else if (!m_DefValue[index] && m_OldDefValue[index])
                    {
                        if (!m_DefValue[index] && m_OldDefValue[index])
                        {
                            if (m_FaultList.ContainsKey(m_MenmoryAddress[index]))
                            {
                                string[] tmp = m_FaultList[m_MenmoryAddress[index]];
                                m_DefaultTmp.m_Code = tmp[1];
                                //���Ͻ���ʱ����ӽ�ȥ
                                m_DefaultTmp.m_OverTime = DateTime.Now.ToLongTimeString();
                                m_DefaultTmp.m_IsReceiveCmd = true;

                                AddEventOver(m_DefaultTmp);

                                //�жϹ����Ƿ������Ȼ��ӵ�ǰ������ɾ��
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

                                //�жϹ����Ƿ������Ȼ������й������ҳ���Ӧ����
                                //�ѽ���ʱ�����ȥ
                                //ɾ����ǰ���ϲ��Ѵ��н���ʱ��Ĺ�����ӵ��Ǹ����ϵ�λ��
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

                //����ǰ���ϸ�����Ϊ0��Ҫ����Ӧλ�û���������
                if (m_DefaultSort.Count != 0)
                    m_BeDefault = true;
                else
                    m_BeDefault = false;
            }
        }

        /// <summary>
        /// ��ȷ�Ϻ�
        /// ���ظ��߼���ǰ����ȷ��
        /// </summary>
        public void SendConfirmation()
        {
            //���͵�ǰ����ȷ��λ
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
        /// ���캯��
        /// ��г2������
        /// </summary>
        /// <param name="defBValue">������</param>
        /// <param name="oldDefBValue">��һ���ڵĹ�����</param>
        /// <param name="memoryIndex">�������ڵ��ڴ��ַ</param>
        /// <param name="_FaultList">�����б�</param>
        /// <param name="imgs">���ϲ�����ʾ��ͼƬ</param>
        /// <param name="levelImgs">���ϵȼ�ͼƬ</param>
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
        /// ���캯��
        /// </summary>
        /// <param name="defBValue">������</param>
        /// <param name="oldDefBValue">��һ���ڵĹ�����</param>
        /// <param name="memoryIndex">�������ڵ��ڴ��ַ</param>
        /// <param name="_FaultList">�����б�</param>
        /// <param name="imgs">���ϲ�����ʾ��ͼƬ</param>
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
        /// ���캯��
        /// </summary>
        /// <param name="defBValue">������</param>
        /// <param name="oldDefBValue">��һ���ڵĹ�����</param>
        /// <param name="memoryIndex">�������ڵ��ڴ��ַ</param>
        /// <param name="_FaultList">�����б�</param>
        /// <param name="_Solution">���ı����ϲ�����ʾ</param>
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
        /// ��������
        /// ���ݹ��캯����ͬ����ͬ
        /// </summary>
        readonly int m_DefType = 0;

        /// <summary>
        /// ��ǰѭ������״̬
        /// </summary>
        readonly bool[] m_DefValue;

        /// <summary>
        /// ǰһѭ������״̬
        /// </summary>
        readonly bool[] m_OldDefValue;

        /// <summary>
        /// �߼���
        /// </summary>
        readonly int[] m_MenmoryAddress;

        /// <summary>
        /// ���Ͻ������ͼƬ��
        /// </summary>
        public Image[] m_GetImgs;

        /// <summary>
        /// ���ϵȼ�ͼƬ
        /// </summary>
        public Image[] m_LevelImgs; 

        /// <summary>
        /// ����ʵ��
        /// </summary>
        DefaultInfo m_DefaultTmp;

        /// <summary>
        /// ��ǰ�Ƿ���ڹ���
        /// </summary>
        public bool m_BeDefault;

        /// <summary>
        /// ���ϼ�ֵ�б�
        /// </summary>
        readonly SortedList<int, string[]> m_FaultList = new SortedList<int, string[]>();


        /// <summary>
        /// ���й���
        /// </summary>
        public List<DefaultInfo> m_GradeAllDefault = new List<DefaultInfo>();

        /// <summary>
        /// δ�ų�����
        /// </summary>
        public List<DefaultInfo> m_TheNoOverDefault = new List<DefaultInfo>();

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public List<DefaultInfo> m_TheCurrentDefault = new List<DefaultInfo>();

        /// <summary>
        /// δ�ų�1������
        /// </summary>
        public List<DefaultInfo> m_TheNoOverDefault1 = new List<DefaultInfo>();

        /// <summary>
        /// δ�ų�2������
        /// </summary>
        public List<DefaultInfo> m_TheNoOverDefault2 = new List<DefaultInfo>();

        /// <summary>
        /// δ�ų�3������
        /// </summary>
        public List<DefaultInfo> m_TheNoOverDefault3 = new List<DefaultInfo>();

        /// <summary>
        /// ������ĵ�ǰ����
        /// </summary>
        public List<DefaultInfo> m_DefaultSort = new List<DefaultInfo>();
        #endregion
    }
}
