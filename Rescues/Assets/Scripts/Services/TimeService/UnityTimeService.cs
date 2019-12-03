using System;
using UnityEngine;


namespace Rescues
{
    public sealed class UnityTimeService: Service
    {
        #region Fields

        int deltaTimeResetFrame;

        #endregion



        #region Class lifecycle

        public UnityTimeService(Contexts contexts) : base(contexts) { }

        #endregion



        #region ITimeService

        public float DeltaTime() => deltaTimeResetFrame == Time.frameCount ? 0.0f : Time.deltaTime;
        public float UnscaledDeltaTime() => deltaTimeResetFrame == Time.frameCount ? 0.0f : Time.unscaledDeltaTime;
        public float FixedDeltaTime() => deltaTimeResetFrame == Time.frameCount ? 0.0f : Time.fixedDeltaTime;
        public float RealtimeSinceStartup() => Time.realtimeSinceStartup;
        public float GameTime() => Time.time;


        public long Timestamp()
        {
            long timestamp = DateTime.UtcNow.ToUnixTimestamp();

            return timestamp;
        }


        public void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
        }


        public void ResetDeltaTime()
        {
            deltaTimeResetFrame = Time.frameCount;
        }

        #endregion
    }
}
