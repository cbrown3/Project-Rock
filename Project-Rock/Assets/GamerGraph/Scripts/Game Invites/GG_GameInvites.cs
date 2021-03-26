using UnityEngine;
using UnityEngine.UI;

public class GG_GameInvites : MonoBehaviour
{
    private GamerGraphCore _GamergraphAPI;
    private GG_RequiredAttributesForSendingGameInvites _RequiredAttributes;

    public Button sendGameInvitesBtn;

    private void Awake()
    {
        _GamergraphAPI = FindObjectOfType<GamerGraphCore>();
        _RequiredAttributes = FindObjectOfType<GG_RequiredAttributesForSendingGameInvites>();
    }

    private void Start()
    {
        if (sendGameInvitesBtn != null)
        {
            sendGameInvitesBtn.onClick.AddListener(() => SendGameInvitesToFriends());
        }
    }

    private void SendGameInvitesToFriends()
    {
        _GamergraphAPI.SendGameInvites(_RequiredAttributes._RequiredAttributesForGameInvites.gameId, _RequiredAttributes._RequiredAttributesForGameInvites.friendIds);
    }

    // Accept or reject a game invite
    public void AcceptOrRejectGameInviteRequest(int gameInviteId, string status)
    {
        _GamergraphAPI.ProcessGameInvite(gameInviteId, status);
    }
}
