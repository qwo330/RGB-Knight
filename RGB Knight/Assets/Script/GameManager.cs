using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    public List<GameObject> Prefabs;

    public int Score = 0;
    public int Level = 1;

    float startTime = 0;
    public float CurTime = 0;

    public void Init()
    {
        Score = 0;
    }

    void Update()
    {
        CurTime = Time.time - startTime;
        ShowTime(CurTime);
    }

    public void ResetTimer()
    {
        startTime = Time.time;
        CurTime = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameOver();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        endHeight = transform.position.y;
        ShowScore(endHeight - startHeight);
    }

    float startHeight, endHeight;
    public void ComputeHeight()
    {
        startHeight = transform.position.y;
    }

    void GameOver()
    {
        // todo : ÆË¾÷
        Debug.Log("GameOver");
        ShowPopup();
        Time.timeScale = 0f;
    }

    public void LoadScene(/*int level*/)
    {
        string sceneName = "Scene" + (Level + 1);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadHome()
    {
        SceneManager.LoadScene("Home");
    }
}

// UI
public partial class GameManager : MonoBehaviour
{
    public Text ScoreText;
    public Text TimeText;
    public GameObject Popup;
    public Button NextButton;
    public Button HomeButton;

    public void InitUI()
    {
        ScoreText.text = "0";
        TimeText.text = "";

        NextButton.onClick.AddListener(OnClickNext);
        HomeButton.onClick.AddListener(OnClickHome);
    }

    public void ShowTime(float time)
    {
        TimeText.text = string.Format("{0:N2}", time);
    }

    public void ShowScore(float score)
    {
        ScoreText.text = score.ToString() + " m";
    }

    public void ShowPopup()
    {
        Popup.SetActive(true);
    }

    public void HidePopup()
    {
        Popup.SetActive(false);
    }

    public void OnClickNext()
    {
        LoadScene();
        HidePopup();
    }

    public void OnClickHome()
    {

        LoadHome();
        HidePopup();
    }
}
