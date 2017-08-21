using System.Drawing;

namespace HXD1.DeepDomestic
{
    public class AllFaultTitleRect
    {
        private Rectangle _mainRect;

        public RectText _type;

        public RectText _number;

        public RectText _code;

        public RectText _describe;

        public RectText _startTime;

        public RectText _endTime;

        public AllFaultTitleRect(Rectangle mainRect, string itstype, string number, string code, string describe, string startTime, string endTime)
        {
            _mainRect = mainRect;
            _type = new RectText(new Rectangle(_mainRect.X, _mainRect.Y, 50, _mainRect.Height), itstype, 12, 1, Color.Red, Color.FromArgb(79, 100, 131), Color.Wheat, 1, false, null, true, true);
            _number = new RectText(new Rectangle(_mainRect.X + 50, _mainRect.Y, 50, _mainRect.Height), number, 12, 1, Color.Black, Color.FromArgb(79, 100, 131), Color.Wheat, 1, false, null, true, true);
            _code = new RectText(new Rectangle(_mainRect.X + 100, _mainRect.Y, 70, _mainRect.Height), code, 12, 1, Color.White, Color.FromArgb(79, 100, 131), Color.Wheat, 1, false, null, true, true);
            _describe = new RectText(new Rectangle(_mainRect.X + 170, _mainRect.Y, 290, _mainRect.Height), describe, 12, 1, Color.Black, Color.FromArgb(79, 100, 131), Color.Wheat, 1, false, null, true, true);
            _startTime = new RectText(new Rectangle(_mainRect.X + 460, _mainRect.Y, 150, _mainRect.Height), startTime, 12, 1, Color.Black, Color.FromArgb(79, 100, 131), Color.Wheat, 1, false, null, true, true);
            _endTime = new RectText(new Rectangle(_mainRect.X + 610, _mainRect.Y, 150, _mainRect.Height), endTime, 12, 1, Color.Black, Color.FromArgb(79, 100, 131), Color.Wheat, 1, false, null, true, true);
        }


        public void OnDraw(Graphics g)
        {
            _type.OnDraw(g);
            _number.OnDraw(g);
            _code.OnDraw(g);
            _describe.OnDraw(g);
            _startTime.OnDraw(g);
            _endTime.OnDraw(g);
        }
    }
}