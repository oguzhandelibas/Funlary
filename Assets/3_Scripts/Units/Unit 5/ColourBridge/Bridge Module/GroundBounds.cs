using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary
{
    public class GroundBounds : MonoBehaviour
    {
        [SerializeField] private PoleController[] corners;

        private void Start()
        {
            CreateCorners();
        }

        private void CreateCorners()
        {
            for (int i = 0; i < corners.Length; i++)
            {
                corners[i].CreateRope();
            }
        }
    }
}
