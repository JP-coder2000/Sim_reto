// TC2008B Modelación de Sistemas Multiagentes con gráficas computacionales
// C# client to interact with Python server via POST
// Sergio Ruiz-Loza, Ph.D. March 2021

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class WebClient : MonoBehaviour
{
    public GameObject agentPrefab;
    public GameObject foodPrefab; 
    public GameObject depositPrefab; 

    private Dictionary<int, GameObject> agents = new Dictionary<int, GameObject>();

    // IEnumerator - yield return
    public IEnumerator SendData(string data, Action<string> callback)
    {
        WWWForm form = new WWWForm();
        form.AddField("bundle", "the data");
        string url = "http://127.0.0.1:5000/step";

        //Debug.Log("Enviando datos a: " + url);
        Debug.Log("Datos enviados: " + data);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(data);
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            //www.SetRequestHeader("Content-Type", "text/html");
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest(); // Talk to Python
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                callback(null);
            }
            else
            {
                //Debug.Log(www.downloadHandler.text); // Respuesta de Python
                callback(www.downloadHandler.text);
            }
        }
    }

    void UpdateScene(Step stepData)
    {
        Debug.Log("Actualizando escena con datos recibidos");
        // Actualiza agentes
        foreach (var agentData in stepData.agents)
        {
            Debug.Log($"Procesando agente con ID: {agentData.unique_id} en posición: {agentData.position[0]}, {agentData.position[1]}");

            if (!agents.ContainsKey(agentData.unique_id))
            {
                if (agentPrefab != null)
                {
                    GameObject agentObj = Instantiate(agentPrefab, new Vector3(agentData.position[0], 0, agentData.position[1]), Quaternion.identity);
                    agents[agentData.unique_id] = agentObj;
                }
                else
                {
                    Debug.LogError("Agent prefab is not set!");
                }
            }
            else
            {
                agents[agentData.unique_id].transform.position = new Vector3(agentData.position[0], 0, agentData.position[1]);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 fakePos = new Vector3(3.44f, 0, -15.707f);
        string json = EditorJsonUtility.ToJson(fakePos);

        StartCoroutine(SendData(json, HandleResponse));
    }

    private void HandleResponse(string response)
    {
        Debug.Log("response");
        if (response != null)
        {
            Step stepData = JsonUtility.FromJson<Step>(response);
            if (stepData != null)
            {
                Debug.Log("Datos deserializados correctamente");
                UpdateScene(stepData);
            }
            else
            {
                Debug.LogError("Error al deserializar la respuesta");
            }
        }
        else
        {
            Debug.LogError("Respuesta nula o vacía");
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}