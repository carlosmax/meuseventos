using System;
using System.Collections.Generic;
using System.Text;

namespace MeusEventos.Tests.Integration.DTO
{

    public class RegistroContaResponse
    {
        public bool sucesso { get; set; }
        public Resultado resultado { get; set; }
        public object erros { get; set; }
    }

    public class Resultado
    {
        public Result result { get; set; }
        public int id { get; set; }
        public object exception { get; set; }
        public int status { get; set; }
        public bool isCanceled { get; set; }
        public bool isCompleted { get; set; }
        public bool isCompletedSuccessfully { get; set; }
        public int creationOptions { get; set; }
        public object asyncState { get; set; }
        public bool isFaulted { get; set; }
    }

    public class Result
    {
        public string access_token { get; set; }
        public DateTime expires_in { get; set; }
        public User user { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public Claim[] claims { get; set; }
    }

    public class Claim
    {
        public string type { get; set; }
        public string value { get; set; }
    }

}
