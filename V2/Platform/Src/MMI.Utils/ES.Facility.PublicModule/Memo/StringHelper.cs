using System.Collections.Generic;

namespace ES.Facility.PublicModule.Memo
{
    public class StringHelper
    {
        /// <summary>
        /// ��ȡָ��2���ַ�֮����ַ���Ϣ
        /// </summary>
        /// <param name="cSoure">ԭʼ�ַ�</param>
        /// <param name="cFirst">����ַ�</param>
        /// <param name="cEnd">�����ַ�</param>
        /// <returns></returns>
        public static bool findStringByKey(string cSoure, string cFirst, string cEnd, ref string outStr)
        {
            var beginIndex = 0;                 //��¼���λ��
            var endIndex = cSoure.Length - 1;   //��¼����λ��

            if (cFirst != string.Empty)
            {
                //������ַ�������� ������Ϊ��0λ��ʼ
                beginIndex = cSoure.IndexOf(cFirst);
                if (beginIndex < 0) return false;       //�����ڸ�����ַ�

                beginIndex += cFirst.Length;
                if (beginIndex > cSoure.Length - 1) return false;   //���ַ��Ѿ������һλ��
            }

            if (cEnd != string.Empty)
            {
                //�н����ַ�������� ������Ϊ��ĩβ
                endIndex = cSoure.IndexOf(cEnd, beginIndex);
                if (endIndex < 0) return false;             //�����ڸý����ַ�
            }

            if (beginIndex == endIndex)
            {
                //ͷ�ַ�����β�ַ�
                return false;
            }

            outStr = cSoure.Substring(beginIndex, endIndex - beginIndex);

            if (outStr != null && outStr != string.Empty) return true;

            return false;
        }

        /// <summary>
        /// ��key�ָ�����ݶ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cSoure"></param>
        /// <param name="outList"></param>
        /// <returns></returns>
        public static List<string> getValueBySpace(string cSoure, string cKey)
        {
            var outList = new List<string>();

            var tmpStr = cSoure.Trim();
            var tmpValue = string.Empty;

            var beginIndex = 0;
            var endIndex = tmpStr.Length - 1;           

            while (beginIndex < tmpStr.Length)
            {
                endIndex = tmpStr.IndexOf(cKey);

                if (endIndex < 0)
                {
                    //û�пո���ڣ������ַ���Ϊ��Ч�ַ�
                    outList.Add(tmpStr);
                    break;
                }

                //��ȡָ�����������
                tmpValue = tmpStr.Substring(beginIndex, endIndex);
                outList.Add(tmpValue);

                //��ȡʣ�����������
                tmpStr = tmpStr.Substring(endIndex);
                tmpStr = tmpStr.Trim();
                beginIndex = 0;
                endIndex = tmpStr.Length;

            }

            return outList;
        }
    }
}
