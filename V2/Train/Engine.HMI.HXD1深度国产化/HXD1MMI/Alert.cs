using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace HXD1.DeepDomestic
{

    /// <summary>
    /// 报警界面
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class Alert : baseClass
    {
        private AlertModel AlertModel1;
        private AlertModel AlertModel2;
        private AlertModel AlertModel3;

        public static List<AlertModel> AlertModelList = new List<AlertModel>();
        public static int SelectIndex = 0;

        //Timer timer = new Timer(1000);
        //private int cuurentJiange;
        //private int currentShijian;

        public static int id;

        private RectText rect;

        public override string GetInfo()
        {
            return "分相/警惕";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            AlertModel1 = new AlertModel(new Rectangle(10, 35, 620, 145), "自动过分相", new List<string>() { "投入", "切除" });
            AlertModel2 = new AlertModel(new Rectangle(10, 190, 620, 145), "分相G1-G2距离", new List<string>() { "170m", "280m" });
            AlertModel3 = new AlertModel(new Rectangle(10, 355, 620, 145), "无人警惕模式\n(间隔+报警)", new List<string>() { "120+20", "60+10" });
            //rect=new RectText()
            //{
            //    Text="0",
            //    IsVisible = false,
            //    PenColor = Color.Chocolate,
            //    PenWide = 3,

            //};
            AlertModelList.Add(AlertModel1);
            AlertModelList.Add(AlertModel2);
            AlertModelList.Add(AlertModel3);

            AlertModelList[SelectIndex].IsSelected = true;
            //timer.Elapsed += timer_Elapsed;
            return true;
        }

        //void timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    cuurentJiange--;
        //    while (cuurentJiange <= 0)
        //    {
        //        currentShijian--;
        //    }
        //}


        private void GetVlaue()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Cancel].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Cancel].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Up].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Up].State == MouseState.MouseDown)
            {
                AlertModelList[SelectIndex].IsSelected = false;
                SelectIndex++;
                if (SelectIndex == 3)
                {
                    SelectIndex = 0;
                }
                AlertModelList[SelectIndex].IsSelected = true;
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Right].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Right].State == MouseState.MouseDown)
            {
                AlertModelList[SelectIndex].SelectIndex++;
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Enter].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Enter].State == MouseState.MouseDown)
            {
                AlertModelList[SelectIndex].SendValue = AlertModelList[SelectIndex].SelectIndex;
               

            }

            //cuurentJiange = AlertModelList[2].SelectIndex == 0 ? 120 : 60;
            //currentShijian = AlertModelList[2].SelectIndex == 0 ? 20 : 10;
        }



        public override void paint(Graphics dcGs)
        {

            GetVlaue();

            AlertModel1.OnDraw(dcGs);
            AlertModel2.OnDraw(dcGs);
            AlertModel3.OnDraw(dcGs);
        }
    }

    public class AlertModel
    {

        private Pen _whitePen = new Pen(Color.White, 2);
        private Pen _bluePen = new Pen(Color.Blue, 2);

        private Rectangle _mainRect;
        private string _title;
        private List<string> _selectList = new List<string>();

        public bool IsSelected = false;

        public int SendValue = 0;

        public int SelectIndex
        {
            get
            {
                return _selectIndex;
            }
            set
            {
                _selectIndex = value;
                if (_selectIndex == _selectList.Count)
                    _selectIndex = 0;
            }
        }
        private int _selectIndex = 0;

        public AlertModel(Rectangle rect, string title, List<string> list)
        {
            _mainRect = rect;
            _title = title;
            _selectList = list;
        }

        public void OnDraw(Graphics g)
        {
            if (!IsSelected)
                g.DrawRectangle(_whitePen, _mainRect);
            else
                g.DrawRectangle(_bluePen, _mainRect);

            g.DrawString(_title, Common._20Font, Common.WhiteBrush, new Point(_mainRect.X + 20, _mainRect.Y + _mainRect.Height / 2 - 20));

            for (int i = 0; i < _selectList.Count; i++)
            {
                if (i != _selectIndex)
                    g.FillEllipse(Common.WhiteBrush, new Rectangle(_mainRect.X + 300 + 150 * i, _mainRect.Y + _mainRect.Height / 2 - 20, 20, 20));
                else
                    g.FillEllipse(Common.GreenBrush, new Rectangle(_mainRect.X + 300 + 150 * i, _mainRect.Y + _mainRect.Height / 2 - 20, 20, 20));
                g.DrawString(_selectList[i], Common._16Font, Common.WhiteBrush, new PointF(_mainRect.X + 320 + 150 * i, _mainRect.Y + _mainRect.Height / 2 - 20));
            }


        }

    }
}
