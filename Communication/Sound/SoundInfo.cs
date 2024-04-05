using DeadlyMath;
using UnityEngine;

namespace Communication
{
    public static class SoundInfo
    {
        public static void GetSpectrumSound(float[] spectrum, out int sound)
        {
            var median = Statistics.GetMedian(spectrum) * 10000f;

            var closestPoint = 0;
            var closestDistance = Mathf.Abs(median - spectrum[0]);
            for (int i = 1; i < spectrum.Length; i++)
            {
                var distance = Mathf.Abs(median - spectrum[i]);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = i;
                }
            }

            sound = closestPoint;
        }
    }
}