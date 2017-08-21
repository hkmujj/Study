using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using CommonUtil.Util;

namespace Subway.ATC.Casco.Config
{
    public class DiagConfig
    {

        public List<DiagProperty> AllDiagConfig { get; set; }
        public List<SpeedPointerProperty> AllSpeedPointerConfig { get; set; }

        private static void Test()
        {
            var tmpPoint = new PointF(310f, 240f);
            var tmp1 = new PointF(160f, 90f);
            var tmp2 = new PointF(125f, 55f);
            var a = new DiagConfig();
            a.AllDiagConfig = new List<DiagProperty>()
            {
                new DiagProperty()
                {
                    Name = "Diag",
                    CenterPoint = tmpPoint,
                    GraduatValue =  new[] { "0", "20", "40", "60", "80", "100" },
                    AllLineNum = 21,
                    GraduatAngle = 15,
                    StartAngle = -240,
                    Radiud = 160,
                    LongGraduat = 30,
                    ShortGraduat = 15,
                    LongShortGraduatMultiple = 5
                },new DiagProperty()
                  {
                    Name = "dialRM",
                    CenterPoint = tmpPoint,
                    GraduatValue =  new [] { "0", "25" },
                    AllLineNum = 6,
                    GraduatAngle = 15,
                    StartAngle = -240,
                    Radiud = 160,
                    LongGraduat = 30,
                    ShortGraduat = 15,
                    LongShortGraduatMultiple = 5
                }
                ,new DiagProperty()
                  {
                    Name = "dialTX",
                    CenterPoint = tmpPoint,
                    GraduatValue =  new [] { "0", "5" },
                    AllLineNum = 2,
                    GraduatAngle = 15,
                    StartAngle = -240,
                    Radiud = 160,
                    LongGraduat = 30,
                    ShortGraduat = 15,
                    LongShortGraduatMultiple = 1
                }
                 ,new DiagProperty()
                  {
                    Name = "m_WmDial",
                    CenterPoint = tmpPoint,
                    GraduatValue =  new [] { "0", "3" },
                    AllLineNum = 2,
                    GraduatAngle = 15,
                    StartAngle = -240,
                    Radiud = 160,
                    LongGraduat = 30,
                    ShortGraduat = 15,
                    LongShortGraduatMultiple = 1
                }
            }; a.AllSpeedPointerConfig = new List<SpeedPointerProperty>()
            {
                new SpeedPointerProperty()
                {
                    Name = "SpeedPointer",
                    MaxAngle = 300,
                    InitAngle = -60,
                    MaxVlaue = 100,
                    ApexPoint = tmp1,
                    CenterPoint = tmpPoint
                },
                 new SpeedPointerProperty()
                {
                    Name = "speedPointerRM",
                    MaxAngle = 62.5f,
                    InitAngle = -60,
                    MaxVlaue = 25,
                    ApexPoint = tmp1,
                    CenterPoint = tmpPoint
                },
                 new SpeedPointerProperty()
                {
                    Name = "speedPointerTX",
                    MaxAngle = 12.5f,
                    InitAngle = -60,
                    MaxVlaue = 5,
                    ApexPoint = tmp1,
                    CenterPoint = tmpPoint
                },
                 new SpeedPointerProperty()
                {
                    Name = "m_SpeedPointerWM",
                    MaxAngle = 12.5f,
                    InitAngle = -60,
                    MaxVlaue = 3,
                    ApexPoint = tmp1,
                    CenterPoint = tmpPoint
                },
                  new SpeedPointerProperty()
                {
                    Name = "m_SpeedPointerWM",
                    MaxAngle = 299,
                    InitAngle = -61,
                    MaxVlaue =100,
                    ApexPoint = tmp2,
                    CenterPoint = tmpPoint
                },
                  new SpeedPointerProperty()
                {
                    Name = "m_SpeedPointerWM",
                    MaxAngle = 299,
                    InitAngle = -61,
                    MaxVlaue = 100,
                    ApexPoint = tmp2,
                    CenterPoint = tmpPoint
                },
            };
            DataSerialization.SerializeToXmlFile(a, @"d:\a.xml");
        }
    }

    public class DiagProperty
    {
        /// <summary>
        /// 表盘名字
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }
        /// <summary>
        /// 中心坐标
        /// </summary>
        public PointF CenterPoint { get; set; }
        /// <summary>
        /// 刻度值
        /// </summary>
        public string[] GraduatValue { get; set; }
        /// <summary>
        /// 所有线条数
        /// </summary>
        public int AllLineNum { get; set; }
        /// <summary>
        /// 每一刻度所占角度
        /// </summary>
        public double GraduatAngle { get; set; }
        /// <summary>
        /// 开始角度
        /// </summary>
        public double StartAngle { get; set; }
        /// <summary>
        /// 半径
        /// </summary>
        public int Radiud { get; set; }
        /// <summary>
        /// 长刻度
        /// </summary>
        public int LongGraduat { get; set; }
        /// <summary>
        /// 短刻度
        /// </summary>
        public int ShortGraduat { get; set; }
        /// <summary>
        /// 长短刻度比
        /// </summary>
        public int LongShortGraduatMultiple { get; set; }
    }

    public class SpeedPointerProperty
    {
        /// <summary>
        /// 指针名字
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }
        /// <summary>
        /// 最大角度
        /// </summary>
        public float MaxAngle { get; set; }
        /// <summary>
        /// 初始角度
        /// </summary>
        public float InitAngle { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxVlaue { get; set; }
        /// <summary>
        /// Apex点
        /// </summary>
        public PointF ApexPoint { get; set; }
        /// <summary>
        /// 中心点
        /// </summary>
        public PointF CenterPoint { get; set; }

    }
}