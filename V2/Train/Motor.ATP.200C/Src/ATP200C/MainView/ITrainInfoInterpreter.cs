using System;
using System.Linq;

namespace ATP200C.MainView
{
    public interface ITrainInfoExpression
    {
        float[] Interprete(TrainInfo trainInfo);
    }

    public class TrainInfoToFloatExpression : ITrainInfoExpression
    {
        public float[] Interprete(TrainInfo trainInfo)
        {
            var rlt = new float[3];
            if (trainInfo.TrainId != null)
            {
                var head = trainInfo.TrainId.ToCharArray().Where(c => char.IsLower(c) || char.IsUpper(c)).Aggregate(string.Empty, (current, c) => current + c);
                if (!string.IsNullOrEmpty(head))
                {
                    if (head.Length > 3)
                    {
                        rlt[0] = 0;
                        rlt[1] = 0;
                    }
                    else
                    {
                        rlt[0] = CommonConvertTo.ConverToFloat(head);
                        var trainid = new string(trainInfo.TrainId.Skip(head.Length).ToArray());
                        try
                        {
                            var id = Convert.ToSingle(trainid);
                            rlt[1] = id;
                        }
                        catch (Exception)
                        {
                            rlt[1] = 0f;
                        }
                    }
                }
            }
            try
            {
                var id = Convert.ToSingle(trainInfo.DriverId);
                rlt[2] = id;
            }
            catch (Exception)
            {
                rlt[2] = 0f;
            }

            return rlt;
        }
    }
}