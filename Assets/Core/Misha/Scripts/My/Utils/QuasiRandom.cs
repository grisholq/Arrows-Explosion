using System;
using Vector2 = UnityEngine.Vector2;

namespace DCFAEngine
{
    public class Quasi2DRandom
    {
        public static readonly Quasi2DRandom Global = new Quasi2DRandom();

        private static float G = 1.32471795724474602596f;
        private static float A1 = 1f / G;
        private static float A2 = 1f / (G * G);

        private float seed = 0;
        private int iteration = 0;

        public Quasi2DRandom(int seed)
        {
            this.seed = Math.Abs(seed / int.MaxValue);
        }
        public Quasi2DRandom(float seed)
        {
            this.seed = Math.Abs(seed / float.MaxValue);
        }
        public Quasi2DRandom()
        {
            seed = DateTime.Now.Ticks / long.MaxValue;
        }

        public Vector2 Random2D()
        {
            iteration++;
            return MathValue(seed, iteration);
        }

        private Vector2 MathValue(float seed, long n)
        {
            Vector2 result = new Vector2(
                seed + A1 * n,
                seed + A2 * n
                );
            result.x = result.x - (float)Math.Floor(result.x);
            result.y = result.y - (float)Math.Floor(result.y);
            return result;
        }
    }

    public class Quasi1DRandom
    {
        public static readonly Quasi1DRandom Global = new Quasi1DRandom();

        private static float G = 1.6180339887498948482f;
        private static float A1 = 1f / G;

        private float seed = 0f;
        private int iteration = 0;

        public Quasi1DRandom(int seed)
        {
            this.seed = Math.Abs(seed / int.MaxValue);
        }
        public Quasi1DRandom(float seed)
        {
            this.seed = Math.Abs(seed / float.MaxValue);
        }
        public Quasi1DRandom()
        {
            seed = DateTime.Now.Ticks / long.MaxValue;
        }

        public float Random()
        {
            iteration++;
            return MathValue(seed, iteration);
        }

        private float MathValue(float seed, long n)
        {
            float result = (seed + A1 * n);
            result -= (float)Math.Floor(result);
            if (result < 0f) result += 1f;
            return result;
        }
    }
}