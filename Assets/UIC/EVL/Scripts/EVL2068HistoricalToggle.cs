using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EVL2068HistoricalToggle : MonoBehaviour {

    public enum EVL2068Mode { Classroom2009, CyberCommons2010, CyberCommons2017, Continuum2018, Continuum };

    [SerializeField]
    public EVL2068Mode currentView = EVL2068Mode.Continuum;

    EVL2068Mode lastView = EVL2068Mode.Continuum;

    [SerializeField]
    GameObject[] cyberCommons2010;

    [SerializeField]
    GameObject[] cyberCommons2017;

    [SerializeField]
    GameObject[] continuum2018;

    [SerializeField]
    GameObject[] continuum;

    private void Start()
    {
        SetView();
    }
    // Update is called once per frame
    void Update () {
        if (currentView != lastView)
            SetView();
	}

    void SetView()
    {
        foreach (GameObject g in cyberCommons2010)
            g.SetActive(false);
        foreach (GameObject g in cyberCommons2017)
            g.SetActive(false);
        foreach (GameObject g in continuum2018)
            g.SetActive(false);
        foreach (GameObject g in continuum)
            g.SetActive(false);

        switch (currentView)
        {
            case EVL2068Mode.CyberCommons2010:
                foreach (GameObject g in cyberCommons2010)
                    g.SetActive(true);
                break;
            case EVL2068Mode.CyberCommons2017:
                foreach (GameObject g in cyberCommons2017)
                    g.SetActive(true);
                break;
            case EVL2068Mode.Continuum2018:
                foreach (GameObject g in continuum2018)
                    g.SetActive(true);
                break;
            case EVL2068Mode.Continuum:
                foreach (GameObject g in continuum)
                    g.SetActive(true);
                break;
        }
        lastView = currentView;
    }
}
