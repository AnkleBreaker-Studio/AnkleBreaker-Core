using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnkleBreaker.Core.MasterInterfaces
{
    public interface IBehaviourBase
    {
        GameObject gameObject { get; }
        Transform transform { get; }        
        int PrefabInstanceId { get; set; }
        T GetComponent<T>();
    }

    public interface IBehaviour<T> : IBehaviourBase where T: IAssetIdentitySO
    {
        void InitializeFrom(T configSO);
    }
}
