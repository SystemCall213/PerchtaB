using UnityEngine;

public class GoToMainMenuBattleButton : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneFader.Instance.FadeToScene("MainMenu");
    }
}
