using Emmellsoft.IoT.Rpi.SenseHat;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Windows.UI;

namespace RaspberryApp.Classes.SenseHatActions
{
    public class ClockAction : BaseAction
    {
        #region Digits
        private static readonly int[,] digit_0 = new int[,]
        {
            {0,1,1,1},
            {0,1,0,1},
            {0,1,1,1},
            {0,0,0,0}
        };

        private static readonly int[,] digit_1 = new int[,]
        {
            {0,0,1,1},
            {0,0,1,0},
            {0,1,1,1},
            {0,0,0,0}
        };

        private static readonly int[,] digit_2 = new int[,]
        {
            {0,0,1,1},
            {0,0,1,0},
            {0,1,1,0},
            {0,0,0,0}
        };

        private static readonly int[,] digit_3 = new int[,]
        {
            {0,1,1,1},
            {0,1,1,0},
            {0,1,1,1},
            {0,0,0,0}
        };

        private static readonly int[,] digit_4 = new int[,]
        {
            {0,1,0,1},
            {0,1,1,1},
            {0,1,0,0},
            {0,0,0,0}
        };

        private static readonly int[,] digit_5 = new int[,]
        {
            {0,1,1,0},
            {0,0,1,0},
            {0,0,1,1},
            {0,0,0,0}
        };

        private static readonly int[,] digit_6 = new int[,]
        {
            {0,0,0,1},
            {0,1,1,1},
            {0,1,1,1},
            {0,0,0,0}
        };

        private static readonly int[,] digit_7 = new int[,]
        {
            {0,1,1,1},
            {0,1,0,0},
            {0,1,0,0},
            {0,0,0,0}
        };

        private static readonly int[,] digit_8 = new int[,]
        {
            {0,1,1,1},
            {0,1,1,1},
            {0,1,1,1},
            {0,0,0,0}
        };

        private static int[,] digit_9 = new int[,]
        {
            {0,1,1,1},
            {0,1,1,1},
            {0,1,0,0},
            {0,0,0,0}
        };

        private static readonly int[][,] digits = new[] { digit_0, digit_1, digit_2, digit_3, digit_4, digit_5, digit_6, digit_7, digit_8, digit_9 };
        #endregion

        public ClockAction(ISenseHat senseHat) : base(senseHat)
        {
        }

        protected override void Execute()
        {
            var counter = 0;
            var time = GetTime();

            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                if (counter == 60)
                {
                    time = GetTime();
                    counter = 0;
                }

                Write(time);
                waitEvent.Wait(500);
                ++counter;
            }
        }

        private int[] GetTime()
        {
            try
            {
                var html = new HttpClient().GetStringAsync("http://zuzia19062017.cba.pl/time.php").GetAwaiter().GetResult();
                var time = Regex.Match(html, "~~~(\\d{4})~~~").Groups[1].ToString();

                return time.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
            }
            catch
            {
                return new[] { 0, 0, 0, 0 };
            }
        }

        private void Write(int[] time)
        {
            var a0 = digits[time[1]];
            var a1 = digits[time[0]];
            var a2 = digits[time[3]];
            var a3 = digits[time[2]];

            var p = 0;
            var q = 0;

            senseHat.Display.Clear();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int[,] a = null;

                    if (i < 4 && j < 4)
                    {
                        a = a0;
                        p = 0;
                        q = 0;
                    }
                    else if (i < 4 && j >= 4)
                    {
                        a = a1;
                        p = 0;
                        q = 4;
                    }
                    else if (i >= 4 && j < 4)
                    {
                        a = a2;
                        p = 4;
                        q = 0;
                    }
                    else if (i >= 4 && j >= 4)
                    {
                        a = a3;
                        p = 4;
                        q = 4;
                    }

                    senseHat.Display.Screen[i, j] = a[i - p, j - q] == 1 ? Colors.DarkRed : Colors.White;
                }
            }

            senseHat.Display.Update();
        }
    }
}