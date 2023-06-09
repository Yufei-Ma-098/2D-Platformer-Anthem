using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentHealth;
    public int collectedFragments;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

}
