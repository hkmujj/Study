using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System.IO;

namespace HXD1.DeepDomestic
{

    /// <summary>
    /// 辅助系统
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class AssistSystem : baseClass
    {
        private Image[] _imageArray;

        private Rectangle _mainRect = new Rectangle(4, 30, 792, 480);

        private Point _pressure1 = new Point(40, 90);
        private Point _current1 = new Point(120, 90);
        private Point _frequency1 = new Point(40, 130);

        private Point _pressure2 = new Point(40, 240);
        private Point _current2 = new Point(120, 240);
        private Point _frequency2 = new Point(40, 280);



        private Rectangle _31_k11 = new Rectangle(205, 55, 35, 20);
        private Rectangle _31_k12 = new Rectangle(205, 272, 35, 20);

        private Rectangle _31_k13 = new Rectangle(228, 162, 20, 35);

        private Rectangle _31_k01 = new Rectangle(298, 58, 35, 20);
        private Rectangle _31_k02 = new Rectangle(315, 275, 35, 20);

        private Rectangle _31_k05 = new Rectangle(337, 162, 20, 35);

        private Rectangle _31_k21 = new Rectangle(315, 302, 35, 20);

        private Rectangle _31_k22 = new Rectangle(292, 235, 20, 38);
        private Rectangle _34_k71 = new Rectangle(300, 365, 20, 35);
        private Rectangle _34_k81 = new Rectangle(170, 365, 20, 35);
        private Rectangle _34_k01 = new Rectangle(418, 102, 20, 35);
        private Rectangle _34_k03 = new Rectangle(593, 105, 20, 35);
        private Rectangle _34_k10 = new Rectangle(366, 320, 20, 35);
        private Rectangle _34_k12 = new Rectangle(526, 355, 20, 35);
        private Rectangle _34_k13 = new Rectangle(576, 355, 20, 35);
        private Rectangle _34_k05 = new Rectangle(730, 153, 20, 35);

        private Rectangle _34_q01 = new Rectangle(419, 153, 34, 34);
        private Rectangle _34_q02 = new Rectangle(474, 153, 34, 34);
        private Rectangle _34_q03 = new Rectangle(592, 153, 34, 34);
        private Rectangle _34_q04 = new Rectangle(650, 153, 34, 34);
        private Rectangle _34_q05 = new Rectangle(730, 106, 34, 34);
        private Rectangle _34_q10 = new Rectangle(366, 365, 34, 34);
        private Rectangle _34_q11 = new Rectangle(469, 313, 34, 34);
        private Rectangle _34_q12 = new Rectangle(523, 313, 34, 34);
        private Rectangle _34_q13 = new Rectangle(575, 312, 34, 34);
        private Rectangle _34_q15 = new Rectangle(630, 313, 34, 34);
        private Rectangle _62_q01 = new Rectangle(684, 313, 34, 34);
        private Rectangle _32_q01 = new Rectangle(733, 313, 34, 34);

        public override string GetInfo()
        {
            return "辅助系统";
        }

        public override void paint(Graphics g)
        {
            g.DrawImage(_imageArray[0], _mainRect);

            DrawAuxiliaryInverter(g);

            DrawContactor(g);

            DrawDisconnectionSwtich(g);
        }

        private void DrawDisconnectionSwtich(Graphics g)
        {
            #region 断路器

            DrawDisconnectionSwtich1(g);

            DrawDisconnectionSwtich2(g);

            #endregion
        }

        private void DrawDisconnectionSwtich2(Graphics g)
        {
            if (BoolList[894])
            {
                g.DrawImage(_imageArray[4], _34_q10);
            }

            if (BoolList[895])
            {
                g.DrawImage(_imageArray[4], _34_q11);
            }

            if (BoolList[896])
            {
                g.DrawImage(_imageArray[4], _34_q12);
            }

            if (BoolList[897])
            {
                g.DrawImage(_imageArray[4], _34_q13);
            }

            if (BoolList[898])
            {
                g.DrawImage(_imageArray[4], _34_q15);
            }

            if (BoolList[899])
            {
                g.DrawImage(_imageArray[4], _62_q01);
            }

            if (BoolList[900])
            {
                g.DrawImage(_imageArray[4], _32_q01);
            }
        }

        private void DrawDisconnectionSwtich1(Graphics g)
        {
            if (BoolList[889])
            {
                g.DrawImage(_imageArray[4], _34_q01);
            }

            if (BoolList[890])
            {
                g.DrawImage(_imageArray[4], _34_q02);
            }

            if (BoolList[891])
            {
                g.DrawImage(_imageArray[4], _34_q03);
            }

            if (BoolList[892])
            {
                g.DrawImage(_imageArray[4], _34_q04);
            }

            if (BoolList[893])
            {
                g.DrawImage(_imageArray[4], _34_q05);
            }
        }

        private void DrawContactor(Graphics g)
        {
            #region 接触器

            DrawDrawContactor1(g);

            DrawDrawContactor2(g);

            #endregion
        }

        private void DrawDrawContactor2(Graphics g)
        {
            if (BoolList[881])
            {
                g.DrawImage(_imageArray[2], _34_k71);
            }

            if (BoolList[882])
            {
                g.DrawImage(_imageArray[2], _34_k81);
            }

            if (BoolList[883])
            {
                g.DrawImage(_imageArray[2], _34_k01);
            }

            if (BoolList[884])
            {
                g.DrawImage(_imageArray[2], _34_k03);
            }

            if (BoolList[885])
            {
                g.DrawImage(_imageArray[2], _34_k10);
            }

            if (BoolList[886])
            {
                g.DrawImage(_imageArray[2], _34_k12);
            }

            if (BoolList[887])
            {
                g.DrawImage(_imageArray[2], _34_k13);
            }

            if (BoolList[888])
            {
                g.DrawImage(_imageArray[2], _34_k05);
            }
        }

        private void DrawDrawContactor1(Graphics g)
        {
            if (BoolList[871])
            {
                g.DrawImage(_imageArray[1], _31_k11);
            }

            if (BoolList[872])
            {
                g.DrawImage(_imageArray[1], _31_k12);
            }

            if (BoolList[873])
            {
                g.DrawImage(_imageArray[2], _31_k13);
            }

            if (BoolList[876])
            {
                g.DrawImage(_imageArray[1], _31_k01);
            }

            if (BoolList[877])
            {
                g.DrawImage(_imageArray[1], _31_k02);
            }

            if (BoolList[878])
            {
                g.DrawImage(_imageArray[2], _31_k05);
            }

            if (BoolList[879])
            {
                g.DrawImage(_imageArray[1], _31_k21);
            }

            if (BoolList[880])
            {
                g.DrawImage(_imageArray[2], _31_k22);
            }
        }

        private void DrawAuxiliaryInverter(Graphics g)
        {
            #region 辅助逆变器

            g.DrawString(((int) FloatList[154]).ToString(), Common._12Font, Common.WhiteBrush, _pressure1); //电压1
            g.DrawString(((int) FloatList[155]).ToString(), Common._12Font, Common.WhiteBrush, _current1); //电流1
            g.DrawString(((int) FloatList[156]).ToString(), Common._12Font, Common.WhiteBrush, _frequency1); //频率1

            g.DrawString(((int) FloatList[157]).ToString(), Common._12Font, Common.WhiteBrush, _pressure2); //电压2
            g.DrawString(((int) FloatList[158]).ToString(), Common._12Font, Common.WhiteBrush, _current2); //电流2
            g.DrawString(((int) FloatList[159]).ToString(), Common._12Font, Common.WhiteBrush, _frequency2); //频率2

            #endregion
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            _imageArray = new Image[UIObj.ParaList.Count];

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath , UIObj.ParaList[i]), FileMode.Open))
                {
                    _imageArray[i] = Image.FromStream(fs);
                }
            }

            return true;
        }
    }
}