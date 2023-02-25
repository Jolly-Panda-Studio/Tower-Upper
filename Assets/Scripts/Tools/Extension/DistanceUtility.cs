using UnityEngine;

public static class DistanceUtility
{
    public static float GetDuration(this Transform transform, Vector3 target, float speed)
    {
        return Vector3.Distance(transform.position, target) / speed;
    }
}
