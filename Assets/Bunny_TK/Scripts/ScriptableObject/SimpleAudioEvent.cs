using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunny_TK.DataDriven
{
    [CreateAssetMenu(fileName ="SimpleAudioEvent", menuName = "Data Driven/Simple Audio Event")]
    public class SimpleAudioEvent : AudioEvent
    {
        public List<AudioClip> clips = new List<AudioClip>();
        public float volume = 1f;
        public float pitch = 1f;

        public override void Play(AudioSource source)
        {
            if (clips.Count <= 0) return;

            source.clip = clips.GetRandom();
            source.volume = volume;
            source.pitch = pitch;
            source.Play();
        }
    }
}
