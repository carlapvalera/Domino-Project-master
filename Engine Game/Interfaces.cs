namespace Project;

/// <summary>
/// Estrategia a jugar
/// </summary>
public interface IStrategy:IPrinteable
{
    Token TokenToPlay(List<Token> itIsOkPlayed, Player player, int nextPlayerCursor, Table table  );
}
/// <summary>
/// Generador de fichas
/// </summary>
public interface ITokensGenerator:IPrinteable
{
    Dictionary<int,(int,int)> Stats();
    List<Token> Generated();
}
/// <summary>
/// Se acabo el juego con sus ganadores
/// </summary>
public interface IEndGame:IPrinteable
{
    (bool,List<Player>) IsEndGame(Game game);
}
/// <summary>
/// Se acabo la ronda con sus ganadores
/// </summary>
public interface IEndRound:IPrinteable
{
    (bool,List<Player>) IsEndRound(Game game);
}
/// <summary>
/// Primer jugador a jugar
/// </summary>
public interface IFirstPlayer:IPrinteable
{
    int IndexFirstPlayer(Game game);
}
/// <summary>
/// calcula la puntuacion
/// </summary>
public interface IScoreCalculator:IPrinteable
{
    void ToCalculateScore(Game game);
}
/// <summary>
/// Distribuye las fichas al inicio
/// </summary>
public interface IDistribution:IPrinteable
{
    void ToDistribute(List<Token> tokens,List<Player> players,int count);
}
/// <summary>
/// Lista de fichas validas independiente de las reglas clasicas
/// </summary>
public interface IActionModeratorToAdd:IPrinteable
{
    List<Token> TokensToAddThatItIsOk();
}
/// <summary>
/// Determina fichas invalidas independiente de las reglas clasicas
/// </summary>
public interface IActionModeratorToSub:IPrinteable
{
    List<Token> TokensToSubThatItIsNotOk();
}
/// <summary>
/// Determina el proximo jugador
/// </summary>
public interface IStep:IPrinteable
{
    int IndexNextPlayer(Game game);
}
/// <summary>
/// Objetos que se imprimen
/// </summary>
public interface IPrinteable
{
    string Print();
}