using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using MMI.Facility.Control.Entry;
using MMI.Facility.DataType.Log;

namespace MMI_PublicApp
{
    internal class MMIWPFApp : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                var boot = new MMIMefBootstrapper();
                boot.Run();
            }
            catch (ReflectionTypeLoadException loadException)
            {
                SysLog.Error("exception when boot run : {0} ", loadException.ToString());
                SysLog.Error("load error: {0} ", string.Join("\r\n", loadException.LoaderExceptions.Select(s => s.ToString())));
                throw;
            }
            catch (Exception exception)
            {
                SysLog.Error("exception when boot run : {0} ", exception.ToString());
                throw;
            }

            //var tmpProcess = Process.GetCurrentProcess();
            //tmpProcess.ProcessorAffinity = (IntPtr)0x0002;
            var ep = new EntryPoint();
            ep.FormClosed += (sender, args) =>
            {
                Shutdown();
            };
            ep.Run(null);
        }
    }
}