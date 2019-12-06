using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour
{
    public static void Move() {
        if (BoatController.if_move) {
            if (BoatController.direction_left) {
                if (BoatController.boat.transform.position.x > -2) {
                    BoatController.boat.transform.Translate(new Vector3(-1, 0, 0) * BoatController.speed * Time.deltaTime, Space.World);
                } else {
                    BoatController.if_move = BoatController.direction_left = false;
                    CreateGUI.run = true;
                }
            } else {
                if (BoatController.boat.transform.position.x < 2) {
                    BoatController.boat.transform.Translate(new Vector3(1, 0, 0) * BoatController.speed * Time.deltaTime, Space.World);
                } else {
                    BoatController.if_move = false;
                    BoatController.direction_left = true;
                    CreateGUI.run = true;
                }
            }
        }
    }

    void OnMouseDown() {
        if (!CreateGUI.run)
            return;
        //在boat上面
        if (this.gameObject.transform.position.y <= BoatController.boat.transform.position.y + 0.61f) {
            string str = this.gameObject.name;
            int pre_index = (str[0] == 'd' ? 3 : 0);
            int index = pre_index + str[str.Length - 1] - '0';
            if (this.gameObject.transform.position.x < BoatController.boat.transform.position.x)
                BoatController.left = true;
            if (BoatController.direction_left) {
                this.gameObject.transform.position = CreateGUI.vec[index];
            } else {
                this.gameObject.transform.position = new Vector3(-CreateGUI.vec[index].x, CreateGUI.vec[index].y, CreateGUI.vec[index].z);
            }
            --BoatController.count;
            this.gameObject.transform.parent = null;
        } else { // 不在boat上面
            if (BoatController.count < 2) {
                if (BoatController.left) { // 左边有空位
                    this.gameObject.transform.position = new Vector3(BoatController.boat.transform.position.x - 0.6f, BoatController.boat.transform.position.y + 0.6f, BoatController.boat.transform.position.z);
                    BoatController.left = false;
                } else {
                    this.gameObject.transform.position = new Vector3(BoatController.boat.transform.position.x + 0.6f, BoatController.boat.transform.position.y + 0.6f, BoatController.boat.transform.position.z);
                }
                ++BoatController.count;
                this.gameObject.transform.parent = BoatController.boat.transform;
            }
        }
    }
}
