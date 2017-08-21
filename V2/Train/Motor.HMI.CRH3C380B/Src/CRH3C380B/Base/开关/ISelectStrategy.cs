using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommonUtil.Model;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.开关
{
    public interface ISelectStrategy
    {
        ISelectable CurrentSelected { get; }

        void Select(Direction direction);
    }

    public abstract class SelectStrategyBase : ISelectStrategy
    {
        protected ReadOnlyCollection<ISelectable> SelectableCollection;

        protected List<List<ISelectable>> Selectables;

        protected int CurrentRow;
        protected int CurrentCol;

        protected SelectStrategyBase(ReadOnlyCollection<ISelectable> selectableCollection)
        {
            SelectableCollection = selectableCollection;
        }

        public ISelectable CurrentSelected
        {
            get
            {
                if (Selectables != null
                    && Selectables.Count > CurrentRow && CurrentRow != -1
                    && Selectables[CurrentRow].Count > CurrentCol && CurrentCol != -1)
                {
                    return Selectables[CurrentRow][CurrentCol];
                }

                return null;
            }
        }

        protected void DeselectCurrent()
        {
            if (CurrentSelected != null)
            {
                CurrentSelected.Selected = false;
            }
        }

        protected void SelectCurrent()
        {
            if (CurrentSelected != null)
            {
                CurrentSelected.Selected = true;
            }
        }

        public abstract void Select(Direction direction);
    }
}