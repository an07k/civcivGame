using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopUp : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TimerUI _timerUI;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private TMP_Text _timerText;

    void OnEnable()
    {
        _timerText.text = _timerUI.GetFinalTime();
        _restartButton.onClick.AddListener(OnRestartClicked);
    }
    private void OnRestartClicked()
    {
        SceneManager.LoadScene(Consts.Scenes.GAME_SCENE);
    }


}
