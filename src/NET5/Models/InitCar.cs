using System.Text;

namespace NET5.Models
{
    public class InitCar
    {
        public InitCar()
        {
        }

        public string Color { get; init; }

        public int CountDoors { get; init; }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine(nameof(InitCar))
                .AppendLine(nameof(CountDoors) + $": {CountDoors}")
                .AppendLine(nameof(Color) + $": {Color}")
                .ToString();
        }
    }
}
