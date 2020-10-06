using System;

namespace euroRefactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = Environment.TickCount;

            Diffusion euroDiffusion = new Diffusion();
            euroDiffusion.Parse(@"input/input.in");

            Console.WriteLine($"{Environment.TickCount - result}ms");
        }
    }
}
