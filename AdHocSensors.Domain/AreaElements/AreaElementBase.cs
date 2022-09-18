using AdHocSensors.Domain.SettingsPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdHocSensors.Domain
{
    public class AreaElementBase : IEquatable<AreaElementBase>
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Range { get; set; }

        protected AreaElementBase(int id, double x, double y)
        {
            Id = id;
            X = x;
            Y = y;
            Range = 1 / Settings.Current.Scale;
        }

        public bool Equals(AreaElementBase? other)
        {
            return other != null &&
                X == other.X &&
                Y == other.Y;
        }

        public double DistanceTo(AreaElementBase other)
        {
            var distance = Math.Sqrt(
                Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
            return Math.Round(distance, 6, MidpointRounding.ToZero);
        }
    }
}