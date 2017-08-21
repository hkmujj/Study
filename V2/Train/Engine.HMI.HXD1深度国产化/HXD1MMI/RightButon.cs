using System.IO;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 右边 按钮框，负责接受用户指令
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class RightButton : baseClass
    {
        private Image[] _imageArray;

        private List<Image> _trainParameterImage = new List<Image>();
        private List<Rectangle> _trainParameterRect = new List<Rectangle>();

        private List<Image> _trainWheelImage = new List<Image>();
        private List<Rectangle> _trainWheelRect = new List<Rectangle>();

        private List<Image> _AlertImage = new List<Image>();
        private List<Rectangle> _AlertRect = new List<Rectangle>();

        private Rectangle[] _rectArray;
        private int _runPage;


        public override string GetInfo()
        {
            return "右边的按钮";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            _rectArray = new Rectangle[6];

            for (int i = 0; i < 6; i++)
            {
                if (i == 5)
                    _rectArray[i] = new Rectangle(720, 90 + 70 * i, 70, 75);
                else
                    _rectArray[i] = new Rectangle(720, 90 + 70 * i, 70, 65);
            }

            _imageArray = new Image[UIObj.ParaList.Count];
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath , UIObj.ParaList[i]), FileMode.Open))
                {
                    _imageArray[i] = Image.FromStream(fs);
                }
            }

            _trainParameterImage.Add(_imageArray[0]);
            _trainParameterImage.Add(_imageArray[1]);
            _trainParameterImage.Add(_imageArray[2]);
            _trainParameterImage.Add(_imageArray[3]);

            _trainParameterRect.Add(_rectArray[0]);
            _trainParameterRect.Add(_rectArray[3]);
            _trainParameterRect.Add(_rectArray[4]);
            _trainParameterRect.Add(_rectArray[5]);


            _trainWheelImage.Add(_imageArray[4]);
            _trainWheelImage.Add(_imageArray[5]);
            _trainWheelImage.Add(_imageArray[6]);
            _trainWheelImage.Add(_imageArray[7]);

            _trainWheelRect.Add(_rectArray[0]);
            _trainWheelRect.Add(_rectArray[1]);
            _trainWheelRect.Add(_rectArray[4]);
            _trainWheelRect.Add(_rectArray[5]);


            _AlertImage.Add(_imageArray[4]);
            _AlertImage.Add(_imageArray[8]);
            _AlertImage.Add(_imageArray[9]);
            _AlertImage.Add(_imageArray[3]);

            _AlertRect.Add(_rectArray[0]);
            _AlertRect.Add(_rectArray[1]);
            _AlertRect.Add(_rectArray[4]);
            _AlertRect.Add(_rectArray[5]);

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            _runPage = nParaB;
        }

        private void DrawTrainParameter(Graphics g)
        {
            for (int i = 0; i < _trainParameterImage.Count; i++)
            {
                g.DrawImage(_trainParameterImage[i], _trainParameterRect[i]);
            }
        }

        private void DrawTrainWheel(Graphics g)
        {
            for (int i = 0; i < _trainWheelImage.Count; i++)
            {
                g.DrawImage(_trainWheelImage[i], _trainWheelRect[i]);
            }
        }

        private void DrawAlert(Graphics g)
        {
            for (int i = 0; i < _trainWheelImage.Count; i++)
            {
                g.DrawImage(_AlertImage[i], _AlertRect[i]);
            }
        }

        public override void paint(Graphics dcGs)
        {
            switch (_runPage)
            {
                case 21:
                    {
                        DrawTrainParameter(dcGs);
                    }
                    break;
                case 22:
                    {
                        DrawTrainWheel(dcGs);
                    }
                    break;
                case 23:
                    {
                        DrawAlert(dcGs);
                    }
                    break;
            }


        }
    }
}
