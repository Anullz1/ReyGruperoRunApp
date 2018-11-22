using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM: MonoBehaviour {

    public static float vertVel = 0;
    public static int cointotal = 0;
    public static float timetotal = 0;
    public float waittoload = 0;

    public float zScenePos = 58;

    public static float zVelAdj = 1; //this a factor  and in the begginin dont have any effect.

    public static string lvlCompleStatus = "";

    public Transform OneStep;
    public Transform bbPitMid;
    public Transform coinObj;
    public Transform obs1obj;
    public Transform obs2obj;
    public Transform CapObj;



    public int randNum;

    // Use this for initialization
    void Start()
    {

        //for (int = 0; j >= 0; j++)
        //{

        //} 

    }

    // Update is called once per frame
    void Update()
    {

        if (zScenePos <= 2500)
        {
            Instantiate(OneStep, new Vector3(0.05f, 0.54f, zScenePos), OneStep.rotation);
            zScenePos += 14;
        }

            zScenePos += 4;


        }

    //    timetotal += Time.deltaTime;

    //   if (lvlCompleStatus == "fail")
    //  {
    //      waittoload += Time.deltaTime;
    //    }

    //    if (waittoload > 2)
    //    {
    //        //SceneManager.LoadScene("levelcomplete");
    //    }

    //}
}
