public class DeckComponent : System.ICloneable
{
    public string name;
    public int weight;

    public DeckComponent(string name, int weight)
    {
        this.name = name;
        this.weight = weight;
    }

    public object Clone()
    {
        return new DeckComponent(name, weight);
    }
}