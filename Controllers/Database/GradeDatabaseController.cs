using Asp_Core_Web_API_test.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Convert;

namespace Asp_Core_Web_API_test.Controllers.Database {

    public class GradeDatabaseController {

        MySqlH MySqlH { get; }

        public GradeDatabaseController(MySqlH mysql) {
            MySqlH = mysql;
        }

        public void Upsert(Grade grade) {
            MySqlH.NonQuery($"replace into grades (`subject`, `year`, `semester`, `grade`, `av`) values (@subject, @year, @semester, @grade, @av);",
                ("@subject", grade.Subject),
                ("@year", grade.Year),
                ("@semester", grade.Semester),
                ("@grade", grade.Value),
                ("@av", grade.AV));
        }

        public List<Grade> Select () {
            var list = new List<Grade>();
            MySqlH.QueryRLoop("select `subject`, `year`, `semester`, `av`, `grade` from grades order by `year`, `semester`, `av`, `subject`;", (r) => {
                var sub = r.GetString("subject");
                list.Add(new Grade() {
                   Subject = sub,
                   Year = r.GetInt32("year"),
                   Semester = r.GetBoolean("semester"),
                   AV = ToByte(r.GetInt32("av")),
                   Value = r.GetDouble("grade")
                });
            });
            return list;
        }

        public void Delete (Grade grade) {
            MySqlH.NonQuery("delete from grades where `subject` = @subject and `year` = @year and `semester` = @semester and `av` = @av;",
                ("@subject", grade.Subject),
                ("@year", grade.Year),
                ("@semester", grade.Semester),
                ("@av", grade.AV));
        }
    }
}
