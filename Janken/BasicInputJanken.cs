using System;
using System.Linq;

namespace Janken
{
    /// <summary>
    /// オブジェクト指向プログラミング版じゃんけん
    /// </summary>
    public class BasicInputJanken
    {
        public void Execute()
        {
            Console.WriteLine("名前を入力してください。");
            var playerName = Console.ReadLine();

            // 審判もプレーヤーも知っている事ならさらに上に出さないといけないのでやっぱりEnum参照が正解だ。
            // プレーヤーだけに定義すべきモノではない。
            Judge judge = new Judge();
            Player player1 = new Player(playerName);
            Player player2 = new Player("COM1");
            judge.StartJanken(player1, player2);
            Console.ReadLine();
        }

        /// <summary>
        /// じゃんけんの判定を表すクラス
        /// </summary>
        private class Judge
        {
            private static readonly Random Random = new Random();

            /// <summary>
            /// じゃんけんを開始する
            /// </summary>
            /// <param name="player1"></param>
            /// <param name="player2"></param>
            public void StartJanken(Player player1, Player player2)
            {
                Console.WriteLine("【じゃんけん開始】" + Environment.NewLine);

                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"【{i + 1:D} 回戦目】");
                    Player winner = JudgeJanken(player1, player2);
                    if (winner != null)
                    {
                        Console.WriteLine($"{winner.Name}が勝ちました。" + Environment.NewLine);
                        winner.NotifyResult(true);
                    }
                    else
                    {
                        Console.WriteLine("引き分けです。" + Environment.NewLine);
                    }
                }

                Console.WriteLine("【じゃんけん終了】" + Environment.NewLine);

                Player finalWinner = JudgeFinalWinner(player1, player2);
                if (finalWinner != null)
                {
                    Console.WriteLine($"{finalWinner.Name}の勝ちです。" + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("引き分けです。" + Environment.NewLine);
                }
            }

            /// <summary>
            /// 「じゃんけんポン」と声をかけ、プレイヤーの手を見て、どちらが勝ちかを判定する。
            /// </summary>
            /// <param name="player1"></param>
            /// <param name="player2"></param>
            /// <returns></returns>
            private Player JudgeJanken(Player player1, Player player2)
            {
                Console.WriteLine("手を入力してください。");
                Console.WriteLine("0 : グー");
                Console.WriteLine("1 : チョキ");
                Console.WriteLine("2 : パー");
                HandEnum player1hand = HandEnum.STONE;
                while (true)
                {
                    if (Enum.TryParse(Console.ReadLine(), out player1hand) && Enum.IsDefined(typeof(HandEnum), player1hand))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("0,1,2 のいずれかを入力してください。");
                    }
                }
                HandEnum player2hand = player2.ShowHand(new Random(Random.Next()));
                Console.WriteLine($"{HandDictionary.HandDict.FirstOrDefault(f => f.Key == player1hand).Value} vs. {HandDictionary.HandDict.FirstOrDefault(f => f.Key == player2hand).Value}");

                Player winner = null;
                if ((player1hand == HandEnum.STONE && player2hand == HandEnum.SCISSORS)
                    || (player1hand == HandEnum.SCISSORS && player2hand == HandEnum.PAPER)
                    || (player1hand == HandEnum.PAPER && player2hand == HandEnum.STONE))
                {
                    winner = player1;
                }
                else if ((player1hand == HandEnum.STONE && player2hand == HandEnum.PAPER)
                    || (player1hand == HandEnum.SCISSORS && player2hand == HandEnum.STONE)
                    || (player1hand == HandEnum.PAPER && player2hand == HandEnum.SCISSORS))
                {
                    winner = player2;
                }

                return winner;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="player1"></param>
            /// <param name="player2"></param>
            /// <returns></returns>
            private Player JudgeFinalWinner(Player player1, Player player2)
            {
                Player winner = null;
                if (player1.WinCount > player2.WinCount)
                {
                    winner = player1;
                }
                else if (player1.WinCount < player2.WinCount)
                {
                    winner = player2;
                }
                return winner;
            }
        }

        /// <summary>
        /// じゃんけんプレーヤークラス
        /// </summary>
        private class Player
        {
            public string Name { get; private set; }
            public int WinCount { get; private set; } = 0;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="Name"></param>
            public Player(string Name)
            {
                this.Name = Name;
            }

            /// <summary>
            /// じゃんけんの手を出す
            /// </summary>
            /// <returns></returns>
            public HandEnum ShowHand(Random rand)
            {
                HandEnum hand = 0;
                switch (rand.Next(3))
                {
                    case 0:
                        hand = HandEnum.STONE;
                        break;
                    case 1:
                        hand = HandEnum.SCISSORS;
                        break;
                    case 2:
                        hand = HandEnum.PAPER;
                        break;
                }
                return hand;
            }

            /// <summary>
            /// 審判から勝敗を聞く
            /// </summary>
            /// <param name="result"></param>
            public void NotifyResult(bool result)
            {
                if (result)
                {
                    WinCount++;
                }
            }
        }
    }
}
