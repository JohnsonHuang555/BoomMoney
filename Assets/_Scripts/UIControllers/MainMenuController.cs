using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("StartButton");

        startButton.clicked += StartButtonPressed;
    }

    void StartButtonPressed()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
