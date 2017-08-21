using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 轮圆润化
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class Wheel : baseClass
    {

        private WorkModel _workModel = new WorkModel();
        private Space _timeSpace;
        private Space _distaceSpace;

        public override string GetInfo()
        {
            return "轮缘润滑";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            _timeSpace = new Space(new Rectangle(10, 190, 620, 145), new Rectangle(240, 210, 200, 60), "时间间隔", "(10-99s)", 2, "s");
            _distaceSpace = new Space(new Rectangle(10, 345, 620, 145), new Rectangle(240, 355, 240, 60), "距离间隔", "(100-999m)", 3, "m");
            return true;
        }

        private void GetVlaue()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Cancel].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Cancel].State == MouseState.MouseDown)
            {
                append_postCmd(CmdType.ChangePage, 1, 0, 0);
            }
        }

        public override void paint(Graphics dcGs)
        {
            GetVlaue();

            _workModel.OnGraw(dcGs);
            _timeSpace.OnDraw(dcGs);
            _distaceSpace.OnDraw(dcGs);
        }
    }

    public class WorkModel
    {
        private Pen _pen = new Pen(Color.White, 2);
        private Rectangle _mainRect = new Rectangle(10, 35, 620, 145);
        public WorkModel()
        {

        }

        public void OnGraw(Graphics g)
        {
            g.DrawRectangle(_pen, _mainRect);

            g.DrawString("工作模式", Common._20Font, Common.WhiteBrush, new Point(_mainRect.X + 30, _mainRect.Y + 50));

            g.FillEllipse(Common.BlueBrush, new Rectangle(_mainRect.X + 200, _mainRect.Y + 60, 20, 20));

            g.DrawString("时间", Common._16Font, Common.WhiteBrush, new Point(_mainRect.X + 220, _mainRect.Y + 60));

            g.FillEllipse(Common.WhiteBrush, new Rectangle(_mainRect.X + 400, _mainRect.Y + 60, 20, 20));

            g.DrawString("距离", Common._16Font, Common.WhiteBrush, new Point(_mainRect.X + 420, _mainRect.Y + 60));
        }
    }

    public class Space
    {
        private Pen _pen = new Pen(Color.White, 2);
        private Rectangle _mainRect;
        private Rectangle _currenRect;

        private string _title;
        private string _timeDistance;

        private int _count;
        private string _unit;

        private List<RectText> _currentRectTextList = new List<RectText>();
        private List<RectText> _rectRectTextList = new List<RectText>();


        public Space(Rectangle rect, Rectangle currentRect, string title, string timeDistance, int n, string unit)
        {
            _mainRect = rect;
            _currenRect = currentRect;

            _title = title;
            _timeDistance = timeDistance;

            _count = n;
            _unit = unit;

            for (int i = 0; i < _count; i++)
            {
                _currentRectTextList.Add(new RectText(new Rectangle(_currenRect.X + 120 + 35 * i, _currenRect.Y + 8, 25, 35), "", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));

                _rectRectTextList.Add(new RectText(new Rectangle(_currenRect.X + 120 + 35 * i, _currenRect.Y + 70, 25, 35), "", 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));
            }
        }

        public void OnDraw(Graphics g)
        {
            g.DrawRectangle(_pen, _mainRect);
            g.DrawRectangle(_pen, _currenRect);


            g.DrawString(_title, Common._20Font, Common.WhiteBrush, new Point(_mainRect.X + 30, _mainRect.Y + 50));

            g.DrawString(_timeDistance, Common._16Font, Common.WhiteBrush, new Point(_mainRect.X + 30, _mainRect.Y + 80));

            g.DrawString("当前值", Common._16Font, Common.WhiteBrush, new Point(_currenRect.X + 20, _currenRect.Y + 8));


            for (int i = 0; i < _count; i++)
            {
                _currentRectTextList[i].OnDraw(g);
                _rectRectTextList[i].OnDraw(g);
            }

            g.DrawString(_unit, Common._12Font, Common.WhiteBrush, new Point(_currenRect.Right + 20, _currenRect.Y + 20));
            g.DrawString(_unit, Common._12Font, Common.WhiteBrush, new Point(_currenRect.Right + 20, _currenRect.Y + 80));

        }
    }
}
