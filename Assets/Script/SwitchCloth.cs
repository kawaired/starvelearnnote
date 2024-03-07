using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCloth : MonoBehaviour
{
    public SkeletonAnimation woodarmor;
    public SkeletonAnimation grassarmor;
    public SkeletonAnimation willowani;

    public Button emptybtn;
    public Button woodbtn;
    public Button grassbtn;

    private MeshRenderer skinmesh;
    private Spine.Skin emptyskin;
    private Spine.Skin woodskin;
    private Spine.Skin grassskin;

    private Spine.Skin willowempty;
    private Spine.Skin willowleftnormal;
    private Spine.Skin willowlefttake;
    // Start is called before the first frame update
    void Start()
    {
        skinmesh = woodarmor.GetComponent<MeshRenderer>();
        emptyskin = woodarmor.skeleton.Data.FindSkin("empty");
        woodskin = woodarmor.skeleton.Data.FindSkin("cloth");
        grassskin = grassarmor.skeleton.Data.FindSkin("cloth");

        willowempty = willowani.skeleton.Data.FindSkin("default");
        willowleftnormal = willowani.skeleton.Data.FindSkin("left_arm_normal");
        willowlefttake = willowani.skeleton.Data.FindSkin("left_arm_take");

        Spine.Skin clothskin = new Spine.Skin("cloth");
        Spine.Skin willowskin = new Spine.Skin("willow");

        clothskin.CopySkin(emptyskin);
        SetTheSkin(woodarmor,clothskin);
        willowskin.CopySkin(willowempty);
        willowskin.AddSkin(willowleftnormal);
        //willowskin.AddSkin(willowlefttake);
        SetTheSkin(willowani, willowskin);

        emptybtn.onClick.AddListener(()=>
        {
            clothskin.Clear();
            clothskin.CopySkin(emptyskin);
            SetTheSkin(woodarmor,clothskin);
        });
        woodbtn.onClick.AddListener(() =>
        {
            clothskin.Clear();
            clothskin.CopySkin(woodskin);
            SetTheSkin(woodarmor,clothskin);
        });
        grassbtn.onClick.AddListener(()=>
        {
            clothskin.Clear();
            clothskin.CopySkin(grassskin);
            SetTheSkin(woodarmor,clothskin);
        });

        SkeletonRenderSeparator willowsparator = willowani.GetComponent<SkeletonRenderSeparator>();
        //willowsparator.SkeletonRenderer.separatorSlots
        foreach (var everypart in willowsparator.partsRenderers)
        {
            Debug.Log(everypart.MeshRenderer.sortingOrder);
        }
    }

    private void SetTheSkin(SkeletonAnimation theani ,Spine.Skin theskin)
    {
        theani.skeleton.SetSkin(theskin);
        theani.skeleton.UpdateCache();
        theani.skeleton.SetSlotsToSetupPose();
    }
}
