namespace md_formatter;

public class ParagraphReader
{
    private const char EndLine = '\n';
    private const char EndLineR = '\r';

    public Paragraph[] Paragraphs { get; }

    public ParagraphReader(string filePath)
    {
        var reader = new ByteReader(filePath);
        var paragraphs = new List<Paragraph>();
        var paragraph = new List<byte>();

        while (reader.Read(out var bt))
        {
            switch ((char)bt)
            {
                case EndLineR:
                case EndLine:
                    if (paragraph.Any(ch => (char)ch is not EndLine && (char)ch is not EndLineR))
                        paragraphs.Add(new(paragraph.ToArray()));
                    paragraph = new();
                    break;
                default:
                    paragraph.Add(bt);
                    break;
            }
        }
        if (paragraph.Any(ch => (char)ch is not EndLine && (char)ch is not EndLineR))
            paragraphs.Add(new(paragraph.ToArray()));

        Paragraphs = paragraphs.ToArray();
    }
}