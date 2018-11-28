using Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outputs
{
    public class ReaderWriter
    {
        public ReaderWriter() { }

        public Fifteen ReadFifteen(string _fileName)
        {
            string text = File.ReadAllText(_fileName);

            string[] textParts = text.Split(new char[] { ' ', '\n' });

            Point empty = new Point();
            int width = Int32.Parse(textParts[0]), length = Int32.Parse(textParts[1]);

            int[,] result = new int[width, length];
            int counter = 2;
            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < length; j++)
                {
                    result[i, j] = Int32.Parse(textParts[counter]);
                    if (result[i, j] == 0)
                    {
                        empty.X = i;
                        empty.Y = j;
                    }
                    counter++;
                }
            }
            return new Fifteen(result,width,length,empty);
        }

        public void WriteResult(string _fileName, string _text)
        {
            File.WriteAllText(_fileName, _text);
        }

        public void WriteMetaData(string _fileName, int _solutionLength, int _visitedAmount, int _processedAmount, int _maxDepth, string _timeElapsed)
        {
            StringBuilder SB = new StringBuilder();
            SB.AppendLine(_solutionLength.ToString());
            SB.AppendLine(_visitedAmount.ToString());
            SB.AppendLine(_processedAmount.ToString());
            SB.AppendLine(_maxDepth.ToString());
            SB.AppendLine(_timeElapsed);

            File.WriteAllText(_fileName, SB.ToString());
        }
    }
}
