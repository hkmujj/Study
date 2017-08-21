using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 控制系统
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class ControlSystem : baseClass
    {
        private TrainControlSystem _self;
        private TrainControlSystem _other;

        public override string GetInfo()
        {
            return "控制系统";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            _self = new TrainControlSystem(ControlState.Self, BoolList);
            _other = new TrainControlSystem(ControlState.Other, BoolList);
            return true;
        }

        private void GetValue()
        {

        }

        public override void paint(Graphics g)
        {
            GetValue();

            _self.OnDraw(g);
            _other.OnDraw(g);
        }
    }

    public enum ControlState
    {
        Self,
        Other
    }

    public class TrainControlSystem
    {
        private const int WIDTH = 800;
        private ControlState _state;

        private IReadOnlyDictionary<int, bool> _boolList;

        #region 左侧的线


        private Point _DX11Point1To;
        private Point _DX11Point2To;



        private Point _DI12Point1To;
        private Point _DI12Point2To;



        private Point _AX13Point1To;
        private Point _AX13Point2To;



        private Point _IDUPoint1To;
        private Point _IDUPoint2To;

        #endregion

        #region 中间的点
        private Point _CCU2Point1To;
        private Point _CCU2Point2To;

        private Point _CentrePoint1From;
        private Point _CentrePoint1To;

        private Point _CentrePoint2From;
        private Point _CentrePoint2To;
        #endregion

        public TrainControlSystem(ControlState state, IReadOnlyDictionary<int, bool> boollist)
        {
            _state = state;

            _boolList = boollist;

            if (state == ControlState.Self)
            {

                #region 矩形
                _DX11 = new BoolRectText(new Rectangle(25, 70, 50, 30), "DX11", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 901, 919);

                _DI12 = new BoolRectText(new Rectangle(25, 140, 50, 30), "DI12", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 902, 920);
                _TCU1 = new BoolRectText(new Rectangle(80, 140, 50, 30), "TCU1", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 903, 921);
                _CCU1 = new BoolRectText(new Rectangle(135, 140, 50, 30), "CCU1", 11, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 904, 922);
                _CCU2 = new BoolRectText(new Rectangle(190, 140, 50, 30), "CCU2", 11, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 905, 923);

                _AX13 = new BoolRectText(new Rectangle(25, 205, 50, 30), "AX13", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 906, 924);




                _IDU = new BoolRectText(new Rectangle(25, 290, 50, 30), "IDU", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 907, 925);



                _TCU2 = new BoolRectText(new Rectangle(80, 290, 50, 30), "TCU2", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 908, 926);
                _ERM = new BoolRectText(new Rectangle(135, 290, 50, 30), "ERM", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 909, 927);
                _DX38 = new BoolRectText(new Rectangle(190, 290, 50, 30), "DX38", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 910, 928);
                _DI37 = new BoolRectText(new Rectangle(245, 290, 50, 30), "DI37", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 911, 929);
                _BCU = new BoolRectText(new Rectangle(315, 290, 50, 30), "BCU", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 912, 930);


                _DX31 = new BoolRectText(new Rectangle(2, 405, 50, 30), "DX31", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 913, 931);
                _DX32 = new BoolRectText(new Rectangle(62, 405, 50, 30), "DX32", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 914, 932);
                _DX33 = new BoolRectText(new Rectangle(122, 405, 50, 30), "DX33", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 915, 933);
                _DX34 = new BoolRectText(new Rectangle(182, 405, 50, 30), "DX34", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 916, 934);
                _DX35 = new BoolRectText(new Rectangle(242, 405, 50, 30), "DX35", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 917, 935);
                _DX36 = new BoolRectText(new Rectangle(302, 405, 50, 30), "DX36", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 918, 936);

                #endregion

                #region 左侧的线


                _DX11Point1To = new Point(15, 80);
                _DX11Point2To = new Point(10, 90);



                _DI12Point1To = new Point(15, 150);
                _DI12Point2To = new Point(10, 160);


                _AX13Point1To = new Point(15, 215);
                _AX13Point2To = new Point(10, 225);



                _IDUPoint1To = new Point(15, 300);
                _IDUPoint2To = new Point(10, 310);
                #endregion

                #region 中间的点
                _CentrePoint1From = new Point(15, 250);
                _CentrePoint1To = new Point(390, 250);

                _CentrePoint2From = new Point(10, 260);
                _CentrePoint2To = new Point(380, 260);
                #endregion
            }
            else
            {
                #region 矩形
                _DX11 = new BoolRectText(new Rectangle(WIDTH - 75, 70, 50, 30), "DX11", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 937, 955);



                _DI12 = new BoolRectText(new Rectangle(WIDTH - 75, 140, 50, 30), "DI12", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true,_boolList, 938, 956);
                _TCU1 = new BoolRectText(new Rectangle(WIDTH - 130, 140, 50, 30), "TCU1", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true,_boolList, 939, 957);
                _CCU1 = new BoolRectText(new Rectangle(WIDTH - 185, 140, 50, 30), "CCU1", 11, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true,_boolList, 940, 958);
                _CCU2 = new BoolRectText(new Rectangle(WIDTH - 240, 140, 50, 30), "CCU2", 11, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true,_boolList, 941, 959);

                _AX13 = new BoolRectText(new Rectangle(WIDTH - 75, 205, 50, 30), "AX13", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true,_boolList, 942, 960);



                _IDU = new BoolRectText(new Rectangle(WIDTH - 75, 290, 50, 30), "IDU", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 943, 961);

                _TCU2 = new BoolRectText(new Rectangle(WIDTH - 130, 290, 50, 30), "TCU2", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 944, 962);
                _ERM = new BoolRectText(new Rectangle(WIDTH - 185, 290, 50, 30), "ERM", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 945, 963);
                _DX38 = new BoolRectText(new Rectangle(WIDTH - 240, 290, 50, 30), "DX38", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 946, 964);
                _DI37 = new BoolRectText(new Rectangle(WIDTH - 295, 290, 50, 30), "DI37", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 947, 965);
                _BCU = new BoolRectText(new Rectangle(WIDTH - 365, 290, 50, 30), "BCU", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 948, 966);


                _DX31 = new BoolRectText(new Rectangle(WIDTH - 52, 405, 50, 30), "DX31", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 949, 967);
                _DX32 = new BoolRectText(new Rectangle(WIDTH - 112, 405, 50, 30), "DX32", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 950, 968);
                _DX33 = new BoolRectText(new Rectangle(WIDTH - 172, 405, 50, 30), "DX33", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 951, 969);
                _DX34 = new BoolRectText(new Rectangle(WIDTH - 232, 405, 50, 30), "DX34", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 952, 970);
                _DX35 = new BoolRectText(new Rectangle(WIDTH - 292, 405, 50, 30), "DX35", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 953, 971);
                _DX36 = new BoolRectText(new Rectangle(WIDTH - 352, 405, 50, 30), "DX36", 12, 1, Color.Black, Common.ControlColor, Color.White, 2, true, null, true, _boolList, 954, 972);

                #endregion

                #region 左侧的线


                _DX11Point1To = new Point(WIDTH - 15, 80);
                _DX11Point2To = new Point(WIDTH - 10, 90);


                _DI12Point1To = new Point(WIDTH - 15, 150);
                _DI12Point2To = new Point(WIDTH - 10, 160);


                _AX13Point1To = new Point(WIDTH - 15, 215);
                _AX13Point2To = new Point(WIDTH - 10, 225);


                _IDUPoint1To = new Point(WIDTH - 15, 300);
                _IDUPoint2To = new Point(WIDTH - 10, 310);
                #endregion

                #region 中间的点
                _CentrePoint1From = new Point(WIDTH - 15, 250);
                _CentrePoint1To = new Point(WIDTH - 390, 250);

                _CentrePoint2From = new Point(WIDTH - 10, 260);
                _CentrePoint2To = new Point(WIDTH - 380, 260);
                #endregion
            }


            #region 中间的点
            _CCU2Point1To = new Point(_CCU2.PointList[5].X, 250);
            _CCU2Point2To = new Point(_CCU2.PointList[4].X, 260);
            #endregion
        }

        public RectText _DX11;


        public RectText _DI12;
        public RectText _TCU1;
        public RectText _CCU1;
        public RectText _CCU2;


        public RectText _AX13;


        public RectText _IDU;
        public RectText _TCU2;
        public RectText _ERM;
        public RectText _DX38;
        public RectText _DI37;
        public RectText _BCU;

        public RectText _DX31;
        public RectText _DX32;
        public RectText _DX33;
        public RectText _DX34;
        public RectText _DX35;
        public RectText _DX36;



        public void OnDraw(Graphics g)
        {
            #region juxing

            _DX11.OnDraw(g);
            _DI12.OnDraw(g);
            _TCU1.OnDraw(g);
            _CCU1.OnDraw(g);
            _CCU2.OnDraw(g);

            _AX13.OnDraw(g);

            _IDU.OnDraw(g);
            _TCU2.OnDraw(g);
            _ERM.OnDraw(g);
            _DX38.OnDraw(g);
            _DI37.OnDraw(g);
            _BCU.OnDraw(g);

            _DX31.OnDraw(g);
            _DX32.OnDraw(g);
            _DX33.OnDraw(g);
            _DX34.OnDraw(g);
            _DX35.OnDraw(g);
            _DX36.OnDraw(g);

            #endregion
            #region 中间
            g.DrawLine(Common.GreenPen, _TCU1.PointList[4], _TCU2.PointList[1]);
            g.DrawLine(Common.GreenPen, _TCU1.PointList[5], _TCU2.PointList[0]);

            g.DrawLine(Common.GreenPen, _CCU1.PointList[4], _ERM.PointList[1]);
            g.DrawLine(Common.GreenPen, _CCU1.PointList[5], _ERM.PointList[0]);

            g.DrawLine(Common.GreenPen, _CCU2.PointList[4], _CCU2Point2To);
            g.DrawLine(Common.GreenPen, _CCU2.PointList[5], _CCU2Point1To);


            g.DrawLine(Common.GreenPen, _CentrePoint1From, _CentrePoint1To);
            g.DrawLine(Common.GreenPen, _CentrePoint2From, _CentrePoint2To);

            #endregion

            #region ziti
            if (_state == ControlState.Self)
            {
                #region 左侧的
                g.DrawLine(Common.GreenPen, _DX11.PointList[7], _DX11Point1To);
                g.DrawLine(Common.GreenPen, _DX11.PointList[6], _DX11Point2To);

                g.DrawLine(Common.GreenPen, _DI12.PointList[7], _DI12Point1To);
                g.DrawLine(Common.GreenPen, _DI12.PointList[6], _DI12Point2To);

                g.DrawLine(Common.GreenPen, _AX13.PointList[7], _AX13Point1To);
                g.DrawLine(Common.GreenPen, _AX13.PointList[6], _AX13Point2To);

                g.DrawLine(Common.GreenPen, _IDU.PointList[7], _IDUPoint1To);
                g.DrawLine(Common.GreenPen, _IDU.PointList[6], _IDUPoint2To);

                g.DrawLine(Common.GreenPen, _DX11Point1To, _IDUPoint1To);
                g.DrawLine(Common.GreenPen, _DX11Point2To, _IDUPoint2To);
                #endregion

                g.DrawString("本车", Common._14Font, Common.WhiteBrush, new Point(160, 450));
            }
            else
            {
                #region 左侧的
                g.DrawLine(Common.GreenPen, _DX11.PointList[2], _DX11Point1To);
                g.DrawLine(Common.GreenPen, _DX11.PointList[3], _DX11Point2To);

                g.DrawLine(Common.GreenPen, _DI12.PointList[2], _DI12Point1To);
                g.DrawLine(Common.GreenPen, _DI12.PointList[3], _DI12Point2To);

                g.DrawLine(Common.GreenPen, _AX13.PointList[2], _AX13Point1To);
                g.DrawLine(Common.GreenPen, _AX13.PointList[3], _AX13Point2To);

                g.DrawLine(Common.GreenPen, _IDU.PointList[2], _IDUPoint1To);
                g.DrawLine(Common.GreenPen, _IDU.PointList[3], _IDUPoint2To);

                g.DrawLine(Common.GreenPen, _DX11Point1To, _IDUPoint1To);
                g.DrawLine(Common.GreenPen, _DX11Point2To, _IDUPoint2To);
                #endregion
                g.DrawString("它车", Common._14Font, Common.WhiteBrush, new Point(WIDTH - 180, 450));
            }
            #endregion

            #region xian


            g.DrawLine(Common.GreenPen, _DX31.PointList[0], new Point(_DX31.PointList[0].X, _DX31.PointList[0].Y - 20));
            g.DrawLine(Common.GreenPen, _DX31.PointList[1], new Point(_DX31.PointList[1].X, _DX31.PointList[1].Y - 30));

            g.DrawLine(Common.GreenPen, _DX32.PointList[0], new Point(_DX32.PointList[0].X, _DX32.PointList[0].Y - 20));
            g.DrawLine(Common.GreenPen, _DX32.PointList[1], new Point(_DX32.PointList[1].X, _DX32.PointList[1].Y - 30));

            g.DrawLine(Common.GreenPen, _DX33.PointList[0], new Point(_DX33.PointList[0].X, _DX33.PointList[0].Y - 20));
            g.DrawLine(Common.GreenPen, _DX33.PointList[1], new Point(_DX33.PointList[1].X, _DX33.PointList[1].Y - 30));

            g.DrawLine(Common.GreenPen, _DX35.PointList[0], new Point(_DX35.PointList[0].X, _DX35.PointList[0].Y - 20));
            g.DrawLine(Common.GreenPen, _DX35.PointList[1], new Point(_DX35.PointList[1].X, _DX35.PointList[1].Y - 30));

            g.DrawLine(Common.GreenPen, _DX36.PointList[0], new Point(_DX36.PointList[0].X, _DX36.PointList[0].Y - 20));
            g.DrawLine(Common.GreenPen, _DX36.PointList[1], new Point(_DX36.PointList[1].X, _DX36.PointList[1].Y - 30));

            g.DrawLine(Common.GreenPen, new Point(_DX31.PointList[0].X, _DX31.PointList[0].Y - 20), new Point(_state == ControlState.Self ? 390 : WIDTH - 390, _DX31.PointList[0].Y - 20));
            g.DrawLine(Common.GreenPen, new Point(_DX31.PointList[1].X, _DX31.PointList[1].Y - 30), new Point(_state == ControlState.Self ? 380 : WIDTH - 380, _DX31.PointList[1].Y - 30));

            g.DrawLine(Common.GreenPen, new Point(_state == ControlState.Self ? 390 : WIDTH - 390, 250), new Point(_state == ControlState.Self ? 390 : WIDTH - 390, _DX31.PointList[0].Y - 20));
            g.DrawLine(Common.GreenPen, new Point(_state == ControlState.Self ? 380 : WIDTH - 380, 260), new Point(_state == ControlState.Self ? 380 : WIDTH - 380, _DX31.PointList[1].Y - 30));

            g.DrawLine(Common.GreenPen, _DI37.PointList[4], new Point(_DI37.PointList[4].X, _DX31.PointList[0].Y - 30));
            g.DrawLine(Common.GreenPen, _DI37.PointList[5], new Point(_DI37.PointList[5].X, _DX31.PointList[0].Y - 20));

            g.DrawLine(Common.GreenPen, _BCU.PointList[4], new Point(_BCU.PointList[4].X, _DX31.PointList[0].Y - 30));
            g.DrawLine(Common.GreenPen, _BCU.PointList[5], new Point(_BCU.PointList[5].X, _DX31.PointList[0].Y - 20));

            g.DrawLine(Common.GreenPen, _DX38.PointList[4], new Point(_DX38.PointList[4].X, _DX34.PointList[0].Y));
            g.DrawLine(Common.GreenPen, _DX38.PointList[5], new Point(_DX38.PointList[5].X, _DX34.PointList[0].Y));


            g.DrawLine(Common.GreenBoldPen, _CCU1.PointList[0], new Point(_CCU1.PointList[0].X, _DX11.PointList[3].Y));
            g.DrawLine(Common.GreenBoldPen, _CCU1.PointList[1], new Point(_CCU1.PointList[1].X, _DX11.PointList[2].Y));

            g.DrawLine(Common.GreenBoldPen, _CCU2.PointList[0], new Point(_CCU2.PointList[0].X, _DX11.PointList[3].Y));
            g.DrawLine(Common.GreenBoldPen, _CCU2.PointList[1], new Point(_CCU2.PointList[1].X, _DX11.PointList[2].Y));

            g.DrawLine(Common.GreenBoldPen, new Point(_state == ControlState.Self ? 110 : WIDTH - 110, _DX11.PointList[2].Y), new Point(400, _DX11.PointList[2].Y));
            g.DrawLine(Common.GreenBoldPen, new Point(_state == ControlState.Self ? 110 : WIDTH - 110, _DX11.PointList[3].Y), new Point(400, _DX11.PointList[3].Y));
            #endregion

            g.DrawString("WTB_LineA", Common._12Font, Common.WhiteBrush, new Point(250, 45));
            g.DrawString("WTB_LineB", Common._12Font, Common.WhiteBrush, new Point(260, 110));

            g.DrawString("MVB_LineA", Common._12Font, Common.WhiteBrush, new Point(50, 330));
            g.DrawString("MVB_LineB", Common._12Font, Common.WhiteBrush, new Point(610, 330));

            g.DrawString("MVB_LineB", Common._12Font, Common.WhiteBrush, new Point(360, 390));

        }
    }












}
