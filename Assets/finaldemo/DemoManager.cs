using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoManager : MonoBehaviour
{
    private struct SkinData
    {
        public Skin cloth;
        public Skin face;
        public Skin hair;
        public Skin head;
        public Skin normal_arm;
        public Skin item_arm;
    }
    [Range(0, 10)]
    public float movespeed = 0;

    public SkeletonAnimation player_ani;

    public SkeletonDataAsset idle_asset;
    public SkeletonDataAsset move_asset;

    public SkeletonAnimation haunted_idle_ani;
    public SkeletonAnimation haunted_move_ani;
    public SkeletonAnimation ice_idle_ani;
    public SkeletonAnimation ice_move_ani;

    public SpineAtlasAsset spear_asset;
    public SpineAtlasAsset torch_asset;

    public Button hauntedclothbtn;
    public Button hauntedfacebtn;
    public Button hauntedhairbtn;
    public Button hauntedheadbtn;

    public Button iceclothbtn;
    public Button icefacebtn;
    public Button icehairbtn;
    public Button iceheadbtn;

    public Button normalhandbtn;
    public Button takespearbtn;
    public Button taketorchbtn;

    private bool istakenothing;

    private SkinData haunted_idle_data;
    private SkinData haunted_move_data;
    private SkinData ice_idle_data;
    private SkinData ice_move_data;

    private SkinData record_idle_data;
    private SkinData record_move_data;

    private Skin idle_final_skin;
    private Skin move_final_skin;

    private RegionAttachment spear_attachment;
    private RegionAttachment torch_attachment;
    private RegionAttachment record_attachment;

    private string spear_address = "swap_spear/swap_spear-0";
    private string torch_address = "swap_torch/swap_torch-0";

    private float hmovement;
    private float vmovement;

    enum PlayerState
    {
        idle,
        move,
    }

    private PlayerState anistate;

    private void Start()
    {
        InitAni();

        normalhandbtn.onClick.AddListener(() =>
        {
            istakenothing = true;
            RefreshSkin();
        });

        takespearbtn.onClick.AddListener(() =>
        {
            istakenothing = false;
            record_attachment = spear_attachment;
            RefreshSkin();
        });

        taketorchbtn.onClick.AddListener(() =>
        {
            istakenothing = false;
            record_attachment = torch_attachment;
            RefreshSkin();
        });

        hauntedclothbtn.onClick.AddListener(() =>
        {
            record_idle_data.cloth = haunted_idle_data.cloth;
            record_move_data.cloth = haunted_move_data.cloth;
            RefreshSkin();
        });

        hauntedfacebtn.onClick.AddListener(() =>
        {
            record_idle_data.face = haunted_idle_data.face;
            record_move_data.face = haunted_move_data.face;
            RefreshSkin();
        });

        hauntedhairbtn.onClick.AddListener(() =>
        {
            record_idle_data.hair = haunted_idle_data.hair;
            record_move_data.hair = haunted_move_data.hair;
            RefreshSkin();
        });

        hauntedheadbtn.onClick.AddListener(() =>
        {
            record_idle_data.head = haunted_idle_data.head;
            record_move_data.head = haunted_move_data.head;
            RefreshSkin();
        });

        iceclothbtn.onClick.AddListener(() =>
        {
            record_idle_data.cloth = ice_idle_data.cloth;
            record_move_data.cloth = ice_move_data.cloth;
            RefreshSkin();
        });

        icefacebtn.onClick.AddListener(() =>
        {
            record_idle_data.face = ice_idle_data.face;
            record_move_data.face = ice_move_data.face;
            RefreshSkin();
        });

        icehairbtn.onClick.AddListener(() =>
        {
            record_idle_data.hair = ice_idle_data.hair;
            record_move_data.hair = ice_move_data.hair;
            RefreshSkin();
        });

        iceheadbtn.onClick.AddListener(() =>
        {
            record_idle_data.head = ice_idle_data.head;
            record_move_data.head = ice_move_data.head;
            RefreshSkin();
        });
    }

    private void InitAni()
    {
        istakenothing = true;
        haunted_idle_data = CreateSkinData(haunted_idle_ani);
        haunted_move_data = CreateSkinData(haunted_move_ani);
        ice_idle_data = CreateSkinData(ice_idle_ani);
        ice_move_data = CreateSkinData(ice_move_ani);
        idle_final_skin = new Skin("final_idle");
        move_final_skin = new Skin("final_move");
        record_idle_data = haunted_idle_data;
        record_move_data = haunted_move_data;
        SetSkinData(idle_final_skin, record_idle_data);
        SetSkinData(move_final_skin, record_move_data);
        UpdataSkin(player_ani, idle_final_skin);
        player_ani.AnimationState.SetAnimation(0, "idle_down", true);
        anistate = PlayerState.idle;
        spear_attachment = GetAttachment(spear_asset,spear_address);
        torch_attachment = GetAttachment(torch_asset, torch_address);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            hmovement = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            hmovement = 1;
        }
        else
        {
            //hmovement = Input.GetAxis("Horizontal");
            hmovement = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            vmovement = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vmovement = -1;
        }
        else
        {
            //vmovement = Input.GetAxis("Vertical");
            vmovement = 0;
        }

        if (hmovement == 0 && vmovement == 0 && anistate != PlayerState.idle)
        {
            bool interrupted = false;
            TrackEntry runstoptrack = null;
            switch (player_ani.AnimationName)
            {
                case "run_loop_down":
                    runstoptrack = player_ani.AnimationState.SetAnimation(0, "run_pst_down", false);
                    runstoptrack.Interrupt += (temptrack) =>
                      {
                          interrupted = true;
                      };
                    runstoptrack.Complete += (temptrack) =>
                    {
                        if(!interrupted)
                        {
                            anistate = PlayerState.idle;
                            SetDataAsset(player_ani, idle_asset);
                            player_ani.AnimationState.SetAnimation(0, "idle_down", false);
                        }
                    };
                    break;
                case "run_loop_side":
                    runstoptrack = player_ani.AnimationState.SetAnimation(0, "run_pst_side", false);
                    runstoptrack.Interrupt += (temptrack) =>
                    {
                        interrupted = true;
                    };
                    runstoptrack.Complete += (temptrack) =>
                    {
                        if(!interrupted)
                        {
                            anistate = PlayerState.idle;
                            SetDataAsset(player_ani, idle_asset);
                            player_ani.AnimationState.SetAnimation(0, "idle_side", false);
                        }
                    };
                    break;
                case "run_loop_up":
                    runstoptrack = player_ani.AnimationState.SetAnimation(0, "run_pst_up", false);
                    runstoptrack.Interrupt += (temptrack) =>
                    {
                        interrupted = true;
                    };
                    runstoptrack.Complete += (temptrack) =>
                    {
                        if(!interrupted)
                        {
                            anistate = PlayerState.idle;
                            SetDataAsset(player_ani, idle_asset);
                            player_ani.AnimationState.SetAnimation(0, "idle_up", false);
                        }
                    };
                    break;
                case "run_pre_up":
                    SetDataAsset(player_ani, idle_asset);
                    player_ani.AnimationState.SetAnimation(0, "idle_up", false);
                    anistate = PlayerState.idle;
                    break;
                case "run_pre_side":
                    SetDataAsset(player_ani, idle_asset);
                    player_ani.AnimationState.SetAnimation(0, "idle_side", false);
                    anistate = PlayerState.idle;
                    break;
                case "run_pre_down":
                    SetDataAsset(player_ani, idle_asset);
                    player_ani.AnimationState.SetAnimation(0, "idle_down", false);
                    anistate = PlayerState.idle;
                    break;
                default:
                    break;
            }
        }
        else if (Mathf.Abs(vmovement) > Mathf.Abs(hmovement) && !(vmovement == 0 && hmovement == 0))
        {
            SetDataAsset(player_ani, move_asset);
            if (vmovement >= 0)
            {
                if (anistate == PlayerState.idle)
                {
                    player_ani.AnimationState.ClearTrack(0);
                    player_ani.AnimationState.SetAnimation(0, "run_pre_up", false);
                    player_ani.AnimationState.AddAnimation(0, "run_loop_up", true, 0);
                }
                else if (player_ani.AnimationName != "run_loop_up" && player_ani.AnimationName != "run_pre_up")
                {
                    player_ani.AnimationState.SetAnimation(0, "run_loop_up", true);
                }
            }
            else
            {
                if (anistate == PlayerState.idle)
                {
                    player_ani.AnimationState.ClearTrack(0);
                    player_ani.AnimationState.SetAnimation(0, "run_pre_down", false);
                    player_ani.AnimationState.AddAnimation(0, "run_loop_down", true, 0);
                }
                else if (player_ani.AnimationName != "run_loop_down" && player_ani.AnimationName != "run_pre_down")
                {
                    player_ani.AnimationState.SetAnimation(0, "run_loop_down", true);
                }
            }
            anistate = PlayerState.move;
            player_ani.transform.localScale = new Vector3(1, 1, 1);
        }
        else if ((Mathf.Abs(hmovement) >= Mathf.Abs(vmovement)) && !(vmovement == 0 && hmovement == 0))
        {
            SetDataAsset(player_ani, move_asset);
            if (anistate == PlayerState.idle)
            {
                player_ani.AnimationState.SetAnimation(0, "run_pre_side", false);
                player_ani.AnimationState.AddAnimation(0, "run_loop_side", true, 0);
            }
            else if (player_ani.AnimationName != "run_loop_side" && player_ani.AnimationName != "run_pre_side")
            {
                player_ani.AnimationState.SetAnimation(0, "run_loop_side", true);
            }
            if (hmovement >= 0)
            {
                player_ani.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                player_ani.transform.localScale = new Vector3(-1, 1, 1);
            }
            anistate = PlayerState.move;
        }

        Move();
    }

    private void SetDataAsset(SkeletonAnimation ani, SkeletonDataAsset dataAsset)
    {
        if (ani.skeletonDataAsset != dataAsset)
        {
            ani.skeletonDataAsset = dataAsset;
            ani.Initialize(true);
            if (dataAsset == idle_asset)
            {
                UpdataSkin(ani, idle_final_skin);
            }
            else if (dataAsset == move_asset)
            {
                UpdataSkin(ani, move_final_skin);
            }
            player_ani.skeleton.FindSlot("tool_slot").Attachment = record_attachment;
        }
    }

    private SkinData CreateSkinData(SkeletonAnimation ani)
    {
        SkinData temp = new SkinData()
        {
            cloth = ani.skeleton.Data.FindSkin("cloth"),
            face = ani.Skeleton.Data.FindSkin("face"),
            hair = ani.skeleton.Data.FindSkin("hair"),
            head = ani.skeleton.Data.FindSkin("head"),
            normal_arm = ani.skeleton.Data.FindSkin("normal_arm"),
            item_arm = ani.skeleton.Data.FindSkin("item_arm"),
        };
        return temp;
    }

    private void RefreshSkin()
    {
        SetSkinData(idle_final_skin, record_idle_data);
        SetSkinData(move_final_skin, record_move_data);
        if (anistate == PlayerState.idle)
        {
            UpdataSkin(player_ani, idle_final_skin);
        }
        else if (anistate == PlayerState.move)
        {
            UpdataSkin(player_ani, move_final_skin);
        }
        player_ani.skeleton.FindSlot("tool_slot").Attachment = record_attachment;
    }

    private void SetSkinData(Skin skin, SkinData skindata)
    {
        skin.Clear();
        skin.AddSkin(skindata.cloth);
        skin.AddSkin(skindata.face);
        skin.AddSkin(skindata.hair);
        skin.AddSkin(skindata.head);
        if(istakenothing)
        {
            skin.AddSkin(skindata.normal_arm);
        }
        else
        {
            skin.AddSkin(skindata.item_arm);
        }
        
    }

    private void UpdataSkin(SkeletonAnimation ani,Skin skin)
    {
        ani.skeleton.SetSkin(skin);
        ani.skeleton.UpdateCache();
        ani.skeleton.SetSlotsToSetupPose();
    }

    private void Move()
    {
        player_ani.transform.position += movespeed * Time.deltaTime * (Vector3.forward*vmovement-Vector3.left*hmovement);
    }

    private RegionAttachment GetAttachment(SpineAtlasAsset assetdata,string addressname)
    {
        Atlas tool_atlas = assetdata.GetAtlas();
        AtlasRegion tool_reg = tool_atlas.FindRegion(addressname);
        return tool_reg.ToRegionAttachment(tool_reg.name);

        //aim.Attachment = tool_attachment;
    }
}
