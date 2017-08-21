using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;

namespace Subway.ATC.THALES.VOBC
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V1_C1_States : baseClass
    {
        private Timer _timer;
        private string _dwell = string.Empty;
        private int _timeCountDown = 0;
        private int _timeCount = 999;
        private bool _isCountDownFlag = false;

        private string[] modes;
        private string[] dockedStatus;
        private string[] departStatus;
        private List<Image> _images = new List<Image>();

        public override string GetInfo()
        {
            return "状态栏";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            modes = new string[8] { "OFF", "ATO", "ATPM", "IATP", "RMF", "RMR", "DTB", "" };
            dockedStatus = new string[4] { "YES", "NO", "", "---" };
            departStatus = new string[4] { "NO", "YES", "", "---" };

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Tick += _timer_Tick;

            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath, a), FileMode.Open))
                {
                    _images.Add(Image.FromStream(fs));
                }
            });

            return true;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            if (_isCountDownFlag)
            {
                if (_timeCountDown < 1000)
                {
                    _timeCountDown++;
                    _dwell = _timeCountDown.ToString();
                }
                else
                {
                    _timer.Stop();
                    _brush_Dwell = new SolidBrush(Color.FromArgb(255, 255, 0));
                    _dwell = "<>";
                }
            }
            else
            {
                if (_timeCount == 0)
                {
                    _timer.Stop();
                }
                else
                {
                    _timeCount--;
                }

                if (_timeCount > 5)
                {
                    _brush_Dwell = new SolidBrush(Color.FromArgb(255, 255, 0));
                }
                else
                {
                    _brush_Dwell = Brushes.Red;
                }
                _dwell = _timeCount.ToString();
            }
        }

        public override void paint(Graphics dcGs)
        {
            drawMode(dcGs);       //模式状态
            drawDeckedStatu(dcGs);//停靠状态
            drawDepartState(dcGs);//发车状态
            drawDwell(dcGs);      //停站状态

            
        }

        private string drawMode(Graphics dcGs)
        {
            string mode = string.Empty;

            for (int i = 0; i < 8; i++)
            {
                if (BoolList[UIObj.InBoolList[0] + i])
                {
                    Font f = new Font("Arial", 16, FontStyle.Bold);
                    dcGs.DrawString(modes[i], new Font("Arial", 16, FontStyle.Bold), new SolidBrush(Color.FromArgb(0, 255, 255)), new PointF(115, 15));
                    mode = modes[i];
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
                    {
                        b = new SolidBrush(Color.Red);
                    }
                    dcGs.DrawString(dockedStatus[i], f, b, new PointF(325, 15));
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
                        _isCountDownFlag = true;
                        _brush_Dwell = new SolidBrush(Color.Red);
                        _timer.Start();
                        _timeCount = 11;
                    }
                    else if (i == 1)
                    {
                        _isCountDownFlag = false;
                        _timer.Start();
                        _timeCountDown = 0;
                    }
                    else
                    {
                        _isCountDownFlag = false;
                        _timer.Stop();
                        _timeCountDown = 0;
                        _timeCount = 10;
                        _dwell = "";
                    }

                    break;
                }
            }

            if (_isCountDownFlag)
            {
                dcGs.DrawImage(_images[0], new PointF(540 - 17f, 3));
            }
            dcGs.DrawString(
                _dwell, 
                new Font("Arial", 16, FontStyle.Bold),
                _brush_Dwell,
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
                    {
                        b = new SolidBrush(Color.Green);
                    }
                    dcGs.DrawString(departStatus[i], f, b, new PointF(735, 15));
                    break;
                }
            }
        }
    }
}
