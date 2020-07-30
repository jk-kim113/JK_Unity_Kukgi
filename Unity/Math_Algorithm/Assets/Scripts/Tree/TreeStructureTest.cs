using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeStructureTest : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Text[] _txtBillBoard;
    [SerializeField]
    Text[] _txtRandomNum;
#pragma warning restore

    int[] _numArr = new int[15];
    int _count = 0;

    BSTarray btArr = new BSTarray();

    void Start()
    {
        #region Binary Searching
        //int[] numArr = new int[10];
        //BinaryTree<int> bt = new BinaryTree<int>();

        //for(int n = 0; n < 10; n++)
        //{
        //    numArr[n] = Random.Range(0, 100);
        //    Debug.Log(numArr[n]);
        //    if (!bt.Add(numArr[n]))
        //        n--;
        //}

        //Debug.Log("=================");

        //bt._Root = bt.Remove(numArr[Random.Range(0, 10)], bt.Root());
        //bt.IteratorDown(bt.Root());
        #endregion

        #region Heap Tree
        //int[] numArr = new int[10];
        //HeapTree<int> ht = new HeapTree<int>();

        //for (int n = 0; n < 10; n++)
        //{
        //    numArr[n] = Random.Range(0, 100);
        //    Debug.Log(numArr[n]);
        //    ht.Add(numArr[n]);
        //}
        //Debug.Log("=================");
        //ht.IteratorDown(ht.Root());

        //Debug.Log("=================");
        //ht.Remove();
        //ht.IteratorDown(ht.Root());

        //Debug.Log("=================");
        //ht.Remove();
        //ht.IteratorDown(ht.Root());
        #endregion

        #region AVL Tree

        //AVLTree<int> at = new AVLTree<int>();

        //for (int n = 0; n < 15; n++)
        //{
        //    int random = Random.Range(1, 100);
        //    _numArr[n] = random;
        //    Debug.Log(random);
        //    at.Add(random);
        //}

        //Debug.Log("=================");
        //at.IteratorDown(at.Root());

        //Debug.Log("=================");
        //at.Delete(_numArr[3]);
        //at.Delete(_numArr[6]);
        //at.Delete(_numArr[9]);
        //at.Delete(_numArr[12]);

        //at.IteratorDown(at.Root());

        #endregion

        for (int n = 0; n < _numArr.Length; n++)
        {
            _numArr[n] = Random.Range(1, 101);
            _txtRandomNum[n].text = _numArr[n].ToString();
        }
            

    }

    public void AddButton()
    {
        _txtRandomNum[_count].color = Color.red;
        btArr.Add(_numArr[_count++]);

        for (int n = 0; n < _txtBillBoard.Length; n++)
        {
            if (btArr._Arr[n] < 0)
                _txtBillBoard[n].text = "0";
            else
                _txtBillBoard[n].text = btArr._Arr[n].ToString();
        }
    }
}
