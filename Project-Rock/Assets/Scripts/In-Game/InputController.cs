using UnityEngine;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    public bool isPlayer1;
    protected InputManager iManager;

    // Start is called before the first frame update
    public void Awake()
    {
        Scene preloadScene = SceneManager.GetSceneByName("PreloadingScene");

        GameObject[] preloadGOs = preloadScene.GetRootGameObjects();

        if (isPlayer1)
        {
            for (int i = 0; i < preloadGOs.Length; i++)
            {
                if (preloadGOs[i].name == "P1InputManager")
                {
                    iManager = preloadGOs[i].GetComponent<P1InputManager>();
                }
            }
        }
        else
        {
            for (int i = 0; i < preloadGOs.Length; i++)
            {
                if (preloadGOs[i].name == "P2InputManager")
                {
                    iManager = preloadGOs[i].GetComponent<P2InputManager>();
                }
            }
        }
    }
}
