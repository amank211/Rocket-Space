using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    GameObject mRocketPrefab;

    [SerializeField]
    Button play;
    [SerializeField]
    Button pause;
    [SerializeField]
    GameObject explosion;


    [SerializeField]
    List<GameObject> mRocketList = new List<GameObject>();

    [SerializeField]
    List<Transform> mRandomSpawnPoints = new List<Transform>();

    [SerializeField]
    Transform mRocketParent;

    private void Awake() {
        Application.targetFrameRate = 120;
    }

    private void Start() {
        mRocketList.Add(Instantiate(mRocketPrefab, mRocketParent));
        mRocketList.Add(Instantiate(mRocketPrefab, mRocketParent));
        mRocketList.Add(Instantiate(mRocketPrefab, mRocketParent));
        mRocketList.Add(Instantiate(mRocketPrefab, mRocketParent));

        foreach (var rocket in mRocketList) {
            int random = Random.Range(0, mRandomSpawnPoints.Count - 1);
            rocket.transform.position = mRandomSpawnPoints[random].position;
            rocket.SetActive(true);
        }


    }

    public void PauseGame() {
        Time.timeScale = 0;
        pause.gameObject.SetActive(false);
        play.gameObject.SetActive(true);
    }

    public void PlayGame() {
        Time.timeScale = 1;
        play.gameObject.SetActive(false);
        pause.gameObject.SetActive(true);
    }

    public void SpawnRocket(GameObject rocket) {
        int random = Random.Range(0, mRandomSpawnPoints.Count - 1);
        rocket.transform.position = mRandomSpawnPoints[random].position;
        rocket.SetActive(true);
    }

    public void SpawnExplosion(Vector3 pos) {
        StartCoroutine(Fade(pos));
    }

    public IEnumerator Fade(Vector3 pos) {
        GameObject obj = Instantiate(explosion);
        obj.transform.position = pos;
        Debug.Log("exploded");
        yield return new WaitForSeconds(0.8f);
        Debug.Log("exploded destroyed");
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
