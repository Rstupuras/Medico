using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DatabaseServerTest
{
    class Program
    {

        public static IConfigurationRoot Configuration { get; set; }
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MedicoContext>();
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["SQLMedico"].ConnectionString);
            MedicoContext medicoContext = new MedicoContext(optionsBuilder.Options);

            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");

            IMedicoModel model = new MedicoModelManager(medicoContext,iPAddress,9011);
            
            
        }
    }
}

