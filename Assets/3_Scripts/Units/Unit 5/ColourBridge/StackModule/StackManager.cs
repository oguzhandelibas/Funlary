using System;
using System.Threading.Tasks;
using Funlary.Unit5.ColourBridge.BridgeModule;
using UnityEngine;

namespace Funlary.Unit5.StackModule
{
    public class StackManager : MonoBehaviour
    {
        [SerializeField] private ColorData colorData;
        [SerializeField] private MeshRenderer stackAreaMeshRenderer;
        [SerializeField] private Stack stack;
        public Vector3 stackAreaSize = new Vector3(30, 0, 20);
        private Vector3[] stackPositions;
        public int opponentCount;
        public float stackWidth = 1.5f;
        public float stackLength = 1.5f;

        [Header("Vertical")]
        public float vSafeStart = 1.0f;
        public float vSpaceBetweenStacks = 2.5f;

        [Header("Horizontal")]
        public float hSafeStart = 1.0f;
        public float hSpaceBetweenStacks = 2.5f;


        void Start()
        {
            GenerateAllStacks();
        }

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

            stackPositions = new Vector3[horizontalStackCount * verticalStackCount];
            int stackIndex = 0;

            for (int i = 0; i < horizontalStackCount; i++)
            {
                for (int j = 0; j < verticalStackCount; j++)
                {
                    float xPos = i * (stackWidth + hSpaceBetweenStacks);
                    float yPos = j * (stackLength + vSpaceBetweenStacks);

                    Vector3 stackLastPos = new Vector3(leftDownCorner.x + xPos, 0, leftDownCorner.y + yPos);

                    stackPositions[stackIndex] = stackLastPos;

                    Stack stackTemp = Instantiate(stack, stackLastPos, Quaternion.identity, transform);
                    stackTemp.stackManager = this;
                    stackTemp.StackIndex = stackIndex++;
                    ColorType colorType = colorData.GetRandomColorType();
                    stackTemp.SetColor(colorType, colorData.ColorType[colorType]);
                }
            }
        }


        public async void GenerateStackAsync(int index, ColorType colorType)
        {
            await Task.Delay(5000);
            Stack stackTemp = Instantiate(stack, stackPositions[index], Quaternion.identity, transform);
            stackTemp.stackManager = this;
            stackTemp.StackIndex = index;
            stackTemp.SetColor(colorType, colorData.ColorType[colorType]);
        }

        void OnDrawGizmos()
        {
            // Draw a semitransparent red cube at the transforms position
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.position, stackAreaSize);
        }
    }
}
