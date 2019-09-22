using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectsplayer : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public AudioClip clip;
        public float volume = 1.0f;

    }

    public Sound throwSound;
    public Sound shieldsound;
    public Sound coinsound;
    public Sound smalldmg;
    public Sound bigdmg;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();    
    }

    public void PlayThrow()
    {
        PlaySound(throwSound);
    }

    public void Playshield()
    {
        PlaySound(shieldsound);
    }

    public void Playcollect()
    {
        PlaySound(coinsound);
    }

    public void PlayMinordmg()
    {
        PlaySound(smalldmg);
    }

    public void PlayMajordmg()
    {
        PlaySound(bigdmg);
    }
    private void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(sound.clip, sound.volume);
    }
}
