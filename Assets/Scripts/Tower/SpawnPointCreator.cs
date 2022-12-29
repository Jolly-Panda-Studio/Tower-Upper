using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointCreator : MonoBehaviour
{
	private List<Transform> m_Points = new List<Transform>();

	public List<Transform> Points => m_Points;

	[SerializeField] private MeshRenderer m_Renderer;
	[SerializeField] private int m_PointCount = 10;
    private void Start()
	{
		var collider = m_Renderer.GetComponent<CapsuleCollider>();
		var radius = m_Renderer.transform.localScale.x / 2;

        GameObject pointParent = new GameObject("SpawnPoints");
		var pointParentTransform = pointParent.transform;

        pointParentTransform.SetParent(collider.transform);
		pointParentTransform.localPosition = new Vector3(0, -collider.height / 2, 0);
        pointParentTransform.localScale = Vector3.one;

        CreateEnemiesAroundPoint(m_PointCount, pointParentTransform, radius);

        pointParentTransform.SetParent(transform);
        pointParentTransform.localScale = Vector3.one;
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
