using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using MMI.Facility.PublicUI;
using System.IO;

namespace NJ_MMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class VC_C0_GetValue : baseClass
    {
        public static List<MessageInfo> Infos = new List<MessageInfo>();
        public static List<MessageInfo> CurrentInfos = new List<MessageInfo>();
        public static List<MessageInfo> AllInfos = new List<MessageInfo>();
        
        public static MessageInfo CurrentInfo
        {
            get 
            {
                if (CurrentInfos != null && CurrentInfos.Count != 0)
                {
                    _currentInfo = CurrentInfos[0];
                }
                    //_currentInfo = CurrentInfos[CurrentInfos.Count - 1];
                else _currentInfo = null;

                return _currentInfo;
            }
            set
            {
                _currentInfo = value;
            }
        }
        private static MessageInfo _currentInfo = null;


        public static List<MessageInfo> Faluts = new List<MessageInfo>();
        public static List<MessageInfo> CurrentFaluts = new List<MessageInfo>();
        public static List<MessageInfo> AllFaluts = new List<MessageInfo>();

        public static MessageInfo CurrentFalut
        {
            get
            {
                if (CurrentFaluts != null && CurrentFaluts.Count != 0)
                {
                    _currentFalut = CurrentFaluts[0];
                }
                //_currentFalut = CurrentFaluts[CurrentFaluts.Count - 1];
                else _currentFalut = null;

                return _currentFalut;
            }
            set
            {
                _currentFalut = value;
            }
        }
        private static MessageInfo _currentFalut = null;

        public override string GetInfo()
        {
            return "获取数据";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            string[] tmpFile1 = System.IO.File.ReadAllLines(
               Path.Combine(RecPath, "..\\config\\消息信息.txt"),
               System.Text.Encoding.Default
               );
            foreach (string str0 in tmpFile1)
            {
                String[] split = str0.Split(new char[] { ':' });

                if (split.Length != 2)
                    continue;
                Infos.Add(new MessageInfo() 
                {
                    LogicID = Convert.ToInt32(split[0]), 
                    Description = split[1] 
                });
            }

            string[] tmpFile0 = System.IO.File.ReadAllLines(
               Path.Combine(RecPath, "..\\config\\故障信息.txt"),
               System.Text.Encoding.Default
               );
            foreach (string str0 in tmpFile0)
            {
                String[] split = str0.Split(new char[] { ':' });

                if (split.Length != 2)
                    continue;
                Faluts.Add(new MessageInfo()
                {
                    LogicID = Convert.ToInt32(split[0]),
                    Description = split[1]
                });
            }

            return true;
        }

        public override void paint(Graphics dcGs)
        {
            foreach (var item in Infos)
            {
                if (BoolList[item.LogicID] && !BoolOldList[item.LogicID])
                {
                    MessageInfo falut = CurrentInfos.Find(a => a.LogicID == item.LogicID);
                    if (falut == null)
                    {
                        falut = new MessageInfo()
                        {
                            LogicID = item.LogicID,
                            Description = item.Description,
                            Time = DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + "."
                                + "  " + DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString() + "." + DateTime.Now.Second.ToString()
                        };
                        CurrentInfos.Add(falut);
                        AllInfos.Add(falut);
                    }

                    //if (!CurrentInfos.Contains(item))
                    //{
                    //    item.IsOK = false;
                    //    item.Time = DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + "."
                    //            + "  " + DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString() + "." + DateTime.Now.Second.ToString();
                    //    CurrentInfos.Add(item);
                    //}

                    //if (!AllInfos.Contains(item))
                    //{
                    //    item.Time = DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + "."
                    //            + "  " + DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString() + "." + DateTime.Now.Second.ToString();
                    //    AllInfos.Add(item);
                    //}
                }
                //else if (!BoolList[item.LogicID] && BoolOldList[item.LogicID])
                //{
                //    if(CurrentInfos.Contains(item))
                //    {
                //        CurrentInfos.Remove(item);
                //    }
                //}
            }

            foreach (var item in Faluts)
            {
                if (BoolList[item.LogicID] && !BoolOldList[item.LogicID])
                {
                    MessageInfo falut = CurrentFaluts.Find(a => a.LogicID == item.LogicID);
                    if (falut == null)
                    {
                        falut = new MessageInfo()
                        {
                            LogicID = item.LogicID,
                            Description = item.Description,
                            Time = DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + "."
                                + "  " + DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString() + "." + DateTime.Now.Second.ToString()
                        };
                        CurrentFaluts.Add(falut);
                        AllFaluts.Add(falut);
                    }
                    //if (!CurrentFaluts.Contains(item))
                    //{
                    //    item.Time = DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + "."
                    //            + "  " + DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString() + "." + DateTime.Now.Second.ToString();
                    //    CurrentFaluts.Add(item);
                    //}

                    //if (!AllFaluts.Contains(item))
                    //{
                    //    item.Time = DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + "."
                    //            + "  " + DateTime.Now.Hour.ToString() + "." + DateTime.Now.Minute.ToString() + "." + DateTime.Now.Second.ToString();
                    //    AllFaluts.Add(item);
                    //}
                }
            }

            base.paint(dcGs);
        }
    }
}
