using System.Collections.Generic;
using System.Drawing;

namespace CRH2MMI.CutState
{
    internal class CutResource
    {
        /// <summary>
        /// 输入BOOL量的对应关系 key: 行, key: 列(车号) value: InboolList的索引
        /// </summary>
        public static readonly Dictionary<CutInfo, Dictionary<int, int>> InBoolDic;

        private readonly RemovalState m_RemovalState;
        private Image[] m_Images;


        static CutResource()
        {
            InBoolDic = new Dictionary<CutInfo, Dictionary<int, int>>()
            {
                {
                    CutInfo.AccessEle, new Dictionary<int, int>
                    {
                        {3, 0},
                        {5, 1}
                    }
                },
                {
                    CutInfo.VCB, new Dictionary<int, int>()
                    {
                        {1, 2},
                        {5, 3},
                    }
                },
                {
                    CutInfo.MCar, new Dictionary<int, int>()
                    {
                        {1, 4},
                        {2, 5},
                        {5, 6},
                        {6, 7},
                    }
                },
                {
                    CutInfo.CondenseMachine, new Dictionary<int, int>()
                    {
                        {2, 8},
                        {4, 9},
                        {6, 10},
                    }
                },
                {
                    CutInfo.Emergency, new Dictionary<int, int>()
                    {
                        {0, 11},
                        {1, 12},
                        {2, 13},
                        {3, 14},
                        {4, 15},
                        {5, 16},
                        {6, 17},
                        {7, 18},
                    }
                },
                {
                    CutInfo.DoorState, new Dictionary<int, int>()
                    {
                        {0, 19},
                        {1, 20},
                        {2, 21},
                        {3, 22},
                        {4, 23},
                        {5, 24},
                        {6, 25},
                        {7, 26},
                    }
                },
                {
                    CutInfo.ParkB, new Dictionary<int, int>()
                    {
                        {0, 27},
                        {3, 28},
                        {4, 29},
                        {7, 30},
                    }
                },
                {
                    CutInfo.Vigilant, new Dictionary<int, int>()
                    {
                        {0, 31},
                        {7, 32},
                    }
                }
            };


        }

        public CutResource(RemovalState removalState)
        {
            m_RemovalState = removalState;
        }

        public bool GetState(CutInfo info, int trainNo)
        {
            return m_RemovalState.BoolList[m_RemovalState.UIObj.InBoolList[InBoolDic[info][trainNo]]];
        }



    }
}
