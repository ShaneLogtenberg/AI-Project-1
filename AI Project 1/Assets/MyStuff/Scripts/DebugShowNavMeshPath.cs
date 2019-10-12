using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]

public class DebugShowNavMeshPath : MonoBehaviour {

    UnityEngine.AI.NavMeshAgent navMeshAgent;
    LineRenderer lineRenderer;

    public Color lineColor;

    void Awake() {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
    }
    // Use this for initialization
    void Start() {
        lineRenderer.SetColors(lineColor, lineColor);
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    // Update is called once per frame
    void Update() {
        lineRenderer.SetVertexCount(navMeshAgent.path.corners.Length);
        for (int i = 0; i < navMeshAgent.path.corners.Length; i++) {
            lineRenderer.SetPosition(i, navMeshAgent.path.corners[i]);
        }

    }
}
