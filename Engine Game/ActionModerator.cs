namespace Project;
public class DoubleWhiteActionToAdd : IActionModeratorToAdd 
{
    public string Print()
    {
        return "Double White is a Valid Token always (Literal)";
    }
    /// <summary>
    /// establece que la ficha del doble blanco sea permitida en cualquier momento del juego
    /// </summary>
    /// <returns></returns>
    public List<Token> TokensToAddThatItIsOk()
    {
        List<Token> TokensToAdd=new List<Token>();
        TokensToAdd.Add(new Token(0, 0));
        return TokensToAdd;
    }
}
public class ClassicActionToAdd : IActionModeratorToAdd
{
    public string Print()
    {
        return "Classic";
    }
    /// <summary>
    /// el modo clasico en que no se permiten fichas a jugar de manera invalida
    /// </summary>
    /// <returns></returns>
    public List<Token> TokensToAddThatItIsOk()
    {
        return new List<Token>();
    }
}
public class ClassicActionToSub : IActionModeratorToSub
{
    public string Print()
    {
        return "Classic";
    }
    /// <summary>
    /// el modo clasico en que se permiten todas las fichas a jugar de manera valida
    /// </summary>
    /// <returns></returns>
    public List<Token> TokensToSubThatItIsNotOk()
    {
        return new List<Token>();
    }
}