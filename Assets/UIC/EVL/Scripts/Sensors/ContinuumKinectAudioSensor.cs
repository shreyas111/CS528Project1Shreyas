using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinuumKinectAudioSensor : MonoBehaviour {

    [SerializeField]
    protected string url = "http://thor.evl.uic.edu:3000/";

    [Header("APISensor")]
    [SerializeField]
    protected string sensorName = "";

    protected string dataType = "events";

    [SerializeField]
    protected SensorList sensorLastStateList;

    [SerializeField]
    bool updateOverTime = true;

    [SerializeField]
    float updateInterval = 5;

    [SerializeField]
    public bool debug;

    [Serializable]
    public class SensorList
    {
        public RESTAPISensor.SensorState[] sensors;
    }

    [Header("Kinect Audio")]
    Vector3[] recentAngles = new Vector3[10];
    Vector3[] recentAnglesData = new Vector3[10];
    LineRenderer[] recentLines = new LineRenderer[10];

    int currentIndex;

    [SerializeField]
    float lineMaxLength = 10;

    [SerializeField]
    Material audioLineMaterial;

    [Header("GUI")]
    SensorGUI sensorGUI;

    [SerializeField]
    GameObject sensorGUIPrefab;

    string lastUpdateTime;

    // Use this for initialization
    void Start () {
        GetLastStateList("audio");

        if (sensorGUIPrefab)
        {
            GameObject guiObj = Instantiate(sensorGUIPrefab) as GameObject;
            guiObj.transform.SetParent(GameObject.Find("Canvas").transform);

            sensorGUI = guiObj.GetComponent<SensorGUI>();
            if (sensorGUI)
            {

                sensorGUI.GetComponent<WorldToCameraSpace>().worldPosition = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < recentAngles.Length; i++)
        {
            float time = (recentAnglesData[i].z + 5.0f / Time.time);
            time = Mathf.Clamp(time, 0, 1);

            if (recentLines[i] == null)
            {
                GameObject lineObj = new GameObject("AudioLine");
                lineObj.transform.parent = transform;
                lineObj.transform.localPosition = Vector3.zero;
                lineObj.transform.localRotation = Quaternion.identity;
                recentLines[i] = lineObj.AddComponent<LineRenderer>();
                recentLines[i].sharedMaterial = audioLineMaterial;
                recentLines[i].positionCount = 2;
                recentLines[i].startWidth = 0.01f;
                recentLines[i].endWidth = 0.01f;
                recentLines[i].SetPosition(0, transform.position);
                recentLines[i].SetPosition(1, transform.position + transform.localRotation * recentAngles[i]);

                float energy = (recentAnglesData[currentIndex].x + 70) / 200.0f;
                recentLines[i].startColor = Color.Lerp(Color.black, Color.green, 1);
                recentLines[i].endColor = Color.Lerp(Color.black, Color.green, 1);
            }
            else
            {
                float energy = (recentAnglesData[currentIndex].x + 70) / 200.0f;
                recentLines[i].sharedMaterial = audioLineMaterial;
                recentLines[i].startColor = Color.Lerp(Color.black, Color.green, time);
                recentLines[i].endColor = Color.Lerp(Color.black, Color.green, time);

                recentLines[i].SetPosition(0, transform.position);
                recentLines[i].SetPosition(1, transform.position + transform.localRotation * recentAngles[i]);
            }
            //Debug.DrawLine(transform.position, transform.position + transform.localRotation * recentAngles[i], Color.magenta, 5);
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
            if (sensorGUI.descriptionText && sensorLastStateList.sensors.Length >= 3)
            {
                string date = sensorLastStateList.sensors[0].time.Substring(0, sensorLastStateList.sensors[0].time.IndexOf("T"));
                string time = sensorLastStateList.sensors[0].time.Substring(sensorLastStateList.sensors[0].time.IndexOf("T") + 1, sensorLastStateList.sensors[0].time.Length - sensorLastStateList.sensors[0].time.LastIndexOf(":"));

                time = RESTAPISensor.GMTToLocalTime(time);
                
                string lastChange = "Last Change:  " + date + " " + time;

                sensorGUI.descriptionText.text = sensorLastStateList.sensors[0].sensor + "\n";
                sensorGUI.descriptionText.text += lastChange + "\n";
                sensorGUI.descriptionText.text += sensorLastStateList.sensors[0].label + ": " + sensorLastStateList.sensors[0].value.ToString("F2") + " " + sensorLastStateList.sensors[0].units + "\n";
                sensorGUI.descriptionText.text += sensorLastStateList.sensors[1].label + ": " + sensorLastStateList.sensors[1].value.ToString("F2") + " " + sensorLastStateList.sensors[1].units + "\n";
                sensorGUI.descriptionText.text += sensorLastStateList.sensors[2].label + ": " + sensorLastStateList.sensors[2].value.ToString("F2") + " " + sensorLastStateList.sensors[2].units + "\n";
            }
            if (sensorGUI.lastChangeText && sensorLastStateList.sensors.Length >= 3)
            {
                sensorGUI.lastChangeText.text = sensorLastStateList.sensors[0].time;
            }
        }
    }

    protected void GetLastStateList(string dataType = "events")
    {
        this.dataType = dataType;
        StartCoroutine("GetLastStateListCR", url);
    }

    private IEnumerator GetLastStateListCR(string url)
    {
        bool runLoop = true;
        while (runLoop)
        {
            if (debug)
                Debug.Log(url + "/" + dataType + "/" + sensorName + "/last");
            WWW www = new WWW(url + "/" + dataType + "/" + sensorName + "/last");
            yield return www;
            if (www.error == null)
            {
                string text = "{ \"sensors\": " + www.text + "}";
                if (debug)
                {
                    Debug.Log(text);
                }

                sensorLastStateList = JsonUtility.FromJson<SensorList>(text);

                if (sensorLastStateList.sensors.Length >= 3)
                {
                    if (lastUpdateTime != sensorLastStateList.sensors[0].time)
                    {
                        float audioEnergy = sensorLastStateList.sensors[0].value;
                        float audioAngle = sensorLastStateList.sensors[1].value;
                        float angleConfidence = sensorLastStateList.sensors[2].value  / 100.0f;

                        Vector3 angleVector = Vector3.zero;
                        angleVector.x = lineMaxLength * angleConfidence * Mathf.Sin(-audioAngle * Mathf.Deg2Rad);
                        angleVector.z = lineMaxLength * angleConfidence * Mathf.Cos(-audioAngle * Mathf.Deg2Rad);

                        recentAnglesData[currentIndex] = new Vector3(audioEnergy, angleConfidence, Time.time);
                        recentAngles[currentIndex] = angleVector;
                        currentIndex++;
                        if (currentIndex > recentAngles.Length - 1)
                        {
                            currentIndex = 0;
                        }
                        lastUpdateTime = sensorLastStateList.sensors[0].time;
                    }
                }
            }
            else
            {
                Debug.Log("ERROR: " + www.error);
            }
            if (updateOverTime)
            {
                yield return new WaitForSeconds(updateInterval);
            }
            else
            {
                runLoop = false;
            }
        }
    }
}
