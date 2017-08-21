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
     *           ��ǰ���ϵĶ���ˢ��  Ϊ������ ÿ����ͼ�о���ʵʱˢ�¹�����Ϣ                      * 
     *                                                                                            * 
     *            �˽�����Ϊһ��������ͼԪ �����ñ��н��� �������е���ͼ֮�С�                    * 
     *                                                                                            * 
     *                                                                                            * 
     *                                                                                            * 
     * --------------------------------------------------------------------------------------------
     */
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    class FaultRefresh:baseClass
    {
        private int starBit;//���Ͽ�ʼλ  �������ļ��ж�ȡ
        private int faultCount;//�������� �������ļ��ж�ȡ
        
        private SortedList<int, Fault> currentFaults = new SortedList<int, Fault>();//��ǰ�����
        public static SortedList<int, Fault> totalFaults = new SortedList<int, Fault>();//���й���

        public override string GetInfo()
        {
            return "������Ϣˢ��";
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
            #region ˢ�»����

            //����µ�
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

            //ɾ���Ѿ���ʧ��
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