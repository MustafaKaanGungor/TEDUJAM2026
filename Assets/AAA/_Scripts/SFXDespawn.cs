using Lean.Pool;
using System.Collections;
using UnityEngine;

public class SFXDespawn : MonoBehaviour
{
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(DespawnAfterAudio());
    }

    IEnumerator DespawnAfterAudio()
    {
        yield return new WaitForSeconds(_audioSource.clip.length);
        LeanPool.Despawn(gameObject);
    }

}
