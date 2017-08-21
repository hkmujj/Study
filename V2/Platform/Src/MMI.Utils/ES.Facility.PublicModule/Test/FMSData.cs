using System.Collections.Generic;

namespace ES.Facility.PublicModule.Test
{
    /// <summary>
    /// 模拟器使用的事件数据结构
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
        public int   nSegNumber;                //进路上的段数目 nSegNumber<=MAX_SEG_NUM
	    public fixed int   nRouteSegList[200];   //进路上的段号序列  带正负号

        public List<int> RouteSegList;

	    public int    nFirstSegNo;              //起点段号 带正负
	    public float  fFirstOffset;             //沿运行方向上在段内的偏移
	    public int    nFirstDirection;          //起始方向    上行1;下行-1

	    byte  ucHour;                           //出发时间小时0-24  用于视景设置早中晚视景不同的光环境

	    bool   bIsDVD;                          //DVD true; CGI false
	    short  nDVDSegNo;                       //1、2、3、4
    }

    unsafe public struct EsNetInfo
    {
        public int		verId;								//版本id

	    public int		cmdType;							//命令类型
	    public int		dataType;							//数据类型

	    //整参 4个
	    public int		paraInt1;	
	    public int		paraInt2;
	    public int		paraInt3;
	    public int		paraInt4;

	    //内部使用的 整参
	    public int		assistInt1;

	    //编号类字参	2个
	    public fixed char	paraChar1[16];
	    public fixed char	paraChar2[16];	
	    public int		index;								//当前包序号
	    public int		endID;								//结束包ID

	    //长字参	1个
	    public fixed char	paraLChar1[800];

    }
}
