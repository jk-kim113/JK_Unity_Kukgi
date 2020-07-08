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

    GameObject _prefabLoadingWnd;    

    int _nowStageNumber, _oldStageNumber;

    static SceneControlManager _uniqueInstance;
    public static SceneControlManager _instance { get { return _uniqueInstance; } }

    public eLoadingState _nowLoadingSate { get { return _currentStateLoad; } }

    private void Awake()
    {
        _uniqueInstance = this;
        DontDestroyOnLoad(gameObject);

        _prefabLoadingWnd = Resources.Load("Prefabs/UI/LoadingWindowFrame") as GameObject;
    }

    private void Start()
    {
        //임시
        StartSceneLobby();
    }

    public void StartSceneLobby()
    {
        _oldSceneType = _nowSceneType;
        _nowSceneType = eSceneType.LOBBY;

        StartCoroutine(LoadingScene("LobbyScene", 0));
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

        GameObject go = Instantiate(_prefabLoadingWnd.gameObject, transform);
        LoadingWindow _loadingWnd = go.GetComponent<LoadingWindow>();

        // Scene을 로드
        _currentStateLoad = eLoadingState.LoadSceneStart;
        _loadingWnd.ShowLoadingTarget(sceneName + " Loading");
        aOper = SceneManager.LoadSceneAsync(sceneName);
        
        while (!aOper.isDone)
        {
            yield return null;
            _loadingWnd.ShowLoadingBar(aOper.progress);
            _currentStateLoad = eLoadingState.LoadingScene;
        }

        aScene = SceneManager.GetSceneByName(sceneName);
        _currentStateLoad = eLoadingState.LoadSceneEnd;

        yield return new WaitForSeconds(1.0f);

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
            _loadingWnd.ShowLoadingTarget(StageName + stageNumber.ToString() + " Loading");
            aOper = SceneManager.LoadSceneAsync(StageName + stageNumber.ToString(), LoadSceneMode.Additive);
            
            while (!aOper.isDone)
            {
                yield return null;
                _loadingWnd.ShowLoadingBar(aOper.progress);
                _currentStateLoad = eLoadingState.LoadingStage;
            }

            aScene = SceneManager.GetSceneByName(StageName + stageNumber.ToString());
            _currentStateLoad = eLoadingState.LoadStageEnd;
            yield return new WaitForSeconds(1.0f);
        }

        SceneManager.SetActiveScene(aScene);
        _currentStateLoad = eLoadingState.LoadEnd;
        Destroy(_loadingWnd.gameObject);

        yield return null;
    }
}
