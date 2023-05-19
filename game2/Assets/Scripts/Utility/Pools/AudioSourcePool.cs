using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    public List<AudioSource> sources = new List<AudioSource>();
    private int originalCount;
    private float timer = 0;
    private float timeToDeleteSources = 5f;
    private bool areNewSourcesSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        sources = GetComponents<AudioSource>().ToList();
        originalCount = sources.Count;
    }
    private void Update()
    {
        if (!areNewSourcesSpawned) return;
        timer += Time.deltaTime;
        if (timer < timeToDeleteSources) return;
        for (int i = sources.Count - 1; i >= originalCount; i--)
        {
            Destroy(sources[i]);
            sources.Remove(sources[i]);
        }
    }
    public AudioSource GetSource()
    {
        foreach(AudioSource source in sources)
        {
            if (!source.isPlaying) return source;
        }
        timer = 0;
        return CreateNewSource();
    }

    private AudioSource CreateNewSource()
    {
        areNewSourcesSpawned = true;
        
        AudioSource source = gameObject.AddComponent<AudioSource>();
        sources.Add(source);
        return source;
    }
}
