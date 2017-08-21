using System;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class DoorMode : TextBase
    {
        private DoorModel m_Model;

        public DoorModel Model
        {
            get { return m_Model; }
            set
            {
                if (m_Model == value)
                {
                    return;
                }
                m_Model = value;
                ChangeString();
            }
        }

        private void ChangeString()
        {
            switch (Model)
            {
                case DoorModel.None:
                    ChangeText("");
                    break;

                case DoorModel.MM:
                    ChangeText("MM");
                    break;

                case DoorModel.AM:
                    ChangeText("AM");
                    break;

                case DoorModel.AA:
                    ChangeText("AA");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public DoorMode()
        {
            InitializeComponent();
        }
    }
}