using System.Windows;

namespace Subway.WuHanLine6.Extention
{
    /// <summary>
    /// Windwos扩展
    /// </summary>
    public static class WindowsExtention
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="window"></param>
        public static void ShowWindows(this Window window)
        {
            //window.Left = Screen.PrimaryScreen.WorkingArea.Width;
            //window.Top = Screen.AllScreens.FirstOrDefault(f => !f.Primary).WorkingArea.Top;
            window.Show();
        }
    }
}