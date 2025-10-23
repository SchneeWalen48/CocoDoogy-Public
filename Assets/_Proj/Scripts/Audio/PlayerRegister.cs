using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI는 깍두기 제어 X
/// 
/// BGM, Cutscene, Voice는 오디오소스 1개만 있으니
/// 이것만 제어하면 됨(클립교체하는 구조니 삭제 X)
/// 
/// SFX, Ambient는 Pool이 있는데 태그로 기본 생성 풀, 새로 생성 풀 비교 가능. 일반 풀 미사용 소리
/// 제어는 2가지, 새로 생성 풀은 초기화시 삭제하는 구조
/// 
/// 초기화 시 모든 오디오 소스의 클립을 뺌. 클립의 볼륨과 피치를 다시 초기화,
/// 일반 대화 시 캐릭터 소리 빼고는 모든 소리를 줄이길 원함. 절반정도.
/// </summary>
public abstract class PlayerRegister
{
    public List<AudioSource> activeSources = new List<AudioSource>();

    protected void Register(AudioSource src)
    {
        if (src == null) return;
        activeSources.Add(src);
    }

    protected void UnRegister(AudioSource src)
    {
        if (src == null) return;
        activeSources.Remove(src);
    }

}
