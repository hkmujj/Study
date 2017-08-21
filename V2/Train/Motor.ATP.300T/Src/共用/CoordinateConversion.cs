using System.Collections.Generic;
using System.Drawing;

namespace Motor.ATP._300T.共用
{
    public static class Coordinate
    {
        public static List<RectangleF> RectangleFLists(View_ID_Name className)
        {
            int i, j;
            switch (className)
            {
                #region ::::::::::::::: 黑屏视图 :::::::::::::::

                case View_ID_Name.BlackScreen:
                    return TheRectFListBlackScreen;

                #endregion

                #region ::::::::::::::: 课程结束 :::::::::::::::

                case View_ID_Name.ClassOverScreen:
                    return TheRectFListClassOverScreen;

                #endregion

                #region ::::::::::::::: 故障接收 :::::::::::::::

                case View_ID_Name.FaultReceive:
                    return TheRectFListFaultReceive;

                #endregion

                #region ::::::::::::::: 主视图 :::::::::::::::

                case View_ID_Name.MainScreen:
                    //左1-A1_制动预警时间   /0-3
                    TheRectFListMainScreen.Add(RectsMovemont(1, 1, 60, 60));
                    TheRectFListMainScreen.Add(RectsMovemont(8.5f, 8.5f, 45, 45));
                    TheRectFListMainScreen.Add(RectsMovemont(15, 15, 30, 30));
                    TheRectFListMainScreen.Add(RectsMovemont(27, 27, 6, 6));
                    //左2-A2_目标距离   /4、5、6、7、8
                    TheRectFListMainScreen.Add(RectsMovemont(20, 75, 40, 30));
                    TheRectFListMainScreen.Add(RectsMovemont(10, 105, 28, 217)); //图
                    TheRectFListMainScreen.Add(RectsMovemont(40, 105, 18, 72)); //上半条
                    TheRectFListMainScreen.Add(RectsMovemont(40, 177, 18, 144)); //下半条
                    TheRectFListMainScreen.Add(RectsMovemont(40, 106, 18, 216)); //一整条

                    //左3-A3_ 、4-C8_级别信息、5-C9_ATP制动状态     /9-11
                    for (i = 0; i < 3; i++)
                    {
                        TheRectFListMainScreen.Add(RectsMovemont(0, 342 + i * 34, 64, 32));
                    }
                    //左6-E3_   /12
                    TheRectFListMainScreen.Add(RectsMovemont(1, 444, 61, 45));
                    //左7-E4_紧急信号、8-E5_机控/人控    /13、14
                    for (i = 0; i < 2; i++)
                    {
                        TheRectFListMainScreen.Add(RectsMovemont(1, 491 + i * 35, 61, 33));
                    }
                    //左9/E1_备用系统状态   /15
                    TheRectFListMainScreen.Add(RectsMovemont(1, 561, 61, 38));

                    //中大框-    /16
                    TheRectFListMainScreen.Add(RectsMovemont(64, 1, 357, 373));

                    //表盘      /17
                    TheRectFListMainScreen.Add(RectsMovemont(97, 35, 290, 290));
                    //指针      /18
                    TheRectFListMainScreen.Add(RectsMovemont(110, 48, 264, 264));

                    //底图      /19
                    TheRectFListMainScreen.Add(RectsMovemont(0, 0, 800, 600));

                    //当前速度  /20
                    TheRectFListMainScreen.Add(RectsMovemont(217, 155, 50, 50));

                    //速度光带  /21
                    TheRectFListMainScreen.Add(RectsMovemont(92, 30, 300, 300));
                    //钩子  /22
                    TheRectFListMainScreen.Add(RectsMovemont(97, 35, 290, 290));

                    //中大框中的小框
                    //23-B6_开口速度
                    TheRectFListMainScreen.Add(RectsMovemont(87, 325, 43, 42));
                    //命令图标  /24-B3_、25-B4_、26-B5
                    for (i = 0; i < 3; i++)
                    {
                        TheRectFListMainScreen.Add(RectsMovemont(170 + i * 45, 325, 43, 43));
                    }
                    //控制模式  27-B7
                    TheRectFListMainScreen.Add(RectsMovemont(340, 325, 43, 42));

                    //中中框    /28-35-C5\C6\C7\C1\C2\C3\C4
                    for (i = 0; i < 8; i++)
                    {
                        TheRectFListMainScreen.Add(RectsMovemont(64 + i * 45, 376, 43, 66));
                    }
                    //中中框的大框  /36-C1_下一控制模式
                    TheRectFListMainScreen.Add(RectsMovemont(199, 376, 86, 66));

                    //E19\E20\E21\E22_文本信息  /37-40
                    for (i = 0; i < 4; i++)
                    {
                        TheRectFListMainScreen.Add(RectsMovemont(64, 444 + i * 31, 299, 31));
                    }
                    //E23_公里标    /41
                    TheRectFListMainScreen.Add(RectsMovemont(64, 570, 299, 29));

                    //E24、E25_上下滚动键   /42、43
                    for (i = 0; i < 2; i++)
                    {
                        TheRectFListMainScreen.Add(RectsMovemont(366, 444 + i * 78, 55, 76));
                    }
                    //D6_机车信号   /44
                    TheRectFListMainScreen.Add(RectsMovemont(423, 1, 62, 62));
                    //空    /45
                    TheRectFListMainScreen.Add(RectsMovemont(487, 1, 234, 62));
                    //菜单  /46
                    TheRectFListMainScreen.Add(RectsMovemont(423, 1, 301, 373));
                    //E6-E10_车站名称   /47
                    TheRectFListMainScreen.Add(RectsMovemont(423, 376, 298, 64));
                    //E11-E15   /48-52
                    for (i = 0; i < 5; i++)
                    {
                        TheRectFListMainScreen.Add(RectsMovemont(423 + i * 60, 442, 58, 58));
                    }
                    //E16a_车次号   /53
                    TheRectFListMainScreen.Add(RectsMovemont(423, 502, 83, 97));
                    //E16b1_GSM-R网络状态\E16b2_TBC连接状态\E16c_放大\E16d_缩小     /54-57
                    for (i = 0; i < 2; i++)
                    {
                        for (j = 0; j < 2; j++)
                        {
                            TheRectFListMainScreen.Add(RectsMovemont(508 + j * 65, 502 + i * 49, 63, 47));
                        }
                    }
                    //E17_日期时间  /58
                    TheRectFListMainScreen.Add(RectsMovemont(638, 502, 83, 97));
                    //右框-D8\D7\D4\D5\D3\D2\D1_计划区消息  /59
                    TheRectFListMainScreen.Add(RectsMovemont(428, 68, 294, 284));
                    //横坐标 /60 - 67
                    for (i = 0; i < 8; i++)
                    {
                        TheRectFListMainScreen.Add(RectsMovemont(413 + i * 40, 352, 30, 20));
                    }
                    //纵坐标    /68-72
                    for (i = 0; i < 5; i++)
                    {
                        TheRectFListMainScreen.Add(RectsMovemont(430, 58 + i * 40, 40, 40));
                    }

                    return TheRectFListMainScreen;

                #endregion

                #region ::::::::::::::: 可扩展按键区 :::::::::::::::

                case View_ID_Name.OpenFunBtn:
                    //0-7
                    for (i = 0; i < 8; i++)
                    {
                        TheRectFListOpenFunBtn.Add(RectsMovemont(726, 1 + i * 75, 74, 74));
                    }
                    //菜单标题  /8
                    TheRectFListOpenFunBtn.Add(RectsMovemont(420, 1, 304, 61));
                    //          /9
                    TheRectFListOpenFunBtn.Add(RectsMovemont(420, 62, 304, 310));
                    //          /10-19
                    for (i = 0; i < 10; i++)
                    {
                        TheRectFListOpenFunBtn.Add(RectsMovemont(420, 65 + i * 30, 304, 30));
                    }
                    //          /20-29
                    for (i = 0; i < 10; i++)
                    {
                        TheRectFListOpenFunBtn.Add(RectsMovemont(472, 65 + i * 30, 200, 30));
                    }
                    //          /30-49
                    for (i = 0; i < 2; i++)
                    {
                        for (j = 0; j < 10; j++)
                        {
                            TheRectFListOpenFunBtn.Add(RectsMovemont(422 + i * 150, 65 + j * 30, 150, 30));
                        }
                    }
                    //
                    //E19\E20\E21\E22_文本信息  /50-53
                    for (i = 0; i < 4; i++)
                    {
                        TheRectFListOpenFunBtn.Add(RectsMovemont(64, 444 + i * 31, 299, 31));
                    }

                    //上下翻滚 //54-55
                    for (i = 0; i < 2; i++)
                    {
                        TheRectFListOpenFunBtn.Add(RectsMovemont(366, 444 + i * 78, 55, 76));
                    }

                    return TheRectFListOpenFunBtn;

                #endregion

                #region ::::::::::::::: 模拟实体按键区 :::::::::::::::

                case View_ID_Name.ButtonScreen:
                    //0-7
                    for (i = 0; i < 8; i++)
                    {
                        TheRectFListButtonScreen.Add(RectsMovemont(815, 2 + i * 75, 70, 70));
                    }
                    //8-18
                    for (i = 0; i < 11; i++)
                    {
                        TheRectFListButtonScreen.Add(RectsMovemont(2 + i * 75, 615, 70, 70));
                    }
                    //19
                    TheRectFListButtonScreen.Add(RectsMovemont(0, 0, 1000, 750));
                    return TheRectFListButtonScreen;

                    #endregion
            }

            return TheRectangleFList;
        }

        public static RectangleF TransformCoord(RectangleF rectangle)
        {
            return RectsMovemont(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        static RectangleF RectsMovemont(float x, float y, float width, float hight)
        {
            return new RectangleF(
                ( ( x + _offsetCoodinates_X ) * _scaling ),
                ( ( y + OffsetCoodinatesY ) * _scaling ),
                ( width * _scaling ),
                ( hight * _scaling ));
        }

        // ReSharper disable once InconsistentNaming
        private const bool _classIsInited = false;

        /// <summary>
        /// 初始化完成
        /// </summary>
        public static bool ClassIsInited
        {
            get { return _classIsInited; }
        }

        private static readonly List<RectangleF> TheRectangleFList = new List<RectangleF>();
        private static readonly List<RectangleF> TheRectFListBlackScreen = new List<RectangleF>();
        private static readonly List<RectangleF> TheRectFListClassOverScreen = new List<RectangleF>();
        private static readonly List<RectangleF> TheRectFListFaultReceive = new List<RectangleF>();
        private static readonly List<RectangleF> TheRectFListMainScreen = new List<RectangleF>();
        private static readonly List<RectangleF> TheRectFListButtonScreen = new List<RectangleF>();
        private static readonly List<RectangleF> TheRectFListOpenFunBtn = new List<RectangleF>();

        #region 屏大小偏移变化相关

        //偏移坐标X
        // ReSharper disable once InconsistentNaming
        private const int _offsetCoodinates_X = 0;

        /// <summary>
        /// 偏移坐标X
        /// </summary>
        public static int OffsetCoodinatesX
        {
            get { return _offsetCoodinates_X; }
        }

        //偏移坐标Y
        private const int OffsetCoodinatesY = 0;

        /// <summary>
        /// 偏移坐标Y
        /// </summary>
// ReSharper disable once InconsistentNaming
        public static int OffsetCoodinates_Y
        {
            get { return OffsetCoodinatesY; }
        }

        //缩放比例
        // ReSharper disable once InconsistentNaming
        private const float _scaling = 1.0f;

        /// <summary>
        /// 整体缩放比例
        /// </summary>
        public static float Scaling
        {
            get { return _scaling; }
        }

        #endregion
    }
}
