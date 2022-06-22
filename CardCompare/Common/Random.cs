namespace CardCompare.Common
{
    public class Random
    {
        private static System.Random random = new System.Random();
        public static int Range(int min, int max) => random.Next(min, max + 1);
    }
}