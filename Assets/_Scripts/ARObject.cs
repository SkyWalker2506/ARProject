using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARObject : MonoBehaviour
{
    public Animation Animation;

    GameObject animContHolder;
    public GameObject AnimationContentHolder
    {
        get
        {
            return animContHolder;
        }
        set
        {
            AnimationButton[] animationButtons = value.GetComponentsInChildren<AnimationButton>();
            if(animationButtons.Length>0)
            {
                animationButtons.ToList().ForEach(AB => AB.ARObj = this);
                animationButtons.ToList().GetRandom().AddAnimationClip(Animation);
            }
                animContHolder = value;
        }
    }
   
    public List<BodyPart> BodyParts = new List<BodyPart>();
    public Transform Root;
    public Animation Anim;
    public bool isRagdollOn;
    public bool isStandingUp;
    public AnimationClip LastPlayingClip;

    private void Awake()
    {
        Anim= GetComponent<Animation>();
        SetBodyParts();
        Root = BodyParts.Find(bp => bp.GetComponent<CharacterJoint>() == null).transform;
        MainManager.Instance.SelectedIndicator.SelectedObject = this;
        isRagdollOn = false;
        ToggleRagdoll(isRagdollOn);
    }

    void SetBodyParts()
    {
        GetComponentsInChildren<Transform>().ToList().ForEach(T =>
        {
            if (T.GetComponent<Rigidbody>())
                BodyParts.Add(T.gameObject.AddComponent<BodyPart>());
        }
        );
        BodyParts.ForEach(bp => bp.InitializeBodyPart(bp.transform));
    }

    void ResetBodyParts()
    {
        transform.position = new Vector3(Root.position.x, transform.position.y, Root.position.z);
        BodyParts.ForEach(bp => bp.ResetTransform(bp.transform));

    }

    public void ToggleRagdoll(bool isOn)
    {
        isRagdollOn = isOn;
        ResetBodyParts();
        GetComponentsInChildren<Rigidbody>().ToList().ForEach(rb => rb.isKinematic = !isOn);
        GetComponentsInChildren<Collider>().ToList().ForEach(c => c.isTrigger = !isOn);
        GetComponent<Animation>().enabled = !isOn;
    }

    public IEnumerator StandUp()
    {
        isStandingUp = true;
        yield return new WaitForSecondsRealtime(2);
        ToggleRagdoll(false);
        AnimationClip Clip;

        if (!gameObject.name.Contains("vanguard"))
            Clip = MainManager.Instance.StandingUpAnims1.GetRandom();
        else
            Clip = MainManager.Instance.StandingUpAnims2.GetRandom();
        Debug.Log(Clip.name);
        Anim.AddClip(Clip, Clip.name);
        Anim.clip = Clip;
        Anim.Play();
      
        yield return new WaitForSecondsRealtime(Clip.length);
        isStandingUp = false;
        Anim.clip = LastPlayingClip;
        Anim.Play();
    }

}

public class BodyPart: MonoBehaviour
{
        Vector3 bodyPartPosition;
        Quaternion bodyPartRotation;

        public void InitializeBodyPart(Transform tr)
        {
            bodyPartPosition = tr.localPosition;
            bodyPartRotation = tr.localRotation;
        }

        public void ResetTransform(Transform tr)
        {
            tr.localPosition = bodyPartPosition;
            tr.localRotation = bodyPartRotation;
        }

}
