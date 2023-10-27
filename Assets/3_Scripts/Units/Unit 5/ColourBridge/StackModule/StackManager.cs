using System.Collections.Generic;
using System.Threading.Tasks;
using Funlary.Unit5.ColourBridge.BridgeModule;
using UnityEngine;

namespace Funlary.Unit5.StackModule
{
    public class StackManager : MonoBehaviour
    {
        #region FIELDS

        public GameObject previousArenaBarrier;
        [SerializeField] private ColorData colorData;
        [SerializeField] private Stack stack;
        [SerializeField] private Collider generationStarterCollider;
        #endregion

        #region VARIABLES
        public bool activeOnStart;
        public Vector3 stackAreaSize = new Vector3(30, 0, 20);
        private List<Stack> _stackList = new List<Stack>();
        private Vector3[] _stackPositions;

        [Header("Stack Variables")]
        [SerializeField][Range(0.0f, 10.0f)] private float stackWidth = 1.5f;
        [SerializeField][Range(0.0f, 10.0f)] private float stackLength = 1.5f;

        [Header("Vertical Placement")]
        [SerializeField][Range(0.0f, 10.0f)] private float vSafeStart = 1.0f;
        [SerializeField][Range(0.0f, 10.0f)] private float vSpaceBetweenStacks = 2.5f;

        [Header("Horizontal Placement")]
        [SerializeField][Range(0.0f, 10.0f)] private float hSafeStart = 1.0f;
        [SerializeField][Range(0.0f, 10.0f)] private float hSpaceBetweenStacks = 2.5f;

        #endregion

        #region UNITY

        void Start()
        {
            GenerateAllStacks();
            StackActiveness(activeOnStart);
        }

        #endregion

        #region STACK GENERATION

        private void GenerateAllStacks()
        {
            float width = stackAreaSize.x;
            float height = stackAreaSize.z;

            Vector2 leftDownCorner = new Vector2(-width / 2 + hSafeStart, -height / 2 + vSafeStart);
            Vector2 rightUpCorner = new Vector2(width / 2, height / 2);

            float a = rightUpCorner.x - leftDownCorner.x;
            float b = rightUpCorner.y - leftDownCorner.y;

            int horizontalStackCount = Mathf.FloorToInt(a / (stackWidth + hSpaceBetweenStacks));
            int verticalStackCount = Mathf.FloorToInt(b / (stackLength + vSpaceBetweenStacks));

            _stackPositions = new Vector3[horizontalStackCount * verticalStackCount];
            int stackIndex = 0;

            for (int i = 0; i < horizontalStackCount; i++)
            {
                for (int j = 0; j < verticalStackCount; j++)
                {
                    float xPos = i * (stackWidth + hSpaceBetweenStacks);
                    float yPos = j * (stackLength + vSpaceBetweenStacks);

                    Vector3 stackLastPos = transform.position + new Vector3(leftDownCorner.x + xPos, 0, leftDownCorner.y + yPos);

                    _stackPositions[stackIndex] = stackLastPos;

                    Stack stackTemp = Instantiate(stack, stackLastPos, Quaternion.identity, transform);
                    stackTemp.stackManager = this;
                    stackTemp.StackIndex = stackIndex++;
                    ColorType colorType = colorData.GetRandomColorType(BridgeManager.Instance.GetColorTypes());
                    stackTemp.SetColor(colorType, colorData.ColorType[colorType]);
                    _stackList.Add(stackTemp);
                }
            }
        }

        public void StackActiveness(bool value)
        {
            foreach (var item in _stackList)
            {
                item.gameObject.SetActive(value);
            }
        }

        public async void GenerateStackAsync(int index, ColorType colorType)
        {
            await Task.Delay(5000);
            if (this == null) return;
            Stack stackTemp = Instantiate(stack, _stackPositions[index], Quaternion.identity, transform);
            stackTemp.stackManager = this;
            stackTemp.StackIndex = index;
            stackTemp.SetColor(colorType, colorData.ColorType[colorType]);
        }

        #endregion

        #region DEBUG
        void OnDrawGizmos()
        {
            // Draw a semitransparent red cube at the transforms position
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.position, stackAreaSize);
        }
        #endregion
    }
}
