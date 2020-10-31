using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controllor : MonoBehaviour,ISceneControllor,IUserAction
{
	public IActionManager ima;
	public DiskFactory df;
	public UserGUI ug;
	public ScoreRecorder sr;
	public RoundControllor rc;

	private Queue<GameObject> dq = new Queue<GameObject> ();
	private List<GameObject> dfree = new List<GameObject> ();

	private int round = 1;
	private int trail = 0;
	private float t = 1;
	private float speed = 2; //飞碟发射间隔时间
	private bool mode = false; //flag == true 物理学， flag == false 运动学
	private bool modeSetting = true;

	void Start(){
		SSDirector director = SSDirector.GetInstance();     
		director.CurrentSceneControllor = this;             
		df = Singleton<DiskFactory>.Instance;
		sr = gameObject.AddComponent<ScoreRecorder> () as ScoreRecorder;
		ima = gameObject.AddComponent<ActionManagerAdapter>() as IActionManager;
		ug = gameObject.AddComponent<UserGUI>() as UserGUI;
		rc = gameObject.AddComponent<RoundControllor> () as RoundControllor;
		t = speed;
	}

	void Update ()
	{
		if(ug.go){
			t-=Time.deltaTime;

			if(trail > 10){ //超过10次trial后，判为游戏失败
				ug.life = 0;
			}

			if (t < 0) { //发射一个飞碟
				LoadResources ();
				SendDisk ();
				t = speed;
				trail++;
			}
			
			if ((sr.score >= 5 && round == 1) || (sr.score >= 15 && round == 2) || (sr.score >= 30 && round == 3)) { //判断是否进入下一轮
				round++;
				trail = 0;
				rc.loadRoundData (round);
				ug.RecoverBlood();
			}
		}
	}

	public void setting(float speed_)
	{
		speed = speed_;	
	}

	public void LoadResources()
	{
		dq.Enqueue(df.GetDisk(round)); 
	}

	private void SendDisk() //发射飞碟
	{
		float position_x = 16;                       
		if (dq.Count != 0)
		{
			GameObject disk = dq.Dequeue();
			dfree.Add(disk);
			disk.SetActive(true);
			float ran_y = Random.Range(1f, 4f);
			disk.GetComponent<DiskData>().direction = new Vector3(1f, ran_y, 0);
			Vector3 position = new Vector3(-disk.GetComponent<DiskData>().direction.x * position_x, ran_y, 0);
			disk.transform.position = position;
			float power = Random.Range(1f * round, 2f * round);
			float angle = Random.Range(0f, 5f);
			ima.UFOFly(disk, angle, power, ug.go, mode);
		}

		for (int i = 0; i < dfree.Count; i++)
		{
			GameObject temp = dfree[i];
			if (temp.transform.position.x > 20 && temp.gameObject.activeSelf == true)
			{
				df.FreeDisk(dfree[i]);
				dfree.Remove(dfree[i]);
				ug.ReduceBlood();
			}
		}
	}

	public void Hit (Vector3 pos){ //集中飞碟
		Ray ray = Camera.main.ScreenPointToRay(pos);
		RaycastHit[] hits;
		hits = Physics.RaycastAll(ray);
		bool not_hit = false;
		for (int i = 0; i < hits.Length; i++)
		{
			RaycastHit hit = hits[i];
			if (hit.collider.gameObject.GetComponent<DiskData>() != null)
			{
				for (int j = 0; j < dfree.Count; j++)
				{
					if (hit.collider.gameObject.GetInstanceID() == dfree[j].gameObject.GetInstanceID())
					{
						not_hit = true;
					}
				}
				if(!not_hit)
				{
					return;
				}
				dfree.Remove(hit.collider.gameObject);
				sr.Record(hit.collider.gameObject);
				hit.collider.gameObject.transform.position = new Vector3(0, -100, 0);
				df.FreeDisk(hit.collider.gameObject);
				break;
			}
		}
	}

	public void Restart (){ //重启游戏
		SceneManager.LoadScene(0);
	}

	public int GetScore (){ //得到得分
		return sr.score;
	}

	public int GetRound(){ //得到轮次
		return round;
	}

	public int GetTrail(){
		return trail;
	}

	public bool GetMode(){
		return mode;
	}

	public void SetMode(bool flag){
		mode = flag;
	}

	public bool GetModeSetting(){
		return modeSetting;
	}

	public void StartGame(){
		modeSetting = false;
		ug.go = true;
	}

}