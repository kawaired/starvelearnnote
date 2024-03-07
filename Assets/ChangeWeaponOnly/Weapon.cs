using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public SkeletonAnimation willow_ani;
    public SpineAtlasAsset weapon_asset;
    public SpineAtlasAsset ham_bat_asset;
    public Atlas test_atlas;

    private Atlas weapon_atlas;
    private AtlasRegion reg_spear;
    private RegionAttachment spear; 
    // Start is called before the first frame update
    void Start()
    {
        weapon_atlas = weapon_asset.GetAtlas();
        reg_spear = weapon_atlas.FindRegion("swap_spear/swap_spear-0");
        spear = reg_spear.ToRegionAttachment(reg_spear.name);
        willow_ani.skeleton.FindSlot("weapon_slot").Attachment = spear;
        willow_ani.skeleton.FindSlot("weapon_slot").Attachment = null;
        Atlas ham_atlas = ham_bat_asset.GetAtlas();
        AtlasRegion ham_reg = ham_atlas.FindRegion("swap_ham_bat/swap_ham_bat-0");
        RegionAttachment ham = ham_reg.ToRegionAttachment(reg_spear.name);
        willow_ani.skeleton.FindSlot("ham_slot").Attachment = ham;
        //willow_ani.skeleton.FindSlot("weapon_slot").Attachment = null;
    }
}
