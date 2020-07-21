using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTest : MonoBehaviour
{
    //CharacterController _cc;

    private void Awake()
    {
        //_cc = GetComponent<CharacterController>();


    }

    private void Start()
    {
        //PVector v1 = new PVector(5, 2.4f, 3);
        //PVector v2 = new PVector(3.1f, 0.8f, 2.6f);

        //// 연산자 오버로드는 public, static이어야만 한다.
        //PVector v3 = v1 + v2;
        //Debug.Log(string.Format("덧셈 ==> X:{0}, Y:{1}, Z:{2}", v3.X, v3.Y, v3.Z));
        //v3 = v1 - v2;
        //Debug.Log(string.Format("뺄셈 ==> X:{0}, Y:{1}, Z:{2}", v3.X, v3.Y, v3.Z));
        //v3 = 3 * v1;
        //Debug.Log(string.Format("곱셈1 ==> X:{0}, Y:{1}, Z:{2}", v3.X, v3.Y, v3.Z));
        //v3 = v2 * 4;
        //Debug.Log(string.Format("곱셈2 ==> X:{0}, Y:{1}, Z:{2}", v3.X, v3.Y, v3.Z));
        //v3 = v1 / 5;
        //Debug.Log(string.Format("나눗셈 ==> X:{0}, Y:{1}, Z:{2}", v3.X, v3.Y, v3.Z));
        //Debug.Log(string.Format("v1의 크기 : {0}, v2의 크기 : {1}", PVector.Magnitude(v1), PVector.Magnitude(v2)));
        //v3 = v3.normalize;
        //Debug.Log(string.Format("v3의 정규화 ==> X:{0}, Y:{1}, Z:{2}", v3.X, v3.Y, v3.Z));
        //Debug.Log(string.Format("v1, v2의 내적 ==> {0}", PVector.Dot(v1, v2)));
        //v3 = PVector.Cross(v1, v2);
        //Debug.Log(string.Format("v1, v2의 외적 ==> X:{0}, Y:{1}, Z:{2}", v3.X, v3.Y, v3.Z));
    }

    private void Update()
    {
        //float mx = Input.GetAxis("Horizontal");
        //float mz = Input.GetAxis("Vertical");

        //PVector mv = new PVector(mx, 0, mz);
        //mv = (mv.magnitude > 1) ? mv.normalize : mv;
        //Vector3 vel = PVector.ToVec(mv);

        //transform.position += vel * Time.deltaTime * 5;
        ////transform.Translate(vel * Time.deltaTime * 5);
        ////_cc.Move(vel * Time.deltaTime * 5);
    }
}
