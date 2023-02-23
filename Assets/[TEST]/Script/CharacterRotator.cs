using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_objects;
    [SerializeField] private float m_rotateSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in m_objects)
        {
            var roatetTransform = obj.transform;
            roatetTransform.RotateAround(roatetTransform.position, roatetTransform.up, Time.deltaTime * 90f * m_rotateSpeed);
        }
    }
}
