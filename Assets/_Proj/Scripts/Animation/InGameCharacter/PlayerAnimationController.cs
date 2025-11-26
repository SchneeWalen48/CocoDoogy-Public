using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator anim;
    public PlayerMovement move;
    public PlayerPush push;
    private WaitForSeconds startDelay;
    private WaitForSeconds nextDelay;
    private Coroutine currentCoroutine;
    private AudioSource src;

    private void Awake()
    {
        startDelay = new WaitForSeconds(UnityEngine.Random.Range(2f, 3f));
        nextDelay = new WaitForSeconds(10f);
    }

    private void Update()
    {
        bool isRunning = move.isRunning;
        if (isRunning && currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
        else if (!isRunning && currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(PlayCocoBreathingSound());
        }

        bool isPushing = push.isPushing;
        //float speed = move.rb.linearVelocity.magnitude;

        anim.SetBool("Push", isPushing);
        anim.SetBool("Run", isRunning && !isPushing);
        //anim.SetFloat("Speed", speed);
    }

    private IEnumerator PlayCocoBreathingSound()
    {
        while (true)
        {
            yield return startDelay;
            AudioEvents.Raise(SFXKey.InGameCocodoogy, 0, loop: false, pooled: false, pos: transform.position);
            AudioClip clip = AudioManager.Instance.LibraryProvider.GetClip(AudioType.SFX, SFXKey.InGameCocodoogy, 0);
            yield return clip.length;
            yield return nextDelay;
        }
    }
    // private IEnumerator PlayCocoBreathingSound()
    // {
    //     while (true)
    //     {
    //         yield return startDelay;
    //         int index = UnityEngine.Random.Range(1, 5);
    //         AudioEvents.Raise(SFXKey.InGameCocodoogy, index, loop: false, pooled: true, pos: transform.position);
    //         yield return nextDelay;
    //     }
    // }
    
}

