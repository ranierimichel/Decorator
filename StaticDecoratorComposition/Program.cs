using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// This does not work!
namespace StaticDecoratorComposition
{
    public abstract class Shape
    {
        public abstract string AsString();
    }

    public class Circle : Shape
    {
        private float radius;

        public Circle() : this(0.0f)
        {
            
        }

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

        public Square() : this(0.0f)
        {
            
        }

        public Square(float side)
        {
            this.side = side;
        }

        public override string AsString() => $"A square with side {side}";
    }

    public class ColoredShape : Shape
    {
        private Shape shape;
        private string color;

        public ColoredShape(Shape shape, string color)
        {
            this.shape = shape;
            this.color = color;
        }
        public override string AsString() => $"{shape.AsString()} has the color {color}";
    }

    public class TransparentShape : Shape
    {
        private Shape shape;
        private float transparency;

        public TransparentShape(Shape shape, float transparency)
        {
            this.shape = shape;
            this.transparency = transparency;
        }

        public override string AsString() => $"{shape.AsString()} has {transparency * 100.0}% transparency";
    }

    public class ColoredShape<T> : Shape where T : Shape, new()
    {
        private string color;
        private T shape = new T();

        public ColoredShape()
        {
            this.color = "Black";
        }
        public ColoredShape(string color)
        {
            this.color = color; 
        }
        public override string AsString() => $"{shape.AsString()} has the color {color}";
    }  
    public class TransparentShape<T> : Shape where T : Shape, new()
    {
        private float transparency;
        private T shape = new T();

        public TransparentShape() : this(0)
        {
            this.transparency = 0;
        }
        public TransparentShape(float transparency)
        {
            this.transparency = transparency; 
        }
        public override string AsString() => $"{shape.AsString()} has {transparency * 100.0f}% transparency";
    }

    class Program
    {
        static void Main(string[] args)
        {
            var redSquare = new ColoredShape<Square>("red");
            var circle = new TransparentShape<ColoredShape<Circle>>(.4f);
            Console.WriteLine(redSquare.AsString());
            Console.WriteLine(circle.AsString());


            //var circle1 = new Circle(2.5f);
            //var circle2 = new Circle(5f);
            //var circle3 = new Circle(10f);
            //var square1 = new Square(1.23f);
            //var square2 = new Square(2.23f);
            //var square3 = new Square(3.23f);

            //var redCircle = new ColoredShape(circle1, "red");
            //var blueCircle = new ColoredShape(circle2, "blue");
            //var redBlueCircle = new ColoredShape(blueCircle, "red");
            //var redHalfTransparentCircle = new TransparentShape(circle3, 0.5f);
            //Console.WriteLine(redCircle.AsString());
            //Console.WriteLine(blueCircle.AsString());
            //Console.WriteLine(redBlueCircle.AsString());
            //Console.WriteLine(redHalfTransparentCircle.AsString());
        }
    }
}
