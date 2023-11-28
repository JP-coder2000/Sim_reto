using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimulationManager : MonoBehaviour
{
    public WebClient webClient;
    public GameObject agentPrefab;
    public GameObject foodPrefab;
    public GameObject depositPrefab;

    private Dictionary<int, GameObject> agents = new Dictionary<int, GameObject>();
    private Dictionary<string, GameObject> foods = new Dictionary<string, GameObject>();
    private GameObject deposit;

    void Start()
    {
        StartCoroutine(UpdateSimulation());
    }

    IEnumerator UpdateSimulation()
    {
        while (true)
        {
            yield return StartCoroutine(webClient.SendData("", (response) => {
                if (response != null)
                {
                    Step simulationData = JsonUtility.FromJson<Step>(response);
                    UpdateAgents(simulationData.agents);
                    UpdateFood(simulationData.food);
                    UpdateDeposit(simulationData.deposit_cell);
                }
            }));
            yield return new WaitForSeconds(1);
        }
    }

    //void UpdateAgents(agent[] agentData)
    //    {
    //    Debug.Log($"Actualizando {agentData.Length} agentes");
    //    foreach (agent a in agentData)
    //        {
    //            GameObject agentObj;
    //            if (!agents.TryGetValue(a.unique_id, out agentObj))
    //            {
    //                agentObj = Instantiate(agentPrefab, new Vector3(a.position[0], 0, a.position[1]), Quaternion.identity);
    //                agents[a.unique_id] = agentObj;
    //            }
    //            Agent agentScript = agentObj.GetComponent<Agent>();
    //            Vector3 newPosition = new Vector3(a.position[0], 0, a.position[1]);
    //            agentScript.MoveTo(newPosition);
    //            }
    //    }

    void UpdateAgents(agent[] agentData)
    {
        if (agentData == null)
        {
            Debug.LogError("agentData es null");
            return;
        }

        Debug.Log($"Actualizando {agentData.Length} agentes");

        foreach (agent a in agentData)
        {
            if (a == null)
            {
                Debug.LogError("Datos de agente inválidos");
                continue;
            }

            GameObject agentObj;
            if (!agents.TryGetValue(a.unique_id, out agentObj))
            {
                if (agentPrefab == null)
                {
                    Debug.LogError("agentPrefab es null");
                    continue;
                }

                Debug.Log($"Creando agente con ID {a.unique_id} en la posición {a.position[0]}, {a.position[1]}");
                agentObj = Instantiate(agentPrefab, new Vector3(a.position[0], 0, a.position[1]), Quaternion.identity);
                agents[a.unique_id] = agentObj;
            }
            else
            {
                Debug.Log($"Actualizando posición del agente con ID {a.unique_id}");
                agentObj.transform.position = new Vector3(a.position[0], 0, a.position[1]);
            }

            // Aquí puedes agregar más lógica para actualizar el estado del agente
        }
    }


    void UpdateFood(int[][] foodData)
    {
        if (foodData == null)
        {
            Debug.LogError("foodData es null");
            return;
        }

        if (foods == null)
        {
            foods = new Dictionary<string, GameObject>();
        }

        Debug.Log($"Actualizando {foodData.Length} objetos de comida");

        foreach (int[] f in foodData)
        {
            if (f == null || f.Length < 2)
            {
                Debug.LogError("Datos de comida inválidos");
                continue;
            }

            Vector3 position = new Vector3(f[0], 0, f[1]);
            string foodKey = $"{f[0]}_{f[1]}";
            GameObject foodObj;

            if (!foods.TryGetValue(foodKey, out foodObj))
            {
                Debug.Log($"Creando comida en {position}");
                foodObj = Instantiate(foodPrefab, position, Quaternion.identity);
                foods[foodKey] = foodObj;
            }
            else
            {
                // Opcional: actualizar la posición de la comida si cambia
            }
        }
    }

    void UpdateDeposit(int[] depositData)
    {
        Debug.Log("Actualizando depósito");

        if (depositData == null || depositData.Length < 2)
        {
            Debug.LogError("Datos de depósito inválidos o incompletos");
            return;
        }

        if (deposit == null)
        {
            Vector3 position = new Vector3(depositData[0], 0, depositData[1]);
            deposit = Instantiate(depositPrefab, position, Quaternion.identity);
        }
        else
        {
            // Actualizar la posición del depósito si es necesario
        }
    }
}
