using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogInManager : MonoBehaviour
{
    static LogInManager _uniqueInstance;
    public static LogInManager _instace { get { return _uniqueInstance; } }

    GameObject _joinWndObj;
    GameObject _systemMessageWndObj;
    GameObject _subInfoWndObj;

    JoinWindow _joinWnd;

    private void Awake()
    {
        _uniqueInstance = this;

        _joinWndObj = Resources.Load("Prefabs/JoinWindow") as GameObject;
        _systemMessageWndObj = Resources.Load("Prefabs/SystemMessageWindow") as GameObject;
        _subInfoWndObj = Resources.Load("Prefabs/SubInfoWindow") as GameObject;
    }

    private void Start()
    {
        ClientManager._instance.ConnectServer();
    }

    public void OverlapReslut_ID(bool isOverlap)
    {
        SystemMessageWindow systemMessageWnd = Instantiate(_systemMessageWndObj).GetComponent<SystemMessageWindow>();

        if(isOverlap)
            systemMessageWnd.OpenWnd("해당 아이디는 이미 존재 합니다.");
        else
            systemMessageWnd.OpenWnd("해당 아이디는 사용 가능 합니다.");

        _joinWnd.OverlapResult(isOverlap);
    }

    public void OpenJoinWnd()
    {
        _joinWnd = Instantiate(_joinWndObj).GetComponent<JoinWindow>();
    }

    public void CompleteJoin()
    {
        Destroy(_joinWnd.gameObject);
    }

    public void LogInResult(bool isSuccess, bool isFirst)
    {   
        if(isSuccess)
        {
            if(isFirst)
            {
                Instantiate(_subInfoWndObj);
            }
            else
            {
                SceneManager.LoadScene("LobbyScene");
            }
        }
        else
        {
            SystemMessageWindow systemMessageWnd = Instantiate(_systemMessageWndObj).GetComponent<SystemMessageWindow>();
            systemMessageWnd.OpenWnd("로그인에 실패 하였습니다.");
        }
    }
}
