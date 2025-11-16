using System.Collections.Generic;
using UnityEngine;

public class PlayerFlags : MonoBehaviour
{
    public static PlayerFlags Instance;
    private HashSet<string> flags = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetFlag(string flag) => flags.Add(flag);
    public void RemoveFlag(string flag) => flags.Remove(flag);

    public bool HasFlag(string flag) => flags.Contains(flag);
}
