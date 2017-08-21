using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using HXD1D.Common;
using HXD1D.Titlte;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
namespace HXD1D
{
    [GksDataType(DataType.isMMIObjectClass)]
    class MaintainTesting : baseClass
    {
        /// <summary>
        /// 图片集
        /// </summary>
        private List<Image> _imgs;
        //<summary>
        //bool逻辑号
        // </summary>
        private List<int> _boolIds;

        //<summary>
        //float逻辑号
        // </summary>
        private List<int> _foolatIds;
        /// <summary>
        /// 矩形框编号
        /// </summary>
        private List<Rectangle> _rects;
        /// <summary>
        /// 整个接受消息的字典
        /// </summary>
        private SortedDictionary<int, string> _allmsgDictionary = new SortedDictionary<int, string>();
        /// <summary>
        /// 整个接受消息的列表
        /// </summary>
        private List<string> _allmsgList;
        /// <summary>
        /// 辅机测试列表
        /// </summary>
        private List<string> _fujiList;
        /// <summary>
        /// 机内动车列表
        /// </summary>
        private List<string> _jineiList;


        public static int KuNeiFenXiangTest_Over = 1;
        public static int KuNeiFenXiangTest_Start = 0;
        public static int LinYuMode_Cancel = 1;
        public static int LinYuMode_Active = 0;
        public static int DingLunMode_Cancel = 1;
        public static int DingLunMode_Active = 0;


        #region 重载的方法
        public override string GetInfo()
        {
            return "维护测试";
        }
        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        public override void paint(Graphics dcGs)
        {
            Judge(Title.CurentView, dcGs);
            ReceiveMsg();

            base.paint(dcGs);

        }
        private int m_CurrentViewId;
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {

            m_CurrentViewId = (int)nParaB;

            base.setRunValue(nParaA, nParaB, nParaC);
        }
        /// <summary>
        /// 读取辅机测试信息
        /// </summary>
        private void ReadFile()
        {
            string file = Path.Combine(RecPath, @"..\config\辅机测试.txt");
            string[] allLine = File.ReadAllLines(file, Encoding.Default);
            foreach (string[] str in allLine.Select(cStr => cStr.Split(new[] { ';' })))
            {
                int i = 0;
                string[] temp = new string[2];
                if (str.Length == 2)
                {
                    foreach (string s in str.Where(s => s.Trim() != ""))
                    {
                        if (i < 2)
                            temp[i] = s;
                        i++;
                    }
                    if (i == 2)
                    {
                        _allmsgDictionary.Add(int.Parse(temp[0]), temp[1]);
                    }
                }


            }
        }
        private void ReceiveMsg()
        {
            int mark = 0;//标志
            foreach (var index in _allmsgDictionary.Keys)
            {
                _allmsgList.Add(_allmsgDictionary[index]);
            }
            for (int i = 0; i < _allmsgList.Count; i++)
            {
                if (_allmsgList[i] == "辅机测试")
                {
                    mark = i;
                }

            }
            for (int i = 0; i < mark; i++)
            {
                _fujiList.Add(_allmsgList[i]);
            }
            for (int i = mark + 1; i < _allmsgList.Count; i++)
            {
                _jineiList.Add(_allmsgList[i]);
            }
        }

        #endregion
        private void DrawrRects(Graphics e)
        {
            for (int i = 0; i < 32; i++)
            {
                e.DrawRectangle(FormatStyle.WhitePen, _rects[i]);
            }
        }
        private void DrawChacters(Graphics e)
        {
            for (int i = 0; i < 16; i++)
            {
                e.DrawString((i + 1).ToString(CultureInfo.InvariantCulture), FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[i], FormatStyle.MFormat);
            }
        }

        private void DrawRect(Graphics e)
        {
            e.DrawRectangle(FormatStyle.WhitePen, _rects[33]);
        }

        private void DrawChacter(Graphics e)
        {
            e.DrawString(FormatStyle.str26[0], FormatStyle.Font14, FormatStyle.GreenBrush, _rects[32], FormatStyle.MFormat);
            if (m_CurrentViewId == 32)
            {
                e.DrawString(FormatStyle.str26[1], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[34], FormatStyle.MFormat);
                e.DrawString("测试结束", FormatStyle.Font12, FormatStyle.WhiteBrush, new Rectangle(_rects[35].X, _rects[35].Y, _rects[34].Width, _rects[34].Height), FormatStyle.MFormat);
                e.DrawString("测试开始", FormatStyle.Font12, FormatStyle.WhiteBrush, new Rectangle(_rects[36].X, _rects[36].Y, _rects[34].Width, _rects[34].Height), FormatStyle.MFormat);
            }
            else if (m_CurrentViewId == 31)
            {
                for (int i = 0; i < 3; i++)
                {
                    e.DrawString(FormatStyle.str26[1 + i], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[34 + i], FormatStyle.MFormat);
                }
            }
            else if (m_CurrentViewId == 45)
            {
                for (int i = 0; i < 3; i++)
                {
                    e.DrawString(FormatStyle.str26[1 + i], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[34 + i], FormatStyle.MFormat);
                }
            }

        }

        private void DrawImg(Graphics e)
        {

            if (Title.buttonIsDown[14])
            {
                e.DrawImage(_imgs[1], _rects[37]);
                e.DrawImage(_imgs[0], _rects[38]);
                switch (Title.CurentView)
                {
                    case 31:
                        LinYuMode_Cancel = 0;
                        LinYuMode_Active = 1;
                        break;
                    case 45:
                        DingLunMode_Cancel = 0;
                        DingLunMode_Active = 1;
                        break;
                    case 32:
                        KuNeiFenXiangTest_Over = 0;
                        KuNeiFenXiangTest_Start = 1;
                        break;
                }
            }
            else
            {
                e.DrawImage(_imgs[0], _rects[37]);
                e.DrawImage(_imgs[1], _rects[38]);
                switch (Title.CurentView)
                {
                    case 31:
                        LinYuMode_Cancel = 1;
                        LinYuMode_Active = 0;
                        break;
                    case 32:
                        KuNeiFenXiangTest_Over = 1;
                        KuNeiFenXiangTest_Start = 0;
                        break;
                    case 45:
                        DingLunMode_Cancel = 1;
                        DingLunMode_Active = 0;
                        break;
                }
            }


        }

        private void DrawView29Chacter(Graphics e)
        {
            for (int i = 0; i < _fujiList.Count && i < 16; i++)
            {

                e.DrawString(_fujiList[i], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[16 + i], FormatStyle.drawFormat);
            }

        }

        private void DrawView30Chacter(Graphics e)
        {
            for (int i = 0; i < 16; i++)
            {
                e.DrawString(_jineiList[i], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[16 + i], FormatStyle.drawFormat);
            }
        }

        private void Judge(int View, Graphics e)
        {
            switch (View)
            {
                case 29:
                    DrawrRects(e);
                    DrawChacters(e);
                    DrawView29Chacter(e);
                    break;
                case 30:
                    DrawrRects(e);
                    DrawChacters(e);
                    DrawView30Chacter(e);
                    break;
                case 31:
                    DrawChacter(e);
                    DrawRect(e);
                    DrawImg(e);

                    break;
                case 32:
                    DrawChacter(e);
                    DrawRect(e);
                    DrawImg(e);
                    break;
                case 45:
                    DrawChacter(e);
                    DrawRect(e);
                    DrawImg(e);
                    break;
            }
        }

        private void InitData()
        {
            ReadFile();
            _boolIds = new List<int>();
            _foolatIds = new List<int>();
            _imgs = new List<Image>();
            _rects = new List<Rectangle>();
            _allmsgList = new List<string>();
            _fujiList = new List<string>();
            _jineiList = new List<string>();

            #region 从配置表里取出数据
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                _imgs.Add(Image.FromFile(RecPath + "\\" + UIObj.ParaList[i]));
            }
            //取出配置表Boolids编号

            for (int index = 0; index < UIObj.InBoolList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InBoolList[index + 1]; i++)
                {
                    _boolIds.Add(UIObj.InBoolList[index] + i);
                }
            }
            //取出配置表Floatids编号

            for (int index = 0; index < UIObj.InFloatList.Count - 1; index = index + 2)
            {
                for (int i = 0; i < UIObj.InFloatList[index + 1]; i++)
                {
                    _foolatIds.Add(UIObj.InFloatList[index] + i);
                }
            }
            #endregion
            for (int j = 0; j < 16; j++)
            {
                _rects.Add(new Rectangle(8, 76 + j * 26, 34, 26));

            }
            for (int i = 0; i < 16; i++)
            {
                _rects.Add(new Rectangle(42, 76 + i * 26, 636, 26));
            }
            //提示
            _rects.Add(new Rectangle(205, 131, 315, 46));
            //外部矩形框
            _rects.Add(new Rectangle(73, 218, 566, 104));
            //模式选择
            _rects.Add(new Rectangle(326, 231, 92, 34));
            //取消
            _rects.Add(new Rectangle(279, 278, 50, 34));
            //激活
            _rects.Add(new Rectangle(487, 279, 50, 34));
            //取消，激活图片的位置
            for (int i = 0; i < 2; i++)
            {
                _rects.Add(new Rectangle(206 + i * 208, 270, 41, 39));
            }
        }
    }
}
