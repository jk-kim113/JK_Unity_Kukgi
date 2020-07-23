using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructureTest : MonoBehaviour
{   
    void Start()
    {
        #region ArrayTest
        //ArrayTest<object> _at = new ArrayTest<object>();

        //Debug.Log(_at.isEmpty);

        //Debug.Log(_at.size);
        //_at.Add("안녕1", 1);
        //Debug.Log(_at.size);
        //_at.Add("안녕2", 1);
        //_at.Add("안녕3", 1);
        //_at.Add("안녕4", 1);
        //_at.Add("안녕5", 1);
        //_at.Set(1, "바꿈");
        //_at.Set(10, "바꿈");

        //Debug.Log(_at.isEmpty);

        //for (int n = 0; n < _at.size; n++)
        //{
        //    Debug.Log(string.Format("_at[{0}] : {1}", n, _at[n]));
        //}

        //_at.Remove(1);
        //_at.Remove(1);
        //_at.Remove(1);

        //for (int n = 0; n < _at.size; n++)
        //{
        //    Debug.Log(string.Format("_at[{0}] : {1}", n, _at[n]));
        //}

        //Debug.Log(_at.Get(10));
        #endregion

        #region LinkedTest

        LinkedTest<string> _lt = new LinkedTest<string>();
        _lt.AddFirst("first");
        _lt.AddLast("second");
        _lt.Add("2", 1);

        Debug.Log("Size : " + _lt.size);

        LinkedData<string> node = _lt._Header;
        while (node != null)
        {
            Debug.Log(node._element);
            node = node._next; 
        }

        //_lt.RemoveAt(1);
        _lt.Remove("2");
        _lt.Remove("second");

        Debug.Log("Size : " + _lt.size);

        node = _lt._Header;
        while (node != null)
        {
            Debug.Log(node._element);
            node = node._next;
        }

        #endregion
    }
}

/* 자료 구조
 *      ArrayList
 *          Get
 *          Set
 *          Add
 *          Remove
 *          Size
 *          isEmpty
 */
