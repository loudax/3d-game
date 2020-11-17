using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSAction : ScriptableObject
{
	public bool enable = true;  
	public bool destroy = false;   
	public GameObject gameobject;  
	public Transform transform;      
	public ISSActionCallback callback; 

	protected SSAction() { }
	public virtual void Start()
	{
		throw new System.NotImplementedException();
	}
	public virtual void Update()
	{
		throw new System.NotImplementedException();
	}
}

public class SSActionManager : MonoBehaviour, ISSActionCallback
{
	private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>(); 
	private List<SSAction> waitingAdd = new List<SSAction>();
	private List<int> waitingDelete = new List<int>();         

	protected void Update()
	{
		foreach (SSAction ac in waitingAdd)
		{
			actions[ac.GetInstanceID()] = ac;
		}
		waitingAdd.Clear();

		foreach (KeyValuePair<int, SSAction> kv in actions)
		{
			SSAction ac = kv.Value;
			if (ac.destroy)
			{
				waitingDelete.Add(ac.GetInstanceID());
			}
			else if (ac.enable)
			{
				ac.Update();
			}
		}

		foreach (int key in waitingDelete)
		{
			SSAction ac = actions[key];
			actions.Remove(key);
			DestroyObject(ac);
		}
		waitingDelete.Clear();
	}

	public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager)
	{
		action.gameobject = gameobject;
		action.transform = gameobject.transform;
		action.callback = manager;
		waitingAdd.Add(action);
		action.Start();
	}

	public void SSActionEvent(SSAction source, int intParam = 0, GameObject objectParam = null)
	{
		if(intParam == 0)
		{
			PatrolFollowAction follow = PatrolFollowAction.GetSSAction(objectParam.gameObject.GetComponent<PatrolData>().player);
			this.RunAction(objectParam, follow, this);
		}
		else
		{
			GoPatrolAction move = GoPatrolAction.GetSSAction(objectParam.gameObject.GetComponent<PatrolData>().start_position);
			this.RunAction(objectParam, move, this);
			Singleton<GameEventManager>.Instance.PlayerEscape();
		}
	}


	public void DestroyAll()
	{
		foreach (KeyValuePair<int, SSAction> kv in actions)
		{
			SSAction ac = kv.Value;
			ac.destroy = true;
		}
	}
}

public class PatrolActionManager : SSActionManager
{
	private GoPatrolAction go_patrol;  
	public void GoPatrol(GameObject patrol)
	{
		go_patrol = GoPatrolAction.GetSSAction(patrol.transform.position);
		this.RunAction(patrol, go_patrol, this);
	}
	public void DestroyAllAction()
	{
		DestroyAll();
	}
}

public class GoPatrolAction : SSAction
{
	private enum Dirction { E, N, W, S };
	private float pos_x, pos_z; 
	private float move_length; 
	private float move_speed = 1.2f; 
	private bool move_sign = true;
	private Dirction dirction = Dirction.E; 
	private PatrolData data;

	private GoPatrolAction() { }
	public static GoPatrolAction GetSSAction(Vector3 location)
	{
		GoPatrolAction action = CreateInstance<GoPatrolAction>();
		action.pos_x = location.x;
		action.pos_z = location.z;
		action.move_length = Random.Range(5, 8);
		return action;
	}
	public override void Update()
	{
		if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
			transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);          
		if (transform.position.y != 0)
			transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		Gopatrol();
		if (data.follow_player && data.wall_sign == data.sign){
			this.destroy = true;
			this.callback.SSActionEvent(this,0,this.gameobject);
		}
	}

	public override void Start()
	{
		data  = this.gameobject.GetComponent<PatrolData>();
	}

	void Gopatrol()
	{
		if (move_sign){
			switch (dirction){
			case Dirction.E:
				pos_x -= move_length;
				break;
			case Dirction.N:
				pos_z += move_length;
				break;
			case Dirction.W:
				pos_x += move_length;
				break;
			case Dirction.S:
				pos_z -= move_length;
				break;
			}
			move_sign = false;
		}
		this.transform.LookAt(new Vector3(pos_x, 0, pos_z));
		float distance = Vector3.Distance(transform.position, new Vector3(pos_x, 0, pos_z));
		if (distance > 0.9){
			transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(pos_x, 0, pos_z), move_speed * Time.deltaTime);
		}
		else{
			dirction = dirction + 1;
			if(dirction > Dirction.S)dirction = Dirction.E;
			move_sign = true;
		}
	}
}

public class PatrolFollowAction : SSAction
{
	private float speed = 2f;
	private GameObject player;
	private PatrolData data;
	
	private PatrolFollowAction() { }
	public static PatrolFollowAction GetSSAction(GameObject player)
	{
		PatrolFollowAction action = CreateInstance<PatrolFollowAction>();
		action.player = player;
		return action;
	}
	public override void Update()
	{
		if (transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
			transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);          
		if (transform.position.y != 0)
			transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		Follow();
		if (!data.follow_player || data.wall_sign != data.sign){
			this.destroy = true;
			this.callback.SSActionEvent(this,1,this.gameobject);
		}
	}
	public override void Start()
	{
		data = this.gameobject.GetComponent<PatrolData>();
	}
	void Follow()
	{
		transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
		this.transform.LookAt(player.transform.position);
	}
}
