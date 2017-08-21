using System;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class Drivemodel : TextBase
    {
        private DriveModel m_Model;

        public DriveModel Model
        {
            get { return m_Model; }
            set
            {
                if (m_Model == value)
                {
                    return;
                }
                m_Model = value;
                ChangeText();
            }
        }

        private void ChangeText()
        {
            switch (Model)
            {
                case DriveModel.None:
                    ChangeText("");
                    break;

                case DriveModel.ATO:
                    ChangeText("AM");
                    break;

                case DriveModel.Supervised:
                    ChangeText("SM");
                    break;

                case DriveModel.Restricted:
                    ChangeText("RM");
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Drivemodel()
        {
            InitializeComponent();
        }
    }
}