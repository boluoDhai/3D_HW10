using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController
{
    public static GameObject boat;
    public static float speed = 2.0f;
    public static bool direction_left = true;
    public static bool if_move = false;
    public static bool left = true;
    public static int count = 0;

    public static void reset() {
        direction_left = left = true;
        if_move = false;
        count = 0;
    }

    public void initializeBoat() {
        Vector3 boat_vec;
        boat_vec.x = 2;
        boat_vec.y = 0.2f;
        boat_vec.z = 0;
        boat = GameObject.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject)), boat_vec, Quaternion.identity, null) as GameObject;
        boat.name = "boat";
    }

    public int getCount() {
        return count;
    }
}
