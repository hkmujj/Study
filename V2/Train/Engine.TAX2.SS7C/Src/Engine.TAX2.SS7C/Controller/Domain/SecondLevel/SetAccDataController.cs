using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using CommonUtil.Model;
using Engine.TAX2.SS7C.Model;
using Engine.TAX2.SS7C.Model.Domain.SecondLevel.Details;
using Engine.TAX2.SS7C.Model.Interface;
using Engine.TAX2.SS7C.ViewModel.Domain.SecondLevel;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TAX2.SS7C.Controller.Domain.SecondLevel
{
    [Export]
    public class SetAccDataController : ControllerBase<SetAccDataViewModel>, IResetSupport
    {
        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ViewModel.Model.AccDataItemCollection =
                new Lazy<ReadOnlyCollection<AccDataItem>>(
                    () =>
                        GlobalParam.Instance.SetAccDataItemConfigCollection.Value.Select(
                            s => new AccDataItem(s) {Value = DateTime.Now.Millisecond})
                            .ToList()
                            .AsReadOnly());
        }

        public void SelectFirst()
        {
            ViewModel.Model.CurrentSelectedAccDataItem = ViewModel.Model.AccDataItemCollection.Value.FirstOrDefault();
        }

        public void SelectNext()
        {
            var c = ViewModel.Model.AccDataItemCollection.Value;
            ViewModel.Model.CurrentSelectedAccDataItem =
                c[(c.IndexOf(ViewModel.Model.CurrentSelectedAccDataItem) + 1)%c.Count];
        }

        public void ApplyModify()
        {
            //TODO 

            ViewModel.Model.HasAnyModified = false;
            ViewModel.Model.IsSureModify = false;
        }

        public void MoveCaret(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                case Direction.Left:
                    if (ViewModel.Model.CurrentSelectedAccDataItem != null)
                    {
                    }
                    break;
                case Direction.Right:
                    if (ViewModel.Model.CurrentSelectedAccDataItem != null)
                    {
                    }
                    break;
                //default:
                //    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public void Reset()
        {
            foreach (var it in ViewModel.Model.AccDataItemCollection.Value)
            {
                it.Text = it.Value.ToString("0");
            }

            SelectFirst();

            ViewModel.Model.HasAnyModified = false;
            ViewModel.Model.IsSureModify = false;
        }

        public void DecreaseCurrentData()
        {
            // TODO 
            ViewModel.Model.HasAnyModified = true;
        }
    }
}