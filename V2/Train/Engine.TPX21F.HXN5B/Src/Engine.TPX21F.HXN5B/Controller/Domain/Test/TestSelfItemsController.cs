using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using CommonUtil.Model;
using Engine.TPX21F.HXN5B.Model;
using Engine.TPX21F.HXN5B.Model.Domain.Constant;
using Engine.TPX21F.HXN5B.Model.Domain.Test.Detail;
using Engine.TPX21F.HXN5B.Model.Interface;
using Engine.TPX21F.HXN5B.ViewModel.Domain;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TPX21F.HXN5B.Controller.Domain.Test
{
    [Export]
    public class TestSelfItemsController : ControllerBase<TestSelfViewModel>, IResetSupport
    {
        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ViewModel.Model.Items =
                new Lazy<ObservableCollection<TestSelfItem>>(
                    () =>
                        new ObservableCollection<TestSelfItem>(
                            GlobalParam.Instance.SelfTestItemConfigs.Value.Select(TestSelfItemFactory.CreateItem))
                    );

            ViewModel.Model.GroupNames =
                new Lazy<List<string>>(
                    () =>
                        ViewModel.Model.Items.Value.GroupBy(g => g.ItemConfig.GroupName)
                            .Select(s => s.Key)
                            .ToList());
        }

        public void Select(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                case Direction.Down:
                    switch (ViewModel.Model.SelectedCondition)
                    {
                        case SelfTestCondition.Group:
                            SelectGroupName(direction);
                            break;
                        case SelfTestCondition.Item:
                            SelectTestItem(direction);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case Direction.Left:
                    if (ViewModel.Model.SelectedCondition != SelfTestCondition.Group)
                    {
                        ViewModel.Model.SelectedCondition = SelfTestCondition.Group;
                    }
                    break;
                case Direction.Right:
                    if (ViewModel.Model.SelectedCondition != SelfTestCondition.Item)
                    {
                        ViewModel.Model.SelectedCondition = SelfTestCondition.Item;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }


        private void SelectGroupName(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    ViewModel.Model.SelectedGroupName = ViewModel.Model.GroupNames.Value[(ViewModel.Model.GroupNames.Value.IndexOf(ViewModel.Model.SelectedGroupName) - 1 + ViewModel.Model.GroupNames.Value.Count)%ViewModel.Model.GroupNames.Value.Count];
                    break;
                case Direction.Down:
                    ViewModel.Model.SelectedGroupName = ViewModel.Model.GroupNames.Value[(ViewModel.Model.GroupNames.Value.IndexOf(ViewModel.Model.SelectedGroupName) + 1)%ViewModel.Model.GroupNames.Value.Count];
                    break;
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }

        private void SelectTestItem(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    ViewModel.Model.SelectedItem = ViewModel.Model.GroupItems[(ViewModel.Model.GroupItems.IndexOf(ViewModel.Model.SelectedItem) - 1 + ViewModel.Model.GroupItems.Count)%ViewModel.Model.GroupItems.Count];
                    break;
                case Direction.Down:
                    ViewModel.Model.SelectedItem = ViewModel.Model.GroupItems[(ViewModel.Model.GroupItems.IndexOf(ViewModel.Model.SelectedItem) + 1)%ViewModel.Model.GroupItems.Count];
                    break;
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }

        public void Reset()
        {
            ViewModel.Model.SelectedCondition = SelfTestCondition.Group;

            if (ViewModel.Model.Items.IsValueCreated)
            {
                ViewModel.Model.Reset();
            }
        }
    }
}