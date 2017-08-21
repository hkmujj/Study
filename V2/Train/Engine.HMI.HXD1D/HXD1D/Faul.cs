using MsgReceiveMod;

namespace HXD1D
{
   public class Faul:FaultMsgOrdinary
   {
       /// <summary>
       /// 车号
       /// </summary>
       public string Code { set; get; }
       /// <summary>
       /// 等级
       /// </summary>
       public string Level { get; set; }
       /// <summary>
       /// V=0时
       /// </summary>
       public string V0 { get; set; }
       /// <summary>
       /// V>0时
       /// </summary>
       public string V1 { get; set; }

       /// <summary>
       /// 原因
       /// </summary>
       public string Reason { get; set; }

       /// <summary>
       /// 结果
       /// </summary>
       public string Result { get; set; }
   }
}
