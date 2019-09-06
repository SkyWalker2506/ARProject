using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationButton : MonoBehaviour
{
    public ARObject ARObj;
    public AnimationClip Clip;
  

    public void PlayAnimationClip()
    {
        if (ARObj.isRagdollOn)
            return;
            ARObj.GetComponentsInChildren<Rigidbody>().ToList().ForEach(rb => rb.isKinematic = true);
        ARObj.GetComponentsInChildren<Collider>().ToList().ForEach(c => c.isTrigger=true);
        AddAnimationClip(ARObj.Anim);
        ARObj.Anim.Play();
    }

    public void AddAnimationClip(Animation animation)
    {
        ARObj.LastPlayingClip = Clip;
        animation.AddClip(Clip, Clip.name);
        animation.clip = Clip;
    }

}
