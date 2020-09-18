using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TestPeristalssDB
{
    class Program
    {
        public enum eStandardKey
        {
            UUID,
            ID,
            PASSWORD,
            NickName,
            AvatarIndex,
            CashCount,

            max
        }

        static void Main(string[] args)
        {
            int action = 0;

            while(true)
            {
                Console.Clear();
                Console.WriteLine("1.추가\n2.삭제\n3.종료");
                Console.Write("명령을 선택하세요 : ");
                action = int.Parse(Console.ReadLine());

                if (action == 3)
                    break;
                else if(action == 1)
                {
                    string[] input = new string[(int)eStandardKey.max];
                    for(int n = 0; n < (int)eStandardKey.max; n++)
                    {
                        Console.Write("{0}을/를 입력하세요 : ", (eStandardKey)n);
                        input[n] = Console.ReadLine();
                    }

                    Console.WriteLine("{0} 하였습니다.", InsertStandardInfo(input) ? "성공" : "실패");
                }
                else if(action == 2)
                {
                    Console.Write("삭제할 아이디를 입력하세요 : ");
                    string delete = Console.ReadLine();

                    Console.WriteLine("{0} 하였습니다.", DeleteStandardInfo(delete) ? "성공" : "실패");
                }
            }
        }

        static bool InsertStandardInfo(string[] input)
        {
            // Server = IP; Port = number; Database = model name; Uid = account ID; Pwd = password
            using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=user;Uid=root;Pwd=kjj521566!;"))
            {
                string insertQuery = GetCommandString(input);

                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("Insert Success");
                        return true;
                    }   
                    else
                    {
                        Console.WriteLine("Insert Fail");
                        return false;
                    }   
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }

        static string GetCommandString(string[] input)
        {
            string insertQuery = string.Format("INSERT INTO standardinfo(UUID,ID,PASSWORD,NickName,AvatarIndex,CashCount)" +
                    "VALUES({0},'{1}','{2}','{3}', {4}, {5})", input[0], input[1], input[2], input[3], input[4], input[5]);

            return insertQuery;
        }

        static bool DeleteStandardInfo(string id)
        {
            using (MySqlConnection connection = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=user;Uid=root;Pwd=kjj521566!;"))
            {
                string deleteQuery = string.Format("DELETE FROM standardinfo WHERE ID = '{0}'", id);

                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(deleteQuery, connection);
                    //MySqlDataReader table = command.ExecuteReader();
                    //string a = table[""].ToString(); // Select 부분 불러오는 방법
                    if (command.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("Delete Success");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Delete Fail");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("연결 실패!!");
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
        }
    }
}
