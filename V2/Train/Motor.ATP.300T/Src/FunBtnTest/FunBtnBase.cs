namespace Motor.ATP._300T.FunBtnTest
{
    /// <summary>
    /// Context类，维护一个ConcreteState子类的实例，这个实例定义当前的状态
    /// </summary>
    public class Context
    {
        public FunBtnData FunBtnData { get; set; }

        /// <summary>
        /// 定义context的初始状态
        /// </summary>
        /// <param name="state"></param>
        public Context(State state)
        {
            State = state;
            FunBtnData = new FunBtnData {DriverId = "0", TrainId = "0", TrainLength = "200"};
        }

        /// <summary>
        /// 可读写的状态属性，用于读取和设置新状态
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// 对请求做处理，并设置下一个状态
        /// </summary>
        public void Request()
        {
            State.Handle(this);
        }
    }

    /// <summary>
    /// 抽象状态类，定义一个接口以封装与context的特定状态相关的行为
    /// </summary>
    public abstract class State
    {
        public abstract void Handle(Context context);
    }

    /// <summary>
    /// 具体的状态类，每一个子类实现一个与context的一个状态相关的行为
    /// </summary>
    public class ConcreteStateA : State
    {
        public override void Handle(Context context)
        {
            
        }
    }
}
