public struct Kvadrat
{
    public int Row { get; }
    public int Col { get; }
    public Kvadrat(int row, int col)
    {  Row = row; Col = col; }
    public static bool operator ==(Kvadrat a, Kvadrat b)=>a.Row == b.Row && a.Col == b.Col;
    public static bool operator!=(Kvadrat a, Kvadrat b)=>!(a == b);
}