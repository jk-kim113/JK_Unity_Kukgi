using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    Animator _animCtrl;

    float _randomSpeed;

    float _intervalJump;
    float _timeCheck;

    float _hp = 100;

    GameObject _prefabScoreEffect;

    private void Awake()
    {
        _animCtrl = GetComponent<Animator>();
        _randomSpeed = Random.Range(1.0f, 6.0f);
        _intervalJump = Random.Range(0.3f, 3.0f);

        _prefabScoreEffect = Resources.Load("Prefabs/ScoreUIEffect") as GameObject;
    }

    private void Update()
    {
        _timeCheck += Time.deltaTime;
        if (_timeCheck > _intervalJump)
        {
            _timeCheck = 0;
            _intervalJump = Random.Range(0.1f, 2.0f);
            _animCtrl.speed = 3.0f;
            _animCtrl.SetTrigger("Jump");
        }

        _animCtrl.speed = 1.0f;
        _animCtrl.SetFloat("Speed", _randomSpeed);
        transform.Translate(Vector3.left * Time.deltaTime * _randomSpeed);
    }

    public void Hit()
    {
        _hp -= 20;
        if(_hp <= 0)
        {
            Instantiate(_prefabScoreEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
