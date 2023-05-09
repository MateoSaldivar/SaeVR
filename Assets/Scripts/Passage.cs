using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{
    public enum Type {
        Straight,
        Diagonal
	}

    public Type type;

    public Transform startPoint;
    public Transform endPoint;

    public Transform spawnAnchor;

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (startPoint != null && endPoint != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(startPoint.position, 0.01f);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(endPoint.position, 0.01f);

            Gizmos.color = Color.white;
            Gizmos.DrawLine(startPoint.position, endPoint.position);
        }
    }
#endif
}
