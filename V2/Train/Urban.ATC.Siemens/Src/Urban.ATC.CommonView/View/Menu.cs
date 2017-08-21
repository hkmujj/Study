using System;
using Urban.ATC.CommonView.Model;
using Urban.ATC.CommonView.View;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.Common.Control
{
    public partial class Menu : TextBase
    {
        private MenuColorTyep m_Type;

        public MenuColorTyep Type
        {
            get { return m_Type; }
            set
            {
                if (m_Type == value)
                {
                    return;
                }
                m_Type = value;
                ChangeColor();
            }
        }

        private void ChangeColor()
        {
            switch (Type)
            {
                case MenuColorTyep.Active:
                    ChangeTextColor(GDICommon.LightGreyColor);
                    break;

                case MenuColorTyep.Invalid:
                    ChangeTextColor(GDICommon.DarkGreyColor);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Menu()
        {
            InitializeComponent();

            ChangeText("菜单\r\nMENU");

            ChangeTextColor(GDICommon.LightGreyColor);
        }
    }
}