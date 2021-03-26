using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamerGraphResponses : MonoBehaviour
{
    private static GamerGraphResponses instance = null;
    public GamerGraph_Plugin.GG_Response GG_Responses;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        GG_Responses = gameObject.AddComponent<GamerGraph_Plugin.GG_Response>();
        GG_Responses = GetComponentInParent<GamerGraph_Plugin.GG_Response>();
    }
}
