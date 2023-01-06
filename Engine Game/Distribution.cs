namespace Project;

public class ClassicDistributionTen : ObjectWithRandom,IDistribution
{
    public string Print()
    {
        return "Classic";
    }
    /// <summary>
    /// Distribucion clasica aleatoria de fichas
    /// </summary>
    /// <param name="tokens"></param> fichas a repartir
    /// <param name="players"></param> jugadores a los que repartir
    /// <param name="count"></param> cantidad de fichas que se desea repartir a cada uno
    public void ToDistribute(List<Token> tokens,List<Player> players,int count)
    {
        for (int i = 0; i < players.Count; i++)
            for (int j = 0; j < count; j++)
            {
                if (tokens.Count > 0)
                {
                    Token token = tokens[Random_.Next(0, tokens.Count)];
                    tokens.Remove(token);
                    players[i].Hand.Add(token);
                }
            }
    }
}
public class DoublesToTrashDistribution : ObjectWithRandom, IDistribution
{
    public string Print()
    {
        return "Doubles To Trash (En caso de que un judagor tenga mas de 5 dobles permitirle botar estos y volverele a repartir)";
    }
    /// <summary>
    /// Se distribuye normal y en caso de tocarle a un jugador 5 o mas dobles 
    /// se le permite botar estos y volver a repartir
    /// </summary>
    /// <param name="tokens"></param>
    /// <param name="players"></param>
    /// <param name="count"></param>
    public void ToDistribute(List<Token> tokens,List<Player> players,int count)
    {
        ClassicDistributionTen classicDistribution = new();
        classicDistribution.ToDistribute(tokens,players,count);
      
        foreach (var player in players)
        {
            List<Token> doubleTokens = new List<Token>();
            foreach (var token in player.Hand)
                if (token.IsDouble)
                    doubleTokens.Add(token);
            if (doubleTokens.Count >= 5)
            {
                for (int i = 0; i < doubleTokens.Count; i++)
                {
                    tokens.Add(doubleTokens[i]);
                    player.Hand.Remove(doubleTokens[i]);
                }
                ToDistributeRandomForOne(tokens, player, doubleTokens.Count);
            }
        }
    }
    /// <summary>
    /// Metodo auxiliar de Double To Trash para la distribucion del jugador que bota
    /// los dobles en caso de tener 5 o mas
    /// </summary>
    /// <param name="tokens"></param>
    /// <param name="player"></param>
    /// <param name="count"></param>
    void ToDistributeRandomForOne(List<Token> tokens,Player player,int count)
    {
        for (int i = 0; i < count; i++)
        {
            Token token = tokens[Random_.Next(0, tokens.Count)];
            player.Hand.Add(token);
        }
    }
}