using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; //сборка для работы с БД
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //сборка для работы с БД


namespace CDB_v1._0
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection; //переменная типа SqlConnection. Для подкл к БД. Поле класса для доступа из методов и обр. событий.

        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }

            //this.Close();
        }

        private async void Form1_Load(object sender, EventArgs e) //asunc позволяет не тормозить UI
        {            
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = "C:\Users\Alexx\source\repos\CDB v1.0\CDB v1.0\CBD.mdf"; Integrated Security = True");
            
            sqlConnection = new SqlConnection(connectionString);
            
            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;
           
            SqlCommand command = new SqlCommand("SELECT * FROM [CBDParts1]", sqlConnection);
            
            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id"]) +
                    "  " + Convert.ToString(sqlReader["Article"]) +
                    "  " + Convert.ToString(sqlReader["Name"]) +
                    "  " + Convert.ToString(sqlReader["Price"]) +
                    "  " + Convert.ToString(sqlReader["Quantity"]) +
                    "  " + Convert.ToString(sqlReader["Description"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
            
            Close();
        }

        private async void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (bunifuCustomLabel11.Visible)
            {
                bunifuCustomLabel11.Visible = false;
            }

            if (!string.IsNullOrEmpty(bunifuCustomTextbox1.Text) && !string.IsNullOrWhiteSpace(bunifuCustomTextbox1.Text) &&
                !string.IsNullOrEmpty(bunifuCustomTextbox2.Text) && !string.IsNullOrWhiteSpace(bunifuCustomTextbox2.Text) &&
                !string.IsNullOrEmpty(bunifuCustomTextbox7.Text) && !string.IsNullOrWhiteSpace(bunifuCustomTextbox7.Text) &&
                !string.IsNullOrEmpty(bunifuCustomTextbox8.Text) && !string.IsNullOrWhiteSpace(bunifuCustomTextbox8.Text))
            {
                //команды выполняются асинхронно            
                SqlCommand command = new SqlCommand("INSERT INTO [CBDParts1] (Article, Name, Price, Quanity, Description)VALUES(@Article, @Name, @Price, @Quanity, @Description", sqlConnection);

                command.Parameters.AddWithValue("Article", bunifuCustomTextbox1.Text);
                command.Parameters.AddWithValue("Name", bunifuCustomTextbox2.Text);
                command.Parameters.AddWithValue("Price", bunifuCustomTextbox7.Text);
                command.Parameters.AddWithValue("Quanity", bunifuCustomTextbox8.Text);
                command.Parameters.AddWithValue("Description", bunifuCustomTextbox9.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                bunifuCustomLabel11.Visible = true;
                bunifuCustomLabel11.Text = "All fields besides 'Description' are required";
            }
        }

        private async void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlDataReader sqlReader = null;
           
            SqlCommand command = new SqlCommand("SELECT * FROM [CBDParts1]", sqlConnection);
            
            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id"]) +
                    "  " + Convert.ToString(sqlReader["Article"]) +
                    "  " + Convert.ToString(sqlReader["Name"]) +
                    "  " + Convert.ToString(sqlReader["Price"]) +
                    "  " + Convert.ToString(sqlReader["Quantity"]) +
                    "  " + Convert.ToString(sqlReader["Description"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
            finally
            {
                if (sqlReader != null)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if (bunifuCustomLabel15.Visible)
            {
                bunifuCustomLabel15.Visible = false;
            }

            if (!string.IsNullOrEmpty(bunifuCustomTextbox5.Text) && !string.IsNullOrWhiteSpace(bunifuCustomTextbox5.Text) &&
                !string.IsNullOrEmpty(bunifuCustomTextbox12.Text) && !string.IsNullOrWhiteSpace(bunifuCustomTextbox12.Text) &&
                !string.IsNullOrEmpty(bunifuCustomTextbox11.Text) && !string.IsNullOrWhiteSpace(bunifuCustomTextbox11.Text) &&
                !string.IsNullOrEmpty(bunifuCustomTextbox10.Text) && !string.IsNullOrWhiteSpace(bunifuCustomTextbox10.Text) &&
                !string.IsNullOrEmpty(bunifuCustomTextbox4.Text) && !string.IsNullOrWhiteSpace(bunifuCustomTextbox4.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [CBDParts1] SET [Article]=@Article, [Name]=@Name, [Price]=@Price, [Quantity]=@Quantity, [Description]=@Description WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", bunifuCustomTextbox5);
                command.Parameters.AddWithValue("Article", bunifuCustomTextbox12);
                command.Parameters.AddWithValue("Name", bunifuCustomTextbox11);
                command.Parameters.AddWithValue("Price", bunifuCustomTextbox10);
                command.Parameters.AddWithValue("Quantity", bunifuCustomTextbox4);
                command.Parameters.AddWithValue("Description", bunifuCustomTextbox3);

                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(bunifuCustomTextbox5.Text) && !string.IsNullOrWhiteSpace(bunifuCustomTextbox5.Text))
            {
                bunifuCustomLabel15.Visible = true;
                bunifuCustomLabel15.Text = "ID field is required";
            }
            else
            {
                bunifuCustomLabel15.Visible = true;
                bunifuCustomLabel15.Text = "All fields besides 'Description' are required";
            }
        }

        private async void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if (bunifuCustomLabel16.Visible)
            {
                bunifuCustomLabel16.Visible = false;
            }

            if (!string.IsNullOrEmpty(bunifuCustomTextbox6.Text) && !string.IsNullOrWhiteSpace(bunifuCustomTextbox6.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [CBDParts1] WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", bunifuCustomTextbox6.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                bunifuCustomLabel16.Visible = true;
                bunifuCustomLabel16.Text = "ID field is required";
            }
        }
    }
}
