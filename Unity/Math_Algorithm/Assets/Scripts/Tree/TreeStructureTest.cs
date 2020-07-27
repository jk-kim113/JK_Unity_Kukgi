using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStructureTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TreeTest<string> tt = new TreeTest<string>();
        TreeNode<string> rootNode = new TreeNode<string>("Root");
        tt.Add(rootNode);
        TreeNode<string> node0 = new TreeNode<string>("node0");
        tt.Add(node0);
        TreeNode<string> node1 = new TreeNode<string>("node1");
        tt.Add(node1);
        TreeNode<string> node01 = new TreeNode<string>("node01");
        tt.Add(node01);
        TreeNode<string> node02 = new TreeNode<string>("node02");
        tt.Add(node02);
        //TreeNode<string> node10 = new TreeNode<string>("node10");
        //tt.Add(node10);
        //tt.Add("Root");
        //tt.Add("node0");
        //tt.Add("node1");
        //tt.Add("node01");
        //tt.Add("node02");
        //tt.Add("node10");
        //tt.Add("node11");

        //Debug.Log(tt.size);

        tt.IteratorDown(tt.Root());
    }

}
