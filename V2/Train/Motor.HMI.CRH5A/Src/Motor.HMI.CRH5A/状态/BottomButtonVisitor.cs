using System.Collections.ObjectModel;
using System.Drawing;

namespace Motor.HMI.CRH5A.Staus
{
    public abstract class BottomButtonVisitor
    {
        public TitleScreen SourceObject { protected set; get; }

        protected ReadOnlyCollection<RectangleF> ContentRegions;

        protected BottomButtonVisitor(TitleScreen sourceObject)
        {
            SourceObject = sourceObject;
            ContentRegions = sourceObject.GetButtonBtnRegions();
        }

        public virtual void Reset()
        {
            
        }

        public abstract void DrawBtnContent(Graphics graphics);

        public virtual bool OnButtonDown(int btnIndex)
        {
            return false;
        }

        public virtual int SelectedBtnIndex { set; get; }
    }
}