using System;
using CommonUtil.Model;
using Motor.HMI.CRH5G.Staus;
using Motor.HMI.CRH5G.底层共用;
using Motor.HMI.CRH5G.底层共用.RectView;

namespace Motor.HMI.CRH5G.指令
{
    internal class SelectableRectMoveStrategy
    {
        public int SelectedRectId { get { return CurrentSelectedRow * ColoumnCount + CurrentSelectedColoumn; } }

        private readonly Command m_Command;

        private bool CarCountIs16
        {
            get
            {
                return m_Command.CurrenTrainInside == TrainInside.Inside16 ||
                       m_Command.CurrenTrainInside == TrainInside.Normal16;
            }
        }

        public int CurrentSelectedRow { private set; get; }

        public int CurrentSelectedColoumn { private set; get; }

        private ShowStyle CurrentShowStyle
        {
            get
            {
                if (CarCountIs16)
                {
                    return m_Command.CurrentMarshallingType == MarshallingType.DoubleMarshalling
                        ? ShowStyle.Full16
                        : ShowStyle.Full8;
                }
                if (m_Command.CurrentMarshallingType == MarshallingType.DoubleMarshalling)
                {
                    if (m_Command.CurrenTrainInside != TrainInside.Normal8)
                    {
                        return ShowStyle.Half16Right;
                    }
                    else
                    {
                        return ShowStyle.Half16Left;
                    }
                }
                else
                {
                    return ShowStyle.Full8;
                }
            }
        }

        private int StartColoumnIndex
        {
            get
            {
                switch (CurrentShowStyle)
                {
                    case ShowStyle.Full16:
                        
                    case ShowStyle.Full8:
                        
                    case ShowStyle.Half16Left:
                        return 0;
                    case ShowStyle.Half16Right:
                        return 8;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private int LastCouumnIndex
        {
            get
            {
                switch (CurrentShowStyle)
                {
                  
                    case ShowStyle.Full8:
                    case ShowStyle.Half16Left:
                        return 7;
                    case ShowStyle.Half16Right:
                  case ShowStyle.Full16:
                        return 15;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private int CarCount
        {
            get { return CarCountIs16 ? 16 : 8; }
        }

        private int ColoumnCount
        {
            get
            {
                if (CurrentShowStyle == ShowStyle.Full8)
                {
                    return 8;
                }
                return 16;
            }
        }
        private readonly int m_MaxRowIndex;

        public SelectableRectMoveStrategy(Command command, int maxRowCount)
        {
            m_Command = command;
            m_MaxRowIndex = maxRowCount - 1;
        }

        public void Reset()
        {
            CurrentSelectedColoumn = StartColoumnIndex;
            CurrentSelectedRow = 0;
        }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (CurrentSelectedRow > 0)
                    {
                        --CurrentSelectedRow;
                    }
                    break;
                case Direction.Down:
                    if (CurrentSelectedRow < m_MaxRowIndex)
                    {
                        ++CurrentSelectedRow;
                    }
                    break;
                case Direction.Left:
                    if (CurrentSelectedColoumn <= 0)
                    {
                        if (CurrentSelectedRow > 0)
                        {
                            --CurrentSelectedRow;
                            CurrentSelectedColoumn = LastCouumnIndex;
                        }
                    }
                    else
                    {
                        --CurrentSelectedColoumn;
                    }
                    break;
                case Direction.Right:
                    if (CurrentSelectedColoumn >= LastCouumnIndex)
                    {
                        if (CurrentSelectedRow < m_MaxRowIndex)
                        {
                            ++CurrentSelectedRow;
                            CurrentSelectedColoumn = StartColoumnIndex;
                        }
                    }
                    else
                    {
                        ++CurrentSelectedColoumn;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }

        }
    }
}