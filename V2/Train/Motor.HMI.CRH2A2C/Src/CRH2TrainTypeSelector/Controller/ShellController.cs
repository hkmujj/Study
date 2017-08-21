using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using CommonUtil.Util;
using CRH2MMI.Config.ConfigModel;
using CRH2TrainTypeSelector.ViewModel;
using Microsoft.Practices.Prism.Commands;

namespace CRH2TrainTypeSelector.Controller
{
    [Export]
    public class ShellController
    {
        public ShellViewModel ViewModel { set; protected get; }

        public DelegateCommand SelectedTypeChanged { private set; get; }

        public DelegateCommand InitalizeCommand { private set; get; }

        private static readonly ReadOnlyCollection<string> m_AllFiles =
            EnumUtil.GetAllEnums<CRH2Type>().Select(s => string.Format("IndexDescriptionConfig.{0}.xml", s)).ToList().AsReadOnly();

        protected string ConfigPath { get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config"); } }

        protected string ConfigFile
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", CRH2Config.FileName); }
        }

        public ShellController()
        {
            InitalizeCommand = new DelegateCommand(OnInitalize);
            SelectedTypeChanged = new DelegateCommand(() => OnSelectedTypeChanged());
        }

        private void OnInitalize()
        {
            var config =
                    DataSerialization.DeserializeFromXmlFile<CRH2Config>(ConfigFile);

            if (config != null)
            {
                ViewModel.Model.SelectedCRH2Type = config.Type;

                if (GlobalParam.Instance.StartupEventArgs.Args.Any())
                {
                    try
                    {
                        var type =
                            (CRH2Type) Enum.Parse(typeof (CRH2Type), GlobalParam.Instance.StartupEventArgs.Args[0]);

                        if (ViewModel.Model.SelectedCRH2Type != type)
                        {
                            ViewModel.Model.SelectedCRH2Type = type;
                            OnSelectedTypeChanged(false);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        Application.Current.Shutdown();
                    }
                }
            }
        }

        private void OnSelectedTypeChanged(bool needMsg = true)
        {
            try
            {

                var config =
                    DataSerialization.DeserializeFromXmlFile<CRH2Config>(ConfigFile);

                if (config != null)
                {
                    config.Type = ViewModel.Model.SelectedCRH2Type;
                    DataSerialization.SerializeToXmlFile(config, ConfigFile);
                }
                else
                {
                    if (needMsg)
                    {
                        MessageBox.Show (Application.Current.MainWindow, "未找到CRH2Config.xml 或 解析失败。");
                    }
                }
                
                var files = Directory.GetFiles(ConfigPath, "IndexDescriptionConfig.*.xml*", SearchOption.TopDirectoryOnly);

                foreach (var xml in files.Where(w => Path.GetExtension(w) == ".xml" && m_AllFiles.Contains(Path.GetFileName(w)) ))
                {
                    File.Move(xml, xml + "~");
                }

                files = Directory.GetFiles(ConfigPath, "IndexDescriptionConfig.*.xml*", SearchOption.TopDirectoryOnly);

                var serchFile = string.Format("IndexDescriptionConfig.{0}.xml", ViewModel.Model.SelectedCRH2Type);
                var target = files.First(f => f.Contains(serchFile));

                var des = target.Replace(string.Format("{0}~", serchFile), serchFile);

                File.Move(target, des);

                if (needMsg)
                {
                    MessageBox.Show(Application.Current.MainWindow, string.Format("选择车型{0}成功", ViewModel.Model.SelectedCRH2Type));
                }
            }
            catch (Exception e)
            {
                if (needMsg)
                {
                    MessageBox.Show(Application.Current.MainWindow, e.ToString(), "选择车型失败", MessageBoxButton.OK);
                }
            }
        }
    }
}