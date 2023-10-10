using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary.MyRoom
{
    public class PurchaseArea : MonoBehaviour
    {
        [SerializeField] private PurchaseItemTypes purchaseItemType;
        [SerializeField] private int price = 25;

        private void OnTriggerEnter(Collider other)
        {
        }

        private void OnTriggerExit(Collider other)
        {
        }
    }
}
