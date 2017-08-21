using System.Drawing;
using MMI.Facility.DataType;
using GT_MMI.Interface.Button;

namespace GT_MMI.Interface.State
{
    internal abstract class StateBase : IState
    {
        protected baseClass m_ViewClass;

        protected IButtonManager m_ButtonManager;

        protected StateBase(baseClass baseClass,IButtonManager buttonManager)
        {
            m_ViewClass = baseClass;
            m_ButtonManager = buttonManager;
        }

        public virtual IState F1Down()
        {
            return this;
        }

        public virtual IState F1Up()
        {
            return this;

        }

        public virtual IState F2Down()
        {
            return this;
        }

        public virtual IState F2Up()
        {
            return this;
        }

        public virtual IState F3Down()
        {
            return this;
        }

        public virtual IState F3Up()
        {
            return this;
        }

        public virtual IState F4Down()
        {
            return this;
        }

        public virtual IState F4Up()
        {
            return this;
        }

        public virtual IState F5Down()
        {
            return this;
        }

        public virtual IState F5Up()
        {
            return this;
        }

        public virtual IState F6Down()
        {
            return this;
        }

        public virtual IState F6Up()
        {
            return this;
        }

        public virtual IState F7Down()
        {
            return this;
        }

        public virtual IState F7Up()
        {
            return this;
        }

        public virtual IState F8Down()
        {
            return this;
        }

        public virtual IState F8Up()
        {
            return this;
        }

        public virtual IState F9Down()
        {
            return this;
        }

        public virtual IState F9Up()
        {
            return this;
        }

        public virtual IState Num1Down()
        {
            return this;
        }

        public virtual IState Num1Up()
        {
            return this;

        }

        public virtual IState Num2Down()
        {
            return this;
        }

        public virtual IState Num2Up()
        {
            return this;
        }

        public virtual IState Num3Down()
        {
            return this;
        }

        public virtual IState Num3Up()
        {
            return this;
        }

        public virtual IState Num4Down()
        {
            return this;
        }

        public virtual IState Num4Up()
        {
            return this;
        }

        public virtual IState Num5Down()
        {
            return this;
        }

        public virtual IState Num5Up()
        {
            return this;
        }

        public virtual IState Num6Down()
        {
            return this;
        }

        public virtual IState Num6Up()
        {
            return this;
        }

        public virtual IState Num7Down()
        {
            return this;
        }

        public virtual IState Num7Up()
        {
            return this;
        }

        public virtual IState Num8Down()
        {
            return this;
        }

        public virtual IState Num8Up()
        {
            return this;
        }

        public virtual IState Num9Down()
        {
            return this;
        }

        public virtual IState Num9Up()
        {
            return this;
        }

        public virtual IState Num0Down()
        {
            return this;
        }

        public virtual IState Num0Up()
        {
            return this;
        }

        public virtual IState VigilantDown()
        {
            return this;
        }

        public virtual IState VigilantUp()
        {
            return this;
        }

        public virtual IState Update()
        {
            return this;
        }

        public virtual void OnPaint(Graphics g)
        {

        }
    }
}
