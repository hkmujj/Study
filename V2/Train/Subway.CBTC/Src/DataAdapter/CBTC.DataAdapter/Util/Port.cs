namespace CBTC.DataAdapter.Util
{
    //门允许状态
    enum DOORPERMITSTATUS
    {
        DOORPERMITSTATUS_NULL = 0,//
        DOORPERMITSTATUS_BYPASS = 1,//旁路
        DOORPERMITSTATUS_NOPERMIT = 2,//不允许
        DOORPERMITSTATUS_PERMIT = 3,//允许
        DOORPERMITSTATUS_PERMITFIRST = 4,//允许先开
        DOORPERMITSTATUS_PERMITSECOND = 5 //允许后开
    };

    //门状态
    enum DOORSTATUSFLAG 
    {
        DOORSTATUS_FLAG_NULL = 0,  //无效 
        DOORSTATUS_FLAG_CLOSE = 1,  //关闭
        DOORSTATUS_FLAG_OPEN = 2,  //开启 
        DOORSTATUS_FLAG_ABNORMALOPEN = 3,  //异常打开
        DOORSTATUS_FLAG_RELEASEDOOR = 4,    //允许释放车门
        DOORSTATUS_FLAG_NOMORESUPERVISE = 5,    //不再监控车门
        DOORSTATUS_FLAG_NOTKNOW = 6    //状态未知
    };

    enum SPEEDSTATUS
    {
        SPEEDSTATUS_ZERO = 1,//0速
        SPEEDSTATUS_OVER = 2,//超速
    };

    //级别
    enum CONTROL_LEVEL
    {
        CONTROL_LEVEL_NULL = 0,
        CONTROL_LEVEL_IXLC = 1,  //联锁级列车控制
        CONTROL_LEVEL_ITC = 2,  //点式列车控制
        CONTROL_LEVEL_CTC = 3,  //连续式列车控制
        CONTROL_LEVEL_BM = 4   //后备模式
    };

    //线路段标识
    enum SEGFLAG
    {
        MAINLINE = 1,                  //正线
        SIDELINE = 2,                 //侧线
        DEPOT = 3,                 //车辆段
        TRANSITTRACK = 4,                 //转换轨
        TURNBACKTRACK = 5,                 //折返轨
        STATION = 6,                  //站台
        ATBPLATFORM = 7,                 //自动折返站台
        MTBPLATFORM = 8                  //人工折返站台
    };
    //车辆实时运行工况
    enum RealTimeWORKSTATUS
    {
        WORKSTATUS_NULL = 0,
        WORKSTATUS_TRACTION = 1,         //牵引工况
        WORKSTATUS_EB = 2,              //紧急工况
        WORKSTATUS_COMMONBRAKE = 3,     //常用制动工况
        WORKSTATUS_FASTBRAKE = 4,       //快速制动工况
        WORKSTATUS_PARKBRAKE = 5,       //停放制动工况
        WORKSTATUS_KEEPBRAKE = 6,       //保持制动
        WORKSTATUS_COAST = 7             //惰行工况
    };

    //停车状态
    enum DOCKEDSTATUS
    {
        DOCKEDSTATUS_NULL = 0,      //
        DOCKEDSTATUS_ALIGNED = 1,                //在车站且停准
        DOCKEDSTATUS_NONALIGNED = 2,             //在车站没有停准
        DOCKEDSTATUS_NOTATSTATION = 3,          //不在车站
        DOCKEDSTATUS_INVALID = 4                    //无效
    };

    //ATC状态信息，给屏的
    enum ATCSTATUS
    {
        ATCSTATUS_NULL = 0,   //无
        ATCSTATUS_NORMAL = 1,      //车载设备正常	
        ATCSTATUS_PARTFAULT = 2,  //车载设备部分故障
        ATCSTATUS_ALLFAULT = 3    //车载设备完全故障
    };

    //模式
    enum CONTROL_RUNMODE
    {
        RUN_MODE_NULL = 0, //无
        RUN_MODE_RM = 1, //RM模式向前
        RUN_MODE_SM = 2,    //ATP模式
        RUN_MODE_AM = 3,  //ATO模式
        RUN_MODE_NRM = 4, //切除ATC
        RUN_MODE_SB = 5, //待机模式
        RUN_MODE_RMR = 6, //RM模式向后
        RUN_MODE_ATB = 7,  //自动折返模式
        RUN_MODE_OFF = 8, //OFF模式
        RUN_MODE_WASH = 9,  //洗车模式
        RUN_MODE_IATO = 10,  //点式ATO
        RUN_MODE_IATP = 11,  //点式ATP
        RUN_MODE_MSHUNT = 12   //调车模式	
    };

    enum BREAKSTATUS
    {
        NoneBrakeSign = 0,  //无制动施加
        RequestBrake = 1,   //请求制动
        BrakeAlert = 2,   //制动预警
        CommonBrake = 3,  //常用制动
        FastBrake = 4,  //快速制动
        ParkBrake = 5,  //停放制动
        EmergencyBrake = 6, //紧急制动
        KeepBrake = 7,   //保持制动
        PowerLoseSign = 8,    //切除牵引
        ReleaseBrake = 9    //允许缓解 

    };

    //ATC请求状态
    enum ATCREQUESTSTATUS
    {
        ATCREQUESTSTATUS_NULL = 0,  //
        ATCREQUESTSTATUS_CLOSEDOOR = 1,  //请求车门关闭
        ATCREQUESTSTATUS_TRAINDEPART = 2,  //ATC请求列车离站 
        ATCREQUESTSTATUS_CLOSEDOORORDER = 3  //关闭门指令

    };

    //方向手柄
    enum MASTERDIRECTION
    {
        MASTERDIRECTION_NULL = 0,
        MASTERDIRECTION_ZERO = 1,         //零位
        MASTERDIRECTION_FORWARD = 2,      //前
        MASTERDIRECTION_BACKWARD = 3      //后
    };
    //模式转换开关
    enum MODESWITCH
    {
        MODESWITCH_NULL = 0,
        MODESWITCH_OFF = 1,  //模式关
        MODESWITCH_WASH = 2,     //洗车模式
        MODESWITCH_RMR = 3, // 限制人工倒车
        MODESWITCH_RMF = 4,//限制人工前行
        MODESWITCH_IATP = 5, //点式ATP
        MODESWITCH_ATP = 6, //ATP
    };

    //ATO相关按钮
    enum ATORELATIONSWITCH
    {
        ATORELATIONSWITCH_NULL = 0,
        ATORELATIONSWITCH_ATO = 1,  //ATO模式开关
        ATORELATIONSWITCH_ATOSWITCH = 2,  //ATO模式选择
        ATORELATIONSWITCH_ATOSTART = 3  //ATO启动按钮
    }

    enum CBTCCONNECTSTATUS
    {
        CBTCCONNECTSTATUS_NULL = 0,
        CBTCCONNECTSTATUS_NOCONNECT = 1,  //无连接 
        CBTCCONNECTSTATUS_ONECONNECTING = 2,  //正在建立一条连接 
        CBTCCONNECTSTATUS_TWOCONNECTING = 3,   //正在建立两条连接 
        CBTCCONNECTSTATUS_ONECONNECTED = 4,   //一条建立  
        CBTCCONNECTSTATUS_ONECONNECTED_OTHERNO = 5,  //一条建立，一条没有建立 
        CBTCCONNECTSTATUS_TWOCONNECTED = 6,        //两条建立
        CBTCCONNECTSTATUS_CONNECTED = 7          //建立连接
    };

    //停车点类型
    enum LMATYPE
    {
        LMATYPE_NULL = 0,
        LMATYPE_REDSIGNAL = 1,  //红灯信号机
        LMATYPE_PLATFORM = 2,  // 停车站台
        LMATYPE_STATIONENTER = 3,  //车站入口
    };
}
