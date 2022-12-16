using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointCreator : MonoBehaviour
{
	private List<Transform> m_Points = new List<Transform>();

	public List<Transform> Points => m_Points;


    private void Start()
	{
		var collider = GetComponentInChildren<CapsuleCollider>();

		GameObject parentPoint = new GameObject("Spawn Points");
		parentPoint.transform.SetParent(collider.transform);
		parentPoint.transform.localPosition = new Vector3(0, -collider.height / 2, 0);
		parentPoint.transform.localScale = Vector3.one;

        CreateEnemiesAroundPoint(10, parentPoint.transform, collider.radius);
	}

	public void CreateEnemiesAroundPoint(int num, Transform parent, float radius)
	{
		for (int i = 0; i < num; i++)
		{
			/* Distance around the circle */
			var radians = 2 * Mathf.PI / num * i;

			/* Get the vector direction */
			var vertical = MathF.Sin(radians);
			var horizontal = MathF.Cos(radians);

			var spawnDir = new Vector3(horizontal, 0, vertical);

			/* Get the spawn position */
			var spawnPos = parent.transform.localPosition + spawnDir * radius; // Radius is just the distance away from the point

			/* Now spawn */
			var point = new GameObject($"({i + 1})");
			point.transform.SetParent(parent);
			point.transform.localPosition = spawnPos;
            point.transform.localScale = Vector3.one;

            m_Points.Add(point.transform);

        }
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
        foreach (var point in m_Points)
        {
            Gizmos.DrawSphere(point.position, 0.1f);
        }
    }
}
