using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private Dictionary<string, GameObject> _soundPrefabs = new();
    [SerializeField] private List<SoundPrefab> _soundPrefabsList;

    private void Awake()
    {
        foreach (var sound in _soundPrefabsList)
        {
            if (!_soundPrefabs.ContainsKey(sound.Key))
            {
                _soundPrefabs.Add(sound.Key, sound.Prefab);
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
        // Dictionary içinde döngü yapmak yerine doðrudan anahtar ile sorgulama yapýlýr.
        if (_soundPrefabs.TryGetValue(key, out GameObject prefabToSpawn))
        {
            LeanPool.Spawn(prefabToSpawn, transform.position, Quaternion.identity, this.transform);
        }
        else
        {
            Debug.LogWarning($"Sound key not found: {key}");
        }
    }
}

[System.Serializable]
public struct SoundPrefab
{
    public string Key;
    public GameObject Prefab; // Havuza alýnacak bu prefabýn üzerinde bir AudioSource bileþeni olmalýdýr.
}