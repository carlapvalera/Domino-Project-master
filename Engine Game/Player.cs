namespace Project;
public class Player
{
    public int CountIsContiniousPassed { get; private set; } = 0; //Cantidad de pases seguidos
    public List<Token> Hand { get; } = new List<Token>(); //La mano del jugador
    public SortedSet<int> IDoNotHaveProvedInGame { get; } //Fichas que se ha probado en juego que 
    //no lleva, o sea que se ha pasado por ella
    public int TokensCount { get { return Hand.Count; } } //Cantidad de fichas
    public string Name { get; }
    public double ScoreGame { get; set; } = 0;
    public double ScoreRound { get; set; } = 0;
    public List <Player> GameMates { get; set; } //Compañeros
    public Player(string name, List <Player> gameMates)
    {
        Name = name;
        IDoNotHaveProvedInGame = new SortedSet<int>();
        GameMates = gameMates;    
    }
    /// <summary>
    /// Manda a la estaregia a jugar, y dicho jugador juega segun la estrategia
    /// Ademas de actualizar alguna que otra estadistica
    /// </summary>
    /// <param name="next"></param>
    /// <param name="table"></param>
    /// <param name="itIsOkPlayed"></param>
    /// <param name="strategy"></param>
    /// <returns></returns>
    public Token Play(int next,Table table,List<Token> itIsOkPlayed,IStrategy strategy)
    {
        if (itIsOkPlayed.Count == 0)
        {
            IDoNotHaveProvedInGame.Add(table.Left);
            IDoNotHaveProvedInGame.Add(table.Right);
            CountIsContiniousPassed++;
            return null;
        }
        CountIsContiniousPassed = 0;
        Token token = strategy.TokenToPlay(itIsOkPlayed, this, next,table);
        return token;
    }
}