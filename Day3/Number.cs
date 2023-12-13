namespace Day3;

public record Number
{
    public int Sum { get; set; }
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public bool IsUsed { get; set; } = false;
}