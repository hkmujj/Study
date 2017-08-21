using System.Collections.Generic;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Data;

namespace HXD1D
{
    public static class Record
    {
        private static List<int> goViewList = new List<int>();

        /// <summary>
        /// 保存下一次跳转后的视图ID
        /// </summary>
        /// <param name="target">对象</param>
        /// <param name="viewId">视图ID</param>
        public static void GoNextView(baseClass target, int viewId)
        {
            if (viewId == 1)
            {
                goViewList.Clear();
            }
            goViewList.Add(viewId);

            target.append_postCmd(CmdType.ChangePage, viewId, 0, 0);
        }

        /// <summary>
        /// 返回以前的视图
        /// </summary>
        /// <param name="target">对象</param>
        /// <param name="num">返回视图次数</param>
        public static void Back(baseClass target, int num)
        {
            if (num <= 0)
            {
                return;
            }
            target.append_postCmd(CmdType.ChangePage, goViewList[goViewList.Count - (num + 1)], 0, 0);
            RemoveListIndex(num);
        }
        /// <summary>
        /// 移除指定数目的ID
        /// </summary>
        /// <param name="num">数目</param>
        public static void RemoveListIndex(int num)
        {
            if (goViewList.Count < num)
            {
                return;
            }
            for (int i = 0; i < num; i++)
            {
                goViewList.Remove(goViewList[goViewList.Count - 1]);
            }
        }

    }

}
