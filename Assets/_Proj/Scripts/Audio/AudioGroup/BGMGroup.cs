using UnityEngine;
using UnityEngine.Audio;

public class BGMGroup : AudioGroupBase
{
    private AudioMixer mixer;
    private AudioMixerGroup group;
    private BGMPlayer player;
    private AudioSource audioS;

    private void Awake()
    {
        mixer = AudioManager.AudioGroupProvider.GetMixer();
        group = AudioManager.AudioGroupProvider.GetGroup(AudioType.BGM);
        Debug.Log($"BGMGroup : {group}");
        player = new BGMPlayer(mixer, transform);
    }

    // 오디오 실행
    public void PlayBGM(AudioClip clip, float fadeIn, float fadeOut, bool loop)
    {
        player.PlayAudio(clip, group, fadeIn, fadeOut, loop);
    }

    // 오디오 상태 제어
    public override void Play()
    {
        base.Play();
    }

    public override void Pause()
    {
        base.Pause();
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
