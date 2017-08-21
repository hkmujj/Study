using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;

namespace Engine.TCMS.HXD3C.Utils
{
    public static class BaseClassExtension
    {
        /// <summary>
        /// 返回上一界面
        /// </summary>
        /// <param name="obj"></param>
        public static void NavigateReturn(this baseClass obj)
        {
            GlobalParam.Instance.NavigetByReture = true;
            obj.append_postCmd(CmdType.ChangePage, GlobalParam.Instance.LastViewIDs.Last(), 0, 0);
            GlobalParam.Instance.LastViewIDs.Remove(GlobalParam.Instance.LastViewIDs.Last());
        }

        public static Image[] GetImages(this baseClass obj)
        {
            return obj.UIObj.ParaList.Select(s => Image.FromFile(Path.Combine(obj.RecPath, s))).ToArray();
        }
    }
}