using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game {

    public interface ISceneController {
        void LoadResources();
    }

    public interface IUserAction {
        void MoveBoat();                                                              
        void MoveRole(RoleModel role);                         
        void Restart();                                         
    }

    public class SSDirector : System.Object {
        private static SSDirector _instance;
        
        public ISceneController CurrentScenceController { get; set; }
        public static SSDirector GetInstance() {
            if (_instance == null) {
                _instance = new SSDirector();
            }
            return _instance;
        }
    }

    public class LandModel {
        GameObject land;                                
        Vector3[] positions;                            
        bool sign;                                  
        RoleModel[] roles = new RoleModel[6];           

        public LandModel(string land_mark) {
            positions = new Vector3[] {new Vector3(7, 2, 30), new Vector3(9, 2, 30), new Vector3(11, 2, 30), new Vector3(13, 2, 30), new Vector3(15, 2, 30), new Vector3(17, 2, 30)};
            if (land_mark == "start") {
                land = Object.Instantiate(Resources.Load("Prefabs/Land", typeof(GameObject)), new Vector3(12, 0, 30), Quaternion.identity) as GameObject;
                sign = true;
            } else {
                land = Object.Instantiate(Resources.Load("Prefabs/Land", typeof(GameObject)), new Vector3(-12, 0, 30), Quaternion.identity) as GameObject;
                sign = false;
            }
        }

        public int GetEmptyNumber() {
            for (int i = 0; i < roles.Length; i++) {
                if (roles[i] == null)
                    return i;
            }
            return -1;
        }

        public bool GetLandSign() { 
            return sign;
        }

        public Vector3 GetEmptyPosition() {
            Vector3 pos = positions[GetEmptyNumber()];
            if(!sign){
                pos.x = -1 * pos.x;
            }            
            return pos;
        }

        public void AddRole(RoleModel role) {
            roles[GetEmptyNumber()] = role;
        }

        public RoleModel DeleteRoleByName(string name) { 
            for (int i = 0; i < roles.Length; i++) {
                if (roles[i] != null && roles[i].GetName() == name) {
                    RoleModel role = roles[i];
                    roles[i] = null;
                    return role;
                }
            }
            return null;
        }

        public int[] GetRoleNum() {
            int[] count = { 0, 0};                    
            for (int i = 0; i < roles.Length; i++) {
                if (roles[i] != null) {
                    if (roles[i].GetSign() == 0){
                        count[0]++;
                    }
                    else{
                        count[1]++;
                    }
                }
            }
            return count;
        }

        public void Reset() {
            roles = new RoleModel[6];
        }

    }

    public class BoatModel {
        GameObject boat;                                          
        Vector3[] start_empty_pos;                                    
        Vector3[] end_empty_pos;                                      
        Move move;                                                    
        Click click;
        bool sign;                                            
        RoleModel[] roles = new RoleModel[2];                        

        public BoatModel() {
            boat = Object.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject)), new Vector3(4, 0, 30), Quaternion.identity) as GameObject;
            boat.name = "boat";
            start_empty_pos = new Vector3[] { new Vector3(5, 1, 30), new Vector3(3, 1, 30)};
            end_empty_pos = new Vector3[] { new Vector3(-3, 1, 30), new Vector3(-5, 1, 30)};
            move = boat.AddComponent(typeof(Move)) as Move;
            click = boat.AddComponent(typeof(Click)) as Click;
            click.SetBoat(this);
            sign = true; 
        }

        public bool IsEmpty() {
            for (int i = 0; i < roles.Length; i++) {
                if (roles[i] != null){
                    return false;
                } 
            }
            return true;
        }

        public void BoatMove() {
            if(sign){
                move.MovePosition(new Vector3(-4, 0, 30));
                sign = false;
            } 
            else{
                move.MovePosition(new Vector3(4, 0, 30));
                sign = true;
            }
        }

        public bool GetBoatSign(){ 
            return sign;
        }

        public RoleModel DeleteRoleByName(string name) {
            for (int i = 0; i < roles.Length; i++) {
                if (roles[i] != null && roles[i].GetName() == name) {
                    RoleModel role = roles[i];
                    roles[i] = null;
                    return role;
                }
            }
            return null;
        }

        public int GetEmptyNumber() {
            for (int i = 0; i < roles.Length; i++) {
                if (roles[i] == null) {
                    return i;
                }
            }
            return -1;
        }

        public Vector3 GetEmptyPosition() {
            Vector3 pos;
            if (sign) {
                pos = start_empty_pos[GetEmptyNumber()];
            }
            else{
                pos = end_empty_pos[GetEmptyNumber()];
            }  
            return pos;
        }

        public void AddRole(RoleModel role) {
            roles[GetEmptyNumber()] = role;
        }

        public GameObject GetBoat() { 
            return boat;
        }

        public int[] GetRoleNum() {
            int[] count = { 0, 0};
            for (int i = 0; i < roles.Length; i++) {
                if (roles[i] == null)
                    continue;
                if (roles[i].GetSign() == 0)
                    count[0]++;
                else
                    count[1]++;
            }
            return count;
        }

        public void Reset() {
            if (!sign)
                BoatMove();
            roles = new RoleModel[2];
        }
    }

    public class RoleModel {
        GameObject role;
        int role_sign;             
        Click click;
        bool on_boat;                 
        Move move;
        LandModel land_model = (SSDirector.GetInstance().CurrentScenceController as Controllor).start_land;

        public RoleModel(string role_name) {
            if (role_name == "priest") {
                role = Object.Instantiate(Resources.Load("Prefabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, -90, 0)) as GameObject;
                role_sign = 0;
            } else {
                role = Object.Instantiate(Resources.Load("Prefabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, -90, 0)) as GameObject;
                role_sign = 1;
            }
            move = role.AddComponent(typeof(Move)) as Move;
            click = role.AddComponent(typeof(Click)) as Click;
            click.SetRole(this);
        }

        public int GetSign() { 
            return role_sign;
        }

        public LandModel GetLandModel(){
            return land_model;
        }

        public string GetName() { 
            return role.name;
        }

        public bool IsOnBoat() { 
            return on_boat;
        }

        public void SetName(string name) { 
            role.name = name;
        }

        public void SetPosition(Vector3 pos) { 
            role.transform.position = pos;
        }

        public void Move(Vector3 vec) {
            move.MovePosition(vec);
        }

        public void GoLand(LandModel land) {  
            role.transform.parent = null;
            land_model = land;
            on_boat = false;
        }

        public void GoBoat(BoatModel boat) {
            role.transform.parent = boat.GetBoat().transform;
            land_model = null;          
            on_boat = true;
        }

        public void Reset() {
            land_model = (SSDirector.GetInstance().CurrentScenceController as Controllor).start_land;
            GoLand(land_model);
            SetPosition(land_model.GetEmptyPosition());
            land_model.AddRole(this);
        }
    }

    public class Move : MonoBehaviour {
        float move_speed = 100;                   
        int move_sign = 0;                        
        Vector3 end_pos;
        Vector3 middle_pos;

        void Update() {
            if (move_sign == 1) {
                transform.position = Vector3.MoveTowards(transform.position, middle_pos, move_speed * Time.deltaTime);
                if (transform.position == middle_pos)
                    move_sign = 2;
            } 
            else if (move_sign == 2) {
                transform.position = Vector3.MoveTowards(transform.position, end_pos, move_speed * Time.deltaTime);
                if (transform.position == end_pos)
                    move_sign = 0;           
            }
        }

        public void MovePosition(Vector3 position) {
            end_pos = position;
            if (position.y == transform.position.y) {               
                move_sign = 2;
            } 
            else if (position.y < transform.position.y) {     
                middle_pos = new Vector3(position.x, transform.position.y, position.z);
                move_sign = 1;
            } 
            else {                                            
                middle_pos = new Vector3(transform.position.x, position.y, position.z);
                move_sign = 1;
            }
        }
    }

    public class Click : MonoBehaviour {
        IUserAction action;
        RoleModel role = null;
        BoatModel boat = null;

        public void SetRole(RoleModel role) {
            this.role = role;
        }

        public void SetBoat(BoatModel boat) {
            this.boat = boat;
        }

        void Start() {
            action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        }

        void OnMouseDown() {
            if (boat == null && role == null) 
                return;
            if (boat != null)
                action.MoveBoat();
            else if(role != null)
                action.MoveRole(role);
        }
    }

    public class Judge {
        LandModel start_land;
        LandModel end_land;
        BoatModel boat;
        public Judge(LandModel start_,LandModel end_,BoatModel boat_)
        {
            start_land = start_;
            end_land = end_;
            boat = boat_;
        }
        public int Check()
        {
            int start_priest = (start_land.GetRoleNum())[0];
            int start_devil = (start_land.GetRoleNum())[1];
            int end_priest = (end_land.GetRoleNum())[0];
            int end_devil = (end_land.GetRoleNum())[1];

            if (end_priest + end_devil == 6)     
                return 2;

            int[] boat_role_num = boat.GetRoleNum();
            if (boat.GetBoatSign() == true)         
            {
                start_priest += boat_role_num[0];
                start_devil += boat_role_num[1];
            }
            else                                  
            {
                end_priest += boat_role_num[0];
                end_devil += boat_role_num[1];
            }
            if (start_priest > 0 && start_priest < start_devil) 
            {      
                return 1;
            }
            if (end_priest > 0 && end_priest < end_devil)        
            {
                return 1;
            }
            return 0;                                             
        }
    }
}
