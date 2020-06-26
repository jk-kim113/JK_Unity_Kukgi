using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    GameObject _prefabLoadingWnd;
#pragma warning restore

    EnumClass.eTypeScene _currentScene;
    EnumClass.eTypeScene _prevScene;
    EnumClass.eStateLoadding _loadState;

    //AsyncOperation _asyncOp;
    //LoadingWindow _wndLoading;
    //float _timeCheck;

    static SceneControlManager _uniqueInstance;
    public static SceneControlManager _instance { get { return _uniqueInstance; } }

    public EnumClass.eStateLoadding _nowLoadingState { get { return _loadState; } }

    private void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //SceneManager.LoadSceneAsync("HomeScene");
        StartLoadHomeScene();
    }

    private void Update()
    {
        if(_loadState == EnumClass.eStateLoadding.EndLoad)
        {
            StopCoroutine("LoadingPrecess");
            _loadState = EnumClass.eStateLoadding.none;
        }

        //if(_asyncOp != null)
        //{
        //    if (!_asyncOp.isDone)
        //    {// 로딩중 처리
        //        _wndLoading.SettingLoadRate(_asyncOp.progress);
        //        _loadState = EnumClass.eStateLoadding.Loading;
        //    }
        //    else
        //    {// 로딩 종료시 = 다음신으로 넘어가는데
        //        _timeCheck += Time.deltaTime;
        //        _wndLoading.SettingLoadRate(1);
        //        if(_timeCheck >= 1.2f)
        //        {
        //            _loadState = EnumClass.eStateLoadding.EndLoad;
        //            Destroy(_wndLoading.gameObject);
        //            _asyncOp = null;
        //        }
        //    }
        //}
    }

    IEnumerator LoadingPrecess(string sceneName)
    {   
        _loadState = EnumClass.eStateLoadding.Load;
        GameObject go = Instantiate(_prefabLoadingWnd, transform);
        LoadingWindow wnd = go.GetComponent<LoadingWindow>();
        wnd.OpenLoadingWnd();

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        _loadState = EnumClass.eStateLoadding.Loading;
        while (!asyncOp.isDone)
        {
            wnd.SettingLoadRate(asyncOp.progress);
            yield return null;
        }

        wnd.SettingLoadRate(1);
        yield return new WaitForSeconds(1.8f);

        _loadState = EnumClass.eStateLoadding.LoadingDone;
        Destroy(wnd.gameObject);
        yield return new WaitForSeconds(1f);
        _loadState = EnumClass.eStateLoadding.EndLoad;
    }

    public void StartLoadHomeScene()
    {
        _prevScene = _currentScene;
        _currentScene = EnumClass.eTypeScene.Home;
        StartCoroutine(LoadingPrecess("HomeScene"));
        SoundManager._instance.PlayBGMSound(SoundManager.eTypeBGM.HOME);

        //_timeCheck = 0;
        //_loadState = EnumClass.eStateLoadding.Load;
        //GameObject go = Instantiate(_prefabLoadingWnd, transform);
        //_wndLoading = go.GetComponent<LoadingWindow>();
        //_wndLoading.OpenLoadingWnd();
        //_asyncOp = SceneManager.LoadSceneAsync("HomeScene");
    }

    public void StartLoadIngameScene(int stageNum)
    {
        _prevScene = _currentScene;
        _currentScene = EnumClass.eTypeScene.Ingame;
        UserInfoManager._instance._nowStageNum = stageNum;
        StartCoroutine(LoadingPrecess("IngameScene"));
        SoundManager._instance.PlayBGMSound(SoundManager.eTypeBGM.INGAME);

        //_timeCheck = 0;
        //_loadState = EnumClass.eStateLoadding.Load;
        //GameObject go = Instantiate(_prefabLoadingWnd, transform);
        //_wndLoading = go.GetComponent<LoadingWindow>();
        //_wndLoading.OpenLoadingWnd();
        //_asyncOp = SceneManager.LoadSceneAsync("IngameScene");
    }
}
