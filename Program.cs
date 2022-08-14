using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database(); 
            bool isWork = true;

            while (isWork)
            {
                Console.Clear();
                Console.WriteLine("1) добавить игрока \n2) забанить игрока " +
                    "\n3) разбанить игрока \n4) удалить игрока \n5) выход");
                Console.SetCursorPosition(0, 10);
                Console.WriteLine("Список игроков:");
                database.ShowPlayers();
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();

                switch (key.KeyChar)
                {
                    case '1':
                        database.AddPlayer();
                        break;
                    case '2':
                        database.BanPlayer();
                        break;
                    case '3':
                        database.UnbanPlayer();
                        break;
                    case '4':
                        database.DeletePlayer();
                        break;
                    case '5':
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("Неверно выбрана команда. Для продолжения нажмите любую клавишу...");
                        break;
                }

                Console.ReadKey(true);
            }

        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();

        public Database()
        {
            _players.Add(new Player(22,"Max"));
            _players.Add(new Player(12,"Ivan"));
            _players.Add(new Player(72,"Ann"));
            _players.Add(new Player(62, "Ivi"));
            _players.Add(new Player(17, "Jack"));
            _players.Add(new Player(76, "Conor"));
        }

        public void AddPlayer()
        {
            Console.WriteLine("Введите никнейм игрока которого хотете добавить:");
            string userInputName = Console.ReadLine();
            Console.WriteLine("Введите уровень игрока которого хотите добавить:");
            int userInputLevel = GetNumber();
            _players.Add (new Player(userInputLevel, userInputName));
        }

        public void DeletePlayer()
        {
            Console.WriteLine("Введите ID игрока которого хотете удалить:");
            _players.RemoveAt(GetIndex());
        }

        public void BanPlayer()
        {
            Console.WriteLine("Введите ID игрока которого хотете забанить:");
            _players[GetIndex()].Ban = true;
        }

        public void UnbanPlayer()
        {
            Console.WriteLine("Введите ID игрока которого хотете разбанить:");
            _players[GetIndex()].Ban = false;
        }

        public void ShowPlayers()
        {
            foreach (Player player in _players)
            {
                player.ShowInfo();
            }

        }

        private int GetNumber()
        {
            int number = 0;
            bool success = false;

            while (success == false)
            {
                string userInput = Console.ReadLine();

                success = int.TryParse(userInput, out number);
                if (success == false)
                {
                    Console.WriteLine($"Введенное значение: '{userInput}' не явлеяется числом, поробуйте еще раз.");
                }
            }

            return number;
        }

        private int GetIndex()
        {            
            int counter = 0;
            int index = 0;           
            bool indexIsRight = false;
            bool isFound = false;

            while (indexIsRight == false)
            {
                int number = GetNumber();

                foreach (Player player in _players)
                {
                    if (player.Identifier == number)
                    {
                        index = counter;
                        isFound = true;
                        indexIsRight = true;
                    }
                    counter++;
                }

                if (isFound == false)
                {
                    Console.WriteLine("Игрока с таким ID не существует");
                    counter = 0;
                }
            }

            return index;
        }
    }

    enum PlayerType
    {
        at_Play,
        Banned,
    }

    class Player
    {
        private int _identifier;
        private int _level;
        private string _username;
        private bool _isBanned;
        private static int _identifiers;

        public Player(int level, string username, bool isBanned = false)
        {
            _identifier= ++_identifiers;
            _username = username;
            _isBanned = isBanned;
            _level = level;
        }

        public int Identifier
        {
            get
            {
                return _identifier;
            }            
        }

        public bool Ban
        {
            set { _isBanned = value; }
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Игрок с ID - {_identifier}. Никнейм - {_username} . Уровень - {_level} . Статус - {(PlayerType)Convert.ToInt32(_isBanned)}");
        }
    }
}

