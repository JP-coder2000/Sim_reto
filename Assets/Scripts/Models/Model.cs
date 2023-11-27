[System.Serializable]

public class Step
{
    public agents[] agent;
}

public class agent
{
    public bool carrying_food;
    public int[] position;
    public string role;
    public int type;
    public int unique_id;
}

public class deposit
{
    public int[] position;
}

public class food
{
    public int[] position;
}

