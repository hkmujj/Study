using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urban.NanJing.NingTian.DDU.Common
{
    /// <summary>
    /// 功能描述：视图状态枚举
    /// </summary>
    public enum ViewState
    {
        黑屏 = 0,

        运行 = 1,
        盘路信息 = 101,
        故障 = 102,

        车辆状态 = 2,

        空调_模式选择 = 3,

        网络通讯 = 4,

        PIS = 5,
        站点 = 501,
        紧急广播 = 502,

        帮助 = 6,
        运行帮助 = 601,
        车辆帮助 = 602,
        空调帮助 = 603,
        通讯帮助 = 604,

        检修 = 7,
        登陆 = 701,
        时间设置 = 702,
        密码设置 = 703,
        轮径设置 = 704,
        加速度测试 = 705,
        测试 = 706,
        数据记录 = 707,
        USB = 709,
        维护指南 = 710,
        端口数据 = 711,
        版本 = 712,
        IO = 713
    }
}
