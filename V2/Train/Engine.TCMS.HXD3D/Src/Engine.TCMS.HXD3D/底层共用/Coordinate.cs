using System.Collections.Generic;
using System.Drawing;

namespace Engine.TCMS.HXD3D.底层共用
{
    public static class Coordinate
    {
        // Fields
        private static readonly List<RectangleF> AuxPowerTestArea = new List<RectangleF>();

        private static readonly List<RectangleF> BrakeSettingArea = new List<RectangleF>();
        private static readonly List<RectangleF> ControlPantoSelectionArea = new List<RectangleF>();
        private static readonly List<RectangleF> CutOffArea = new List<RectangleF>();
        private static readonly List<RectangleF> CutOffStateArea = new List<RectangleF>();
        private static readonly List<RectangleF> DataInOthersArea = new List<RectangleF>();
        private static readonly List<RectangleF> DistanceCountersArea = new List<RectangleF>();
        private static readonly List<RectangleF> FaultEnsureArea = new List<RectangleF>();
        private static readonly List<RectangleF> FaultFiltrationArea = new List<RectangleF>();
        private static readonly List<RectangleF> LampTestArea = new List<RectangleF>();
        private static readonly List<RectangleF> MainArea = new List<RectangleF>();
        private static readonly List<RectangleF> MainControlTestArea = new List<RectangleF>();
        private static readonly List<RectangleF> MainScreenArea = new List<RectangleF>();
        private static readonly List<RectangleF> MaintenanceLubrificationValveTestArea = new List<RectangleF>();
        private static readonly int m_OffsetCoodinatesX = 0;
        private static readonly int m_OffsetCoodinatesY = 0;
        private static readonly List<RectangleF> ProcessAuxArea = new List<RectangleF>();
        private static readonly List<RectangleF> ProcessBatteryArea = new List<RectangleF>();
        private static readonly List<RectangleF> ProcessCbArea = new List<RectangleF>();
        private static readonly List<RectangleF> ProcessCountersArea = new List<RectangleF>();
        private static readonly List<RectangleF> ProcessDrivesArea = new List<RectangleF>();
        private static readonly List<RectangleF> ProcessNetControlArea = new List<RectangleF>();
        private static readonly List<RectangleF> ProcessTebeArea = new List<RectangleF>();
        private static readonly float m_Scaling = 1f;
        private static readonly List<RectangleF> StartTestArea = new List<RectangleF>();
        private static readonly List<RectangleF> TitleArea = new List<RectangleF>();
        private static readonly List<RectangleF> TitleScreenArea = new List<RectangleF>();
        private static readonly List<RectangleF> TrainOverviewArea = new List<RectangleF>();
        private static readonly List<RectangleF> TrainPowerArea = new List<RectangleF>();
        private static readonly List<RectangleF> ZeroOrderTestArea = new List<RectangleF>();

        static Coordinate()
        {
            Init();
        }

        // Methods
        private static void Init()
        {
            int num;
            int num2;
            TitleArea.Add(RectsMovemont(8f, 16f, 132f, 42f));
            TitleArea.Add(RectsMovemont(139f, 16f, 318f, 42f));
            for (num = 0; num < 2; num++)
            {
                TitleArea.Add(RectsMovemont(0x1ca + num * 170, 16f, 170f, 42f));
            }
            TitleArea.Add(RectsMovemont(5f, 542f, 173f, 50f));
            for (num = 0; num < 10; num++)
            {
                TitleArea.Add(RectsMovemont(0xb7 + num * 0x3f, 542f, 54f, 54f));
            }
            TitleArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            TitleArea.Add(RectsMovemont(6f, 543f, 171f, 48f));
            for (num = 0; num < 10; num++)
            {
                TitleArea.Add(RectsMovemont(0xb8 + num * 0x3f, 543f, 50f, 50f));
            }
            MainScreenArea.Add(RectsMovemont(34f, 158f, 50f, 25f));
            MainScreenArea.Add(RectsMovemont(120f, 158f, 50f, 25f));
            MainScreenArea.Add(RectsMovemont(221f, 158f, 50f, 25f));
            MainScreenArea.Add(RectsMovemont(314f, 158f, 50f, 25f));
            MainScreenArea.Add(RectsMovemont(416f, 158f, 50f, 25f));
            MainScreenArea.Add(RectsMovemont(471f, 158f, 50f, 25f));
            MainScreenArea.Add(RectsMovemont(564f, 158f, 50f, 25f));
            MainScreenArea.Add(RectsMovemont(619f, 158f, 50f, 25f));
            MainScreenArea.Add(RectsMovemont(34f, 185f, 50f, 256f));
            MainScreenArea.Add(RectsMovemont(120f, 185f, 50f, 256f));
            MainScreenArea.Add(RectsMovemont(221f, 185f, 50f, 256f));
            MainScreenArea.Add(RectsMovemont(314f, 185f, 50f, 256f));
            MainScreenArea.Add(RectsMovemont(416f, 185f, 50f, 256f));
            MainScreenArea.Add(RectsMovemont(471f, 185f, 50f, 256f));
            MainScreenArea.Add(RectsMovemont(564f, 185f, 50f, 256f));
            MainScreenArea.Add(RectsMovemont(619f, 185f, 50f, 256f));
            MainScreenArea.Add(RectsMovemont(746f, 138f, 50f, 24f));
            MainScreenArea.Add(RectsMovemont(746f, 162f, 50f, 28f));
            MainScreenArea.Add(RectsMovemont(746f, 190f, 25f, 29f));
            MainScreenArea.Add(RectsMovemont(771f, 190f, 25f, 29f));
            MainScreenArea.Add(RectsMovemont(679f, 244f, 112f, 38f));
            MainScreenArea.Add(RectsMovemont(679f, 321f, 112f, 38f));
            MainScreenArea.Add(RectsMovemont(693f, 449f, 89f, 38f));
            MainScreenArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            MainScreenArea.Add(RectsMovemont(221f, 446f, 50f, 48f));
            MainScreenArea.Add(RectsMovemont(379f, 525f, 417f, 28f));
            MainScreenArea.Add(RectsMovemont(222f, 447f, 46f, 46f));
            MainArea.Add(RectsMovemont(45f, 116f, 37f, 22f));
            MainArea.Add(RectsMovemont(121f, 116f, 37f, 22f));
            MainArea.Add(RectsMovemont(265f, 116f, 37f, 22f));
            MainArea.Add(RectsMovemont(351f, 116f, 37f, 22f));
            MainArea.Add(RectsMovemont(43f, 145f, 38f, 209f));
            MainArea.Add(RectsMovemont(119f, 145f, 38f, 209f));
            MainArea.Add(RectsMovemont(261f, 145f, 38f, 209f));
            MainArea.Add(RectsMovemont(346f, 145f, 38f, 209f));
            for (num = 0; num < 9; num++)
            {
                num2 = 0;
                while (num2 < 2)
                {
                    MainArea.Add(RectsMovemont(4 + num * 0x2c, 0x1c3 + num2 * 0x2d, 43f, 41f));
                    num2++;
                }
            }
            for (num = 0; num < 2; num++)
            {
                MainArea.Add(RectsMovemont(462f, 0x55 + num * 0x33, 96f, 31f));
            }
            for (num = 0; num < 2; num++)
            {
                num2 = 0;
                while (num2 < 2)
                {
                    MainArea.Add(RectsMovemont(0x216 + num * 0x84, 0xb2 + num2 * 30, 61f, 28f));
                    num2++;
                }
            }
            MainArea.Add(RectsMovemont(456f, 239f, 344f, 26f));
            MainArea.Add(RectsMovemont(461f, 267f, 350f, 121f));
            MainArea.Add(RectsMovemont(456f, 391f, 344f, 23f));
            MainArea.Add(RectsMovemont(461f, 416f, 350f, 121f));
            MainArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            for (num = 0; num < 9; num++)
            {
                num2 = 0;
                while (num2 < 2)
                {
                    MainArea.Add(RectsMovemont(5 + num * 0x2c, 0x1c4 + num2 * 0x2d, 41f, 40f));
                    num2++;
                }
            }
            for (num = 0; num < 6; num++)
            {
                MainArea.Add(RectsMovemont(461f, 0x10b + 20 * num, 350f, 20f));
            }
            for (num = 0; num < 6; num++)
            {
                MainArea.Add(RectsMovemont(461f, 0x1a0 + 20 * num, 350f, 20f));
            }
            TitleScreenArea.Add(RectsMovemont(0f, 0f, 99f, 44f));
            for (num = 0; num < 7; num++)
            {
                TitleScreenArea.Add(RectsMovemont(0x63 + num * 100, 0f, 99f, 44f));
            }
            for (num = 0; num < 7; num++)
            {
                TitleScreenArea.Add(RectsMovemont(num * 100, 46f, 99f, 44f));
            }
            TitleScreenArea.Add(RectsMovemont(699f, 46f, 99f, 44f));
            TitleScreenArea.Add(RectsMovemont(1f, 93f, 500f, 22f));
            for (num = 0; num < 3; num++)
            {
                TitleScreenArea.Add(RectsMovemont(500 + num * 100, 93f, 100f, 22f));
            }
            TitleScreenArea.Add(RectsMovemont(2f, 556f, 376f, 42f));
            TitleScreenArea.Add(RectsMovemont(379f, 556f, 42f, 42f));
            TitleScreenArea.Add(RectsMovemont(422f, 556f, 375f, 42f));
            for (num = 0; num < 2; num++)
            {
                TitleScreenArea.Add(RectsMovemont(758f, 0x22c + num * 0x16, 40f, 20f));
            }
            TitleScreenArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            TitleScreenArea.Add(RectsMovemont(2f, 2f, 95f, 40f));
            for (num = 0; num < 7; num++)
            {
                TitleScreenArea.Add(RectsMovemont(0x65 + num * 100, 2f, 95f, 40f));
            }
            for (num = 0; num < 7; num++)
            {
                TitleScreenArea.Add(RectsMovemont(2 + num * 100, 48f, 95f, 40f));
            }
            TitleScreenArea.Add(RectsMovemont(701f, 48f, 95f, 40f));
            TitleScreenArea.Add(RectsMovemont(192f, 208f, 389f, 221f));
            TitleScreenArea.Add(RectsMovemont(379f, 525f, 417f, 28f));
            for (num = 0; num < 10; num++)
            {
                TitleScreenArea.Add(RectsMovemont(422f + 37.5f * num, 556f, 37.5f, 42f));
            }
            for (num = 0; num < 10; num++)
            {
                TitleScreenArea.Add(RectsMovemont(424f + 37.5f * num, 558f, 33.5f, 38f));
            }
            TrainOverviewArea.Add(RectsMovemont(47f, 128f, 38f, 38f));
            TrainOverviewArea.Add(RectsMovemont(163f, 188f, 43f, 30f));
            for (num = 0; num < 2; num++)
            {
                TrainOverviewArea.Add(RectsMovemont(340 + num * 0x138, 128f, 38f, 38f));
            }
            TrainOverviewArea.Add(RectsMovemont(469f, 133f, 94f, 18f));
            for (num = 0; num < 2; num++)
            {
                TrainOverviewArea.Add(RectsMovemont(0x128 + num * 0x1a1, 181f, 21f, 19f));
            }
            for (num = 0; num < 2; num++)
            {
                TrainOverviewArea.Add(RectsMovemont(0x175 + num * 0xe3, 191f, 57f, 32f));
            }
            for (num = 0; num < 2; num++)
            {
                TrainOverviewArea.Add(RectsMovemont(496f, 0xa7 + num * 0x2b, 38f, 38f));
            }
            for (num = 0; num < 3; num++)
            {
                TrainOverviewArea.Add(RectsMovemont(0x138 + num * 0x2b, 227f, 32f, 32f));
            }
            for (num = 0; num < 3; num++)
            {
                TrainOverviewArea.Add(RectsMovemont(600 + num * 0x2b, 227f, 32f, 32f));
            }
            TrainOverviewArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            CutOffArea.Add(RectsMovemont(176f, 173f, 343f, 287f));
            for (num = 0; num < 4; num++)
            {
                CutOffArea.Add(RectsMovemont(0xc4 + num * 0x4c, 198f, 75f, 29f));
            }
            for (num = 0; num < 4; num++)
            {
                CutOffArea.Add(RectsMovemont(0xc4 + num * 0x4c, 329f, 75f, 29f));
            }
            CutOffArea.Add(RectsMovemont(534f, 198f, 75f, 29f));
            CutOffArea.Add(RectsMovemont(534f, 329f, 75f, 29f));
            for (num = 0; num < 4; num++)
            {
                CutOffArea.Add(RectsMovemont(0xc6 + num * 0x4c, 200f, 71f, 25f));
            }
            for (num = 0; num < 4; num++)
            {
                CutOffArea.Add(RectsMovemont(0xc6 + num * 0x4c, 331f, 71f, 25f));
            }
            CutOffArea.Add(RectsMovemont(536f, 200f, 71f, 25f));
            CutOffArea.Add(RectsMovemont(536f, 331f, 71f, 25f));
            for (num = 0; num < 4; num++)
            {
                CutOffArea.Add(RectsMovemont(0xc4 + num * 0x4c, 228f, 75f, 29f));
            }
            for (num = 0; num < 4; num++)
            {
                CutOffArea.Add(RectsMovemont(0xc4 + num * 0x4c, 359f, 75f, 29f));
            }
            CutOffArea.Add(RectsMovemont(534f, 228f, 75f, 29f));
            CutOffArea.Add(RectsMovemont(534f, 359f, 75f, 29f));
            for (num = 0; num < 4; num++)
            {
                CutOffArea.Add(RectsMovemont(0xc6 + num * 0x4c, 230f, 71f, 25f));
            }
            for (num = 0; num < 4; num++)
            {
                CutOffArea.Add(RectsMovemont(0xc6 + num * 0x4c, 361f, 71f, 25f));
            }
            CutOffArea.Add(RectsMovemont(536f, 230f, 71f, 25f));
            CutOffArea.Add(RectsMovemont(536f, 361f, 71f, 25f));
            for (num = 0; num < 4; num++)
            {
                CutOffArea.Add(RectsMovemont(0xc4 + num * 0x4c, 258f, 75f, 49f));
            }
            for (num = 0; num < 4; num++)
            {
                CutOffArea.Add(RectsMovemont(0xc4 + num * 0x4c, 390f, 75f, 49f));
            }
            CutOffArea.Add(RectsMovemont(534f, 258f, 75f, 49f));
            CutOffArea.Add(RectsMovemont(534f, 390f, 75f, 49f));
            for (num = 0; num < 4; num++)
            {
                CutOffArea.Add(RectsMovemont(0xc6 + num * 0x4c, 260f, 71f, 45f));
            }
            for (num = 0; num < 4; num++)
            {
                CutOffArea.Add(RectsMovemont(0xc6 + num * 0x4c, 392f, 71f, 45f));
            }
            CutOffArea.Add(RectsMovemont(536f, 260f, 71f, 45f));
            CutOffArea.Add(RectsMovemont(536f, 392f, 71f, 45f));
            CutOffArea.Add(RectsMovemont(589f, 506f, 99f, 35f));
            CutOffArea.Add(RectsMovemont(591f, 508f, 95f, 31f));
            CutOffArea.Add(RectsMovemont(689f, 506f, 99f, 35f));
            CutOffArea.Add(RectsMovemont(691f, 508f, 95f, 31f));
            for (num = 0; num < 4; num++)
            {
                ControlPantoSelectionArea.Add(RectsMovemont(32f, 0xb8 + num * 0x40, 95f, 38f));
            }
            for (num = 0; num < 4; num++)
            {
                ControlPantoSelectionArea.Add(RectsMovemont(34f, 0xba + num * 0x40, 91f, 34f));
            }
            for (num = 0; num < 2; num++)
            {
                ControlPantoSelectionArea.Add(RectsMovemont(100 * num, 507f, 100f, 44f));
            }
            for (num = 0; num < 2; num++)
            {
                ControlPantoSelectionArea.Add(RectsMovemont(2 + 100 * num, 509f, 96f, 40f));
            }
            for (num = 0; num < 2; num++)
            {
                ControlPantoSelectionArea.Add(RectsMovemont(0x128 + num * 0x138, 248f, 38f, 38f));
            }
            for (num = 0; num < 2; num++)
            {
                ControlPantoSelectionArea.Add(RectsMovemont(0xfc + num * 0x1a1, 301f, 21f, 19f));
            }
            ControlPantoSelectionArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            ControlPantoSelectionArea.Add(RectsMovemont(192f, 208f, 389f, 221f));
            ControlPantoSelectionArea.Add(RectsMovemont(425f, 253f, 94f, 18f));
            for (num = 0; num < 2; num++)
            {
                DistanceCountersArea.Add(RectsMovemont(89f, 200 + num * 0xa8, 238f, 22f));
            }
            for (num = 0; num < 2; num++)
            {
                DistanceCountersArea.Add(RectsMovemont(89f, 0xe1 + num * 0xa8, 238f, 62f));
            }
            DistanceCountersArea.Add(RectsMovemont(360f, 224f, 136f, 64f));
            DistanceCountersArea.Add(RectsMovemont(362f, 226f, 132f, 60f));
            DistanceCountersArea.Add(RectsMovemont(360f, 392f, 136f, 64f));
            DistanceCountersArea.Add(RectsMovemont(362f, 394f, 132f, 60f));
            BrakeSettingArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            BrakeSettingArea.Add(RectsMovemont(54f, 162f, 50f, 20f));
            BrakeSettingArea.Add(RectsMovemont(187f, 162f, 50f, 20f));
            BrakeSettingArea.Add(RectsMovemont(249f, 162f, 50f, 20f));
            BrakeSettingArea.Add(RectsMovemont(404f, 162f, 50f, 20f));
            BrakeSettingArea.Add(RectsMovemont(550f, 162f, 50f, 20f));
            BrakeSettingArea.Add(RectsMovemont(611f, 162f, 50f, 20f));
            BrakeSettingArea.Add(RectsMovemont(708f, 162f, 50f, 20f));
            BrakeSettingArea.Add(RectsMovemont(54f, 194f, 50f, 218f));
            BrakeSettingArea.Add(RectsMovemont(187f, 194f, 50f, 218f));
            BrakeSettingArea.Add(RectsMovemont(249f, 194f, 50f, 218f));
            BrakeSettingArea.Add(RectsMovemont(404f, 194f, 50f, 218f));
            BrakeSettingArea.Add(RectsMovemont(550f, 195f, 50f, 218f));
            BrakeSettingArea.Add(RectsMovemont(611f, 195f, 50f, 218f));
            BrakeSettingArea.Add(RectsMovemont(708f, 196f, 50f, 218f));
            for (num = 0; num < 2; num++)
            {
                num2 = 0;
                while (num2 < 4)
                {
                    CutOffStateArea.Add(RectsMovemont(0x30 + 0xaf * num, 0xd1 + 0x2d * num2, 167f, 42f));
                    num2++;
                }
            }
            for (num = 0; num < 3; num++)
            {
                CutOffStateArea.Add(RectsMovemont(398f, 0xd1 + 0x2d * num, 167f, 42f));
            }
            CutOffStateArea.Add(RectsMovemont(573f, 209f, 167f, 42f));
            for (num = 0; num < 2; num++)
            {
                num2 = 0;
                while (num2 < 4)
                {
                    CutOffStateArea.Add(RectsMovemont(50 + 0xaf * num, 0xd3 + 0x2d * num2, 163f, 38f));
                    num2++;
                }
            }
            for (num = 0; num < 3; num++)
            {
                CutOffStateArea.Add(RectsMovemont(400f, 0xd3 + 0x2d * num, 163f, 38f));
            }
            CutOffStateArea.Add(RectsMovemont(575f, 211f, 163f, 38f));
            TrainPowerArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            TrainPowerArea.Add(RectsMovemont(43f, 355f, 55f, 25f));
            TrainPowerArea.Add(RectsMovemont(43f, 442f, 55f, 25f));
            TrainPowerArea.Add(RectsMovemont(213f, 155f, 38f, 21f));
            TrainPowerArea.Add(RectsMovemont(213f, 335f, 38f, 21f));
            TrainPowerArea.Add(RectsMovemont(626f, 155f, 38f, 21f));
            TrainPowerArea.Add(RectsMovemont(626f, 277f, 38f, 21f));
            TrainPowerArea.Add(RectsMovemont(626f, 335f, 38f, 21f));
            TrainPowerArea.Add(RectsMovemont(626f, 458f, 38f, 21f));
            TrainPowerArea.Add(RectsMovemont(212f, 217f, 39f, 19f));
            TrainPowerArea.Add(RectsMovemont(212f, 398f, 39f, 19f));
            TrainPowerArea.Add(RectsMovemont(200f, 280f, 52f, 24f));
            TrainPowerArea.Add(RectsMovemont(200f, 462f, 52f, 24f));
            TrainPowerArea.Add(RectsMovemont(202f, 282f, 50f, 22f));
            TrainPowerArea.Add(RectsMovemont(202f, 464f, 50f, 22f));
            TrainPowerArea.Add(RectsMovemont(278f, 154f, 69f, 21f));
            TrainPowerArea.Add(RectsMovemont(461f, 155f, 69f, 21f));
            TrainPowerArea.Add(RectsMovemont(278f, 336f, 69f, 21f));
            TrainPowerArea.Add(RectsMovemont(461f, 335f, 69f, 21f));
            TrainPowerArea.Add(RectsMovemont(356f, 159f, 100f, 143f));
            TrainPowerArea.Add(RectsMovemont(358f, 161f, 96f, 139f));
            TrainPowerArea.Add(RectsMovemont(356f, 341f, 100f, 143f));
            TrainPowerArea.Add(RectsMovemont(358f, 343f, 96f, 139f));
            TrainPowerArea.Add(RectsMovemont(465f, 227f, 57f, 39f));
            TrainPowerArea.Add(RectsMovemont(464f, 411f, 57f, 39f));
            TrainPowerArea.Add(RectsMovemont(699f, 233f, 48f, 20f));
            TrainPowerArea.Add(RectsMovemont(699f, 414f, 48f, 20f));
            ProcessDrivesArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            for (num = 0; num < 3; num++)
            {
                num2 = 0;
                while (num2 < 3)
                {
                    ProcessDrivesArea.Add(RectsMovemont(0x9e + 260 * num, 130 + 0x18 * num2, 98f, 22f));
                    num2++;
                }
            }
            for (num = 0; num < 2; num++)
            {
                num2 = 0;
                while (num2 < 2)
                {
                    ProcessDrivesArea.Add(RectsMovemont(0x101 + 0x44 * num, 0x18f + 0x21 * num2, 66f, 31f));
                    num2++;
                }
            }
            for (num = 0; num < 6; num++)
            {
                num2 = 0;
                while (num2 < 3)
                {
                    ProcessDrivesArea.Add(RectsMovemont(0x185 + 0x44 * num, 0xf4 + 0x21 * num2, 66f, 31f));
                    num2++;
                }
            }
            for (num = 0; num < 6; num++)
            {
                num2 = 0;
                while (num2 < 2)
                {
                    ProcessDrivesArea.Add(RectsMovemont(0x187 + 0x44 * num, 0x153 + 0x21 * num2, 52f, 26f));
                    num2++;
                }
            }
            for (num = 0; num < 6; num++)
            {
                num2 = 0;
                while (num2 < 2)
                {
                    ProcessDrivesArea.Add(RectsMovemont(0x185 + 0x44 * num, 430 + 0x21 * num2, 66f, 31f));
                    num2++;
                }
            }
            ProcessTebeArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            for (num = 0; num < 3; num++)
            {
                ProcessTebeArea.Add(RectsMovemont(0x53 + num * 0x37, 491f, 51f, 26f));
            }
            for (num = 0; num < 3; num++)
            {
                ProcessTebeArea.Add(RectsMovemont(0x10c + num * 0x37, 491f, 51f, 26f));
            }
            for (num = 0; num < 2; num++)
            {
                ProcessTebeArea.Add(RectsMovemont(0x287 + num * 0x37, 491f, 51f, 26f));
            }
            ProcessTebeArea.Add(RectsMovemont(647f, 466f, 106f, 25f));
            for (num = 0; num < 3; num++)
            {
                ProcessTebeArea.Add(RectsMovemont(438f, 170 + num * 0x55, 155f, 51f));
            }
            for (num = 0; num < 3; num++)
            {
                ProcessTebeArea.Add(RectsMovemont(0x53 + num * 0x37, 170f, 50f, 125.5f));
            }
            for (num = 0; num < 3; num++)
            {
                ProcessTebeArea.Add(RectsMovemont(0x10c + num * 0x37, 170f, 50f, 125.5f));
            }
            for (num = 0; num < 3; num++)
            {
                ProcessTebeArea.Add(RectsMovemont(0x53 + num * 0x37, 295.5f, 50f, 125.5f));
            }
            for (num = 0; num < 3; num++)
            {
                ProcessTebeArea.Add(RectsMovemont(0x10c + num * 0x37, 295.5f, 50f, 125.5f));
            }
            ProcessTebeArea.Add(RectsMovemont(647f, 170f, 11f, 125.5f));
            ProcessTebeArea.Add(RectsMovemont(658f, 170f, 39f, 125.5f));
            ProcessTebeArea.Add(RectsMovemont(647f, 295.5f, 11f, 125.5f));
            ProcessTebeArea.Add(RectsMovemont(658f, 295.5f, 39f, 125.5f));
            ProcessTebeArea.Add(RectsMovemont(702f, 170f, 11f, 125.5f));
            ProcessTebeArea.Add(RectsMovemont(713f, 170f, 39f, 125.5f));
            ProcessTebeArea.Add(RectsMovemont(702f, 295.5f, 11f, 125.5f));
            ProcessTebeArea.Add(RectsMovemont(713f, 295.5f, 39f, 125.5f));
            for (num = 0; num < 3; num++)
            {
                ProcessTebeArea.Add(RectsMovemont(0x53 + num * 0x37, 170f, 50f, 251f));
            }
            for (num = 0; num < 3; num++)
            {
                ProcessTebeArea.Add(RectsMovemont(0x10c + num * 0x37, 170f, 50f, 251f));
            }
            for (num = 0; num < 2; num++)
            {
                ProcessTebeArea.Add(RectsMovemont(0x287 + num * 0x37, 170f, 50f, 251f));
            }
            for (num = 0; num < 2; num++)
            {
                ProcessCbArea.Add(RectsMovemont(0x1a + num * 0x174, 146f, 363f, 333f));
            }
            for (num = 0; num < 2; num++)
            {
                num2 = 0;
                while (num2 < 6)
                {
                    ProcessCbArea.Add(RectsMovemont(0x25 + num * 0xb0, 0x9d + num2 * 0x2d, 165f, 39f));
                    num2++;
                }
            }
            for (num = 0; num < 6; num++)
            {
                ProcessCbArea.Add(RectsMovemont(409f, 0x9d + num * 0x2d, 165f, 39f));
            }
            for (num = 0; num < 3; num++)
            {
                ProcessCbArea.Add(RectsMovemont(585f, 0x9d + num * 0x2d, 165f, 39f));
            }
            ProcessCbArea.Add(RectsMovemont(585f, 337f, 165f, 39f));
            for (num = 0; num < 2; num++)
            {
                num2 = 0;
                while (num2 < 6)
                {
                    ProcessCbArea.Add(RectsMovemont(0x27 + num * 0xb0, 0x9f + num2 * 0x2d, 161f, 35f));
                    num2++;
                }
            }
            for (num = 0; num < 6; num++)
            {
                ProcessCbArea.Add(RectsMovemont(411f, 0x9f + num * 0x2d, 161f, 35f));
            }
            for (num = 0; num < 3; num++)
            {
                ProcessCbArea.Add(RectsMovemont(587f, 0x9f + num * 0x2d, 161f, 35f));
            }
            ProcessCbArea.Add(RectsMovemont(587f, 339f, 161f, 35f));
            ProcessAuxArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            ProcessAuxArea.Add(RectsMovemont(74f, 167f, 39f, 9f));
            ProcessAuxArea.Add(RectsMovemont(74f, 184f, 39f, 9f));
            ProcessAuxArea.Add(RectsMovemont(74f, 203f, 39f, 9f));
            ProcessAuxArea.Add(RectsMovemont(74f, 436f, 39f, 9f));
            ProcessAuxArea.Add(RectsMovemont(74f, 454f, 39f, 9f));
            ProcessAuxArea.Add(RectsMovemont(74f, 472f, 39f, 9f));
            ProcessAuxArea.Add(RectsMovemont(160f, 172f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(162f, 442f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(450f, 214f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(450f, 259f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(208f, 281f, 13f, 28f));
            ProcessAuxArea.Add(RectsMovemont(244f, 173f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(244f, 218f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(244f, 268f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(244f, 313f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(407f, 214f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(407f, 259f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(407f, 306f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(407f, 352f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(407f, 398f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(407f, 443f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(611f, 301f, 29f, 17f));
            ProcessAuxArea.Add(RectsMovemont(610f, 212f, 29f, 11f));
            ProcessAuxArea.Add(RectsMovemont(610f, 260f, 29f, 11f));
            ProcessBatteryArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            ProcessBatteryArea.Add(RectsMovemont(55f, 165f, 66f, 24f));
            ProcessBatteryArea.Add(RectsMovemont(299f, 167f, 66f, 24f));
            ProcessBatteryArea.Add(RectsMovemont(490f, 167f, 66f, 24f));
            ProcessBatteryArea.Add(RectsMovemont(23f, 207f, 66f, 24f));
            ProcessBatteryArea.Add(RectsMovemont(687f, 199f, 66f, 24f));
            ProcessBatteryArea.Add(RectsMovemont(56f, 369f, 67f, 24f));
            ProcessBatteryArea.Add(RectsMovemont(298f, 380f, 67f, 24f));
            ProcessBatteryArea.Add(RectsMovemont(24f, 410f, 66f, 24f));
            ProcessBatteryArea.Add(RectsMovemont(686f, 408f, 66f, 24f));
            ProcessBatteryArea.Add(RectsMovemont(154f, 166f, 124f, 101f));
            ProcessBatteryArea.Add(RectsMovemont(155f, 368f, 124f, 101f));
            ProcessBatteryArea.Add(RectsMovemont(184f, 248f, 60f, 17f));
            ProcessBatteryArea.Add(RectsMovemont(184f, 449f, 60f, 17f));
            ProcessBatteryArea.Add(RectsMovemont(455f, 293f, 23f, 47f));
            ProcessBatteryArea.Add(RectsMovemont(501f, 137f, 41f, 28f));
            ProcessNetControlArea.Add(RectsMovemont(13f, 139f, 136f, 91f));
            ProcessNetControlArea.Add(RectsMovemont(223f, 139f, 139f, 91f));
            ProcessNetControlArea.Add(RectsMovemont(431f, 169f, 61f, 36f));
            ProcessNetControlArea.Add(RectsMovemont(13f, 264f, 773f, 101f));
            ProcessNetControlArea.Add(RectsMovemont(13f, 399f, 201f, 91f));
            ProcessNetControlArea.Add(RectsMovemont(218f, 399f, 136f, 91f));
            ProcessNetControlArea.Add(RectsMovemont(359f, 399f, 201f, 91f));
            ProcessNetControlArea.Add(RectsMovemont(564f, 399f, 136f, 91f));
            ProcessNetControlArea.Add(RectsMovemont(712f, 424f, 61f, 36f));
            ProcessNetControlArea.Add(RectsMovemont(16f, 168f, 63f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(81f, 168f, 63f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(227f, 168f, 63f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(292f, 168f, 63f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(430f, 168f, 63f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(261f, 302f, 134f, 53f));
            ProcessNetControlArea.Add(RectsMovemont(397f, 302f, 134f, 53f));
            for (num = 0; num < 3; num++)
            {
                ProcessNetControlArea.Add(RectsMovemont(0x10 + 0x41 * num, 423f, 63f, 38f));
            }
            ProcessNetControlArea.Add(RectsMovemont(222f, 423f, 63f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(287f, 423f, 63f, 38f));
            for (num = 0; num < 3; num++)
            {
                ProcessNetControlArea.Add(RectsMovemont(0x16a + 0x41 * num, 423f, 63f, 38f));
            }
            ProcessNetControlArea.Add(RectsMovemont(568f, 423f, 63f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(633f, 423f, 63f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(711f, 423f, 63f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(43f, 206f, 1f, 39f));
            ProcessNetControlArea.Add(RectsMovemont(43f, 244f, 33f, 1f));
            ProcessNetControlArea.Add(RectsMovemont(75f, 244f, 1f, 19f));
            ProcessNetControlArea.Add(RectsMovemont(108f, 206f, 1f, 39f));
            ProcessNetControlArea.Add(RectsMovemont(76f, 244f, 33f, 1f));
            ProcessNetControlArea.Add(RectsMovemont(253f, 206f, 1f, 39f));
            ProcessNetControlArea.Add(RectsMovemont(253f, 244f, 34f, 1f));
            ProcessNetControlArea.Add(RectsMovemont(286f, 245f, 1f, 19f));
            ProcessNetControlArea.Add(RectsMovemont(319f, 206f, 1f, 39f));
            ProcessNetControlArea.Add(RectsMovemont(287f, 244f, 33f, 1f));
            ProcessNetControlArea.Add(RectsMovemont(459f, 206f, 1f, 57f));
            ProcessNetControlArea.Add(RectsMovemont(113f, 364f, 1f, 21f));
            ProcessNetControlArea.Add(RectsMovemont(48f, 384f, 65f, 1f));
            ProcessNetControlArea.Add(RectsMovemont(48f, 385f, 1f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(113f, 385f, 1f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(114f, 384f, 65f, 1f));
            ProcessNetControlArea.Add(RectsMovemont(178f, 385f, 1f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(286f, 364f, 1f, 21f));
            ProcessNetControlArea.Add(RectsMovemont(253f, 384f, 33f, 1f));
            ProcessNetControlArea.Add(RectsMovemont(253f, 385f, 1f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(287f, 384f, 33f, 1f));
            ProcessNetControlArea.Add(RectsMovemont(319f, 385f, 1f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(459f, 364f, 1f, 21f));
            ProcessNetControlArea.Add(RectsMovemont(393f, 384f, 66f, 1f));
            ProcessNetControlArea.Add(RectsMovemont(393f, 385f, 1f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(459f, 385f, 1f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(460f, 384f, 65f, 1f));
            ProcessNetControlArea.Add(RectsMovemont(524f, 385f, 1f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(627f, 364f, 1f, 21f));
            ProcessNetControlArea.Add(RectsMovemont(594f, 384f, 33f, 1f));
            ProcessNetControlArea.Add(RectsMovemont(594f, 385f, 1f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(628f, 384f, 33f, 1f));
            ProcessNetControlArea.Add(RectsMovemont(660f, 385f, 1f, 38f));
            ProcessNetControlArea.Add(RectsMovemont(740f, 364f, 1f, 59f));
            for (num = 0; num < 3; num++)
            {
                ProcessNetControlArea.Add(RectsMovemont(490 + 0x68 * num, 504f, 102f, 38f));
            }
            for (num = 0; num < 3; num++)
            {
                ProcessNetControlArea.Add(RectsMovemont(0x1ec + 0x68 * num, 506f, 98f, 34f));
            }
            ProcessNetControlArea.Add(RectsMovemont(18f, 170f, 59f, 34f));
            ProcessNetControlArea.Add(RectsMovemont(83f, 170f, 59f, 34f));
            ProcessNetControlArea.Add(RectsMovemont(229f, 170f, 59f, 34f));
            ProcessNetControlArea.Add(RectsMovemont(294f, 170f, 59f, 34f));
            ProcessNetControlArea.Add(RectsMovemont(432f, 170f, 59f, 34f));
            ProcessNetControlArea.Add(RectsMovemont(263f, 304f, 130f, 49f));
            ProcessNetControlArea.Add(RectsMovemont(399f, 304f, 130f, 49f));
            for (num = 0; num < 3; num++)
            {
                ProcessNetControlArea.Add(RectsMovemont(0x12 + 0x41 * num, 425f, 59f, 34f));
            }
            ProcessNetControlArea.Add(RectsMovemont(224f, 425f, 59f, 34f));
            ProcessNetControlArea.Add(RectsMovemont(289f, 425f, 59f, 34f));
            for (num = 0; num < 3; num++)
            {
                ProcessNetControlArea.Add(RectsMovemont(0x16c + 0x41 * num, 425f, 59f, 34f));
            }
            ProcessNetControlArea.Add(RectsMovemont(570f, 425f, 59f, 34f));
            ProcessNetControlArea.Add(RectsMovemont(635f, 425f, 59f, 34f));
            ProcessNetControlArea.Add(RectsMovemont(713f, 425f, 59f, 34f));
            for (num = 0; num < 3; num++)
            {
                num2 = num;
                while (num2 < 9)
                {
                    ProcessCountersArea.Add(RectsMovemont(0x9d + 0xcf * num, 0xa7 + 0x24 * num2, 109f, 25f));
                    num2++;
                }
            }
            for (num = 0; num < 3; num++)
            {
                num2 = num;
                while (num2 < 9)
                {
                    ProcessCountersArea.Add(RectsMovemont(0x47 + 0xcf * num, 0xa7 + 0x24 * num2, 72f, 25f));
                    num2++;
                }
            }
            ProcessCountersArea.Add(RectsMovemont(701f, 506f, 99f, 39f));
            ProcessCountersArea.Add(RectsMovemont(703f, 508f, 95f, 35f));
            for (num = 0; num < 3; num++)
            {
                DataInOthersArea.Add(RectsMovemont(168f, 0x103 + 0x53 * num, 143f, 22f));
            }
            for (num = 0; num < 2; num++)
            {
                DataInOthersArea.Add(RectsMovemont(0x16d + 0x7f * num, 139f, 126f, 50f));
            }
            for (num = 0; num < 3; num++)
            {
                DataInOthersArea.Add(RectsMovemont(0x16d + 0x7f * num, 327f, 126f, 50f));
            }
            for (num = 0; num < 2; num++)
            {
                DataInOthersArea.Add(RectsMovemont(0x16d + 0x7f * num, 411f, 126f, 50f));
            }
            for (num = 0; num < 2; num++)
            {
                DataInOthersArea.Add(RectsMovemont(0x16f + 0x7f * num, 141f, 122f, 46f));
            }
            for (num = 0; num < 3; num++)
            {
                DataInOthersArea.Add(RectsMovemont(0x16f + 0x7f * num, 329f, 122f, 46f));
            }
            for (num = 0; num < 2; num++)
            {
                DataInOthersArea.Add(RectsMovemont(0x16f + 0x7f * num, 413f, 122f, 46f));
            }
            for (num = 0; num < 2; num++)
            {
                num2 = 0;
                while (num2 < 2)
                {
                    DataInOthersArea.Add(RectsMovemont(0x16d + 0x7f * num2, 0xbd + num * 0x35, 122f, 50f));
                    num2++;
                }
            }
            for (num = 0; num < 2; num++)
            {
                num2 = 0;
                while (num2 < 2)
                {
                    DataInOthersArea.Add(RectsMovemont(0x16f + 0x7f * num2, 0xbd + num * 0x31, 122f, 46f));
                    num2++;
                }
            }
            for (num = 0; num < 2; num++)
            {
                num2 = 0;
                while (num2 < 3)
                {
                    MainControlTestArea.Add(RectsMovemont(0xa2 + 220 * num, 0xb9 + 0x1f * num2, 220f, 31f));
                    num2++;
                }
            }
            MainControlTestArea.Add(RectsMovemont(87f, 430f, 562f, 113f));
            MainControlTestArea.Add(RectsMovemont(691f, 504f, 101f, 39f));
            MainControlTestArea.Add(RectsMovemont(693f, 506f, 97f, 35f));
            for (num = 0; num < 3; num++)
            {
                num2 = 0;
                while (num2 < 7)
                {
                    StartTestArea.Add(RectsMovemont(0x56 + 0xc6 * num, 0x9f + 0x1c * num2, 198f, 28f));
                    num2++;
                }
            }
            StartTestArea.Add(RectsMovemont(87f, 430f, 562f, 113f));
            StartTestArea.Add(RectsMovemont(691f, 504f, 101f, 39f));
            StartTestArea.Add(RectsMovemont(693f, 506f, 97f, 35f));
            for (num = 0; num < 2; num++)
            {
                num2 = 0;
                while (num2 < 7)
                {
                    ZeroOrderTestArea.Add(RectsMovemont(0xa2 + 220 * num, 0xb9 + 0x1f * num2, 220f, 31f));
                    num2++;
                }
            }
            ZeroOrderTestArea.Add(RectsMovemont(87f, 430f, 562f, 113f));
            ZeroOrderTestArea.Add(RectsMovemont(691f, 504f, 101f, 39f));
            ZeroOrderTestArea.Add(RectsMovemont(693f, 506f, 97f, 35f));
            for (num = 0; num < 5; num++)
            {
                for (num2 = 0; num2 < 3; num2++)
                {
                    AuxPowerTestArea.Add(RectsMovemont(0x1b + 150 * num, 0xb9 + 0x1f * num2, 150f, 31f));
                }
            }
            AuxPowerTestArea.Add(RectsMovemont(87f, 430f, 562f, 113f));
            AuxPowerTestArea.Add(RectsMovemont(691f, 504f, 101f, 39f));
            AuxPowerTestArea.Add(RectsMovemont(693f, 506f, 97f, 35f));
            LampTestArea.Add(RectsMovemont(87f, 430f, 562f, 113f));
            LampTestArea.Add(RectsMovemont(691f, 504f, 101f, 39f));
            LampTestArea.Add(RectsMovemont(693f, 506f, 97f, 35f));
            MaintenanceLubrificationValveTestArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            for (num = 0; num < 2; num++)
            {
                MaintenanceLubrificationValveTestArea.Add(RectsMovemont(0xb5 + num * 0x1a1, 201f, 21f, 19f));
            }
            MaintenanceLubrificationValveTestArea.Add(RectsMovemont(87f, 430f, 562f, 113f));
            for (num = 0; num < 2; num++)
            {
                MaintenanceLubrificationValveTestArea.Add(RectsMovemont(662f, 430 + 0x39 * num, 122f, 57f));
            }
            for (num = 0; num < 2; num++)
            {
                MaintenanceLubrificationValveTestArea.Add(RectsMovemont(664f, 0x1b0 + 0x39 * num, 118f, 53f));
            }
            FaultFiltrationArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            FaultFiltrationArea.Add(RectsMovemont(3f, 507f, 132f, 43f));
            FaultFiltrationArea.Add(RectsMovemont(145f, 507f, 100f, 44f));
            for (num = 0; num < 3; num++)
            {
                FaultFiltrationArea.Add(RectsMovemont(0x1e8 + 0x6a * num, 508f, 100f, 44f));
            }
            for (num = 0; num < 15; num++)
            {
                FaultFiltrationArea.Add(RectsMovemont(3f, 0x91 + num * 0x18, 29f, 23f));
                FaultFiltrationArea.Add(RectsMovemont(33f, 0x91 + num * 0x18, 29f, 23f));
                FaultFiltrationArea.Add(RectsMovemont(65f, 0x91 + num * 0x18, 139f, 23f));
                FaultFiltrationArea.Add(RectsMovemont(205f, 0x91 + num * 0x18, 139f, 23f));
                FaultFiltrationArea.Add(RectsMovemont(345f, 0x91 + num * 0x18, 99f, 23f));
                FaultFiltrationArea.Add(RectsMovemont(445f, 0x91 + num * 0x18, 39f, 23f));
                FaultFiltrationArea.Add(RectsMovemont(485f, 0x91 + num * 0x18, 313f, 23f));
            }
            for (num = 0; num < 15; num++)
            {
                FaultFiltrationArea.Add(RectsMovemont(3f, 0x91 + num * 0x18, 795f, 23f));
            }
            FaultEnsureArea.Add(RectsMovemont(0f, 0f, 800f, 600f));
            FaultEnsureArea.Add(RectsMovemont(600f, 176f, 162f, 46f));
            FaultEnsureArea.Add(RectsMovemont(30f, 180f, 500f, 30f));
            FaultEnsureArea.Add(RectsMovemont(30f, 220f, 300f, 300f));
            FaultFiltrationArea.Add(RectsMovemont(382f, 508f, 100f, 44f));
            FaultFiltrationArea.Add(RectsMovemont(232f, 508f, 100f, 44f));
        }

        public static bool RectangleFLists(ViewIDName className, ref List<RectangleF> rectanglesList)
        {
            switch (className)
            {
                case ViewIDName.Main:
                    rectanglesList = MainArea;
                    return true;

                case ViewIDName.BottomTitleScreen:
                    rectanglesList = TitleArea;
                    return true;

                case ViewIDName.TopTitleScreen:
                    rectanglesList = TitleScreenArea;
                    return true;

                case ViewIDName.MainScreen:
                    rectanglesList = MainScreenArea;
                    return true;

                case ViewIDName.TrainOverview:
                    rectanglesList = TrainOverviewArea;
                    return true;

                case ViewIDName.ConverterCutOff:
                    rectanglesList = CutOffArea;
                    return true;

                case ViewIDName.ControlPantoSelection:
                    rectanglesList = ControlPantoSelectionArea;
                    return true;

                case ViewIDName.DistanceCounters:
                    rectanglesList = DistanceCountersArea;
                    return true;

                case ViewIDName.BrakeSetting:
                    rectanglesList = BrakeSettingArea;
                    return true;

                case ViewIDName.CutOffState:
                    rectanglesList = CutOffStateArea;
                    return true;

                case ViewIDName.TrainPower:
                    rectanglesList = TrainPowerArea;
                    return true;

                case ViewIDName.ProcessDrives:
                    rectanglesList = ProcessDrivesArea;
                    return true;

                case ViewIDName.ProcessTebe:
                    rectanglesList = ProcessTebeArea;
                    return true;

                case ViewIDName.ProcessCb:
                    rectanglesList = ProcessCbArea;
                    return true;

                case ViewIDName.ProcessAux:
                    rectanglesList = ProcessAuxArea;
                    return true;

                case ViewIDName.ProcessBattery:
                    rectanglesList = ProcessBatteryArea;
                    return true;

                case ViewIDName.ProcessNetControl:
                    rectanglesList = ProcessNetControlArea;
                    return true;

                case ViewIDName.ProcessCounters:
                    rectanglesList = ProcessCountersArea;
                    return true;

                case ViewIDName.DataInOthers:
                    rectanglesList = DataInOthersArea;
                    return true;

                case ViewIDName.MainControlTest:
                    rectanglesList = MainControlTestArea;
                    return true;

                case ViewIDName.StartTest:
                    rectanglesList = StartTestArea;
                    return true;

                case ViewIDName.ZeroOrderTest:
                    rectanglesList = ZeroOrderTestArea;
                    return true;

                case ViewIDName.AuxPowerTest:
                    rectanglesList = AuxPowerTestArea;
                    return true;

                case ViewIDName.LampTest:
                case ViewIDName.AlertToTest:
                    rectanglesList = LampTestArea;
                    return true;

                case ViewIDName.MaintenanceLubrificationValveTest:
                    rectanglesList = MaintenanceLubrificationValveTestArea;
                    return true;

                case ViewIDName.FaultEnsure:
                    rectanglesList = FaultEnsureArea;
                    return true;

                case ViewIDName.FaultFiltration:
                case ViewIDName.FaultUnfiltration:
                    rectanglesList = FaultFiltrationArea;
                    return true;
            }

            return false;
        }

        private static RectangleF RectsMovemont(float x, float y, float width, float hight)
        {
            return new RectangleF((x + m_OffsetCoodinatesX) * m_Scaling, (y + m_OffsetCoodinatesY) * m_Scaling, width * m_Scaling, hight * m_Scaling);
        }

        public static RectangleF TransformCoord(RectangleF rectangle)
        {
            return RectsMovemont(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }

        public static int OffsetCoodinatesX
        {
            get
            {
                return m_OffsetCoodinatesX;
            }
        }

        public static int OffsetCoodinatesY
        {
            get
            {
                return m_OffsetCoodinatesY;
            }
        }

        public static float Scaling
        {
            get
            {
                return m_Scaling;
            }
        }
    }
}