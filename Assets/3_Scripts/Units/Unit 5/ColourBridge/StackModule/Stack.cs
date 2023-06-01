using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Funlary.Unit5.StackModule
{
    public class Stack : MonoBehaviour, IStack
    {
        public void MoveTo(Transform parent, float height)
        {
            transform.SetParent(parent);
            transform.localScale = new Vector3(1, 1, 1);
            transform.DOLocalMove(Vector3.zero + (height*Vector3.up), .1f).SetEase(Ease.Linear);
            transform.localRotation = Quaternion.Euler(0,0,0);
        }

        public void DropStack()
        {
            throw new NotImplementedException();
        }
    }
}
