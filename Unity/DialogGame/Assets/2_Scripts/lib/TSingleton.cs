using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSingleton<T> : MonoBehaviour where T : TSingleton<T>
{
    // volatile - 컴파일시에 메모리에 접근하기위한 키워드. 변수를 고유하게 만듬.
    // 쓰레드가 많으면 많을수록 유용해진다. 어떤 상황에서도 문제가 발생하지 않기 위한 장치.
    // 이 변수에 컴파일 최적화를 적용하지 마라는 명령.
    // 캐시메모리에 접근하지말고 바로 메모리에 넘기기위함.
    static volatile T _uniqueInstance = null;
    static volatile GameObject _uniqueObject = null;

    // 상속을 받지 않은 상황에서는 쓰지 못하게 하기 위해 protected로 생성자를 변경
    protected TSingleton()
    {
    }

    public static T _instance
    {
        get
        {
            if(_uniqueInstance == null)
            {
                // 여러 쓰레드에서 동시 생성을 막기 위해 lock 키워드를 사용. 문제의 발생을 해소 하기 위한 방법
                lock(typeof(T))
                {
                    // _uniqueObject만 null인경우는 확인을 해봐야함
                    if (_uniqueInstance == null && _uniqueObject == null)
                    {
                        _uniqueObject = new GameObject(typeof(T).Name, typeof(T));
                        _uniqueInstance = _uniqueObject.GetComponent<T>();
                        _uniqueInstance.Init();
                    }
                }
            }

            return _uniqueInstance;
        }
    }

    protected virtual void Init()
    {
        DontDestroyOnLoad(gameObject);
    }
}
