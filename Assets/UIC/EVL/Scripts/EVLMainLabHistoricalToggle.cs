using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EVLMainLabHistoricalToggle : MonoBehaviour {

    public enum ObjectGroup { MainLab2018, MainLab2017, MainLab2016, MainLab2009 };

    [SerializeField]
    public ObjectGroup currentView = ObjectGroup.MainLab2018;

    ObjectGroup lastView = ObjectGroup.MainLab2018;

    [SerializeField]
    GameObject[] group1Objects;

    [SerializeField]
    GameObject[] group2Objects;

    [SerializeField]
    GameObject[] group3Objects;

    [SerializeField]
    GameObject[] group4Objects;

    private void Start()
    {
        SetView();
    }
    // Update is called once per frame
    void Update()
    {
        if (currentView != lastView)
            SetView();
    }

    void ClearAll()
    {
        foreach (GameObject g in group1Objects)
            g.SetActive(false);
        foreach (GameObject g in group2Objects)
            g.SetActive(false);
        foreach (GameObject g in group3Objects)
            g.SetActive(false);
        foreach (GameObject g in group4Objects)
            g.SetActive(false);
    }

    void SetView()
    {
        ClearAll();

        switch (currentView)
        {
            case ObjectGroup.MainLab2018:
                foreach (GameObject g in group1Objects)
                    g.SetActive(true);
                break;
            case ObjectGroup.MainLab2017:
                foreach (GameObject g in group2Objects)
                    g.SetActive(true);
                break;
            case ObjectGroup.MainLab2016:
                foreach (GameObject g in group3Objects)
                    g.SetActive(true);
                break;
            case ObjectGroup.MainLab2009:
                foreach (GameObject g in group4Objects)
                    g.SetActive(true);
                break;
        }
        lastView = currentView;
    }


}
