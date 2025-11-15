using UnityEngine;

public class RestartBattleButton : MonoBehaviour
{
    public void Restart()
    {
        SceneFader.Instance.FadeToScene("BattleScene");
    }
}
