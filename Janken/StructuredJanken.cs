using System;
using System.Linq;

namespace Janken
{
    /// <summary>
    /// 構造化プログラミング版じゃんけん
    /// </summary>
    public class StructuredJanken
    {
        private int Player1WonCount { get; set; }
        private int Player2WonCount { get; set; }
        private static readonly Random Random = new Random();

        public void Execute()
        {
            Console.WriteLine("【じゃんけん開始】" + Environment.NewLine);

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"【{i + 1:D} 回戦目】");
                // 手を決める
                HandEnum player1Hand = GetHand();
                HandEnum player2Hand = GetHand();
                Console.WriteLine($"{HandDictionary.HandDict.FirstOrDefault(f => f.Key == player1Hand).Value} vs. {HandDictionary.HandDict.FirstOrDefault(f => f.Key == player2Hand).Value}");
                // 判定
                JudgAndCount(player1Hand, player2Hand);
            }

            Console.WriteLine("【じゃんけん終了】" + Environment.NewLine);

            // 最終判定
            LastJudg();
        }


        public HandEnum GetHand()
        {
            int randNum = new Random(Random.Next()).Next(3);
            switch (randNum)
            {
                case 0:
                    return HandEnum.STONE;
                case 1:
                    return HandEnum.SCISSORS;
                case 2:
                    return HandEnum.PAPER;
            }
            return HandEnum.STONE;
        }

        private void JudgAndCount(HandEnum player1, HandEnum player2)
        {
            // P1が勝つ手
            if ((player1 == HandEnum.STONE && player2 == HandEnum.SCISSORS)
                || (player1 == HandEnum.SCISSORS && player2 == HandEnum.PAPER)
                || (player1 == HandEnum.PAPER && player2 == HandEnum.STONE)
                )
            {
                Player1WonCount++;
                Console.WriteLine("プレイヤー1が勝ちました。" + Environment.NewLine);
            }
            // P2が勝つ手
            else if ((player2 == HandEnum.STONE && player1 == HandEnum.SCISSORS)
                || (player2 == HandEnum.SCISSORS && player1 == HandEnum.PAPER)
                || (player2 == HandEnum.PAPER && player1 == HandEnum.STONE)
                )
            {
                Player2WonCount++;
                Console.WriteLine("プレイヤー2が勝ちました。" + Environment.NewLine);
            }
            // 引き分け
            else
            {
                Console.WriteLine("引き分けです。" + Environment.NewLine);
            }
        }

        private void LastJudg()
        {
            // P1の勝ち
            if (Player1WonCount > Player2WonCount)
            {
                Console.WriteLine($"{Player1WonCount}対{Player2WonCount}でプレイヤー1の勝ちです。");
            }
            // P2の勝ち
            else if (Player1WonCount < Player2WonCount)
            {
                Console.WriteLine($"{Player1WonCount}対{Player2WonCount}でプレイヤー2の勝ちです。");
            }
            // 引き分け
            else
            {
                Console.WriteLine($"{Player1WonCount}対{Player2WonCount}で引き分けです。" + Environment.NewLine);
            }
        }
    }
}
