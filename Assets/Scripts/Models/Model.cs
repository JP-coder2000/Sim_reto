[System.Serializable]
public class Step
{
    public agent[] agents;
    public int[][] food;
    public int[] deposit_cell;
}

[System.Serializable]
public class agent
{
    public bool carrying_food;
    public int[] position;
    public string role;
    public int type;
    public int unique_id;
}
