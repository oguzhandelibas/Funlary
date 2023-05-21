using UnityEngine;

namespace Funlary.UIModule.Menu.Unit
{
    public class UnitManager : AbstractSingleton<UnitManager>
    {
        public UnitData[] UnitDatas;
        [SerializeField] private Transform unitBlockParent;
        [SerializeField] private GameObject unitBlockPrefab;

        private void Start()
        {
            if (UnitDatas.Length < 1) Debug.LogError("Insufficient Data!");
            foreach (var item in UnitDatas)
            {
                UnitBlock unitBlock = 
                    Instantiate(unitBlockPrefab, unitBlockParent).GetComponent<UnitBlock>();
                int UnitOrder = (int)item.UnitType;
                string UnitName = "Unit " + UnitOrder + " - " + item.UnitType;
                bool active = UnitOrder == 4;
                unitBlock.SetUnit(UnitName, item.UnitExplanation, UnitOrder, active);
            }
        }
    }
}
