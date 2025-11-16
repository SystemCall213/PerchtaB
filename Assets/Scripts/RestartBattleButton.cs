using UnityEngine;

public class RestartBattleButton : MonoBehaviour
{
    public string battleSceneName;

    public void Restart()
    {
        SceneFader.Instance.FadeToScene(battleSceneName);
    }
}
