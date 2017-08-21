using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtil.Model;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.开关.Lighting
{
    public class LightItemSelectStrategy : SelectStrategyBase
    {

        public LightItemSelectStrategy(IList<SelectableLightViewItemDecorator> selectableCollection)
            : base(selectableCollection.Cast<ISelectable>().ToList().AsReadOnly())
        {
            if (GlobalParam.Instance.ProjectType == ProjectType.CRH380BL)
            {
                Selectables = new List<List<ISelectable>>
                {
                    new List<ISelectable> {selectableCollection[0]},
                    new List<ISelectable>(selectableCollection.Skip(1))
                };
            }
            else
            {
                if (selectableCollection.Count > 12)
                {
                    var lst = new List<ISelectable>(selectableCollection.Skip(2).Take(8));
                    lst.AddRange(selectableCollection.Skip(11).Take(8));
                    Selectables = new List<List<ISelectable>>
                    {
                                new List<ISelectable> { selectableCollection[0]},
                                new List<ISelectable> { selectableCollection[1],selectableCollection[10]},
                              lst
                            };
                }
                else
                {
                    Selectables = new List<List<ISelectable>>
                    {
                                new List<ISelectable> { selectableCollection[0]},
                                new List<ISelectable> { selectableCollection[1]},
                                new List<ISelectable>(selectableCollection.Skip(2))
                            };
                }

            }


            CurrentRow = -1;
            CurrentCol = -1;
        }


        public override void Select(Direction direction)
        {
            DeselectCurrent();
            //TODO tan add others   
            switch (direction)
            {
                case Direction.Up:
                    CurrentRow = Math.Min((CurrentRow - 1), Selectables.Count - 1);

                    CurrentRow = Math.Max(-1, CurrentRow);

                    if (CurrentRow < 0)
                    {
                        CurrentRow = -1;
                        CurrentCol = -1;
                        return;
                    }
                    //if (m_CurrentCol == -1)
                    //{
                    //    m_CurrentCol = 0;
                    //}
                    //if (m_CurrentCol >= m_Selectables[m_CurrentRow].Count)
                    //{
                    //    m_CurrentCol = m_Selectables[m_CurrentRow].Count - 1;
                    //}
                    CurrentCol = 0;
                    break;
                case Direction.Down:
                    if (CurrentRow != Selectables.Count - 1)
                    {
                        CurrentCol = 0;
                    }
                    CurrentRow = Math.Min((CurrentRow + 1), Selectables.Count - 1);
                    if (CurrentCol == -1)
                    {
                        CurrentCol = 0;
                    }
                    if (CurrentCol >= Selectables[CurrentRow].Count)
                    {
                        CurrentCol = Selectables[CurrentRow].Count - 1;
                    }
                    break;
                case Direction.Left:
                    if (CurrentRow == -1)
                    {
                        CurrentRow = 0;
                    }
                    CurrentCol = Math.Min(CurrentCol - 1, Selectables[CurrentRow].Count);
                    if (CurrentCol == -1)
                    {
                        CurrentCol = 0;
                    }
                    if (CurrentCol >= Selectables[CurrentRow].Count)
                    {
                        CurrentCol = Selectables[CurrentRow].Count - 1;
                    }
                    break;
                case Direction.Right:
                    if (CurrentRow == -1)
                    {
                        CurrentRow = 0;
                    }
                    CurrentCol = Math.Min(CurrentCol + 1, Selectables[CurrentRow].Count);
                    if (CurrentCol == -1)
                    {
                        CurrentCol = 0;
                    }
                    if (CurrentCol >= Selectables[CurrentRow].Count)
                    {
                        CurrentCol = Selectables[CurrentRow].Count - 1;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction");
            }
            SelectCurrent();
        }
    }
}