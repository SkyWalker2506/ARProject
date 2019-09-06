using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationDataHolder : MonoBehaviour
{
    public List<AnimationData> AnimationDatas;
}

[Serializable]
public class AnimationData
{
    public Sprite Sprite;
    public AnimationClip Clip;
}