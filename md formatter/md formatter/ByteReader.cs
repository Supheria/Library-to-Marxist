using System;
using System.Diagnostics.CodeAnalysis;

namespace md_formatter;

public class ByteReader
{
    private byte[] Buffer { get; } = Array.Empty<byte>();

    private uint BufferPosition { get; set; }

    public ByteReader(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"could not open file: {filePath}");
            return;
        }
        using var file = File.OpenRead(filePath);
        Buffer = new byte[file.Length];
        _ = file.Read(Buffer, 0, Buffer.Length);
    }

    public bool Read(out byte bt)
    {
        bt = 0;
        if (BufferPosition >= Buffer.Length)
            return false;
        bt = Buffer[BufferPosition++];
        return true;
    }
}