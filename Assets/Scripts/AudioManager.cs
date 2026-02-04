using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class AudioEntry
    {
        public AudioClip Clip;
        [Range(0f, 1f)]
        public float Volume = 1.0f;
    }

    public static AudioManager Instance { get; private set; }

    [Header("Audio Config")]
    public List<AudioEntry> AudioList = new List<AudioEntry>();

    [Header("Audio Source")]
    public AudioSource SfxSource;

    private Dictionary<string, AudioEntry> mAudioMap;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        BuildAudioMap();
    }

    private void BuildAudioMap()
    {
        mAudioMap = new Dictionary<string, AudioEntry>(AudioList.Count);

        for (int i = 0; i < AudioList.Count; ++i)
        {
            AudioEntry entry = AudioList[i];
            if (entry == null)
                continue;

            if (entry.Clip == null)
                continue;

            if (mAudioMap.ContainsKey(entry.Clip.name))
            {
                Debug.LogWarning($"AudioManager: Duplicate audio name '{entry.Clip.name}'");
                continue;
            }

            mAudioMap.Add(entry.Clip.name, entry);
        }
    }

    public void Play(string name)
    {
        if (string.IsNullOrEmpty(name))
            return;

        if (!mAudioMap.TryGetValue(name, out AudioEntry entry))
        {
            Debug.LogWarning($"AudioManager: Audio '{name}' not found");
            return;
        }

        SfxSource.PlayOneShot(entry.Clip, entry.Volume);
    }

    public bool HasAudio(string name)
    {
        return mAudioMap != null && mAudioMap.ContainsKey(name);
    }
}