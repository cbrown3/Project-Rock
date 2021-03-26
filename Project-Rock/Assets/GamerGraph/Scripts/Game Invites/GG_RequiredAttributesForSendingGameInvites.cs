using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GG_RequiredAttributesForSendingGameInvites : MonoBehaviour
{
    //public string gameId;
    //public List<int> friendIds;

    //public void PointToAttributes(string gameId, List<int> friendIds)
    //{
    //    this.gameId = gameId;
    //    this.friendIds = friendIds;
    //}

    public GamerGraph_Plugin.GG_RequiredAttributesForSendingGameInvites _RequiredAttributesForGameInvites;

    private void Awake()
    {
        _RequiredAttributesForGameInvites = gameObject.AddComponent<GamerGraph_Plugin.GG_RequiredAttributesForSendingGameInvites>();
        _RequiredAttributesForGameInvites = GetComponentInParent<GamerGraph_Plugin.GG_RequiredAttributesForSendingGameInvites>();
    }
}
