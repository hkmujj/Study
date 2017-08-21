using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
namespace Urban.NanJing.AirportLine.DDU
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Operation : baseClass
    {
        private MovingStateData m_Power;
        private MovingStateData m_Braking;
        private MovingStateData2 m_DraughtBrake;
        private MovingStateData m_AirCondition;
        private MovingStateData m_Park;


        private Image[] m_ImageArray;


        public override string GetInfo()
        {
            return "运行信息";
        }


        public override bool init(ref int nErrorObjectIndex)
        {

            m_ImageArray = new Image[UIObj.ParaList.Count];
            for (int i = 0; i < UIObj.ParaList.Count; i++)
            {
                using (FileStream fs = new FileStream(RecPath + "\\" + UIObj.ParaList[i], FileMode.Open))
                {
                    m_ImageArray[i] = Image.FromStream(fs);
                }
            }


            m_Power = new MovingStateData(new Rectangle(Common.m_InitialPosX + 4, Common.m_InitialPosY + 295, 670, 42), "供电");
            m_Braking = new MovingStateData(new Rectangle(Common.m_InitialPosX + 4, Common.m_InitialPosX + 343, 670, 42), "制动");
            m_AirCondition = new MovingStateData(new Rectangle(Common.m_InitialPosX + 4, Common.m_InitialPosX + 460, 670, 42), "空调/照明");
            m_Park = new MovingStateData(new Rectangle(Common.m_InitialPosX + 4, Common.m_InitialPosX + 510, 670, 42), "停放制动");
            m_DraughtBrake = new MovingStateData2(new Rectangle(Common.m_InitialPosX + 4, Common.m_InitialPosX + 393, 670, 62), "牵引", "电气制动");



            return true;
        }

        private void GetVlaue()
        {

            #region 设置状态
            for (int i = 0; i < 4; i++)
            {
                #region 牵引
                for (int j = 0; j < 5; j++)
                {
                    if (BoolList[UIObj.InBoolList[2] + 5 * i + j])
                    {
                        m_DraughtBrake.m_Draught[i].ImagePicture = m_ImageArray[8 + j];
                    }
                }
                #endregion

                #region 电气制动
                for (int j = 0; j < 5; j++)
                {
                    if (BoolList[UIObj.InBoolList[3] + 5 * i + j])
                    {
                        m_DraughtBrake.m_Brake[i].ImagePicture = m_ImageArray[8 + j];
                    }
                }
                #endregion
            }

            for (int i = 0; i < 6; i++)
            {
                #region 供电
                for (int j = 0; j < 3; j++)
                {
                    if (BoolList[UIObj.InBoolList[0] + 3 * i + j])
                    {
                        m_Power.m_StateList[i].ImagePicture = m_ImageArray[j];
                    }
                }
                #endregion


                #region 制动
                for (int j = 0; j < 5; j++)
                {
                    if (BoolList[UIObj.InBoolList[1] + 5 * i + j])
                    {
                        m_Braking.m_StateList[i].ImagePicture = m_ImageArray[3 + j];
                    }
                }
                #endregion

                #region 舒适度
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[4] + 4 * i + j])
                    {
                        m_AirCondition.m_StateList[i].ImagePicture = m_ImageArray[18 + j];
                    }
                }
                #endregion

                #region 停放制动
                for (int j = 0; j < 4; j++)
                {
                    if (BoolList[UIObj.InBoolList[5] + 4 * i + j])
                    {
                        m_Park.m_StateList[i].ImagePicture = m_ImageArray[22 + j];
                    }
                }
                #endregion
            }
            #endregion


        }

        public override void paint(Graphics g)
        {
            //if (_imageArray[17] != null)
            //{
            //    _airCondition.StateList[0].ImagePicture = _imageArray[17];
            //}

            GetVlaue();
            m_Power.OnDraw(g);
            m_Braking.OnDraw(g);
            m_AirCondition.OnDraw(g);
            m_Park.OnDraw(g);
            m_DraughtBrake.OnDraw(g);



        }



    }
}
