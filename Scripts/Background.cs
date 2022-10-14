using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public void Shake() {
        GetComponent<Animator>().Play("Shake", -1, 0f);
        Handheld.Vibrate();
    }
}
