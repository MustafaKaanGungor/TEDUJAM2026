using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> _soundPrefabs = new();
    [SerializeField] private List<SoundClips> _soundPrefabsList;
    [SerializeField] private SFXDespawn _audioSourcePrefab; // Havuza alýnacak prefab, üzerinde bir AudioSource bileþeni içermelidir.
    private void Awake()
    {
        foreach (var sound in _soundPrefabsList)
        {
            if (!_soundPrefabs.ContainsKey(sound.Key))
            {
                _soundPrefabs.Add(sound.Key, sound.Clip);
            }
            else
            {
                Debug.LogWarning($"Duplicate Sound key detected: {sound.Key}. Skipping.");
            }
        }
    }

    private void OnEnable()
    {
        GameEvents.PlaySound += OnPlaySound;
    }

    private void OnDisable()
    {
        GameEvents.PlaySound -= OnPlaySound;
    }

    private void OnPlaySound(string key)
    {
        if (_soundPrefabs.TryGetValue(key, out AudioClip clipToPlay))
        {
            // Spawn iþlemi GetComponent yükünden kurtarýldý. Doðrudan AudioPlayer tipinde spawn ediliyor.
            SFXDespawn player = LeanPool.Spawn(_audioSourcePrefab, transform.position, Quaternion.identity, this.transform);
            player.PlaySound(clipToPlay);
        }
        else
        {
            Debug.LogWarning($"Sound key not found: {key}");
        }
    }
}

[System.Serializable]
public struct SoundClips
{
    public string Key;
    public AudioClip Clip; // Havuza alýnacak bu prefabýn üzerinde bir AudioSource bileþeni olmalýdýr.
}