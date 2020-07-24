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
        //LinkedTest<string> _lt = new LinkedTest<string>();
        //_lt.AddFirst("first");
        //_lt.AddLast("second");
        //_lt.Add("2", 1);

        //Debug.Log("Size : " + _lt.size);

        //LinkedData<string> node = _lt._Header;
        //while (node != null)
        //{
        //    Debug.Log(node._element);
        //    node = node._next; 
        //}

        ////_lt.RemoveAt(1);
        //_lt.Remove("2");
        //_lt.Remove("second");

        //Debug.Log("Size : " + _lt.size);

        //node = _lt._Header;
        //while (node != null)
        //{
        //    Debug.Log(node._element);
        //    node = node._next;
        //}

        #endregion

        #region Queue
        //QueueArray<int> test1 = new QueueArray<int>();
        //test1.Enqueue(1);
        //test1.Enqueue(2);
        //test1.Enqueue(3);

        //Debug.Log("Size Of Test 1 : " + test1.size);

        //Debug.Log("Front : " + test1.Front());

        //while (!test1.isEmpty)
        //{
        //    Debug.Log(test1.Dequeue());
        //}

        //Debug.Log("Size Of Test 1 : " + test1.size);

        //QueueLinked<int> test2 = new QueueLinked<int>();
        //test2.Enqueue(111);
        //test2.Enqueue(222);
        //test2.Enqueue(333);
        //test2.Enqueue(444);
        //test2.Enqueue(555);

        //Debug.Log("Size Of Test 2 : " + test2.size);

        //Debug.Log("Front : " + test2.Front());

        //while (!test2.isEmpty)
        //{
        //    Debug.Log(test2.Dequeue());
        //}

        //Debug.Log("Size Of Test 2 : " + test2.size);
        #endregion

        #region Stack
        //StackArray<int> test1 = new StackArray<int>();
        //test1.Push(1);
        //test1.Push(2);
        //test1.Push(3);

        //Debug.Log("Size Of Test 1 : " + test1.size);

        //Debug.Log("Top : " + test1.Top());

        //while (!test1.isEmpty)
        //{
        //    Debug.Log(test1.Pop());
        //}

        //Debug.Log("Size Of Test 1 : " + test1.size);

        //StackLinked<int> test2 = new StackLinked<int>();
        //test2.Push(111);
        //test2.Push(222);
        //test2.Push(333);
        //test2.Push(444);
        //test2.Push(555);

        //Debug.Log("Size Of Test 2 : " + test2.size);

        //Debug.Log("Top : " + test2.Top());

        //while (!test2.isEmpty)
        //{
        //    Debug.Log(test2.Pop());
        //}

        //Debug.Log("Size Of Test 2 : " + test2.size);
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
