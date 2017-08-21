using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using MMI.Facility.Interface.Attribute;
using Urban.Iran.HMI.Common;

namespace Urban.Iran.HMI
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class Interlocks : HMIBase
    {
        private Bitmap[] m_BmpArr;
        private int[] m_MemIndex;
        private List<CommonInnerControlBase> m_Collection;
        public override string GetInfo()
        {
            return "Interlocks";
        }

        protected override bool Initalize()
        {
            InitData();

            foreach (string[] strings in m_Collection.Select(s => s.Tag).Where(w => w != null))
            {
                UIObj.InBoolList.AddRange(strings.Select(s => Convert.ToInt32(GlobleParam.Instance.FindInBoolIndex(s))));
            }

            return true;
        }

        private void InitData()
        {
            m_Collection = new List<CommonInnerControlBase>();
            m_MemIndex = new int[3];
            InitImage();
            InitLineOneText();
            InitItemAndText();
        }

        private void InitItemAndText()
        {
            var temp = new Point(37, 145);
            var tempLocaiton = new Point(15, 135);
            var itemSize = new Size(20, 20);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    var tmp = GlobleParam.ResManagerText.GetString("Text" + (i * 12 + 69 + j).ToString("0000"));
                    m_Collection.Add(new GDIRectText()
                    {
                        Text = tmp,
                        TextBrush = GdiCommon.MediumGreyBrush,
                        BackColorVisible = false,
                        Location = temp,
                        DrawFont = GdiCommon.Txt10Font
                    });
                    if (!string.IsNullOrEmpty(tmp) && (i * 12 + 69 + j) != 96)
                    {
                        m_Collection.Add(new GDIRectText()
                        {
                            Tag = new[]
                            {
                                tmp + " status1",
                                tmp + " status2"
                            },
                            Location = tempLocaiton,
                            Size = itemSize,
                            RefreshAction = o => RefreshItem((GDIRectText)o),
                        });
                    }

                    temp.Offset(0, 24);
                    tempLocaiton.Offset(0, 24);
                }
                temp.Y = 145;
                tempLocaiton.Y = 135;
                temp.Offset(270, 0);
                tempLocaiton.Offset(270, 0);
            }
        }

        private void InitLineOneText()
        {
            Point[] tmpP = { new Point(15, 100), new Point(280, 100), new Point(555, 100) };
            string[] str = { "Ready to drive", "Traction block", "Emergency brake" };
            for (int i = 0; i < 3; i++)
            {
                m_Collection.Add(new GDIRectText()
                {
                    Text = str[i],
                    TextBrush = GdiCommon.MediumGreyBrush,
                    BackColorVisible = false,
                    Location = tmpP[i],
                    DrawFont = GdiCommon.Txt12Font
                });
            }
        }

        private void InitImage()
        {
            m_BmpArr = new[]
            {
                new Bitmap(RecPath + "\\frame/ItemRed.png"),
                new Bitmap(RecPath + "\\frame/ItemYellow.png"),
                new Bitmap(RecPath + "\\frame/ItemGreyBlue.png")
            };
        }

        private void RefreshItem(GDIRectText item)
        {
            var names = item.Tag as string[];
            if (names.Length < 2)
            {
                return;
            }
            if (BoolList[Convert.ToInt32(GlobleParam.Instance.FindInBoolIndex(names[0]))])
            {
                item.BKImage = m_BmpArr[0];
            }
            else if (BoolList[Convert.ToInt32(GlobleParam.Instance.FindInBoolIndex(names[1]))])
            {
                item.BKImage = m_BmpArr[1];
            }
            else
            {
                item.BKImage = m_BmpArr[2];
            }
        }

        public override void paint(Graphics g)
        {
            m_Collection.ForEach(e => e.OnPaint(g));
        }
    }
}