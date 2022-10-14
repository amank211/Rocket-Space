using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    GameObject PauseMenu, GameOverMenu;
    [SerializeField]
    GameObject pause;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    TextMeshProUGUI finalScoreText;



    public static State mCurrentState = State.MAINMENU;

    public enum State { 
        PLAY,
        PAUSE,
        GAMEOVER,
        MAINMENU
    }

    public static bool isPaused() {
        return (mCurrentState == State.PAUSE);
    }

    public static bool isPlaying() {
        return (mCurrentState == State.PLAY);
    }

    static bool hasRestarted = false;

    private void Awake() {
        Application.targetFrameRate = 60;
    }

    private void Start() {
        if (hasRestarted)
        {
            PlayGame();
        }
        else {
            LoadMainMenu();
        }
            
    }

    public void PauseGame() {
        mCurrentState = State.PAUSE;
        pause.SetActive(false);
        PauseMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        GameOverMenu.SetActive(false);
    }

    public void RestartGame() {
        hasRestarted = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        mainMenu.gameObject.SetActive(false);
        GameOverMenu.SetActive(false);
        mCurrentState = State.PLAY;
    }

    public void LoadMainMenu() {
        if(mCurrentState == State.MAINMENU)
            return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        mCurrentState = State.MAINMENU;
        pause.SetActive(false);
        PauseMenu.gameObject.SetActive(false);
        GameOverMenu.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }

    public void GameOver() {
        mCurrentState = State.GAMEOVER;
        finalScoreText.text = "Score: " + FindObjectOfType<ScoreManager>().Score;
        pause.SetActive(false);
        PauseMenu.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);
        GameOverMenu.SetActive(true);
        FindObjectOfType<AdManager>().ShowInterstitial();
    }

    public void PlayGame() {
        mCurrentState = State.PLAY;
        PauseMenu.SetActive(false);
        pause.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    public void SpawnExplosion(Vector3 pos, Transform parent) {
        StartCoroutine(Fade(pos, parent));
    }

    public IEnumerator Fade(Vector3 pos, Transform parent) {

        GameObject obj = null;

        if (parent == null)
        {
            obj = Instantiate(explosion);
        }
        else {
           obj = Instantiate(explosion, parent);
        }

        
        obj.transform.position = pos;
        yield return new WaitForSeconds(0.8f);
        Destroy(obj);
    }

    public void EnableTrail(TrailRenderer rndr) {
        StartCoroutine(EnableTrailRenderer(rndr));    
    }

    public IEnumerator EnableTrailRenderer(TrailRenderer render) {
        yield return new WaitForEndOfFrame();
        render.emitting = true;
    }

}
