using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuumStatus : MonoBehaviour {

    protected string url = "https://iot.evl.uic.edu:4000/api/";

    [SerializeField]
    bool updateOverTime = true;

    [SerializeField]
    float updateInterval = 5;

    [SerializeField]
    bool debug;

    RESTAPISensor.RoomStatus roomSensors;

    // Build all sensors as a single list
    // Mostly for editor viewing
    [SerializeField]
    RESTAPISensor.SensorState[] sensors;

    bool hasData;

    // Use this for initialization
    void Start () {
        roomSensors = new RESTAPISensor.RoomStatus();
        roomSensors.sensors = new Dictionary<string, RESTAPISensor.SensorState>();
        GetRoomStatus();
    }

    public RESTAPISensor.SensorState GetSensorState(string sensorName)
    {
        if (hasData)
        {
            return roomSensors.sensors[sensorName];
        }
        else
            return null;
    }

    protected void GetRoomStatus()
    {
        StartCoroutine("GetRoomStatusCR", url);
    }

    IEnumerator GetRoomStatusCR(string url)
    {
        if (debug)
            Debug.Log(url + "/status");
        WWW www = new WWW(url + "/status");
        yield return www;
        if (www.error == null)
        {
            //roomSensors = JsonUtility.FromJson<RESTAPISensor.RoomStatus>(www.text);

            IDictionary roomSensorDict = (IDictionary)MiniJSON.Json.Deserialize(www.text);

            if (debug)
            {
                Debug.Log(www.text);
            }


            ArrayList sensorList = new ArrayList();
            foreach (string key in roomSensorDict.Keys)
            {
                RESTAPISensor.SensorState sensor = DictionaryToSensorState(key, roomSensorDict);
                roomSensors.sensors[key] = sensor;

                if (!sensor.isMultiSensor)
                {
                    sensorList.Add(sensor);
                }
                else
                {
                    foreach (RESTAPISensor.SensorState multiSensor in sensor.multiSensor.Values)
                    {
                        multiSensor.name = sensor.name + "-" + multiSensor.label;
                        sensorList.Add(multiSensor);
                    }
                }

                hasData = true;

                if (debug)
                {
                    Debug.Log("key: " + key);
                    if (!sensor.isMultiSensor)
                    {
                        Debug.Log("   " + sensor.time);
                        Debug.Log("   " + sensor.c_id);
                        Debug.Log("   " + sensor.label);
                        Debug.Log("   " + sensor.name);
                        Debug.Log("   " + sensor.sensor);
                        Debug.Log("   " + sensor.units);
                        Debug.Log("   " + sensor.value);
                    }
                    else
                    {
                        foreach (RESTAPISensor.SensorState multiSensor in sensor.multiSensor.Values)
                        {
                            Debug.Log("   " + multiSensor.time);
                            Debug.Log("   " + multiSensor.c_id);
                            Debug.Log("   " + multiSensor.label);
                            Debug.Log("   " + multiSensor.name);
                            Debug.Log("   " + multiSensor.sensor);
                            Debug.Log("   " + multiSensor.units);
                            Debug.Log("   " + multiSensor.value);
                        }
                    }
                }
            }

            sensors = new RESTAPISensor.SensorState[sensorList.Count];
            for(int i = 0; i < sensorList.Count; i++)
            {
                sensors[i] = (RESTAPISensor.SensorState)sensorList[i];
            }
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
    }

    RESTAPISensor.SensorState DictionaryToSensorState(string key, IDictionary dictionary)
    {
        RESTAPISensor.SensorState sensor = new RESTAPISensor.SensorState();
        IDictionary sensorInfo = dictionary[key] as IDictionary;

        if (GetSensorAttribute("time", sensorInfo, out sensor.time))
        {

        }
        else
        {
            sensor.isMultiSensor = true;
            sensor.multiSensor = new Dictionary<string, RESTAPISensor.SensorState>();
            foreach (string key2 in sensorInfo.Keys)
            {
                sensor.multiSensor[key2] = DictionaryToSensorState(key2, sensorInfo);
            }
            sensor.name = key;
            return sensor;
        }

        string id_str = "";
        if (GetSensorAttribute("c_id", sensorInfo, out id_str))
        {
            
        }

        if (GetSensorAttribute("label", sensorInfo, out sensor.label))
        {

        }

        if (GetSensorAttribute("name", sensorInfo, out sensor.name))
        {

        }

        if (GetSensorAttribute("sensor", sensorInfo, out sensor.sensor))
        {

        }

        if (GetSensorAttribute("units", sensorInfo, out sensor.units))
        {

        }

        string value_str = "";
        if (GetSensorAttribute("value", sensorInfo, out value_str))
        {
            float.TryParse(value_str, out sensor.value);
        }

        return sensor;
    }

    bool GetSensorAttribute(string attribute, IDictionary dict, out string result)
    {
        if (dict[attribute] != null)
        {
            result = dict[attribute].ToString();
            return true;
        }
        else
        {
            result = "";
            return false;
        }
    }
}
