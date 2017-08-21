using System;
using System.ComponentModel.Composition;
using System.IO;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace MMITool.Addin.YdConfigCreater.ViewModel
{
    [Export]
    public class HelpViewModel : NotificationObject
    {
        [ImportingConstructor]
        public HelpViewModel(IApplicationService applicationService)
        {
            m_ApplicationService = applicationService;
            VersionInfo = new Lazy<string>(() =>
            {
                try
                {
                    return File.ReadAllText(Path.Combine(applicationService.AddinPath,
                        "Changes_MMITool.YdConfigCreater.txt"));
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            });
        }

        public Lazy<string> VersionInfo { get; private set; }

        private IApplicationService m_ApplicationService;
    }
}