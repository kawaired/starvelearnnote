using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class changewillowskin : MonoBehaviour
{
    public SkeletonAnimation haunteddoll_ani;
    public SkeletonAnimation ice_ani;

    public Button hauntclothbtn;
    public Button haunthairbtn;
    public Button hauntheadbtn;
    public Button iceclothbtn;
    public Button icehairbtn;
    public Button iceheadbtn;

    //[SpineAnimation]
    //public string testani;


    public  struct SkinData
    {
        public Spine.Skin cloth;
        public Spine.Skin hair;
        public Spine.Skin head;
    }

    private SkinData haunteddoll_data;
    private SkinData ice_data;
    private SkinData record_data;

    //private Spine.Skin defaultskin;
    // Start is called before the first frame update
    void Start()
    {
        haunteddoll_data = CreateSkinData(haunteddoll_ani);
        ice_data = CreateSkinData(ice_ani);
        Spine.Skin finalskin = new Spine.Skin("final");
        finalskin.CopySkin(haunteddoll_ani.skeleton.Data.FindSkin("default"));
        //finalskin.CopySkin(haunteddoll_data.empty);
        //finalskin.AddSkin(haunteddoll_data.cloth);
        //finalskin.AddSkin(haunteddoll_data.hair);
        //finalskin.AddSkin(haunteddoll_data.head);

        record_data = haunteddoll_data;
        SetTheSkin(haunteddoll_ani, finalskin, record_data);

        //SetTheSkin(haunteddoll_ani, finalskin);

        iceclothbtn.onClick.AddListener(()=>
        {
            record_data.cloth = ice_data.cloth;
            SetTheSkin(haunteddoll_ani, finalskin, record_data);
        });

        icehairbtn.onClick.AddListener(() =>
        {
            record_data.hair = ice_data.hair;
            SetTheSkin(haunteddoll_ani, finalskin, record_data);
        });

        iceheadbtn.onClick.AddListener(() =>
        {
            record_data.head = ice_data.head;
            SetTheSkin(haunteddoll_ani, finalskin, record_data);
        });

        hauntclothbtn.onClick.AddListener(()=>
        {
            record_data.cloth = haunteddoll_data.cloth;
            SetTheSkin(haunteddoll_ani, finalskin, record_data);
        });

        hauntheadbtn.onClick.AddListener(() =>
        {
            record_data.head = haunteddoll_data.head;
            SetTheSkin(haunteddoll_ani, finalskin, record_data);
        });

        haunthairbtn.onClick.AddListener(() =>
        {
            record_data.hair = haunteddoll_data.hair;
            SetTheSkin(haunteddoll_ani, finalskin, record_data);
        });
    }

    SkinData CreateSkinData(SkeletonAnimation ani)
    {
        SkinData temp= new SkinData() 
        {
            //empty=ani.skeleton.Data.FindSkin("default"),
            cloth= ani.skeleton.Data.FindSkin("cloth"),
            hair= ani.skeleton.Data.FindSkin("hair"),
            head= ani.skeleton.Data.FindSkin("head"),
            //head_hat= ani.skeleton.Data.FindSkin("head_hat")
        };
        return temp;
    }

    private void SetTheSkin(SkeletonAnimation theani, Spine.Skin theskin,SkinData skindata)
    {
        theskin.Clear();
        theskin.AddSkin(skindata.cloth);
        theskin.AddSkin(skindata.hair);
        theskin.AddSkin(skindata.head);
        theani.skeleton.SetSkin(theskin);
        theani.skeleton.UpdateCache();
        theani.skeleton.SetSlotsToSetupPose();
    }
}
