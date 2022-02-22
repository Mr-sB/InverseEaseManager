using System;
using DG.Tweening;
using UnityEngine;

namespace GameUtil
{
    public class IntegralEaseManager
    {
        public const float PI = 3.141593f;
        public const float PIOver2 = PI * 0.5f;
        public const float TwoOverPI = 2 / PI;
        //10 * Math.Log(2) = 6.9314718
        private const float Tenln2 = 6.9314718f;
        private const float OneOver3 = 0.3333333333f;

        /// <param name="from">between 0 and 1</param>
        /// <param name="to">between 0 and 1</param>
        public static float GetDefiniteIntegral(Ease ease, float from, float to)
        {
            //跨越了0.5的临界点
            if ((from < 0.5f || to < 0.5f) && (from >= 0.5f || to >= 0.5f))
            {
                bool negative;
                float min, max;
                if (from < to)
                {
                    negative = false;
                    min = from;
                    max = to;
                }
                else
                {
                    negative = true;
                    min = to;
                    max = from;
                }
                  
                float? value = null;
                switch (ease)
                {
                    case Ease.InOutQuad:
                        value = InOutQuadPrimitiveFunctionLeft(0.5f) - InOutQuadPrimitiveFunctionLeft(min) +
                            InOutQuadPrimitiveFunctionRight(max) - InOutQuadPrimitiveFunctionRight(0.5f);
                        break;
                    case Ease.InOutCubic:
                        value = InOutCubicPrimitiveFunctionLeft(0.5f) - InOutCubicPrimitiveFunctionLeft(min) +
                            InOutCubicPrimitiveFunctionRight(max) - InOutCubicPrimitiveFunctionRight(0.5f);
                        break;
                    case Ease.InOutQuart:
                        value = InOutQuartPrimitiveFunctionLeft(0.5f) - InOutQuartPrimitiveFunctionLeft(min) +
                            InOutQuartPrimitiveFunctionRight(max) - InOutQuartPrimitiveFunctionRight(0.5f);
                        break;
                    case Ease.InOutQuint:
                        value = InOutQuintPrimitiveFunctionLeft(0.5f) - InOutQuintPrimitiveFunctionLeft(min) +
                            InOutQuintPrimitiveFunctionRight(max) - InOutQuintPrimitiveFunctionRight(0.5f);
                        break;
                    case Ease.InOutExpo:
                        value = InOutExpoPrimitiveFunctionLeft(0.5f) - InOutExpoPrimitiveFunctionLeft(min) +
                            InOutExpoPrimitiveFunctionRight(max) - InOutExpoPrimitiveFunctionRight(0.5f);
                        break;
                    case Ease.InOutCirc:
                        value = InOutCircPrimitiveFunctionLeft(0.5f) - InOutCircPrimitiveFunctionLeft(min) +
                            InOutCircPrimitiveFunctionRight(max) - InOutCircPrimitiveFunctionRight(0.5f);
                        break;
                }

                if (value.HasValue)
                    return negative ? -value.Value: value.Value;
            }
            
            var primitiveFunction = ToPrimitiveFunction(ease);
            return primitiveFunction(to) - primitiveFunction(from);
        }
        
        public static Func<float, float> ToPrimitiveFunction(Ease ease)
        {
            switch (ease)
            {
                case Ease.Unset:
                case Ease.Linear:
                    return LinearPrimitiveFunction;
                case Ease.InSine:
                    return InSinePrimitiveFunction;
                case Ease.OutSine:
                    return OutSinePrimitiveFunction;
                case Ease.InOutSine:
                    return InOutSinePrimitiveFunction;
                case Ease.InQuad:
                    return InQuadPrimitiveFunction;
                case Ease.OutQuad:
                    return OutQuadPrimitiveFunction;
                case Ease.InOutQuad:
                    return InOutQuadPrimitiveFunction;
                case Ease.InCubic:
                    return InCubicPrimitiveFunction;
                case Ease.OutCubic:
                    return OutCubicPrimitiveFunction;
                case Ease.InOutCubic:
                    return InOutCubicPrimitiveFunction;
                case Ease.InQuart:
                    return InQuartPrimitiveFunction;
                case Ease.OutQuart:
                    return OutQuartPrimitiveFunction;
                case Ease.InOutQuart:
                    return InOutQuartPrimitiveFunction;
                case Ease.InQuint:
                    return InQuintPrimitiveFunction;
                case Ease.OutQuint:
                    return OutQuintPrimitiveFunction;
                case Ease.InOutQuint:
                    return InOutQuintPrimitiveFunction;
                case Ease.InExpo:
                    return InExpoPrimitiveFunction;
                case Ease.OutExpo:
                    return OutExpoPrimitiveFunction;
                case Ease.InOutExpo:
                    return InOutExpoPrimitiveFunction;
                case Ease.InCirc:
                    return InCircPrimitiveFunction;
                case Ease.OutCirc:
                    return OutCircPrimitiveFunction;
                case Ease.InOutCirc:
                    return InOutCircPrimitiveFunction;
                default:
                    Debug.LogError("Not support " + ease);
                    // OutQuad
                    return OutQuadPrimitiveFunction;
            }
        }

        /// <param name="value">between 0 and 1</param>
        public static float LinearPrimitiveFunction(float value)
        {
            return value * value * 0.5f;
        }

        public static float InSinePrimitiveFunction(float value)
        {
            return -TwoOverPI * (float) Math.Sin(value * PIOver2) + value;
        }

        public static float OutSinePrimitiveFunction(float value)
        {
            return -TwoOverPI * (float) Math.Cos(value * PIOver2);
        }

        public static float InOutSinePrimitiveFunction(float value)
        {
            return -(1f / 2 * PI) * (float) Math.Sin(value * PI) - value;
        }

        public static float InQuadPrimitiveFunction(float value)
        {
            return OneOver3 * value * value * value;
        }
        
        public static float OutQuadPrimitiveFunction(float value)
        {
            return value * value - OneOver3 * value * value * value;
        }

        public static float InOutQuadPrimitiveFunction(float value)
        {
            return value < 0.5f ? InOutQuadPrimitiveFunctionLeft(value) : InOutQuadPrimitiveFunctionRight(value);
        }
        
        public static float InOutQuadPrimitiveFunctionLeft(float value)
        {
            return 2 * OneOver3 * value * value * value;
        }
        
        public static float InOutQuadPrimitiveFunctionRight(float value)
        {
            return -2 * OneOver3 * value * value * value + 2 * value * value - value;
        }

        public static float InCubicPrimitiveFunction(float value)
        {
            return 0.25f * (float) Math.Pow(value, 4);
        }

        public static float OutCubicPrimitiveFunction(float value)
        {
            return 0.25f * (float) Math.Pow(value - 1, 4) + value;
        }

        public static float InOutCubicPrimitiveFunction(float value)
        {
            return value < 0.5f ? InOutCubicPrimitiveFunctionLeft(value) : InOutCubicPrimitiveFunctionRight(value);
        }
        
        public static float InOutCubicPrimitiveFunctionLeft(float value)
        {
            return (float) Math.Pow(value, 4);
        }
        
        public static float InOutCubicPrimitiveFunctionRight(float value)
        {
            return (float) Math.Pow(value - 1, 4) + value;
        }

        public static float InQuartPrimitiveFunction(float value)
        {
            return 0.2f * (float) Math.Pow(value, 5);
        }

        public static float OutQuartPrimitiveFunction(float value)
        {
            return value - 0.2f * (float) Math.Pow(value - 1, 5);
        }

        public static float InOutQuartPrimitiveFunction(float value)
        {
            return value < 0.5f ? InOutQuartPrimitiveFunctionLeft(value) : InOutQuartPrimitiveFunctionRight(value);
        }
        
        public static float InOutQuartPrimitiveFunctionLeft(float value)
        {
            return 1.6f * (float) Math.Pow(value, 5);
        }
        
        public static float InOutQuartPrimitiveFunctionRight(float value)
        {
            return 1.6f * (float) Math.Pow(value - 1, 5) + value;
        }

        public static float InQuintPrimitiveFunction(float value)
        {
            return OneOver3 * 0.5f * (float) Math.Pow(value, 6);
        }

        public static float OutQuintPrimitiveFunction(float value)
        {
            return OneOver3 * 0.5f * (float) Math.Pow(value - 1, 6) + value;
        }

        public static float InOutQuintPrimitiveFunction(float value)
        {
            return value < 0.5f ? InOutQuintPrimitiveFunctionLeft(value) : InOutQuintPrimitiveFunctionRight(value);
        }
        
        public static float InOutQuintPrimitiveFunctionLeft(float value)
        {
            return 8 * OneOver3 * (float) Math.Pow(value, 6);
        }
        
        public static float InOutQuintPrimitiveFunctionRight(float value)
        {
            return 8 * OneOver3 * (float) Math.Pow(value - 1, 6) + value;
        }

        public static float InExpoPrimitiveFunction(float value)
        {
            return (float) Math.Pow(2, 10 * (value - 1)) / Tenln2;
        }

        public static float OutExpoPrimitiveFunction(float value)
        {
            return (float) Math.Pow(2, -10 * value) / Tenln2 + value;
        }

        public static float InOutExpoPrimitiveFunction(float value)
        {
            return value < 0.5 ? InOutExpoPrimitiveFunctionLeft(value) : InOutExpoPrimitiveFunctionRight(value);
        }
        
        public static float InOutExpoPrimitiveFunctionLeft(float value)
        {
            return (float) Math.Pow(2, 10 * (2 * value - 1)) / (4 * Tenln2);
        }
        
        public static float InOutExpoPrimitiveFunctionRight(float value)
        {
            return value - (float) Math.Pow(2, -10 * (2 * value - 1)) / (4 * Tenln2);
        }

        public static float InCircPrimitiveFunction(float value)
        {
            return -0.5f * ((float) Math.Asin(value) + (float) Math.Sqrt(1 - value) * value * (float) Math.Sqrt(value + 1) - 2 * value);
        }

        public static float OutCircPrimitiveFunction(float value)
        {
            return 0.5f * ((float) Math.Sqrt(2 - value) * (value - 1) * (float) Math.Sqrt(value) + (float) Math.Asin(value - 1));
        }

        public static float InOutCircPrimitiveFunction(float value)
        {
            return value < 0.5 ? InOutCircPrimitiveFunctionLeft(value) : InOutCircPrimitiveFunctionRight(value);
        }
        
        public static float InOutCircPrimitiveFunctionLeft(float value)
        {
            return -0.125f * ((float) Math.Asin(2 * value) + 2 * (float) Math.Sqrt(1 - 2 * value) * value * (float) Math.Sqrt(2 * value + 1) -
                              4 * value);
        }
        
        public static float InOutCircPrimitiveFunctionRight(float value)
        {
            return 0.125f * ((float) Math.Asin(2 * value - 2) + (float) Math.Sqrt(1 - 2 * value) * (float) Math.Sqrt(2 * value - 3) * (2 * value - 2) +
                            4 * value);
        }
    }
}