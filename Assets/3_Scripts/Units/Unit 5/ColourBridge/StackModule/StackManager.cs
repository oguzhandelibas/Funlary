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
        public Vector3 stackAreaSize;
        public Vector3 safeArea;
        private Vector3[] stackPositions;

        [Header("Vertical")]
        public float verticalStartPoint = 1.0f;
        public float stackLength = 1.5f;
        public float verticalSpaceBetweenStacks = 2.5f;

        [Header("Horizontal")]
        public float horizontalStartPoint = 1.0f;
        public float stackWidth = 1.5f;
        public float horizontalSpaceBetweenStacks = 2.5f;
        public int opponentCount;

        void Start()
        {
            GenerateAllStacks();
        }

        private Vector3 CalculateStackCount() => stackAreaMeshRenderer.bounds.size - safeArea;

        private void GenerateAllStacks()
        {
            stackAreaSize = CalculateStackCount();
            float width = stackAreaSize.x / 2;
            float height = stackAreaSize.z / 2;

            Vector2 leftDownCorner = new Vector2(-width, -height);
            Vector2 leftUpCorner = new Vector2(-width, height);
            Vector2 rightDownCorner = new Vector2(width, -height);
            Vector2 rightUpCorner = new Vector2(width, height);

            Vector3 stackPosition = new Vector3(leftDownCorner.x, 0.0f, leftDownCorner.y);

            int horizontalStackCount = (int)((rightDownCorner.x - leftDownCorner.x) / stackWidth);
            int verticalStackCount = (int)((leftUpCorner.y - leftDownCorner.y) / stackLength);
            stackPositions = new Vector3[horizontalStackCount * verticalStackCount];
            int stackIndex = 0;
            for (int i = 0; i < horizontalStackCount; i++)
            {
                for (int j = 0; j < verticalStackCount; j++)
                {
                    Vector3 stackLastPos = stackPosition + new Vector3(((j + horizontalStartPoint) * horizontalSpaceBetweenStacks), 0, ((i + verticalStartPoint) * verticalSpaceBetweenStacks));
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

        void OnDrawGizmosSelected()
        {
            // Draw a semitransparent red cube at the transforms position
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(transform.position, stackAreaSize);
        }
    }
}
