using Asp_Core_Web_API_test.Controllers.Database;
using Asp_Core_Web_API_test.Models;
using Asp_Core_Web_API_test.Scripts;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Convert;

namespace Asp_Core_Web_API_test.Controllers {

    [ApiController]
    [Route("grades")]
    public class GradesController : Controller {

        GradeDatabaseController GradeDatabaseController { get; }

        public GradesController () {
            var csBuilder = new MySqlConnectionStringBuilder {
                Database = "unifor",
                UserID = Environment.GetEnvironmentVariable("alibabaMysql_user"),
                Server = Environment.GetEnvironmentVariable("alibabaMysql_url"),
                Password = Environment.GetEnvironmentVariable("alibabaMysql_pwd"),
                Port = ToUInt32(Environment.GetEnvironmentVariable("alibabaMysql_port"))
            };
            var mysql = new MySqlH(csBuilder);
            mysql.SetConnection();
            GradeDatabaseController = new GradeDatabaseController(mysql);
        }

        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public async Task<string> Post () {
            var grade = await HttpUtil.GetJsonBody<Grade>(Request);
            GradeDatabaseController.Upsert(grade);
            return $"Upserted: {JsonConvert.SerializeObject(grade)}";
        }

        [HttpGet]
        public Grade[] Get () {
            return GradeDatabaseController.Select().ToArray();
        }

        [HttpDelete]
        public async Task<string> Delete () {
            var grade = await HttpUtil.GetJsonBody<Grade>(Request);
            GradeDatabaseController.Delete(grade);
            return $"Grade deleted: {JsonConvert.SerializeObject(grade)}";
        }
    }
}
