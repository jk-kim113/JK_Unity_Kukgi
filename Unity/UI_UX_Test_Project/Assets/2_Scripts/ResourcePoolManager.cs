using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoolManager : MonoBehaviour
{
    static ResourcePoolManager _uniqueInstance;
    public static ResourcePoolManager _instance { get { return _uniqueInstance; } }

    Dictionary<int, ItemData> _dicItemData = new Dictionary<int, ItemData>();
    public Dictionary<int, ItemData> _ItemDataDic { get { return _dicItemData; } }

    Dictionary<int, int> _dicItemNum = new Dictionary<int, int>();

    private void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MakeItemData();
    }

    public void BuyItem(int id)
    {
        if (!_dicItemNum.ContainsKey(id))
            _dicItemNum.Add(id, 1);
        else
            _dicItemNum[id]++;
    }

    void MakeItemData()
    {
        ItemData item = new ItemData("체력 포션(소)", "체력을 {0}만큼 회복한다.", 3, ItemData.eTypeItem.Consume);
        _dicItemData.Add(0, item);
        item = new ItemData("체력 포션(중)", "체력을 {0}만큼 회복한다.", 8, ItemData.eTypeItem.Consume);
        _dicItemData.Add(1, item);
        item = new ItemData("체력 포션(대)", "체력을 {0}만큼 회복한다.", 15, ItemData.eTypeItem.Consume);
        _dicItemData.Add(2, item);
        item = new ItemData("마나 포션(소)", "마나를 {0}만큼 회복한다.", 1, ItemData.eTypeItem.Consume);
        _dicItemData.Add(3, item);
        item = new ItemData("마나 포션(중)", "마나를 {0}만큼 회복한다.", 4, ItemData.eTypeItem.Consume);
        _dicItemData.Add(4, item);
        item = new ItemData("마나 포션(대)", "마나를 {0}만큼 회복한다.", 8, ItemData.eTypeItem.Consume);
        _dicItemData.Add(5, item);
        item = new ItemData("낡은 대검", "공격력이 {0}만큼 상승한다.", 10, ItemData.eTypeItem.Equip);
        _dicItemData.Add(6, item);
        item = new ItemData("강화된 대검", "공격력이 {0}만큼 상승한다.", 30, ItemData.eTypeItem.Equip);
        _dicItemData.Add(7, item);
        item = new ItemData("낡은 가죽", "방어력이 {0}만큼 상승한다.", 8, ItemData.eTypeItem.Equip);
        _dicItemData.Add(8, item);
        item = new ItemData("강화된 가죽", "방어력이 {0}만큼 상승한다.", 15, ItemData.eTypeItem.Equip);
        _dicItemData.Add(9, item);
    }
}
