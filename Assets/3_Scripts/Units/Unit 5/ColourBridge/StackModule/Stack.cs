using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace Funlary.Unit5.StackModule
{
    public class Stack : MonoBehaviour, IStack
    {
        private MeshRenderer renderer;
        public Material StackMaterial { get => renderer.material; }
        private Color startColor;
        public bool collectable = true;
        private Transform stackParent;

        private void Start()
        {
            renderer = GetComponent<MeshRenderer>();
            startColor = StackMaterial.color;
            stackParent = transform.parent;
        }

        public void MoveTo(Transform parent, float height)
        {
            SetStackColor(startColor);
            transform.SetParent(parent);
            transform.localScale = new Vector3(1, 1, 1);
            transform.DOLocalMove(Vector3.zero + (height*Vector3.up), .1f).SetEase(Ease.Linear);
            transform.localRotation = Quaternion.Euler(0,0,0);
        }

        public void DropStack()
        {
            collectable = false;
            print(transform.parent);
            transform.SetParent(stackParent);
            print(transform.parent);
            Vector3 startPos = transform.localPosition;
            Vector3 controlPos = startPos + new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(4.0f, 7.0f), Random.Range(-6.0f, 6.0f));
            Vector3 endPos = new Vector3(controlPos.x, transform.localScale.y / 2, controlPos.z + 2);
            Vector3[] path = { startPos, controlPos, endPos };
            
            SetStackColor(Color.gray);
            transform.DOPath(path, 1.0f, PathType.CatmullRom).OnComplete(SetAsColletable);
        }

        public void SetStackColor(Color targetColor)
        {
            StackMaterial.DOColor(targetColor, 0.4f).From(startColor);
        }

        public void SetAsColletable()
        {
            collectable = true;
        }

        public bool CanCollectable()
        {
            return collectable;
        }
    }
}
