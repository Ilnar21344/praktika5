using Ilnar123.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BoysAPI.Controllers
{
    [Route("api/boys")]
    [ApiController]
    public class BoysController : ControllerBase
    {
        private readonly IConfiguration _config;

        public BoysController(IConfiguration config) => _config = config;
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                string connectionString = _config.GetConnectionString("DefaultConnection");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Boys";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader reader = command.ExecuteReader();

                    List<Boy> boys = new List<Boy>();
                    while (reader.Read())
                    {
                        Boy boy = new Boy
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FullName = reader["FullName"].ToString(),
                            Birthdate = Convert.ToDateTime(reader["Birthdate"])
                        };

                        boys.Add(boy);
                    }

                    return Ok(boys);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Метод POST для добавления парня
        [HttpPost]
        public IActionResult Post([FromBody] Boy boy)
        {
            try
            {
                string connectionString = _config.GetConnectionString("DefaultConnection");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Boys (FullName, Birthdate) VALUES (@FullName, @Birthdate)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FullName", boy.FullName);
                    command.Parameters.AddWithValue("@Birthdate", boy.Birthdate);
                    command.ExecuteNonQuery();

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Метод DELETE для удаления парня по идентификатору
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                string connectionString = _config.GetConnectionString("DefaultConnection");
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Boys WHERE Id = @Id";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class Boy
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
   
    