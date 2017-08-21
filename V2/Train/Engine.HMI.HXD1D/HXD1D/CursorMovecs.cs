using HXD1D.Titlte;
using HXD1D.控制设置;

namespace HXD1D
{
    public class CursorMovecs
    {
        //按键号
        private int _btnid;
        //传递内容
        private int _content;
        //
        private int _type;
        public CursorMovecs(int index, int content, int type)
        {
            _btnid = index;
            _content = content;
            _type = type;
        }

        public void Cursors()
        {
            if (Title.CurentView == 19 || Title.CurentView == 20)
            {
                Title.Current = _content;
                if (_type == 1)
                {
                    Title.ButtonReset();
                    Title.buttonIsDown[_btnid] = true;
                    if (Title.ContentDictionary.ContainsKey(ControlSeting._rowid))
                    {
                        Title.ContentDictionary.Remove(ControlSeting._rowid);
                        Title.ContentDictionary.Add(ControlSeting._rowid, Title.Current);
                    }
                    if (!Title.ContentDictionary.ContainsKey(ControlSeting._rowid) && Title.ContentDictionary.Count < 7)

                        Title.ContentDictionary.Add(ControlSeting._rowid, Title.Current);
                    ControlSeting._rowid++;
                    if (Title.CurentView == 19) ControlSeting._rowid %= 7;
                }
                else
                {
                    Title.buttonIsDown[_btnid] = true;
                    if (Title.ContentDictionary.ContainsKey(ControlSeting._rowid))
                    {
                        Title.ContentDictionary.Remove(ControlSeting._rowid);
                        Title.ContentDictionary.Add(ControlSeting._rowid, Title.Current);
                    }
                    if (!Title.ContentDictionary.ContainsKey(ControlSeting._rowid) && Title.ContentDictionary.Count < 7)

                        Title.ContentDictionary.Add(ControlSeting._rowid, Title.Current);
                    ControlSeting._rowid++;
                }


            }
            else if (Title.CurentView == 22)
            {
                Title.Current = _content;
                if (_type == 1)
                {
                    Title.ButtonReset();
                    Title.buttonIsDown[_btnid] = true;
                    //if (Title.ContentLists.Count < 3)
                    //{
                    //    Title.ContentLists.Add(Title.Current);
                    //}

                }
                else
                {
                    Title.buttonIsDown[_btnid] = true;
                }
                if (Title.ContentLists.Count < 3)
                {
                    Title.ContentLists.Add(Title.Current);
                }
            }
            else if (Title.CurentView == 39)
            {
                Title.Current = _content;
                if (_type == 1)
                {
                    if (Title.ContentDictionary.ContainsKey(ControlSeting._rowid))
                    {
                        Title.ContentDictionary.Remove(ControlSeting._rowid);
                        Title.ContentDictionary.Add(ControlSeting._rowid, Title.Current);
                    }
                    if (!Title.ContentDictionary.ContainsKey(ControlSeting._rowid) && Title.ContentDictionary.Count < 6)
                        Title.ContentDictionary.Add(ControlSeting._rowid, Title.Current);
                    if (!Title.PassWordDictionary.ContainsKey(ControlSeting._rowid) &&
                        Title.PassWordDictionary.Count < 6)
                        Title.PassWordDictionary.Add(ControlSeting._rowid, "*");
                    ControlSeting._rowid++;
                }

                else
                {
                    if (Title.ContentDictionary.ContainsKey(ControlSeting._rowid))
                    {
                        Title.ContentDictionary.Remove(ControlSeting._rowid);
                        Title.ContentDictionary.Add(ControlSeting._rowid, Title.Current);
                    }
                    if (!Title.ContentDictionary.ContainsKey(ControlSeting._rowid) && Title.ContentDictionary.Count < 6)
                        Title.ContentDictionary.Add(ControlSeting._rowid, Title.Current);
                    if (!Title.PassWordDictionary.ContainsKey(ControlSeting._rowid) &&
                        Title.PassWordDictionary.Count < 6)
                        Title.PassWordDictionary.Add(ControlSeting._rowid, "*");
                    ControlSeting._rowid++;
                }
            }
            else if (Title.CurentView == 46)
            {
                Title.Current = _content;
                if (_type == 1)
                {
                    if (!Title.PassWordDictionary.ContainsKey(ControlSeting._rowid) &&
                        Title.PassWordDictionary.Count < 6)
                        Title.PassWordDictionary.Add(ControlSeting._rowid, Title.Current.ToString());
                    ControlSeting._rowid = (ControlSeting._rowid+1) % 6;
                }
                else
                {
                    if (!Title.PassWordDictionary.ContainsKey(ControlSeting._rowid) &&
                        Title.PassWordDictionary.Count < 6)
                        Title.PassWordDictionary.Add(ControlSeting._rowid, Title.Current.ToString());
                    ControlSeting._rowid = (ControlSeting._rowid+1) % 6;
                }
            }
        }



    }
}

