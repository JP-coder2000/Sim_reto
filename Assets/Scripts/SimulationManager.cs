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
    private Dictionary<int, GameObject> foods = new Dictionary<int, GameObject>();
    private GameObject deposit;

    void Start()
    {
        StartCoroutine(UpdateSimulation());
    }

    IEnumerator UpdateSimulation()
    {
        while (true)
        {
            yield return StartCoroutine(webClient.SendData("")); 
            Step simulationData = JsonUtility.FromJson<Step>(webClient.data);

            UpdateAgents(simulationData.agents);
            UpdateFood(simulationData.food);
            UpdateDeposit(simulationData.deposit);

            yield return new WaitForSeconds(1); 
        }
    }

    void UpdateAgents(agent[] agentData)
    {
        void UpdateAgents(agent[] agentData)
        {
            foreach (agent a in agentData)
            {
                GameObject agentObj;
                if (!agents.TryGetValue(a.unique_id, out agentObj))
                {
                    agentObj = Instantiate(agentPrefab, new Vector3(a.position[0], 0, a.position[1]), Quaternion.identity);
                    agents[a.unique_id] = agentObj;
                }
                Agent agentScript = agentObj.GetComponent<Agent>();
                Vector3 newPosition = new Vector3(a.position[0], 0, a.position[1]);
                agentScript.MoveTo(newPosition);
                }
        }
    }

    void UpdateFood(food[] foodData)
    {
        foreach (food f in foodData)
        {
            GameObject foodObj;
            if (!foods.TryGetValue(f.unique_id, out foodObj))
            {
                Vector3 position = new Vector3(f.position[0], 0, f.position[1]);
                foodObj = Instantiate(foodPrefab, position, Quaternion.identity);
                foods[f.unique_id] = foodObj;
            }
            else
            {
                // Actualizar la posición de la comida si es necesario
            }
        }
    }

    void UpdateDeposit(deposit depositData)
    {
        if (deposit == null)
        {
            Vector3 position = new Vector3(depositData.position[0], 0, depositData.position[1]);
            deposit = Instantiate(depositPrefab, position, Quaternion.identity);
        }
        else
        {
            // Actualizar la posición del depósito si es necesario
        }
    }
}
