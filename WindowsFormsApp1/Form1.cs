using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\sokol\source\repos\WindowsFormsApp1\Database1.mdf; Integrated Security=True";
            sqlConnection = new SqlConnection(ConnectionString);
            await sqlConnection.OpenAsync();
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Пассажир] (Имя, Id_рейса)VALUES(@Имя, @Id_рейса)", sqlConnection);
                command.Parameters.AddWithValue("Имя", textBox1.Text);
                command.Parameters.AddWithValue("Id_рейса", textBox2.Text);
                await command.ExecuteNonQueryAsync();
            }
            catch { MessageBox.Show("Заполните все поля корректно"); }
        }
        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            string ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\sokol\source\repos\WindowsFormsApp1\Database1.mdf; Integrated Security=True";
            sqlConnection = new SqlConnection(ConnectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReadr = null;
            SqlCommand command = new SqlCommand("SELECT * FROM Flight", sqlConnection);
            try
            {
                sqlReadr = await command.ExecuteReaderAsync();
                List<string[]> data = new List<string[]>();
                while (await sqlReadr.ReadAsync())
                {
                    data.Add(new string[3]);
                    data[data.Count - 1][0] = sqlReadr[0].ToString();
                    data[data.Count - 1][1] = sqlReadr[1].ToString();
                    data[data.Count - 1][2] = sqlReadr[2].ToString();
                    using (StreamWriter sw = new StreamWriter("abc.txt", false, System.Text.Encoding.Default))
                    {
                        for (int i = 0; i < 3; i++)
                        sw.WriteLine(data[data.Count - 1][i]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReadr != null)
                    sqlReadr.Close();
            }
        }
    }
}
