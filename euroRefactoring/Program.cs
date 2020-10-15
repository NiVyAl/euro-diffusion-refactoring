using System;

namespace euroRefactoring
{
    class Program
    {
        static void Main(string[] args)
        {
            Diffusion euroDiffusion = new Diffusion();
            euroDiffusion.Parse(@"input/input.in");
        }
    }
}