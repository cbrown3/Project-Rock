using UnityEngine;
using UnityEngine.UI;

public class GG_AddNewFriends : MonoBehaviour
{
    private GamerGraphCore _GamerGraphAPI;

    public Button addNewFriendsBtn;

    private void Awake()
    {
        _GamerGraphAPI = FindObjectOfType<GamerGraphCore>();
        if (_GamerGraphAPI == null)
        {
            Debug.Log("Unable to find gamergraph api");
        }
    }

    private void Start()
    {
        if (addNewFriendsBtn != null)
        {
            addNewFriendsBtn.onClick.AddListener(() => AddNewFriends());
        }
    }

    private void AddNewFriends()
    {
        _GamerGraphAPI.GetPlayerProfileList();
    }

    // Send a new friend request
    public void SendFriendRequest(int receiverId)
    {
        _GamerGraphAPI.SendFriendRequest(receiverId);
    }

    // Accept or reject a request
    public void AcceptOrRejectFriendRequest(int requestId, string status)
    {
        _GamerGraphAPI.ProcessFriendRequests(requestId, status);
    }
}
