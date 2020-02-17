using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinuumDoorSensor : RESTAPISensor
{
    [Header("Sensor Data")]
    // Generic data
    [SerializeField]
    new string name;

    [SerializeField]
    string description;

    [SerializeField]
    string lastChange;

    // Specific data
    [SerializeField]
    bool open;

    bool lastOpenState;

    [Header("Model")]
    [SerializeField]
    Transform door;

    [SerializeField]
    Vector3 closedAngle;

    [SerializeField]
    Vector3 openAngle;

    [SerializeField]
    float doorSpeed = 1;

    float doorProgress;

    [SerializeField]
    bool overrideSensor;

    [Header("GUI")]
    SensorGUI sensorGUI;

    [SerializeField]
    GameObject sensorGUIPrefab;

    Color openColor = new Color(75/255.0f, 250 / 255.0f, 75 / 255.0f);
    Color closedColor = new Color(200 / 255.0f, 75 / 255.0f, 75 / 255.0f);
    // Use this for initialization
    void Start()
    {
        GetInfo();
        GetLastState();

        if (sensorGUIPrefab)
        {
            GameObject guiObj = Instantiate(sensorGUIPrefab) as GameObject;
            guiObj.transform.SetParent(GameObject.Find("Canvas").transform);

            sensorGUI = guiObj.GetComponent<SensorGUI>();
            if (sensorGUI)
            {
                if (sensorGUI.doorStateButton)
                {
                    sensorGUI.doorStateButton.onClick.AddListener(ToggleOpen);
                }
                if (sensorGUI.overrideButton)
                {
                    sensorGUI.overrideButton.onClick.AddListener(ToggleOverride);
                }
                sensorGUI.GetComponent<WorldToCameraSpace>().worldPosition = door;
            }
        }
    }

	// Update is called once per frame
	void Update () {

        // Make sure sensor got data
        if (sensorLastState != null)
        {
            // Sensor info
            if (sensorInfo != null)
            {
                name = sensorInfo.c_name;
                description = sensorInfo.c_location + " " + sensorInfo.c_position;
            }

            // Data
            string date = sensorLastState.time.Substring(0, sensorLastState.time.IndexOf("T"));
            string time = sensorLastState.time.Substring(sensorLastState.time.IndexOf("T") + 1, sensorLastState.time.Length - sensorLastState.time.LastIndexOf(":"));
            time = GMTToLocalTime(time);
            lastChange = "Last Change:  " + date + " " + time;

            if (!overrideSensor)
                open = sensorLastState.value == 255 ? true : false;

            // GUI
            if (sensorGUI)
            {
                if (sensorGUI.nameText)
                {
                    sensorGUI.nameText.text = name;
                }
                if (sensorGUI.iconButton)
                {
                    sensorGUI.iconButton.GetComponentInChildren<Text>().text = name;
                    sensorGUI.iconButton.GetComponent<Image>().color = open ? openColor : closedColor;
                }
                if (sensorGUI.descriptionText)
                {
                    sensorGUI.descriptionText.text = description;
                }
                if (sensorGUI.lastChangeText)
                {
                    sensorGUI.lastChangeText.text = lastChange;
                }
                if (sensorGUI.doorStateButton)
                {
                    sensorGUI.doorStateButton.GetComponentInChildren<Text>().text = open ? "OPEN" : "CLOSED";
                    sensorGUI.doorStateButton.GetComponent<Image>().color = open ? openColor : closedColor;
                }
                if (sensorGUI.overrideButton)
                {
                    sensorGUI.overrideButton.GetComponentInChildren<Text>().text = overrideSensor ? "Manual" : "Live";
                }
            }
        }

        if (lastOpenState != open)
        {
            if (door)
            {
                // Lerp between last and new values
                if(open)
                {
                    doorProgress += Time.deltaTime * doorSpeed;
                    if (doorProgress > 1)
                        lastOpenState = open;
                }
                else
                {
                    doorProgress -= Time.deltaTime * doorSpeed;
                    if(doorProgress < 0)
                        lastOpenState = open;
                }
                door.localEulerAngles = Vector3.Lerp(closedAngle, openAngle, doorProgress);
            }
        }
    }

    public void ToggleOpen()
    {
        open = !open;
    }

    public void ToggleOverride()
    {
        overrideSensor = !overrideSensor;
    }
}
