using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections.Generic;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using System.Timers;
using ES.Facility.Common;
using ES.Facility.Common.Control;
using ES.Facility.Common.Control.Common;


namespace NJ_MMI
{

    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    class EmergencyBrake : baseClass 
    {
        Timer timer =new Timer (1000);

        public int a;
        public override string GetInfo()
        {

            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            //timer.Enabled = true;
            timer.AutoReset = true;
 
            return "紧急制动";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            a = 60;
            return true;
        }
        public void timer_Elapsed(object sender, EventArgs e)
        {
            
            --a;
           
        }
        public override void paint(Graphics g)
        {
            timer.Start();
            
            if (a <=0)
            {
                if (a == 0)
                {
                    append_postCmd(CmdType.ChangePage, 104, 0, 0);
                }
                timer.Stop();
                a = 60;
            }
 
            g.DrawString(Convert.ToString(a), Global.Font_Arial_11_B, Global.brush1, new Rectangle(240, 495, 200, 20), Global.SF_NC);
            g.DrawString("紧急制动", Global.Font_Arial_11_B, Global.brush1, new Rectangle(20, 470, 120, 20), Global.SF_NC);
            //timer.Start();
            g.DrawString("需等待60s回重联位解锁", Global.Font_Arial_11_B, Global.brush1, new Rectangle(20, 495, 200,20), Global.SF_NC);
            DateTime d = new DateTime(2016,1,25,0,0,0);
            base.paint(g);
        }
    }
}

