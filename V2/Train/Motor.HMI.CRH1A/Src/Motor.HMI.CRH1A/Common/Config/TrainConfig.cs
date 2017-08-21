using System.Collections.ObjectModel;

namespace Motor.HMI.CRH1A.Common.Config
{
     public class TrainConfig
     {
         /// <summary>
         /// 房间个数 8
         /// </summary>
         public const int RoomCount = 8;

         public static readonly ReadOnlyCollection<string> TrainNames = new ReadOnlyCollection<string>(new string[] { "01", "02", "03", "04", "05", "06", "07", "00" });

         /// <summary>
         /// 右司机室
         /// </summary>
         public const int RightDriverIndex = 7;
         /// <summary>
         /// 左司机室
         /// </summary>
         public const int LeftDriverIndex = 0;


         public enum DriverType
         {
             Left = 0,
             Right,
         }

     }
}
