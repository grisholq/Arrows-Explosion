using UnityEngine;

namespace DCFAEngine
{
    [System.Serializable]
    public class RangedInt
    {
        public int min;
        public int length;
        public int max => min + length;

        public float GetRandom()
        {
            return Random.Range(min, max);
        }

        public float Evaluate(float prcent)
        {
            return min + length * prcent;
        }

        public float MathPercent(float value)
        {
            return value - min / length;
        }

        public RangedInt() { }
        public RangedInt(int min, int length)
        {
            this.min = min;
            this.length = length;
        }
    }
}
