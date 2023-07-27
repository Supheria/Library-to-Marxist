namespace md_formatter;

public class Formatter
{
    private const byte EndLine = (byte)'\n';

    private Dictionary<int, Paragraph> Comments { get; } = new();

    private List<Paragraph> Paragraphs { get; } = new();

    public Formatter(string sourcePath, string targetPath)
    {
        GetParagraphs(sourcePath);

        try
        {
            using var writer = File.Open(targetPath, FileMode.Create);
            foreach (var enumerator in Paragraphs.Select(paragraph => paragraph.Value.GetEnumerator()))
            {
                while (enumerator.MoveNext())
                {
                    var bt = (byte)enumerator.Current;
                    if ((char)bt is '[')
                    {
                        var bytes = new List<byte> { bt };
                        var isComment = false;
                        while (enumerator.MoveNext())
                        {
                            bt = (byte)enumerator.Current;
                            bytes.Add(bt);
                            if ((char)bt is not ']')
                                continue;
                            isComment = true;
                            break;
                        }

                        var para = new Paragraph(bytes.ToArray());
                        if (isComment && para.ToString().GetMatch(@"\[(\d+)\]", out var match) &&
                            Comments.ContainsKey(match.Groups[1].Value.ToInt32() ?? -1)) 
                        {
                            writer.WriteByte((byte)'`');
                            writer.Write(Comments[match.Groups[1].Value.ToInt32() ?? -1].Value);
                            writer.WriteByte((byte)'`');
                        }
                        else
                            writer.Write(para.Value);
                    }
                    else
                        writer.WriteByte(bt);
                }
                writer.WriteByte(EndLine);
                writer.WriteByte(EndLine);
            }
        }
        catch
        {
            // ignored
        }
    }

    private void GetParagraphs(string sourcePath)
    {
        foreach (var paragraph in new ParagraphReader(sourcePath).Paragraphs)
        {
            if (paragraph.ToString().GetMatch(@"^\s*\[(\d+)\] +(.+)", out var match))
                Comments[match.Groups[1].Value.ToInt32() ?? -1] = new(match.Groups[2].Value.ToBytes());
            else
                Paragraphs.Add(paragraph);
        }
    }
}