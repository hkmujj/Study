using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Constant;
using Motor.HMI.CRH3C380B.Resource;
using MsgReceiveMod;

namespace Motor.HMI.CRH3C380B.Base.Fault
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIFault : CRH3C380BBase
    {
        private List<FaultItemViewBase> m_FaultViewCollection;

        private List<FaultContentViewBase> m_FaultContentViewCollection;

        /// <summary>
        /// 发送故障还没有被解决
        /// </summary>

        private Timer m_SendFaultAtiveOfOld;

        private Timer m_ClearSendData;
        private FaultItemViewBase m_CurrentFaultView;
        private Timer m_ResetFaultAtiveOfOld;

        private List<RectangleF> m_RectsList;

        private readonly List<int> m_FaultIdList = new List<int>();

        /// <summary>
        /// 故障字典
        /// </summary>
        private readonly Dictionary<int, string[]> m_FaultDict = new Dictionary<int, string[]>();

        public FaultItemViewBase CurrentFaultView
        {
            get { return m_CurrentFaultView; }
            private set
            {
                if (m_CurrentFaultView != null)
                {
                    value.CurrentSelectedFaultInfo = m_CurrentFaultView.CurrentSelectedFaultInfo;
                }
                m_CurrentFaultView = value;
            }
        }

        public int CurrentViewId { get; private set; }

        public MsgHandlerFault0<FaultInfo> MsgHandler { get; private set; }

        public List<RectangleF> RectsList
        {
            get { return m_RectsList; }
        }

        /// <summary>
        /// 进故障界面前所在视图编号
        /// </summary>
        public int LastViewIdNotFault { get; private set; }

        //2
        public override string GetInfo()
        {
            return "DMI故障";
        }

        //6
        public override bool Initalize()
        {
            m_SendFaultAtiveOfOld = new Timer(SendFaultAtiveOfOld);
            m_SendFaultAtiveOfOld.Change(int.MaxValue, -1);
            m_ResetFaultAtiveOfOld = new Timer(ResetFaultAtiveOfOld);
            m_ClearSendData = new Timer(ClearSendData);
            m_ClearSendData.Change(int.MaxValue, -1);

            InitData();

            m_FaultViewCollection = new List<FaultItemViewBase>
            {
                new FaultViewResume(this, m_RectsList),
                new FaultContentViewEq0(this, m_RectsList),
                new FaultContentViewReport(this, m_RectsList),
                new FaultContentViewVLarger0(this, m_RectsList),
            };

            m_FaultViewCollection[0].NavigateToThis(true);

            m_FaultContentViewCollection = m_FaultViewCollection.Skip(1).Cast<FaultContentViewBase>().ToList();

            m_FaultViewCollection.ForEach(e => e.NavigateToView += OnNavigateToView);

            ResetCurrentFaultView();

            UIObj.InBoolList.AddRange(m_FaultDict.Keys);

            return true;
        }

        private void OnNavigateToView(FaultViewType faultViewType)
        {
            CurrentFaultView = m_FaultViewCollection.First(f => f.ViewType == faultViewType);

            append_postCmd(CmdType.ChangePage, CurrentFaultView.BelongToView, 0, 0);
        }

        private void ResetCurrentFaultView()
        {
            CurrentFaultView = m_FaultViewCollection.First(f => f is FaultViewResume);
        }

        /// <summary>
        /// 重置30S
        /// </summary>
        /// <param name="state"></param>
        private void ResetFaultAtiveOfOld(object state = null)
        {
            m_SendFaultAtiveOfOld.Change(30000, 30000);
            m_ClearSendData.Change(500, -1);
        }

        /// <summary>
        /// 发送30S 间隔提示音
        /// </summary>
        /// <param name="state"></param>
        private void SendFaultAtiveOfOld(object state = null)
        {
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub发送30S间隔提示声音0), 1, 0);
            AppLog.Debug("发送30S间隔提示声音");
            m_ClearSendData.Change(500, -1);
        }

        /// <summary>
        /// 发送新故障提示音
        /// </summary>
        /// <param name="state"></param>
        // ReSharper disable once UnusedParameter.Local
        private void SendFaultActiving(object state = null)
        {
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub发送30S间隔提示声音1), 1, 0);
            AppLog.Debug("发生新故障，发送声音数据！");
            ResetFaultAtiveOfOld();
        }

        /// <summary>
        /// 清除发送的数据
        /// </summary>
        private void ClearSendData(object state = null)
        {
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub发送30S间隔提示声音1), 0, 0);
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub发送30S间隔提示声音0), 0, 0);
            AppLog.Debug("清除发送声音数据！");
        }

        /// <summary>
        /// 读取故障信息
        /// </summary>
        private void ReadFaultFile()
        {
            string file = Path.Combine(RecPath, @"..\config\故障信息.txt");
            string[] allLine = File.ReadAllLines(file, Encoding.Default);
            foreach (var strTmp in allLine.Select(cStr => cStr.Split(new[] {'\t'})))
            {
                int intTmp;
                if (strTmp.Length == 10 && int.TryParse(strTmp[0], out intTmp))
                {
                    m_FaultDict.Add(intTmp, strTmp);
                }
            }
        }

        public override void Paint(Graphics g)
        {
            Update();

            CurrentFaultView.OnFaultChanged();

            if (m_FaultViewCollection.Any(a => a.BelongToView == CurrentViewId))
            {
                CurrentFaultView.OnDraw(g);

            }
            CurrentFaultView.ResponseUser();

            DrawFlashFaultIfNeed(g);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                var lastView = Convert.ToInt32(nParaC);

                if (lastView == 0 || lastView == 1)
                {
                    MsgHandler.ClearAllList(); //课程开始，清空消息列表
                    m_FaultIdList.Clear();
                    //ResetCurrentFaultView();
                }
                else if (NotInFaultView(lastView))
                {
                    LastViewIdNotFault = lastView;
                }

                if (NotInFaultView(nParaB))
                {
                    return;
                }

                CurrentFaultView.NavigateToThis(NotInFaultView(lastView));

                if (NotInFaultContentView(nParaB))
                {
                    return;
                }

                if (CurrentFaultView != null && CurrentFaultView.CurrentSelectedFaultInfo != null)
                {
                    MsgHandler.MsgBeRead(CurrentFaultView.CurrentSelectedFaultInfo.MsgLogicID);
                    append_postCmd(CmdType.SetBoolValue, CurrentFaultView.CurrentSelectedFaultInfo.MsgSendLogicID, 1, 0);
                }

            }
            else
            {
                CurrentViewId = nParaB;
            }
        }

        private bool NotInFaultContentView(int viewId)
        {
            return m_FaultContentViewCollection.All(a => a.BelongToView != viewId);
        }

        private bool NotInFaultView(int viewId)
        {
            return m_FaultViewCollection.All(a => a.BelongToView != viewId);
        }

        private void Update()
        {
            UpdateFault();

            FirPolice();
        }

        /// <summary>
        /// 火灾报警声音
        /// </summary>
        private void FirPolice()
        {
            var tmp = MsgHandler.CurrentMsgList.FindAll(f => f.MsgID.Equals("1214")); //根据故障代码取火灾故障集合
            if (tmp.Count != 0 && tmp.Any(a => !a.TheMsgFlag))
            {
                append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub火灾报警声音控制位), 1, 0);
            }
            else
            {
                append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub火灾报警声音控制位), 0, 0);
            }
        }

        private void UpdateFault()
        {
            foreach (var faultIdx in m_FaultDict.Keys)
            {
                if (!m_FaultIdList.Contains(faultIdx) && BoolList[faultIdx])
                {
                    var faultInfo = new FaultInfo
                    {
                        MsgLogicID = faultIdx,
                        CarriageID = m_FaultDict[faultIdx][1],
                        FaultTypeName = m_FaultDict[faultIdx][2],
                        MsgID = m_FaultDict[faultIdx][3],
                        FaultName = m_FaultDict[faultIdx][5],
                        MsgContent = m_FaultDict[faultIdx][6].Replace("\\n", "\n"),
                        VelocityIsnot0 = m_FaultDict[faultIdx][7].Replace("\\n", "\n"),
                        VelocityIs0 = m_FaultDict[faultIdx][8].Replace("\\n", "\n"),
                        MsgSendLogicID = ParserSendLogicId(faultIdx)
                    };

                    append_postCmd(CmdType.SetBoolValue, faultInfo.MsgSendLogicID, 0, 0); //新消息到达，故障已读标志重置


                    SendFaultActiving();

                    MsgHandler.AddNewMsg(faultInfo, CurrentTime);
                    MsgHandler.CurrentMsgListSort(SortCriteriaOfMsg.TimeDown);
                    m_FaultIdList.Add(faultIdx);
                }
                else if (m_FaultIdList.Contains(faultIdx) && !BoolList[faultIdx])
                {
                    append_postCmd(CmdType.SetBoolValue, ParserSendLogicId(faultIdx), 0, 0);
                    //消息结束，故障已读标志重置
                    MsgHandler.MsgOver(faultIdx);
                    m_FaultIdList.Remove(faultIdx);
                }

                //同步故障
                if (MsgHandler.CurrentMsgList.Count != 0 && OutBoolList[ParserSendLogicId(faultIdx)])
                {
                    var fault = MsgHandler.CurrentMsgList.FirstOrDefault(f => f.MsgLogicID == faultIdx);
                    if (fault != null)
                    {
                        fault.TheMsgFlag = true;
                    }
                }

                if (!m_FaultIdList.Any() || MsgHandler.CurrentMsgList.All(a => a.TheMsgFlag))
                {
                    StopSendActiveTimer();
                }
            }
        }

        private int ParserSendLogicId(int faultIdx)
        {
            var v = Convert.ToInt32(m_FaultDict[faultIdx][9]);

            return this.NeedFix4800() ? v - LogicInterface.VirOutBoolStartIndex : v;
        }


        private void DrawFlashFaultIfNeed(Graphics g)
        {
            #region ::::::::::::::: 右下角闪烁故障 ::::::::::::::

            bool isVisible = false;
            if (MsgHandler.CurrentMsgList.Count == 0)
            {
                SetOutBool(OutBoolKeys.Oub是否还有未确认的故障发生着, 0);
                return;
            }

            foreach (var tmp in MsgHandler.CurrentMsgList.OrderBy(o => o.AffirmTime))
            {
                switch (DMITitle.CurrentMMIName)
                {
                    case MMIType.左侧MMI屏:
                        if (!tmp.TheMsgFlag)
                        {
                            DrawOwn(g, tmp);
                            isVisible = true;
                            break;
                        }

                        if (IsLeftScreen)
                        {
                            DrawFaultOther(g);
                        }
                        else if (!IsLeftScreen && !IsGoTime(tmp.AffirmTime, CurrentTime))
                        {
                            DrawOwn(g, tmp);
                        }
                        else if (!IsLeftScreen && IsGoTime(tmp.AffirmTime, CurrentTime))
                        {
                            DrawFaultOther(g);
                        }
                        break;
                    case MMIType.右侧MMI屏:
                        if (!tmp.TheMsgFlag)
                        {
                            DrawOwn(g, tmp);
                            isVisible = true;
                            break;
                        }

                        if (IsRightScreen)
                        {
                            DrawFaultOther(g);
                        }
                        if (!IsRightScreen && !IsGoTime(tmp.AffirmTime, CurrentTime))
                        {
                            DrawOwn(g, tmp);
                        }
                        else if (!IsRightScreen && IsGoTime(tmp.AffirmTime, CurrentTime))
                        {
                            DrawFaultOther(g);
                        }
                        break;
                }

                if (isVisible)
                {
                    break;
                }
            }

            #endregion
        }


        private void DrawOwn(Graphics g, FaultInfo tmp)
        {
            SetOutBool(OutBoolKeys.Oub是否还有未确认的故障发生着, 1);

            g.FillRectangle(CurrentTime.Second%2 == 0
                ? SolidBrushsItems.YellowBrush
                : SolidBrushsItems.BlackBrush,
                m_RectsList[147]);
            g.DrawString("故障: " + tmp.FaultTypeName + " " + tmp.CarriageID,
                FontsItems.FontC11,
                CurrentTime.Second%2 == 0 ? SolidBrushsItems.BlackBrush : SolidBrushsItems.YellowBrush,
                m_RectsList[147],
                FontsItems.TheAlignment(FontRelated.靠左));
        }

        private void DrawFaultOther(Graphics g)
        {
            SetOutBool(OutBoolKeys.Oub是否还有未确认的故障发生着, 0);
            g.FillRectangle(SolidBrushsItems.YellowBrush, m_RectsList[147]);
            g.DrawString("已知故障",
                FontsItems.FontC11,
                SolidBrushsItems.BlackBrush,
                m_RectsList[147],
                FontsItems.TheAlignment(FontRelated.靠左));
        }

        private bool IsGoTime(DateTime t1, DateTime t2)
        {
            TimeSpan ts1 = new TimeSpan(t1.Ticks);
            TimeSpan ts2 = new TimeSpan(t2.Ticks);
            TimeSpan ts = ts2.Subtract(ts1);
            int second = ts.Days*86400 + ts.Hours*3600 + ts.Minutes*60 + ts.Seconds;
            return second >= 45;
        }

        private void InitData()
        {
            ReadFaultFile();

            if (!Coordinate.ClassIsInited)
            {
                Coordinate.InitData();
            }

            if (Coordinate.RectangleFLists(ViewIDName.DMIFault, ref m_RectsList))
            {
            }

            MsgHandler = new MsgHandlerFault0<FaultInfo>();
        }

        public override void Clear()
        {
            StopSendActiveTimer();
        }

        private void StopSendActiveTimer()
        {
            m_SendFaultAtiveOfOld.Change(int.MaxValue, -1);
            m_ResetFaultAtiveOfOld.Change(int.MaxValue, -1);
        }
    }
}