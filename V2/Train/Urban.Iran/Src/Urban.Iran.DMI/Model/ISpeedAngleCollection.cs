namespace Urban.Iran.DMI.Model
{
    public interface ISpeedAngleCollection
    {
        float MinAngle { get; }

        float MaxAngle { get; }

        float MinSpeed { get; }

        float MaxSpeed { get; }

        float ConvertToAngle(float speed);
    }
}