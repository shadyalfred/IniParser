using IniParser.Models;
using Sprache;

namespace IniParser;

public static class Parser
{
    private static Parser<string> KeyIdentifier = Parse.Identifier(Parse.Letter, Parse.LetterOrDigit);
    private static Parser<string> SectionNameIdentifier = Parse.Identifier(
        Parse.Letter,
        Parse.AnyChar.Except(Parse.WhiteSpace.Or(Parse.Chars(']'))));


    private static Parser<string> CommentParser = Parse
        .Token(Parse.Char('#').Or(Parse.Char(';')))
        .Then(
            _ => Parse.AnyChar.Until(Parse.LineTerminator).Text()
        );

    private static Parser<KeyValuePair<string, string>> PropertyParser =
        from comment in CommentParser.Many().Optional()
        from key in KeyIdentifier.Text()
        from equalSign in Parse.Token(Parse.Char('=')).Once()
        from value in Parse.Token(
            Parse.LetterOrDigit.Many()
        ).Text()
        select new KeyValuePair<string, string>(key, value);

    private static Parser<string> SectionNameParser =
        from comment in CommentParser.Many().Optional()
        from open in Parse.Char('[')
        from section in SectionNameIdentifier.Text()
        from close in Parse.Char(']')
        from comment2 in CommentParser.Many().Return(string.Empty)
                        .Or(Parse.LineTerminator.Text())
        select section;

    private static Parser<IniSection> SectionParser =
        from sectionName in SectionNameParser.Optional()
        from properties in PropertyParser.Many().Optional()
        select new IniSection(
            sectionName.IsDefined ? sectionName.Get() : string.Empty,
            properties.GetOrDefault());

    public static IEnumerable<IniSection> Run(string input)
    {
        return SectionParser.Many().Parse(input);
    }
}

