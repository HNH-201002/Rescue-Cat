using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMapAppearance : MonoBehaviour
{
    [SerializeField]
    private GameObject[] decorations;

    [SerializeField]
    private GameObject leftSide;

    [SerializeField]
    private GameObject rightSide;

    private Queue<GameObject> leftDecorationPool;
    private Queue<GameObject> rightDecorationPool;
    private int poolSize = 20; 

    void Start()
    {
        InitializePools();

        int randomLeftCount = Random.Range(35, 75);
        int randomRightCount = Random.Range(35, 75);

        Generate(randomLeftCount, randomRightCount);
    }

    private void InitializePools()
    {
        leftDecorationPool = new Queue<GameObject>();
        rightDecorationPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject leftDecoration = InstantiateRandomDecoration();
            leftDecoration.SetActive(false);
            leftDecorationPool.Enqueue(leftDecoration);

            GameObject rightDecoration = InstantiateRandomDecoration();
            rightDecoration.SetActive(false);
            rightDecorationPool.Enqueue(rightDecoration);
        }
    }

    private GameObject InstantiateRandomDecoration()
    {
        int randomIndex = Random.Range(0, decorations.Length);
        return Instantiate(decorations[randomIndex]);
    }

    private void Generate(int numberOfLeftDecorations, int numberOfRightDecorations)
    {
        List<Vector3> leftPositions = GeneratePositions(numberOfLeftDecorations, leftSide);
        List<Vector3> rightPositions = GeneratePositions(numberOfRightDecorations, rightSide);

        Shuffle(leftPositions);
        Shuffle(rightPositions);

        GenerateDecorations(leftPositions, leftDecorationPool, leftSide);
        GenerateDecorations(rightPositions, rightDecorationPool, rightSide);
    }

    private List<Vector3> GeneratePositions(int count, GameObject side)
    {
        List<Vector3> positions = new List<Vector3>();

        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(Random.Range(-5f, 5f),0, Random.Range(-5f, 5f)); 
            positions.Add(position);
        }

        return positions;
    }

    private void Shuffle(List<Vector3> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Vector3 temp = positions[i];
            int randomIndex = Random.Range(i, positions.Count);
            positions[i] = positions[randomIndex];
            positions[randomIndex] = temp;
        }
    }

    private void GenerateDecorations(List<Vector3> positions, Queue<GameObject> decorationPool, GameObject parent)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            GameObject decoration;

            if (decorationPool.Count > 0)
            {
                decoration = decorationPool.Dequeue();
                decoration.SetActive(true);
            }
            else
            {
                decoration = InstantiateRandomDecoration(); 
            }

            decoration.transform.SetParent(parent.transform);
            decoration.transform.localPosition = positions[i];
        }
    }

    public void ReturnToPool(GameObject decoration, bool isLeftSide)
    {
        decoration.SetActive(false);
        if (isLeftSide)
        {
            leftDecorationPool.Enqueue(decoration);
        }
        else
        {
            rightDecorationPool.Enqueue(decoration);
        }
    }
}
