var ini = System.IO.File.ReadAllText(Path.Join("ConfigFiles", "example-1.ini"));

// var parsed = Parser.Run(ini);

// foreach (var kV in parsed)
// {
//     System.Console.WriteLine($"{kV.Key} => {kV.Value}");
// }

// var parsedIni = iniParser.Many().Parse(ini);

var p = Parser.Run(ini);

foreach (var k in p)
{
    System.Console.WriteLine(k);
}