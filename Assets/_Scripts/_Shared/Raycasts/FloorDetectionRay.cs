using UnityEngine;

public static class FloorDetectionRay
{
    public static Vector3 GetFloorPosition(Vector3 position, LayerMask groundLayer)
    {
        Vector3 origin = position;
        origin.y = 500f;
        float range = 1000f;

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, range, groundLayer))
        {
            return hit.point;
        }

        return position;
    }
}
