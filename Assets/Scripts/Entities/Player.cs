using UnityEngine;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        protected void Awake()
        {
            Gameplay.Gameplay.Player = GetComponent<Character>();
        }
    }
}