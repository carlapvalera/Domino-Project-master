namespace Project;

public class ScoreCalculatorA : IScoreCalculator
{
    public string Print()
    {
        return "Classic";
    }

    /// <summary>
    /// Se asigna las puntuaciones a los jugadores de acuerdo a la suma de todos los valores
    /// de las caras de sus respectivas fichas
    /// </summary>
    /// <param name="game"></param>
    public void ToCalculateScore(Game game)
    {
        Auxiliar.ScoreCalculate((i, j) => false, game);
    }
}
public class ScoreCalculatorB : IScoreCalculator
{
    public string Print()
    {
        return "Doubles Have Double Puntuaction (Los dobles a la hora de sumar la puntuación final valen el doble)";
    }

    /// <summary>
    /// Analogo al calsico de arriba con la excepcion de que los dobles valen
    /// el doble de puntos
    /// </summary>
    /// <param name="game"></param>
    public void ToCalculateScore(Game game)
    {
        Auxiliar.ScoreCalculate((i, j) => game.Players[i].Hand[j].IsDouble, game);
    }
}