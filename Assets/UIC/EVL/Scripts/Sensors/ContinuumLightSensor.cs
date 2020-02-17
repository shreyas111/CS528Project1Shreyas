using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinuumLightSensor : RESTAPISensor
{
    [Header("Sensor Data")]
    // Generic data
    [SerializeField]
    new string name;

    [SerializeField]
    string description;

    [SerializeField]
    string lastChange;

    // Specfic data
    [SerializeField]
    float value;

    float lastValue;

    [SerializeField]
    string units;

    [Header("Model")]
    [SerializeField]
    Transform lightObjectRoot;

    [SerializeField]
    Vector2 luxToIntensity = new Vector2(40, 1);

    [SerializeField]
    Transform[] lightModelRoot;

    [SerializeField]
    Color lightColor = Color.white;

    Light[] lights;
    ArrayList lightMaterials = new ArrayList();

    [SerializeField]
    float transitionSpeed = 1;

    float transitionProgress;

    [SerializeField]
    bool overrideSensor;

    [Header("GUI")]
    SensorGUI sensorGUI;

    [SerializeField]
    GameObject sensorGUIPrefab;

    // Use this for initialization
    void Start () {
        GetInfo();
        GetLastState("luminance");

        // Setup / Initial State
        if(lightObjectRoot)
            lights = lightObjectRoot.GetComponentsInChildren<Light>();
        foreach(Transform t in lightModelRoot)
        {
            MeshRenderer[] lightRenderers = t.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in lightRenderers)
            {
                foreach (Material m in r.materials)
                {
                    Color matColor = m.GetColor("_EmissionColor");
                    if (matColor != Color.black)
                    {
                        //lightColor = matColor;
                        lightMaterials.Add(m);
                    }
                }
            }
        }
        lastValue = value;

        // GUI
        GameObject guiObj = Instantiate(sensorGUIPrefab) as GameObject;
        guiObj.transform.SetParent(GameObject.Find("Canvas").transform);

        sensorGUI = guiObj.GetComponent<SensorGUI>();
        if (sensorGUI)
        {
            if (sensorGUI.overrideButton)
            {
                sensorGUI.overrideButton.onClick.AddListener(ToggleOverride);
            }
            if (sensorGUI.inputField)
            {
                sensorGUI.inputField.onValueChanged.AddListener(UpdateValue);
            }
            sensorGUI.GetComponent<WorldToCameraSpace>().worldPosition = lightObjectRoot;
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Make sure sensor got data
        if (sensorInfo != null && sensorLastState != null)
        {
            // Sensor info
            name = sensorInfo.c_name;
            description = sensorInfo.c_location + " " + sensorInfo.c_position;

            // Data
            string date = sensorLastState.time.Substring(0, sensorLastState.time.IndexOf("T"));
            string time = sensorLastState.time.Substring(sensorLastState.time.IndexOf("T") + 1, sensorLastState.time.Length - sensorLastState.time.LastIndexOf(":"));
            time = GMTToLocalTime(time);
            lastChange = "Last Change:  " + date + " " + time;

            units = sensorLastState.units;

            if(!overrideSensor)
                value = sensorLastState.value;

            if (lastValue != value)
            {
                // Lerp between last and new values
                transitionProgress += Time.deltaTime * transitionSpeed;
                float newValue = Mathf.Lerp(lastValue, value, transitionProgress);

                float intensity = (newValue / (float)luxToIntensity.x) * (float)luxToIntensity.y;

                if (lights != null)
                {
                    foreach (Light l in lights)
                    {
                        l.intensity = intensity;
                    }
                }

                foreach (Material m in lightMaterials)
                {
                    m.SetColor("_EmissionColor", lightColor * intensity);
                }

                if (transitionProgress > 1)
                {
                    transitionProgress = 0;
                    lastValue = value;
                }
            }

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
                }
                if (sensorGUI.descriptionText)
                {
                    sensorGUI.descriptionText.text = description;
                }
                if (sensorGUI.lastChangeText)
                {
                    sensorGUI.lastChangeText.text = lastChange;
                }
                if (sensorGUI.inputField)
                {
                    sensorGUI.inputField.text = value.ToString();
                }
                if (sensorGUI.unitsText)
                {
                    sensorGUI.unitsText.text = units;
                }
                if (sensorGUI.overrideButton)
                {
                    sensorGUI.overrideButton.GetComponentInChildren<Text>().text = overrideSensor ? "Manual" : "Live";
                }
            }
        }
    }

    public void ToggleOverride()
    {
        overrideSensor = !overrideSensor;
    }

    public void UpdateValue(string newValue)
    {
        float.TryParse(newValue, out value);
    }
}
