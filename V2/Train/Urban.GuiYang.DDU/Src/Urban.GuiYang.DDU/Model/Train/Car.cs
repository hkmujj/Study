using System;
using System.Diagnostics;
using Microsoft.Practices.Prism.ViewModel;
using Urban.GuiYang.DDU.Model.Train.CarPart;

namespace Urban.GuiYang.DDU.Model.Train
{
    [DebuggerDisplay("Name={Name}, Index={Index}")]
    public class Car : NotificationObject
    {
        private string m_Name;

        public Car(string name, int index)
        {
            Fire = new Fire() {Car = this};
            MainViewPage = new MainViewPage() {Car = this};
            Name = name;
            Index = index;
            Brake = new Brake();
            AirCondition = new AirConditionModel();
        }

        public int Index { get; private set; }

        public string Name
        {
            get { return m_Name; }
            private set
            {
                if (value == m_Name)
                {
                    return;
                }

                m_Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public DriverRoom DriverRoom { get; set; }

        public Door Door { get; set; }

        public PECU PECU { get; set; }

        public Fire Fire { get; private set; }

        public MainViewPage MainViewPage { get; private set; }

        public Pantograph Pantograph { get; set; }

        public Coupling Coupling { get; set; }

        public Lazy<TowPage> TowPage { get; set; }

        public Lazy<MainPageByPass1> MainPageByPass1 { get; set; }

        public Lazy<MainPageByPass2> MainPageByPass2 { get; set; }

        public Brake Brake { private set; get; }

        public Lazy<AssistPage> AssistPage { get; set; }

        public AirConditionModel AirCondition { get; set; }

    }
}