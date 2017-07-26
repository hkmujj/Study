using System.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Motor.ATP.Infrasturcture.Model
{
    /// <summary>
    /// 车站解释器
    /// </summary>
    public interface IStationInterpreter
    {
        /// <summary>
        /// 解释车站
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        StationInterpreterResult Interpreter(float index);
    }

    [DebuggerDisplay("Sucess={Sucess}, StationName={StationName}, ErrorMessage={ErrorMessage}")]
    public class StationInterpreterResult
    {
        [DebuggerStepThrough]
        public StationInterpreterResult(bool sucess = false, string stationName = null, string errorMessage = null)
        {
            Sucess = sucess;
            StationName = stationName;
            ErrorMessage = errorMessage;
        }

        public bool Sucess { private set; get; }

        public string StationName { private set; get; }

        public string ErrorMessage { private set; get; }
    }
}