using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;

namespace TreesZaimovMS
{
    public partial class Form1 : Form
    {
        string constr = "server=10.6.0.33;" +
            "port=3306;" +
            "user=PC1;" +
            "password=1111;" +
            "database=trees_zaimov";
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnUPDATE_Click(object sender, EventArgs e)
        {
            int id = 0;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnection connect = new MySqlConnection(constr);
            if (connect.State == 0) connect.Open();
            MessageBox.Show("Connection now opened!");

            CmFillIn(cm1, connect, "otdel");
            CmFillIn(cm2, connect, "class");
            CmFillIn(cm3, connect, "razred");
            CmFillIn(cm4, connect, "family");
            CmFillIn(cm5, connect, "rod");
            CmFillIn(cm6, connect, "type");

            connect.Close();
        }
        public void CmFillIn(ComboBox cm, MySqlConnection conn, string table)
        {
            MySqlCommand query = new MySqlCommand($"Select*from {table}", conn);
            MySqlDataReader reader = query.ExecuteReader();
            List<ComboBoxItem> items = new List<ComboBoxItem>();

            while (reader.Read())
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Text = reader[1].ToString();
                item.Value = (int)reader[0];

                items.Add(item);
            }

            reader.Close();
            cm.DataSource = items;
            cm.DisplayMember = "Text";
            cm.ValueMember = "Value";
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO `trees_zaimov`.`tree`" +
                "(`name`,imageURL," +
                "otdel_id,class_id,razred_id," +
                "family_id,rod_id,vid_id," +
                "description,DateLastUpdate)" +
                "VALUES(@name,@img,@otdel,@class,@razred,@family,@rod,@vid,@info,@dateReg);";

            MySqlConnection conn = new MySqlConnection(constr);
            if (conn.State == 0) conn.Open();
            MySqlCommand query=new MySqlCommand(sql, conn);
            query.Parameters.AddWithValue("@name", txt1.Text);
            query.Parameters.AddWithValue("@otdel", cm1.SelectedValue);
            query.Parameters.AddWithValue("@class", cm2.SelectedValue);
            query.Parameters.AddWithValue("@razred", cm3.SelectedValue);
            query.Parameters.AddWithValue("@family", cm4.SelectedValue);
            query.Parameters.AddWithValue("@rod", cm5.SelectedValue);
            query.Parameters.AddWithValue("@vid", cm6.SelectedValue);
            query.Parameters.AddWithValue("@img", txt4.Text);
            query.Parameters.AddWithValue("@info", txt3.Text);
            query.Parameters.AddWithValue("@dateReg", DateAndTime.Now);

            query.ExecuteNonQuery();
            
                
        }
    }
}
