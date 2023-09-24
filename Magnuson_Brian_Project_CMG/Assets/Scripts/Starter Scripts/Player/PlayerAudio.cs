using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    //Here is where you can add more Audioclips if you want 
    [Header("Audio Clips for the Player")]
    public AudioClip WalkAudioClip;
    public bool LoopWalkAudio = false;
    public AudioClip AttackAudioClip;
    public bool LoopAttackAudio = false;
    public AudioClip DeathAudioClip;
    public bool LoopDeathAudio = false;
    public AudioClip JumpAudioClip;
    public bool LoopJumpAudio = false;

    [Range(0,1)]
    public float VolumeLevel = 1;


    //And here is where you should create the respective AudioSource
    [HideInInspector] public AudioSource WalkSource;
    [HideInInspector] public AudioSource AttackSource;
    [HideInInspector] public AudioSource DeathSource;
    [HideInInspector] public AudioSource JumpSource;
    //The whole [HideInInspector] thing just makes it so that way you can't see these public variables in editor

    void Start()
    {
        SetUpAudio();
    }

    //Here is where you can add more audio sources and the like
    void SetUpAudio()
    {
        //First you have to make a new GameObject with a name
        GameObject WalkGameObject = new GameObject("WalkAudioSource");
        GameObject AttackGameObject = new GameObject("AttackAudioSource");
        GameObject DeathGameObject = new GameObject("DeathAudioSource");
        GameObject JumpGameObject = new GameObject("JumpAudioSource");

        //Next you have to Assign the parent so it's all organized
        AssignParent(WalkGameObject);
        AssignParent(AttackGameObject);
        AssignParent(DeathGameObject);
        AssignParent(JumpGameObject);

        //Then you have to add the actual audiosource to each gameobject
        WalkSource = WalkGameObject.AddComponent<AudioSource>();
        AttackSource = AttackGameObject.AddComponent<AudioSource>();
        DeathSource = DeathGameObject.AddComponent<AudioSource>();
        JumpSource = DeathGameObject.AddComponent<AudioSource>();
        //And finally you assign the clip to the audio source
        WalkSource.clip = WalkAudioClip;
        AttackSource.clip = AttackAudioClip;
        DeathSource.clip = DeathAudioClip;
        JumpSource.clip = JumpAudioClip;

        //And here is just where we assign the global volume level, you can make these individualized if you want
        WalkSource.volume = VolumeLevel;
        AttackSource.volume = VolumeLevel;
        DeathSource.volume = VolumeLevel;
        JumpSource.volume = VolumeLevel;

        WalkSource.loop = LoopWalkAudio;
        AttackSource.loop = LoopAttackAudio;
        DeathSource.loop = LoopDeathAudio;
        JumpSource.loop = LoopJumpAudio;
    }

    //Just a helper function that assigns whatever object as a child of this gameObject
    void AssignParent(GameObject obj)
    {
        obj.transform.parent = transform;
    }
}
