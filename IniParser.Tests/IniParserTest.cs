using System.Collections.Generic;
using System.IO;
using IniParser.Models;
using Xunit;

namespace IniParser.Tests;

public class IniParserTest
{
    [Fact]
    public void CanParseIni()
    {
        var ini = File.ReadAllText(Path.Join("IniConfigFiles", "example-1.ini"));

        var iniParsed = Parser.Run(ini);

        Assert.True(AreIniSectionsEqual(example1, iniParsed));
    }

    private IEnumerable<IniSection> example1 = new IniSection[]
    {
        new IniSection(
            string.Empty,
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("key1", "value1"),
                new KeyValuePair<string, string>("key2", "value2"),
                new KeyValuePair<string, string>("key3", "value3"),
            }
        ),
        new IniSection(
            "section1",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("key4", "value4"),
            }
        ),
        new IniSection(
            "section2",
            new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("key5", "value5"),
                new KeyValuePair<string, string>("key6", "value6"),
            }
        ),
    };

    private bool AreIniSectionsEqual(
        IEnumerable<IniSection> ini1,
        IEnumerable<IniSection> ini2)
    {
        var ini1List = new List<IniSection>(ini1);
        var ini2List = new List<IniSection>(ini2);

        if (ini1List.Count != ini2List.Count)
        {
            return false;
        }

        for (int i = 0; i < ini1List.Count; i++)
        {
            var iniSection1 = ini1List[i];
            var iniSection2 = ini2List[i];

            if (iniSection1.Name != iniSection2.Name)
            {
                return false;
            }

            var properties1 = new List<KeyValuePair<string, string>>(iniSection1.Properties);
            var properties2 = new List<KeyValuePair<string, string>>(iniSection2.Properties);

            if (properties1.Count != properties2.Count)
            {
                return false;
            }

            for (int j = 0; j < properties1.Count; j++)
            {
                var kv1 = properties1[j];
                var kv2 = properties2[j];

                if (kv1.Key != kv2.Key || kv1.Value != kv2.Value)
                {
                    return false;
                }
            }
        }

        return true;
    }
}