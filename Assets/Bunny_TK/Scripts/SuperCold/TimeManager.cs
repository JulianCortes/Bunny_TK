using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Bunny_TK.Utils
{
    public class TimeManager : Singleton<TimeManager>
    {
        public float BaseTime { get { return Time.time; } }

        public float BaseDeltaTime { get { return Time.deltaTime; } }

        public float ScaledTime { get; private set; }

        public float ScaledDeltaTime { get { return Time.deltaTime * scaler; } }

        [Range(0f, 1f)]
        public float scaler;

        private void Update()
        {
            scaler = Mathf.Clamp01(Mathf.Max(Mathf.Abs(Player.Instance.controls.HorizzontalAxis), Mathf.Abs(Player.Instance.controls.VerticalAxis)) + 0.1f);
            ScaledTime += ScaledDeltaTime;
        }
    }
}