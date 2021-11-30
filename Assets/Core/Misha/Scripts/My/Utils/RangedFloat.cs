using UnityEngine;

namespace DCFAEngine
{
    [System.Serializable]
    public class RangedFloat
    {
        public float min;
        public float length;
        public float max => min + length;

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

        public RangedFloat() { }
        public RangedFloat(float min, float length)
        {
            this.min = min;
            this.length = length;
        }
    }
}
