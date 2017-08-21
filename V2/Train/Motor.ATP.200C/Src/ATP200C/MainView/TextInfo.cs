using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ATP200C.ButtonEventHandler;
using ATP200C.Common;
using ATP200C.MainView.FunctionButton;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Extension;
using MMI.Facility.Interface.Service;

namespace ATP200C.MainView
{
    /// <summary>
    /// �ı���Ϣ��ʾ��
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class TextInfo : ATPBaseClass, IDataListener
    {
        private readonly GtRectText[] m_MessageTexts = new GtRectText[5];

        /// <summary>
        /// ���ı��ļ��д洢������е��ı���Ϣ����
        /// </summary>
        private readonly SortedList<int, string> m_TotalInfos = new SortedList<int, string>();

        private readonly List<ConfigInfo> m_ConfigInfos = new List<ConfigInfo>();

        private int _lastTriggerIndex1 = -1;

        private int _lastTriggerIndex2 = -1;

        private GtRectText m_ConfirmingTextInfo;
        private Pen m_ConfirmInfoPen;

        /// <summary>
        /// ÿ�е������ʾ����
        /// </summary>
        private const int MaxCountPerLine = 10;


        public override string GetInfo()
        {
            return "�ı���Ϣ������ʾ";
        }

        public override bool Initalize()
        {
            TextInfoManager.Instance.MaxShowCount = m_MessageTexts.Length;
            m_ConfirmInfoPen = new Pen(Color.FromArgb(255, 255, 0), 1);

            for (int i = 4; i >= 0; i--)
            {
                m_MessageTexts[i] = new GtRectText();
                m_MessageTexts[i].SetBkColor(0, 0, 0);
                m_MessageTexts[i].SetTextColor(90, 90, 101);
                m_MessageTexts[i].SetTextStyle(12, FormatStyle.DirectionLeftToRight, true, "����");
                m_MessageTexts[i].SetTextRect(ATP200CCommon.RectE[i + 4].X + 2, ATP200CCommon.RectE[i + 4].Y + 8, 280,
                    ATP200CCommon.RectE[i + 4].Height - 12);
            }

            m_MessageTexts.First().SetTextColor(255, 255, 255);

            m_ConfirmingTextInfo = new GtRectText();
            m_ConfirmingTextInfo.SetBkColor(0, 0, 0);
            m_ConfirmingTextInfo.SetTextColor(255, 255, 255);
            m_ConfirmingTextInfo.SetTextStyle(14, FormatStyle.Center, true, "����");
            m_ConfirmingTextInfo.SetTextRect(ATP200CCommon.RectE[4].X + 2, ATP200CCommon.RectE[4].Y + 3, 220,
                ATP200CCommon.RectE[4].Height - 6);
            m_ConfirmingTextInfo.SetLinePen(255, 255, 0, 1);

            LoadTextFile();

            UIObj.InBoolList.AddRange(m_ConfigInfos.Select(s => s.ReceiveIndex));

            this.RegistDataListener(this);

            return true;
        }

        private void ReadServiceOnDataChanged(object sender, CommunicationDataChangedArgs communicationDataChangedArgs)
        {
            foreach (var configInfo in m_ConfigInfos)
            {
                if (communicationDataChangedArgs.ChangedBools.NewValue.ContainsKey(configInfo.ReceiveIndex))
                {
                    if (communicationDataChangedArgs.ChangedBools.NewValue[configInfo.ReceiveIndex])
                    {
                        TextInfoManager.Instance.CurrentConfirmingInfo = configInfo;
                        ConfigInfo info = configInfo;
                        ConfirmSignalEventButtonEventHandler.Confirmed = obj =>
                        {
                            TextInfoManager.Instance.CurrentConfirmingInfo = null;
                            obj.append_postCmd(CmdType.SetBoolValue, info.SendIndex, 1, 0);
                            FunctionButtonView.Instance.RequestNavigateBack();
                        };
                        FunctionButtonView.Instance.RequestNavigate(9);
                    }
                    else
                    {
                        append_postCmd(CmdType.SetBoolValue, configInfo.SendIndex, 1, 0);
                    }
                }
            }
        }

        private void LoadTextFile()
        {
            var file = Path.Combine(RecPath, "..\\config\\�ı���Ϣ.txt");
            if (File.Exists(file))
            {
                var contents = File.ReadAllLines(file, Encoding.Default);

                for (int i = 0; i < contents.Length; i++)
                {
                    var content = contents[i];
                    if (string.IsNullOrEmpty(content.Trim()))
                    {
                        LogMgr.Warn(string.Format("Can not parser string {0} in file {1}  line : {2}", content, file, i));
                        return;
                    }

                    ParserLine(content, file, i);
                }
            }
        }

        private void ParserLine(string content, string file, int lineIndex)
        {
            var str = content.Split(';');
            switch (str.Length)
            {
                case 2:
                    try
                    {
                        m_TotalInfos.Add(Convert.ToInt32(str[0]), str[1]);
                    }
                    catch (Exception)
                    {
                        LogMgr.Error(string.Format("Can not parser string {0} in file {1}  line : {2}", content, file,
                            lineIndex));
                    }
                    break;
                case 3:
                    var configInfo = new ConfigInfo
                    {
                        ReceiveIndex = Convert.ToInt32(str[0]),
                        ShowInfo = str[1],
                        SendIndex = Convert.ToInt32(str[2])
                    };
                    m_ConfigInfos.Add(configInfo);
                    break;
                default:
                    LogMgr.Warn(string.Format("Can not parser string {0} in file {1}  line : {2}", content, file,
                        lineIndex));
                    break;
            }
        }

        public override void paint(Graphics g)
        {
            RefreshState();

            OnDraw(g);
        }

        public void RefreshState()
        {
            RefreshText();

            UpdateTextContents();

        }

        private void RefreshText()
        {
            var currentTriggerIndex = Convert.ToInt32(FloatList[UIObj.InFloatList[0]]);
            if (m_TotalInfos.ContainsKey(currentTriggerIndex))
            {
                if (currentTriggerIndex != _lastTriggerIndex1)
                {
                    var textInfo = new Text_Info
                    {
                        TriggerTime = CurrentTime.ToLongTimeString(),
                        TextInfo = m_TotalInfos[currentTriggerIndex]
                    };
                    TextInfoManager.Instance.Add(textInfo);
                    _lastTriggerIndex1 = currentTriggerIndex;
                }
            }
            else
            {
                _lastTriggerIndex1 = -1;
            }

            var currentTriggerIndex2 = Convert.ToInt32(FloatList[UIObj.InFloatList[1]]);
            if (m_TotalInfos.ContainsKey(currentTriggerIndex2)) //����ǰ���ϼ������иñ�� 
            {
                if (currentTriggerIndex2 != _lastTriggerIndex2)
                {
                    var textInfo = new Text_Info
                    {
                        TriggerTime = CurrentTime.ToLongTimeString(),
                        TextInfo = m_TotalInfos[currentTriggerIndex2]
                    };
                    TextInfoManager.Instance.Add(textInfo);
                    _lastTriggerIndex2 = currentTriggerIndex2;
                }
            }
        }

        private void UpdateTextContents()
        {
            var index = 0;

            if (TextInfoManager.Instance.IsConfirming)
            {
                UpdateTextContent(0, null);
                index = 1;
            }

            index = TextInfoManager.Instance.CurrentShowTextInfoCollection.Reverse().Aggregate(index, UpdateTextContent);

            for (int i = index; i < m_MessageTexts.Length; i++)
            {
                UpdateTextContent(i, null);
            }

            UpdataFirstTextColor();
        }

        private void UpdataFirstTextColor()
        {
            var last = TextInfoManager.Instance.CurrentShowTextInfoCollection.LastOrDefault();

            if (TextInfoManager.Instance.CanGoForward)
            {
                m_MessageTexts[0].SetTextColor(90, 90, 101);
                m_MessageTexts[1].SetTextColor(90, 90, 101);
            }
            else
            {
                m_MessageTexts.First().SetTextColor(255, 255, 255);
                if (last != null && last.TextInfo.Length > MaxCountPerLine)
                {
                    m_MessageTexts[1].SetTextColor(255, 255, 255);
                }
                else
                {
                    m_MessageTexts[1].SetTextColor(90, 90, 101);
                }
            }
        }

        private int UpdateTextContent(int index, Text_Info txt)
        {
            if (index >= m_MessageTexts.Length)
            {
                return index;
            }

            if (txt == null)
            {
                m_MessageTexts[index].SetText(string.Empty);
            }
            else
            {
                if (txt.TextInfo.Length <= MaxCountPerLine)
                {
                    m_MessageTexts[index].SetText(string.Format("{0} {1}", txt.TriggerTime, txt.TextInfo));
                }
                else
                {
                    m_MessageTexts[index].SetText(string.Format("{0} {1}", txt.TriggerTime,
                        new string(txt.TextInfo.Take(MaxCountPerLine).ToArray())));
                    if (index + 1 < m_MessageTexts.Length)
                    {
                        m_MessageTexts[++index].SetText(string.Format("  {0}",
                            new String(txt.TextInfo.Skip(MaxCountPerLine).ToArray())));
                    }
                }
            }

            return index + 1;
        }

        public void OnDraw(Graphics g)
        {
            for (int i = 0; i < 5; i++)
            {
                m_MessageTexts[i].OnDraw(g);
            }

            DrawConfirmingInfo(g);
        }


        private void DrawConfirmingInfo(Graphics g)
        {
            if (TextInfoManager.Instance.IsConfirming)
            {
                if (TextInfoManager.Instance.CurrentConfirmingInfo != null)
                {
                    m_ConfirmingTextInfo.SetText(TextInfoManager.Instance.CurrentConfirmingInfo.ShowInfo);
                }
                m_ConfirmingTextInfo.OnDraw(g);
                if (CurrentTime.Second%2 == 0)
                {

                    g.DrawRectangle(m_ConfirmInfoPen, m_ConfirmingTextInfo.RectPosition);
                }
            }
        }


        /// <summary>
        /// ��յ�ǰ������Ϣ��¼
        /// </summary>
        public static void ClearCurrentInforRecords()
        {
            TextInfoManager.Instance.Clear();
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2 && (int) nParaC == 11)
            {
                ClearCurrentInforRecords();
            }
        }

        /// <summary>bool ֵ�仯</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {
        }

        /// <summary>floatֵ�仯</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {
        }

        /// <summary>dataֵ�仯</summary>
        /// <param name="sender"></param>
        /// <param name="dataChangedArgs"></param>
        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {
            ReadServiceOnDataChanged(sender, dataChangedArgs);
        }
    }
}
