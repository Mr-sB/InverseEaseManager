using System;
using DG.Tweening;
using UnityEngine;

namespace GameUtil
{
    public static class InverseEaseManager
    {
        private const float PI = 3.141593f;
        private const float PIOver2 = PI * 0.5f;
        //10 * Math.Log(2) = 6.9314718
        private const float Tenln2 = 6.9314718f;
        private const float OneOver3 = 0.3333333333f;

        /// <summary>
        /// Returns the elapsed time between 0 and duration (inclusive) based on a value between 0 and 1 (inclusive) and ease selected
        /// </summary>
        public static float Evaluate(Ease easeType, float value, float duration)
        {
            value = value < 0 ? 0 : value > 1 ? 1 : value;
            switch (easeType)
            {
                case Ease.Unset:
                case Ease.Linear:
                    return value * duration;
                case Ease.InSine:
                    return (float) Math.Acos(1 - value) / PIOver2 * duration;
                case Ease.OutSine:
                    return (float) Math.Asin(value) / PIOver2 * duration;
                case Ease.InOutSine:
                    return (float) Math.Acos(1 - 2 * value) / PI * duration;
                case Ease.InQuad:
                    return (float) Math.Sqrt(value) * duration;
                case Ease.OutQuad:
                    return (1 - (float) Math.Sqrt(1 - value)) * duration;
                case Ease.InOutQuad:
                    return value < 0.5f
                        ? (float) Math.Sqrt(value * 0.5f) * duration
                        : (1 - (float) Math.Sqrt((1 - value) * 0.5f)) * duration;
                case Ease.InCubic:
                    return (float) Math.Pow(value, OneOver3) * duration;
                case Ease.OutCubic:
                    return (1 - (float) Math.Pow(1 - value, OneOver3)) * duration;
                case Ease.InOutCubic:
                    return value < 0.5f
                        ? (float) Math.Pow(2 * value, OneOver3) * 0.5f * duration
                        : (1 - (float) Math.Pow(2 * (1 - value), OneOver3) * 0.5f) * duration;
                case Ease.InQuart:
                    return (float) Math.Pow(value, 0.25) * duration;
                case Ease.OutQuart:
                    return (1 - (float) Math.Pow(1 - value, 0.25)) * duration;
                case Ease.InOutQuart:
                    return value < 0.5f
                        ? (float) Math.Pow(2 * value, 0.25) * 0.5f * duration
                        : (1 - (float) Math.Pow(2 * (1 - value), 0.25) * 0.5f) * duration;
                case Ease.InQuint:
                    return (float) Math.Pow(value, 0.2) * duration;
                case Ease.OutQuint:
                    return (1 - (float) Math.Pow(1 - value, 0.2)) * duration;
                case Ease.InOutQuint:
                    return value < 0.5f
                        ? (float) Math.Pow(2 * value, 0.2) * 0.5f * duration
                        : (1 - (float) Math.Pow(2 * (1 - value), 0.2) * 0.5f) * duration;
                case Ease.InExpo:
                    return value <= 0 ? 0 : (1 + (float) Math.Log(value) / Tenln2) * duration;
                case Ease.OutExpo:
                    return value >= 1 ? duration : -(float) Math.Log(1 - value) / Tenln2 * duration;
                case Ease.InOutExpo:
                    if (value <= 0) return 0;
                    if (value >= 1) return duration;
                    return value < 0.5f
                        ? (1 + (float) Math.Log(2 * value) / Tenln2) * 0.5f * duration
                        : (1 - (float) Math.Log(2 * (1 - value)) / Tenln2) * 0.5f * duration;
                case Ease.InCirc:
                    return (float) Math.Sqrt(1 - (1 - value) * (1 - value)) * duration;
                case Ease.OutCirc:
                    return (1 - (float) Math.Sqrt(1 - value * value)) * duration;
                case Ease.InOutCirc:
                    float tmp = 2 * value - 1;
                    return value < 0.5f
                        ? (float) Math.Sqrt(1 - tmp * tmp) * 0.5f * duration
                        : (1 - (float) Math.Sqrt(1 - tmp * tmp) * 0.5f) * duration;
                // Default
                default:
                    Debug.LogError("Not support " + easeType);
                    // OutQuad
                    return (1 - (float) Math.Sqrt(1 - value)) * duration;
            }
        }
    }
}
