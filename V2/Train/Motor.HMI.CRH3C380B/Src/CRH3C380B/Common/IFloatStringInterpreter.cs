namespace Motor.HMI.CRH3C380B.Common
{
    public interface IFloatStringInterpreter
    {
        char[] InterpreterFloatArray(float[] data);

        float[] InterpreterCharArray(char[] data);
    }
}