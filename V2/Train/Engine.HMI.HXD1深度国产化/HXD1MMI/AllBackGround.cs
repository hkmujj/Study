using System.Drawing;

namespace HXD1.DeepDomestic
{
    public class AllBackGround
    {
        public Rectangle _type;

        public Rectangle _number;

        public Rectangle _code;

        public Rectangle _describe;

        public Rectangle _startTime;

        public Rectangle _endTime;

        public AllBackGround(Rectangle type, Rectangle number, Rectangle code, Rectangle describe, Rectangle startTime, Rectangle endTime)
        {
            _type = type;
            _number = number;
            _code = code;
            _describe = describe;
            _startTime = startTime;
            _endTime = endTime;
        }


        public void OnDraw(Graphics g)
        {
            g.FillRectangle(Common.YellowBrush, _type);
            g.FillRectangle(Common.GrayBrush, _number);
            g.FillRectangle(Common.BlueBrush, _code);
            g.FillRectangle(Common.YellowBrush, _describe);
            g.FillRectangle(Common.YellowBrush, _startTime);
            g.FillRectangle(Common.YellowBrush, _endTime);
        }
    }
}