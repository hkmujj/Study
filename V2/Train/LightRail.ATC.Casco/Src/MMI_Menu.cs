using System;
using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace Ethiopia_MMI
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class MMI_Menu : baseClass
    {
        #region :::::::::::::::::::::::::: init override :::::::::::::::::::::::::
        public override string GetInfo()
        {
            return "菜单";
        }

        public override int getInBoolParaCount()
        {
            return 0;
        }

        public override int getInFloatParaCount()
        {
            return 0;
        }

        public override int getUIParaCount()
        {
            return 3;
        }

        /////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////
        public override string GetTypeName()
        {
            return "MMI_Menu";
        }

        public override bool initObject(string nPara)
        {
            return base.initObject(nPara);
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();
            return true;
        }

        public override bool canSetStringList(int nPara)
        {
            return base.canSetStringList(nPara);
        }

        public override void addStringByLine(int nIndex, string cStr)
        {
        }
        #endregion

        #region :::::::::::::::::::::::::: event override   :::::::::::::::::::::::::
        public override void paint(Graphics dcGs)
        {
            DrawMenu(dcGs);
            base.paint(dcGs);
        }
        #endregion

        #region ::::::::::::::::::::::::::   ex funes    :::::::::::::::::::::::::
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void DrawMenu(Graphics e)
        {
            e.DrawImage(Img[48], pDrawPoint[2]);

            if (btnViewId == 1)
            {
                e.DrawString("", FormatStyle.Font22B,
                    FormatStyle.WhiteSolidBrush, rects[80], drawFormat);

                for (int i = 0; i < 7; i++)
                {
                    if (i == 4 || i == 5) continue;
                    if (buttonIsDown[19 + i])
                        e.DrawImage(Img[44], rects[81 + i]);
                    else
                        e.DrawImage(Img[42], rects[81 + i]);

                    e.DrawString(str1[i], FormatStyle.Font22B,
                        FormatStyle.BlackSolidBrush, rects[81 + i], drawFormat);
                }
            }
        }
        #endregion

        #region :::::::::::::::::::::::::: init funes :::::::::::::::::::::::::
        private void InitData()
        {
            drawFormat.LineAlignment = (StringAlignment)1;
            drawFormat.Alignment = (StringAlignment)1;

            RightFormat.LineAlignment = (StringAlignment)1;
            RightFormat.Alignment = (StringAlignment)2;

            LeftFormat.LineAlignment = (StringAlignment)1;
            LeftFormat.Alignment = (StringAlignment)0;

            bValue = new bool[50];
            oldbValue = new bool[50];
            setBValue = new bool[10];
            setBValueNumb = new int[10];

            fValue = new float[20];
            oldfValue = new float[20];
            setFValue = new float[5];
            setFValueNumb = new int[5];

            pDrawPoint = new PointF[20];

            rects = new RectangleF[20];

            Img = new Image[5];

            buttonIsDown = new bool[10];

            Rect = new List<Region>();

            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                Img[i] = Image.FromFile(_recPath + "\\" + UIObj.ParaList[i]);
            }



            //菜单
            rects[0] = new RectangleF(500, 0, 280, 48);
            for (int i = 0; i < 7; i++)
            {
                rects[1 + i] = new RectangleF(500, 100 + 70 * i, 280, 48);
            }
            //////////////////////////////////菜单内容
            //-------------菜单--------------btnViewId == 1
            //语言、测试、状态、重启、空白、空白、关闭
            //  19、  20、  21、  22、  23、  24、  25

            //-------------语言--------------btnViewId == 12
            //英文、中文、空白、空白、空白、空白、返回
            //  19、  20、  21、  22、  23、  24、  25

            //-------------测试--------------btnViewId == 13
            //声音、信息、空白、空白、空白、空白、返回
            //  19、  20、  21、  22、  23、  24、  25

            //-------------SOUNDS--------------btnViewId == 14
            //空白、空白、空白、空白、空白、NEXT、CLOSE
            //  19、  20、  21、  22、  23、  24、  25

            //-------------信息--直接跳页面--------------

            //-------------状态--------------btnViewId == 15
            //配置、网络、内部配置、空白、空白、空白、返回
            //  19、  20、  21、  22、  23、  24、  25

            //-------------配置--直接跳页面--------------

            //-------------DMI NETWORK--------------btnViewId == 151
            //空白、空白、空白、空白、空白、空白、CLOSE
            //  19、  20、  21、  22、  23、  24、  25

            //-------------内部配置--直接跳页面--------------
            for (int i = 0; i < 7; i++)
            {
                Rect.Add(new Region(rects[1 + i]));
            }


            str1 = new String[7] { "语言", "测试", "状态", "重新启动", "空白", "空白", "关闭" };
        }

        public static StringFormat drawFormat = new StringFormat();

        public static StringFormat RightFormat = new StringFormat();

        public static StringFormat LeftFormat = new StringFormat();

        internal String[] str1;


        /// <summary>
        /// 按键是否能按
        /// 
        /// </summary>
        private bool[] btnCanDown = new bool[7] { true, true, true, true, true, true, true };

        /// <summary>
        /// 当前所在页面
        /// 用来区分按键的有效性
        /// 0为主界面、1为菜单界面、2为声音、3为亮度、4为工号
        /// </summary>
        private int btnViewId = 0;

        /// <summary>
        /// 键是否按下
        /// </summary>
        internal bool[] buttonIsDown;

        /// <summary>
        /// 按键列表
        /// </summary>
        internal List<Region> Rect;

        #region:::::::::传值部分::::::::::::
        /// <summary>
        /// 当前周期接收数字量
        /// </summary>
        internal bool[] bValue;

        /// <summary>
        /// 前一个周期接收的数字量
        /// </summary>
        internal bool[] oldbValue;

        /// <summary>
        /// 发送的数字量
        /// </summary>
        internal bool[] setBValue;

        /// <summary>
        /// 发送的数字量在boollist中的序号
        /// </summary>
        internal int[] setBValueNumb;

        /// <summary>
        /// 接收模拟量
        /// </summary>
        internal float[] fValue;

        /// <summary>
        /// 前一个周期接收的模拟量
        /// </summary>
        internal float[] oldfValue;

        /// <summary>
        /// 发送的模拟量
        /// </summary>
        internal float[] setFValue;

        /// <summary>
        /// 发送的模拟量在floatlist中的序号
        /// </summary>
        internal int[] setFValueNumb;

        /// <summary>
        /// 坐标集
        /// </summary>
        internal PointF[] pDrawPoint;

        /// <summary>
        /// 矩形框集
        /// </summary>
        internal RectangleF[] rects;

        /// <summary>
        /// 图片集
        /// </summary>
        internal Image[] Img;
        #endregion#
        #endregion
    }
}
