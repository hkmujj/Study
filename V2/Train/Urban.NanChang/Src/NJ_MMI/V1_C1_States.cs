using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace NJ_MMI
{
    [GksDataType(MMI.Facility.Interface.Attribute.DataType.isMMIObjectClass)]
    public class V1_C1_States : baseClass
    {
        private Timer _timer;
        private String _dwell = String.Empty;
        private Int32 _timeCountDown = 0;
        private Int32 _timeCount = 999;
        private Boolean _isCountDownFlag = false;

        private String[] modes;
        private String[] dockedStatus;
        private String[] departStatus;
        private List<Image> _images = new List<Image>();

        public override string GetInfo()
        {
            return "状态栏";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            this.modes = new String[8] { "OFF", "ATO", "ATPM", "IATP", "RMF", "RMR", "DTB", "" };
            this.dockedStatus = new String[4] { "YES", "NO", "", "---" };
            this.departStatus = new String[4] { "NO", "YES", "", "---" };

            this._timer = new Timer();
            this._timer.Interval = 1000;
            this._timer.Tick += _timer_Tick;

            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    this._images.Add(Image.FromStream(fs));
                }
            });

            return true;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (this._isCountDownFlag)
            {
                if (this._timeCountDown < 1000)
                {
                    this._timeCountDown++;
                    _dwell = this._timeCountDown.ToString();
                }
                else
                {
                    this._timer.Stop();
                    this._brush_Dwell = new SolidBrush(Color.FromArgb(255, 255, 0));
                    _dwell = "<>";
                }
            }
            else
            {
                if (this._timeCount == 0)
                {
                    this._timer.Stop();
                }
                else this._timeCount--;

                if (this._timeCount > 5)
                {
                    this._brush_Dwell = new SolidBrush(Color.FromArgb(255, 255, 0));
                }
                else
                {
                    this._brush_Dwell = Brushes.Red;
                }
                _dwell = this._timeCount.ToString();
            }
        }

        public override void paint(Graphics dcGs)
        {
            this.drawMode(dcGs);       //模式状态
            this.drawDeckedStatu(dcGs);//停靠状态
            this.drawDepartState(dcGs);//发车状态
            this.drawDwell(dcGs);      //停站状态

            base.paint(dcGs);
        }

        private String drawMode(Graphics dcGs)
        {
            String mode = String.Empty;

            for (int i = 0; i < 8; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    Font f = new Font("Arial", 16, FontStyle.Bold);
                    dcGs.DrawString(this.modes[i], new Font("Arial", 16, FontStyle.Bold), new SolidBrush(Color.FromArgb(0, 255, 255)), new PointF(115, 15));
                    mode = this.modes[i];
                    break;
                }
            }

            return mode;
        }

        private void drawDeckedStatu(Graphics dcGs)
        {
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[1] + i])
                {
                    Font f = new Font("Arial", 16, FontStyle.Bold);
                    Brush b = new SolidBrush(Color.FromArgb(0, 255, 0));
                    if (i == 3)
                        b = new SolidBrush(Color.Red);
                    dcGs.DrawString(this.dockedStatus[i], f, b, new PointF(325, 15));
                    break;
                }
            }
        }
        private Brush _brush_Dwell = new SolidBrush(Color.FromArgb(255, 255, 0));
        /// <summary>
        /// 绘制停站状态
        /// </summary>
        /// <param name="dcGs"></param>
        private void drawDwell(Graphics dcGs)
        {
            for (int i = 0; i < 3; i++)
            {
                if (BoolList[UIObj.InBoolList[2] + i])
                {
                    if (i == 0)
                    {
                        this._isCountDownFlag = true;
                        _brush_Dwell = new SolidBrush(Color.Red);
                        this._timer.Start();
                        this._timeCount = 11;
                    }
                    else if (i == 1)
                    {
                        this._isCountDownFlag = false;
                        this._timer.Start();
                        this._timeCountDown = 0;
                    }
                    else
                    {
                        this._isCountDownFlag = false;
                        this._timer.Stop();
                        this._timeCountDown = 0;
                        this._timeCount = 10;
                        this._dwell = "";
                    }

                    break;
                }
            }

            if (this._isCountDownFlag)
            {
                dcGs.DrawImage(this._images[0], new PointF(540 - 17f, 3));
            }
            dcGs.DrawString(
                _dwell, 
                new Font("Arial", 16, FontStyle.Bold),
                this._brush_Dwell,
                new RectangleF(540 - 17, 3+3, 53, 50), 
                new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
        }

        private void drawDepartState(Graphics dcGs)
        {
            for (int i = 0; i < 4; i++)
            {
                if (BoolList[UIObj.InBoolList[3] + i])
                {
                    Font f = new Font("Arial", 16, FontStyle.Bold);
                    Brush b = new SolidBrush(Color.Red);
                    if (i == 1)
                        b = new SolidBrush(Color.Green);
                    dcGs.DrawString(this.departStatus[i], f, b, new PointF(735, 15));
                    break;
                }
            }
        }
    }
}
