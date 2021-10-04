using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    public static Audio Instance { get; private set; }

    private AudioSource _source;

    private void Awake()
    {
        Instance = this;

        _source = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }
}