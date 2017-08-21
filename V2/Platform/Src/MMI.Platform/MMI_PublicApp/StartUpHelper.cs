using System.Windows;

namespace MMI_PublicApp
{
    /// <summary>
    /// 
    /// </summary>
    internal class StartUpHelper
    {
        public static readonly StartUpHelper Instance = new StartUpHelper();

        private StartUpHelper()
        {
            
        }


        private SplashScreen m_StartUpView;

        public void ShowStartUpView()
        {
            m_StartUpView = new SplashScreen("YundaStartingUp.png");
            m_StartUpView.Show(true, false);
        }
    }
}