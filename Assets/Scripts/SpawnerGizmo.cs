using UnityEngine;

public class SpawnerGizmo : MonoBehaviour
{
    public Transform P0;
    public Transform P1;

    private void OnDrawGizmos()
    {
        if (P0 != null && P1 != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(P0.position, 0.2f);
            Gizmos.DrawSphere(P1.position, 0.2f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(P0.position, P1.position);
        }
    }
}
