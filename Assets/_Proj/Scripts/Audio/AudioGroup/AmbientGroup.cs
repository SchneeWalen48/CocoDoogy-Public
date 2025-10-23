using UnityEngine;
using UnityEngine.Audio;

public class AmbientGroup : AudioGroupBase
{
    [Header("Pooling Settings")]
    [SerializeField] private int poolSize = 5;

    private AudioMixer mixer;
    private AudioMixerGroup group;
    private AmbientPlayer player;
    private AudioPool audioPool;

    private void Awake()
    {
        mixer = AudioManager.AudioGroupProvider.GetMixer();
        group = AudioManager.AudioGroupProvider.GetGroup(AudioType.Ambient);
        Debug.Log($"AmbientGroup : {group}");
        audioPool = new AudioPool(transform, group, poolSize);
        player = new AmbientPlayer(mixer, transform, audioPool);
    }

    // 오디오 실행
    public void PlayAmbient(AudioClip clip, bool loop, bool pooled, Vector3? pos = null)
    {
        player.PlayAudio(clip, group, loop, pooled, pos);
    }

    public override void Play()
    {
        base.Play();
    }

    public override void Pause()
    {
        base .Pause();
    }

    public override void Resume()
    {
        base.Resume();
    }

    public override void Stop()
    {
        base.Stop();
    }

    public override void ResetGroup()
    {
        base.ResetGroup();
    }
}
