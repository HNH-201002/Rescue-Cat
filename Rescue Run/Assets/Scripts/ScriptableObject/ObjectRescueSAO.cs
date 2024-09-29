using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CatRescueData", menuName = "ScriptableObjects/CatRescueData", order = 1)]
public class ObjectRescueSAO : ScriptableObject
{
    [SerializeField]
    private int quantity;

    public int Quantity => quantity;

    [SerializeField]
    private GameObject[] prefabs;

    public GameObject[] Prefabs => prefabs;
}
