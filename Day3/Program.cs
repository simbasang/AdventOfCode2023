using Day3;

var input = File.ReadLines("input.txt").ToArray();

var lastRowIndexs = new List<int>();
var total = 0;
for (var i = 0; i < input.Length; i++)
{
    var row = ConvertRow(input[i]);

    var previusRow = i == 0 ? new Row() : ConvertRow(input[i - 1]);
    var nextRow = i == (input.Length - 1) ? new Row() : ConvertRow(input[i + 1]);
    var wt = CalculateRow(row);

    var t = CalculateRelativeRows(previusRow, row, nextRow);

    var stopp = wt + t;
    total += wt + t;
}

var stop = 0;

static Row ConvertRow(string row)
{
    var numbers = new List<Number>();
    var signIndexs = new List<int>();

    var numberstring = "";
    var startIndex = -1;
    var endIndex = 0;

    for (var i = 0; i < row.Length; i++)
    {
        if (row[i] == '.' || !char.IsDigit(row[i]) || i == row.Length - 1)
        {
            if (i == row.Length - 1 && char.IsDigit(row[i]))
                numberstring += row[i];
            

            if (startIndex != -1)
            {
                endIndex = i == 0 ? i : i - 1;
                numbers.Add(new Number
                {
                    Sum = int.Parse(numberstring),
                    StartIndex = startIndex,
                    EndIndex = endIndex,
                });
            }

            numberstring = "";
            startIndex = -1;
            endIndex = 0;

            if (!char.IsDigit(row[i]) && row[i] != '.')
                signIndexs.Add(i);

            continue;
        }

        if (startIndex == -1)
            startIndex = i;

        numberstring += row[i];
    }

    return new Row { Numbers = numbers, SpecialSignIndexs = signIndexs };
}


static int CalculateRow(Row row)
{
    var sum = 0;

    foreach (var number in row.Numbers)
    {
        if (!row.SpecialSignIndexs.Contains(number.StartIndex - 1)
            && !row.SpecialSignIndexs.Contains(number.EndIndex + 1))
            continue;

        sum += number.Sum;
        number.IsUsed = true;
    }

    return sum;
}

static int CalculateRelativeRows(Row Upper, Row center, Row next)
{
    var sum = 0;
    foreach (var number in center.Numbers.Where(number => !number.IsUsed))
    {
        for (var i = number.StartIndex - 1; i <= number.EndIndex + 1; i++)
        {
            if (!Upper.SpecialSignIndexs.Contains(i)
                && !next.SpecialSignIndexs.Contains(i))
                continue;

            sum += number.Sum;
            number.IsUsed = true;
            break;
        }
    }

    return sum;
}