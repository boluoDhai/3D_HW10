using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour{
    public static void insCount(string str, int choice) {
        if (choice == 0) {
            if (str[0] == 'p') ++Coast.east_priest_count;
            else ++Coast.east_devil_count;
        } else {
            if (str[0] == 'p') ++Coast.west_priest_count;
            else ++Coast.west_devil_count;
        }
    }

    public static void decCount(string str, int choice) {
        if (choice == 0) {
            if (str[0] == 'p') --Coast.east_priest_count;
            else --Coast.east_devil_count;
        } else {
            if (str[0] == 'p') --Coast.west_priest_count;
            else --Coast.west_devil_count;
        }
    }
}
