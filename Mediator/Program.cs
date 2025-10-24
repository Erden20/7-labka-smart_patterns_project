using System;
using System.Collections.Generic;

namespace MediatorPattern
{
    interface IChatMediator
    {
        void SendMessage(string message, User user);
        void AddUser(User user);
    }

    class ChatMediator : IChatMediator
    {
        private List<User> _users = new List<User>();

        public void AddUser(User user) => _users.Add(user);

        public void SendMessage(string message, User sender)
        {
            foreach (var user in _users)
            {
                if (user != sender)
                    user.Receive(message);
            }
        }
    }

    abstract class User
    {
        protected IChatMediator mediator;
        protected string name;

        public User(IChatMediator mediator, string name)
        {
            this.mediator = mediator;
            this.name = name;
        }

        public abstract void Send(string message);
        public abstract void Receive(string message);
    }

    class ConcreteUser : User
    {
        public ConcreteUser(IChatMediator mediator, string name) : base(mediator, name) { }

        public override void Send(string message)
        {
            Console.WriteLine($"{name} отправляет сообщение: {message}");
            mediator.SendMessage(message, this);
        }

        public override void Receive(string message)
        {
            Console.WriteLine($"{name} получает сообщение: {message}");
        }
    }

    class Program
    {
        static void Main()
        {
            IChatMediator mediator = new ChatMediator();

            User user1 = new ConcreteUser(mediator, "Айдар");
            User user2 = new ConcreteUser(mediator, "Дана");
            User user3 = new ConcreteUser(mediator, "Руслан");

            mediator.AddUser(user1);
            mediator.AddUser(user2);
            mediator.AddUser(user3);

            user1.Send("Привет всем!");
            Console.WriteLine();
            user2.Send("Всем хорошего дня!");
        }
    }
}
