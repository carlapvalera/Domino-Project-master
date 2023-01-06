namespace Project;

public class FirstPlayerA : ObjectWithRandom, IFirstPlayer
{
    public FirstPlayerA() { }
    /// <summary>
    /// Se escoge un jugador random para iniciar
    /// </summary>
    /// <param name="game"></param>
    /// <returns></returns>
    public int IndexFirstPlayer(Game game)
    {
        return Random_.Next(0, game.Players.Count);
    }

    public string Print()
    {
        return "Random First Player";
    }
}