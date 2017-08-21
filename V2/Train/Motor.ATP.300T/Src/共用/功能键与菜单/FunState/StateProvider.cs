using System.Collections.Generic;

namespace Motor.ATP._300T.����.���ܼ���˵�.FunState
{
    public class StateProvider
    {
        private readonly OpenFunBtnCTCS300T m_OpenFunBtnCTCS300T;

        public StateProvider(OpenFunBtnCTCS300T openFunBtnCTCS300T)
        {
            BtnStrDict = new Dictionary<int, string[]>();
            MenuValueArrDict = new Dictionary<int, MenuValue[]>();
            m_OpenFunBtnCTCS300T = openFunBtnCTCS300T;
        }

        /// <summary>
        /// ��ť���ݴʵ䣬����MenuID�仯
        /// </summary>
        public Dictionary<int, string[]> BtnStrDict { get; private set; }

        public Dictionary<int, MenuValue[]> MenuValueArrDict { get; private set; }

        public void FixModelText()
        {
            BtnStrDict[2][0] = m_OpenFunBtnCTCS300T.ModeSelect[0] ? "�˳�\n����" : "����";
            BtnStrDict[2][1] = m_OpenFunBtnCTCS300T.ModeSelect[1] ? "�˳�\nĿ��" : "Ŀ��";
            BtnStrDict[2][2] = m_OpenFunBtnCTCS300T.ModeSelect[2] ? "�˳�\n����" : "����";
        }

        public void InitalizeStates()
        {
            BtnStrDict.Clear();

            #region ::::::::::::::::::::::: _btnStrDict :::::::::::::::::::::::::::

            //δѡ�� = 0
            BtnStrDict.Add(0, new[] {"����", "ģʽ", "��Ƶ", "�ȼ�", "����", "����", "����", "����"});

            //ȷ��ȡ�� = 9
            BtnStrDict.Add(9,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", string.Empty, "ȡ��"});

            //ȷ�� = 10
            BtnStrDict.Add(10,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��"});

            //������ʼ_˾���� = 100
            BtnStrDict.Add(100,
                new[] {"˾����", "���κ�", string.Empty, string.Empty, string.Empty, "ȷ��", "ɾ��", "ȡ��", "��ʻ����"});

            //������ʼ_���κ� = 101
            BtnStrDict.Add(101,
                new[] {"˾����", "���κ�", string.Empty, string.Empty, string.Empty, "ȷ��", "ɾ��", "ȡ��", "��ʻ����"});

            //������ʼ_˾�����κ�ȷ�� = 1016
            BtnStrDict.Add(1016,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", string.Empty, "ȡ��", "��ʻ����"});

            //������ʼ_�ƶ�����_�����ƶ����Գɹ� = 1021
            BtnStrDict.Add(1021,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", string.Empty, "ȡ��"});

            //������ʼ_�ƶ�����_�������ƶ����Գɹ� = 1022
            BtnStrDict.Add(1022,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��"});

            //F1���� = 1
            BtnStrDict.Add(1, new[] {"˾����", "���κ�", "�г�\n����", "����", "����", string.Empty, string.Empty, "����"});

            //F1����_F1˾���� = 11
            BtnStrDict.Add(11,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", "ɾ��", "ȡ��", "��ʻ����"});

            //F1����_F1˾����_F6ȷ�� = 116
            BtnStrDict.Add(116,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", string.Empty, "ȡ��", "��ʻ����"});

            //F1����_F2���κ� = 12
            BtnStrDict.Add(12,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", "ɾ��", "ȡ��", "��ʻ����"});

            //F1����_F2���κ�_F6ȷ�� = 126
            BtnStrDict.Add(126,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", string.Empty, "ȡ��", "��ʻ����"});

            //F1����_F3�г����� = 13
            BtnStrDict.Add(13,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", "ɾ��", "ȡ��", "�г�����"});

            //F1����_F3�г�����_F6ȷ�� = 136
            BtnStrDict.Add(136,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", string.Empty, "ȡ��", "�г�����"});

            //F2ģʽ = 2
            BtnStrDict.Add(2,
                new[] {"����", "Ŀ��", "����", string.Empty, string.Empty, string.Empty, string.Empty, "����", "ģʽѡ��"});

            //F2ģʽ_F1���� = 210
            BtnStrDict.Add(210,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "ȷ��",
                    string.Empty,
                    "ȡ��",
                    "����ģʽȷ��",
                    "����ȷ���Ƿ�\r\nѡ�����ģʽ��"
                });

            //F2ģʽ_F2Ŀ�� = 220
            BtnStrDict.Add(220,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "ȷ��",
                    string.Empty,
                    "ȡ��",
                    "Ŀ��ģʽȷ��",
                    "����ȷ���Ƿ�\r\nѡ��Ŀ��ģʽ��"
                });

            //F2ģʽ_F3���� = 230
            BtnStrDict.Add(230,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "ȷ��",
                    string.Empty,
                    "ȡ��",
                    "����ģʽȷ��",
                    "����ȷ���Ƿ�\r\nѡ�����ģʽ��"
                });

            //F2ģʽ_F1���� = 211
            BtnStrDict.Add(211,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "ȷ��",
                    string.Empty,
                    "ȡ��",
                    "�˳�����ģʽ",
                    "����ȷ���Ƿ�\r\n�˳�����ģʽ��"
                });

            //F2ģʽ_F2Ŀ�� = 221
            BtnStrDict.Add(221,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "ȷ��",
                    string.Empty,
                    "ȡ��",
                    "�˳�Ŀ��ģʽ",
                    "����ȷ���Ƿ�\r\n�˳�Ŀ��ģʽ��"
                });

            //F2ģʽ_F3���� = 231
            BtnStrDict.Add(231,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "ȷ��",
                    string.Empty,
                    "ȡ��",
                    "�˳�����ģʽ",
                    "����ȷ���Ƿ�\r\n�˳�����ģʽ��"
                });
            //F3��Ƶ = 3
            BtnStrDict.Add(3,
                new[]
                {"����", "����", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "����", "��Ƶѡ��"});

            //F3��Ƶ_F1���� = 31
            BtnStrDict.Add(31,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "ȷ��",
                    string.Empty,
                    "ȡ��",
                    "������Ƶȷ��",
                    "����ȷ���Ƿ�\r\n\r\n ѡ��������Ƶ?"
                });

            //F3��Ƶ_F1����_ȷ�� = 316
            BtnStrDict.Add(316,
                new[]
                {"����", "����", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", "��Ƶѡ��"});

            //F3��Ƶ_F2���� = 32
            BtnStrDict.Add(32,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "ȷ��",
                    string.Empty,
                    "ȡ��",
                    "������Ƶȷ��",
                    "����ȷ���Ƿ�\r\n\r\n ѡ��������Ƶ?"
                });

            //F3��Ƶ_F2����_ȷ�� = 326
            BtnStrDict.Add(326,
                new[]
                {"����", "����", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", "��Ƶѡ��"});

            //F4�ȼ� = 4
            BtnStrDict.Add(4,
                new[]
                {
                    "CTCS3",
                    "CTCS2",
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "����",
                    "CTCS�ȼ�ѡ��"
                });

            //F4�ȼ�_F1CTCS3 = 41
            BtnStrDict.Add(41,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", string.Empty, "ȡ��"});

            //F4�ȼ�_F1CTCS3_RBC = 411
            BtnStrDict.Add(411,
                new[] {"RBC\nID", "�绰\n����", string.Empty, string.Empty, string.Empty, "ȷ��", "ɾ��", "ȡ��", "RBC����"});

            //F4�ȼ�_F1CTCS3_�绰����
            BtnStrDict.Add(412,
                new[] {"RBC\nID", "�绰\n����", string.Empty, string.Empty, string.Empty, "ȷ��", "ɾ��", "ȡ��", "RBC����"});

            //F4�ȼ�_F2CTCS3_RBCȷ�� = 4116
            BtnStrDict.Add(4116,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", string.Empty, "ȡ��", "��ʻ����"});

            //F4�ȼ�_F2CTCS2 = 42
            BtnStrDict.Add(42,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", string.Empty, "ȡ��"});

            //F5���� = 5
            BtnStrDict.Add(5,
                new[] {"�ƶ�����", "����", "����", string.Empty, string.Empty, string.Empty, string.Empty, "����"});

            //F5�ƶ����� = 51
            BtnStrDict.Add(51,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��", string.Empty, "ȡ��"});

            //F5����_F2���� = 52
            BtnStrDict.Add(52,
                new[]
                {"��", "С", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "����", "��������"});

            //F5����_F3���� = 53
            BtnStrDict.Add(53,
                new[]
                {"��", "��", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "����", "��������"});

            //F6���� = 6
            BtnStrDict.Add(6,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "ȷ��",
                    string.Empty,
                    "ȡ��",
                    "����ȷ��",
                    "����ȷ���Ƿ�\r\n\r\n  ��Ҫ�����豸��"
                });

            //F6����_F6ȷ�� = 66
            BtnStrDict.Add(66,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��"});

            //F7���� = 7
            BtnStrDict.Add(7,
                new[]
                {
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    string.Empty,
                    "ȷ��",
                    string.Empty,
                    "ȡ��",
                    "����ȷ��",
                    "�Ƿ񻺽⣬��ȷ����"
                });

            //F7����_F6ȷ�� = 76
            BtnStrDict.Add(76,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��"});

            //F8���� = 8
            BtnStrDict.Add(8,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��"});
            //F4�ȼ�_F1_CTCS3_ȷ��
            BtnStrDict.Add(414,
                new[]
                {string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "ȷ��"});

            #endregion

            MenuValueArrDict.Clear();

            #region :::::::::::::::::::::::  MenuOfFun  :::::::::::::::::::::::::::

            //������ʼ_˾���� = 100
            MenuValueArrDict.Add(100,
                new[]
                {
                    new MenuValue(false, false, "˾���ţ�", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.DriverId, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "���κţ�", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TrainId, null, m_OpenFunBtnCTCS300T.RectList[27])
                });

            //������ʼ_���κ� = 101
            MenuValueArrDict.Add(101,
                new[]
                {
                    new MenuValue(false, false, "˾���ţ�", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.DriverId, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "���κţ�", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.TrainId, null, m_OpenFunBtnCTCS300T.RectList[27])
                });

            //������ʼ_˾�����κ�ȷ�� = 1016
            MenuValueArrDict.Add(1016,
                new[]
                {
                    new MenuValue(false, false, "˾���ţ�", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.DriverId, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "���κţ�", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TrainId, null, m_OpenFunBtnCTCS300T.RectList[27])
                });

            //F1����_F1˾���� = 11
            MenuValueArrDict.Add(11,
                new[]
                {
                    new MenuValue(false, false, "˾���ţ�", null, m_OpenFunBtnCTCS300T.RectList[33]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.DriverId, null, m_OpenFunBtnCTCS300T.RectList[24])
                });
            //F1����_F1˾����_F6ȷ�� = 116
            MenuValueArrDict.Add(116,
                new[]
                {
                    new MenuValue(false, false, "˾���ţ�", null, m_OpenFunBtnCTCS300T.RectList[33]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.DriverId, null, m_OpenFunBtnCTCS300T.RectList[24])
                });
            //F1����_F2���κ� = 12
            MenuValueArrDict.Add(12,
                new[]
                {
                    new MenuValue(false, false, "���κţ�", null, m_OpenFunBtnCTCS300T.RectList[33]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.TrainId, null, m_OpenFunBtnCTCS300T.RectList[24])
                });
            //F1����_F2���κ�_F6ȷ�� = 126
            MenuValueArrDict.Add(126,
                new[]
                {
                    new MenuValue(false, false, "���κţ�", null, m_OpenFunBtnCTCS300T.RectList[33]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TrainId, null, m_OpenFunBtnCTCS300T.RectList[24])
                });
            //F1����_F3�г����� = 13
            MenuValueArrDict.Add(13,
                new[]
                {
                    new MenuValue(false, false, "��������:1(8��),2(16��)", null, m_OpenFunBtnCTCS300T.RectList[12]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.TrainData, null, m_OpenFunBtnCTCS300T.RectList[24])
                });
            //F1����_F3�г�����_F6ȷ�� = 136
            MenuValueArrDict.Add(136,
                new[]
                {
                    new MenuValue(false, false, "��������:1(8��),2(16��)", null, m_OpenFunBtnCTCS300T.RectList[12]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TrainData, null, m_OpenFunBtnCTCS300T.RectList[24])
                });
            //F4�ȼ�_F1CTCS3_RBC = 411
            MenuValueArrDict.Add(411,
                new[]
                {
                    new MenuValue(false, false, "RBC  ID��", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.RBCID, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "�绰���룺", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TelNmub, null, m_OpenFunBtnCTCS300T.RectList[27]),
                    //new MenuValue(false, false, "�����ţ�", null, m_RectList[36]),
                    //  new MenuValue(true, false, NetId, null, m_RectList[27]),
                });
            //F4�ȼ�_F1CTCS3_�绰���� = 412
            MenuValueArrDict.Add(412,
                new[]
                {
                    new MenuValue(false, false, "RBC  ID��", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.RBCID, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "�绰���룺", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, true, m_OpenFunBtnCTCS300T.TelNmub, null, m_OpenFunBtnCTCS300T.RectList[27]),
                    // new MenuValue(false, false, "�����ţ�", null, m_RectList[36]),
                    //  new MenuValue(true, false, NetId, null, m_RectList[27]),
                });

            MenuValueArrDict.Add(413,
                new[]
                {
                    new MenuValue(false, false, "RBC  ID��", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.RBCID, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "�绰���룺", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TelNmub, null, m_OpenFunBtnCTCS300T.RectList[27]),
                    //  new MenuValue(false, false, "�����ţ�", null, m_RectList[36]),
                    //  new MenuValue(true, true, NetId, null, m_RectList[27]),
                });

            //F4�ȼ�_F2CTCS3_RBCȷ�� = 4116
            MenuValueArrDict.Add(4116,
                new[]
                {
                    new MenuValue(false, false, "RBC  ID��", null, m_OpenFunBtnCTCS300T.RectList[32]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.RBCID, null, m_OpenFunBtnCTCS300T.RectList[23]),
                    new MenuValue(false, false, "�绰���룺", null, m_OpenFunBtnCTCS300T.RectList[36]),
                    new MenuValue(true, false, m_OpenFunBtnCTCS300T.TelNmub, null, m_OpenFunBtnCTCS300T.RectList[27]),
                    //  new MenuValue(false, false, "�����ţ�", null, m_RectList[36]),
                    //  new MenuValue(true, false, NetId, null, m_RectList[27]),
                });

            //F2ģʽ = 2
            MenuValueArrDict.Add(2,
                new[]
                {
                    new MenuValue(false, false, "����ģʽ", null, m_OpenFunBtnCTCS300T.RectList[22], FontRelated.����),
                    new MenuValue(false, false, "Ŀ��ģʽ", null, m_OpenFunBtnCTCS300T.RectList[24], FontRelated.����),
                    new MenuValue(false, false, "�����ź�ģʽ", null, m_OpenFunBtnCTCS300T.RectList[26], FontRelated.����)
                });
            //F3��Ƶ = 3
            MenuValueArrDict.Add(3,
                new[]
                {
                    new MenuValue(false, false, "������Ƶ", null, m_OpenFunBtnCTCS300T.RectList[22], FontRelated.����),
                    new MenuValue(false, false, "������Ƶ", null, m_OpenFunBtnCTCS300T.RectList[24], FontRelated.����)
                });
            //F3��Ƶ_F1����_ȷ�� = 316
            MenuValueArrDict.Add(316,
                new[]
                {
                    new MenuValue(false, true, "������Ƶ", null, m_OpenFunBtnCTCS300T.RectList[22], FontRelated.����),
                    new MenuValue(false, false, "������Ƶ", null, m_OpenFunBtnCTCS300T.RectList[24], FontRelated.����)
                });

            //F3��Ƶ_F2����_ȷ�� = 326
            MenuValueArrDict.Add(326,
                new[]
                {
                    new MenuValue(false, false, "������Ƶ", null, m_OpenFunBtnCTCS300T.RectList[22], FontRelated.����),
                    new MenuValue(false, true, "������Ƶ", null, m_OpenFunBtnCTCS300T.RectList[24], FontRelated.����)
                });
            //F4�ȼ� = 4
            MenuValueArrDict.Add(4,
                new[]
                {
                    new MenuValue(false, false, "CTCS3 ��", null, m_OpenFunBtnCTCS300T.RectList[22], FontRelated.����),
                    new MenuValue(false, false, "CTCS2 ��", null, m_OpenFunBtnCTCS300T.RectList[24], FontRelated.����)
                });
            m_OpenFunBtnCTCS300T.m_AppraiseList = new List<FunMenuButtonId>()
            {
                FunMenuButtonId.F1����_F1˾����,
                FunMenuButtonId.F1����_F1˾����_F6ȷ��,
                FunMenuButtonId.������ʼ_˾����,
                FunMenuButtonId.������ʼ_���κ�,
                FunMenuButtonId.������ʼ_˾�����κ�ȷ��
            };

            #endregion
        }
    }
}