using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using MMI.Facility.DataType;
using System.Threading;
using System.IO;

using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace KumM_TMS
{
    /// <summary>
    /// 故障结构体
    /// </summary>
    public struct DefaultInfo
    {
        /// <summary>
        /// 故障图标类型
        /// </summary>
        public string picType;

        /// <summary>
        /// 故障代码
        /// </summary>
        public string code;

        /// <summary>
        /// 故障开始时间
        /// </summary>
        public string StartTime;

        /// <summary>
        /// 故障结束时间
        /// </summary>
        public string OverTime;

        /// <summary>
        /// 故障名称
        /// </summary>
        public string fauleName;

        /// <summary>
        /// 故障解决方法
        /// </summary>
        public string solveName;

        /// <summary>
        /// 故障解决方法的图片
        /// </summary>
        public Image solvePic;

        /// <summary>
        /// 发送确认位
        /// </summary>
        public int sendCmd;

        /// <summary>
        /// 故障是否结束
        /// </summary>
        public bool eventOver;

        /// <summary>
        /// 是否已按确认
        /// </summary>
        public bool isReceiveCmd;
    }

    /// <summary>
    /// 故障类
    /// </summary>
    public class DefaultItemInfo : baseClass
    {
        /// <summary>
        /// 故障解决方法
        /// </summary>
        /// <param name="e"></param>
        public void DrawTest(Graphics e, Rectangle rect)
        {
            if (_DefaultSort.Count > 0)
            {
                if (_DefaultSort[0].solvePic != null)
                {
                    e.DrawImage(_DefaultSort[0].solvePic, rect);
                }
            }
        }

        /// <summary>
        /// 故障名称
        /// </summary>
        /// <param name="e"></param>
        /// <param name="rect"></param>
        public void DrawTitle(Graphics e, Rectangle rectPic, Font font,
            Brush brush, Rectangle rect, StringFormat strFor)
        {
            if (_DefaultSort.Count > 0)
            {
                e.DrawImage(DefaultLevelPic(_DefaultSort[0].picType), rectPic);
                e.DrawString(_DefaultSort[0].fauleName, font, brush, rect, strFor);
                e.DrawString("机车1A端", new Font("宋体", 12, FontStyle.Bold), brush, new Rectangle(640, 70, 100, 30), strFor);
            }
        }

        #region ::::::::::::::::::::::::::::::::: Fun :::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 故障等级相应图片显示
        /// </summary>
        /// <param name="Level"></param>
        /// <returns></returns>
        private Image DefaultLevelPic(string Level)
        {
            switch (Level)
            {
                case "1":
                    return levelImgs[0];
                case "2":
                    return levelImgs[1];
                case "3":
                    return levelImgs[2];
                default:
                    return levelImgs[2];
            }
        }

        /// <summary>
        /// 记录历史事件（事件发生）[未用]
        /// </summary>
        /// <param name="item"></param>
        private void AddEvent(DefaultInfo item)
        {
            if (_gradeAllDefault.Count <= 0) _gradeAllDefault.Add(item);
            else
            {
                bool flag = false;
                for (int index = _gradeAllDefault.Count - 1; index >= 0; index--)
                {
                    if (_gradeAllDefault[index].code == item.code)
                    {
                        flag = true;
                        if (_gradeAllDefault[index].eventOver)
                        {
                            _gradeAllDefault.Add(item);
                        }
                        return;
                    }
                }
                if (!flag)
                {
                    _gradeAllDefault.Add(item);
                }
            }
        }

        /// <summary>
        /// 记录时间（事件结束）
        /// </summary>
        /// <param name="item"></param>
        private void AddEventOver(DefaultInfo item)
        {
            if (_gradeAllDefault.Count < 0) return;
            for (int index = _gradeAllDefault.Count - 1; index >= 0; index--)
            {
                if (_gradeAllDefault[index].code == item.code && _gradeAllDefault[index].eventOver == false)
                {
                    DefaultInfo tmp = new DefaultInfo();
                    tmp = _gradeAllDefault[index];
                    tmp.OverTime = item.OverTime;
                    tmp.eventOver = true;
                    return;
                }
            }
        }

        /// <summary>
        /// 临时添加
        /// 判断是否接受到确认
        /// </summary>
        /// <param name="nItemInfo"></param>
        /// <param name="nCode"></param>
        /// <returns></returns>
        private int ReceiveTheItem(List<DefaultInfo> nItemInfo, string nCode)
        {
            for (int tmpIndex = 0; tmpIndex < nItemInfo.Count; tmpIndex++)
            {
                if (nItemInfo[tmpIndex].code.Trim() == nCode.Trim())
                {
                    return tmpIndex;
                }
            }
            return -1;
        }


        /// <summary>
        /// 校验方法，判断是否相应的故障结束
        /// </summary>
        /// <param name="nItemInfo"></param>
        /// <param name="nCode"></param>
        /// <returns></returns>
        private int HasTheItem(List<DefaultInfo> nItemInfo, string nCode)
        {
            for (int tmpIndex = 0; tmpIndex < nItemInfo.Count; tmpIndex++)
            {
                if (nItemInfo[tmpIndex].code.Trim() == nCode.Trim() && nItemInfo[tmpIndex].OverTime == null)
                {
                    return tmpIndex;
                }
            }
            return -1;
        }

        /// <summary>
        /// 故障排序
        /// 排过序的当前故障列表清空，重新添加
        /// </summary>
        private void DefaultSort()
        {
            _theNoOverDefault1.Clear();
            _theNoOverDefault2.Clear();
            _theNoOverDefault3.Clear();
            foreach (DefaultInfo def in _theCurrentDefault)
            {
                if (!def.isReceiveCmd && def.picType == "1")
                {
                    _theNoOverDefault1.Add(def);
                }
                if (!def.isReceiveCmd && def.picType == "2")
                {
                    _theNoOverDefault2.Add(def);
                }
                if (!def.isReceiveCmd && def.picType == "3")
                {
                    _theNoOverDefault3.Add(def);
                }
            }

            _DefaultSort.Clear();
            if (_theNoOverDefault1.Count > 0)
            {
                for (int i = 0; i < _theNoOverDefault1.Count; i++)
                {
                    _DefaultSort.Add(_theNoOverDefault1[i]);
                }
            }
            if (_theNoOverDefault2.Count > 0)
            {
                for (int i = 0; i < _theNoOverDefault2.Count; i++)
                {
                    _DefaultSort.Add(_theNoOverDefault2[i]);
                }
            }
            if (_theNoOverDefault3.Count > 0)
            {
                for (int i = 0; i < _theNoOverDefault3.Count; i++)
                {
                    _DefaultSort.Add(_theNoOverDefault3[i]);
                }
            }
        }

        /// <summary>
        /// 故障获取
        /// </summary>
        public void GetDefault()
        {
            if (defValue.Length == oldDefValue.Length)
            {
                for (int index = 0; index < defValue.Length; index++)
                {
                    #region :::::::::::::::::::::::::::::: 故障发生 :::::::::::::::::::::::::::::::::::::::
                    if (defValue[index] && !oldDefValue[index])
                    {
                        if (_FaultList.ContainsKey(menmoryAddress[index]))
                        {
                            defaultTmp = new DefaultInfo();
                            string[] tmp = _FaultList[menmoryAddress[index]];
                            if (defType == 1)
                            {
                                defaultTmp.code = tmp[1];
                                defaultTmp.picType = tmp[2];
                                defaultTmp.fauleName = tmp[3];
                                defaultTmp.solveName = tmp[4];
                                defaultTmp.solvePic = getImgs[index];
                                defaultTmp.eventOver = false;
                                defaultTmp.isReceiveCmd = false;
                                defaultTmp.StartTime = DateTime.Now.ToLongTimeString();
                                defaultTmp.sendCmd = Convert.ToInt32(tmp[5]);
                            }
                            else if (defType == 2)
                            {
                            }
                            else if (defType == 3)
                            {
                            }
                            //故障加入所有故障
                            _gradeAllDefault.Add(defaultTmp);

                            //故障加入未排除故障、未根据等级排序
                            _theNoOverDefault.Add(defaultTmp);

                            //故障加入当前故障、未根据等级排序
                            _theCurrentDefault.Add(defaultTmp);

                            DefaultSort();
                        }
                    }
                    #endregion

                    #region ::::::::::::::::::::::::::::::: 故障结束 :::::::::::::::::::::::::::::::::::::
                    else if (!defValue[index] && oldDefValue[index])
                    {
                        if (!defValue[index] && oldDefValue[index])
                        {
                            if (_FaultList.ContainsKey(menmoryAddress[index]))
                            {
                                string[] tmp = _FaultList[menmoryAddress[index]];
                                defaultTmp.code = tmp[1];
                                //故障结束时间添加进去
                                defaultTmp.OverTime = DateTime.Now.ToLongTimeString();
                                defaultTmp.isReceiveCmd = true;

                                AddEventOver(defaultTmp);

                                //判断故障是否结束，然后从当前故障中删掉
                                int tmpIndex = HasTheItem(_theNoOverDefault, defaultTmp.code);
                                if (tmpIndex > -1)
                                {
                                    _theNoOverDefault.RemoveAt(tmpIndex);
                                }
                                tmpIndex = HasTheItem(_theCurrentDefault, defaultTmp.code);
                                if (tmpIndex > -1)
                                {
                                    _theCurrentDefault.RemoveAt(tmpIndex);
                                }

                                //判断故障是否结束，然后从所有故障中找出相应故障
                                //把结束时间填进去
                                //删除当前故障并把带有结束时间的故障添加到那个故障的位置
                                tmpIndex = HasTheItem(_gradeAllDefault, defaultTmp.code);
                                if (tmpIndex > -1)
                                {
                                    DefaultInfo tmpItemInfo = _gradeAllDefault[tmpIndex];
                                    tmpItemInfo.OverTime = defaultTmp.OverTime;
                                    tmpItemInfo.isReceiveCmd = true;
                                    tmpItemInfo.eventOver = true;
                                    _gradeAllDefault.RemoveAt(tmpIndex);
                                    _gradeAllDefault.Insert(tmpIndex, tmpItemInfo);
                                }

                                DefaultSort();                         
                            }
                        }
                    }
                    #endregion

                    DefaultSort();
                }

                //有序当前故障个数不为0则要在相应位置画故障名称
                if (_DefaultSort.Count != 0)
                    beDefault = true;
                else
                    beDefault = false;
            }
        }

        /// <summary>
        /// 按确认后
        /// 返回给逻辑当前故障确认
        /// </summary>
        public void SendConfirmation()
        {
            //发送当前按得确认位
            if (_DefaultSort.Count != 0)
            {
                int tmpIndex = ReceiveTheItem(_gradeAllDefault, _DefaultSort[0].code);
                if (tmpIndex > -1)
                {
                    DefaultInfo tmpItemInfo = _gradeAllDefault[tmpIndex];
                    tmpItemInfo.isReceiveCmd = true;
                    _gradeAllDefault.RemoveAt(tmpIndex);
                    _gradeAllDefault.Insert(tmpIndex, tmpItemInfo);
                }

                tmpIndex = ReceiveTheItem(_theNoOverDefault, _DefaultSort[0].code);
                if (tmpIndex > -1)
                {
                    DefaultInfo tmpItemInfo = _theNoOverDefault[tmpIndex];
                    tmpItemInfo.isReceiveCmd = true;
                    _theNoOverDefault.RemoveAt(tmpIndex);
                    _theNoOverDefault.Insert(tmpIndex, tmpItemInfo);
                }

                tmpIndex = ReceiveTheItem(_theCurrentDefault, _DefaultSort[0].code);
                if (tmpIndex > -1)
                {
                    DefaultInfo tmpItemInfo = _theCurrentDefault[tmpIndex];
                    tmpItemInfo.isReceiveCmd = true;
                    _theCurrentDefault.RemoveAt(tmpIndex);
                    DefaultSort();
                }
            }
        }
        #endregion

        #region ::::::::::::::::::::::::::::::::: init ::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// 构造函数
        /// 和谐2机车用
        /// </summary>
        /// <param name="defBValue">故障量</param>
        /// <param name="oldDefBValue">上一周期的故障量</param>
        /// <param name="MemoryIndex">故障所在的内存地址</param>
        /// <param name="_FaultList">故障列表</param>
        /// <param name="Imgs">故障操作提示用图片</param>
        /// <param name="levelImgs">故障等级图片</param>
        public DefaultItemInfo(ref bool[] defBValue, ref bool[] oldDefBValue,
            ref int[] MemoryIndex, SortedList<int, string[]> _FaultList,
            List<int> SendFaultNumb, Image[] Imgs, Image[] levelImgs)
        {
            defValue = defBValue;
            oldDefValue = oldDefBValue;
            this.menmoryAddress = MemoryIndex;
            this._FaultList = _FaultList;
            _SendConfNumb = SendFaultNumb;
            getImgs = Imgs;
            this.levelImgs = levelImgs;
            defType = 1;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="defBValue">故障量</param>
        /// <param name="oldDefBValue">上一周期的故障量</param>
        /// <param name="MemoryIndex">故障所在的内存地址</param>
        /// <param name="_FaultList">故障列表</param>
        /// <param name="Imgs">故障操作提示用图片</param>
        public DefaultItemInfo(ref bool[] defBValue, ref bool[] oldDefBValue,
            ref int[] MemoryIndex, SortedList<int, string[]> _FaultList,
            Image[] Imgs)
        {
            defValue = defBValue;
            oldDefValue = oldDefBValue;
            this.menmoryAddress = MemoryIndex;
            this._FaultList = _FaultList;
            getImgs = Imgs;
            defType = 2;

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="defBValue">故障亮</param>
        /// <param name="oldDefBValue">上一周期的故障量</param>
        /// <param name="MemoryIndex">故障所在的内存地址</param>
        /// <param name="_FaultList">故障列表</param>
        /// <param name="_Solution">读文本故障操作提示</param>
        public DefaultItemInfo(ref bool[] defBValue, ref bool[] oldDefBValue,
            ref int[] MemoryIndex, SortedList<int, string[]> _FaultList,
            SortedList<int, string[]> _Solution)
        {
            defValue = defBValue;
            oldDefValue = oldDefBValue;
            this.menmoryAddress = MemoryIndex;
            this._FaultList = _FaultList;
            this._Soultion = _Solution;
            defType = 3;
        }

        /// <summary>
        /// 故障类型
        /// 根据构造函数不同而不同
        /// </summary>
        int defType = 0;

        /// <summary>
        /// 当前循环故障状态
        /// </summary>
        bool[] defValue;

        /// <summary>
        /// 前一循环故障状态
        /// </summary>
        bool[] oldDefValue;

        /// <summary>
        /// 逻辑号
        /// </summary>
        int[] menmoryAddress;

        /// <summary>
        /// 故障解决方法图片集
        /// </summary>
        public Image[] getImgs;

        /// <summary>
        /// 故障等级图片
        /// </summary>
        public Image[] levelImgs; 

        /// <summary>
        /// 故障实例
        /// </summary>
        DefaultInfo defaultTmp;

        /// <summary>
        /// 当前是否存在故障
        /// </summary>
        public bool beDefault;

        /// <summary>
        /// 故障键值列表
        /// </summary>
        SortedList<int, string[]> _FaultList = new SortedList<int, string[]>();

        /// <summary>
        /// 故障操作提示
        /// </summary>
        SortedList<int, string[]> _Soultion = new SortedList<int, string[]>();

        /// <summary>
        /// 按确认后发送故障编号
        /// </summary>
        List<int> _SendConfNumb = new List<int>();

        /// <summary>
        /// 所有故障
        /// </summary>
        public List<DefaultInfo> _gradeAllDefault = new List<DefaultInfo>();

        /// <summary>
        /// 未排除故障
        /// </summary>
        public List<DefaultInfo> _theNoOverDefault = new List<DefaultInfo>();

        /// <summary>
        /// 当前故障
        /// </summary>
        public List<DefaultInfo> _theCurrentDefault = new List<DefaultInfo>();

        /// <summary>
        /// 未排除1级故障
        /// </summary>
        public List<DefaultInfo> _theNoOverDefault1 = new List<DefaultInfo>();

        /// <summary>
        /// 未排除2级故障
        /// </summary>
        public List<DefaultInfo> _theNoOverDefault2 = new List<DefaultInfo>();

        /// <summary>
        /// 未排除3级故障
        /// </summary>
        public List<DefaultInfo> _theNoOverDefault3 = new List<DefaultInfo>();

        /// <summary>
        /// 排完序的当前故障
        /// </summary>
        public List<DefaultInfo> _DefaultSort = new List<DefaultInfo>();
        #endregion
    }
}
