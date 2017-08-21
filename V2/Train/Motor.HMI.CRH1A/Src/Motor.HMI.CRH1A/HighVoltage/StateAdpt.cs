using System;
using System.Collections.Generic;
using System.Text;

namespace Motor.HMI.CRH1A.HighVoltage
{
    public class StateAdpt
    {

        public AcceptEleArc.State GetAcceptEleArcState(AcceptEleArc acceptEleArc, bool[] valueb)
        {
            var offset = 0;
            if (acceptEleArc.CurrentId == AcceptEleArc.Id.No7)
            {
                offset = 1;
            }
            if (valueb[offset]) //2号受电弓升起
            {
                if (valueb[2 + offset]) //2号受电弓切断
                {
                    return AcceptEleArc.State.CutOffAndRise;
                    //g.DrawImage(image[14], Gong_Rect[i]);
                }
                if (valueb[4 + offset])
                {
                    return AcceptEleArc.State.FaultAndRise;
                    //g.DrawImage(image[12], Gong_Rect[i]);
                }
                return AcceptEleArc.State.HasRise;
                //g.DrawImage(image[16], Gong_Rect[i]);
            }
            else //受电弓降下时的状态
            {
                if (valueb[2 + offset]) //2号受电弓切断
                {
                    return AcceptEleArc.State.CutOffAndDrop;
                    //g.DrawImage(image[15], Gong_Rect[i]);
                }
                if (valueb[4 + offset])
                {
                    return AcceptEleArc.State.FaultAndDrop;
                    //g.DrawImage(image[13], Gong_Rect[i]);
                }
                return AcceptEleArc.State.DropOrUnkown;
                //g.DrawImage(image[17], Gong_Rect[i]);
            }
        }

        public HighVoltageSwitch.State GetHighVoltageSwitchState(HighVoltageSwitch highVoltageSwitch, bool[] valueb)
        {
            int i = highVoltageSwitch.Id;
            if (valueb[i + 6]) //第i个网侧断路器闭合
            {
                if (valueb[i + 16]) //第i个网侧断路器故障并且闭合
                {

                    return HighVoltageSwitch.State.FaultButConnect;
                    //if (i == 1 || i==3)
                    //{
                    //    g.DrawImage(image[10], Duan_Rect[i]);
                    //}
                    //else
                    //{
                    //  g.DrawImage(image[4], Duan_Rect[i]);
                    //}

                }
                else if (valueb[i + 11]) //第i个断路器切断并闭合
                {
                    return HighVoltageSwitch.State.CutOffButConnect;
                    //if (i == 1 || i==3)
                    //{
                    //    g.DrawImage(image[8], Duan_Rect[i]);
                    //}
                    //else
                    //{
                    //    g.DrawImage(image[2], Duan_Rect[i]);
                    //}

                }
                else
                {
                    return HighVoltageSwitch.State.HasConnect;
                    //if (i == 1 || i==3)
                    //{
                    //    g.DrawImage(image[6], Duan_Rect[i]);
                    //}
                    //else
                    //{
                    //    g.DrawImage(image[0], Duan_Rect[i]);
                    //}

                }
            }
            else
            {
                if (valueb[i + 16]) //第i个网侧断路器故障并且断开
                {
                    return HighVoltageSwitch.State.FaultAndDisconnect;
                    //if (i == 1 || i == 3)
                    //{
                    //    g.DrawImage(image[11], Duan_Rect[i]);
                    //}
                    //else
                    //{
                    //    g.DrawImage(image[5], Duan_Rect[i]);
                    //}
                }
                else if (valueb[i + 11]) //第i个网侧断路器切断并断开
                {
                    return HighVoltageSwitch.State.CutOffAndDisconnect;
                    //if (i == 1 || i==3)
                    //{
                    //    g.DrawImage(image[9], Duan_Rect[i]);
                    //}
                    //else
                    //{
                    //    g.DrawImage(image[3], Duan_Rect[i]);
                    //}
                }
                else
                {
                    return HighVoltageSwitch.State.DisconnectOrUnkown;
                    //if (i == 1 || i==3)
                    //{
                    //    g.DrawImage(image[7], Duan_Rect[i]);//第i个网侧断路器切断并断开
                    //}
                    //else
                    //{
                    //    g.DrawImage(image[1], Duan_Rect[i]);//第i个网侧断路器切断并断开}

                    //}
                }
            }
        }
    }
}
