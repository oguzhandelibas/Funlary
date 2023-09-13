using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Funlary.Unit5.ColourBridge.BridgeModule;
using Random = UnityEngine.Random;

namespace Funlary.Unit5.StackModule
{
    public class Stack : MonoBehaviour, IStack
    {
        #region FIELDS
        public StackManager stackManager;
        public int StackIndex;
        public ColorType StackColorType { get; set; }
        public Material StackMaterial
        {
            get => renderer.material;
            set => renderer.material = value;
        }
        [SerializeField] private MeshRenderer renderer;
        private Color startColor;
        private Transform stackParent;
        #endregion

        #region PROPERTIES

        public bool CanCollectable { get; set; }
        public bool SetAsStairStep { get; set; }

        #endregion

        #region UNITY FUNCTIONS
        private void Start()
        {
            startColor = StackMaterial.color;
            stackParent = transform.parent;
            CanCollectable = true;
        }
        #endregion

        #region STEP FUNCTIONS
        public void MoveTo(Transform parent, float height)
        {
            if (SetAsStairStep) return;
            //SetColor(StackMaterial);
            stackManager.GenerateStackAsync(StackIndex, StackColorType);
            Vector3 scale = transform.localScale;
            Destroy(transform.GetComponent<BoxCollider>());
            transform.SetParent(parent);
            transform.localScale = scale;
            transform.DOLocalMove(Vector3.zero + (height * (Vector3.up / 4)), .1f).SetEase(Ease.Linear);
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }

        public void SetAsStep(Vector3 position)
        {
            //SetColor(stackMaterial);
            transform.SetParent(null);
            //transform.localScale = new Vector3(1, 1, 1);
            transform.DOLocalMove(position, .1f).SetEase(Ease.Linear).OnComplete(() => Destroy(gameObject));
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        public void DropStack()
        {
            transform.SetParent(stackParent);

            Vector3 startPos = transform.localPosition;
            Vector3 controlPos = startPos + new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(4.0f, 7.0f), Random.Range(-6.0f, 6.0f));
            Vector3 endPos = new Vector3(controlPos.x, transform.localScale.y / 2, controlPos.z + 2);
            Vector3[] path = { startPos, controlPos, endPos };

            // Material stackMatTemp = stackMaterial;
            //stackMatTemp.color = Color.gray;
            //SetColor(stackMatTemp);
            transform.DOPath(path, 1.0f, PathType.CatmullRom).OnComplete(SetAsCollectable);
        }

        public void SetColor(ColorType colorType, Material targetMaterial, float duration = 0.3f)
        {
            StackColorType = colorType;
            StackMaterial.DOColor(targetMaterial.color, duration)
                .From(startColor)
                .OnComplete((() => StackMaterial = targetMaterial));
        }

        public void SetColor(ColorType colorType, Color targetColor, float duration = 0.3f)
        {
            StackColorType = colorType;
            Debug.Log(StackMaterial == null);
            StackMaterial.DOColor(targetColor, duration).From(startColor);
        }


        public void SetAsCollectable()
        {
            print("CanCollectable");
            CanCollectable = true;
        }

        #endregion
    }
}
