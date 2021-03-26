using UnityEngine;
using UnityEngine.UI;

public class GG_LoginSignup : MonoBehaviour 
{
	private GG_Login_Signup_InputFields _AIFs;
    private GamerGraphCore _GamerGraphAPI;

    public Button loginButton;
	public Button signUpButton;

	private void Awake()
	{
		_AIFs = FindObjectOfType<GG_Login_Signup_InputFields>();
		_GamerGraphAPI = FindObjectOfType<GamerGraphCore>();
	}

	private void Start()
	{
        if (loginButton != null)
		    loginButton.onClick.AddListener(() => Login());

        if (signUpButton != null)
            signUpButton.onClick.AddListener(() => Signup());
	}

	private void Login()
	{
		string email = _AIFs.loginEmailField.text.ToString();
		string password = _AIFs.loginPasswordField.text.ToString();
		
		_GamerGraphAPI.Login(email, password);
	}

	private void Signup()
	{
		string email = _AIFs.signupEmailField.text.ToString();
		string password = _AIFs.signupPasswordField.text.ToString();
		string firstName = _AIFs.signupFirstNameField.text.ToString();
		string lastName = _AIFs.signupLastNameField.text.ToString();
		string ph = _AIFs.signupPhoneNumberField.text.ToString();
		string gamerTag = _AIFs.signupGamerTagField.text.ToString();

        string city = null, state = null, country = null, postcode = null, dob = null, gender = null, fb = null, ws = null, insta = null, twitch = null;

        if (_AIFs.signupCityField != null)
		    city = _AIFs.signupCityField.text.ToString();

        if (_AIFs.signupStateField != null)
            state = _AIFs.signupStateField.text.ToString();

        if (_AIFs.signupCountryField != null)
            country = _AIFs.signupCountryField.text.ToString();

        if (_AIFs.signupPostcodeField != null)
            postcode = _AIFs.signupPostcodeField.text.ToString();

        if (_AIFs.signupDobField != null)
            dob = _AIFs.signupDobField.text.ToString();

        if (_AIFs.signupGenderField != null)
            gender = _AIFs.signupGenderField.text.ToString();

        if (_AIFs.signupFacebookField != null)
            fb = _AIFs.signupFacebookField.text.ToString();

        if (_AIFs.signupWebsiteField != null)
            ws = _AIFs.signupWebsiteField.text.ToString();

        if (_AIFs.signupInstagramField != null)
            insta = _AIFs.signupInstagramField.text.ToString();

        if (_AIFs.signupTwitchField != null)
            twitch = _AIFs.signupTwitchField.text.ToString();

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

		_GamerGraphAPI.Signup(email, password, firstName, lastName, ph, gamerTag, city, state, country, postcode, gen, dob, fb, ws, insta, twitch);
	}
}
