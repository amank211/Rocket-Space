using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPlaying())
            return;

        if (Input.GetMouseButtonDown(0)) {
            if (!IsMouseOverUI()) { 
                playerController.Shoot();
            }
        }


    }

    bool IsMouseOverUI() { 
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        foreach (RaycastResult raycastResult in raycastResults) {
            if (raycastResult.gameObject.tag == "UI") {
                Debug.Log("hit ui");
                return true;
            }
        }
        Debug.Log("hit no");

        return false;
    }

}
