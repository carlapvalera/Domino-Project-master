namespace Project;
public class Game
{
    public int CountPasses { get; private set; } = 0; //cantidad total de pases en el juego
    public List<string> Log { get; private set; } = new(); //El historial de todo lo que ocurre en el juego
    public int Cursor { get; private set; } = -1; 
    public IDistribution Distribution { get; }
    public IEndGame EndGame { get; }
    public (bool,List<Player>) IsEndRound_ { get; private set; } = (false,new List<Player>());
    public (bool,List<Player>) IsEndGame { get; private set; } = (false,new List<Player>());
    public IEndRound EndRound { get; }
    public IScoreCalculator ScoreCalculator { get; }
    public IFirstPlayer FirstPlayer { get; }
    public ITokensGenerator Generator { get; }
    public IActionModeratorToAdd ActionModeratorToAdd { get; }
    public IActionModeratorToSub ActionModeratorToSub { get; }
    public IStep Steps { get; }
    public Table Table_{ get; private set; }
    public List<Player> Players { get; private set; }
    public Game(IDistribution distribution,IEndGame endGame,
                IEndRound endRound,IScoreCalculator scoreCalculator,
                IFirstPlayer firstPlayer, IStep steps,
                IActionModeratorToAdd actionModeratorToAdd,
                IActionModeratorToSub actionModeratorToSub, 
                ITokensGenerator generator, List<Player> players)
    {
        Steps = steps;
        Distribution = distribution;
        EndGame = endGame;
        EndRound = endRound;
        ScoreCalculator = scoreCalculator;
        FirstPlayer = firstPlayer;
        ActionModeratorToAdd = actionModeratorToAdd;
        ActionModeratorToSub = actionModeratorToSub;
        Players = players;
        Generator = generator;
        Table_ = new Table(Generator);
    }
    /// <summary>
    /// Es donde ocurre el proceso ciclico del juego, la retroalimentacion misma
    /// hasta que este se de por concluido
    /// </summary>
    /// <param name="strategy"></param>
    /// <returns></returns>
    public bool Play(IStrategy strategy)
    {
        IsEndRound_ = EndRound.IsEndRound(this);
        IsEndGame = EndGame.IsEndGame(this);
        if (Table_.IsStart)
        {
            Log.Add("Start\n");
            Distribution.ToDistribute(Table_.TokensTotal,Players,Table_.Stats.Count);
            Log.Add("Distribution of Tokens\n");
            Cursor = FirstPlayer.IndexFirstPlayer(this);
            Log.Add("The first selected player is " + Players[Cursor].Name+'\n');
            BasicPlay(strategy);
            return true;
        }
        if (!IsEndGame.Item1)
        {
            if (IsEndRound_.Item1)
            {
                ScoreCalculator.ToCalculateScore(this);
                Log.Add("End Round\n");
                for (int i = 0; i < IsEndRound_.Item2.Count; i++)
                    Log.Add(IsEndRound_.Item2[i].Name + " has winned the round\n");
                Reset();
                return true;
            }
            BasicPlay(strategy);
            return true;
        }
        for (int i = 0; i < IsEndGame.Item2.Count; i++)
            Log.Add(IsEndGame.Item2[i].Name + " has winned the game\n");
        if(IsEndGame.Item2.Count==0)
            Log.Add("Tie\n");
        Log.Add("End Game\n");
        return false;
    }

    public void Reset()
    {
        Table_ = new Table(Generator);
        CountPasses = 0;
        for (int i = 0; i < Players.Count; i++)
            Players[i].Hand.Clear();
    }
    /// <summary>
    /// Metodo auxiliar para no repetir codigo innecesario en el metodo anterior
    /// respetando el princio DRY
    /// </summary>
    /// <param name="strategy"></param>
    public void BasicPlay(IStrategy strategy)
    {
        Player currentPlayer = Players[Cursor];
        List<Token> tokensToPlay = Auxiliar.ValidTokensToPlay(currentPlayer.Hand, ActionModeratorToAdd, ActionModeratorToSub, Table_);
        Token tokenToPlay = currentPlayer.Play(Steps.IndexNextPlayer(this), Table_, tokensToPlay, strategy);
        if (currentPlayer.Hand.Remove(tokenToPlay))
            Log.Add(currentPlayer.Name + " has played " + tokenToPlay + '\n');
        else
        {
            CountPasses++;
            Log.Add(currentPlayer.Name + " has passed\n");
        }
        Cursor = Steps.IndexNextPlayer(this);
        Table_.Eject(tokenToPlay);
    }
}
