using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge
{
    public static int judgeEnd() {
        if (Coast.east_priest_count != 0 && Coast.east_priest_count < Coast.east_devil_count)
            return 1; // 东边失败
        else if (Coast.west_priest_count != 0 && Coast.west_priest_count < Coast.west_devil_count)
            return 2; // 西边失败
        else if (Coast.west_priest_count == 3 && Coast.west_devil_count == 3)
            return 0; // 成功
        return 3; // 普通情况
    }
}
