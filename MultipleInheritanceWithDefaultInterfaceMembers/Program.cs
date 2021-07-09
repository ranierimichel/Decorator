using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// This only work on C# 8
namespace MultipleInheritanceWithDefaultInterfaceMembers
{
    public interface ICreature
    {
        int Age { get; set; }
    }

    public interface IBird : ICreature
    {
        void Fly()
        {
            if (Age >= 10)
            {
                Console.WriteLine("I'm flying");
            }
        }
    }

    public interface ILizard : ICreature
    {
        void Crawl()
        {
            if (Age < 10)
            {
                Console.WriteLine("I'm crawling");
            }
        }
    }

    public class Organism{}

    public class Dragon : Organism, ILizard, IBird
    {
        public int Age { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Dragon d = new Dragon() {Age = 5};

            if(d is IBird bird)
                bird.Fly();
            if(d is ILizard lizard)
                lizard.Crawl();
        }
    }
}
