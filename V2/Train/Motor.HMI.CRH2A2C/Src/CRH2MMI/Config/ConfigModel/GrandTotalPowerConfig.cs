using System.Collections.Generic;
using System.Xml.Serialization;
using CommonUtil.Util;

namespace CRH2MMI.Config.ConfigModel
{
    public class GrandTotalPowerConfig
    {
        [XmlAttribute]
        public int RowNum { get; set; }
        public int[] Widths { get; set; }
        public List<GrandTotalPowerCar> GrandTotalPowerCars { get; set; }

        static void Test()
        {
            var a = new GrandTotalPowerConfig();
            a.RowNum = 14;
            a.Widths = new[] { 10, 20, 30, 40, 50, 60 };
            a.GrandTotalPowerCars = new List<GrandTotalPowerCar>()
            {
                new GrandTotalPowerCar()
                {
                    CarNum = "2车厢",
                    Row = 0,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                },
                 new GrandTotalPowerCar()
                {
                    CarNum = "3车厢",
                    Row = 1,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                }
                ,
                 new GrandTotalPowerCar()
                {
                    CarNum = "4车厢",
                    Row = 2,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                }
                ,
                 new GrandTotalPowerCar()
                {
                    CarNum = "5车厢",
                    Row = 3,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                } ,
                 new GrandTotalPowerCar()
                {
                    CarNum = "6车厢",
                    Row = 4,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                } ,
                 new GrandTotalPowerCar()
                {
                    CarNum = "7车厢",
                    Row = 5,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                } ,
                 new GrandTotalPowerCar()
                {
                    CarNum = "8车厢",
                    Row = 6,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                } ,
                 new GrandTotalPowerCar()
                {
                    CarNum = "9车厢",
                    Row = 7,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                } ,
                 new GrandTotalPowerCar()
                {
                    CarNum = "10车厢",
                    Row = 8,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                } ,
                 new GrandTotalPowerCar()
                {
                    CarNum = "11车厢",
                    Row = 9,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                } ,
                 new GrandTotalPowerCar()
                {
                    CarNum = "12车厢",
                    Row = 10,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                } ,
                 new GrandTotalPowerCar()
                {
                    CarNum = "13车厢",
                    Row = 11,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                } ,
                 new GrandTotalPowerCar()
                {
                    CarNum = "14车厢",
                    Row = 12,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                } ,
                 new GrandTotalPowerCar()
                {
                    CarNum = "15车厢",
                    Row = 13,
                    CarDetails = new List<GrandTotalPowerCarDetail>()
                    {
                        new GrandTotalPowerCarDetail()
                        {
                            TotlaStartDataTime = "2012/10/ 9",
                            TotlaStartTime = "21",
                            TotalDistance = "941253",
                            TowElectricity = "2129092",
                            RiviveElectricity = "386874",
                            ConsumeElectricity = "1742218"
                        }
                    }
                }
            };
            DataSerialization.SerializeToXmlFile(a, @"d:\a.xml");
        }
    }

    public class GrandTotalPowerCar
    {
        [XmlAttribute]
        public string CarNum { get; set; }
        [XmlAttribute]
        public int Row { get; set; }
        [XmlElement]
        public List<GrandTotalPowerCarDetail> CarDetails { get; set; }
    }

    public class GrandTotalPowerCarDetail
    {
        [XmlAttribute]
        public string TotlaStartDataTime { get; set; }
        [XmlAttribute]
        public string TotlaStartTime { get; set; }
        [XmlAttribute]
        public string TotalDistance { get; set; }
        [XmlAttribute]
        public string TowElectricity { get; set; }
        [XmlAttribute]
        public string RiviveElectricity { get; set; }
        [XmlAttribute]
        public string ConsumeElectricity { get; set; }
    }
}