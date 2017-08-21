using System.Collections.Generic;
using System.Drawing;
using HXD1D.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    class Meter:baseClass
    {

        /// <summary>
        /// 图片集
        /// </summary>
        private List<Image> _imgs;
        ///<summary>
        ///bool逻辑号
        /// </summary>
        private List<int> _boolIds;

        //<summary>
        //float逻辑号
        // </summary>
        private List<int> _foolatIds;
        /// <summary>
        /// 视图19矩形框
        /// </summary>
        private List<Rectangle> _rects;

        private List<Point> _points;
        public override string GetInfo()
        {
            return "仪表";
        }
        public override bool init(ref int nErrorObjectIndex)
        {

            InitData();
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            DrawCricle(dcGs);
            base.paint(dcGs);


        }

        private void DrawCricle(Graphics e)
        {
            e.FillEllipse(FormatStyle.WhiteBrush, 30, 30, 310, 310);
            e.DrawEllipse(FormatStyle.BlackPen,36,36,298,298);
            e.DrawEllipse(FormatStyle.BlackPen, 30, 30, 310, 310);
            for (int i = 0; i < 8; i=i+2)
            {
                e.DrawLine(FormatStyle.BlackPen1,_points[i],_points[i+1]);
            }
         
        }

        private void InitData()
        {
            _imgs=new List<Image>();
            _rects=new List<Rectangle>();
            _foolatIds=new List<int>();
            _boolIds=new List<int>();
            _points=new List<Point>();
            for (int i = 0; i <4; i++)
            {
                _points.Add(new Point(75-i*13, 292-i*26));
                _points.Add(new Point(101 - i * 13, 270 - i * 26));
                //_points.Add(new Point(62,266));
                //_points.Add(new Point(88, 244));
                //_points.Add(new Point(49, 240));
                //_points.Add(new Point(75, 218));
            }
        }
    }
}
