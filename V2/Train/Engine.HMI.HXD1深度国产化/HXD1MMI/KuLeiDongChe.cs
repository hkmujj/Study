using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using CommonUtil.Util;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace HXD1.DeepDomestic
{
    /// <summary>
    /// 库内动车
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class KuNeiDongChe : baseClass
    {
        private List<RectText> IndexForwadList = new List<RectText>();
        private List<RectText> _middleList = new List<RectText>();

        private Dictionary<int, string> _middleStateMap = new Dictionary<int, string>();

        private int _currentPage = 0;

        private List<int> _trainSelectIndexList = new List<int>();

        public override string GetInfo()
        {
            return "库内动车";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            for (int i = 0; i < 16; i++)
            {
                IndexForwadList.Add(new RectText(new Rectangle(2, 70 + 26 * i, 40, 26), (i + 1).ToString(), 12, 1, Color.White, Color.Black, Color.White, 1, true, null, true));

                _middleList.Add(new RectText(new Rectangle(42, 70 + 26 * i, 638, 26), "", 12, 0, Color.White, Color.Black, Color.White, 1, true, null, true));

            }

            var file = Path.Combine(AppPaths.ConfigDirectory, "辅机测试.txt");

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
            var index = 0;
            foreach (string[] str in from cStr in allLines where cStr.Trim() != "" select cStr.Split(';') into str where str.Length == 2 where int.Parse(str[0]) == 2 select str)
            {
                _middleStateMap.Add(index, str[1]);
                ++index;
            }
        }


        private void GetValue()
        {
            for (int i = 0; i < 8; i++)
            {
                if (BoolList[1199 + i])
                {
                    if (!_trainSelectIndexList.Contains(i))
                        _trainSelectIndexList.Add(i);
                }
                else
                {
                    if (_trainSelectIndexList.Contains(i))
                    {
                        _trainSelectIndexList.Remove(i);
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

            for (int i = 0; i < _trainSelectIndexList.Count; i++)
            {
                if ((_trainSelectIndexList[i] - _currentPage * 16) < 16 && (_trainSelectIndexList[i] - _currentPage * 16) >= 0)
                {
                    _middleList[_trainSelectIndexList[i] - _currentPage * 16].BackgroundColor = Color.White;
                    _middleList[_trainSelectIndexList[i] - _currentPage * 16].TextColor = Color.Black;

                    IndexForwadList[_trainSelectIndexList[i] - _currentPage * 16].BackgroundColor = Color.White;
                    IndexForwadList[_trainSelectIndexList[i] - _currentPage * 16].TextColor = Color.Black;
                }
            }

        }

        public override void paint(Graphics g)
        {

            GetValue();

            for (int i = _currentPage * 16; i < _middleStateMap.Count && i - _currentPage * 16 < 16; i++)
            {
                _middleList[i - _currentPage * 16].Text = _middleStateMap[i];
            }

            for (int i = 0; i < 16; i++)
            {
                IndexForwadList[i].OnDraw(g);
                _middleList[i].OnDraw(g);
            }
        }
    }
}
