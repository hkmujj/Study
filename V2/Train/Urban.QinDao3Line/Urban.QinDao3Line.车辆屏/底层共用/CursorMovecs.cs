using Urban.QingDao3Line.MMI.����;

namespace Urban.QingDao3Line.MMI.�ײ㹲��
{
    public class CursorMovecs
    {
        //������
        private int m_Btnid;
        //��������
        private readonly string  m_Content;

        public CursorMovecs(int index, string  content)
        {
            m_Btnid = index;
            m_Content = content;
        }

        public void Cursors()
        {
            if(!PassWordScreen.m_ContentDictionary.ContainsKey(PassWordScreen.Index)&&PassWordScreen.m_ContentDictionary.Count<6)
                PassWordScreen.m_ContentDictionary.Add(PassWordScreen.Index,PassWordScreen.m_View);
            if(!PassWordScreen.m_PassWordDictionary.ContainsKey(PassWordScreen.Index)&&PassWordScreen.m_PassWordDictionary.Count<6)
                PassWordScreen.m_PassWordDictionary.Add(PassWordScreen.Index,m_Content.ToString());

            if (!DateAndTimeScreen.m_ContentDictionary.ContainsKey(DateAndTimeScreen.m_Index) && DateAndTimeScreen.m_ContentDictionary.Count < 12)
                DateAndTimeScreen.m_ContentDictionary.Add(DateAndTimeScreen.m_Index, DateAndTimeScreen.m_View);
            if (!DateAndTimeScreen.m_PassWordDictionary.ContainsKey(DateAndTimeScreen.m_Index) && DateAndTimeScreen.m_PassWordDictionary.Count < 12)
                DateAndTimeScreen.m_PassWordDictionary.Add(DateAndTimeScreen.m_Index, m_Content.ToString());
            //PassWordScreen.m_Index++;
        }
    }
}