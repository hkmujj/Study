using System.Drawing;

namespace HXD1.DeepDomestic
{
    public class FaultAssistInfoRect
    {
        private Rectangle _mainRect;

        public RectText _type;

        public RectText _describe;

        public RectText _v;


        public FaultAssistInfoRect(Rectangle mainRect, string itstype, string describe, string v)
        {
            _mainRect = mainRect;
            _type = new RectText(new Rectangle(_mainRect.X, _mainRect.Y, 200, _mainRect.Height), itstype, 12, 1, Color.Black, Color.FromArgb(182, 195, 209), Color.White, 1, true, null, true, true);
            _describe = new RectText(new Rectangle(_mainRect.X + 200, _mainRect.Y, 380, _mainRect.Height), describe, 12, 0, Color.Black, Color.FromArgb(182, 195, 209), Color.White, 1, true, null, true, true);
            _v = new RectText(new Rectangle(_mainRect.X + 580, _mainRect.Y, 200, _mainRect.Height), v, 12, 0, Color.Black, Color.FromArgb(182, 195, 209), Color.White, 1, true, null, true, true);
        }


        public void OnDraw(Graphics g)
        {
            _type.OnDraw(g);

            _describe.OnDraw(g);

            _v.OnDraw(g);
        }
    }
}