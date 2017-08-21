using System.Collections.Generic;
using System.Drawing;
using ATP200C.MainView;
using ATP200C.Resource.Images;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace ATP200C.PublicComponents
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ATPButtonScreen : ATPBaseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        //2
        public override string GetInfo()
        {
            return "边框按钮";
        }

        //6
        public override bool Initalize()
        {
            _rectsList = new List<RectangleF>();
            //0-7
            for (var i = 0; i < 8; i++)
            {
                _rectsList.Add(new RectangleF(815, 2 + i * 75, 70, 70));
            }
            //8-17
            for (var i = 0; i < 11; i++)
            {
                _rectsList.Add(new RectangleF(2 + i * 75, 615, 70, 70));
            }
            //19
            _rectsList.Add(new RectangleF(0, 0, 1000, 750));

            _btnRegionArr = new Region[19];
            _btnIsDown = new bool[19];

            for (int i = 0; i < _btnRegionArr.Length; i++)
            {
                _btnRegionArr[i] = new Region(_rectsList[i]);
            }

            _btnStr = new[]
                      {
                          "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "调车\n1\n ", "目视\n2\nABC", "启动\n3\nDEF", "缓解\n4\nGHI", "\n5\nJKL", "\n6\nMNO", "\n7\nPQRS",
                          "\n8\nTUV", "\n9\nWXYZ", "\n0\n ", "警惕\n字母"
                      };
            return true;
        }

        #endregion

        private int _index;

        public override void paint(Graphics dcGs)
        {
            //dcGs.DrawImage(_imgsArr[2], _rectsList[19]);
            for (_index = 0; _index < _btnIsDown.Length; _index++)
            {
                dcGs.DrawImage(_btnIsDown[_index] ? BtnImageResource.Btn_Down : BtnImageResource.Btn_Up, _rectsList[_index]);
                dcGs.DrawString(_btnStr[_index],
                    _index < 8 ? FontsItems.Font18_You_B : FontsItems.Font13_You_B,
                    _btnIsDown[_index] ? _whiteBrush : _blackBrush,
                    _rectsList[_index],
                    FontsItems.TheAlignment(FontRelated.居中));
            }

            dcGs.DrawRectangle(PenItems.WhitePen1, _backgroundRect);

            for (var index = 0; index < 38; index++)
            {
                var target = BoolList;
                var idx = UIObj.InBoolList[index];
                if (idx > 4800)
                {
                    target = OutBoolList;
                }

                if (target[idx] && !_btnList.Contains(index))//判断按钮按下
                {
                    _btnList.Add(index);
                    _btnIsDown[index % 19] = true;
                    BtnState = new BtnInfo((BtnTypeName) ( index % 19 ));
                    break;
                }
                if (!_btnList.Contains(index) || target[idx])
                {
                    continue;
                }
                _btnList.RemoveAll(i => i == index);
                _btnIsDown[index % 19] = false;
                BtnState = null;
                break;
            }
        }

        private int _btnDownId = -1;

        public override bool mouseDown(Point nPoint)
        {
            var btnIndex = 0;
            for (; btnIndex < _btnIsDown.Length; btnIndex++)
            {
                if (!_btnRegionArr[btnIndex].IsVisible(nPoint))
                {
                    continue;
                }
                _btnIsDown[btnIndex] = true;
                append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[btnIndex], 1, 0);
                _btnDownId = btnIndex;
                break;
            }
            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            if (_btnDownId == -1)
            {
                return base.mouseUp(nPoint);
            }
            _btnIsDown[_btnDownId] = false;
            append_postCmd(CmdType.SetBoolValue, UIObj.OutBoolList[_btnDownId], 0, 0);
            _btnDownId = -1;
            return base.mouseUp(nPoint);
        }
        private Rectangle _backgroundRect = new Rectangle(0, 0, 800, 600);
        private List<RectangleF> _rectsList;
        private Region[] _btnRegionArr;
        private bool[] _btnIsDown;
        private string[] _btnStr;

        /// <summary>
        /// 按钮状态，为null即没有按钮消息
        /// </summary>
        public static BtnInfo BtnState;

        //存放所有按着的按钮编号
        private readonly List<int> _btnList = new List<int>();

        /// <summary>
        /// 黑色
        /// Color.Black
        /// </summary>
        private readonly SolidBrush _blackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// 白色
        /// Color.White
        /// </summary>
        private readonly SolidBrush _whiteBrush = new SolidBrush(Color.White);
    }
}
