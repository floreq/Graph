using System;

namespace GraphModel
{
    public class Vertice : ICloneable
    {
        public string Name { get; private set; }

        public Vertice(string name)
        {
            Name = name;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
