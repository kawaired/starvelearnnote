using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class AniCtrl : MonoBehaviour
{
    public SkeletonDataAsset move_asset;
    public SkeletonDataAsset idle_asset;
    public SkeletonAnimation playerani;

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
        SetDataAsset(playerani, idle_asset);
        playerani.AnimationState.SetAnimation(0, "idle_down", true);
        anistate = PlayerState.idle;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            hmovement = -1;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            hmovement = 1;
        }
        else
        {
            hmovement = 0;
        }

        if(Input.GetKey(KeyCode.W))
        {
            vmovement = 1;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            vmovement = -1;
        }
        else
        {
            vmovement = 0;
        }

        if (hmovement == 0 && vmovement == 0 && anistate != PlayerState.idle)
        {
            TrackEntry runstoptrack = null;
            switch (playerani.AnimationName)
            {
                case "run_loop_down":
                    runstoptrack = playerani.AnimationState.SetAnimation(0, "run_pst_down", false);
                    runstoptrack.Complete += (temptrack) =>
                    {
                        SetDataAsset(playerani, idle_asset);
                        playerani.AnimationState.SetAnimation(0, "idle_down", false);
                    };
                    break;
                case "run_loop_side":
                    runstoptrack = playerani.AnimationState.SetAnimation(0, "run_pst_side", false);
                    runstoptrack.Complete += (temptrack) =>
                    {
                        SetDataAsset(playerani, idle_asset);
                        playerani.AnimationState.SetAnimation(0, "idle_side", false);
                    };
                    break;
                case "run_loop_up":
                    runstoptrack = playerani.AnimationState.SetAnimation(0, "run_pst_up", false);
                    runstoptrack.Complete += (temptrack) =>
                    {
                        SetDataAsset(playerani, idle_asset);
                        playerani.AnimationState.SetAnimation(0, "idle_up", false);
                    };
                    break;
                case "run_pre_up":
                    SetDataAsset(playerani, idle_asset);
                    playerani.AnimationState.SetAnimation(0, "idle_up", false);
                    break;
                case "run_pre_side":
                    SetDataAsset(playerani, idle_asset);
                    playerani.AnimationState.SetAnimation(0, "idle_side", false);
                    break;
                case "run_pre_down":
                    SetDataAsset(playerani, idle_asset);
                    playerani.AnimationState.SetAnimation(0, "idle_down", false);
                    break;
                default:
                    break;
            }
            anistate = PlayerState.idle;
        }
        else if (Mathf.Abs(vmovement) > Mathf.Abs(hmovement) && !(vmovement == 0 && hmovement == 0))
        {
            SetDataAsset(playerani, move_asset);
            if (vmovement >= 0)
            {
                if (anistate == PlayerState.idle)
                {
                    playerani.AnimationState.SetAnimation(0, "run_pre_up", false);
                    playerani.AnimationState.AddAnimation(0, "run_loop_up", true, 0);
                }
                else if (playerani.AnimationName != "run_loop_up" && playerani.AnimationName != "run_pre_up")
                {
                    playerani.AnimationState.SetAnimation(0, "run_loop_up", true);
                }

            }
            else
            {
                if (anistate == PlayerState.idle)
                {
                    playerani.AnimationState.SetAnimation(0, "run_pre_down", false);
                    playerani.AnimationState.AddAnimation(0, "run_loop_down", true, 0);
                }
                else if (playerani.AnimationName != "run_loop_down" && playerani.AnimationName != "run_pre_down")
                {
                    playerani.AnimationState.SetAnimation(0, "run_loop_down", true);
                }
            }
            anistate = PlayerState.move;
            playerani.transform.localScale = new Vector3(1, 1, 1);
        }
        else if ((Mathf.Abs(hmovement) >= Mathf.Abs(vmovement)) && !(vmovement == 0 && hmovement == 0))
        {
            SetDataAsset(playerani, move_asset);
            if (anistate == PlayerState.idle)
            {
                playerani.AnimationState.SetAnimation(0, "run_pre_side", false);
                playerani.AnimationState.AddAnimation(0, "run_loop_side", true, 0);
            }
            else if (playerani.AnimationName != "run_loop_side" && playerani.AnimationName != "run_pre_side")
            {
                playerani.AnimationState.SetAnimation(0, "run_loop_side", true);
            }
            if (hmovement >= 0)
            {
                playerani.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                playerani.transform.localScale = new Vector3(-1, 1, 1);
            }
            anistate = PlayerState.move;
        }
    }

    private void SetDataAsset(SkeletonAnimation ani ,SkeletonDataAsset dataAsset)
    {
        if(ani.skeletonDataAsset!=dataAsset)
        {
            ani.skeletonDataAsset = dataAsset;
            ani.Initialize(true);
        } 
    }
}
