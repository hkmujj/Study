using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using CommonUtil.Model;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5G.Resource;
using Motor.HMI.CRH5G.底层共用;
using Motor.HMI.CRH5G.底层共用.RectView;

namespace Motor.HMI.CRH5G.指令
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class Command : CRH5GBase
    {
        private int m_LineIndex;
        private List<RectangleF> m_ButtonList;
        private int m_ValueIndex;
        private const int MaxCommand = 32;

        private List<RectangleF> m_RectsList;

        private List<CommandRectInfo> m_TheRectStateList;

        private readonly Pen m_GreenPen = new Pen(Color.Green, 2.0f);
        private readonly Pen m_RedPen = new Pen(Color.Red, 2.0f);

        private int m_SendDataId;

        private SelectableRectMoveStrategy m_MoveStrategy;

        private readonly string[] m_NameArr =
        {
            "切除受电弓", "切除牵引变\n压器", "切除牵引变\n流器",
            "切除辅助变\n流器", "切除主压缩\n机"
        };

        //private int m_SelectRectId; //当前选择的矩形框编号
        private TrainInside m_CurrenTrainInside;
        private string m_Button10Str;
        private MarshallingType m_CurrentMarshallingType;
        private Image m_Image;

        private string[] SendLogic = new[]
        {
            OutBoolKeys.OutB切除受电弓3,
            OutBoolKeys.OutB切除受电弓6,
            OutBoolKeys.OutB切除受电弓11,
            OutBoolKeys.OutB切除受电弓14,
            OutBoolKeys.OutB切除牵引变压器3,
            OutBoolKeys.OutB切除牵引变压器6,
            OutBoolKeys.OutB切除牵引变压器11,
            OutBoolKeys.OutB切除牵引变压器14,
            OutBoolKeys.OutB切除牵引变流器1,
            OutBoolKeys.OutB切除牵引变流器2,
            OutBoolKeys.OutB切除牵引变流器5,
            OutBoolKeys.OutB切除牵引变流器7,
            OutBoolKeys.OutB切除牵引变流器8,
            OutBoolKeys.OutB切除牵引变流器9,
            OutBoolKeys.OutB切除牵引变流器10,
            OutBoolKeys.OutB切除牵引变流器13,
            OutBoolKeys.OutB切除牵引变流器15,
            OutBoolKeys.OutB切除牵引变流器16,
            OutBoolKeys.OutB切除辅助变流器1,
            OutBoolKeys.OutB切除辅助变流器2,
            OutBoolKeys.OutB切除辅助变流器5,
            OutBoolKeys.OutB切除辅助变流器7,
            OutBoolKeys.OutB切除辅助变流器8,
            OutBoolKeys.OutB切除辅助变流器9,
            OutBoolKeys.OutB切除辅助变流器10,
            OutBoolKeys.OutB切除辅助变流器13,
            OutBoolKeys.OutB切除辅助变流器15,
            OutBoolKeys.OutB切除辅助变流器16,
            OutBoolKeys.OutB切除主压缩机3,
            OutBoolKeys.OutB切除主压缩机6,
            OutBoolKeys.OutB切除主压缩机11,
            OutBoolKeys.OutB切除主压缩机14,
        };
        public MarshallingType CurrentMarshallingType
        {
            get { return m_CurrentMarshallingType; }
            protected set
            {
                if (value == m_CurrentMarshallingType)
                {
                    return;
                }
                m_CurrentMarshallingType = value;
                UpdateMarshallingView();
                SetButton10Value();
            }
        }

        protected string Button10Str
        {
            get { return m_Button10Str; }
            set
            {
                if (value != null && value == m_Button10Str)
                {
                    return;
                }
                m_Button10Str = value;

            }
        }

        public TrainInside CurrenTrainInside
        {
            get { return m_CurrenTrainInside; }
            protected set
            {
                if (value == m_CurrenTrainInside)
                {
                    return;
                }
                m_CurrenTrainInside = value;
                UpdateTrainView();
                SetButton10Value();
            }
        }

        private void Init()
        {

        }

        public override bool Initalize()
        {
            m_MoveStrategy = new SelectableRectMoveStrategy(this, 5);
            Init();
            m_RectsList = new List<RectangleF>();
            m_RectsList = Coordinate.RectangleFLists(ViewIDName.Command);
            m_TheRectStateList = new List<CommandRectInfo>(180);

            var appConfig = IConfig.AppConfigs.FirstOrDefault(f => f.AppName == ProjectName);
            Debug.Assert(appConfig != null, "appConfig != null");
            ReadFile(Path.Combine(appConfig.AppPaths.ConfigDirectory, "车辆换端信息.txt"));
            m_Image = TitleScreen.CurrentSelectedMarshallingType == MarshallingType.SingleMarshalling
                ? TitleScreen.ImgsList[1]
                : TitleScreen.ImgsList[0];
            return true;
        }

        public override string GetInfo()
        {
            return "指令视图";
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);


            if (TitleScreen.ChangeLength)
            {
                CurrenTrainInside = TitleScreen.TrainInside ? TrainInside.Inside16 : TrainInside.Normal16;
            }
            else
            {
                CurrenTrainInside = TitleScreen.TrainInside ? TrainInside.Inside8 : TrainInside.Normal8;
            }

            CurrentMarshallingType = TitleScreen.CurrentSelectedMarshallingType;

            if (nParaA != 2)
            {
                return;
            }
            if (TitleScreen.ChangeLength)
            {
                ChangeTrainNum16();
            }
            else
            {
                ChangeTrainNum8();
            }

            NotifyInCurrentView();

            m_MoveStrategy.Reset();
        }

        private void UpdateMarshallingView()
        {
            m_TheRectStateList.ForEach(e => e.ShowMarshallingType = CurrentMarshallingType);
        }

        private void GetValue()
        {
            foreach (var index in m_TheRectStateList)
            {
                index.ChangeState(BoolList[index.LogicIndex]);
            }
            if (m_SendDataId != 0 && OutBoolList[m_SendDataId])
            {
                append_postCmd(CmdType.SetBoolValue, m_SendDataId, 0, 0);
            }
        }

        protected override void ParseLine(int line, string content)
        {
            string[] strArr = content.Split(new[] { '\t' });
            if (strArr.Length >= 7 && strArr[0].Trim() == "Command")
            {
                m_TheRectStateList.Add(new CommandRectInfo(
                    (!string.IsNullOrEmpty(strArr[1]) ? Convert.ToInt32(strArr[1]) : 1),
                    (!string.IsNullOrEmpty(strArr[6]) ? Convert.ToInt32(strArr[6]) : 0),
                    new[]
                    {
                        m_RectsList[!string.IsNullOrEmpty(strArr[2]) ? Convert.ToInt32(strArr[2]) : 0],
                        m_RectsList[!string.IsNullOrEmpty(strArr[3]) ? Convert.ToInt32(strArr[3]) : 0],
                        m_RectsList[!string.IsNullOrEmpty(strArr[4]) ? Convert.ToInt32(strArr[4]) : 0],
                        m_RectsList[!string.IsNullOrEmpty(strArr[5]) ? Convert.ToInt32(strArr[5]) : 0],
                        m_RectsList[!string.IsNullOrEmpty(strArr[7]) ? Convert.ToInt32(strArr[7]) : 0],
                        m_RectsList[!string.IsNullOrEmpty(strArr[8]) ? Convert.ToInt32(strArr[8]) : 0]
                    },
                    new[]
                    {
                        Convert.ToInt32(strArr[2]), Convert.ToInt32(strArr[3]),
                        Convert.ToInt32(strArr[4]), Convert.ToInt32(strArr[5])
                    },
                    m_LineIndex < MaxCommand ? GetOutBoolIndex(SendLogic[m_LineIndex]) : 0 //发送位
                    , string.IsNullOrEmpty(strArr[1]) ? 0 : Convert.ToInt32(strArr[1])
                    ));
                m_LineIndex++;
            }
        }

        private void UpdateTrainView()
        {
            if (TitleScreen.ChangeLength)
            {
                ChangeTrainNum16();
            }
            else
            {
                ChangeTrainNum8();
            }
        }

        private void ChangeTrainNum8()
        {
            m_TheRectStateList.ForEach(f =>
            {
                f.TrainInsideType = TitleScreen.TrainInside ? TrainInside.Inside8 : TrainInside.Normal8;
                f.TrainNumbIs16 = false;
            });
        }

        public void ChangeTitleMarshingType()
        {
            TitleScreen.CurrentSelectedMarshallingType = TitleScreen.CurrentSelectedMarshallingType ==
                                                         MarshallingType.DoubleMarshalling
                ? MarshallingType.SingleMarshalling
                : MarshallingType.DoubleMarshalling;
            m_Image = TitleScreen.CurrentSelectedMarshallingType == MarshallingType.SingleMarshalling
                ? TitleScreen.ImgsList[1]
                : TitleScreen.ImgsList[0];
        }

        /// <summary>
        /// 矩形框颜色变化，选中状态为红色
        /// </summary>
        /// <returns></returns>
        private Pen GetTheRectanglePen()
        {
            foreach (var rectInfo in m_TheRectStateList)
            {
                if (rectInfo.GetRectLocal().Equals(m_RectsList[(TitleScreen.ChangeLength ? 99 : 11) + m_MoveStrategy.SelectedRectId]))
                {
                    m_SendDataId = rectInfo.SendDataId;
                    return m_RedPen;
                }

            }
            m_SendDataId = 0;
            return m_GreenPen;
        }

        private void ChangeTrainNum16()
        {
            m_TheRectStateList.ForEach(f =>
            {
                f.TrainInsideType = TitleScreen.TrainInside ? TrainInside.Inside16 : TrainInside.Normal16;
                f.TrainNumbIs16 = true;
            });
        }

        private List<RectangleF> GetRecList()
        {
            if (m_ButtonList == null)
            {
                m_ButtonList = TitleScreen.GetButtonBtnRegions().ToList();
            }
            return m_ButtonList;
        }

        public override void Paint(Graphics g)
        {
            GetValue();

            DrawOn(g);

            if (ButtonsScreen.BtnState == null || ButtonsScreen.BtnState.IsPress)
            {
                return;
            }

            switch (ButtonsScreen.BtnState.BtnId)
            {
                case 11: //上翻键
                    m_MoveStrategy.Move(Direction.Up);
                    break;
                case 12: //下翻键
                    m_MoveStrategy.Move(Direction.Down);
                    break;
                case 21: //左移键
                    m_MoveStrategy.Move(Direction.Left);
                    Car.MoveLast();
                    break;
                case 22: //右移键
                    m_MoveStrategy.Move(Direction.Right);
                    Car.MoveNext();
                    break;
                case 13: //回车键
                    if (m_SendDataId != 0)
                    {
                        append_postCmd(CmdType.SetBoolValue, m_SendDataId, 1, 0);
                    }
                    break;
                case 14:
                    append_postCmd(CmdType.ChangePage, 1, 0, 0);
                    break;
                case 23: // 切换单双组
                    ChangeTitleMarshingType();
                    m_MoveStrategy.Reset();
                    ButtonsScreen.BtnState.Press();
                    Car.m_CurrentIndex = 0;
                    break;
                //case 24:
                //    m_TheRectStateList.ForEach(f => f.IsCar16 = !f.IsCar16);
                //    Car.IsCar8To16 = m_TheRectStateList.All(a => a.IsCar16);
                //    ButtonsScreen.BtnState.Press();
                //    break;
            }

            Car.CurrentSelectedCarIndex = m_MoveStrategy.CurrentSelectedColoumn + 1;
        }

        private void DrawOn(Graphics g)
        {
            for (m_ValueIndex = 0; m_ValueIndex < m_NameArr.Length; m_ValueIndex++)
            {
                g.DrawString(m_NameArr[m_ValueIndex], FontsItems.DefaultFont,
                    SolidBrushsItems.WhiteBrush, m_RectsList[m_ValueIndex],
                    FontsItems.TheAlignment(FontRelated.靠左));
            }
            foreach (var index in m_TheRectStateList)
            {
                index.DrawRectState(g, this);
            }
            g.DrawString(Button10Str, FontsItems.DefaultFont, SolidBrushsItems.GreenBrush1, GetRecList()[9],
                FontsItems.TheAlignment(FontRelated.居中));
            g.DrawImage(m_Image, GetRecList()[8]);
            g.DrawRectangle(GetTheRectanglePen(),
                Rectangle.Round(
                    m_RectsList[
                        (m_CurrentMarshallingType == MarshallingType.DoubleMarshalling

                            ? 99
                            : 11) + m_MoveStrategy.SelectedRectId]));
        }

        /// <summary>
        /// 通知在当前界面，用于评价
        /// </summary>
        protected virtual void NotifyInCurrentView()
        {
            append_postCmd(CmdType.SetBoolValue,
                GetOutBoolIndex(OutBoolKeys.OutBTD屏处于软件切除界面), 1, 0);
        }

        private void SetButton10Value()
        {
            //if (m_CurrentMarshallingType == MarshallingType.SingleMarshalling &&
            //    (m_CurrenTrainInside == TrainInside.Inside16 || m_CurrenTrainInside == TrainInside.Normal16))
            //{
            //    Button10Str = ">>";
            //}
            //else
            //{
                Button10Str = string.Empty;
            //}
        }
    }

}

