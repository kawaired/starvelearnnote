using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class WearShoes : MonoBehaviour
{
    public SkeletonAnimation willowani;
    public SkeletonAnimation shoesani;

    private Spine.Skin willownormal;
    private Spine.Skin willowfoot;
    private Spine.Skin feet_shoes;
    // Start is called before the first frame update
    void Start()
    {
        willownormal = willowani.skeleton.Data.FindSkin("left_arm_normal");
        willowfoot = willowani.skeleton.Data.FindSkin("foot");
        feet_shoes = shoesani.skeleton.Data.FindSkin("foot");

        Spine.Skin finalskin = new Spine.Skin("final");
        finalskin.CopySkin(willownormal);
        //finalskin.AddSkin(willowfoot);
        //finalskin.CopySkin(feet_shoes);
        willowani.skeleton.SetSkin(finalskin);
        willowani.skeleton.UpdateCache();
        willowani.skeleton.SetSlotsToSetupPose();
    }
}
