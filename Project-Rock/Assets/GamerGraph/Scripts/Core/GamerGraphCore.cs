using System.Collections.Generic;
using UnityEngine;

public class GamerGraphCore : MonoBehaviour
{
    private static GamerGraphCore instance = null;
    private GamerGraph_Plugin.GamerGraphAPI GG_API;

    public GamerGraphIds GG_Profile;

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

        GG_API = gameObject.AddComponent<GamerGraph_Plugin.GamerGraphAPI>();
        GG_API = GetComponentInParent<GamerGraph_Plugin.GamerGraphAPI>();

        if (GG_Profile.gameKey.Equals("") || GG_Profile.gameKey.Equals(null))
        {
            Debug.LogError("No game key found!");
        }
        else
        {
            GG_API._GG_Ids.gameKey = GG_Profile.gameKey;
        }

        if (GG_Profile.publisherKey.Equals("") || GG_Profile.publisherKey.Equals(null))
        {
            Debug.LogError("No publisher key found!");
        }
        else
        {
            GG_API._GG_Ids.publisherKey = GG_Profile.publisherKey;
        }
    }

    private void Start()
    {
        //Debug.Log(GG_API._GG_Ids.gameKey);
    }

    public void Login(string email, string password)
    {
        GG_API.Login(email, password);
    }

    public void Signup(string email, string password, string firstName, string lastName, string phoneNumber, string gamerTag, string city,
        string state, string country, string postcode, int gender, string dob, string facebook, string website, string instagram, string twitch)
    {
        GG_API.SignUp(email, password, firstName, lastName, phoneNumber, gamerTag, city,
        state, country, postcode, gender, dob, facebook, website, instagram, twitch);
    }

    public void GetAchievementProcess(int id, int objectIndex)
    {
        GG_API.GetAchievementProcess(id, objectIndex);
    }

    public void GetPlayerInfo(bool isNewSignUp)
    {
        GG_API.GetPlayerInfo(isNewSignUp);
    }

    // Get achievemnet status
    public void GetAchievementStatus(int id, int objectIndex)
    {
        GG_API.GetAchievementStatus(id, objectIndex);
    }

    // Get achievemnet last work
    public void GetAchievementLastWork(int id, int objectIndex)
    {
        GG_API.GetAchievementLastWork(id, objectIndex);
    }

    // Get achievements list
    public void GetAchievementsList()
    {
        GG_API.GetAchievementsList();
    }

    // Get player's friends
    public void GetUserFriends()
    {
        GG_API.GetUserFriends();
    }

    // Get player profile list
    public void GetPlayerProfileList()
    {
        GG_API.GetPlayerProfileList();
    }

    // Check unique field status
    public void CheckUniqueFieldStatus(string email, string firstName, string lastName, string phoneNumber, string gamerTag, string city,
    string state, string country, string postcode, int gender, string dob, string facebook, string website, string instagram, string twitch)
    {
        GG_API.CheckUniqueFieldStatus(email, firstName, lastName, phoneNumber, gamerTag, city, state, country, postcode, gender, dob, facebook, website, instagram, twitch);
    }

    // Update Player Profile
    public void UpdatePlayerProfile(string email, string firstName, string lastName, string phoneNumber, string gamerTag, string city,
    string state, string country, string postcode, int gender, string dob, string facebook, string website, string instagram, string twitch)
    {
        GG_API.UpdatePlayerProfile(email, firstName, lastName, phoneNumber, gamerTag, city, state, country, postcode, gender, dob, facebook, website, instagram, twitch);
    }

    // Delete player profile
    public void DeletePlayerProfile()
    {
        GG_API.DeletePlayerProfile();
    }

    // Send a friend request
    public void SendFriendRequest(int receiverId)
    {
        GG_API.SendFriendRequest(receiverId);
    }

    // Get list of friend requests received from other users
    public void FriendRequestsReceived()
    {
        GG_API.FriendRequestsReceived();
    }

    // Get list of friend requests sent to other players
    public void GetRequestsSentToOtherUsers()
    {
        GG_API.GetRequestsSentToOtherUsers();
    }

    /* Process receivedFriend requests */
    public void ProcessFriendRequests(int requestId, string status)
    {
        GG_API.ProcessFriendRequests(requestId, status);
    }

    // Get Player's games
    public void GetPlayerGames()
    {
        GG_API.GetPlayerGames();
    }

    // Create a new Player game
    public void CreateNewPlayerGame()
    {
        GG_API.CreateNewPlayerGame();
    }

    // Get list of game invites i have sent
    public void GetGameInvitesSent()
    {
        GG_API.GetGameInvitesSent();
    }

    // Get list of game invites i have received
    public void GetGameInvitesReceived()
    {
        GG_API.GetGameInvitesReceived();
    }

    /* Send game invites */
    public void SendGameInvites(string gameKey, List<int> friendIds)
    {
        GG_API.SendGameInvites(gameKey, friendIds);
    }

    /* Process a game invite */
    public void ProcessGameInvite(int gameInviteId, string status)
    {
        GG_API.ProcessGameInvite(gameInviteId, status);
    }

    // Get list of esrb publishers
    public void GetPublisherEsrbList()
    {
        GG_API.GetPublisherEsrbList();
    }

    // Get list of publisher games
    public void GetPublisherGameList()
    {
        GG_API.GetPublisherGameList();
    }

    // Get list of publisher platforms
    public void GetPublisherPlatformList()
    {
        GG_API.GetPublisherPlatformList();
    }

    // Get a list of custom actions
    public void GetCustomActionsList(string publisherKey, string gameKey)
    {
        GG_API.GetCustomActionsList(publisherKey, gameKey);
    }

    // Create a custom action trigger
    public void CreateCustomActionTrigger(string customActionId)
    {
        GG_API.CreateCustomActionTrigger(customActionId);
    }

    // Get a list of custom actions trigger list
    public void GetCustomActionsTriggerList(int gameKey)
    {
        GG_API.GetCustomActionsTriggerList(gameKey);
    }

    // Create a player action
    public void CreatePlayerAction(string gameKey, string actionValue)
    {
        GG_API.CreatePlayerAction(gameKey, actionValue);
    }

    // Get a list of player actions
    public void GetPlayerActionsList(string gameKey)
    {
        GG_API.GetPlayerActionsList(gameKey);
    }

    // Send Offline datastore
    public void SendOfflineDataStore()
    {
        GG_API.SendOfflineDataStore();
    }
}
