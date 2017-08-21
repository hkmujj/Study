using System.Collections.Generic;

namespace Motor.ATP._300T.共用.功能键与菜单.FunState
{
    public class StateProvider
    {
        private readonly OpenFunBtnCTCS300T m_OpenFunBtnCTCS300T;

        public StateProvider(OpenFunBtnCTCS300T openFunBtnCTCS300T)
        {
            BtnStrDict = new Dictionary<int, string[]>();
            MenuValueArrDict = new Dictionary<int, MenuValue[]>();
            m_OpenFunBtnCTCS300T = openFunBtnCTCS300T;
        }

        /// <summary>
        /// 按钮内容词典，根据MenuID变化
        /// </summary>
        public Dictionary<int, string[]> BtnStrDict { get; private set; }

        public Dictionary<int, MenuValue[]> MenuValueArrDict { get; private set; }

        public void FixModelText()
        {
            BtnStrDict[2][0] = m_OpenFunBtnCTCS300T.ModeSelect[0] ? "退出\n调车" : "调车";
            BtnStrDict[2][1] = m_OpenFunBtnCTCS300T.ModeSelect[1] ? "退出\n目视" : "目视";
            BtnStrDict[2][2] = m_OpenFunBtnCTCS300T.ModeSelect[2] ? "退出\n机信" : "机信";
        }

        public void InitalizeStates()
        {
            BtnStrDict.Clear();

            #region ::::::::::::::::::::::: _btnStrDict :::::::::::::::::::::::::::

            //未选择 = 0
            BtnStrDict.Add(0, new[] {"数据", "模式", "载频", "等级", "其他", "启动", "缓解", "警惕"});

            //确定取消 = 9
            BtnStrDict.Add(9,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", string.Empty, "取消"});

            //确定 = 10
            BtnStrDict.Add(10,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定"});

            //启动开始_司机号 = 100
            BtnStrDict.Add(100,
                new[] {"司机号", "车次号", string.Empty, string.Empty, string.Empty, "确定", "删除", "取消", "驾驶数据"});

            //启动开始_车次号 = 101
            BtnStrDict.Add(101,
                new[] {"司机号", "车次号", string.Empty, string.Empty, string.Empty, "确定", "删除", "取消", "驾驶数据"});

            //启动开始_司机车次号确定 = 1016
            BtnStrDict.Add(1016,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", string.Empty, "取消", "驾驶数据"});

            //启动开始_制动测试_存在制动测试成功 = 1021
            BtnStrDict.Add(1021,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", string.Empty, "取消"});

            //启动开始_制动测试_不存在制动测试成功 = 1022
            BtnStrDict.Add(1022,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定"});

            //F1数据 = 1
            BtnStrDict.Add(1, new[] {"司机号", "车次号", "列车\n数据", "向上", "向下", string.Empty, string.Empty, "返回"});

            //F1数据_F1司机号 = 11
            BtnStrDict.Add(11,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", "删除", "取消", "驾驶数据"});

            //F1数据_F1司机号_F6确定 = 116
            BtnStrDict.Add(116,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", string.Empty, "取消", "驾驶数据"});

            //F1数据_F2车次号 = 12
            BtnStrDict.Add(12,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", "删除", "取消", "驾驶数据"});

            //F1数据_F2车次号_F6确定 = 126
            BtnStrDict.Add(126,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", string.Empty, "取消", "驾驶数据"});

            //F1数据_F3列车数据 = 13
            BtnStrDict.Add(13,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", "删除", "取消", "列车数据"});

            //F1数据_F3列车数据_F6确定 = 136
            BtnStrDict.Add(136,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", string.Empty, "取消", "列车数据"});

            //F2模式 = 2
            BtnStrDict.Add(2,
                new[] {"调车", "目视", "机信", string.Empty, string.Empty, string.Empty, string.Empty, "返回", "模式选择"});

            //F2模式_F1调车 = 210
            BtnStrDict.Add(210,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "确定",
                    string.Empty,
                    "取消",
                    "调车模式确定",
                    "请您确定是否\r\n选择调车模式？"
                });

            //F2模式_F2目视 = 220
            BtnStrDict.Add(220,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "确定",
                    string.Empty,
                    "取消",
                    "目视模式确定",
                    "请您确定是否\r\n选择目视模式？"
                });

            //F2模式_F3机信 = 230
            BtnStrDict.Add(230,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "确定",
                    string.Empty,
                    "取消",
                    "机信模式确定",
                    "请您确定是否\r\n选择机信模式？"
                });

            //F2模式_F1调车 = 211
            BtnStrDict.Add(211,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "确定",
                    string.Empty,
                    "取消",
                    "退出调车模式",
                    "请您确定是否\r\n退出调车模式？"
                });

            //F2模式_F2目视 = 221
            BtnStrDict.Add(221,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "确定",
                    string.Empty,
                    "取消",
                    "退出目视模式",
                    "请您确定是否\r\n退出目视模式？"
                });

            //F2模式_F3机信 = 231
            BtnStrDict.Add(231,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "确定",
                    string.Empty,
                    "取消",
                    "退出机信模式",
                    "请您确定是否\r\n退出机信模式？"
                });
            //F3载频 = 3
            BtnStrDict.Add(3,
                new[]
                {"上行", "下行", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "返回", "载频选择"});

            //F3载频_F1上行 = 31
            BtnStrDict.Add(31,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "确定",
                    string.Empty,
                    "取消",
                    "上行载频确定",
                    "请您确定是否\r\n\r\n 选择上行载频?"
                });

            //F3载频_F1上行_确定 = 316
            BtnStrDict.Add(316,
                new[]
                {"上行", "下行", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", "载频选择"});

            //F3载频_F2下行 = 32
            BtnStrDict.Add(32,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "确定",
                    string.Empty,
                    "取消",
                    "下行载频确定",
                    "请您确定是否\r\n\r\n 选择下行载频?"
                });

            //F3载频_F2下行_确定 = 326
            BtnStrDict.Add(326,
                new[]
                {"上行", "下行", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", "载频选择"});

            //F4等级 = 4
            BtnStrDict.Add(4,
                new[]
                {
                    "CTCS3",
                    "CTCS2",
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "返回",
                    "CTCS等级选择"
                });

            //F4等级_F1CTCS3 = 41
            BtnStrDict.Add(41,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", string.Empty, "取消"});

            //F4等级_F1CTCS3_RBC = 411
            BtnStrDict.Add(411,
                new[] {"RBC\nID", "电话\n号码", string.Empty, string.Empty, string.Empty, "确定", "删除", "取消", "RBC数据"});

            //F4等级_F1CTCS3_电话号码
            BtnStrDict.Add(412,
                new[] {"RBC\nID", "电话\n号码", string.Empty, string.Empty, string.Empty, "确定", "删除", "取消", "RBC数据"});

            //F4等级_F2CTCS3_RBC确认 = 4116
            BtnStrDict.Add(4116,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", string.Empty, "取消", "驾驶数据"});

            //F4等级_F2CTCS2 = 42
            BtnStrDict.Add(42,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", string.Empty, "取消"});

            //F5其他 = 5
            BtnStrDict.Add(5,
                new[] {"制动测试", "音量", "亮度", string.Empty, string.Empty, string.Empty, string.Empty, "返回"});

            //F5制动测试 = 51
            BtnStrDict.Add(51,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定", string.Empty, "取消"});

            //F5其他_F2音量 = 52
            BtnStrDict.Add(52,
                new[]
                {"大", "小", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "返回", "音量设置"});

            //F5其他_F3亮度 = 53
            BtnStrDict.Add(53,
                new[]
                {"亮", "暗", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "返回", "亮度设置"});

            //F6启动 = 6
            BtnStrDict.Add(6,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "确定",
                    string.Empty,
                    "取消",
                    "启动确认",
                    "请您确认是否\r\n\r\n  需要启动设备？"
                });

            //F6启动_F6确定 = 66
            BtnStrDict.Add(66,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定"});

            //F7缓解 = 7
            BtnStrDict.Add(7,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "确定",
                    string.Empty,
                    "取消",
                    "缓解确认",
                    "是否缓解，请确定？"
                });

            //F7缓解_F6确定 = 76
            BtnStrDict.Add(76,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定"});

            //F8警惕 = 8
            BtnStrDict.Add(8,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定"});
            //F4等级_F1_CTCS3_确认
            BtnStrDict.Add(414,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "确定"});

            #endregion

            MenuValueArrDict.Clear();

            #region :::::::::::::::::::::::  MenuOfFun  :::::::::::::::::::::::::::

            //启动开始_司机号 = 100
            MenuValueArrDict.Add(100,
                new[]
                {
                    new MenuValue(false, false, "司机号：", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.DriverId, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "车次号：", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TrainId, null, m_OpenFunBtnCTCS300T.RectList[27])
                });

            //启动开始_车次号 = 101
            MenuValueArrDict.Add(101,
                new[]
                {
                    new MenuValue(false, false, "司机号：", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.DriverId, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "车次号：", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.TrainId, null, m_OpenFunBtnCTCS300T.RectList[27])
                });

            //启动开始_司机车次号确定 = 1016
            MenuValueArrDict.Add(1016,
                new[]
                {
                    new MenuValue(false, false, "司机号：", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.DriverId, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "车次号：", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TrainId, null, m_OpenFunBtnCTCS300T.RectList[27])
                });

            //F1数据_F1司机号 = 11
            MenuValueArrDict.Add(11,
                new[]
                {
                    new MenuValue(false, false, "司机号：", null, m_OpenFunBtnCTCS300T.RectList[33]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.DriverId, null, m_OpenFunBtnCTCS300T.RectList[24])
                });
            //F1数据_F1司机号_F6确定 = 116
            MenuValueArrDict.Add(116,
                new[]
                {
                    new MenuValue(false, false, "司机号：", null, m_OpenFunBtnCTCS300T.RectList[33]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.DriverId, null, m_OpenFunBtnCTCS300T.RectList[24])
                });
            //F1数据_F2车次号 = 12
            MenuValueArrDict.Add(12,
                new[]
                {
                    new MenuValue(false, false, "车次号：", null, m_OpenFunBtnCTCS300T.RectList[33]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.TrainId, null, m_OpenFunBtnCTCS300T.RectList[24])
                });
            //F1数据_F2车次号_F6确定 = 126
            MenuValueArrDict.Add(126,
                new[]
                {
                    new MenuValue(false, false, "车次号：", null, m_OpenFunBtnCTCS300T.RectList[33]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TrainId, null, m_OpenFunBtnCTCS300T.RectList[24])
                });
            //F1数据_F3列车数据 = 13
            MenuValueArrDict.Add(13,
                new[]
                {
                    new MenuValue(false, false, "工作个数:1(8编),2(16编)", null, m_OpenFunBtnCTCS300T.RectList[12]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.TrainData, null, m_OpenFunBtnCTCS300T.RectList[24])
                });
            //F1数据_F3列车数据_F6确定 = 136
            MenuValueArrDict.Add(136,
                new[]
                {
                    new MenuValue(false, false, "工作个数:1(8编),2(16编)", null, m_OpenFunBtnCTCS300T.RectList[12]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TrainData, null, m_OpenFunBtnCTCS300T.RectList[24])
                });
            //F4等级_F1CTCS3_RBC = 411
            MenuValueArrDict.Add(411,
                new[]
                {
                    new MenuValue(false, false, "RBC  ID：", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.RBCID, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "电话号码：", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TelNmub, null, m_OpenFunBtnCTCS300T.RectList[27]),
                    //new MenuValue(false, false, "网络编号：", null, m_RectList[36]),
                    //  new MenuValue(true, false, NetId, null, m_RectList[27]),
                });
            //F4等级_F1CTCS3_电话号码 = 412
            MenuValueArrDict.Add(412,
                new[]
                {
                    new MenuValue(false, false, "RBC  ID：", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.RBCID, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "电话号码：", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.TelNmub, null, m_OpenFunBtnCTCS300T.RectList[27]),
                    // new MenuValue(false, false, "网络编号：", null, m_RectList[36]),
                    //  new MenuValue(true, false, NetId, null, m_RectList[27]),
                });

            MenuValueArrDict.Add(413,
                new[]
                {
                    new MenuValue(false, false, "RBC  ID：", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.RBCID, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "电话号码：", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TelNmub, null, m_OpenFunBtnCTCS300T.RectList[27]),
                    //  new MenuValue(false, false, "网络编号：", null, m_RectList[36]),
                    //  new MenuValue(true, true, NetId, null, m_RectList[27]),
                });

            //F4等级_F2CTCS3_RBC确认 = 4116
            MenuValueArrDict.Add(4116,
                new[]
                {
                    new MenuValue(false, false, "RBC  ID：", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.RBCID, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "电话号码：", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TelNmub, null, m_OpenFunBtnCTCS300T.RectList[27]),
                    //  new MenuValue(false, false, "网络编号：", null, m_RectList[36]),
                    //  new MenuValue(true, false, NetId, null, m_RectList[27]),
                });

            //F2模式 = 2
            MenuValueArrDict.Add(2,
                new[]
                {
                    new MenuValue(false, false, "调车模式", null, m_OpenFunBtnCTCS300T.RectList[22], FontRelated.居中),
                    new MenuValue(false, false, "目视模式", null, m_OpenFunBtnCTCS300T.RectList[24], FontRelated.居中),
                    new MenuValue(false, false, "机车信号模式", null, m_OpenFunBtnCTCS300T.RectList[26], FontRelated.居中)
                });
            //F3载频 = 3
            MenuValueArrDict.Add(3,
                new[]
                {
                    new MenuValue(false, false, "上行载频", null, m_OpenFunBtnCTCS300T.RectList[22], FontRelated.居中),
                    new MenuValue(false, false, "下行载频", null, m_OpenFunBtnCTCS300T.RectList[24], FontRelated.居中)
                });
            //F3载频_F1上行_确定 = 316
            MenuValueArrDict.Add(316,
                new[]
                {
                    new MenuValue(false, true, "上行载频", null, m_OpenFunBtnCTCS300T.RectList[22], FontRelated.居中),
                    new MenuValue(false, false, "下行载频", null, m_OpenFunBtnCTCS300T.RectList[24], FontRelated.居中)
                });

            //F3载频_F2下行_确定 = 326
            MenuValueArrDict.Add(326,
                new[]
                {
                    new MenuValue(false, false, "上行载频", null, m_OpenFunBtnCTCS300T.RectList[22], FontRelated.居中),
                    new MenuValue(false, true, "下行载频", null, m_OpenFunBtnCTCS300T.RectList[24], FontRelated.居中)
                });
            //F4等级 = 4
            MenuValueArrDict.Add(4,
                new[]
                {
                    new MenuValue(false, false, "CTCS3 级", null, m_OpenFunBtnCTCS300T.RectList[22], FontRelated.居中),
                    new MenuValue(false, false, "CTCS2 级", null, m_OpenFunBtnCTCS300T.RectList[24], FontRelated.居中)
                });
            m_OpenFunBtnCTCS300T.m_AppraiseList = new List<FunMenuButtonId>()
            {
                FunMenuButtonId.F1数据_F1司机号,
                FunMenuButtonId.F1数据_F1司机号_F6确定,
                FunMenuButtonId.启动开始_司机号,
                FunMenuButtonId.启动开始_车次号,
                FunMenuButtonId.启动开始_司机车次号确定
            };

            #endregion
        }
    }
}