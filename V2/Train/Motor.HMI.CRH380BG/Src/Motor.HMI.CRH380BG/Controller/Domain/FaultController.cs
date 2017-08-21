using MMI.Facility.WPFInfrastructure.Interfaces;
using Motor.HMI.CRH380BG.Model.Interface;
using Motor.HMI.CRH380BG.ViewModel.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Motor.HMI.CRH380BG.Model.ConfigModel;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Mvvm.Native;
using Motor.HMI.CRH380BG.Model.Common;
using Motor.HMI.CRH380BG.Model.Domain.Fault.Detail;
using Motor.HMI.CRH380BG.Model.Domain.Constant;

namespace Motor.HMI.CRH380BG.Controller.Domain
{
    [Export]
    [Export(typeof(IResetSupport))]
    public class FaultController : ControllerBase<FaultViewModel>, IResetSupport
    {
        [ImportingConstructor]
        public FaultController()
        {

        }

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ViewModel.Model.AllPagedItems.CurrentListIndex = 0;
            //AddItem(new NotifyInfoConfig(1, "fatype1", "faultcode1", "faultLevel1", "fname1", "report1121", "trun 1", "trainstop1", "at1"));
            if (ViewModel.Model.AllItems.Any())
            {
                ViewModel.Model.CurrentSelectedItem = ViewModel.Model.AllItems[0];
            }
            UpdateItems();
        }

        public void AddItem(NotifyInfoConfig itemConfig)
        {
            foreach (var config in ViewModel.Model.AllItems)
            {
                if (config.InfoConfig.Index == itemConfig.Index)
                {
                    return;
                }
            }
            ViewModel.Model.AllItems.Add(new FaultItem(itemConfig, DateTime.Now));
            int count = ViewModel.Model.AllItems.Count;
            ViewModel.Model.CurrentFlickingFault= ViewModel.Model.AllItems[count-1];

            if (ViewModel.Model.AllItems.Count==1)
            {
                ViewModel.Model.CurrentSelectedItem = ViewModel.Model.AllItems[0];
            }
            UpdateItems();
        }

        public void ResetItem(NotifyInfoConfig itemConfig)
        {
            bool x;
            bool t=true;
            int z = ViewModel.Model.AllPagedItems.CurrentListIndex;
            if(ViewModel.Model.AllItems.Count>1)
            { 
            if (ViewModel.Model.CurrentSelectedItem== ViewModel.Model.AllItems[z])
            {
                t = true;
            }
            else
            {
                t = false;
            }
            }


            var allPageItems = ViewModel.Model.AllPagedItems;
            foreach (var it in ViewModel.Model.AllItems.Where(w => w.InfoConfig.Index == itemConfig.Index))
            {
                x = ViewModel.Model.AllItems.Remove(it);
                if (x)
                {
                    break;
                }
            }
            UpdateItems();
            if (ViewModel.Model.AllItems.Count == ViewModel.Model.AllPagedItems.CurrentListIndex)
            {
                ViewModel.Model.AllPagedItems.CurrentListIndex=0;
                allPageItems.ItemsIndex = 0;
            }
             int y = ViewModel.Model.AllItems.Count;
            if (y == 0)
            {
                ViewModel.Model.CurrentFlickingFault = null;
            }
            else
            {
                ViewModel.Model.CurrentFlickingFault = ViewModel.Model.AllItems[y - 1];
            }

            //count+1,Index=select CurrentSelectedItem==[+1]
            //z是删除后的AllPagedItems的index,y是删除后的AllItems的Count
             z = ViewModel.Model.AllPagedItems.CurrentListIndex;
            if (z == y)
            {
                if (ViewModel.Model.AllItems.Count==0)
                {
                    ViewModel.Model.CurrentSelectedItem = null;
                }
                else
                {
                    ViewModel.Model.AllPagedItems.CurrentListIndex = 0;
                    allPageItems.ItemsIndex = 0;
                    ViewModel.Model.CurrentSelectedItem = ViewModel.Model.AllItems[0];
                }
                
            }
            else
            {
                ViewModel.Model.CurrentSelectedItem = ViewModel.Model.AllItems[z];
            }

            if (t)
            {
                if (ViewModel.Model.AllItems.Count == 0)
                {
                    ViewModel.Model.CurrentSelectedItem = null;
                   
                }
                else
                {
                    ViewModel.Model.CurrentSelectedItem = ViewModel.Model.AllItems[z];
                    allPageItems.ItemsIndex = allPageItems.CurrentListIndex - allPageItems.SkipCount;
                }
                
            }
            
            UpdateItems();
            
            
        }

        //public void ResetItem(NotifyInfoConfig itemConfig)
        //{
        //    bool x;
         
        //    foreach (var it in ViewModel.Model.AllItems.Where(w => w.InfoConfig.Index == itemConfig.Index))
        //    {
        //        if (ViewModel.Model.AllPagedItems.ListItemCount > 0)
        //        {
        //            if (it.InfoConfig.Index==ViewModel.Model.CurrentSelectedItem.InfoConfig.Index)
        //           {

        //               ViewModel.Model.AllPagedItems.CurrentListIndex = (ViewModel.Model.AllPagedItems.CurrentListIndex + 1) % ViewModel.Model.AllPagedItems.ListItemCount;
        //               ViewModel.Model.AllPagedItems.ItemsIndex = (ViewModel.Model.AllPagedItems.ItemsIndex + 1) % ViewModel.Model.AllPagedItems.ItemNumber;
        //                ViewModel.Model.CurrentSelectedItem = null;
        //                x = ViewModel.Model.AllItems.Remove(it);
        //                UpdateItems();
        //                return;
        //            }
        //                x = ViewModel.Model.AllItems.Remove(it);
        //                UpdateItems();
        //                return;
        //        }
            
        //    UpdateItems();
        //    }
        //}

        public void GotoNext()
        {
            int x;
            var allPageItems = ViewModel.Model.AllPagedItems;

            if (allPageItems.CurrentListIndex >= (allPageItems.ListItemCount - 1))
            {
                allPageItems.CurrentListIndex = (allPageItems.ListItemCount - 1);
                allPageItems.ItemsIndex = allPageItems.CurrentListIndex - allPageItems.SkipCount;
                x = allPageItems.CurrentListIndex;
                ViewModel.Model.CurrentSelectedItem = ViewModel.Model.AllItems[x];
                UpdateItems();
                return;
            }

            ++allPageItems.CurrentListIndex;
          
            if (allPageItems.CurrentListIndex > (allPageItems.ItemNumber - 1))
            {
                ++allPageItems.SkipCount;
                UpdateItems();
            }
            allPageItems.ItemsIndex = allPageItems.CurrentListIndex - allPageItems.SkipCount;

            x = allPageItems.CurrentListIndex;
            ViewModel.Model.CurrentSelectedItem = ViewModel.Model.AllItems[x];
            UpdateItems();

        }

        public void GotoPre()
        {
            int x;
            var allPageItems = ViewModel.Model.AllPagedItems;
            if (allPageItems.CurrentListIndex <= 0)
            {
                allPageItems.CurrentListIndex = 0;
                allPageItems.SkipCount = 0;

                x = allPageItems.CurrentListIndex;
                ViewModel.Model.CurrentSelectedItem = ViewModel.Model.AllItems[x];

                UpdateItems();
                allPageItems.ItemsIndex = 0;
                return;
            }
            --allPageItems.CurrentListIndex;
            x = allPageItems.CurrentListIndex;
            ViewModel.Model.CurrentSelectedItem = ViewModel.Model.AllItems[x];
            if (allPageItems.CurrentListIndex >= (allPageItems.ItemNumber - 1))
            {
                if (allPageItems.SkipCount > 0)
                {
                    --allPageItems.SkipCount;
                    UpdateItems();
                    allPageItems.ItemsIndex = allPageItems.CurrentListIndex - allPageItems.SkipCount;
                }


            }
            else
            {
                UpdateItems();
                allPageItems.ItemsIndex = allPageItems.CurrentListIndex;
            }


        }

        public void Reset()
        {
            var model = ViewModel.Model;
            model.AllItems.Clear();
            UpdateItems();
        }

        private void UpdateItems()
        {

            var model = ViewModel.Model;
            model.AllPagedItems.Reset(model.AllItems);
        }







    }
}
