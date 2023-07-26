using System.Text;

namespace md_formatter;

public class Paragraph
{
    public byte[] Value { get; }

    public Paragraph(byte[] value)
    {
        Value = value;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var bt in Value)
            sb.Append((char)bt);
        return sb.ToString();
    }
}