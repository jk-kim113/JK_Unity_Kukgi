using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeStructureTest : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _billboardRoot;
    [SerializeField]
    GameObject _numberRoot;
#pragma warning restore

    int[] _numArr = new int[31];
    int _count = 0;

    BSTarray btArr = new BSTarray();

    Text[] _txtBillBoard;
    Text[] _txtRandomNum;

    BST<int> bst = new BST<int>();

    void Start()
    {
        _txtBillBoard = _billboardRoot.GetComponentsInChildren<Text>();
        _txtRandomNum = _numberRoot.GetComponentsInChildren<Text>();

        #region Binary Searching
        //int[] numArr = new int[15];
        //BinaryTree<int> bt = new BinaryTree<int>();

        //for (int n = 0; n < numArr.Length; n++)
        //{
        //    numArr[n] = Random.Range(0, 100);
        //    Debug.Log(numArr[n]);
        //    if (!bt.Add(numArr[n]))
        //        n--;
        //}

        //Debug.Log("=================");

        //bt._Root = bt.Remove(numArr[3], bt.Root());
        //bt._Root = bt.Remove(numArr[6], bt.Root());
        //bt._Root = bt.Remove(numArr[9], bt.Root());
        //bt._Root = bt.Remove(numArr[12], bt.Root());
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

        //for (int n = 0; n < _numArr.Length; n++)
        //{
        //    _numArr[n] = Random.Range(1, 101);
        //    _txtRandomNum[n].text = _numArr[n].ToString();
        //}


        bst.TraversalOrder = BST<int>.TraversalMode.PreOrder;

        for (int n = 0; n < 15; n++)
        {
            int random = Random.Range(1, 100);
            _numArr[n] = random;
            _txtRandomNum[n].text = _numArr[n].ToString();
            //Debug.Log(random);
            //bst.Add(random);
        }

        Debug.Log("===============");

        //int[] temp = new int[_numArr.Length];
        //bst.CopyTo(temp);
        //for(int n = 0; n < _txtBillBoard.Length; n++)
        //{
        //    _txtBillBoard[n].text = temp[n].ToString();
        //}
    }

    public void AddButton()
    {
        _txtRandomNum[_count].color = Color.red;
        bst.Add(_numArr[_count++]);

        int[] temp = new int[_txtBillBoard.Length];
        bst.CopyTo(temp);
        for (int n = 0; n < _txtBillBoard.Length; n++)
        {
            if(temp[n] > 0)
                _txtBillBoard[n].color = Color.blue;

            _txtBillBoard[n].text = temp[n].ToString();
        }
    }
}
