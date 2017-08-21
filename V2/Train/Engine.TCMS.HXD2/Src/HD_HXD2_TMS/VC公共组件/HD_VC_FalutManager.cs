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
    public class HD_VC_FalutManager : baseClass
    {
        public static List<FalutInfo> FalutsAHistroy = null;
        public static List<FalutInfo> FalutsBHistroy = null;
        public static List<FalutInfo> FalutsACurrent = null;
        public static List<FalutInfo> FalutsBCurrent = null;
        public static FalutInfo CurrentFalut = null;

        public static Boolean IsShowCurrentView { get; set; }
        public static Boolean IsShowCurrentAView { get; set; }
        public static Boolean IsShowCurrentBView { get; set; }

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
                File = Path.Combine(AppConfig.AppPaths.ConfigDirectory + @"\HXD2车辆屏故障接口表.xls"),
                SheetNames = new List<string>() { "Sheet1" },
                Coloumns = new List<ColoumnConfig>()
                {
                    new ColoumnConfig() { Name = "InBoolIndex" }, 
                    new ColoumnConfig() { Name = "车头编号" },
                    new ColoumnConfig() { Name = "机车编号" }, 
                    new ColoumnConfig() { Name = "代码" },
                    new ColoumnConfig() { Name = "名称" }, 
                    new ColoumnConfig() { Name = "分级" },
                    new ColoumnConfig() { Name = "故障点" }, 
                    new ColoumnConfig() { Name = "故障提示" }
                }
            };
            DataSet ds = ea.Adapter(erc);
            DataRowCollection drc = ds.Tables["Sheet1"].Rows;
            foreach (DataRow item in drc)
            {
                _faluts.Add(
                    new FalutInfo(
                        Convert.ToInt32(item[0]), 
                        (String)item[1], 
                        Convert.ToInt32(item[2]),
                        (String)item[3],
                        (String)item[4],
                        (String)item[5],
                        (String)item[6],
                        (String)item[7]
                        )
                    );
            }

            IsShowCurrentView = false;
            IsShowCurrentAView = false;
            IsShowCurrentBView = false;

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
                        falutInfo.Speed = FloatList[UIObj.InFloatList[0]];
                        falutInfo.Voltage = FloatList[UIObj.InFloatList[1]];
                        falutInfo.Electric = FloatList[UIObj.InFloatList[2]];
                        falutInfo.Level = FloatList[UIObj.InFloatList[3]];
                        if (BoolList[UIObj.InBoolList[0]])
                            falutInfo.Direction = Direction.向前;
                        else if (BoolList[UIObj.InBoolList[1]])
                            falutInfo.Direction = Direction.向后;
                        else falutInfo.Direction = Direction.零位;

                        FalutInfo fi = new FalutInfo(falutInfo);
                        switch (fi.TrainName)
                        {
                            case "A":
                                FalutsAHistroy.Add(fi);
                                FalutsACurrent.Add(fi);
                                break;
                            case "B":
                                FalutsBHistroy.Add(fi);
                                FalutsBCurrent.Add(fi);
                                break;
                        }

                        //获取到当前故障（最高等级的故障）
                        if (CurrentFalut == null) CurrentFalut = fi;
                        else if (((Int32)CurrentFalut.Grade) >= ((Int32)fi.Grade)) CurrentFalut = fi;
                    }
                    else //解除故障
                    {
                        FalutInfo fi = FalutsACurrent.Find(a => a.LogicID == falutInfo.LogicID);
                        if (fi != null) //A车故障
                        {
                            FalutsACurrent.Remove(fi);
                            CurrentFalut = null;
                            if (CurrentFalut == fi)
                            {
                                if (FalutsACurrent.Count != 0)
                                {
                                    CurrentFalut = FalutsACurrent[0];
                                    for (int i = 0; i < FalutsACurrent.Count; i++)
                                    {
                                        if (((Int32) CurrentFalut.Grade) >= ((Int32) FalutsACurrent[i].Grade))
                                            CurrentFalut = FalutsACurrent[i];
                                    }
                                }
                                else
                                {
                                    if (FalutsBCurrent.Count != 0)
                                    {
                                        CurrentFalut = FalutsBCurrent[0];
                                        for (int i = 0; i < FalutsBCurrent.Count; i++)
                                        {
                                            if (((Int32) CurrentFalut.Grade) >= ((Int32) FalutsBCurrent[i].Grade))
                                                CurrentFalut = FalutsBCurrent[i];
                                        }
                                    }
                                }
                            }
                            else
                            {
                                FalutInfo fOK = null;
                                if (HD_VC_FalutManager.FalutsACurrent.Count != 0)
                                {
                                    foreach (var i in HD_VC_FalutManager.FalutsACurrent)
                                    {
                                        if (!i.IsOK)
                                        {
                                            if (HD_VC_FalutManager.CurrentFalut == null)
                                                HD_VC_FalutManager.CurrentFalut = i;
                                            else if (((Int32)HD_VC_FalutManager.CurrentFalut.Grade) < ((Int32)i.Grade))
                                            {
                                                HD_VC_FalutManager.CurrentFalut = i;
                                            }
                                        }
                                        else
                                        {
                                            if (fOK == null)
                                                fOK = i;
                                            else if (((Int32)fOK.Grade) < ((Int32)i.Grade))
                                            {
                                                fOK = i;
                                            }
                                        }
                                    }
                                }
                                if (HD_VC_FalutManager.FalutsBCurrent.Count != 0)
                                {
                                    foreach (var i in HD_VC_FalutManager.FalutsBCurrent)
                                    {
                                        if (!i.IsOK)
                                        {
                                            if (HD_VC_FalutManager.CurrentFalut == null)
                                                HD_VC_FalutManager.CurrentFalut = i;
                                            else if (((Int32)HD_VC_FalutManager.CurrentFalut.Grade) < ((Int32)i.Grade))
                                            {
                                                HD_VC_FalutManager.CurrentFalut = i;
                                            }
                                        }
                                        else
                                        {
                                            if (fOK == null)
                                                fOK = i;
                                            else if (((Int32)fOK.Grade) < ((Int32)i.Grade))
                                            {
                                                fOK = i;
                                            }
                                        }
                                    }
                                }
                                if (HD_VC_FalutManager.CurrentFalut == null)
                                    HD_VC_FalutManager.CurrentFalut = fOK;
                            }
                        }

                        fi = FalutsBCurrent.Find(a => a.LogicID == falutInfo.LogicID);
                        if (fi != null)
                        {
                            FalutsBCurrent.Remove(fi);
                            CurrentFalut = null;
                            if (CurrentFalut == fi)
                            {
                                if (FalutsBCurrent.Count != 0)
                                {
                                    CurrentFalut = FalutsBCurrent[0];
                                    for (int i = 0; i < FalutsBCurrent.Count; i++)
                                    {
                                        if (((Int32) CurrentFalut.Grade) >= ((Int32) FalutsBCurrent[i].Grade))
                                            CurrentFalut = FalutsBCurrent[i];
                                    }
                                }
                                else
                                {
                                    if (FalutsACurrent.Count != 0)
                                    {
                                        CurrentFalut = FalutsACurrent[0];
                                        for (int i = 0; i < FalutsACurrent.Count; i++)
                                        {
                                            if (((Int32) CurrentFalut.Grade) >= ((Int32) FalutsACurrent[i].Grade))
                                                CurrentFalut = FalutsACurrent[i];
                                        }
                                    }
                                }
                            }
                            else
                            {
                                FalutInfo fOK = null;
                                if (HD_VC_FalutManager.FalutsACurrent.Count != 0)
                                {
                                    foreach (var i in HD_VC_FalutManager.FalutsACurrent)
                                    {
                                        if (!i.IsOK)
                                        {
                                            if (HD_VC_FalutManager.CurrentFalut == null)
                                                HD_VC_FalutManager.CurrentFalut = i;
                                            else if (((Int32)HD_VC_FalutManager.CurrentFalut.Grade) < ((Int32)i.Grade))
                                            {
                                                HD_VC_FalutManager.CurrentFalut = i;
                                            }
                                        }
                                        else
                                        {
                                            if (fOK == null)
                                                fOK = i;
                                            else if (((Int32)fOK.Grade) < ((Int32)i.Grade))
                                            {
                                                fOK = i;
                                            }
                                        }
                                    }
                                }
                                if (HD_VC_FalutManager.FalutsBCurrent.Count != 0)
                                {
                                    foreach (var i in HD_VC_FalutManager.FalutsBCurrent)
                                    {
                                        if (!i.IsOK)
                                        {
                                            if (HD_VC_FalutManager.CurrentFalut == null)
                                                HD_VC_FalutManager.CurrentFalut = i;
                                            else if (((Int32)HD_VC_FalutManager.CurrentFalut.Grade) < ((Int32)i.Grade))
                                            {
                                                HD_VC_FalutManager.CurrentFalut = i;
                                            }
                                        }
                                        else
                                        {
                                            if (fOK == null)
                                                fOK = i;
                                            else if (((Int32)fOK.Grade) < ((Int32)i.Grade))
                                            {
                                                fOK = i;
                                            }
                                        }
                                    }
                                }

                                if (HD_VC_FalutManager.CurrentFalut == null)
                                    HD_VC_FalutManager.CurrentFalut = fOK;
                            }
                        }
                    }
                }
            }

            base.paint(g);
        }
    }
}
