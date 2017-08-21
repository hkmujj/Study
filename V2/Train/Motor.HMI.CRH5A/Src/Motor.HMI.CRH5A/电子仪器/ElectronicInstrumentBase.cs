using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Motor.HMI.CRH5A.底层共用;
using Motor.HMI.CRH5A.底层共用.RectView;

namespace Motor.HMI.CRH5A.电子仪器
{
    public abstract class ElectronicInstrumentBase : CRH5ABase
    {
        protected List<Tuple<string, int>> NameIndexCollection;
        protected Image m_Image;
        protected List<RectangleF> RectsList;
        private List<RectangleF> BtnRecList;
        protected List<MarshallingChangableRectState> TheRectStateList;

        protected string Button10Str
        {
            get { return m_Button10Str; }
            set
            {
                if (value == m_Button10Str)
                {
                    return;
                }
                m_Button10Str = value;

            }
        }

        protected string[] NameArr;
        private TrainInside m_CurrenTrainInside;
        private MarshallingType m_CurrentMarshallingType;
        private string m_Button10Str;

        protected TrainInside CurrenTrainInside
        {
            get { return m_CurrenTrainInside; }
            set
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

        protected MarshallingType CurrentMarshallingType
        {
            get { return m_CurrentMarshallingType; }
            set
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
        private void UpdateMarshallingView()
        {
            TheRectStateList.ForEach(e => e.ShowMarshallingType = CurrentMarshallingType);
        }
        protected bool ChangeLength { get; set; }

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
            if (nParaA != 2) return;

           

            SetButton10Value();

            if (TitleScreen.ChangeLength)
                ChangeTrainNum16();
            else
                ChangeTrainNum8();
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
        private void ChangeTrainNum16()
        {
            foreach (var index in TheRectStateList)
            {
                index.TrainInsideType = TitleScreen.TrainInside ? TrainInside.Inside16 : TrainInside.Normal16;
                index.TrainNumbIs16 = true;
            }
        }

        public void ChangeTrainNum8()
        {
            foreach (var index in TheRectStateList)
            {
                index.TrainInsideType = TitleScreen.TrainInside ? TrainInside.Inside8 : TrainInside.Normal8;
                index.TrainNumbIs16 = false;
            }
        }

        private List<RectangleF> GetRecList()
        {
            if (BtnRecList == null || BtnRecList.Count == 0)
            {
                BtnRecList = TitleScreen.GetButtonBtnRegions().ToList();
            }
            return BtnRecList;
        }
        public void ChangeTitleMarshingType()
        {
            TitleScreen.CurrentSelectedMarshallingType = TitleScreen.CurrentSelectedMarshallingType == MarshallingType.DoubleMarshalling ? MarshallingType.SingleMarshalling : MarshallingType.DoubleMarshalling;
            m_Image = TitleScreen.CurrentSelectedMarshallingType == MarshallingType.SingleMarshalling ? TitleScreen.ImgsList[1] : TitleScreen.ImgsList[0];
        }
        private void SetButton10Value()
        {
            if (m_CurrentMarshallingType == MarshallingType.SingleMarshalling && (m_CurrenTrainInside == TrainInside.Inside16 || m_CurrenTrainInside == TrainInside.Normal16))
            {
                Button10Str = ">>";
            }
            else
            {
                Button10Str = string.Empty;
            }
        }
        public override void Paint(Graphics g)
        {
            for (var i = 0; i < NameArr.Length; i++)
            {
                g.DrawString(NameArr[i],
                    FontsItems.DefaultFont,
                    SolidBrushsItems.WhiteBrush,
                    RectsList[i],
                    FontsItems.TheAlignment(FontRelated.靠左));
            }
            g.DrawString(Button10Str, FontsItems.DefaultFont, SolidBrushsItems.GreenBrush1, GetRecList()[9], FontsItems.TheAlignment(FontRelated.居中));
            g.DrawImage(m_Image, GetRecList()[8]);

            for (int i = 0; i < NameIndexCollection.Count; i++)
            {
                var data = NameIndexCollection[i];
                var region = TheRectStateList[i];

                region.DrawRectState(g, this);

                if (region.HasInit(this))
                {
                    g.DrawString(FloatList[data.Item2].ToString("0.0"),
                        FontsItems.DefaultFont,
                        SolidBrushsItems.YellowBrush1,
                        region.GetRectLocal(this),
                        FontsItems.TheAlignment(FontRelated.居中));
                }
            }
        }

        public override bool Initalize()
        {
            m_Image = TitleScreen.CurrentSelectedMarshallingType == MarshallingType.SingleMarshalling ? TitleScreen.ImgsList[1] : TitleScreen.ImgsList[0];
            return true;
        }
    }
}