using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 5f;
    public float range = 100f;
    public AudioSource audioSource;
    public AudioClip sfx1;

    public Camera fpsCam;

    // Update is called once per frame
    void Update()
    {
        // conferir sobre o Fire1
        if (Input.GetButtonDown("Fire1")){
            Shoot();
            audioSource.clip = sfx1;
            audioSource.Play();
        }
    }

    void Shoot(){
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            TrollCubeTarget trollCube = hit.transform.GetComponent<TrollCubeTarget>();
            GoalCubeTarget goalCube = hit.transform.GetComponent<GoalCubeTarget>();
            if(target != null){
                target.TakeDamage(damage);
            }
            if(trollCube != null){
                trollCube.TakeDamage(damage);
            }
            if(goalCube != null){
                goalCube.TakeDamage(damage);
            }
        };
    }
}
