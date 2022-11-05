// Reference:
//    - https://stackoverflow.com/questions/67291497/how-to-keep-track-of-the-damage-taken

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public void OnTriggerEnter(Collider other){
        Player player = other.gameObject.GetComponent<Player>();
        player.TakeDamage(Random.Range(5, 15));
    }
}
