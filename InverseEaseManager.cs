using System;
using DG.Tweening;
using UnityEngine;

namespace GameUtil
{
    public static class InverseEaseManager
    {
        private const float PI = 3.141593f;
        private const float PIOver2 = PI * 0.5f;

        /// <summary>
        /// Returns the elapsed time between 0 and duration (inclusive) based on a value between 0 and duration (inclusive) and ease selected
        /// </summary>
        public static float Evaluate(Ease easeType, float value, float duration)
        {
            value = value < 0 ? 0 : value > 1 ? 1 : value;
            switch (easeType)
            {
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
                    if (value < 0.5f)
                        return (float) Math.Sqrt(value * 0.5f) * duration;
                    return (1 - (float) Math.Sqrt((1 - value) * 0.5f)) * duration;
                case Ease.InCubic:
                    return (float) Math.Pow(value, 0.3333333333) * duration;
                case Ease.OutCubic:
                    return (1 - (float) Math.Pow(1 - value, 0.3333333333)) * duration;
                case Ease.InOutCubic:
                    if (value < 0.5f)
                        return (float) Math.Pow(2 * value, 0.3333333333) * 0.5f * duration;
                    return (1 - (float) Math.Pow(2 * (1 - value), 0.3333333333) * 0.5f) * duration;
                case Ease.InQuart:
                    return (float) Math.Pow(value, 0.25) * duration;
                case Ease.OutQuart:
                    return (1 - (float) Math.Pow(1 - value, 0.25)) * duration;
                case Ease.InOutQuart:
                    if(value < 0.5f)
                        return (float) Math.Pow(2 * value, 0.25) * 0.5f * duration;
                    return (1 - (float) Math.Pow(2 * (1 - value), 0.25) * 0.5f) * duration;
                case Ease.InQuint:
                    return (float) Math.Pow(value, 0.2) * duration;
                case Ease.OutQuint:
                    return (1 - (float) Math.Pow(1 - value, 0.2)) * duration;
                case Ease.InOutQuint:
                    if(value < 0.5f)
                        return (float) Math.Pow(2 * value, 0.2) * 0.5f * duration;
                    return (1 - (float) Math.Pow(2 * (1 - value), 0.2) * 0.5f) * duration;
                case Ease.InExpo:
                    //10 * Math.Log(2) = 6.9314718
                    return value <= 0 ? 0 : (1 + (float) Math.Log(value) / 6.9314718f) * duration;
                case Ease.OutExpo:
                    return value >= 1 ? duration : -(float) Math.Log(1 - value) / 6.9314718f * duration;
                case Ease.InOutExpo:
                    if (value <= 0) return 0;
                    if (value >= 1) return duration;
                    if (value < 0.5f)
                        return (1 + (float) Math.Log(2 * value) / 6.9314718f) * 0.5f * duration;
                    return (1 - (float) Math.Log(2 * (1 - value)) / 6.9314718f) * 0.5f * duration; 
                case Ease.InCirc:
                    return (float) Math.Sqrt(1 - (1 - value) * (1 - value)) * duration;
                case Ease.OutCirc:
                    return (1 - (float) Math.Sqrt(1 - value * value)) * duration;
                case Ease.InOutCirc:
                    float tmp;
                    if (value < 0.5f)
                    {
                        tmp = 1 - 2 * value;
                        return (float) Math.Sqrt(1 - tmp * tmp) * 0.5f * duration;
                    }
                    tmp = 2 * value - 1;
                    return (1 - (float) Math.Sqrt(1 - tmp * tmp) * 0.5f) * duration;
                // Default
                default:
                    Debug.LogError("Not support " + easeType);
                    // OutQuad
                    return (1 - (float) Math.Sqrt(1 - value)) * duration;
            }
        }
    }
}