using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointCreator : MonoBehaviour
{
	private List<Point> m_Points = new List<Point>();

	public List<Point> Points => m_Points;

	[SerializeField] private MeshRenderer m_Renderer;

	[SerializeField] private float radiusOffset = 0;

	public void CreatePoints(int pointCount)
	{
		var collider = m_Renderer.GetComponent<CapsuleCollider>();
		var radius = (m_Renderer.transform.localScale.x / 2) + radiusOffset;
		var height = m_Renderer.transform.localScale.y * 2;

		GameObject pointParent = new GameObject("SpawnPoints");
		var pointParentTransform = pointParent.transform;

		pointParentTransform.SetParent(collider.transform);
		pointParentTransform.localPosition = new Vector3(0, -collider.height / 2, 0);
		pointParentTransform.localScale = Vector3.one;

		CreateEnemiesAroundPoint(pointCount, pointParentTransform, radius, height);

		pointParentTransform.SetParent(transform);
		pointParentTransform.localScale = Vector3.one;
	}

	private void CreateEnemiesAroundPoint(int num, Transform parent, float radius, float height)
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
			point.transform.localPosition = Vector3.zero;
			point.transform.localScale = Vector3.one;

			var startPoint = new GameObject($"START");
			startPoint.transform.SetParent(point.transform);
			startPoint.transform.localPosition = new Vector3(spawnPos.x, 0, spawnPos.z);
			startPoint.transform.localScale = Vector3.one;

			var finishPoint = new GameObject($"FINISH");
			finishPoint.transform.SetParent(point.transform);
			finishPoint.transform.localPosition = new Vector3(startPoint.transform.localPosition.x, startPoint.transform.localPosition.y + height, startPoint.transform.localPosition.z);
			finishPoint.transform.localScale = Vector3.one;

			m_Points.Add(new Point(startPoint.transform, finishPoint.transform, point.transform));

		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		foreach (var point in m_Points)
		{
			Vector3 start = point.StartPoint.position;
			Vector3 finish = point.FinishPoint.position;

			Gizmos.DrawSphere(start, 0.1f);
			Gizmos.DrawSphere(finish, 0.1f);
			Gizmos.DrawLine(start, finish);
		}
	}
}

public struct Point
{
	public Point(Transform startPoint, Transform finishPoint, Transform centerPoint)
	{
		StartPoint = startPoint ?? throw new ArgumentNullException(nameof(startPoint));
		FinishPoint = finishPoint ?? throw new ArgumentNullException(nameof(finishPoint));
		CenterPoint = centerPoint ?? throw new ArgumentNullException(nameof(centerPoint));
	}

	public Transform StartPoint { get; private set; }
	public Transform FinishPoint { get; private set; }
	public Transform CenterPoint { get; private set; }
}