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
                        value = InOutQuadIntegralFunctionLeft(0.5f) - InOutQuadIntegralFunctionLeft(min) +
                            InOutQuadIntegralFunctionRight(max) - InOutQuadIntegralFunctionRight(0.5f);
                        break;
                    case Ease.InOutCubic:
                        value = InOutCubicIntegralFunctionLeft(0.5f) - InOutCubicIntegralFunctionLeft(min) +
                            InOutCubicIntegralFunctionRight(max) - InOutCubicIntegralFunctionRight(0.5f);
                        break;
                    case Ease.InOutQuart:
                        value = InOutQuartIntegralFunctionLeft(0.5f) - InOutQuartIntegralFunctionLeft(min) +
                            InOutQuartIntegralFunctionRight(max) - InOutQuartIntegralFunctionRight(0.5f);
                        break;
                    case Ease.InOutQuint:
                        value = InOutQuintIntegralFunctionLeft(0.5f) - InOutQuintIntegralFunctionLeft(min) +
                            InOutQuintIntegralFunctionRight(max) - InOutQuintIntegralFunctionRight(0.5f);
                        break;
                    case Ease.InOutExpo:
                        value = InOutExpoIntegralFunctionLeft(0.5f) - InOutExpoIntegralFunctionLeft(min) +
                            InOutExpoIntegralFunctionRight(max) - InOutExpoIntegralFunctionRight(0.5f);
                        break;
                    case Ease.InOutCirc:
                        value = InOutCircIntegralFunctionLeft(0.5f) - InOutCircIntegralFunctionLeft(min) +
                            InOutCircIntegralFunctionRight(max) - InOutCircIntegralFunctionRight(0.5f);
                        break;
                }

                if (value.HasValue)
                    return negative ? -value.Value: value.Value;
            }
            
            var primitiveFunction = GetIntegralFunction(ease);
            return primitiveFunction(to) - primitiveFunction(from);
        }
        
        public static Func<float, float> GetIntegralFunction(Ease ease)
        {
            switch (ease)
            {
                case Ease.Unset:
                case Ease.Linear:
                    return LinearIntegralFunction;
                case Ease.InSine:
                    return InSineIntegralFunction;
                case Ease.OutSine:
                    return OutSineIntegralFunction;
                case Ease.InOutSine:
                    return InOutSineIntegralFunction;
                case Ease.InQuad:
                    return InQuadIntegralFunction;
                case Ease.OutQuad:
                    return OutQuadIntegralFunction;
                case Ease.InOutQuad:
                    return InOutQuadIntegralFunction;
                case Ease.InCubic:
                    return InCubicIntegralFunction;
                case Ease.OutCubic:
                    return OutCubicIntegralFunction;
                case Ease.InOutCubic:
                    return InOutCubicIntegralFunction;
                case Ease.InQuart:
                    return InQuartIntegralFunction;
                case Ease.OutQuart:
                    return OutQuartIntegralFunction;
                case Ease.InOutQuart:
                    return InOutQuartIntegralFunction;
                case Ease.InQuint:
                    return InQuintIntegralFunction;
                case Ease.OutQuint:
                    return OutQuintIntegralFunction;
                case Ease.InOutQuint:
                    return InOutQuintIntegralFunction;
                case Ease.InExpo:
                    return InExpoIntegralFunction;
                case Ease.OutExpo:
                    return OutExpoIntegralFunction;
                case Ease.InOutExpo:
                    return InOutExpoIntegralFunction;
                case Ease.InCirc:
                    return InCircIntegralFunction;
                case Ease.OutCirc:
                    return OutCircIntegralFunction;
                case Ease.InOutCirc:
                    return InOutCircIntegralFunction;
                default:
                    Debug.LogError("Not support " + ease);
                    // OutQuad
                    return OutQuadIntegralFunction;
            }
        }

        /// <param name="value">between 0 and 1</param>
        public static float LinearIntegralFunction(float value)
        {
            return value * value * 0.5f;
        }

        public static float InSineIntegralFunction(float value)
        {
            return -TwoOverPI * (float) Math.Sin(value * PIOver2) + value;
        }

        public static float OutSineIntegralFunction(float value)
        {
            return -TwoOverPI * (float) Math.Cos(value * PIOver2);
        }

        public static float InOutSineIntegralFunction(float value)
        {
            return -(1f / 2 * PI) * (float) Math.Sin(value * PI) - value;
        }

        public static float InQuadIntegralFunction(float value)
        {
            return OneOver3 * value * value * value;
        }
        
        public static float OutQuadIntegralFunction(float value)
        {
            return value * value - OneOver3 * value * value * value;
        }

        public static float InOutQuadIntegralFunction(float value)
        {
            return value < 0.5f ? InOutQuadIntegralFunctionLeft(value) : InOutQuadIntegralFunctionRight(value);
        }
        
        public static float InOutQuadIntegralFunctionLeft(float value)
        {
            return 2 * OneOver3 * value * value * value;
        }
        
        public static float InOutQuadIntegralFunctionRight(float value)
        {
            return -2 * OneOver3 * value * value * value + 2 * value * value - value;
        }

        public static float InCubicIntegralFunction(float value)
        {
            return 0.25f * (float) Math.Pow(value, 4);
        }

        public static float OutCubicIntegralFunction(float value)
        {
            return 0.25f * (float) Math.Pow(value - 1, 4) + value;
        }

        public static float InOutCubicIntegralFunction(float value)
        {
            return value < 0.5f ? InOutCubicIntegralFunctionLeft(value) : InOutCubicIntegralFunctionRight(value);
        }
        
        public static float InOutCubicIntegralFunctionLeft(float value)
        {
            return (float) Math.Pow(value, 4);
        }
        
        public static float InOutCubicIntegralFunctionRight(float value)
        {
            return (float) Math.Pow(value - 1, 4) + value;
        }

        public static float InQuartIntegralFunction(float value)
        {
            return 0.2f * (float) Math.Pow(value, 5);
        }

        public static float OutQuartIntegralFunction(float value)
        {
            return value - 0.2f * (float) Math.Pow(value - 1, 5);
        }

        public static float InOutQuartIntegralFunction(float value)
        {
            return value < 0.5f ? InOutQuartIntegralFunctionLeft(value) : InOutQuartIntegralFunctionRight(value);
        }
        
        public static float InOutQuartIntegralFunctionLeft(float value)
        {
            return 1.6f * (float) Math.Pow(value, 5);
        }
        
        public static float InOutQuartIntegralFunctionRight(float value)
        {
            return 1.6f * (float) Math.Pow(value - 1, 5) + value;
        }

        public static float InQuintIntegralFunction(float value)
        {
            return OneOver3 * 0.5f * (float) Math.Pow(value, 6);
        }

        public static float OutQuintIntegralFunction(float value)
        {
            return OneOver3 * 0.5f * (float) Math.Pow(value - 1, 6) + value;
        }

        public static float InOutQuintIntegralFunction(float value)
        {
            return value < 0.5f ? InOutQuintIntegralFunctionLeft(value) : InOutQuintIntegralFunctionRight(value);
        }
        
        public static float InOutQuintIntegralFunctionLeft(float value)
        {
            return 8 * OneOver3 * (float) Math.Pow(value, 6);
        }
        
        public static float InOutQuintIntegralFunctionRight(float value)
        {
            return 8 * OneOver3 * (float) Math.Pow(value - 1, 6) + value;
        }

        public static float InExpoIntegralFunction(float value)
        {
            return (float) Math.Pow(2, 10 * (value - 1)) / Tenln2;
        }

        public static float OutExpoIntegralFunction(float value)
        {
            return (float) Math.Pow(2, -10 * value) / Tenln2 + value;
        }

        public static float InOutExpoIntegralFunction(float value)
        {
            return value < 0.5 ? InOutExpoIntegralFunctionLeft(value) : InOutExpoIntegralFunctionRight(value);
        }
        
        public static float InOutExpoIntegralFunctionLeft(float value)
        {
            return (float) Math.Pow(2, 10 * (2 * value - 1)) / (4 * Tenln2);
        }
        
        public static float InOutExpoIntegralFunctionRight(float value)
        {
            return value - (float) Math.Pow(2, -10 * (2 * value - 1)) / (4 * Tenln2);
        }

        public static float InCircIntegralFunction(float value)
        {
            return -0.5f * ((float) Math.Asin(value) + (float) Math.Sqrt(1 - value) * value * (float) Math.Sqrt(value + 1) - 2 * value);
        }

        public static float OutCircIntegralFunction(float value)
        {
            return 0.5f * ((float) Math.Sqrt(2 - value) * (value - 1) * (float) Math.Sqrt(value) + (float) Math.Asin(value - 1));
        }

        public static float InOutCircIntegralFunction(float value)
        {
            return value < 0.5 ? InOutCircIntegralFunctionLeft(value) : InOutCircIntegralFunctionRight(value);
        }
        
        public static float InOutCircIntegralFunctionLeft(float value)
        {
            return -0.125f * ((float) Math.Asin(2 * value) + 2 * (float) Math.Sqrt(1 - 2 * value) * value * (float) Math.Sqrt(2 * value + 1) -
                              4 * value);
        }
        
        public static float InOutCircIntegralFunctionRight(float value)
        {
            return 0.125f * ((float) Math.Asin(2 * value - 2) + (float) Math.Sqrt(1 - 2 * value) * (float) Math.Sqrt(2 * value - 3) * (2 * value - 2) +
                            4 * value);
        }
    }
}