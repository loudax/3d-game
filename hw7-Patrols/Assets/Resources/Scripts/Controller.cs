using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour, IUserAction, ISceneController
{
    public PropFactory patrol_factory;
    public ScoreRecorder recorder;
    public PatrolActionManager action_manager;
    public int wall_sign = 1;
    public GameObject player;
    public float player_speed = 30; 
	public float rotate_speed = 10; 
    private List<GameObject> patrols;
    private bool game_over = false;

	public int GetScore()
    {
        return recorder.GetScore();
    }

	public bool GetGameover()
    {
        return game_over;
    }

    public bool GetWin()
    {
        return recorder.score == 9;
    }

	public void Restart()
    {
        SceneManager.LoadScene(0);
    }

	void OnEnable()
    {
        GameEventManager.ScoreChange += AddScore;GameEventManager.GameoverChange += Gameover;
    }
	
    void OnDisable()
    {
        GameEventManager.ScoreChange -= AddScore;GameEventManager.GameoverChange -= Gameover;
    }

	void AddScore()
    {
        recorder.AddScore();
    }

	void Gameover(){
        game_over = true;
        action_manager.DestroyAllAction();
    }

    void Start()
    {
		wall_sign = 1;
        SSDirector director = SSDirector.GetInstance();
        director.CurrentSceneController = this;
        patrol_factory = Singleton<PropFactory>.Instance;
        action_manager = gameObject.AddComponent<PatrolActionManager>() as PatrolActionManager;
        LoadResources();
        recorder = Singleton<ScoreRecorder>.Instance;
    }

    void Update()
    {
        for (int i = 0; i < patrols.Count; i++) {
            patrols[i].gameObject.GetComponent<PatrolData>().wall_sign = wall_sign;
        }
		for (int i = 0; i < patrols.Count; i++){
			if (patrols [i].gameObject.GetComponent<PatrolData> ().sign == patrols [i].gameObject.GetComponent<PatrolData> ().wall_sign) {
				patrols [i].gameObject.GetComponent<PatrolData> ().follow_player = true;
				patrols [i].gameObject.GetComponent<PatrolData> ().player = player;
			} else {
				patrols [i].gameObject.GetComponent<PatrolData>().follow_player = false;
				patrols [i].gameObject.GetComponent<PatrolData>().player = null;
			}
		}
    }

    public void LoadResources()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Plane"));
        player = Instantiate(Resources.Load("Prefabs/Player"), new Vector3(-10, 0, 10), Quaternion.Euler(0, 90, 0)) as GameObject;
        patrols = patrol_factory.GetPatrols();
        for (int i = 0; i < patrols.Count; i++){
            action_manager.GoPatrol(patrols[i]);
        }
    }

	public Vector3 movement;
    public void MovePlayer(float translationX, float translationZ)
    {
        if(!game_over)
        {
			Vector3 direction = new Vector3(translationX, 0, translationZ).normalized;
			player.transform.position = Vector3.MoveTowards(player.transform.position, player.transform.position+direction, player_speed * Time.deltaTime);
			player.transform.LookAt(player.transform.position + direction);
        }
    }
}