using System.Drawing;
using System.IO;
using System.Timers;
using CommonUtil.Controls.Button;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;
namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MasterRest : HMIBase
    {
        private Image[] m_Img;
        private GDIButton m_Button;
        private static readonly Timer _timer = new Timer(2000);
        protected sealed override bool Initalize()
        {
            m_Img = new Image[2];
            m_Img[0] = Image.FromFile(Path.Combine(RecPath, "frame\\btnWarning.jpg"));
            m_Img[1] = Image.FromFile(Path.Combine(RecPath, "frame\\btnBkNormal.jpg"));
            m_Button = ButtonFactory.CreateButton(new Rectangle(380, 250, 97, 62), GlobleParam.ResManager.GetString("String188"));
            m_Button.ButtonDownEvent += (sender, arg) =>
            {
                m_Button.BackImage = (Bitmap)m_Img[0];
                _timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                _timer.Enabled = true;
            };
            return true;
        }
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            m_Button.BackImage =m_Img[1];
        }
        public override bool mouseUp(Point point)
        {
                m_Button.OnMouseUp(point);
            return true;
        }
        public override bool mouseDown(Point point)
        {
            m_Button.OnMouseDown(point);
            return true;
        }
        public sealed override string GetInfo()
        {
            return "MasterRest";
        }

        public override void paint(Graphics dcGs)
        {
            m_Button.OnDraw(dcGs);
        }
    }
}
