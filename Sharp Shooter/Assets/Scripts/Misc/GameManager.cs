using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text enemiesLeftText;// 中の数字を.textで変えるので、TMP_Text型で持ってくる
    [SerializeField] GameObject youWinText; // SetActiveするだけなのでGameObjectを拾ってくる

    int enemiesLeft = 0;

    const string ENEMIES_LEFT_STRING = "Enemies Left : ";

    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft += amount;
        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();
        if (enemiesLeft <= 0) youWinText.SetActive(true);
    }
    public void RestartLevelButton()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    public void QuitButton()
    {
        Debug.LogWarning("Does not work in the Unity Editor");
        Application.Quit();
    }
}
