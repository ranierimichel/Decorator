using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleInheritanceWithInterfaces
{
    public interface IBird
    {
        int Weight { get; set; }

        void Fly();
    }

    public class Bird : IBird
    {
        public int Weight { get; set; }
        public void Fly()
        {
            Console.WriteLine($"Soaring in the sky with weight {Weight}");
        }
    }

    public interface ILizard
    {
        int Weight { get; set; }

        void Crawl();
    }

    public class Lizard : ILizard
    {
        public int Weight { get; set; }
        public void Crawl()
        {
            Console.WriteLine($"Crawling in the dirt with weight {Weight}");
        }
    }

    public class Dragon : IBird, ILizard
    {
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();
        private int _weight;

        public int Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                bird.Weight = value;
                lizard.Weight = value;
            }
        }

        public void Fly()
        {
            bird.Fly();
        }

        public void Crawl()
        {
            lizard.Crawl();
        }
        
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            var d = new Dragon();
            d.Weight = 321;
            d.Fly();
            d.Crawl();
            Console.WriteLine(d.Weight);
        }
    }
}
