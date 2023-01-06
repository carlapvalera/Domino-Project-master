namespace Project;
public class Generator_9_Variant : ITokensGenerator
{
    /// <summary>
    /// Variante doble 9 del domino con sus 55 fichas
    /// </summary>
    /// <returns></returns>
    public List<Token> Generated()
    {
        return Auxiliar.BasicTokensGenerator(0, 9);
    }
    /// <summary>
    /// Este aspecto se explica mejor en Intelligent Strategy  
    /// </summary>
    /// <returns></returns>
    public Dictionary<int,(int,int)> Stats()
    {
        Dictionary<int, (int, int)> stats = new Dictionary<int, (int, int)>();
        for (int i = 0; i <= 9; i++)
        {
            stats[i] = (0, 10);
        }
        return stats;
    }
    public string Print()
    {
        return "Double Nine";
    }
}
public class Generator_6_Variant : ITokensGenerator
{
    /// <summary>
    /// Variante doble 6 con sus 28 fichas
    /// </summary>
    /// <returns></returns>
    public List<Token> Generated()
    {
        return Auxiliar.BasicTokensGenerator(0, 6);
    }
    /// <summary>
    /// Analogo al de la variante doble 9
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, (int, int)> Stats()
    {
        Dictionary<int, (int, int)> stats = new Dictionary<int, (int, int)>();
        for (int i = 0; i <= 6; i++)
        {
            stats[i] = (0, 7);
        }
        return stats;
    }
    public string Print()
    {
        return "Double Six";
    }
}