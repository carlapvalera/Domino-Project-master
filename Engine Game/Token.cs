namespace Project;
public class Token:IEquatable<Token> 
{
    public int Left { get; private set; }
    public int Right { get; private set; }
    public bool IsDouble { get; }
    public Token(int left, int right)
    {
        Left = left;
        Right = right;
        IsDouble = Left == Right;
    }
    /// <summary>
    /// Permite girar la ficha para acomodarse a la mesa
    /// </summary>
    public void ToTurn()
    {
        int temp = Left;
        Left = Right;
        Right = temp;
    }
    public override string ToString()
    {
        return "[" + this.Left + "/" + this.Right + "]";
    }
    /// <summary>
    /// Determina si una ficha es igual a otra
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Token? other)
    {
        return (Left == other.Left && Right == other.Right) ||
               (Left == other.Right && Right == other.Left);
    }
}