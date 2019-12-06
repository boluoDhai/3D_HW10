using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEditor.SceneManagement;

public class CreateGUI : MonoBehaviour
{
    private GameObject[] priests = new GameObject[3];
    private GameObject[] devils = new GameObject[3];
    public static Vector3[] vec = new Vector3[6];

    private float begin_len = 3.5f; // 东边海岸的object起始点
    private float space = 1.3f; // 每个物体的距离

    private GameObject east_coast;
    private GameObject west_coast;
    private GameObject water;

    private GUIStyle mstyle;

    private BoatController boat_controller = new BoatController();

    private float east_pos = 8.0f;
    private float west_pos = -8.0f;

    public static bool run = true;

    private string tips;
    private int step = 0;

    void initializePD() {
        for (int i = 0; i < 3; ++i) {
            vec[i].x = begin_len + i * space;
            vec[i].y = 1;
            vec[i].z = 0;
        }
        for (int i = 3; i < 6; ++i) {
            vec[i].x = begin_len + 2 * space + (i - 2) * space;
            vec[i].y = 1;
            vec[i].z = 0;
        }
        for (int i = 0; i < 3; ++i) {
            priests[i] = GameObject.Instantiate(Resources.Load("Prefabs/Priest", typeof(GameObject)), vec[i], Quaternion.identity, null) as GameObject;
            priests[i].name = "priest" + i.ToString();
            priests[i].AddComponent<SSActionManager>();
            devils[i] = GameObject.Instantiate(Resources.Load("Prefabs/Devil", typeof(GameObject)), vec[i + 3], Quaternion.identity, null) as GameObject;
            devils[i].name = "devils" + i.ToString();
            devils[i].AddComponent<SSActionManager>();
        }
    }

    void initializeCoast() {
        Vector3 east_vec, west_vec;
        east_vec.x = west_pos;
        east_vec.z = west_vec.z = 0;
        west_vec.x = east_pos;
        east_vec.y = west_vec.y = 0;
        east_coast = GameObject.Instantiate(Resources.Load("Prefabs/Coast", typeof(GameObject)), east_vec, Quaternion.identity, null) as GameObject;
        west_coast = GameObject.Instantiate(Resources.Load("Prefabs/Coast", typeof(GameObject)), west_vec, Quaternion.identity, null) as GameObject;
    }

    void initializeWater() {
        water = GameObject.Instantiate(Resources.Load("Prefabs/Water", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
    }

    // Start is called before the first frame update
    void Start(){
        initializeCoast();
        initializeWater();
        boat_controller.initializeBoat();
        initializePD();
        mstyle = new GUIStyle();
        mstyle.fontSize = 40;
        mstyle.alignment = TextAnchor.MiddleCenter;
        // DEBUG
        //InvokeRepeating("PRINT_DEBUG", 1, 1);
    }

    // Update is called once per frame
    void Update(){
        SSActionManager.Move();
    }

    void OnGUI() {
        if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 200, 100, 100), "Go!")) {
            if (BoatController.count >= 1) {
                BoatController.if_move = true;
                run = false;
                int count = BoatController.boat.transform.childCount;
                if (BoatController.direction_left) {
                    for (int i = 0; i < count; ++i) {
                        Click.decCount(BoatController.boat.transform.GetChild(i).name, 0);
                        Click.insCount(BoatController.boat.transform.GetChild(i).name, 1);
                    }
                } else {
                    for (int i = 0; i < count; ++i) {
                        Click.decCount(BoatController.boat.transform.GetChild(i).name, 1);
                        Click.insCount(BoatController.boat.transform.GetChild(i).name, 0);
                    }
                }
                ++step;
            }
            tips = "";
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 260, 40, 40), "Tips")) {
            if (Coast.east_devil_count == 3 && Coast.east_priest_count == 3) {
                tips = "Tips\nDevil: left -> 2\tright -> 1\nPriest: left -> 0\t right-> 3";
            } else if (Coast.east_devil_count == 1 && Coast.east_priest_count == 3 && step == 1) {
                tips = "Tips\nDevil: left -> 1\tright -> 2\nPriest: left -> 0\t right-> 3";
            } else if (Coast.east_devil_count == 2 && Coast.east_priest_count == 3) {
                tips = "Tips\nDevil: left -> 3\tright -> 0\nPriest: left -> 0\t right-> 3";
            } else if (Coast.east_devil_count == 0 && Coast.east_priest_count == 3) {
                tips = "Tips\nDevil: left -> 2\tright -> 1\nPriest: left -> 0\t right-> 3";
            } else if (Coast.east_devil_count == 1 && Coast.east_priest_count == 3 && step == 4) {
                tips = "Tips\nDevil: left -> 2\tright -> 1\nPriest: left -> 2\t right-> 1";
            } else if (Coast.east_devil_count == 1 && Coast.east_priest_count == 1) {
                tips = "Tips\nDevil: left -> 1\tright -> 2\nPriest: left -> 1\t right-> 2";
            } else if (Coast.east_devil_count == 2 && Coast.east_priest_count == 2) {
                tips = "Tips\nDevil: left -> 1\tright -> 2\nPriest: left -> 3\t right-> 0";
            } else if (Coast.east_devil_count == 2 && Coast.east_priest_count == 0 && step == 7) {
                tips = "Tips\nDevil: left -> 0\tright -> 3\nPriest: left -> 3\t right-> 0";
            } else if (Coast.east_devil_count == 3 && Coast.east_priest_count == 0) {
                tips = "Tips\nDevil: left -> 2\tright -> 1\nPriest: left -> 3\t right-> 0";
            } else if (Coast.east_devil_count == 1 && Coast.east_priest_count == 0) {
                tips = "Tips\nDevil: left -> 1\tright -> 2\nPriest: left -> 3\t right-> 0";
            } else if (Coast.east_devil_count == 2 && Coast.east_priest_count == 0) {
                tips = "Tips\nDevil: left -> 3\tright -> 0\nPriest: left -> 3\t right-> 0";
            } else {
                tips = "";
            }
            Debug.Log(step);
        }
        GUI.Label(new Rect(Screen.width / 5, Screen.height / 5, 200, 200), tips, mstyle);

        // success or lose or normal condition
        if (Judge.judgeEnd() == 2 || Judge.judgeEnd() == 1) { // lose
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 200, 200), "Failed, reload after 2s", mstyle);
            Invoke("reset", Time.time + 2);
        } else if (Judge.judgeEnd() == 0) { // success
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 200, 200), "Success! reload after 2s", mstyle);
            Invoke("reset", Time.time + 2);
        }
    }

    public void reset() {
        EditorSceneManager.LoadScene(0);
        Coast.reset();
        BoatController.reset();
    }

    public void PRINT_DEBUG() {
        Debug.Log("west: " + Coast.west_priest_count + ", " + Coast.west_devil_count + "    east: " + Coast.east_priest_count + ", " + Coast.east_devil_count);
    }
}

