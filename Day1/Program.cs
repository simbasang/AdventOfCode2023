using System.Text.RegularExpressions;

var inputs = File.ReadAllLines("input.txt");

const string pattern = @"\d|one|two|three|four|five|six|seven|eight|nine";

var total = inputs
    .Select(input =>
    {
        var first = Regex.Match(input, pattern, RegexOptions.IgnoreCase).Value;
        var last = Regex.Match(input, pattern, RegexOptions.RightToLeft).Value;

        if (first.Length > 1)
            first = TranslateToNumber(first);
        if (last.Length > 1)
            last = TranslateToNumber(last);

        return int.Parse(first + last);
    }).Sum();

static string TranslateToNumber(string input)
{
    return input switch
    {
        "one" => "1",
        "two" => "2",
        "three" => "3",
        "four" => "4",
        "five" => "5",
        "six" => "6",
        "seven" => "7",
        "eight" => "8",
        "nine" => "9",
        _ => ""
    };
}