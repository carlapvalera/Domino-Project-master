using System.Collections;

namespace Project;
public class ClassicStep : IStep
{
    /// <summary>
    /// La forma clasica de jugar a favor de las manecillas del reloj
    /// </summary>
    /// <param name="game"></param>
    /// <returns></returns>
    public int IndexNextPlayer(Game game)
    {
        return (game.Cursor + 1) % game.Players.Count;
    }

    public string Print()
    {
        return "Classic";
    }
}
public class ChangeDirectionWithPassStep : IStep
{
    public bool toLeft { get; private set; } = false;
    /// <summary>
    /// Juega como el calsico de arriba y en caso de pase se cambia el sentido en el que se juega
    /// </summary>
    /// <param name="game"></param>
    /// <returns></returns>
    public int IndexNextPlayer(Game game)
    {
        if(game.CountPasses%2!=0)
            return (game.Cursor - 1 + game.Players.Count) % game.Players.Count;
        ClassicStep classicStep = new();
        return classicStep.IndexNextPlayer(game);
    }

    public string Print()
    {
        return "Change Direction With Pass (Cambia el sentido del juego por cada pase en el juego)";
    }
}