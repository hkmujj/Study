using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System.IO;
using CommonUtil.Controls;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 牵引模块B
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class DraughtBlockB : baseClass
    {

        private Image[] _imageArray;

        private List<RectText> IndexForwadList = new List<RectText>();
        private List<RectText> IndexBackList = new List<RectText>();

        private List<RectText> _middleList = new List<RectText>();

        private Dictionary<int, string> _draughtStateMap = new Dictionary<int, string>();

        private List<int> _train1SelectIndexList = new List<int>();

        private List<int> _train2SelectIndexList = new List<int>();

        private int _currentPage = 0;


        public override string GetInfo()
        {
            return "牵引封锁";
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



            for (int i = 0; i < 16; i++)
            {
                IndexForwadList.Add(new RectText(new Rectangle(2, 70 + 26 * i, 40, 26), (i + 1).ToString(), 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));
                IndexBackList.Add(new RectText(new Rectangle(680, 80 + 26 * i, 40, 26), (i + 1).ToString(), 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));

                _middleList.Add(new RectText(new Rectangle(42, 70 + 26 * i, 638, 26), "", 12, 0, Color.White, Color.Black, Color.White, 1, true, null, true));

            }
            var file = Path.Combine(AppPaths.ConfigDirectory, "牵引状态.txt");

            try
            {
                var allLines = File.ReadAllLines(file, Encoding.Default);

                ParseLines(allLines);
            }
            catch (Exception e)
            {
                LogMgr.Fatal(string.Format("Can not read file : {0} \r\n {1}", file, e));
            }

            return true;
        }

        private void ParseLines(string[] allLines)
        {
            for (int nIndex = 0; nIndex < allLines.Length; nIndex++)
            {
                var cStr = allLines[nIndex];
                if (cStr.Trim() != "")
                {
                    string[] str = cStr.Split(';');
                    _draughtStateMap.Add(nIndex, str[1]);
                }
            }
        }

        private void GetVlaue()
        {
            for (int i = 0; i < 25; i++)
            {
                if (BoolList[1068 + i])
                {
                    if (!_train1SelectIndexList.Contains(i))
                        _train1SelectIndexList.Add(i);
                }
                else
                {
                    if (_train1SelectIndexList.Contains(i))
                    {
                        _train1SelectIndexList.Remove(i);
                        if ((i - _currentPage * 16) < 16 && (i - _currentPage * 16) >= 0)
                        {
                            _middleList[i - _currentPage * 16].BackgroundColor = Color.Black;
                            _middleList[i - _currentPage * 16].TextColor = Color.White;

                            IndexForwadList[i - _currentPage * 16].BackgroundColor = Color.Black;
                            IndexForwadList[i - _currentPage * 16].TextColor = Color.White;

                        }
                    }
                }
            }

            for (int i = 0; i < _train1SelectIndexList.Count; i++)
            {
                if ((_train1SelectIndexList[i] - _currentPage * 16) < 16 && (_train1SelectIndexList[i] - _currentPage * 16) >= 0)
                {
                    _middleList[_train1SelectIndexList[i] - _currentPage * 16].BackgroundColor = Color.White;
                    _middleList[_train1SelectIndexList[i] - _currentPage * 16].TextColor = Color.Black;

                    IndexForwadList[_train1SelectIndexList[i] - _currentPage * 16].BackgroundColor = Color.White;
                    IndexForwadList[_train1SelectIndexList[i] - _currentPage * 16].TextColor = Color.Black;
                }
            }
        }



        private void SetPage()
        {
            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Down].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Down].State == MouseState.MouseDown)//下一页
            {
                int nowValue = _currentPage + 1;
                _currentPage = nowValue >_draughtStateMap.Count / 16 ? _currentPage : nowValue;
            }

            if (BoolList[ReceiveRequest.ButtonInfoDictionary[ButtonType.Up].LogicIndex] ||ReceiveRequest.ButtonInfoDictionary[ButtonType.Up].State == MouseState.MouseDown)//上一页
            {
                int nowValue = _currentPage - 1;
                _currentPage = nowValue < 0 ? 0 : nowValue;

            }
        }

        private void DrawRightImage(Graphics g)
        {
            if (_currentPage == 0)
            {
                g.DrawImage(_imageArray[0], new Rectangle(720, 240, 70, 65));
            }
            else
            {
                g.DrawImage(_imageArray[1], new Rectangle(720, 170, 70, 65));
            }

        }

        public void Clear()
        {
            for (int i = 0; i < _middleList.Count; i++)
            {
                _middleList[i].Text = "";
                _middleList[i].BackgroundColor = Color.Black;
                _middleList[i].TextColor = Color.White;

                IndexForwadList[i].BackgroundColor = Color.Black;
                IndexForwadList[i].TextColor = Color.White;
            }
        }

        public override void paint(Graphics g)
        {

            Clear();

            SetPage();

            DrawRightImage(g);

            GetVlaue();

         

            for (int i = _currentPage * 16; i < _draughtStateMap.Count && i - _currentPage * 16 < 16; i++)
            {
                _middleList[i - _currentPage * 16].Text = _draughtStateMap[i];
            }

            for (int i = 0; i < 16; i++)
            {
                IndexForwadList[i].OnDraw(g);
                IndexBackList[i].OnDraw(g);
                _middleList[i].OnDraw(g);
            }

        }
    }
}
