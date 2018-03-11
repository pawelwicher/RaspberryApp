using Emmellsoft.IoT.Rpi.SenseHat;
using Windows.UI;

namespace RaspberryApp.Classes
{
    public static class ScreenHelper
    {
        public static void DisplayLetter(ISenseHat senseHat, char c)
        {
            var letterArray = GetLetterArray(c);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (letterArray[i, j] == 1)
                    {
                        senseHat.Display.Screen[i, j] = Colors.White;
                    }
                    else
                    {
                        senseHat.Display.Screen[i, j] = Colors.Black;
                    }
                }
            }
            senseHat.Display.Update();
        }

        private static int[,] GetLetterArray(char c)
        {
            switch (c)
            {
                case ' ': return EMPTY;
                case 'E': return LETTER_E;
                case 'H': return LETTER_H;
                case 'I': return LETTER_I;
                case 'L': return LETTER_L;
                case 'O': return LETTER_O;
            }

            return EMPTY;
        }

        private static readonly int[,] EMPTY =
        {
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 },
            {0, 0, 0, 0, 0, 0, 0, 0 }
        };

        private static readonly int[,] LETTER_E =
        {
            {0, 1, 1, 1, 1, 1, 1, 0 },
            {0, 1, 1, 1, 1, 1, 1, 0 },
            {0, 0, 0, 0, 0, 1, 1, 0 },
            {0, 1, 1, 1, 1, 1, 1, 0 },
            {0, 1, 1, 1, 1, 1, 1, 0 },
            {0, 0, 0, 0, 0, 1, 1, 0 },
            {0, 1, 1, 1, 1, 1, 1, 0 },
            {0, 1, 1, 1, 1, 1, 1, 0 }
        };

        private static readonly int[,] LETTER_H =
        {
            {0, 1, 1, 0, 0, 1, 1, 0 },
            {0, 1, 1, 0, 0, 1, 1, 0 },
            {0, 1, 1, 0, 0, 1, 1, 0 },
            {0, 1, 1, 1, 1, 1, 1, 0 },
            {0, 1, 1, 1, 1, 1, 1, 0 },
            {0, 1, 1, 0, 0, 1, 1, 0 },
            {0, 1, 1, 0, 0, 1, 1, 0 },
            {0, 1, 1, 0, 0, 1, 1, 0 }
        };

        private static readonly int[,] LETTER_I =
        {
            {0, 0, 1, 1, 1, 1, 0, 0 },
            {0, 0, 1, 1, 1, 1, 0, 0 },
            {0, 0, 0, 1, 1, 0, 0, 0 },
            {0, 0, 0, 1, 1, 0, 0, 0 },
            {0, 0, 0, 1, 1, 0, 0, 0 },
            {0, 0, 0, 1, 1, 0, 0, 0 },
            {0, 0, 1, 1, 1, 1, 0, 0 },
            {0, 0, 1, 1, 1, 1, 0, 0 }
        };

        private static readonly int[,] LETTER_L =
        {
            {0, 0, 0, 0, 0, 1, 1, 0 },
            {0, 0, 0, 0, 0, 1, 1, 0 },
            {0, 0, 0, 0, 0, 1, 1, 0 },
            {0, 0, 0, 0, 0, 1, 1, 0 },
            {0, 0, 0, 0, 0, 1, 1, 0 },
            {0, 0, 0, 0, 0, 1, 1, 0 },
            {0, 0, 1, 1, 1, 1, 1, 0 },
            {0, 0, 1, 1, 1, 1, 1, 0 }
        };

        private static readonly int[,] LETTER_O =
        {
            {0, 0, 1, 1, 1, 1, 0, 0 },
            {0, 1, 1, 0, 0, 1, 1, 0 },
            {0, 1, 0, 0, 0, 0, 1, 0 },
            {0, 1, 0, 0, 0, 0, 1, 0 },
            {0, 1, 0, 0, 0, 0, 1, 0 },
            {0, 1, 0, 0, 0, 0, 1, 0 },
            {0, 1, 1, 0, 0, 1, 1, 0 },
            {0, 0, 1, 1, 1, 1, 0, 0 }
        };
    }
}