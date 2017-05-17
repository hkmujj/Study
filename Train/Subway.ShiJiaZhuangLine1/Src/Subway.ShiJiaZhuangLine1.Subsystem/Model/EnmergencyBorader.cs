using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using MMI.Facility.Interface.Data;
using Subway.ShiJiaZhuangLine1.Interface;
using Subway.ShiJiaZhuangLine1.Interface.Model;
using Subway.ShiJiaZhuangLine1.Interface.Resouce;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class EnmergencyBorader : ViewModelBase, IEnmergencyBorader
    {
        private List<IBoradcast> m_DisplayBorder;
        private IBoradcast m_SelectBorder;

        public void ChangeBools(CommunicationDataChangedArgs<bool> args)
        {

        }

        private void ChangeCurrentPageMethod()
        {
            Parent.BoradercastMgr.ResetPage();
        }


        private void ClearDataMethod()
        {
            foreach (var boradcast in Parent.BoradercastMgr.AllData)
            {
                Parent.Dataserver.WriteService.ChangeBool(boradcast.Value.LogicNum, false);
            }

        }

        private void SentDataMethod()
        {
            if (SelectBorder != null)
            {
                ClearDataMethod();
                Parent.Dataserver.WriteService.ChangeBool(SelectBorder.LogicNum, true);
            }
        }

        private void GetCurrentMethod()
        {

            DisplayBorder = Parent.BoradercastMgr.GetCurrent().ToList();

            SelectBorder = DisplayBorder.FirstOrDefault();

        }

        private void LastPageMethod()
        {
            if (Parent.BoradercastMgr.AllData.Values.ToList().IndexOf(SelectBorder) == 0)
            {
                return;
            }

            if (DisplayBorder.FirstOrDefault(f => f.Number == SelectBorder.Number - 1) != default(IBoradcast))
            {
                SelectBorder = DisplayBorder.FirstOrDefault(f => f.Number == SelectBorder.Number - 1);
            }
            else
            {
                Parent.BoradercastMgr.LastPage();
                DisplayBorder = Parent.BoradercastMgr.GetCurrent().ToList();
                SelectBorder = DisplayBorder[DisplayBorder.Count - 1];

            }
        }

        private void NextPageMethod()
        {
            if (Parent.BoradercastMgr.AllData.Values.ToList().IndexOf(SelectBorder) == Parent.BoradercastMgr.AllData.Values.ToList().Count - 1)
            {
                return;
            }

            if (DisplayBorder.FirstOrDefault(f => f.Number == SelectBorder.Number + 1) != default(IBoradcast))
            {
                SelectBorder = DisplayBorder.FirstOrDefault(f => f.Number == SelectBorder.Number + 1);
            }
            else
            {
                Parent.BoradercastMgr.NextPage();
                DisplayBorder = Parent.BoradercastMgr.GetCurrent().ToList();
                SelectBorder = DisplayBorder[0];

            }
        }

        public void ChangeFloats(CommunicationDataChangedArgs<float> args)
        {

        }

        public List<IBoradcast> DisplayBorder
        {
            get { return m_DisplayBorder; }
            set
            {
                if (Equals(value, m_DisplayBorder))
                    return;
                m_DisplayBorder = value;
                RaisePropertyChanged(() => DisplayBorder);
            }
        }

        public IBoradcast SelectBorder
        {
            get { return m_SelectBorder; }
            set
            {
                if (Equals(value, m_SelectBorder))
                    return;
                m_SelectBorder = value;
                RaisePropertyChanged(() => SelectBorder);
            }
        }


        public ICommand NextPage { get; private set; }
        public ICommand LastPage { get; private set; }
        public ICommand GetCurrent { get; private set; }
        public ICommand SentData { get; private set; }
        public ICommand ClearData { get; private set; }
        public ICommand ChangeCurrentPage { get; private set; }




        public EnmergencyBorader(IMMI parent) : base(parent)
        {
            NextPage = new DelegateCommand(NextPageMethod);
            LastPage = new DelegateCommand(LastPageMethod);
            GetCurrent = new DelegateCommand(GetCurrentMethod);
            SentData = new DelegateCommand(SentDataMethod);
            ClearData = new DelegateCommand(ClearDataMethod);
            ChangeCurrentPage = new DelegateCommand(ChangeCurrentPageMethod);
        }
    }
}