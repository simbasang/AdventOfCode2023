// See https://aka.ms/new-console-template for more information

var inputs = File.ReadAllLines("input.txt");

var resOne = PartOne(inputs);
var resTwo = PartTwo(inputs);

static int PartOne(string[] inputs)
{
    var colorAmountMax = new Dictionary<string, int>()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 },
    };

    //convert to linq?
    var sum = 0;
    foreach (var input in inputs)
    {
        var game = input.Split(":").Last();
        var gameId = input.Split(":").First().Split(" ").Last();
        var turns = game.Split(";");
        if (turns
            .Select(x => x.Trim().Split(","))
            .Any(x =>
                x.Any(
                    z => int.Parse(z.Trim().Split(" ").First()) >
                         colorAmountMax[z.Trim().Split(" ").Last()])
            ))
            continue;

        sum += int.Parse(gameId);
    }

    return sum;
}

static int PartTwo(string[] inputs)
{
   var sum = 0;
   //convert to linq?
    foreach (var input in inputs)
    {
        var game = input.Split(":").Last();
        var gameId = input.Split(":").First().Split(" ").Last();
        var turns = game.Split(";");
        var colorAmount = turns
            .Select(x => x.Trim().Split(","));


        var red = colorAmount.SelectMany(x =>
            x.Where(z => 
                    z.Trim().Split(" ").Last() == "red")
                    .Select(z => int.Parse(z.Trim().Split(" ").First())))
            .Max();
        
        var green = colorAmount.SelectMany(x =>
            x.Where(z => 
                    z.Trim().Split(" ").Last() == "green")
                    .Select(z => int.Parse(z.Trim().Split(" ").First()))).Max()
            ;
        
        var blue = colorAmount.SelectMany(x =>
            x.Where(z => 
                z.Trim().Split(" ").Last() == "blue")
                .Select(z => int.Parse(z.Trim().Split(" ").First())))
            .Max();

        sum += (red*green*blue);
    }

    return sum;
}