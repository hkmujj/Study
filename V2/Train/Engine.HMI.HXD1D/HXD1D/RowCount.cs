using System;
using System.Collections.Generic;
using System.Drawing;
using HXD1D.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface;

namespace HXD1D
{
    class RowCount : baseClass
    {
        private List<Rectangle> _rects=new List<Rectangle>();

        private Dictionary<int, string> _content=new Dictionary<int, string>();

        private IReadOnlyDictionary<int, bool> _rowlist;
     

        public RowCount(List<Rectangle> rects,Dictionary<int,string> content,IReadOnlyDictionary<int,bool> rowList)
        {
            _rects = rects;

            _content = content;

            _rowlist = rowList;

        }


        public void Count(Graphics e)
        {
            List<int> id = new List<int>();
       
            try
            {
                Int32 j = 0;
                id.AddRange(_content.Keys);
                for (int i = id.Count-1; i >= 0; i--)
                {
                    j++;
                    if (_rowlist[id[i]])
                    {
                        e.DrawString(_content[id[i]], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[_content.Count - 1 - i]);
                    }
                    if (j == 5) break;
                }

                //for (int i = 0; i < _content.Count && i < 5; i++)
                //{
                //    if (_rowlist[id[i]])
                //    {
                //        e.DrawString(_content[id[i]], FormatStyle.Font12, FormatStyle.WhiteBrush, _rects[_content.Count - 1 - i]);
                //    }


                //}
            }
            catch (Exception)
            {
            	
            }
           

        }

    }
}
