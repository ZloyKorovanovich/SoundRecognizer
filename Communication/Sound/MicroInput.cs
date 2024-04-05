using DeadlyMath;
using System.Collections;
using UnityEngine;

namespace Communication
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(VoiceSource))]
    public class MicroInput : MonoBehaviour
    {
        public int clipLength = 10;
        public int soundLength = 30;

        private AudioSource _audio;
        private VoiceSource _voice;

        private float[] _sounds;
        private int _iterator;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            _voice = GetComponent<VoiceSource>();
        }

        private void Start()
        {
            _sounds = new float[soundLength];
            _iterator = 0;

            var micro = Microphone.devices[0];
            var clip = Microphone.Start(micro, true, clipLength, AudioSettings.outputSampleRate);
            _audio.clip = clip;

            StartCoroutine(startDelay());
        }

        private void FixedUpdate()
        {
            if (_audio.isPlaying)
                Detect();
        }

        private void Detect()
        {
            var spectrum = new float[128];

            _audio.GetSpectrumData(spectrum, 0, FFTWindow.Triangle);
            SoundInfo.GetSpectrumSound(spectrum, out var sound);

            _sounds[_iterator] = sound;
            _iterator++;
            if (_iterator >= _sounds.Length)
                CombineWord();
        }

        private void CombineWord()
        {
            var median = Statistics.GetMedian(_sounds);
            Debug.Log(median);
            SpeechBase.instance.GetWord(median, out var word);
            //_voice.Say(word);

            _iterator = 0;
        }
        private IEnumerator startDelay()
        {
            yield return new WaitForSeconds(0.1f);
            _audio.Play();
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }

}

