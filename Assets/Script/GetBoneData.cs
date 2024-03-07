using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBoneData : MonoBehaviour
{
    //SkeletonRenderSeparator data;
    public SkeletonAnimation willowani;
    private SkeletonRenderSeparator separator;
    // Start is called before the first frame update
    void Awake()
    {
        willowani.UpdateComplete +=(willowani)=> 
        {
            Debug.Log("update compelet");
        };
    }

    private void Update()
    {
        //int i = 0;
        //foreach (Slot slot in willowani.skeleton.DrawOrder)
        //{
        //    //if (slot.Data.Name == "root-ext_skirt")
        //    //{
        //    //    Debug.Log(slot.Data.Index);
        //    //}
        //    i++;
        //    Debug.Log(i);
        //    Debug.Log(slot.Bone.Data.Name);
        //    Debug.Log(slot.Bone.Active);
        //}
        //for(int i=0;i<willowani.skeleton.DrawOrder.Count;i++)
        //{
        //    Debug.Log(i);
        //    Debug.Log(willowani.skeleton.DrawOrder(i));
        //}
        //Debug.Log("++++++++++++++++++++++");
    }
}
