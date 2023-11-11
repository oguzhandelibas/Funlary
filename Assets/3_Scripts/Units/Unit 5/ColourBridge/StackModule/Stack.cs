using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Funlary.Unit5.ColourBridge.BridgeModule;
using Funlary.Unit5.OpponentModule;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

namespace Funlary.Unit5.StackModule
{
    public class Stack : MonoBehaviour, IStack
    {
        #region FIELDS

        [SerializeField] private TrailRenderer[] trailRenderers;
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
            SetTrailRendererActiveness(true);
        }
        #endregion

        #region STEP FUNCTIONS
        public void Collect(Opponent opponent, Transform parent, float height)
        {
            if (SetAsStairStep) return;
            Vector3 scale = transform.localScale;
            Destroy(transform.GetComponent<BoxCollider>());
            transform.SetParent(parent);
            transform.localScale = scale;
            transform.DOLocalMove(Vector3.zero + (height * (Vector3.up / 4)), .1f).
                SetEase(Ease.Linear)
                .OnComplete((() => SetTrailRendererActiveness(false, 100)));
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            SetColor(opponent.ColorType, opponent.colorData);

            stackManager.GenerateStackAsync(StackIndex, StackColorType);
        }

        public void SetAsStep(Vector3 position)
        {
            //SetColor(stackMaterial);
            transform.SetParent(null);
            //transform.localScale = new Vector3(1, 1, 1);
            transform.DOLocalMove(position, .1f).SetEase(Ease.Linear).
                OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        public void DropStack(bool destroyAfter = false)
        {
            transform.SetParent(stackParent);
            
            BoxCollider boxCollider = transform.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            boxCollider.size = new Vector3(3.0f, 0.25f, 2.0f);

            Vector3 startPos = transform.localPosition;
            Vector3 controlPos = startPos + new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(4.0f, 7.0f), Random.Range(-6.0f, 6.0f));
            Vector3 endPos = new Vector3(controlPos.x, transform.localScale.y / 2, controlPos.z + 2);
            Vector3[] path = { startPos, controlPos, endPos };

            Material stackMatTemp = StackMaterial;
            stackMatTemp.color = Color.gray;

            if (destroyAfter)
            {
                transform.DOPath(path, 1.0f, PathType.CatmullRom).OnComplete((() => Destroy(gameObject)));
            }
            else
            {
                transform.DOPath(path, 1.0f, PathType.CatmullRom).OnComplete(() => { CanCollectable = true; });
            }
        }

        public void SetColor(ColorType targetColorType, ColorData colorData, float duration = 0.3f)
        {
            StackColorType = targetColorType;
            StackMaterial.DOColor(colorData.ColorType[targetColorType].color, duration)
                .From(startColor)
                .OnComplete((() => StackMaterial = colorData.ColorType[targetColorType]));
        }


        private async void SetTrailRendererActiveness(bool value, int delay = 0)
        {
            await Task.Delay(delay);
            foreach (var item in trailRenderers)
            {
                item.gameObject.SetActive(value);
            }
        }

        #endregion
    }
}
