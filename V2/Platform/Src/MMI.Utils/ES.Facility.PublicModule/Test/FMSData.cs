using System.Collections.Generic;

namespace ES.Facility.PublicModule.Test
{
    /// <summary>
    /// ģ����ʹ�õ��¼����ݽṹ
    /// </summary>
    unsafe public struct FmsEvent
    {
        public long nID;
        public byte nType;
        public byte nSubType;
        public byte ucDestination;

        public byte rank;
        public long flags;

        public long lPara1;
        public long lPara2;
        public long lPara3;
        public long lPara4;

        public fixed byte ucPara1[8];

        public double dPara1;
        public double dPara2;

        public double dPara3;
        public double dPara4;
        public double dPara5;
        public double dPara6;
        public double dPara7;
        public double dPara8;
        public double dPara9;
        public double dPara10;

        public bool HYRunEventFlag;
        public int HYIndex;
    }

    unsafe public struct FmsRountInfo
    {
        public int   nSegNumber;                //��·�ϵĶ���Ŀ nSegNumber<=MAX_SEG_NUM
	    public fixed int   nRouteSegList[200];   //��·�ϵĶκ�����  ��������

        public List<int> RouteSegList;

	    public int    nFirstSegNo;              //���κ� ������
	    public float  fFirstOffset;             //�����з������ڶ��ڵ�ƫ��
	    public int    nFirstDirection;          //��ʼ����    ����1;����-1

	    byte  ucHour;                           //����ʱ��Сʱ0-24  �����Ӿ������������Ӿ���ͬ�Ĺ⻷��

	    bool   bIsDVD;                          //DVD true; CGI false
	    short  nDVDSegNo;                       //1��2��3��4
    }

    unsafe public struct EsNetInfo
    {
        public int		verId;								//�汾id

	    public int		cmdType;							//��������
	    public int		dataType;							//��������

	    //���� 4��
	    public int		paraInt1;	
	    public int		paraInt2;
	    public int		paraInt3;
	    public int		paraInt4;

	    //�ڲ�ʹ�õ� ����
	    public int		assistInt1;

	    //������ֲ�	2��
	    public fixed char	paraChar1[16];
	    public fixed char	paraChar2[16];	
	    public int		index;								//��ǰ�����
	    public int		endID;								//������ID

	    //���ֲ�	1��
	    public fixed char	paraLChar1[800];

    }
}
