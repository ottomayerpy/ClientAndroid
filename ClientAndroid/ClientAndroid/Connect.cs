using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientAndroid
{
    public static class Connect
    {
        public static string IP = "192.168.0.1";
        public static int Port = 8005;
        public static string Log;

        public static string Message(string Command)
        {
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Создаем TCP сокет
                socket.Connect(new IPEndPoint(IPAddress.Parse(IP), Port)); // Подключаемся к удаленному сокету через созданный IPEndPoint(хост, порт)
                socket.Send(Encoding.Unicode.GetBytes(Command)); // Отправляем команду

                byte[] data = new byte[4]; // Буфер для приема ответа
                StringBuilder builder = new StringBuilder();

                do
                {
                    int bytes = socket.Receive(data, data.Length, 0); // Получаем число принятых байт
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes)); // Конвертируем байты в строку и записываем в StringBuilder
                }
                while (socket.Available > 0); // Проверяем есть ли доступные данные в сокете для чтения

                socket.Shutdown(SocketShutdown.Both); // Отключаем Socket
                socket.Close(); // Закрываем Socket

                if (Command == "$P" || Command == "$C" || Command == "$F")
                    return builder.ToString();
                else
                    return DateTime.Now.ToShortTimeString() + ": Ответ сервера: " + builder.ToString();
            }
            catch (Exception ex)
            {
                return DateTime.Now.ToShortTimeString() + ": Ошибка клиента: " + ex.Message;
            }
        }
    }
}