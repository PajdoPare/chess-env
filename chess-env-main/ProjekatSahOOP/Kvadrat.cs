public struct Kvadrat
{
    public int Row { get; }
    public int Col { get; }
    public Kvadrat(int row, int col)
    {  Row = row; Col = col; }
    public static bool operator ==(Kvadrat a, Kvadrat b)=>a.Row == b.Row && a.Col == b.Col;
    public static bool operator!=(Kvadrat a, Kvadrat b)=>!(a == b);
    public override string ToString() => $"{(char)('a' + Col)} {8 - Row}";
    public static Kvadrat FromString(string S)
    {
        int col = S[0] - 'a';
        int row = 8 - (S[1] - '0');
        return new Kvadrat(row, col);
    }



}