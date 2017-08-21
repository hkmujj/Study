using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using CommonUtil.Rtf;
using CommonUtil.Util;
using Excel.Interface;
using MMI.Facility.Interface.Data;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Model;
using Motor.ATP.Infrasturcture.Model.Infomation;

namespace Motor.ATP.Infrasturcture.Control.Infomation
{
    public class InfomationCreater : IInformationCreater
    {
        /// <summary>
        /// 只显示文件的消息
        /// </summary>
        private ReadOnlyCollection<IInfomationItemContent> m_OnlyTextInfos;

        private ReadOnlyCollection<IInfomationItemContent> m_PopupViewInfos;

        public event Action<ReadOnlyCollection<IInfomationItemContent>> InfomationCreated;

        public event Action<ReadOnlyCollection<IInfomationItemContent>> InfomationDeleted;

        public event Action<ReadOnlyCollection<IInfomationItemContent>> InfomationEnsured;

        public event Action InfomationCleared;

        private List<IInfomationItemContent> m_ActiviongItemContents;

        private List<IInfomationItemContent> m_ToCreateContents;

        private List<IInfomationItemContent> m_ToDeleteContents;

        private List<IInfomationItemContent> m_ToEnsureContents;

        private readonly GlobalParamBase m_GlobalParam;

        public InfomationCreater(string configFileName, GlobalParamBase globalParam)
        {
            ConfigFileName = configFileName;
            m_GlobalParam = globalParam;
        }

        public string ConfigFileName { private set; get; }

        public void Initalize()
        {
            m_ToCreateContents = new List<IInfomationItemContent>();
            m_ToDeleteContents = new List<IInfomationItemContent>();
            m_ToEnsureContents = new List<IInfomationItemContent>();
            m_ActiviongItemContents = new List<IInfomationItemContent>();
            LoadInfoConfig();
        }

        /// <summary>
        /// 是否有需要计时 的消息
        /// </summary>
        /// <returns></returns>
        public bool HasAnyNeedTimeInfomation()
        {
            return m_OnlyTextInfos.Any(a => a.TimeShowType.NeedTime()) ||
                   m_PopupViewInfos.Any(a => a.TimeShowType.NeedTime());
        }

        public void ClearShowingItems()
        {
            OnInfomationClear();
        }

        public void ClearContents()
        {
            m_ToCreateContents.Clear();
            m_ToDeleteContents.Clear();
            m_ToEnsureContents.Clear();
        }

        public void UpdateInfomation(int infoID, InformationUpdateType updateType = InformationUpdateType.Add)
        {
            foreach (var info in m_OnlyTextInfos)
            {
                if (info.Id == infoID)
                {
                    UpdateContens(info, updateType);
                }
            }
            foreach (var info in m_PopupViewInfos)
            {
                if (info.Id == infoID)
                {
                    UpdateContens(info, updateType);
                }
            }
        }

        /// <summary>
        /// 刷新消息
        /// </summary>
        public void FlushInfomation()
        {
            if (m_ToEnsureContents.Any())
            {
                OnInfomationEnsured(m_ToEnsureContents.ToList().AsReadOnly());
            }
            if (m_ToDeleteContents.Any())
            {
                OnInfomationDeleted(m_ToDeleteContents.ToList().AsReadOnly());
            }
            if (m_ToCreateContents.Any())
            {
                OnInfomationCreated(m_ToCreateContents.ToList().AsReadOnly());
            }
        }


        //更新文本事件信息
        public void UpdateContens(IInfomationItemContent infomation, InformationUpdateType updateType )
        {
            switch (updateType)
            {
                case InformationUpdateType.Add:
                    m_ToCreateContents.Add(infomation);
                    break;
                case InformationUpdateType.Remove:
                    if (infomation.AutoCancelType != InfomationAutoDeleteType.Never)
                    {
                        m_ToDeleteContents.Add(infomation);
                    }
                    break;
                case InformationUpdateType.Ensure:
                    if (!infomation.IsOnlyTextInfo())
                    {
                        m_ToEnsureContents.Add(infomation);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("updateType", updateType, null);
            }
        }

        protected virtual void OnInfomationCreated(ReadOnlyCollection<IInfomationItemContent> obj)
        {
            var handler = InfomationCreated;
            if (handler != null)
            {
                handler(obj);
            }
        }

        protected virtual void OnInfomationDeleted(ReadOnlyCollection<IInfomationItemContent> obj)
        {
            var handler = InfomationDeleted;
            if (handler != null)
            {
                handler(obj);
            }
        }

        private void LoadInfoConfig()
        {

            List<IInfomationItemContent> contens = LoadInfoTable();

            var txts = contens.Where(w => w.IsOnlyTextInfo()).ToList();
            txts.Sort((a, b) => a.Priority.CompareTo(b.Priority));

            m_OnlyTextInfos = txts.AsReadOnly();

            var pop = contens.Where(w => !w.IsOnlyTextInfo()).ToList();
            pop.Sort((a, b) => a.Priority.CompareTo(b.Priority));
            m_PopupViewInfos = pop.AsReadOnly();

        }

        public void OnBoolChanged(object sender, CommunicationDataChangedArgs<bool> dataChangedArgs)
        {

        }

        public void OnFloatChanged(object sender, CommunicationDataChangedArgs<float> dataChangedArgs)
        {

        }

        public void OnDataChanged(object sender, CommunicationDataChangedArgs dataChangedArgs)
        {

        }

        private List<IInfomationItemContent> LoadInfoTable()
        {
            var contens = new List<IInfomationItemContent>();

            var infoConfig = new ExcelReaderConfig
            {
                File = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", ConfigFileName),
                Coloumns = new List<ColoumnConfig>
                {
                    new ColoumnConfig() {Name = "*"},

                },
                SheetNames = new List<string> {"消息通知表"}
            };

            var dt = infoConfig.Adapter();
            var sb = new StringBuilder();
            var rtfWriter = new RtfWriter();

            foreach (DataRow row in dt.Tables[0].Rows)
            {
                var id = Convert.ToInt32(row["ID"]);
                var priority = Convert.IsDBNull(row["消息优先级"]) ? int.MaxValue : Convert.ToInt32(row[1]);
                var showType =
                    EnumUtil.FindEnumByDescriptio<InfomationShowType>(row["E区文本显示方式"].ToString());
                var msgContent = row["E区消息内容"].ToString();
                var responseType =
                    EnumUtil.FindEnumByDescriptio<InfomationResponseType>(row["F区响应方式"].ToString());
                var sysResponse =
                    EnumUtil.FindEnumByDescriptio<InfomationSystemResonseType>(
                        row["F区系统响应方式子类"].ToString());
                var popTitle = row["弹出框标题（F区响应方式不为无或系统时需要）"].ToString();
                var popcontent = row["弹出框内容（F区响应方式不为无或系统时需要）"].ToString();

                var contentAutoCancel = EnumUtil.FindEnumByDescriptio<InfomationAutoDeleteType>(
                    row["文本取消方式"].ToString());

                var okReturn = Convert.IsDBNull(row["确认后返回接口"]) ? int.MaxValue : Convert.ToInt32(row[9]);
                var cancelReturn = Convert.IsDBNull(row["取消后返回接口"]) ? int.MaxValue : Convert.ToInt32(row[10]);

                var nextControltype = dt.Tables[0].Columns.Contains("下一控制模式") && Convert.IsDBNull(row["下一控制模式"])
                    ? EnumUtil.FindEnumByDescriptio<ControlType>(row["下一控制模式"].ToString())
                    : ControlType.Unknown;

                var rtf = RtfHelper.CreateDefaultPopupViewRtf(paragraph =>
                {
                    paragraph.AppendText(new RtfTabCharacter());
                    paragraph.AppendText(popcontent);
                });
                sb.Clear();
                using (var writer = new StringWriter(sb))
                {
                    rtfWriter.Write(writer, rtf);
                }

                var col = "消息的时间显示方式";
                InfomationTimeShowType timeShowType = InfomationTimeShowType.Normal;
                if (dt.Tables[0].Columns.Contains(col))
                {
                    timeShowType = EnumUtil.FindEnumByDescriptio<InfomationTimeShowType>(row[col].ToString());
                }

                contens.Add(new InfomationItemContent
                {
                    Id = id,
                    Priority = priority,
                    ShowType = showType,
                    ResponseType = responseType,
                    MessageContent = msgContent,
                    PopupTitle = popTitle,
                    PopupContentRtf = sb.ToString(),
                    PopupContent = popcontent,
                    PopupContentCodePage = (int)rtf.CodePage,
                    SystemResonseType = sysResponse,
                    AutoCancelType = contentAutoCancel,
                    OkReturnIndex = okReturn,
                    CancelReturnIndex = cancelReturn,
                    TimeShowType = timeShowType,
                    NextControlType = nextControltype,
                });
            }
            return contens;
        }

        protected virtual void OnInfomationClear()
        {
            var handler = InfomationCleared;
            if (handler != null)
            {
                handler();
            }
        }

        protected virtual void OnInfomationEnsured(ReadOnlyCollection<IInfomationItemContent> obj)
        {
            if (InfomationEnsured != null)
            {
                InfomationEnsured(obj);
            }
        }
    }
}
