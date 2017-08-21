namespace Engine.TCMS.HXD3D.Title.TopTitle
{
    public class TopTitleManticeNameFixder : ITopTitleNameFixder
    {
        private TopTitleScreen m_TopTitleScreen;

        public TopTitleManticeNameFixder(TopTitleScreen topTitleScreen)
        {
            m_TopTitleScreen = topTitleScreen;
        }

        public string UpdateTitleName(int nParaA, int nParaB, float nParaC, string currentName = null)
        {
            if (nParaB == 1 && m_TopTitleScreen.MainModel)
            {
                return "维护";
            }

            return currentName;
        }
    }
}