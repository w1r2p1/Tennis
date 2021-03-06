﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tennis_Betfair.Events;
using Tennis_Betfair.TO;

namespace Tennis_Betfair
{
    public class Player : INotifyPropertyChanged
    {
        public delegate void PlayerUpdateHandler(PlayerScoreUpdEventArgs scoreUpdEventArgs);


        private string _scoreBet365;
        private string _scoreBetfair;
        private string _scoreSkyBet;


        public Player(string name, string score, TypeDBO typeDbo)
        {
            Name = name;
            switch (typeDbo)
            {
                case TypeDBO.None:
                    break;
                case TypeDBO.BetFair:
                    _scoreBetfair = score;
                    break;
                case TypeDBO.Bet365:
                    _scoreBet365 = score;
                    break;
                case TypeDBO.SkyBet:
                    _scoreSkyBet = score;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeDbo), typeDbo, null);
            }
        }

        public string Name { get; set; }

        public string ScoreBetfair1
        {
            get { return _scoreBetfair; }
            set
            {
                OnPropertyChanged();
                var integ = 0;
                if (!int.TryParse(value, out integ))
                {
                    _scoreBetfair = "Adv";
                    PlayerHanlder?.Invoke(new PlayerScoreUpdEventArgs("Adv", TypeDBO.BetFair));
                    return;
                }
                PlayerHanlder?.Invoke(new PlayerScoreUpdEventArgs(value, TypeDBO.BetFair));
                _scoreBetfair = value;
            }
        }

        public string ScoreSkyBet
        {
            get { return _scoreSkyBet; }
            set
            {
                OnPropertyChanged();
                var integ = 0;
                if (!int.TryParse(value, out integ))
                {
                    _scoreSkyBet = "Adv";
                    PlayerHanlder?.Invoke(new PlayerScoreUpdEventArgs("Adv", TypeDBO.SkyBet));
                    return;
                }
                PlayerHanlder?.Invoke(new PlayerScoreUpdEventArgs(value, TypeDBO.SkyBet));
                _scoreSkyBet = value;
            }
        }

        public string ScoreBet366
        {
            get { return _scoreBet365; }
            set
            {
                OnPropertyChanged();
                var integ = 0;
                if (!int.TryParse(value, out integ))
                {
                    _scoreBet365 = "Adv";
                    PlayerHanlder?.Invoke(new PlayerScoreUpdEventArgs("Adv", TypeDBO.Bet365));
                    return;
                }
                PlayerHanlder?.Invoke(new PlayerScoreUpdEventArgs(value, TypeDBO.Bet365));
                _scoreBet365 = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event PlayerUpdateHandler PlayerHanlder;


        public static int scoreComparator(string sr1, string sr2)
        {
            if (toIntScore(sr1) > toIntScore(sr2))
                return 1;
            if (toIntScore(sr1) == toIntScore(sr2))
                return 0;
            return -1;
        }

        public static int toIntScore(string sr1)
        {
            switch (sr1)
            {
                case "0":
                    return 0;
                case "15":
                    return 15;
                case "30":
                    return 30;
                case "40":
                    return 40;
                case "Adv":
                    return 50;
                default:
                    return 0;
            }
        }

        public override string ToString()
        {
            return "Player: " + Name + " Score(betfair): " + _scoreBetfair + " Score(365): " + _scoreBet365;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}