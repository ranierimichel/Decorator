using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DetectingDecoratorCycles
{
    public abstract class Shape
    {
        public abstract string AsString();
    }

    public class Circle : Shape
    {
        private float radius;

        public Circle(float radius)
        {
            this.radius = radius;
        }

        public void Resize(float factor)
        {
            radius *= factor;
        }

        public override string AsString() => $"A circle with radius {radius}";
    }

    public class Square : Shape
    {
        private float side;

        public Square(float side)
        {
            this.side = side;
        }

        public override string AsString() => $"A square with side {side}";
    }

    public abstract class ShapeDecorator : Shape
    {
        protected internal readonly List<Type> types = new List<Type>();
        protected internal Shape shape;

        public ShapeDecorator(Shape shape)
        {
            this.shape = shape;
            if (shape is ShapeDecorator sd)
                types.AddRange(sd.types);
        }
    }

    public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecorator
        where TCyclePolicy : ShapeDecoratorCyclePolicy, new()
    {
        protected readonly TCyclePolicy policy = new TCyclePolicy();

        protected ShapeDecorator(Shape shape) : base(shape)
        {
            if (policy.TypeAdditionAllowed(typeof(TSelf), types))
                types.Add(typeof(TSelf));
        }
    }

    public class ColoredShape
        : ShapeDecorator<ColoredShape, AbsorbCyclePolicy>
    {
        private string color;

        public ColoredShape(Shape shape, string color) : base(shape)
        {
            this.shape = shape;
            this.color = color;
        }
        public override string AsString()
        {
            var sb = new StringBuilder($"{shape.AsString()}");
            if (policy.ApplicationAllowed(types[0], types.Skip(1).ToList()))
                sb.Append($" has the color {color}");
            return sb.ToString();
        }
    }


    public class TransparentShape 
        : ShapeDecorator<TransparentShape, ThrowOnCyclePolicy>
    {
        private float transparency;

        public TransparentShape(Shape shape, float transparency) : base(shape)
        {
            this.shape = shape;
            this.transparency = transparency;
        }

        public override string AsString() => $"{shape.AsString()} has {transparency * 100.0}% transparency";
    }

    public abstract class ShapeDecoratorCyclePolicy
    {
        public abstract bool TypeAdditionAllowed(Type type, IList<Type> allTypes);
        public abstract bool ApplicationAllowed(Type type, IList<Type> allTypes);
    }

    public class CyclesAllowedPolicy : ShapeDecoratorCyclePolicy
    {
        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }

        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }
    }

    public class ThrowOnCyclePolicy : ShapeDecoratorCyclePolicy
    {
        private bool handler(Type type, IList<Type> allTypes)
        {
            if (allTypes.Contains(type))
                throw new InvalidOperationException(
                    $"Cycle detected! Type is already a {type.FullName}!");
            return true;
        }
        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return handler(type, allTypes);
        }

        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return handler(type, allTypes);
        }
    }

    public class AbsorbCyclePolicy : ShapeDecoratorCyclePolicy
    {
        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }

        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return !allTypes.Contains(type);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var circle = new Circle(2);
            WriteLine(circle.AsString());

            var colored1 = new ColoredShape(circle, "red");
            WriteLine(colored1.AsString());

            var colored2 = new ColoredShape(colored1, "blue");
            WriteLine(colored2.AsString());

            var transparency1 = new TransparentShape(circle, .5f);
            WriteLine(transparency1.AsString());

            var transparency2 = new TransparentShape(transparency1, .5f);
            WriteLine(transparency2.AsString());
        }
    }
}
