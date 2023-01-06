namespace Project;
public class IntelligentRandomStrategy : IStrategy
{
    public string Print()
    {
        return "Intelligent Random";
    }
    /// <summary>
    /// Si existe alguna ficha con la que pueda pasar la pone, sino juega aleatorio
    /// </summary>
    /// <param name="itIsOkPlayed"></param>
    /// <param name="player"></param>
    /// <param name="cursor"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public Token TokenToPlay(List<Token> itIsOkPlayed, Player player, int cursor, Table table)
    {
        IStrategy strategy = new RandomStrategy();
        List<int> values = Auxiliar.ValuesToPlay(itIsOkPlayed, table);
        List<Token> smartTokens = Auxiliar.WinnerTokens(itIsOkPlayed, values, player, cursor, table);

        if (smartTokens.Count != 0)
            return strategy.TokenToPlay(smartTokens, player, cursor, table);
        return strategy.TokenToPlay(itIsOkPlayed, player, cursor, table);
    }

}
public class RandomStrategy : ObjectWithRandom, IStrategy
{
    public string Print()
    {
        return "Random";
    }
    /// <summary>
    /// Juega aleatorio
    /// </summary>
    /// <param name="itIsOkPlayed"></param>
    /// <param name="player"></param>
    /// <param name="cursor"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public Token TokenToPlay(List<Token> itIsOkPlayed, Player player, int cursor, Table table)
    {
        return itIsOkPlayed[Random_.Next(0, itIsOkPlayed.Count)];
    }
}
public class BotagordaStrategy : IStrategy
{
    public string Print()
    {
        return "Botagorda";
    }
    /// <summary>
    /// Escoge la ficha que mas valor tenga y la devuelve (Botagorda)
    /// </summary>
    /// <param name="itIsOkPlayed"></param>
    /// <param name="player"></param>
    /// <param name="cursor"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public Token TokenToPlay(List<Token> itIsOkPlayed, Player player, int cursor, Table table)
    {
        List<int> values = new List<int>();
        for (int i = 0; i < itIsOkPlayed.Count; i++)
            values.Add(itIsOkPlayed[i].Left + itIsOkPlayed[i].Right);
        return itIsOkPlayed[values.IndexOf(values.Max())];
    }
}

public class IntelligentBotagordaStrategy : IStrategy
{
    public string Print()
    {
        return "Intelligent Botagorda";
    }

    /// <summary>
    /// Si existe alguna ficha que pueda pasar la pone, sino juega como botagorda
    /// </summary>
    /// <param name="itIsOkPlayed"></param>
    /// <param name="player"></param>
    /// <param name="cursor"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public Token TokenToPlay(List<Token> itIsOkPlayed, Player player, int cursor, Table table)
    {
        List<int> smartValues = Auxiliar.ValuesToPlay(itIsOkPlayed, table);
        List<Token> intelligentplays = Auxiliar.WinnerTokens(itIsOkPlayed, smartValues, player, cursor, table);
        BotagordaStrategy botagordaStrategy = new BotagordaStrategy();

        if (intelligentplays.Count > 0)
            return botagordaStrategy.TokenToPlay(intelligentplays, player, cursor, table);
        return botagordaStrategy.TokenToPlay(itIsOkPlayed, player, cursor, table);
    }
}

public class IntelligentStrategy : IStrategy
{
    public string Print()
    {
        return "Intelligent";
    }

    /// <summary>
    /// En esencia si tiene alguna ficha que pueda pasar la pone,sino
    /// pone la ficha que menos probabilidad tiene de llevar el rival
    /// </summary>
    /// <param name="itIsOkPlayed"></param>
    /// <param name="player"></param>
    /// <param name="cursor"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public Token TokenToPlay(List<Token> itIsOkPlayed, Player player, int cursor, Table table)
    {
        //Aqui lo que hace es quedarse con la ficha que el rival no tenga ,en caso de que ocurra
        List<int> valuesToPlay = Auxiliar.ValuesToPlay(itIsOkPlayed, table);
        List<Token> smartTokens = Auxiliar.WinnerTokens(itIsOkPlayed, valuesToPlay, player, cursor, table);

        //en caso de que exista una ficha que lo pase ponerla
        if (smartTokens.Count > 0)
            return smartTokens[0];

        int piecesNotGame = table.TokensTotal.Count - table.TokensInGame.Count;

        //Antes que nada expliquemos que es Stats ,
        //esto es un diccionario en el que se almacenan por cada posible cara del juego se
        //le hace asignar la cantidad de caras de ese tipo que se han jugado y el elemento
        //maximo + 1 que tengan estas caras, por ejemplo
        //Sea la variante doble 6 en el que se ha jugado el [1/3] y el [2,3] 
        //el diccionario quedaria como sigue
        // [0]=(0,7)
        // [1]=(1,7)
        // [2]=(1,7)
        // [3]=(2,7)
        // [4]=(0,7)
        // [5]=(0,7)
        // [6]=(0,7)
        //Aqui añadimos las estadisticas de lo que ha ocurrido hasta el momento a statsAux 
        Dictionary<int, (int, int)> statsAux = Clone(table.Stats);
        //y actualizamos stats aux con las fichas que ademas tengo para hacer mas preciso
        //la probabilidad
        for (int i = 0; i < player.Hand.Count; i++)
        {
            if(player.Hand[i].IsDouble)
                statsAux[player.Hand[i].Left] = (statsAux[player.Hand[i].Left].Item1 + 1, statsAux[player.Hand[i].Left].Item2);
            else
            {
                statsAux[player.Hand[i].Left] = (statsAux[player.Hand[i].Left].Item1 + 1, statsAux[player.Hand[i].Left].Item2);
                statsAux[player.Hand[i].Right] = (statsAux[player.Hand[i].Right].Item1 + 1, statsAux[player.Hand[i].Right].Item2);
            }
        }

        //Aqui realizamos con dichas estadisticas el calculo de la probabilidad de que
        //que el jugador posea dicha ficha y devolvemos la que menos probabilidad tiene 
        //de llevar el rival
        double[] probabilities = new double[valuesToPlay.Count];
        double min = double.MaxValue;
        int indexAux = 0;
        for (int i = 0; i < probabilities.Length; i++)
        {
            probabilities[i] = 1 / ((double)(statsAux[valuesToPlay[i]].Item2 + 1 - statsAux[valuesToPlay[i]].Item1) * piecesNotGame);
            if (min > probabilities[i])
            {
                min = probabilities[i];
                indexAux = i;
            }
        }
        return itIsOkPlayed[indexAux];
    }
    public Dictionary<int, (int, int)> Clone(Dictionary<int, (int, int)> dictionary)
    {
        Dictionary<int, (int, int)> clone = new Dictionary<int, (int, int)>();
        foreach (var item in dictionary)
        {
            clone[item.Key] = item.Value;
        }
        return clone;
    }
} 