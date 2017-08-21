using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MMI.Common.Msg.Interface.ButtonReactivation
{
    public interface IButtonReactivationMgr : IInfo<int, IButtonInfo>
    {
        event Action FaultButtonCoutChanged;
        event Action FaultButtonChange;
        IList<IButtonInfo> ButtonFaultInfo { get; }
        void ChangStatus(int para);
        void ChangStatus(IButtonInfo para);
        void ReActiva(int para);
        void Reactiva(IButtonInfo para);

    }

    public class ButtonReactivationMgr : IButtonReactivationMgr
    {
        public ButtonReactivationMgr()
        {
            AllData = new Dictionary<int, IButtonInfo>();
            ButtonFaultInfo = new List<IButtonInfo>();
        }
        public string Name { get; private set; }
        public Dictionary<int, IButtonInfo> AllData { get; private set; }
        public void Load(string file)
        {
            if (File.Exists(file))
            {
                Name = file;
                var allLine = File.ReadAllLines(file, Encoding.Default);
                foreach (var line in allLine.Skip(1))
                {
                    var slip = line.Split(';');
                    if (slip.Length == 4)
                    {
                        var tmp = new ButtonInfo(int.Parse(slip[0]), int.Parse(slip[1]), slip[2], slip[3]);
                        AllData.Add(tmp.Logic, tmp);
                    }
                }
            }
            else
            {
                throw new FileNotFoundException(file);
            }
        }

        public event Action FaultButtonCoutChanged;
        public event Action FaultButtonChange;
        public IList<IButtonInfo> ButtonFaultInfo { get; private set; }

        protected void FalutButton()
        {
            var hander = FaultButtonChange;
            if (hander != null)
            {
                hander();
            }
        }

        protected void FaultButtonCountChange()
        {
            var hander = FaultButtonCoutChanged;
            if (hander != null)
            {
                hander();
            }
        }
        public void ChangStatus(int para)
        {
            if (AllData.ContainsKey(para))
            {
                ChangStatus(AllData[para]);
            }
        }

        public void ChangStatus(IButtonInfo para)
        {
            para.Status = ButtonStatus.Fault;
            ButtonFaultInfo.Add(para);
            FalutButton();
            FaultButtonCountChange();
        }

        public void ReActiva(int para)
        {
            if (AllData.ContainsKey(para))
            {
                Reactiva(AllData[para]);
            }
        }

        public void Reactiva(IButtonInfo para)
        {
            para.Status = ButtonStatus.Normal;
            ButtonFaultInfo.Remove(para);
            FalutButton();
            FaultButtonCountChange();
        }
    }
}