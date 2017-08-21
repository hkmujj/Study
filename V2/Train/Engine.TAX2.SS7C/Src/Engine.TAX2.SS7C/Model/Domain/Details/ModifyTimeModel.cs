using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.WPFInfrastructure.View;

namespace Engine.TAX2.SS7C.Model.Domain.Details
{
    [Export]
    public class ModifyTimeModel : NotificationObject, ICaretTextModel
    {
        private int m_BindableCaretIndex;
        private string m_Text;
        private DateTime m_SettingTime;

        public static readonly char[] ConstantText = {'年', '月', '日', '时', '分', '秒'};

        /// <summary>
        /// </summary>
        public int BindableCaretIndex
        {
            get { return m_BindableCaretIndex; }
            set
            {
                if (value == m_BindableCaretIndex)
                {
                    return;
                }

                m_BindableCaretIndex = value;
                RaisePropertyChanged(() => BindableCaretIndex);
            }
        }

        public char CharOfIndex
        {
            get
            {
                if (BindableCaretIndex >= 1 && BindableCaretIndex <= Text.Length)
                {
                    return Text[BindableCaretIndex-1];
                }

                return char.MinValue;
            }
        }

        /// <summary>显示文本</summary>
        public string Text
        {
            get { return m_Text; }
            set
            {
                if (value == m_Text)
                {
                    return;
                }

                m_Text = value;
                RaisePropertyChanged(() => Text);
            }
        }

        public DateTime SettingTime
        {
            get { return m_SettingTime; }
            set
            {
                if (value.Equals(m_SettingTime))
                {
                    return;
                }

                m_SettingTime = value;
                Text = value.ToString("yyyy年MM月dd日HH时mm分ss秒");
                RaisePropertyChanged(() => SettingTime);
            }
        }
    }
}