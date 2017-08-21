using System.Windows;
using Microsoft.Practices.ServiceLocation;

namespace MMITool.Platform
{
    class ShellFactory
    {
        public static Window CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Window>("Shell");
        }
    }
}
