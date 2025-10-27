using UnityEngine;

public class ObjectAnimationController
{
    private readonly Animator anim;
    public ObjectAnimationController(Animator anim)
    {
        this.anim = anim;
    }

    
    public void MoveAnim(float speed)
    {
        anim.SetFloat("Speed", speed);
    }
}
