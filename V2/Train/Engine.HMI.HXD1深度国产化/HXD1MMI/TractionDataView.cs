using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 牵引数据信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class TractionDataView : baseClass
    {
        private List<ForceDial> _leftForceDialList = new List<ForceDial>();
        private List<ForceDial> _rightForceDialList = new List<ForceDial>();
        private Point _left = new Point(35, 470 + Common.InitialPosY);
        private Point _right = new Point(355, 470 + Common.InitialPosY);

        private bool _leftIsDraught;
        private bool _leftIsBrake;

        private bool _rightIsDraught;
        private bool _rightIsBrake;

        public override string GetInfo()
        {
            return "牵引数据";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            _leftForceDialList.Add(new ForceDial(new Rectangle(75, 50 + Common.InitialPosY, 50, 400), 4, 27, "1-1", Color.White, 5, true));
            _leftForceDialList.Add(new ForceDial(new Rectangle(134, 50 + Common.InitialPosY, 50, 400), 4, 27, "1-2", Color.White, 5, false));
            _leftForceDialList.Add(new ForceDial(new Rectangle(193, 50 + Common.InitialPosY, 50, 400), 4, 27, "2-1", Color.White, 5, false));
            _leftForceDialList.Add(new ForceDial(new Rectangle(251, 50 + Common.InitialPosY, 50, 400), 4, 27, "2-2", Color.White, 5, false));

            _rightForceDialList.Add(new ForceDial(new Rectangle(400, 50 + Common.InitialPosY, 50, 400), 4, 27, "1-1", Color.White, 5, true));
            _rightForceDialList.Add(new ForceDial(new Rectangle(459, 50 + Common.InitialPosY, 50, 400), 4, 27, "1-2", Color.White, 5, false));
            _rightForceDialList.Add(new ForceDial(new Rectangle(518, 50 + Common.InitialPosY, 50, 400), 4, 27, "2-1", Color.White, 5, false));
            _rightForceDialList.Add(new ForceDial(new Rectangle(575, 50 + Common.InitialPosY, 50, 400), 4, 27, "2-2", Color.White, 5, false));
            return true;
        }

        private void GetValue()
        {
            _leftIsDraught = BoolList[UIObj.InBoolList[0]];
            _leftIsBrake = BoolList[UIObj.InBoolList[1]];
            //_rightIsDraught = BoolList[UIObj.InBoolList[2]];
            //_rightIsBrake = BoolList[UIObj.InBoolList[3]];


            if (_leftIsDraught)
            {
                _leftForceDialList[0].FillColor = Color.Blue;
                _leftForceDialList[1].FillColor = Color.Blue;
                _leftForceDialList[2].FillColor = Color.Blue;
                _leftForceDialList[3].FillColor = Color.Blue;

                _leftForceDialList[0].TriangleColor = Color.Blue;
                _leftForceDialList[1].TriangleColor = Color.Blue;
                _leftForceDialList[2].TriangleColor = Color.Blue;
                _leftForceDialList[3].TriangleColor = Color.Blue;

                _leftForceDialList[0].Value = FloatList[138];
                _leftForceDialList[0].DesireValue = FloatList[122];

                _leftForceDialList[1].Value = FloatList[140];
                _leftForceDialList[1].DesireValue = FloatList[124];

                _leftForceDialList[2].Value = FloatList[142];
                _leftForceDialList[2].DesireValue = FloatList[126];

                _leftForceDialList[3].Value = FloatList[144];
                _leftForceDialList[3].DesireValue = FloatList[128];





                _rightForceDialList[0].FillColor = Color.Blue;
                _rightForceDialList[1].FillColor = Color.Blue;
                _rightForceDialList[2].FillColor = Color.Blue;
                _rightForceDialList[3].FillColor = Color.Blue;

                _rightForceDialList[0].TriangleColor = Color.Blue;
                _rightForceDialList[1].TriangleColor = Color.Blue;
                _rightForceDialList[2].TriangleColor = Color.Blue;
                _rightForceDialList[3].TriangleColor = Color.Blue;

                _rightForceDialList[0].Value = FloatList[146];
                _rightForceDialList[0].DesireValue = FloatList[130];

                _rightForceDialList[1].Value = FloatList[148];
                _rightForceDialList[1].DesireValue = FloatList[132];

                _rightForceDialList[2].Value = FloatList[150];
                _rightForceDialList[2].DesireValue = FloatList[134];

                _rightForceDialList[3].Value = FloatList[152];
                _rightForceDialList[3].DesireValue = FloatList[136];
            }

            if (_leftIsBrake)
            {
                _leftForceDialList[0].FillColor = Color.Red;
                _leftForceDialList[1].FillColor = Color.Red;
                _leftForceDialList[2].FillColor = Color.Red;
                _leftForceDialList[3].FillColor = Color.Red;

                _leftForceDialList[0].TriangleColor = Color.Red;
                _leftForceDialList[1].TriangleColor = Color.Red;
                _leftForceDialList[2].TriangleColor = Color.Red;
                _leftForceDialList[3].TriangleColor = Color.Red;

                _leftForceDialList[0].Value = FloatList[139];
                _leftForceDialList[0].DesireValue = FloatList[123];

                _leftForceDialList[1].Value = FloatList[141];
                _leftForceDialList[1].DesireValue = FloatList[125];

                _leftForceDialList[2].Value = FloatList[143];
                _leftForceDialList[2].DesireValue = FloatList[127];

                _leftForceDialList[3].Value = FloatList[145];
                _leftForceDialList[3].DesireValue = FloatList[129];




                _rightForceDialList[0].FillColor = Color.Red;
                _rightForceDialList[1].FillColor = Color.Red;
                _rightForceDialList[2].FillColor = Color.Red;
                _rightForceDialList[3].FillColor = Color.Red;

                _rightForceDialList[0].TriangleColor = Color.Red;
                _rightForceDialList[1].TriangleColor = Color.Red;
                _rightForceDialList[2].TriangleColor = Color.Red;
                _rightForceDialList[3].TriangleColor = Color.Red;

                _rightForceDialList[0].Value = FloatList[147];
                _rightForceDialList[0].DesireValue = FloatList[131];

                _rightForceDialList[1].Value = FloatList[149];
                _rightForceDialList[1].DesireValue = FloatList[133];

                _rightForceDialList[2].Value = FloatList[151];
                _rightForceDialList[2].DesireValue = FloatList[135];

                _rightForceDialList[3].Value = FloatList[153];
                _rightForceDialList[3].DesireValue = FloatList[137];
            }    

            if (!(_leftIsDraught || _leftIsBrake))
            {
                _leftForceDialList[0].Value = 0;
                _leftForceDialList[0].DesireValue = 0;

                _leftForceDialList[1].Value = 0;
                _leftForceDialList[1].DesireValue = 0;

                _leftForceDialList[2].Value = 0;
                _leftForceDialList[2].DesireValue = 0;

                _leftForceDialList[3].Value = 0;
                _leftForceDialList[3].DesireValue = 0;



                _rightForceDialList[0].Value = 0;
                _rightForceDialList[0].DesireValue = 0;

                _rightForceDialList[1].Value = 0;
                _rightForceDialList[1].DesireValue = 0;

                _rightForceDialList[2].Value = 0;
                _rightForceDialList[2].DesireValue = 0;

                _rightForceDialList[3].Value = 0;
                _rightForceDialList[3].DesireValue = 0;


                _leftForceDialList[0].TriangleColor = Color.White;
                _leftForceDialList[1].TriangleColor = Color.White;
                _leftForceDialList[2].TriangleColor = Color.White;
                _leftForceDialList[3].TriangleColor = Color.White;




                _rightForceDialList[0].TriangleColor = Color.White;
                _rightForceDialList[1].TriangleColor = Color.White;
                _rightForceDialList[2].TriangleColor = Color.White;
                _rightForceDialList[3].TriangleColor = Color.White;
            }
        }

        public override void paint(Graphics dcGs)
        {

            GetValue();

            foreach (var v in _leftForceDialList)
            {
                v.OnDraw(dcGs);
            }

            dcGs.DrawString("本车", Common._14Font, Common.WhiteBrush, _left);
            dcGs.DrawString("它车", Common._14Font, Common.WhiteBrush, _right);

            foreach (var v in _rightForceDialList)
            {
                v.OnDraw(dcGs);
            }
        }

    }
}
