using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControlManager : MonoBehaviour
{
    public enum eSceneType
    {
        START       = 0,
        LOBBY,
        INGAME
    }

    public enum eLoadingState
    {
        none        =  0,
        LoadSceneStart,
        LoadingScene,
        LoadSceneEnd,
        UnloadStageStart,
        UnloadingStage,
        UnloadEndStage,
        LoadStageStart,
        LoadingStage,
        LoadStageEnd,
        LoadEnd
    }

    eSceneType _nowSceneType;
    eSceneType _oldSceneType;

    eLoadingState _currentStateLoad;

    LoadingWindow _loadingWnd;

    int _nowStageNumber, _oldStageNumber;

    static SceneControlManager _uniqueInstance;
    public static SceneControlManager _instance { get { return _uniqueInstance; } }

    public eLoadingState _nowLoadingSate { get { return _currentStateLoad; } }

    private void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //임시
        StartSceneIngame();
    }

    public void StartSceneLobby()
    {

    }

    public void StartSceneIngame(int stageNum = 1)
    {
        _oldSceneType = _nowSceneType;
        _nowSceneType = eSceneType.INGAME;
        _oldStageNumber = _nowStageNumber;
        _nowStageNumber = stageNum;

        StartCoroutine(LoadingScene("IngameScene", stageNum));
    }

    IEnumerator LoadingScene(string sceneName, int stageNumber)
    {
        AsyncOperation aOper;
        Scene aScene;

        GameObject go = Resources.Load("Prefabs/UI/LoadingWindowFrame") as GameObject;
        _loadingWnd = go.GetComponent<LoadingWindow>();
        Instantiate(go, transform);
        _loadingWnd.gameObject.SetActive(true);
        
        // Scene을 로드
        _currentStateLoad = eLoadingState.LoadSceneStart;
        aOper = SceneManager.LoadSceneAsync(sceneName);
        _loadingWnd.gameObject.SetActive(true);
        _loadingWnd.ShowLoadingTarget(sceneName + " Loading");
        _loadingWnd.ShowLoadingBar(0);
        yield return new WaitForSeconds(1f);
        while (!aOper.isDone)
        {
            _loadingWnd.ShowLoadingBar(aOper.progress);
            _currentStateLoad = eLoadingState.LoadingScene;
            yield return null;
        }

        _loadingWnd.ShowLoadingBar(1);
        aScene = SceneManager.GetSceneByName(sceneName);
        _currentStateLoad = eLoadingState.LoadSceneEnd;

        yield return new WaitForSeconds(5f);

        if (_nowSceneType == eSceneType.INGAME)
        {// IngameScene일 경우 스테이지를 로드한다, 그리고 SetActiveScene을 StageScene으로 한다
            string StageName = "Stage";
            //if (_oldSceneType == eSceneType.INGAME)
            //{   
            //    _currentStateLoad = eLoadingState.UnloadStageStart;
            //    /// 과거의 Scene이 IngameScene이고 현재의 Scene도 IngameScene이라면
            //    /// 현재 Stage(Old)를 Unload하고 다음 Stage(now)를 load하도록 한다.
            //    aOper = SceneManager.UnloadSceneAsync(StageName + _oldStageNumber.ToString());
            //    while (!aOper.isDone)
            //    {
            //        _currentStateLoad = eLoadingState.UnloadingStage;
            //        yield return null;
            //    }

            //    _currentStateLoad = eLoadingState.UnloadEndStage;
            //    yield return new WaitForSeconds(5f);
            //}

            _currentStateLoad = eLoadingState.LoadStageStart;
            aOper = SceneManager.LoadSceneAsync(StageName + stageNumber.ToString(), LoadSceneMode.Additive);
            _loadingWnd.gameObject.SetActive(true);
            _loadingWnd.ShowLoadingTarget(StageName + stageNumber.ToString() + " Loading");
            _loadingWnd.ShowLoadingBar(0);
            yield return new WaitForSeconds(1f);
            while (!aOper.isDone)
            {
                _loadingWnd.ShowLoadingBar(aOper.progress);
                _currentStateLoad = eLoadingState.LoadingStage;
                yield return null;
            }

            _loadingWnd.ShowLoadingBar(1);
            aScene = SceneManager.GetSceneByName(StageName + stageNumber.ToString());
            _currentStateLoad = eLoadingState.LoadStageEnd;
            yield return new WaitForSeconds(5f);
        }

        SceneManager.SetActiveScene(aScene);
        yield return new WaitForSeconds(1f);
        _currentStateLoad = eLoadingState.LoadEnd;
        Destroy(_loadingWnd.gameObject);

        yield return null;
    }
}
