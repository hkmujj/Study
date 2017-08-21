using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using Motor.ATP._200H.Resource;

namespace Motor.ATP._200H
{
       [GksDataType(DataType.isMMIObjectClass)]
    public class TextInfo : baseClass
    {
        public string TriggerTime { get; set; }
        public string Text { get; set; }

        #region  ˽���ֶ�
        private static readonly RectText[] m_GTextTime = new RectText[5];
        private static readonly RectText[] m_GTextInfo = new RectText[5];
        public static SortedList<int, string> m_TotalInfos = new SortedList<int, string>();//���ı��ļ��д洢������е��ı���Ϣ����
        public static List<ConfigInfo> m_ConfigInfos = new List<ConfigInfo>();

        private int m_LastTriggerIndex1 = -1;

        private int m_LastTriggerIndex2 = -1;

        private static readonly SortedList<int, TextModel> m_HappenedInfos = new SortedList<int, TextModel>();

        #endregion

        #region ���ط���
        public override string GetInfo()
        {
            return "�ı���Ϣ������ʾ";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            nErrorObjectIndex = -1;

            LoadTestFile();

            return true;
        }

        private void LoadTestFile()
        {
            var file = Path.Combine(RecPath, "..\\config\\�ı���Ϣ.txt");
            if (File.Exists(file))
            {
                var allLines = File.ReadAllLines(file, Encoding.Default);
                foreach (var str in allLines.Select(s => s.Split(';')))
                {
                    ParserLine(str);
                }
            }
        }

        private void ParserLine(string[] str)
        {
            switch (str.Length)
            {
                case 2:
                    try
                    {
                        m_TotalInfos.Add(Convert.ToInt32(str[0]), str[1]);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                    break;
                case 3:
                    {
                        var configInfo = new ConfigInfo { ReceiveIndex = Convert.ToInt32(str[0]), ShowInfo = str[1], SendIndex = Convert.ToInt32(str[2]) };
                        m_ConfigInfos.Add(configInfo);
                    }
                    break;
            }
        }

        public override void paint(Graphics g)
        {
            GetValue();
            OnDraw(g);
        }
        #endregion

        #region ˽�з���
        private void InitData()
        {
            for (int i = 0; i < 5; i++)
            {
                m_GTextTime[i] = new RectText();
                m_GTextTime[i].SetBkColor(0, 0, 0);
                if (i == 0)
                {
                    m_GTextTime[i].SetTextColor(255, 255, 255);
                }
                else
                {
                    m_GTextTime[i].SetTextColor(90, 90, 101);
                }
                m_GTextTime[i].SetTextStyle(12, FormatStyle.DirectionLeftToRight, true, "����");
                m_GTextTime[i].SetTextRect(Common2D.RectE[i + 4].X + 2, Common2D.RectE[i + 4].Y + 8, 80, Common2D.RectE[i + 4].Height - 12);
            }

            for (int i = 0; i < 5; i++)
            {
                m_GTextInfo[i] = new RectText();
                m_GTextInfo[i].SetBkColor(0, 0, 0);
                if (i == 0)
                {
                    m_GTextInfo[i].SetTextColor(255, 255, 255);
                }
                else
                {
                    m_GTextInfo[i].SetTextColor(90, 90, 101);
                }
                m_GTextInfo[i].SetTextStyle(14, FormatStyle.DirectionLeftToRight, true, "����");
                m_GTextInfo[i].SetTextRect(Common2D.RectE[i + 4].X + 78, Common2D.RectE[i + 4].Y + 6, 200, Common2D.RectE[i + 4].Height - 10);
            }

        }

        public void GetValue()
        {
            #region ˢ�µ�һλ�ı���Ϣ
            int currentTriggerIndex = Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf�ı���Ϣ)]);
            if (m_TotalInfos.ContainsKey(currentTriggerIndex))
            {
                if (currentTriggerIndex != m_LastTriggerIndex1)
                {
                    var textInfo = new TextModel();
                    textInfo.TriggerTime = this.CurrenTime().ToLongTimeString();
                    textInfo.TextInfo = m_TotalInfos[currentTriggerIndex];
                    m_HappenedInfos.Add(m_HappenedInfos.Count, textInfo);
                    m_LastTriggerIndex1 = currentTriggerIndex;
                }
            }
            else
            {
                m_LastTriggerIndex1 = -1;
            }
            #endregion

            #region ˢ�µڶ�λ
            int currentTriggerIndex2 = Convert.ToInt32(FloatList[this.GetInFloatIndex(InFloatKeys.Inf�ı���Ϣ2)]);
            if (m_TotalInfos.ContainsKey(currentTriggerIndex2))//����ǰ���ϼ������иñ�� 
            {
                if (currentTriggerIndex2 != m_LastTriggerIndex2)
                {
                    var textInfo = new TextModel();
                    textInfo.TriggerTime = this.CurrenTime().ToLongTimeString();
                    textInfo.TextInfo = m_TotalInfos[currentTriggerIndex2];
                    m_HappenedInfos.Add(m_HappenedInfos.Count, textInfo);
                    m_LastTriggerIndex2 = currentTriggerIndex2;
                }
            }
            #endregion

            #region �����Ƿ���ȷ��ģʽ���� �ı���
            if (Signal.IsConfigFlag)//����ȷ��ģʽ
            {
                m_GTextTime[0].SetText(string.Empty);
                m_GTextInfo[0].SetText(string.Empty);

                for (int i = m_Index; i < 4 + m_Index; i++)
                {
                    if (i < m_HappenedInfos.Count)
                    {
                        m_GTextTime[i + 1 - m_Index].SetText(m_HappenedInfos[m_HappenedInfos.Count - i - 1].TriggerTime);
                        m_GTextInfo[i + 1 - m_Index].SetText(m_HappenedInfos[m_HappenedInfos.Count - i - 1].TextInfo);
                    }
                    else
                    {
                        m_GTextTime[i + 1 - m_Index].SetText(string.Empty);
                        m_GTextInfo[i + 1 - m_Index].SetText(string.Empty);
                    }
                }
            }
            else
            {
                for (int i = m_Index; i < 5 + m_Index; i++)
                {
                    if (i < m_HappenedInfos.Count)
                    {
                        m_GTextTime[i - m_Index].SetText(m_HappenedInfos[m_HappenedInfos.Count - i - 1].TriggerTime);
                        m_GTextInfo[i - m_Index].SetText(m_HappenedInfos[m_HappenedInfos.Count - i - 1].TextInfo);
                    }
                    else
                    {
                        m_GTextTime[i - m_Index].SetText(string.Empty);
                        m_GTextInfo[i - m_Index].SetText(string.Empty);
                    }
                }
            }
            #endregion
        }

        public void OnDraw(Graphics g)
        {
            for (int i = 0; i < 5; i++)
            {
                m_GTextTime[i].OnDraw(g);
                m_GTextInfo[i].OnDraw(g);
            }
        }
        #endregion

        #region ��̬����
        /// <summary>
        /// ��յ�ǰ������Ϣ��¼
        /// </summary>
        public static void ClearCurrentInforRecords()
        {
            m_HappenedInfos.Clear();
        }

        private static int m_Index;

        private static void SetDisplayText()
        {
            for (int i = m_Index; i < 5 + m_Index; i++)
            {
                m_GTextInfo[i - m_Index].SetText(m_HappenedInfos[m_Index].TextInfo);
                m_GTextTime[i - m_Index].SetText(m_HappenedInfos[m_Index].TriggerTime);
            }
        }
        /// <summary>
        /// ��һ��
        /// </summary>
        public static void Next()
        {
            if (m_HappenedInfos.Count() > 5 + m_Index)
            {
                m_Index++;
            }
            SetDisplayText();
        }

        public static void Last()
        {
            if (m_HappenedInfos.Count() > 5 && m_Index != 0)
            {
                m_Index--;
            }
            SetDisplayText();
        }
        #endregion
    }
}