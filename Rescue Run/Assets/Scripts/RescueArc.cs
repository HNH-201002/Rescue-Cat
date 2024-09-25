using UnityEngine;
using Watermelon;

public class ArcRenderer : MonoBehaviour
{
    [SerializeField]
    private float radius = 5f;

    [SerializeField]
    private float arcAngle = 90f;

    [SerializeField]
    private int segments = 50;

    [SerializeField]
    private Color arcColor = Color.red;

    [SerializeField]
    private float detectionRange = 10f;

    private Mesh mesh;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private MeshCollider meshCollider;

    [SerializeField]
    private LayerMask detectionLayerMask;

    private bool isPlayerInRange = false;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        meshRenderer = GetComponent<MeshRenderer>();

        Material material = new Material(Shader.Find("Unlit/Color"));
        material.color = arcColor;
        meshRenderer.material = material;

        meshCollider = GetComponent<MeshCollider>();

        meshRenderer.enabled = false;

        CreateArc();
    }

    private void Update()
    {
        DetectPlayerWithOverlapSphere();
    }

    private void CreateArc()
    {
        Vector3[] vertices = new Vector3[segments + 2];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero;

        float angleStep = arcAngle / segments;
        for (int i = 0; i <= segments; i++)
        {
            float angle = (-arcAngle / 2 + angleStep * i) * Mathf.Deg2Rad;
            vertices[i + 1] = new Vector3(Mathf.Sin(angle) * radius, 0, Mathf.Cos(angle) * radius);
        }

        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        meshCollider.sharedMesh = mesh;
    }

    private void DetectPlayerWithOverlapSphere()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, detectionLayerMask);

        bool playerDetected = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == PhysicsHelper.TAG_ANIMAL)
            {
                playerDetected = true;
                if (!isPlayerInRange)
                {
                    ShowArc();
                    isPlayerInRange = true;
                }
                break;
            }
        }

        if (!playerDetected && isPlayerInRange)
        {
            HideArc();
            isPlayerInRange = false;
        }
    }

    private void ShowArc()
    {
        meshRenderer.enabled = true;
    }

    private void HideArc()
    {
        meshRenderer.enabled = false;
    }
}
