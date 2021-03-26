using UnityEngine;
using UnityEngine.UI;

public class GG_UpdateProfileInformation : MonoBehaviour
{
    private GamerGraphCore _GamergraphAPI;
    private GG_PlayerProfileUpdate_InputFields _UpdateInputFields;

    public Button updateProfileBtn;

    private void Awake()
    {
        _GamergraphAPI = FindObjectOfType<GamerGraphCore>();
        _UpdateInputFields = FindObjectOfType<GG_PlayerProfileUpdate_InputFields>();
    }

    private void Start()
    {
        if (updateProfileBtn != null)
        {
            updateProfileBtn.onClick.AddListener(() => UpdatePlayerInformation());
        }
    }

    private void UpdatePlayerInformation()
    {
        string email = _UpdateInputFields.playerProfileUpdateEmailField.text.ToString();
		string firstName = _UpdateInputFields.playerProfileUpdateFirstNameField.text.ToString();
		string lastName = _UpdateInputFields.playerProfileUpdateLastNameField.text.ToString();
		string ph = _UpdateInputFields.playerProfileUpdatePhoneNumberField.text.ToString();
		string gamerTag = _UpdateInputFields.playerProfileUpdateGamerTagField.text.ToString();
		string city = _UpdateInputFields.playerProfileUpdateCityField.text.ToString();
		string state = _UpdateInputFields.playerProfileUpdateStateField.text.ToString();
		string country = _UpdateInputFields.playerProfileUpdateCountryField.text.ToString();
		string postcode = _UpdateInputFields.playerProfileUpdatePostcodeField.text.ToString();
		string dob = _UpdateInputFields.playerProfileUpdateDobField.text.ToString();
		string gender = _UpdateInputFields.playerProfileUpdateGenderField.text.ToString();
		string fb = _UpdateInputFields.playerProfileUpdateFacebookField.text.ToString();
		string ws = _UpdateInputFields.playerProfileUpdateWebsiteField.text.ToString();
		string insta = _UpdateInputFields.playerProfileUpdateInstagramField.text.ToString();
		string twitch = _UpdateInputFields.playerProfileUpdateTwitchField.text.ToString();

		int gen = -1;

		if (gender != null && gender != "")
        {
            if (gender.ToLower().Equals("male"))
            {
                gen = 0;
            }

            if (gender.ToLower().Equals("female"))
            {
                gen = 1;
            }

            if (gender.ToLower().Equals("other"))
            {
                gen = 2;
            }
        }

        _GamergraphAPI.CheckUniqueFieldStatus(email, firstName, lastName, ph, gamerTag, city, state, country, postcode, gen, dob, fb, ws, insta, twitch);
    }
}
