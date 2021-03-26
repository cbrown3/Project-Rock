using UnityEngine;

public class ZeroFriction : MonoBehaviour
{
    private static ZeroFriction instance = null;
    private GamerGraph_Plugin.GamerGraphUniqueDeviceIdentifier GG_UUID;

    public GamerGraph_Plugin.GamerGraphFrictionLess GG_FrictionlessFlow;
    public GamerGraphIds GG_Profile;

    [Header("Polling system runs every CHECKFORTIMEOUTAFTER seconds")]
    [Range(60, 600)]
    public int checkForTimeoutTime = 60;

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

        GG_UUID = gameObject.AddComponent<GamerGraph_Plugin.GamerGraphUniqueDeviceIdentifier>();
        GG_UUID = GetComponentInParent<GamerGraph_Plugin.GamerGraphUniqueDeviceIdentifier>();

        GG_FrictionlessFlow = gameObject.AddComponent<GamerGraph_Plugin.GamerGraphFrictionLess>();
        GG_FrictionlessFlow = GetComponentInParent<GamerGraph_Plugin.GamerGraphFrictionLess>();

        if (GG_Profile.gameKey.Equals("") || GG_Profile.gameKey.Equals(null))
        {
            Debug.LogError("No game key found!");
        }
        else
        {
            GG_FrictionlessFlow.GG_IdProfile.gameKey = GG_Profile.gameKey;
        }

        if (GG_Profile.publisherKey.Equals("") || GG_Profile.publisherKey.Equals(null))
        {
            Debug.LogError("No publisher key found!");
        }
        else
        {
            GG_FrictionlessFlow.GG_IdProfile.publisherKey = GG_Profile.publisherKey;
        }

        GG_FrictionlessFlow.checkForTimeoutTime = checkForTimeoutTime;
    }
}
