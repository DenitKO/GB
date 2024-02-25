namespace Metanit
{
    class Program
    {
        static void Main()
        {
            IMessenger<Message> outlook = new EmailMessenger();
            Message message = outlook.WriteMessage("Hello World");
            Console.WriteLine(message.Text); // Email: Hello World

            IMessenger<EmailMessage> emailClient = new EmailMessenger();
            IMessenger<Message> messenger = emailClient;
            Message message2 = messenger.WriteMessage("Hi!");
            Console.WriteLine(message2.Text); // Email: Hi!

            Console.WriteLine();

            var myMessage = new Message("Yo!");
            var myMessage2 = new EmailMessenger();
            myMessage = myMessage2.WriteMessage("Еще что то");
            Console.WriteLine($"Type: {myMessage.GetType().Name}");
            Console.WriteLine(myMessage.Text);
        }
    }

    class Message
    {
        public string Text { get; set; }
        public Message(string text) => Text = text;

        /* Почему не использовать ключевое слово Required?
         * (required - ключевое слово используемое для обозначения обязательного для инициализации поля)
         * Или раз тут только хранимые данные, почему не использовать Record?
         */
    }


    class EmailMessage : Message
    {
        public EmailMessage(string text) : base(text) { }
        // странно, не думал что base может вызвать конструктор с другим названием
        // необходимо уточнить работу ключевого слова base
    }


    interface IMessenger<out T>
    {
        T WriteMessage(string text);
    }


    class EmailMessenger : IMessenger<EmailMessage>
    {
        public EmailMessage WriteMessage(string text)
        {
            return new EmailMessage($"Email: {text}");
        }
    }
}