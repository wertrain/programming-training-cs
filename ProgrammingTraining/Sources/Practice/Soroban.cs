using System;
using System.Text;

namespace ProgrammingTraining
{
    /// <summary>
    /// そろばんを扱うクラス
    /// 規定の文字列しか扱えないものとする
    /// </summary>
    class Soroban
    {
        /// <summary>
        /// そろばん文字列の長さ
        /// </summary>
        private static readonly int TextLength = 8;

        /// <summary>
        /// 数値があることを示す文字
        /// </summary>
        private static readonly char CharOne = '*';

        /// <summary>
        /// 数値があることを示す文字列
        /// </summary>
        private static readonly string StringOne = CharOne.ToString();

        /// <summary>
        /// 数値があることを示す文字
        /// </summary>
        private static readonly string StringOne5 = StringOne + StringOne + StringOne + StringOne + StringOne;

        /// <summary>
        /// 数値がないことを示す文字
        /// </summary>
        private static readonly char CharNone = '|';

        /// <summary>
        /// 数値がないことを示す文字列
        /// </summary>
        private static readonly string StringNone = CharNone.ToString();

        /// <summary>
        /// 区切り線を示す文字
        /// </summary>
        private static readonly char CharSeparater = '=';

        /// <summary>
        /// 区切り線を示す文字列
        /// </summary>
        private static readonly string StringSeparater = CharSeparater.ToString();

        /// <summary>
        /// 数値をそろばん文字列として出力する
        /// </summary>
        /// <param name="num"></param>
        public static string[] Output(ulong num, int length)
        {
            var numString = num.ToString();

            var chars = numString.ToCharArray();

            StringBuilder[] soroban = new StringBuilder[TextLength];
            for (int j = 0, jmax = soroban.Length; j < jmax; ++j)
            {
                soroban[j] = new StringBuilder();
            }

            // 不足分の桁を埋める
            if (length > chars.Length)
            {
                int diff = length- numString.Length;
                string empty = StringOne + StringNone + StringSeparater + StringNone + StringOne + StringOne + StringOne + StringOne;
                var emptyChars = empty.ToCharArray();
                for (int j = 0; j < diff; ++j)
                {
                    for (int i = 0; i < emptyChars.Length; ++i)
                    {
                        soroban[i].Append(emptyChars[i]);
                    }
                }
            }

            for (int i = 0, imax = chars.Length; i < imax; ++i)
            {
                var result = new StringBuilder();
                var n = int.Parse(chars[i].ToString());

                if (n >= 5)
                {
                    n -= 5;
                    result.Append(CharNone);
                    result.Append(CharOne);
                }
                else
                {
                    result.Append(CharOne);
                    result.Append(CharNone);
                }

                result.Append(CharSeparater);

                StringBuilder fourOrLess = new StringBuilder(StringOne5);
                fourOrLess[n] = CharNone;
                result.Append(fourOrLess);

                var resChars = result.ToString().ToCharArray();
                for (int j = 0, jmax = resChars.Length; j < jmax; ++j)
                {
                    soroban[j].Append(resChars[j]);
                }
            }

            string[] ret = new string[TextLength];
            for (int i = 0, imax = ret.Length; i < imax; ++i)
            {
                ret[i] = soroban[i].ToString();
            }

            return ret;
        }

        /// <summary>
        /// そろばん文字列を数値に変換する
        /// </summary>
        /// <param name="soroban"></param>
        /// <returns></returns>
        public static ulong Input(string[] soroban)
        {
            var width = soroban[0].Length;
            ulong amount = 0;

            for (int x = 0; x < width; ++x)
            {
                int y = 1;
                var chars = soroban[y].ToCharArray();

                ulong p = (ulong)Math.Pow(10, (width - x) - 1);
                ulong value = 0;
                if (chars[x] == '*')
                    value = (5 * p);

                ulong count = 0;
                for (y = 3; y < 8; ++y)
                {
                    chars = soroban[y].ToCharArray();
                    if (chars[x] == '*')
                        ++count;
                    else break;
                }

                amount += value;
                amount += (count * p);
            }

            return amount;
        }

        /// <summary>
        /// コンソール出力ユーティリティ
        /// </summary>
        /// <param name="soroban"></param>
        public static void Print(string [] soroban)
        {
            for (int i = 0; i < TextLength; ++i)
            {
                Console.WriteLine(soroban[i]);
            }
        }
    }

    class SorobanTest : IPracticeTest
    {
        public bool Test()
        {
            string[] sorobanA =
{
                "***|*||||*",
                "|||*|****|",
                "==========",
                "|***|**||*",
                "*|********",
                "*****|***|",
                "**|*******",
                "***|**|***"
            };
            string[] sorobanB =
            {
                "||||||||*|",
                "********|*",
                "==========",
                "*|**|*****",
                "|*********",
                "******|***",
                "**||*|*|*|",
                "********|*"
            };

            var a = Soroban.Input(sorobanA);
            var b = Soroban.Input(sorobanB);

            Console.WriteLine("SorobanA is {0}.", a);
            Console.WriteLine("SorobanB is {0}.", b);

            var c = Soroban.Output(UInt64.MaxValue, 32);

            Console.WriteLine();
            Console.WriteLine("Print SorobanC in 32 characters");
            Soroban.Print(c);

            Console.WriteLine("SorobanC is {0}.", Soroban.Input(c));

            return true;
        }
    }
}
