using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBullets : MonoBehaviour
{

    int mBullets;
    [SerializeField]
    GameObject mBulletPrefab;

    Stack<GameObject> mStack = new Stack<GameObject>();

    public void InitBullets(int bullets) {
        AddBullets(bullets);
    }

    public void RemoveOneBullet() {
        mStack.Pop().SetActive(false);
    }

    public void AddBullets(int bullets) {
        GameObject obj = null;
        int count = mStack.Count;
        for (int i = count; i < bullets + count; i++)
        {
            if (i == 0)
            {
                obj = Instantiate(mBulletPrefab, transform);
                obj.transform.localPosition = new Vector3(0, 0, 0);
            }
            else if (i % 2 == 0)
            {
                obj = Instantiate(mBulletPrefab, transform);
                obj.transform.localPosition = new Vector3(i * 0.4f, 0, 0);
            }
            else
            {
                obj = Instantiate(mBulletPrefab, transform);
                obj.transform.localPosition = new Vector3(-i * 0.4f - 0.4f, 0, 0);
            }

            mStack.Push(obj);
        }
    }
}
