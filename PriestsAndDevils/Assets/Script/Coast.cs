using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coast{

    public static int east_priest_count = 3;
    public static int east_devil_count = 3;
    public static int west_priest_count = 0;
    public static int west_devil_count = 0;

    public static void reset() {
        east_priest_count = 3;
        east_devil_count = 3;
        west_priest_count = 0;
        west_devil_count = 0;
    }
}
