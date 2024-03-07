using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinController : MonoBehaviour
{
    public SkeletonAnimation rookobj;
    public SkeletonAnimation nightmareobj;

    public Button bodybtn;
    public Button headbtn;
    public Button shoulderbtn;

    //private bool bodystate = true;
    private bool headstate = true;
    private bool shoulderstate = true;

    Spine.Skin normalheadskin;
    Spine.Skin normalshoulderskin;
    //Spine.Skin normalbodyskin;
    Spine.Skin nightheadskin;
    Spine.Skin nightshoulderskin;
    //Spine.Skin nightbodyskin;
    Spine.Skin recordheadskin;
    Spine.Skin recordshoulderskin;
    //Spine.Skin recordbodyskin;
    Spine.Skin finalskin;

    // Start is called before the first frame update
    void Start()
    {
        normalheadskin = rookobj.skeleton.Data.FindSkin("head");
        normalshoulderskin = rookobj.skeleton.Data.FindSkin("shoulder");
        //normalbodyskin = rookobj.skeleton.Data.FindSkin("default");

        nightheadskin = nightmareobj.skeleton.Data.FindSkin("head");
        nightshoulderskin = nightmareobj.skeleton.Data.FindSkin("shoulder");
        //nightbodyskin = nightmareobj.skeleton.Data.FindSkin("default");

        //nightmareobj.gameObject.SetActive(false);

        finalskin = new Spine.Skin("final");
        Spine.Skin nightskin = new Spine.Skin("night");
        nightskin.AddSkin(nightheadskin);
        nightskin.AddSkin(nightshoulderskin);
        nightmareobj.skeleton.SetSkin(nightskin);
        nightmareobj.skeleton.UpdateCache();
        nightmareobj.skeleton.SetSlotsToSetupPose();
        finalskin.CopySkin(nightmareobj.skeleton.Data.FindSkin("head"));
        //finalskin.AddSkin(normalbodyskin);
        //finalskin.AddSkin(normalheadskin);
        finalskin.AddSkin(normalshoulderskin);
       
        recordheadskin = nightheadskin;
        //recordbodyskin = normalbodyskin;
        recordshoulderskin = normalshoulderskin;
        //finalskin.Clear();
        SetSkin();

        headbtn.onClick.AddListener(()=>
        {
            
            headstate = (!headstate);
            //Debug.Log(bodystate);
            SwitchHead(headstate);
        });

        shoulderbtn.onClick.AddListener(() => {
            shoulderstate = (!shoulderstate);
            SwitchShoulder(shoulderstate);
        });

        //rookobj = GetComponent<SkeletonAnimation>();
        //////Spine.Skin defaultskin = spineobj.skeleton.Data.FindSkin("default");
        //Spine.Skin headskin = rookobj.Skeleton.Data.FindSkin("head");
        //Spine.Skin shoulderskin = rookobj.skeleton.Data.FindSkin("shoulder");
        //Spine.Skin finalskin = new Spine.Skin("finalskin");
        //Spine.Skin defaultskin = rookobj.skeleton.Data.FindSkin("default");
        //finalskin.CopySkin(defaultskin);
        //finalskin.AddSkin(headskin);
        //finalskin.AddSkin(shoulderskin);

        //rookobj.skeleton.SetSkin(finalskin);
        //rookobj.skeleton.UpdateCache();
        //rookobj.skeleton.SetSlotsToSetupPose();
    }

    private void SetSkin()
    {
        rookobj.skeleton.SetSkin(finalskin);
        rookobj.skeleton.UpdateCache();
        rookobj.skeleton.SetSlotsToSetupPose();
    }

    //private void SwitchBody(bool skinstate)
    //{
    //    finalskin.Clear();
    //    if(skinstate)
    //    {
    //        finalskin.AddSkin(normalbodyskin);
    //    }
    //    else
    //    {
    //        finalskin.AddSkin(nightbodyskin);
    //    }
    //    finalskin.AddSkin(recordheadskin);
    //    finalskin.AddSkin(recordshoulderskin);

    //    SetSkin();
    //}
    private void SwitchHead(bool skinstate)
    {
        finalskin.Clear();
        if (skinstate)
        {
            finalskin.AddSkin(normalheadskin);
            recordheadskin = normalheadskin;
        }
        else
        {
            finalskin.AddSkin(nightheadskin);
            recordheadskin = nightheadskin;
        }
        //finalskin.AddSkin(recordbodyskin);
        finalskin.AddSkin(recordshoulderskin);

        SetSkin();
    }
    private void SwitchShoulder(bool skinstate)
    {
        finalskin.Clear();
        if (skinstate)
        {
            finalskin.AddSkin(normalshoulderskin);
            recordshoulderskin = normalshoulderskin;
        }
        else
        {
            finalskin.AddSkin(nightshoulderskin);
            recordshoulderskin = nightshoulderskin;
        }
        //finalskin.AddSkin(recordbodyskin);
        finalskin.AddSkin(recordheadskin);

        SetSkin();
    }
}
