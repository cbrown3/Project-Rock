using UnityEngine;

public class GG_CustomActions : MonoBehaviour
{
    private static GG_CustomActions instance = null;
    private GamerGraphCore _GG_API;

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

        _GG_API = FindObjectOfType<GamerGraphCore>();
    }

    public void PerformCustomAction(string customActionId)
    {
        _GG_API.CreateCustomActionTrigger(customActionId);
    }
}
