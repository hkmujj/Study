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
    /// ���Ͻṹ��
    /// </summary>
    public struct DefaultInfo
    {
        /// <summary>
        /// ����ͼ������
        /// </summary>
        public string picType;

        /// <summary>
        /// ���ϴ���
        /// </summary>
        public string code;

        /// <summary>
        /// ���Ͽ�ʼʱ��
        /// </summary>
        public string StartTime;

        /// <summary>
        /// ���Ͻ���ʱ��
        /// </summary>
        public string OverTime;

        /// <summary>
        /// ��������
        /// </summary>
        public string fauleName;

        /// <summary>
        /// ���Ͻ������
        /// </summary>
        public string solveName;

        /// <summary>
        /// ���Ͻ��������ͼƬ
        /// </summary>
        public Image solvePic;

        /// <summary>
        /// ����ȷ��λ
        /// </summary>
        public int sendCmd;

        /// <summary>
        /// �����Ƿ����
        /// </summary>
        public bool eventOver;

        /// <summary>
        /// �Ƿ��Ѱ�ȷ��
        /// </summary>
        public bool isReceiveCmd;
    }

    /// <summary>
    /// ������
    /// </summary>
    public class DefaultItemInfo : baseClass
    {
        /// <summary>
        /// ���Ͻ������
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
        /// ��������
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
                e.DrawString("����1A��", new Font("����", 12, FontStyle.Bold), brush, new Rectangle(640, 70, 100, 30), strFor);
            }
        }

        #region ::::::::::::::::::::::::::::::::: Fun :::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// ���ϵȼ���ӦͼƬ��ʾ
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
        /// ��¼��ʷ�¼����¼�������[δ��]
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
        /// ��¼ʱ�䣨�¼�������
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
        /// ��ʱ���
        /// �ж��Ƿ���ܵ�ȷ��
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
        /// У�鷽�����ж��Ƿ���Ӧ�Ĺ��Ͻ���
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
        /// ��������
        /// �Ź���ĵ�ǰ�����б���գ��������
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
        /// ���ϻ�ȡ
        /// </summary>
        public void GetDefault()
        {
            if (defValue.Length == oldDefValue.Length)
            {
                for (int index = 0; index < defValue.Length; index++)
                {
                    #region :::::::::::::::::::::::::::::: ���Ϸ��� :::::::::::::::::::::::::::::::::::::::
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
                            //���ϼ������й���
                            _gradeAllDefault.Add(defaultTmp);

                            //���ϼ���δ�ų����ϡ�δ���ݵȼ�����
                            _theNoOverDefault.Add(defaultTmp);

                            //���ϼ��뵱ǰ���ϡ�δ���ݵȼ�����
                            _theCurrentDefault.Add(defaultTmp);

                            DefaultSort();
                        }
                    }
                    #endregion

                    #region ::::::::::::::::::::::::::::::: ���Ͻ��� :::::::::::::::::::::::::::::::::::::
                    else if (!defValue[index] && oldDefValue[index])
                    {
                        if (!defValue[index] && oldDefValue[index])
                        {
                            if (_FaultList.ContainsKey(menmoryAddress[index]))
                            {
                                string[] tmp = _FaultList[menmoryAddress[index]];
                                defaultTmp.code = tmp[1];
                                //���Ͻ���ʱ����ӽ�ȥ
                                defaultTmp.OverTime = DateTime.Now.ToLongTimeString();
                                defaultTmp.isReceiveCmd = true;

                                AddEventOver(defaultTmp);

                                //�жϹ����Ƿ������Ȼ��ӵ�ǰ������ɾ��
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

                                //�жϹ����Ƿ������Ȼ������й������ҳ���Ӧ����
                                //�ѽ���ʱ�����ȥ
                                //ɾ����ǰ���ϲ��Ѵ��н���ʱ��Ĺ�����ӵ��Ǹ����ϵ�λ��
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

                //����ǰ���ϸ�����Ϊ0��Ҫ����Ӧλ�û���������
                if (_DefaultSort.Count != 0)
                    beDefault = true;
                else
                    beDefault = false;
            }
        }

        /// <summary>
        /// ��ȷ�Ϻ�
        /// ���ظ��߼���ǰ����ȷ��
        /// </summary>
        public void SendConfirmation()
        {
            //���͵�ǰ����ȷ��λ
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
        /// ���캯��
        /// ��г2������
        /// </summary>
        /// <param name="defBValue">������</param>
        /// <param name="oldDefBValue">��һ���ڵĹ�����</param>
        /// <param name="MemoryIndex">�������ڵ��ڴ��ַ</param>
        /// <param name="_FaultList">�����б�</param>
        /// <param name="Imgs">���ϲ�����ʾ��ͼƬ</param>
        /// <param name="levelImgs">���ϵȼ�ͼƬ</param>
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
        /// ���캯��
        /// </summary>
        /// <param name="defBValue">������</param>
        /// <param name="oldDefBValue">��һ���ڵĹ�����</param>
        /// <param name="MemoryIndex">�������ڵ��ڴ��ַ</param>
        /// <param name="_FaultList">�����б�</param>
        /// <param name="Imgs">���ϲ�����ʾ��ͼƬ</param>
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
        /// ���캯��
        /// </summary>
        /// <param name="defBValue">������</param>
        /// <param name="oldDefBValue">��һ���ڵĹ�����</param>
        /// <param name="MemoryIndex">�������ڵ��ڴ��ַ</param>
        /// <param name="_FaultList">�����б�</param>
        /// <param name="_Solution">���ı����ϲ�����ʾ</param>
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
        /// ��������
        /// ���ݹ��캯����ͬ����ͬ
        /// </summary>
        int defType = 0;

        /// <summary>
        /// ��ǰѭ������״̬
        /// </summary>
        bool[] defValue;

        /// <summary>
        /// ǰһѭ������״̬
        /// </summary>
        bool[] oldDefValue;

        /// <summary>
        /// �߼���
        /// </summary>
        int[] menmoryAddress;

        /// <summary>
        /// ���Ͻ������ͼƬ��
        /// </summary>
        public Image[] getImgs;

        /// <summary>
        /// ���ϵȼ�ͼƬ
        /// </summary>
        public Image[] levelImgs; 

        /// <summary>
        /// ����ʵ��
        /// </summary>
        DefaultInfo defaultTmp;

        /// <summary>
        /// ��ǰ�Ƿ���ڹ���
        /// </summary>
        public bool beDefault;

        /// <summary>
        /// ���ϼ�ֵ�б�
        /// </summary>
        SortedList<int, string[]> _FaultList = new SortedList<int, string[]>();

        /// <summary>
        /// ���ϲ�����ʾ
        /// </summary>
        SortedList<int, string[]> _Soultion = new SortedList<int, string[]>();

        /// <summary>
        /// ��ȷ�Ϻ��͹��ϱ��
        /// </summary>
        List<int> _SendConfNumb = new List<int>();

        /// <summary>
        /// ���й���
        /// </summary>
        public List<DefaultInfo> _gradeAllDefault = new List<DefaultInfo>();

        /// <summary>
        /// δ�ų�����
        /// </summary>
        public List<DefaultInfo> _theNoOverDefault = new List<DefaultInfo>();

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public List<DefaultInfo> _theCurrentDefault = new List<DefaultInfo>();

        /// <summary>
        /// δ�ų�1������
        /// </summary>
        public List<DefaultInfo> _theNoOverDefault1 = new List<DefaultInfo>();

        /// <summary>
        /// δ�ų�2������
        /// </summary>
        public List<DefaultInfo> _theNoOverDefault2 = new List<DefaultInfo>();

        /// <summary>
        /// δ�ų�3������
        /// </summary>
        public List<DefaultInfo> _theNoOverDefault3 = new List<DefaultInfo>();

        /// <summary>
        /// ������ĵ�ǰ����
        /// </summary>
        public List<DefaultInfo> _DefaultSort = new List<DefaultInfo>();
        #endregion
    }
}
