namespace eLib.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class MathHelper
    {


        public const float RadianPi = 57.29578f; // 180.0 / Math.PI
        public const float DegreePi = 0.01745329f; // Math.PI / 180.0
        public const float TwoPi = 6.28319f; // Math.PI * 2


        public static float RadianToDegree(float radian)
        {
            return radian * RadianPi;
        }

        public static float DegreeToRadian(float degree)
        {
            return degree * DegreePi;
        }

       



    }
}
