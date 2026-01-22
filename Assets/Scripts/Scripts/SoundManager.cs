using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AccuChekVRGame
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
        public AudioSource soundSource, crowdSoundSource, boundarySoundSource;
        public AudioClip[] soundClips;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        public void PlaySound(int clip)
        {
            soundSource.Stop();
            soundSource.clip = soundClips[clip];
            soundSource.Play();
        }
        public void PlayBoundarySound()
        {
            Debug.Log("commentary after boundary");
            boundarySoundSource.Stop();
            boundarySoundSource.Play();
        }
        public async void CrowdSound()
        {
            if (crowdSoundSource == null) return;
            crowdSoundSource.volume = 1f;
            await DelayedTask(5000, () => { crowdSoundSource.volume = 0.3f; });
            

        }
        private async Task DelayedTask(int delayTime, Action action) 
        {
            await Task.Delay(delayTime);
            action();
        }
    }


}
