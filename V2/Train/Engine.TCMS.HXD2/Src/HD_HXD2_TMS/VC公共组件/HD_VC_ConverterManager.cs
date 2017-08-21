using Excel.Interface;
using HD_HXD2_TMS.Common;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;

namespace HD_HXD2_TMS.VC公共组件
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class HD_VC_ConverterManager : baseClass
    {
        public static List<FalutInfo> FalutsAHistroy = null;
        public static List<FalutInfo> FalutsBHistroy = null;
        public static List<FalutInfo> FalutsACurrent = null;
        public static List<FalutInfo> FalutsBCurrent = null;
        public static FalutInfo CurrentFalut = null;

        private List<FalutInfo> _faluts = null;

        public override string GetInfo()
        {
            return "公共试图-标题信息";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            FalutsAHistroy = new List<FalutInfo>();
            FalutsBHistroy = new List<FalutInfo>();
            FalutsACurrent = new List<FalutInfo>();
            FalutsBCurrent = new List<FalutInfo>();
            _faluts = new List<FalutInfo>();

            ExcelAdapter ea = new ExcelAdapter();
            ExcelReaderConfig erc = new ExcelReaderConfig()
            {
                File = Path.Combine(AppConfig.AppPaths.ConfigDirectory + @"\HXD2车辆屏主断查询接口表.xls"),
                SheetNames = new List<string>() { "Sheet1" },
                Coloumns = new List<ColoumnConfig>()
                {
                    new ColoumnConfig() { Name = "InBoolIndex" }, 
                    new ColoumnConfig() { Name = "车头编号" },
                    new ColoumnConfig() { Name = "故障代码" },
                    new ColoumnConfig() { Name = "故障内容" }
                }
            };
            DataSet ds = ea.Adapter(erc);
            DataRowCollection drc = ds.Tables["Sheet1"].Rows;
            foreach (DataRow item in drc)
            {
                Int32 a = Convert.ToInt32(item[0]);
                String b = (String) item[1];
                String c = (String)item[2];
                String d = (String)item[3];
                _faluts.Add(
                    new FalutInfo(
                        Convert.ToInt32(item[0]),
                        (String)item[1],
                        0,
                        (String)item[2],
                        (String)item[3],
                        "A",
                        "",
                        ""
                        )
                    );
            }

            return true;
        }

        public override void paint(Graphics g)
        {
            foreach (var falutInfo in _faluts)
            {
                bool value = BoolList[falutInfo.LogicID];
                if (falutInfo.GetValue(value))
                {
                    if (value) //触发故障
                    {
                        String time = DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "-" + DateTime.Now.ToLongTimeString();
                        falutInfo.StartTime = time;

                        FalutInfo fi = new FalutInfo(falutInfo);
                        switch (fi.TrainName)
                        {
                            case "A":
                                FalutsAHistroy.Add(fi);
                                FalutsACurrent.Insert(0,fi);
                                break;
                            case "B":
                                FalutsBHistroy.Add(fi);
                                FalutsBCurrent.Insert(0,fi);
                                break;
                        }

                        //获取到当前故障
                         CurrentFalut = fi;
                    }
                    else //解除故障
                    {
                        FalutInfo fi = FalutsACurrent.Find(a => a.LogicID == falutInfo.LogicID);
                        if (fi != null) //A车故障
                        {
                            FalutsACurrent.Remove(fi);
                            if (CurrentFalut == fi)
                            {
                                CurrentFalut = null;
                                if (FalutsACurrent.Count != 0)
                                {
                                    CurrentFalut = FalutsACurrent[0];
                                }
                                else
                                {
                                    if (FalutsBCurrent.Count != 0)
                                    {
                                        CurrentFalut = FalutsBCurrent[0];
                                    }
                                }
                            }
                        }

                        fi = FalutsBCurrent.Find(a => a.LogicID == falutInfo.LogicID);
                        if (fi != null)
                        {
                            FalutsBCurrent.Remove(fi);
                            if (CurrentFalut == fi)
                            {
                                CurrentFalut = null;
                                if (FalutsBCurrent.Count != 0)
                                {
                                    CurrentFalut = FalutsBCurrent[0];
                                }
                                else
                                {
                                    if (FalutsACurrent.Count != 0)
                                    {
                                        CurrentFalut = FalutsACurrent[0];
                                    }
                                }
                            }
                        }
                    }
                }
            }

            base.paint(g);
        }
    }
}
