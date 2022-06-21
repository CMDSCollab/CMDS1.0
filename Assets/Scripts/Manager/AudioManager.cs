using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public List<NamedAudio> audiosWrapper;
    public Dictionary<string, AudioClip> nameToClipDic;
    public Camera gameCamera
    {
        get
        {
            return FindObjectOfType<Camera>();
        }
    }

    public static AudioManager Instance  { get; private set; }


    private void Awake()
    {
        nameToClipDic = new Dictionary<string, AudioClip>();
        foreach (var namedAudio in audiosWrapper)
        {
            nameToClipDic.Add(namedAudio.audioClip.name, namedAudio.audioClip);
        }

        AudioManager.Instance = this;
    }


    public AudioClip GetClipFromName(string name)
    {
        if (nameToClipDic.ContainsKey(name))
        {
            return nameToClipDic[name];
        }
        else
        {
            Debug.LogError("Audio Not Found.");
            return null;
        }
    }

    public void PlayAudio(string audioName)
    {
        if (gameCamera == null)
        {
            Debug.LogError("Camera in AudioManager can not be found.");
            return;
        }
        else
        {
            AudioSource.PlayClipAtPoint(GetClipFromName(audioName), gameCamera.transform.position);
        }
    }
}

[System.Serializable]
public class NamedAudio
{
    public AudioClip audioClip;
}