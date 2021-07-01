using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class MySqlH {

    public string CS;
    public bool ThrowExceptions { get; set; } = false;

    public MySqlConnection Connection;

    public MySqlH(MySqlConnectionStringBuilder sb) {
        CS = sb.ToString();
    }

    public MySqlH(string cs) {
        CS = cs;
    }

    public void NonQuery(MySqlCommand command) {
        using (command) {
            Run((con) => {
                command.Connection = con;
                command.ExecuteNonQuery();
            });
        }
    }

    public void NonQuery(string a, params (string, object)[] parameters) {
        Run(a, (command) => {
            foreach (var item in parameters) {
                command.Parameters.AddWithValue(item.Item1, item.Item2);
            }
            command.ExecuteNonQuery();
        });
    }

    public void QueryR(MySqlCommand command, Action<MySqlDataReader> action) {
        Run((_con) => {
            command.Connection = _con;
            var r = command.ExecuteReader();
            action.Invoke(r);
        });
    }

    public void QueryR(string q, Action<MySqlDataReader> action) {
        QueryR(new MySqlCommand(q), action);
    }

    public void QueryRLoop(MySqlCommand c, Action<MySqlDataReader> action) {
        QueryR(c, (r) => {
            while (r.Read()) {
                action.Invoke(r);
            }
        });
    }

    public void QueryRLoop(string c, Action<MySqlDataReader> action) {
        MySqlCommand c1 = new MySqlCommand(c);
        QueryRLoop(c1, action);
    }

    public string[] GetColumns(string table_name) {
        var list = new List<string>();
        //QueryRLoop();
        return list.ToArray();
    }

    public void SetConnection () {
        if (Connection != null) return;
        Connection = new MySqlConnection(CS);
        Connection.Open();
    }

    private void Run(Action<MySqlConnection> action) {
        if (Connection == null) {
            using var connection = new MySqlConnection(CS);
            connection.Open();
            action(connection);
        } else {
            action(Connection);
        }
    }

    private void Run(string b, Action<MySqlCommand> a) {
        Run((c) => {
            using var command = c.CreateCommand(); command.CommandText = b;
            a.Invoke(command);
        });
    }

}
