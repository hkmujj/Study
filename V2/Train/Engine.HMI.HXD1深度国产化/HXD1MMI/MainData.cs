using System.Collections.Generic;
using System.Drawing;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 主要数据
    /// </summary>
    public class MainData
    {
        private string _symbol;
        private Point _point;

        private Point _symbolPoint;

        public RowMainData Bow
        {
            get
            {
                return _bow;
            }
            set
            {
                _bow = value;
            }
        }
        private RowMainData _bow;

        public RowMainData RDC
        {
            get
            {
                return _RDC;

            }
            set
            {
                _RDC = value;
            }
        }
        private RowMainData _RDC;

        public RowMainData TM
        {
            get
            {
                return _TM;

            }
            set
            {
                _TM = value;
            }
        }
        private RowMainData _TM;

        public RowMainData CC
        {
            get
            {
                return _CC;

            }
            set
            {
                _CC = value;
            }
        }
        private RowMainData _CC;

        public RowMainData Model
        {
            get
            {
                return _model;

            }
            set
            {
                _model = value;
            }
        }
        private RowMainData _model;

        public RowMainData Node
        {
            get
            {
                return _node;

            }
            set
            {
                _node = value;
            }
        }
        private RowMainData _node;

        public RowMainData MVB
        {
            get
            {
                return _MVB;

            }
            set
            {
                _MVB = value;
            }
        }
        private RowMainData _MVB;

        public RowMainData WTB
        {
            get
            {
                return _WTB;

            }
            set
            {
                _WTB = value;
            }
        }
        private RowMainData _WTB;
        public MainData(string symbol, Point startPoint)
        {
            _symbol = symbol;
            _point = startPoint;
            _symbolPoint = new Point(_point.X, _point.Y);
            _bow = new RowMainData(new Point(_point.X, _point.Y), "受电弓", new string[] { "", "" }, "主断路器");
            _RDC = new RowMainData(new Point(_point.X, _point.Y + 35), "RDC", new string[] { "", "" }, "",100,180,false);
            _TM = new RowMainData(new Point(_point.X, _point.Y + 70), "TM1/2", new string[] { "", "", "", "" }, "TM3/4", 100, 180, false);
            _CC = new RowMainData(new Point(_point.X, _point.Y + 105), "CCU1", new string[] { "", "" }, "CCU2", 100, 180, false);
            _model = new RowMainData(new Point(_point.X, _point.Y + 140), "机车模式", new string[] { "" }, "", 100, 180, false);
            _node = new RowMainData(new Point(_point.X, _point.Y + 175), "节点编号", new string[] { "" }, "", 100, 180, false);
            _MVB = new RowMainData(new Point(_point.X, _point.Y + 210), "MVB.LnA", new string[] { "", "" }, "MVB.LnB", 100, 180, false);
            _WTB = new RowMainData(new Point(_point.X, _point.Y + 245), "WTB.LnA", new string[] { "", "" }, "WTB.LnB", 100, 180, false);
        }




        public void OnDraw(Graphics g)
        {

            _bow.OnDraw(g);
            _RDC.OnDraw(g);
            _TM.OnDraw(g);
            _CC.OnDraw(g);
            _model.OnDraw(g);
            _node.OnDraw(g);
            _MVB.OnDraw(g);
            _WTB.OnDraw(g);
            g.DrawString(_symbol, Common._16Font, Common.WhiteBrush, _symbolPoint);
        }
    }

    public class RowMainData
    {
        private Point _startPoint;

        private RectText _title;    

        public List<RectText> RectTextList
        {
            get
            {
                return _rectTextList;
            }
            set
            {
                _rectTextList = value;
            }
        }
        private List<RectText> _rectTextList = new List<RectText>();

        private RectText _extra;


        public RowMainData(Point startPoint, string label, string[] data, string extraLabel, int stringLength = 100, int middleLength = 180,bool labelColorIsWhite=true)
        {
            _startPoint = startPoint;
            _title = new RectText(new Rectangle(_startPoint.X, _startPoint.Y, stringLength, 35), label, 14, 2, Color.White, Color.Black, Color.White, 1, false, null, true);

            int length = data.Length;

            Color labelColor;
            if (labelColorIsWhite)
                labelColor = Color.White;
            else
                labelColor = Color.Yellow;

            for (int i = 0; i < length; i++)
            {
                _rectTextList.Add(new RectText(new Rectangle(_startPoint.X + stringLength + middleLength / length * i, _startPoint.Y, middleLength / length, 35), data[i], 14, 1, labelColor, Color.Black, Color.White, 1, true));
            }

            _extra = new RectText(new Rectangle(_startPoint.X + middleLength + stringLength, _startPoint.Y, stringLength, 35), extraLabel, 14, 0, Color.White, Color.Black, Color.White, 1, false, null, true);

        }


        public void OnDraw(Graphics g)
        {
            _title.OnDraw(g);
            _extra.OnDraw(g);
            for (int i = 0; i < _rectTextList.Count; i++)
            {
                _rectTextList[i].OnDraw(g);
            }
        }
    }
}
