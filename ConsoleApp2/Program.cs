using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string xml = File.ReadAllText("Properties.xml");
        string[] allLines = xml.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
        for (int i = 0; i < allLines.Length; i += 100)
        {
            int chunkSize = Math.Min(100, allLines.Length - i);
            string[] lineChunkSize = new string[chunkSize];
            Array.Copy(allLines, i, lineChunkSize, 0, chunkSize);
            string chunkContent = string.Join(Environment.NewLine, lineChunkSize);
            Regex regex = new Regex(@"<Value>(.*?)</Value>", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(chunkContent);
            Console.WriteLine($"Chunk {i / 100 + 1}:");
            foreach (Match match in matches)
            {
                string value = match.Groups[1].Value;
                Console.WriteLine($"{value}");
            }
        }
    }
}