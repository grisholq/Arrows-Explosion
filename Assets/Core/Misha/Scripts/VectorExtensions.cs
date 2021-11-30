using UnityEngine;

namespace DCFAEngine
{
    public static class Vector3Extension
    {
        public static Vector3 Direction(Vector3 startPosition, Vector3 finishPosition)
        {
            return Vector3.Normalize(finishPosition - startPosition);
        }
        #region Vector2
        public static Vector2 XY(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }
        public static Vector2 XZ(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }
        public static Vector2 YZ(this Vector3 vector)
        {
            return new Vector2(vector.y, vector.z);
        }

        public static Vector2 YX(this Vector3 vector)
        {
            return new Vector2(vector.y, vector.x);
        }
        public static Vector2 ZX(this Vector3 vector)
        {
            return new Vector2(vector.z, vector.x);
        }
        public static Vector2 ZY(this Vector3 vector)
        {
            return new Vector2(vector.z, vector.y);
        }
        #endregion
        #region Vector3
        public static Vector3 XYZ(this Vector3 vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }
        public static Vector3 XZY(this Vector3 vector)
        {
            return new Vector3(vector.x, vector.y, vector.y);
        }


        public static Vector3 YXZ(this Vector3 vector)
        {
            return new Vector3(vector.y, vector.x, vector.z);
        }
        public static Vector3 YZX(this Vector3 vector)
        {
            return new Vector3(vector.y, vector.z, vector.x);
        }

        
        public static Vector3 ZXY(this Vector3 vector)
        {
            return new Vector3(vector.z, vector.x, vector.y);
        }
        public static Vector3 ZYX(this Vector3 vector)
        {
            return new Vector3(vector.z, vector.y, vector.x);
        }
        #endregion

        public static Vector3 oYZ(this Vector3 vector)
        {
            return new Vector3(0f, vector.y, vector.z);
        }
        public static Vector3 XoZ(this Vector3 vector)
        {
            return new Vector3(vector.x, 0f, vector.z);
        }
        public static Vector3 XYo(this Vector3 vector)
        {
            return new Vector3(vector.x, vector.y, 0f);
        }
    }

    public static class Vector2Extension
    {
        #region Vector2
        public static Vector2 XX(this Vector2 vector)
        {
            return new Vector2(vector.x, vector.x);
        }
        public static Vector2 YY(this Vector2 vector)
        {
            return new Vector2(vector.y, vector.y);
        }
        public static Vector2 XY(this Vector2 vector)
        {
            return new Vector2(vector.x, vector.y);
        }
        public static Vector2 YX(this Vector2 vector)
        {
            return new Vector2(vector.y, vector.x);
        }
        #endregion
        #region Vector3
        public static Vector3 XYo(this Vector2 vector)
        {
            return new Vector3(vector.x, vector.y, 0f);
        }
        public static Vector3 XoY(this Vector2 vector)
        {
            return new Vector3(vector.x, 0f, vector.y);
        }
        public static Vector3 oXY(this Vector2 vector)
        {
            return new Vector3(0f, vector.x, vector.y);
        }

        public static Vector3 YXo(this Vector2 vector)
        {
            return new Vector3(vector.y, vector.x, 0f);
        }
        public static Vector3 YoX(this Vector2 vector)
        {
            return new Vector3(vector.y, 0f, vector.x);
        }
        public static Vector3 oYX(this Vector2 vector)
        {
            return new Vector3(0f, vector.y, vector.x);
        }
        #endregion
    }
}
