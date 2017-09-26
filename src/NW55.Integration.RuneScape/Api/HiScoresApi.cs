using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NW55.Integration.RuneScape.Api
{
    public class HiScoresApi : RuneScapeApi<string, HiScores>
    {
        public override string GetUri(string playerName)
             => $"{ServicesBaseUri}/m=hiscore/index_lite.ws?player={playerName}";

        public override HiScores ParseResult(string parameter, string responseText)
        {
            if (responseText == null)
                return null;

            string[] lines = responseText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            HiScores.Entry[] entries = new HiScores.Entry[lines.Length];
            try
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] segments = line.Split(',');
                    if (segments.Length == 3)
                    {
                        entries[i] = new HiScores.Entry()
                        {
                            Rank = Int32.Parse(segments[0]),
                            Level = Int32.Parse(segments[1]),
                            Score = Int64.Parse(segments[2])
                        };
                    }
                    else if (segments.Length == 2)
                    {
                        entries[i] = new HiScores.Entry()
                        {
                            Rank = Int32.Parse(segments[0]),
                            Score = Int64.Parse(segments[1]),
                            Level = null
                        };
                    }
                    else
                    {
                        throw new FormatException("Found line with less than two or more than three columns.");
                    }
                }
            }
            catch (Exception e)
            {
                throw new InvalidDataException("Error parsing response.", e);
            }
            return new HiScores(entries);
        }
    }
}
