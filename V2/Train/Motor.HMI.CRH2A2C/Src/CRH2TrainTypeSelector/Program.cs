using System;
using System.Reflection;

namespace CRH2TrainTypeSelector
{
    public static class Program
    {
        static Program()
        {
            var privatePths = "Addin";
            AppDomain.CurrentDomain.SetData("PRIVATE_BINPATH", privatePths);
            AppDomain.CurrentDomain.SetData("BINPATH_PROBE_ONLY", privatePths);
            var m = typeof(AppDomainSetup).GetMethod("UpdateContextProperty", BindingFlags.NonPublic | BindingFlags.Static);
            var funsion = typeof(AppDomain).GetMethod("GetFusionContext", BindingFlags.NonPublic | BindingFlags.Instance);
            m.Invoke(null, new[] {funsion.Invoke(AppDomain.CurrentDomain, null), "PRIVATE_BINPATH", privatePths});
        }

        [STAThread]
        public static void Main(string[] args)
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}