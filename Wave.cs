using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Wave", menuName ="Wave")]
public class Wave : ScriptableObject
{
    public int ID;
    public Transform randomSpawnPoints;
    public List<GameObject> Enemies;
    public bool isBossWave = false;
    public int bulletToBeAdded = 5;
}
