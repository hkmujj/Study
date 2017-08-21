using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CommonUtil.Model;
using CommonUtil.Util;
using flash;
using General.FlashLoader.FlashView.Model;
using General.FlashLoader.Subsystem.Interface;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Project;

namespace General.FlashLoader.Subsystem.Adpter
{
    public class ProcessFlashInteractive : IFlashInteractive, IDisposable
    {
        private readonly FlashSender m_FlashSender;

        private FlashStartArg m_FlashStartArg;

        private readonly Process m_FlashWindProcess;

        private readonly SubsystemInitParam m_InitParam;

        public ProcessFlashInteractive(SubsystemInitParam initParam)
        {
            m_InitParam = initParam;

            m_FlashWindProcess = new Process()
            {
                StartInfo =
                    new ProcessStartInfo(Path.Combine(initParam.DataPackage.Config.SystemDicrectory.BaseDirectory,
                        "General.FlashLoader.FlashView.exe"))
                    {
                        Arguments = string.Format("\"{0}\"", GenerStartArg(initParam)),
                    },
            };

            try
            {
                m_FlashSender = new FlashSender(m_FlashStartArg.ServiceSenderName);
                m_FlashSender.OnReceive += FlashSenderOnOnReceive;
                m_FlashWindProcess.Start();
            }
            catch (Exception e)
            {
                AppLog.Error("Start flash process error.{0}", e);
            }
        }

        public void SetValue(FlashCommandType cmdType, string value)
        {
            m_FlashSender.Send(m_FlashStartArg.ClilentSenderName, cmdType.ToString(), value);
        }

        public event FlashDataEventHandler FlashDataRevceived;

        private void FlashSenderOnOnReceive(string cmd, string value)
        {
            FlashCommandType cmdType;
            if (Enum.TryParse(cmd, true, out cmdType))
            {
                OnFlashDataRevceived(cmdType, value);
            }
            else
            {
                AppLog.Debug("Can not parser flash command 【{0}】", cmd);
            }
        }

        private string GenerStartArg(SubsystemInitParam initParam)
        {
            var file = Path.Combine(initParam.DataPackage.Config.SystemDicrectory.BaseDirectory, "Temp\\HXD2StartArg.xml");
            m_FlashStartArg = new FlashStartArg()
            {
                AppName = initParam.AppConfig.AppName,
                ClilentSenderName = "hxd2",
                ServiceSenderName = "hxd2_Service",
                Bound = initParam.AppConfig.ActureFormConfig != null
                    ? new XmlRectangle()
                    {
                        X = initParam.AppConfig.ActureFormConfig.Rectangle.X,
                        Y = initParam.AppConfig.ActureFormConfig.Rectangle.Y,
                        Width = initParam.AppConfig.ActureFormConfig.Rectangle.Width,
                        Height = initParam.AppConfig.ActureFormConfig.Rectangle.Height,
                    }
                    : new XmlRectangle() { Width = 800, Height = 600 },
                FormBorderStyle =
                    initParam.DataPackage.Config.SystemConfig.StartModel == StartModel.Edit
                        ? FormBorderStyle.Sizable
                        : FormBorderStyle.None,
                TopMost =
                    initParam.AppConfig.ActureFormConfig != null &&
                    (initParam.DataPackage.Config.SystemConfig.StartModel == StartModel.Normal &&
                     initParam.AppConfig.ActureFormConfig.TopMost),

                SwfConfigFile =
                    Path.Combine(
                        Path.Combine(initParam.DataPackage.Config.SystemDicrectory.BaseDirectory,
                            initParam.AppConfig.AppName), "Flash\\FlashLoader.xml")
            };

            var dir = Path.GetDirectoryName(file);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            DataSerialization.SerializeToXmlFile(m_FlashStartArg, file);

            return file;
        }


        public void Dispose()
        {
            try
            {
                m_FlashWindProcess.CloseMainWindow();
                m_FlashWindProcess.Close();
            }
            catch (Exception e)
            {
                AppLog.Error("Close flash windonws error, {0}", e);
            }
        }

        protected virtual void OnFlashDataRevceived(FlashCommandType cmdtype, string value)
        {
            var handler = FlashDataRevceived;
            if (handler != null)
            {
                handler(cmdtype, value);
            }
        }
    }
}