using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using Motor.HMI.CRH5G.底层共用;
using Motor.HMI.CRH5G.底层共用.RectView;
using Motor.HMI.CRH5G.电子仪器;

namespace Motor.HMI.CRH5G.Staus.Menu
{
    public class SateBottomButtonVisitor : BottomButtonVisitor
    {
        private int m_SelectedBtnIndex;

        public event Action<SateBottomButtonVisitor, int> ButtonDownEvent;
        /*
                private Car m_Car;
        */
        public List<string> ButtonContentCollection { set; get; }

        public ReadOnlyCollection<int> SelectableIndexCollection { set; get; }

        public override int SelectedBtnIndex
        {
            get { return m_SelectedBtnIndex; }
            set
            {
                m_SelectedBtnIndex = value;
                //if (SelectableIndexCollection.Contains(value))
                //{
                //    m_SelectedBtnIndex = value;
                //}
                //else
                //{
                //    m_SelectedBtnIndex = -1;
                //}
            }
        }

        public MenuScreenBase CurrentMenuScreen { set; get; }
        public ElectronicInstrumentBase CurrentTest { get; set; }
        public SateBottomButtonVisitor(TitleScreen sourceObject)
            : base(sourceObject)
        {

        }

        public override void Reset()
        {

        }

        public override void DrawBtnContent(Graphics graphics)
        {
            var legth = Math.Min(ButtonContentCollection.Count, ContentRegions.Count);
            for (int i = 0; i < legth; i++)
            {
                var btnContent = ButtonContentCollection[i];

                graphics.DrawString(btnContent,
                    FontsItems.DefaultFont,
                    SolidBrushsItems.GreenBrush1,
                    ContentRegions[i],
                    FontsItems.TheAlignment(FontRelated.居中));
            }

            foreach (var region in ContentRegions.Skip(legth))
            {
                graphics.DrawString(string.Empty,
                    FontsItems.DefaultFont,
                    SolidBrushsItems.GreenBrush1,
                    region,
                    FontsItems.TheAlignment(FontRelated.居中));
            }

            //if (SourceObject.ScreenIdentification == ScreenIdentification.ScreenTD)
            {
                var img = SourceObject.CurrentSelectedMarshallingType == MarshallingType.SingleMarshalling ? SourceObject.ImgsList[1] : SourceObject.ImgsList[0];
                graphics.DrawImage(img, ContentRegions[8]);
            }


            if (SelectedBtnIndex != -1)
            {
                graphics.DrawRectangle(PenItems.GetThePen(SolidBrushsItems.RedBrush1, 2f), Rectangle.Round(ContentRegions[SelectedBtnIndex]));
            }
        }

        public override bool OnButtonDown(int btnIndex)
        {
            //if (btnIndex == 8 && SourceObject.ScreenIdentification == ScreenIdentification.ScreenTD)
            if (btnIndex == 8)
            {
                switch (SourceObject.CurrentSelectedMarshallingType)
                {
                    case MarshallingType.SingleMarshalling:
                        SourceObject.CurrentSelectedMarshallingType = MarshallingType.DoubleMarshalling;
                        break;
                    case MarshallingType.DoubleMarshalling:
                        SourceObject.CurrentSelectedMarshallingType = MarshallingType.SingleMarshalling;
                        break;
                }
                SourceObject.Car.CurrentSelectedCarIndex = 1;
                SourceObject.Car.m_CurrentIndex = 0;
                //SourceObject.CurrentSelectedMarshallingType = SourceObject.CurrentSelectedMarshallingType == MarshallingType.SingleMarshalling
                //    ? MarshallingType.DoubleMarshalling
                //    : MarshallingType.SingleMarshalling;
            }
            else if (btnIndex == 6)
            {
                SourceObject.Car.MoveLast();
            }
            else if (btnIndex == 7)
            {
                SourceObject.Car.MoveNext();
            }
            if (btnIndex < 3)
            {
                SelectedBtnIndex = btnIndex;
            }
            OnButtonDownEvent(btnIndex);

            return true;

        }

        private void OnButtonDownEvent(int obj)
        {
            var handler = ButtonDownEvent;
            if (handler != null)
            {
                handler(this, obj);
            }
        }
    }
}