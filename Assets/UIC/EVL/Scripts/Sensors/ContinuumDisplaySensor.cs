using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ContinuumDisplaySensor : RESTAPISensor {

    [Serializable]
    public class DisplayWallInfo
    {
        public string lastcommand;
        public string network_gateway;
        public string network_internalip;
        public string network_ipaddress;
        public string network_macaddress;
        public int network_usedhcp;
        public int num_panels;
        public int num_poweroutlets;
        public int num_powerrectifiers;
        public int num_presets;
        public int num_videocontrollers;
        public string powersupply_model;
        public float powersupply_temperature;
    }

    [Serializable]
    public class PanelState
    {
        public string name;
        public string panel;
        public string position;
        public float temperature;
        public float watts;
    }

    [Serializable]
    public class PowerOutletState
    {
        public string name;
        public float current;
    }

    [Serializable]
    public class RectifierState
    {
        public string name;
        public string status;
        public float current;
        public float temperature;
    }

    // Use this for initialization
    void Start() {
        //GetInfo(DeviceType.Display);
        GetDisplayStatus();

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

    [Header("GUI")]
    SensorGUI sensorGUI;

    [SerializeField]
    GameObject sensorGUIPrefab;

    [Header("Display Info")]
    // Sensor Info
    public string time;
    public string c_device;
    public int c_id;
    public string c_location;
    public string c_name;
    public string c_position;
    public string manufactuererid;
    public string manufacturer;
    public string manufacturerid;
    public string product;
    public string productid;
    public string producttype;
    public bool ready;
    public string sensor;
    public string type;

    // Displays
    public string lastcommand;
    public string network_gateway;
    public string network_internalip;
    public string network_ipaddress;
    public string network_macaddress;
    public int network_usedhcp;
    public int num_panels;
    public int num_poweroutlets;
    public int num_powerrectifiers;
    public int num_presets;
    public int num_videocontrollers;

    [SerializeField]
    PanelState[] displays;

    [SerializeField]
    PowerOutletState[] outlets;

    [SerializeField]
    RectifierState[] rectifiers;

    //"panel1":"LX55X","panel10":"LX55X","panel10_position":"2x4","panel10_temperature":40,"panel10_watts":52.76,"panel11":"LX55X","panel11_position":"2x5","panel11_temperature":40,"panel11_watts":52.65,"panel12":"LX55X","panel12_position":"2x6","panel12_temperature":40,"panel12_watts":55.86,"panel13":"LX55X","panel13_position":"3x1","panel13_temperature":39,"panel13_watts":51.99,"panel14":"LX55X","panel14_position":"3x2","panel14_temperature":39,"panel14_watts":53.85,"panel15":"LX55X","panel15_position":"3x3","panel15_temperature":39,"panel15_watts":49.58,"panel16":"LX55X","panel16_position":"3x4","panel16_temperature":39,"panel16_watts":49.82,"panel17":"LX55X","panel17_position":"3x5","panel17_temperature":39,"panel17_watts":51.43,"panel18":"LX55X","panel18_position":"3x6","panel18_temperature":39,"panel18_watts":53.43,"panel1_position":"1x1","panel1_temperature":40,"panel1_watts":52.62,"panel2":"LX55X","panel2_position":"1x2","panel2_temperature":41,"panel2_watts":52.66,"panel3":"LX55X","panel3_position":"1x3","panel3_temperature":41,"panel3_watts":48.11,"panel4":"LX55X","panel4_position":"1x4","panel4_temperature":42,"panel4_watts":55.24,"panel5":"LX55X","panel5_position":"1x5","panel5_temperature":41,"panel5_watts":55.92,"panel6":"LX55X","panel6_position":"1x6","panel6_temperature":39,"panel6_watts":53.61,"panel7":"LX55X","panel7_position":"2x1","panel7_temperature":40,"panel7_watts":53.18,"panel8":"LX55X","panel8_position":"2x2","panel8_temperature":40,"panel8_watts":51.42,"panel9":"LX55X","panel9_position":"2x3","panel9_temperature":41,"panel9_watts":47.38,"poweroutlets1_current":4.743055555555555,"poweroutlets2_current":6.25,"poweroutlets3_current":6.131944444444445,"powerrectifiers1":"OK","powerrectifiers1_current":8.10647040979566,"powerrectifiers1_temperature":36.99996209134501,"powerrectifiers2":"OK","powerrectifiers2_current":9.314707730744798,"powerrectifiers2_temperature":38.99999999999994,"powerrectifiers3":"OK","powerrectifiers3_current":7.724408132474145,"powerrectifiers3_temperature":37.00033417437051,"powerrectifiers4":"No AC","powerrectifiers4_current":0,"powerrectifiers4_temperature":21.999999999999996,"powersupply_model":"RPS110-3","powersupply_temperature":37.611346,"preset1":"SAGE2","preset10":"SAGE+Laptop","preset2":"Laptop","preset3":"Movie","preset4":"Laptop - Duplicate","preset5":"SAGE+LaptopRight","preset6":"SAGE+LaptopLeft","preset9":"SAGE2_old","product":"","productid":"","producttype":"","ready":true,"sensor":"MainWall","system_brightness":30,"system_columns":6,"system_height":3240,"system_orientation":"LANDSCAPE","system_panel_model":"MX55X","system_preset":9,"system_rows":3,"system_state":"ON","system_width":11520,"type":"display","videocontroller1_exhaust_temperature":42.28125,"videocontroller1_fanstatus":"OK","videocontroller1_fpga_temperature":74.75,"videocontroller1_intake_temperature":36.926187,"videocontroller2_exhaust_temperature":41.78125,"videocontroller2_fanstatus":"OK","videocontroller2_fpga_temperature":74.80125,"videocontroller2_intake_temperature":37.309063,"videocontroller3_exhaust_temperature":41.21875,"videocontroller3_fanstatus":"OK","videocontroller3_fpga_temperature":71.715187,"videocontroller3_intake_temperature":36.676187,"videocontroller4_exhaust_temperature":40.75,"videocontroller4_fanstatus":"OK","videocontroller4_fpga_temperature":74.938,"videocontroller4_intake_temperature":36.863375,"videocontroller5_exhaust_temperature":41.28125,"videocontroller5_fanstatus":"OK","videocontroller5_fpga_temperature":71.6915,"videocontroller5_intake_temperature":35.684063}

    protected void GetDisplayStatus()
    {
        StartCoroutine("GetDisplayStatusCR", url);
    }

    IEnumerator GetDisplayStatusCR(string url)
    {
        if (debug)
            Debug.Log(url + "/displays/" + sensorName);
        WWW www = new WWW(url + "/displays/" + sensorName);
        yield return www;
        if (www.error == null)
        {
            sensorInfo = JsonUtility.FromJson<SensorInfo>(www.text);
            DisplayWallInfo wallInfo = JsonUtility.FromJson<DisplayWallInfo>(www.text);

            time = sensorInfo.time;
            c_device = sensorInfo.c_device;
            c_id = sensorInfo.c_id;
            c_location = sensorInfo.c_location;
            c_name = sensorInfo.c_name;
            c_position = sensorInfo.c_position;
            manufactuererid = sensorInfo.manufactuererid;
            manufacturer = sensorInfo.manufacturer;
            manufacturerid = sensorInfo.manufacturerid;
            product = sensorInfo.product;
            productid = sensorInfo.productid;
            producttype = sensorInfo.producttype;
            ready = sensorInfo.ready;
            sensor = sensorInfo.sensor;
            type = sensorInfo.type;

            lastcommand = wallInfo.lastcommand;
            network_gateway = wallInfo.network_gateway;
            network_internalip = wallInfo.network_internalip;
            network_ipaddress = wallInfo.network_ipaddress;
            network_macaddress = wallInfo.network_macaddress;
            network_usedhcp = wallInfo.network_usedhcp;
            num_panels = wallInfo.num_panels;
            num_poweroutlets = wallInfo.num_poweroutlets;
            num_powerrectifiers = wallInfo.num_powerrectifiers;
            num_presets = wallInfo.num_presets;
            num_videocontrollers = wallInfo.num_videocontrollers;

            IDictionary roomSensorDict = (IDictionary)MiniJSON.Json.Deserialize(www.text);

            float avgDisplayTemp = 0;
            float avgDisplayWatts = 0;
            displays = new PanelState[num_panels];
            for (int i = 1; i < num_panels + 1; i++)
            {
                PanelState display = new PanelState();
                display.name = "panel " + i;
                display.panel = (string)roomSensorDict["panel" + i];
                display.position = (string)roomSensorDict["panel" + i + "_position"];

                float.TryParse(roomSensorDict["panel" + i + "_temperature"].ToString(), out display.temperature);
                float.TryParse(roomSensorDict["panel" + i + "_watts"].ToString(), out display.watts);

                avgDisplayTemp += display.temperature;
                avgDisplayWatts += display.watts;
                displays[i - 1] = display;
            }
            avgDisplayTemp /= num_panels;
            avgDisplayWatts /= num_panels;

            outlets = new PowerOutletState[num_poweroutlets];
            for (int i = 1; i < num_poweroutlets + 1; i++)
            {
                PowerOutletState outlet = new PowerOutletState();
                outlet.name = "poweroutlets " + i;

                float.TryParse(roomSensorDict["poweroutlets" + i + "_current"].ToString(), out outlet.current);

                outlets[i - 1] = outlet;
            }

            rectifiers = new RectifierState[num_powerrectifiers];
            for (int i = 1; i < num_powerrectifiers + 1; i++)
            {
                RectifierState rectifier = new RectifierState();
                rectifier.name = "powerrectifiers " + i;
                rectifier.status = roomSensorDict["powerrectifiers" + i].ToString();

                float.TryParse(roomSensorDict["powerrectifiers" + i + "_current"].ToString(), out rectifier.current);
                float.TryParse(roomSensorDict["powerrectifiers" + i + "_temperature"].ToString(), out rectifier.temperature);

                rectifiers[i - 1] = rectifier;
            }

            // GUI
            if (sensorGUI)
            {
                if (sensorGUI.nameText)
                {
                    sensorGUI.nameText.text = sensorName;
                }
                if (sensorGUI.iconButton)
                {
                    sensorGUI.iconButton.GetComponentInChildren<Text>().text = sensorName;
                }
                if (sensorGUI.descriptionText)
                {
                    sensorGUI.descriptionText.text = c_location + " " + c_position + "\n";
                    sensorGUI.descriptionText.text += "Panels: " + num_panels + "\n";
                    sensorGUI.descriptionText.text += "Power Outlets: " + num_poweroutlets + "\n";
                    sensorGUI.descriptionText.text += "Power Rectifiers: " + num_powerrectifiers + "\n";
                    sensorGUI.descriptionText.text += "Video Controllers: " + num_videocontrollers + "\n";
                    sensorGUI.descriptionText.text += "Presets: " + num_presets + "\n";
                    sensorGUI.descriptionText.text += "Avg Panel Temp: " + avgDisplayTemp + "\n";
                    sensorGUI.descriptionText.text += "Avg Panel Watts: " + avgDisplayWatts + "\n";
                }
            }

            if (debug)
            {
                Debug.Log(www.text);
            }
            
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
    }
}
