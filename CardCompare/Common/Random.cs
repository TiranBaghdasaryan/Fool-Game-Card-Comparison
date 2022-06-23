using System;

namespace CardCompare.Common
{
    public class Random
    {
        static Random() => _instance = new System.Random(DateTime.Now.Millisecond);
        private static System.Random _instance;


        private System.Random random;

        public static int Range(int min, int max) => _instance.Next(min, max + 1);
    }
}