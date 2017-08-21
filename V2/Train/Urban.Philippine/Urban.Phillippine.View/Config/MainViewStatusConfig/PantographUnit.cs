using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Excel.Interface;
using Microsoft.Practices.Prism.ViewModel;
using Urban.Phillippine.View.Extend;
using Urban.Phillippine.View.Interface;
using Urban.Phillippine.View.Interface.Enum;

namespace Urban.Phillippine.View.Config.MainViewStatusConfig
{
    [ExcelLocation("MainviewStatus.xls", "Pantograph")]
    public class PantographUnit : NotificationObject, IUnit
    {
        private PantographStatus m_Status;
        private Visibility m_TrainFour;
        private Visibility m_TrainOne;
        private Visibility m_TrainThree;
        private Visibility m_TrainTwo;

        public PantographStatus Status
        {
            get { return m_Status; }
            set
            {
                if (value == m_Status)
                {
                    return;
                }
                m_Status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        public Visibility TrainOne
        {
            get { return m_TrainOne; }
            set
            {
                if (value == m_TrainOne)
                {
                    return;
                }
                m_TrainOne = value;
                RaisePropertyChanged(() => TrainOne);
            }
        }

        public Visibility TrainTwo
        {
            get { return m_TrainTwo; }
            set
            {
                if (value == m_TrainTwo)
                {
                    return;
                }
                m_TrainTwo = value;
                RaisePropertyChanged(() => TrainTwo);
            }
        }

        public Visibility TrainThree
        {
            get { return m_TrainThree; }
            set
            {
                if (value == m_TrainThree)
                {
                    return;
                }
                m_TrainThree = value;
                RaisePropertyChanged(() => TrainThree);
            }
        }

        public Visibility TrainFour
        {
            get { return m_TrainFour; }
            set
            {
                if (value == m_TrainFour)
                {
                    return;
                }
                m_TrainFour = value;
                RaisePropertyChanged(() => TrainFour);
            }
        }

        [ExcelField("Train")]
        public int Train { get; set; }

        [ExcelField("Down")]
        public string Down { get; set; }

        [ExcelField("Up")]
        public string Up { get; set; }

        public void SetValue(string name, string value)
        {
            switch (name)
            {
                case "Train":
                    Train = System.Convert.ToInt32(value);
                    break;

                case "Down":
                    Down = value;
                    break;

                case "Up":
                    Up = value;
                    break;
            }
        }

        public void Clear()
        {
            Status = default(PantographStatus);
        }

        public void Clear(object obj, Type type)
        {
        }

        public void Changed(IDictionary<int, bool> args, int? iPara = null)
        {
            var index1 = IndexConfigure.InBoolIndex[Down];
            var index2 = IndexConfigure.InBoolIndex[Up];
            if (args.Keys.Any(a => a == index1 || a == index2))
            {
                if (args.ContainsKey(index1) && args[index1])
                {
                    Status = PantographStatus.Down;
                }
                if (args.ContainsKey(index2) && args[index2])
                {
                    Status = PantographStatus.Up;
                }
            }
            TrainOne = (iPara >= 1).ConvertVisibility();
            TrainTwo = (iPara >= 2).ConvertVisibility();
            TrainThree = (iPara >= 3).ConvertVisibility();
            TrainFour = (iPara >= 4).ConvertVisibility();
        }
        
        public void Changed(IDictionary<int, float> args, int? ipara = null)
        {
        }
    }
}