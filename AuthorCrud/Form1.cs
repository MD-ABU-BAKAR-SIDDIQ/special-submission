using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace AuthorCrud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=CYBERSPACE\SQLEXPRESS;Initial Catalog=Authersql;Integrated Security=True");
        public int AuthorId;
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetAuthorRecord();
        }

        private void GetAuthorRecord()
        {
           
            SqlCommand cmd = new SqlCommand("select * from AuthorTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            AuthorRecordDataGridView.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO AuthorTb VALUES (@AuthorName,@AuthorEmail,@BookId,@Address,@Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@AuthorName", txtAuthorName.Text);
                cmd.Parameters.AddWithValue("@AuthorEmail", txtAuthorEmail.Text);
                cmd.Parameters.AddWithValue("@BookId", txtBookId.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("New author is successfully saved in the database","Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                GetAuthorRecord();
                ResetFormControls();
            }

        }

        private bool IsValid()
        {
          if(txtAuthorName.Text == string.Empty)
            {
                MessageBox.Show("Author Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetFormControls();
        }

        private void ResetFormControls()
        {
            AuthorId = 0;
            txtAuthorName.Clear();
            txtAuthorEmail.Clear();
            txtBookId.Clear();
            txtMobile.Clear();
            txtAddress.Clear();

            txtAuthorName.Focus();
        }

        private void AuthorRecordDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AuthorId = Convert.ToInt32(AuthorRecordDataGridView.SelectedRows[0].Cells[0].Value);
            txtAuthorName.Text = AuthorRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtAuthorEmail.Text = AuthorRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtBookId.Text = AuthorRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = AuthorRecordDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtMobile.Text = AuthorRecordDataGridView.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(AuthorId > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE AuthorTb SET AuthorName = @AuthorName, AuthorEmail = @AuthorEmail,BookId = @BookId,Address = @Address,Mobile = @Mobile WHERE AuthorId = @ID ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@AuthorName", txtAuthorName.Text);
                cmd.Parameters.AddWithValue("@AuthorEmail", txtAuthorEmail.Text);
                cmd.Parameters.AddWithValue("@BookId", txtBookId.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@ID", this.AuthorId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Author information is updated successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                GetAuthorRecord();
                ResetFormControls();
            }

            else
            {
                MessageBox.Show("Please select an Author to update his/her information", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(AuthorId > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM AuthorTb  WHERE AuthorId = @ID ", con);
                cmd.CommandType = CommandType.Text;
                
               cmd.Parameters.AddWithValue("@ID", this.AuthorId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Author is deleted from the system", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetAuthorRecord();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Please select an Author to delete", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
 