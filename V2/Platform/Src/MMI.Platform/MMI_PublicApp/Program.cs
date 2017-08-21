using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CommonUtil.Util;
using log4net.Config;
using MMI.Facility.Control.Data;
using MMI.Facility.DataType.Config;
using MMI.Facility.DataType.Log;
using MMI.Facility.Interface.Data;

namespace MMI_PublicApp
{
    internal static class Program
    {
        public static Mutex AppMutex;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            
            XmlConfigurator.Configure(
                new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\LogConfig.xml")));

            SysLog.Info("The current app is {0}", typeof(Program).Assembly);

            Contract.ContractFailed += ContractOnContractFailed;
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomainOnAssemblyLoad;
            AppDomain.CurrentDomain.UnhandledException +=
                (sender, eventArgs) => DisplayAndRecordError((Exception) eventArgs.ExceptionObject);
            Application.ThreadException += (sender, eventArgs) => DisplayAndRecordError(eventArgs.Exception);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ThreadPool.QueueUserWorkItem(obj => RecordeExsitFiles(AppDomain.CurrentDomain.BaseDirectory));

            WaitDebuggerIfNeed();

            if (!CheckOneAppInstanceIfNeed())
            {
                return;
            }

            StartUpHelper.Instance.ShowStartUpView();

            if (System.Windows.Application.Current == null)
            {
                var app = new MMIWPFApp();
                if (System.Windows.Application.Current != null)
                {
                    System.Windows.Application.Current.DispatcherUnhandledException +=
                        (sender, eventArgs) => DisplayAndRecordError(eventArgs.Exception);
                    System.Windows.Application.Current.Dispatcher.UnhandledException +=
                        (sender, eventArgs) => DisplayAndRecordError(eventArgs.Exception);
                }
                app.Run();
            }
        }

        private static bool CheckOneAppInstanceIfNeed()
        {
            var cm = ConfigManager.Instance;
            cm.LoadSysConfigIfNeed(AppDomain.CurrentDomain.BaseDirectory);

            if (cm.Config.SystemConfig != null &&
                cm.Config.SystemConfig.StartModel != StartModel.Edit)
            {
                AppMutex = new Mutex(true, typeof(Program).FullName);
                if (!AppMutex.WaitOne(0, false))
                {
                    MessageBox.Show(@"只允许存在一个正常运行的实例。", @"MMI");
                    return false;
                }
            }

            return true;
        }

        private static void WaitDebuggerIfNeed()
        {
            var platConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", PlatformConfig.FileName);
            if (File.Exists(platConfig))
            {
                var config = DataSerialization.DeserializeFromXmlFile<PlatformConfig>(platConfig);
                if (config != null && config.WaitForDebugger)
                {
                    SysLog.Info("Setted PlatformConfig.WaitForDebugger, wait for debugger to contue.");

                    var semaphore = new Semaphore(0, 1);
                    ThreadPool.QueueUserWorkItem(obj =>
                    {
                        var sh = (Semaphore) obj;

                        while (!Debugger.IsAttached)
                        {
                            Thread.Sleep(1000);
                        }

                        SysLog.Info("Debugger has attached.");

                        sh.Release();
                    }, semaphore);

                    semaphore.WaitOne();
                    semaphore.Dispose();
                    semaphore = null;
                }
            }
        }

        private static void RecordeExsitFiles(string rootDir)
        {
            if (string.IsNullOrEmpty(rootDir))
            {
                SysLog.Error("Can not RecordeFiles , the directory is null or empty.");
                return;
            }

            var files =
                Directory.GetFiles(rootDir, "*", SearchOption.AllDirectories).Select(s => new FileInfo(s)).ToList();

            var sb = new StringBuilder();
            sb.AppendFormat("All file in root path {0} : Total count = {1}, ", rootDir, files.Count);

            foreach (var gb in files.GroupBy(g => g.Extension))
            {
                sb.AppendFormat("Count of {0} = {1}, ", gb.Key, gb.Count());
            }

            sb.AppendLine();

            foreach (var f in files)
            {
                var info = FileVersionInfo.GetVersionInfo(f.FullName);
                sb.AppendFormat("\t\t文件名称={0};\t", info.FileName);
                sb.AppendFormat("产品名称={0};\t", info.ProductName);
                sb.AppendFormat("公司名称={0};\t", info.CompanyName);
                sb.AppendFormat("文件版本={0};\t", info.FileVersion);
                sb.AppendFormat("产品版本={0};\t", info.ProductVersion);
                sb.AppendFormat("系统显示文件版本：{0}.{1}.{2}.{3};\t", info.ProductMajorPart, info.ProductMinorPart,
                    info.ProductBuildPart, info.ProductPrivatePart);
                sb.AppendFormat("文件大小={0};\r\n", Math.Ceiling(f.Length/1024.0) + " KB");

            }

            SysLog.Debug("All file in root path {0} : \r\n{1}", rootDir, sb.ToString());
        }

        private static void CurrentDomainOnAssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            SysLog.Debug("{0} loaded !", args.LoadedAssembly);
        }

        private static void ContractOnContractFailed(object sender, ContractFailedEventArgs contractFailedEventArgs)
        {
            var stack = new StackTrace(true);
            SysLog.Fatal(
                string.Format(
                    "Occuse some code contract fail ! \n\tType: {0} \n\tCondition: {1} \n\tMessage: {2} \n\tStack: {3}",
                    contractFailedEventArgs.FailureKind, contractFailedEventArgs.Condition,
                    contractFailedEventArgs.Message, stack));
        }

        private static void DisplayAndRecordError(Exception error)
        {
            if (AppDomain.CurrentDomain.IsFinalizingForUnload() || Environment.HasShutdownStarted)
            {
                MessageBox.Show(error.ToString());
            }
            SysLog.Fatal(error.ToString());
        }
    }
}