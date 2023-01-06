namespace Project;
public class Table
{
    /// <summary>
    /// estas son las estadisticas que hacen que el jugador inteligente sea
    /// preciso a la hora de tomar decisiones
    /// </summary>
    public Dictionary<int,(int,int)> Stats { get; }
    public bool IsStart { get { return TokensInGame.Count == 0; } }
    public List<Token> TokensInGame { get; set; } = new();
    public List<Token> TokensTotal { get; set; } = new();
    public int Left
    {
        get { return !IsStart ? TokensInGame[0].Left : -1; }
    }
    public int Right
    {
        get { return !IsStart ? TokensInGame[TokensInGame.Count - 1].Right : -1; }
    }

    public Table(ITokensGenerator generator) 
    {
        TokensTotal = generator.Generated();
        Stats = generator.Stats();
    }

    /// <summary>
    /// Pone la ficha en la mesa (empatar caras)
    /// </summary>
    /// <param name="token"></param> ficha a poner
    public void Eject(Token token)
    {
        bool hasBeenTurned = false;
        Eject(token, ref hasBeenTurned);
    }
    private void Eject(Token token, ref bool hasBeenTurned)
    {
        if (token == null) return;
        if (IsStart || token.Right == Left || token.Left == Right || hasBeenTurned)
        {
            if (token.IsDouble)
                Stats[token.Left] = (Stats[token.Left].Item1 + 1, Stats[token.Left].Item2);
            else
            {
                Stats[token.Left] = (Stats[token.Left].Item1 + 1, Stats[token.Left].Item2);
                Stats[token.Right] = (Stats[token.Right].Item1 + 1, Stats[token.Right].Item2);
            }
            if (TokensInGame.Count == 0 || token.Right == Left)
                ToLeft(token);
            else if (token.Left == Right)
                ToRight(token);
            else
                ToLeft(token);
        }
        else
        {
            token.ToTurn();
            hasBeenTurned = true;
            Eject(token, ref hasBeenTurned);
        }
    }
    /// <summary>
    /// La pone a la izquierda de la mesa
    /// </summary>
    /// <param name="piece"></param>
    public void ToLeft(Token piece)
    {
        TokensInGame.Insert(0, piece);
    }
    /// <summary>
    /// La pone a la derecha de la mesa
    /// </summary>
    /// <param name="piece"></param>
    public void ToRight(Token piece)
    {
        TokensInGame.Add(piece);
    } 
    /// <summary>
    /// Imprime la mesa actual
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string toString = "";
        for (int i = 0; i < TokensInGame.Count; i++)
        {
            toString+=TokensInGame[i]+" ";
        }
        return toString;
    }
}