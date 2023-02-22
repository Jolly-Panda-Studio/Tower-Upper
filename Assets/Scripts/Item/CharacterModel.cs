using UnityEngine;

namespace Lindon.TowerUpper.Data
{
    public class CharacterModel : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer m_CharacterRenderer;
        [SerializeField] private SkinnedMeshRenderer m_ClockRenderer;
        [SerializeField] private Mesh m_CharcterMesh;
        [SerializeField] private Mesh m_ClockMesh;

        [ContextMenu("Set")]
        private void Set()
        {
            m_CharacterRenderer.sharedMesh = m_CharcterMesh;
            m_ClockRenderer.sharedMesh = m_ClockMesh;
        }
    }
}