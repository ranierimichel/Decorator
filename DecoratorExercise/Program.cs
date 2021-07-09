using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorExercise
{
    class Program
    {
        public abstract class Organism
        {
        }
        public class Bird : Organism
        {
            public int Age { get; set; }

            public string Fly()
            {
                return (Age < 10) ? "flying" : "too old";
            }
        }

        public class Lizard : Organism
        {
            public int Age { get; set; }

            public string Crawl()
            {
                return (Age > 1) ? "crawling" : "too young";
            }
        }

        public class Dragon : Organism
        {
            private Bird bird = new Bird();
            private Lizard lizard = new Lizard();
            private int _age;
            
            public int Age
            {
                get { return _age; } // The use of expression body will give errors on Check solutions
                set
                {
                    _age = value;
                    bird.Age = value;
                    lizard.Age = value;
                } 
            }

            public string Fly()
            {
                return bird.Fly();
            }

            public string Crawl()
            {
                return lizard.Crawl();
            }
        }
        static void Main(string[] args)
        {
            Dragon dragon = new Dragon();
            dragon.Age = 100;
            Console.WriteLine(dragon.Age);
            Console.WriteLine(dragon.Fly());
            Console.WriteLine(dragon.Crawl());
        }
    }
}
