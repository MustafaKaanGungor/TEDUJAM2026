using Lean.Pool;
using System.Collections;
using UnityEngine;

public class SFXDespawn : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();

        // Klip uzunluðu kadar bekle ve havuza geri gönder.
        LeanPool.Despawn(gameObject, clip.length);
    }
}
