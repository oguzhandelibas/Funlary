using UnityEngine;

namespace Funlary.UIModule.Menu.Unit
{
    public class UnitManager : MonoBehaviour
    {
        [SerializeField] private UnitData[] unitDatas;
        [SerializeField] private Transform unitBlockParent;
        [SerializeField] private GameObject unitBlockPrefab;


        private void Start()
        {
            if (unitDatas.Length < 1) Debug.LogError("Insufficient Data!");
            foreach (var item in unitDatas)
            {
                UnitBlock unitBlock = Instantiate(unitBlockPrefab, unitBlockParent).GetComponent<UnitBlock>();
                int UnitOrder = (int)item.UnitTypes + 1;
                string UnitName = "Unit " + UnitOrder + " - " + item.UnitTypes;
                bool active = UnitOrder == 5;
                unitBlock.SetUnit(UnitName, item.UnitExplanation, active);
            }
        }
    }
}
