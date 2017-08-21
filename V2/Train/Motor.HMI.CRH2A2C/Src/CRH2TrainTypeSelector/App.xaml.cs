using System;
using System.Windows;

namespace CRH2TrainTypeSelector
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.SetupInformation.PrivateBinPath = @"Addin";

            
            base.OnStartup(e);

            //AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;

            GlobalParam.Instance.StartupEventArgs = e;

            var app = new CRH2TrainTypeSelectorMefBootstrapper();
            app.Run();
        }

        //private Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
        //{
        //    if (args.Name == typeof(CRH2Type).Assembly.FullName)
        //    {
        //        return Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Addin", typeof(CRH2Type).Assembly.ManifestModule.Name));
        //        return null;
        //    }

        //    return null;
        //}
    }
}
