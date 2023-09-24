using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class SpeakerLibrary : ScriptableObject
{
    
    public List<SpriteInfo> speakerLibrary = new List<SpriteInfo>();

    [System.Serializable]
    public class SpriteInfo
    {
        public string name;
        public Sprite sprite;
    }
}
