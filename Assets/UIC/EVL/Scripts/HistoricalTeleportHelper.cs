using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoricalTeleportHelper : MonoBehaviour {

    EVLMainLabHistoricalToggle mainLabToggle;
    EVL2068HistoricalToggle classroomToggle;

    public void Start()
    {
        mainLabToggle = GetComponentInParent<EVLMainLabHistoricalToggle>();
        classroomToggle = GetComponentInParent<EVL2068HistoricalToggle>();
    }

    public void Teleport(string sceneName)
    {
        if(mainLabToggle)
        {
            if (sceneName == "2018")
            {
                mainLabToggle.currentView = EVLMainLabHistoricalToggle.ObjectGroup.MainLab2018;
            }
            else if (sceneName == "2017")
            {
                mainLabToggle.currentView = EVLMainLabHistoricalToggle.ObjectGroup.MainLab2017;
            }
            else if (sceneName == "2016")
            {
                mainLabToggle.currentView = EVLMainLabHistoricalToggle.ObjectGroup.MainLab2016;
            }
            else if (sceneName == "2009")
            {
                mainLabToggle.currentView = EVLMainLabHistoricalToggle.ObjectGroup.MainLab2009;
            }
        }
        else if(classroomToggle)
        {
            if (sceneName == "2019")
            {
                classroomToggle.currentView = EVL2068HistoricalToggle.EVL2068Mode.Continuum;
            }
            else if (sceneName == "2018")
            {
                classroomToggle.currentView = EVL2068HistoricalToggle.EVL2068Mode.Continuum2018;
            }
            else if (sceneName == "2017")
            {
                classroomToggle.currentView = EVL2068HistoricalToggle.EVL2068Mode.CyberCommons2017;
            }
            else if (sceneName == "2010")
            {
                classroomToggle.currentView = EVL2068HistoricalToggle.EVL2068Mode.CyberCommons2010;
            }
        }
    }
}
