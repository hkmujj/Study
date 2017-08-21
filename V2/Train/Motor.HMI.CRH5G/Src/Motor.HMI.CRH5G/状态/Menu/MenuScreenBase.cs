using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH5G.底层共用;
using Motor.HMI.CRH5G.底层共用.RectView;

namespace Motor.HMI.CRH5G.Staus.Menu
{
    public abstract class MenuScreenBase : CRH5GBase
    {

        private static Dictionary<ScreenIdentification, SateBottomButtonVisitor> m_SateBottomButtonVisitorDictionary;

    
        protected List<RectangleF> RectsList;
        protected List<MarshallingChangableRectState> TheRectStateList;

        protected string Button10
        {
            get { return m_Button10; }
            set
            {
                if (value == m_Button10)
                {
                    return;
                }
                m_Button10 = value;
                InitalizeButtomBtnContents(SateBottomButtonVisitor);
            }
        }

        public SateBottomButtonVisitor SateBottomButtonVisitor
        {
            get { return m_SateBottomButtonVisitorDictionary[ScreenIdentification]; }
        }


        protected string[] NameArr;

        protected string TextFileIndex;
        private MarshallingType m_CurrentMarshallingType;
        private TrainInside m_CurrenTrainInside;
        private string m_Button10;

        private void SetButton10Value()
        {
            //if (m_CurrentMarshallingType == MarshallingType.SingleMarshalling && (m_CurrenTrainInside == TrainInside.Inside16 || m_CurrenTrainInside == TrainInside.Normal16))
            //{
            //    Button10 = ">>";
            //}
            //else
            //{
                Button10 = string.Empty;
            //}
        }
        public MarshallingType CurrentMarshallingType
        {
            private set
            {
                var old = m_CurrentMarshallingType;
                if (old == value) return;
                m_CurrentMarshallingType = value;
                SetButton10Value();
                UpdateMarshallingView();
            }
            get { return m_CurrentMarshallingType; }
        }

        public TrainInside CurrenTrainInside
        {
            set
            {
                if (m_CurrenTrainInside != value)
                {
                    m_CurrenTrainInside = value;
                    SetButton10Value();
                    UpdateTrainInsideView();
                }

            }
            get { return m_CurrenTrainInside; }
        }

        private void UpdateTrainInsideView()
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

        private void UpdateMarshallingView()
        {
            TheRectStateList.ForEach(e => e.ShowMarshallingType = CurrentMarshallingType);
        }

        public override bool Initalize()
        {
         

            InitalizeVisitorDictionary();

            CurrentMarshallingType = TitleScreen.CurrentSelectedMarshallingType;

            RectsList = Coordinate.RectangleFLists(ViewIDName.MenuScreen);
            TheRectStateList = new List<MarshallingChangableRectState>(180);

            var appConfig = IConfig.AppConfigs.FirstOrDefault(f => f.AppName == ProjectName);
            Debug.Assert(appConfig != null, "appConfig != null");
            ReadFile(Path.Combine(appConfig.AppPaths.ConfigDirectory, "车辆换端信息.txt"));

            return true;
        }

        private void InitalizeVisitorDictionary()
        {
            if (m_SateBottomButtonVisitorDictionary == null)
            {
                m_SateBottomButtonVisitorDictionary = new Dictionary<ScreenIdentification, SateBottomButtonVisitor>();
            }

            if (!m_SateBottomButtonVisitorDictionary.ContainsKey(ScreenIdentification))
            {
                var vistor = new SateBottomButtonVisitor(TitleScreen);
                vistor.ButtonDownEvent += SateBottomButtonVisitorOnButtonDownEvent;
                m_SateBottomButtonVisitorDictionary.Add(ScreenIdentification, vistor);
            }
        }

        private void SateBottomButtonVisitorOnButtonDownEvent(SateBottomButtonVisitor visitor, int btnIndex)
        {

            if (btnIndex < 0)
            {
                return;
            }
            if (!string.IsNullOrEmpty(visitor.ButtonContentCollection[btnIndex]))
            {
                try
                {
                    var page = Convert.ToInt32(visitor.ButtonContentCollection[btnIndex]);
                    visitor.SourceObject.append_postCmd(CmdType.ChangePage, page, 0, 0);
                }
                catch
                {
                    if (visitor.ButtonContentCollection[btnIndex] == "其它")
                    {
                        var page = Convert.ToInt32(visitor.ButtonContentCollection[btnIndex - 1]);
                        visitor.SourceObject.append_postCmd(CmdType.ChangePage, page, 0, 0);
                        visitor.SelectedBtnIndex = 0;
                    }
                    else
                    {
                        visitor.CurrentMenuScreen.TheRectStateList.ForEach(f => f.ChangeIsCar16());
                        visitor.CurrentMenuScreen.Car.IsCar8To16 =
                            visitor.CurrentMenuScreen.TheRectStateList.All(a => a.IsCar16);
                    }
                }
            }
        }

        protected override void ParseLine(int line, string content)
        {
            var strArr = content.Split(new[] { '\t' });
            if (strArr.Length >= 7 && strArr[0].Trim() == TextFileIndex)
            {
                TheRectStateList.Add(new MarshallingChangableRectState(
                    (!string.IsNullOrEmpty(strArr[1]) ? Convert.ToInt32(strArr[1]) : 1),
                    (!string.IsNullOrEmpty(strArr[6]) ? Convert.ToInt32(strArr[6]) : 0),
                    new[]
                    {
                        RectsList[!string.IsNullOrEmpty(strArr[2]) ? Convert.ToInt32(strArr[2]) : 0],
                        RectsList[!string.IsNullOrEmpty(strArr[3]) ? Convert.ToInt32(strArr[3]) : 0],
                        RectsList[!string.IsNullOrEmpty(strArr[4]) ? Convert.ToInt32(strArr[4]) : 0],
                        RectsList[!string.IsNullOrEmpty(strArr[5]) ? Convert.ToInt32(strArr[5]) : 0],
                        RectsList[!string.IsNullOrEmpty(strArr[7]) ? Convert.ToInt32(strArr[7]) : 0],
                        RectsList[!string.IsNullOrEmpty(strArr[8]) ? Convert.ToInt32(strArr[8]) : 0]
                    }, string.IsNullOrEmpty(strArr[1]) ? 0 : Convert.ToInt32(strArr[1])
                    ));
            }
        }

        protected abstract void InitalizeButtomBtnContents(SateBottomButtonVisitor visitor);

        public override sealed void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            CurrentMarshallingType = TitleScreen.CurrentSelectedMarshallingType;

            if (TitleScreen.ChangeLength)
            {
                CurrenTrainInside = TitleScreen.TrainInside ? TrainInside.Inside16 : TrainInside.Normal16;
            }
            else
            {
                CurrenTrainInside = TitleScreen.TrainInside ? TrainInside.Inside8 : TrainInside.Normal8;
            }


            if (nParaA != SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                return;
            }

            SetButton10Value();
            InitalizeButtomBtnContents(SateBottomButtonVisitor);
            SateBottomButtonVisitor.CurrentMenuScreen = this;
            if (nParaB % 3 == 0)
            {
                SateBottomButtonVisitor.SelectedBtnIndex = 2;
            }
            else
            {
                SateBottomButtonVisitor.SelectedBtnIndex = nParaB % 3 - 1;
            }
            // SateBottomButtonVisitor.SelectedBtnIndex = nParaB - 1;
            TitleScreen.AcceptBottomBtnVisitor(SateBottomButtonVisitor);


        }
        private void ChangeTrainNum16()
        {
            TheRectStateList.ForEach(f =>
            {
                f.TrainInsideType = TitleScreen.TrainInside ? TrainInside.Inside16 : TrainInside.Normal16;
                f.TrainNumbIs16 = true;
            });
        }

        protected void ChangeTrainNum8()
        {
            TheRectStateList.ForEach(f =>
            {
                f.TrainInsideType = TitleScreen.TrainInside ? TrainInside.Inside8 : TrainInside.Normal8;
                f.TrainNumbIs16 = false;
            });
        }

        public override sealed void Paint(Graphics g)
        {
            UpdateStates();

            ResponseUser();

            DrawContent(g);
        }

        private void UpdateStates()
        {
            foreach (var index in TheRectStateList)
            {
                index.ChangeRectState(this, BoolList[index.LogicIds[0]],
                    BoolList[index.LogicIds[1]],
                    BoolList[index.LogicIds[2]]);
            }
        }

        protected virtual void ResponseUser()
        {

        }

        private void DrawContent(Graphics g)
        {
            for (var i = 0; i < NameArr.Length; i++)
            {
                g.DrawString(NameArr[i],
                    FontsItems.DefaultFont,
                    SolidBrushsItems.WhiteBrush,
                    RectsList[i],
                    FontsItems.TheAlignment(FontRelated.靠左));
            }
            foreach (var index in TheRectStateList)
            {
                index.DrawRectState(g, this);
            }
        }

    }
}