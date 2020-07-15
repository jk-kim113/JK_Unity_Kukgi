using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    EnemySight _sight;
#pragma warning restore

    Animator _aniCtrl;

    int _idxHit;
    int _idxDie;

    // 임시
    int _hp;

    private void Awake()
    {
        _aniCtrl = GetComponent<Animator>();
        _hp = 100;
    }

    private void Start()
    {
        _idxHit = Animator.StringToHash("Hit");
        _idxDie = Animator.StringToHash("Die");

        _sight.InitSetting(transform.GetChild(0));
    }

    void Hit()
    {
        _hp -= 10;
        _aniCtrl.SetTrigger(_idxHit);
        if(_hp <= 0)
        {   
            gameObject.layer = 1;
            GetComponent<CapsuleCollider>().enabled = false;
            _aniCtrl.SetTrigger(_idxDie);
            Destroy(gameObject, 5f);
        }
    }
}
