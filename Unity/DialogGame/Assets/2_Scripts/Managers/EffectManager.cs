using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : TSingleton<EffectManager>
{
    public enum eKindEffect
    {
        MouseClick,
        MiniGameWin,
        MiniGameLose,

        max
    }

    List<GameObject>[] mPool;

    protected override void Init()
    {
        base.Init();
        PoolSetUp();
    }

    void PoolSetUp()
    {
        mPool = new List<GameObject>[(int)eKindEffect.max];
        for (int i = 0; i < mPool.Length; i++)
        {
            mPool[i] = new List<GameObject>();
        }
    }

    public GameObject GetEffect(eKindEffect eff)
    {
        for (int i = 0; i < mPool[(int)eff].Count; i++)
        {
            if (!mPool[(int)eff][i].gameObject.activeInHierarchy)
            {
                mPool[(int)eff][i].gameObject.SetActive(true);
                return mPool[(int)eff][i];
            }
        }

        return MakeNewInstance(eff);
    }

    GameObject MakeNewInstance(eKindEffect eff)
    {
        GameObject newObj = Instantiate(
            ResourcePoolManager._instance.GetObj<GameObject>(
                ResourcePoolManager.eResourceKind.Prefab, eff.ToString()), transform);
        mPool[(int)eff].Add(newObj);
        return newObj;
    }
}
