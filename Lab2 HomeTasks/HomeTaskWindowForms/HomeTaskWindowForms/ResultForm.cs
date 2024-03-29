using CRUD_Operations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeTaskWindowForms
{
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();
            LoadCourses();
            LoadStudent();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];

            textBox1.Text = selectedRow.Cells["RegistrationNumber"].Value.ToString();
            textBox6.Text = selectedRow.Cells["Name"].Value.ToString();
            textBox8.Text = selectedRow.Cells["Session"].Value.ToString();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            DataGridViewRow selectedRow = dataGridView3.Rows[e.RowIndex];

            textBox5.Text = selectedRow.Cells["Course_Name"].Value.ToString();
            textBox7.Text = selectedRow.Cells["Semester"].Value.ToString();
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

            textBox5.Text = selectedRow.Cells["Course_Name"].Value.ToString();
            textBox7.Text = selectedRow.Cells["Semester"].Value.ToString();

            textBox1.Text = selectedRow.Cells["Student_Id"].Value.ToString();
            textBox6.Text = selectedRow.Cells["Student_Name"].Value.ToString();
            textBox8.Text = selectedRow.Cells["Session"].Value.ToString();
            textBox4.Text = selectedRow.Cells["Marks"].Value.ToString();
            textBox2.Text = selectedRow.Cells["Grade"].Value.ToString();
            textBox3.Text = selectedRow.Cells["Section"].Value.ToString();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Note: Click on the Cell to Get its contents into the text boxes ");
        }
        private void LoadStudent()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from student", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        private void LoadCourses()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Course", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView3.DataSource = dt;
        }
        private void LoadResult()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Result", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadResult();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into Result values (@Student_Id, @Student_Name, @Course_Name, @Marks, @Grade,@Section, @Semester,@Session)", con);
            if (textBox1.Text == "" || textBox6.Text == "" || textBox5.Text == "" || textBox2.Text == "" || textBox4.Text == "" || textBox3.Text == "" || textBox7.Text == "" || textBox8.Text == "")
            {
                MessageBox.Show("No Input Field can be empty!!!");
            }
            else
            {
                if (!textBox4.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Marks can only contain numbers");
                    return;
                }
                cmd.Parameters.AddWithValue("@Student_Id", textBox1.Text);
                cmd.Parameters.AddWithValue("@Student_Name", textBox6.Text);
                cmd.Parameters.AddWithValue("@Course_Name", textBox5.Text);
                cmd.Parameters.AddWithValue("@Marks", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@Grade", textBox2.Text);
                cmd.Parameters.AddWithValue("@Section", textBox3.Text);
                cmd.Parameters.AddWithValue("@Semester", ExtractIntegers(textBox7.Text));
                cmd.Parameters.AddWithValue("@Session", textBox8.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully saved");
                LoadResult();
            }

        }
        private string ExtractIntegers(string input)
        {
            // Define a regular expression pattern to match integers
            Regex regex = new Regex(@"\d+");

            // Find all matches in the input string
            MatchCollection matches = regex.Matches(input);

            // Concatenate all matched integers
            string result = "";
            foreach (Match match in matches)
            {
                result += match.Value;
            }

            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("UPDATE Result SET Marks=@Marks, Grade=@Grade,Section=@Section WHERE Student_Id=@Student_Id", con);
            if (textBox1.Text == "" || textBox6.Text == "" || textBox5.Text == "" || textBox2.Text == "" || textBox4.Text == "" || textBox3.Text == "" || textBox7.Text == "" || textBox8.Text == "")
            {
                MessageBox.Show("No Input Field can be empty!!!");
            }
            else
            {
                if (!textBox4.Text.All(char.IsDigit))
                {
                    MessageBox.Show("Marks can only contain numbers");
                    return;
                }
                cmd.Parameters.AddWithValue("@Student_Id", textBox1.Text);
                cmd.Parameters.AddWithValue("@Marks", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@Grade", textBox2.Text);
                cmd.Parameters.AddWithValue("@Section", textBox3.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Updated");
                LoadResult();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("DELETE FROM Result WHERE Student_Id=@Student_Id", con);
            if (textBox1.Text == "")
            {
                MessageBox.Show("Student_Id is missing!!!");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Student_Id", int.Parse(textBox1.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
                LoadResult();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a valid column from the ComboBox. to perform search on.");
                return;
            }
            string selectedColumn = comboBox1.SelectedItem.ToString();

            if (!string.IsNullOrEmpty(selectedColumn))
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Result WHERE {selectedColumn} = @{selectedColumn}", con);
                string value = GetTextBoxValue(selectedColumn);
                if (value == "" || value == null)
                {
                    MessageBox.Show("The TextBox on whose basis you want to search cannot be empty");
                    return;
                }
                cmd.Parameters.AddWithValue($"@{selectedColumn}", value);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Please select a valid column from the ComboBox. to perform search on.");
            }
        }
        private string GetTextBoxValue(string selectedColumn)
        {
            switch (selectedColumn)
            {
                case "Student_Id":
                    return textBox1.Text;
                case "Student_Name":
                    return textBox6.Text;
                case "Course_Name":
                    return textBox5.Text;
                case "Marks":
                    return textBox4.Text;
                case "Grade":
                    return textBox2.Text;
                case "Section":
                    return textBox3.Text;
                case "Semester":
                    return textBox7.Text;
                case "Session":
                    return textBox8.Text;
                default:
                    return "";
            }
        }
    }

}
