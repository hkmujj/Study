using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace HXD
{
    /*--------------------------------------------------------------------------------------------
     *                                                                                            * 
     *                                                                                            * 
     *           当前故障的读入刷新  为了能在 每个视图中均能实时刷新故障信息                      * 
     *                                                                                            * 
     *            顾将其作为一个独立的图元 在配置表中将其 放入所有的视图之中。                    * 
     *                                                                                            * 
     *                                                                                            * 
     *                                                                                            * 
     * --------------------------------------------------------------------------------------------
     */
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    class FaultRefresh:baseClass
    {
        private int starBit;//故障开始位  从配置文件中读取
        private int faultCount;//故障数量 从配置文件中读取
        
        private SortedList<int, Fault> currentFaults = new SortedList<int, Fault>();//当前活动故障
        public static SortedList<int, Fault> totalFaults = new SortedList<int, Fault>();//所有故障

        public override string GetInfo()
        {
            return "故障信息刷新";
        }

        public override string GetTypeName()
        {
            //1
            return "FaultRefresh";
        }

        public override bool initValue(string nParaString, ref int nErrorObjectIndex)
        {
            return base.initValue(nParaString, ref nErrorObjectIndex);
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            InitData();

            nErrorObjectIndex = -1;

            return true;
        }

        public override void paint(System.Drawing.Graphics dcGs)
        {
            GetValue();
            DrawOn(dcGs);
            base.paint(dcGs);
        }

        public void InitData()
        {
            starBit = UIObj.InBoolList[0];
            faultCount = UIObj.InBoolList[1];

        }

        public void GetValue()
        {
            #region 刷新活动故障

            //添加新的
            for (int i = 0; i < faultCount; i++)
            {
                if (BoolList[starBit + i])
                {
                    if (!currentFaults.ContainsKey(i))
                    {
                        Fault fault = new Fault();
                        fault.LogicalBit = HX_Fault.StaticFaults[i].LogicalBit;
                        fault.FaultID = HX_Fault.StaticFaults[i].FaultID;
                        fault.FaultText = HX_Fault.StaticFaults[i].FaultText;
                        fault.Level = HX_Fault.StaticFaults[i].Level;
                        fault.TrainNo = HX_Fault.StaticFaults[i].TrainNo;
                        fault.HappenedTime = DateTime.Now;

                        currentFaults.Add(fault.LogicalBit, fault);
                    }
                }
            }

            //删除已经消失的
            foreach (int key in currentFaults.Keys)
            {
                if (!BoolList[starBit + key])
                {
                    currentFaults[key].EndedTime = DateTime.Now;
                    totalFaults.Add(totalFaults.Count, currentFaults[key]);
                    currentFaults.Remove(key);
                    break;
                }
            }
            #endregion

        }

        public void DrawOn(Graphics g)
        {
        }
    }
}