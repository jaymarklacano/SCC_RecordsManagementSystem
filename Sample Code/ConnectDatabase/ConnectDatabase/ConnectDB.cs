using MySql.Data.MySqlClient;

using System;

using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Windows.Forms;
using static ConnectDatabase.PersonalInfo;



namespace ConnectDatabase

{

    public class ConnectDB

    {

        public static MySqlConnection GetConnection()

        {

            string sql =

                "server=localhost;" +

                "port=3306;" +

                "user=root;" +

                "password=;" +

                "database=personalDB;";



            MySqlConnection conn =

                new MySqlConnection(sql);



            try

            { 
                conn.Open();
            }

            catch (MySqlException ex)

            {

                MessageBox.Show(

                    "MySQL Connection Error: " + ex.Message,

                    "Error",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Error

                );

            }



            return conn;

        }



        public static void getMaxID()

        {

            try

            {

                using (MySqlConnection conn = GetConnection())

                {

                    if (conn.State != System.Data.ConnectionState.Open)

                        return;



                    string sql =

                        "SELECT MAX(ID) FROM personal_info";



                    using (MySqlCommand cmd =

                        new MySqlCommand(sql, conn))

                    {

                        object result = cmd.ExecuteScalar();



                        if (result == null || result == DBNull.Value)

                        {

                            Instantiate.recordChecker.MaxID = 2024001;

                        }

                        else

                        {

                            Instantiate.recordChecker.MaxID =

                                Convert.ToInt32(result) + 1;

                        }

                    }

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(

                    "Database error: " + ex.Message

                );

            }

        }


        public static void saveUpdateDeleteData(

            string query,

            Dictionary<string, object> parameters)

        {

            MySqlConnection conn = GetConnection();



            try

            {

                MySqlCommand cmd = new MySqlCommand(query, conn);



                foreach (var param in parameters)

                {

                    if (param.Key != "@command")

                    {

                        cmd.Parameters.AddWithValue(param.Key, param.Value);

                    }

                }



                int result = cmd.ExecuteNonQuery();



                if (result > 0 && parameters.ContainsKey("@command"))

                {

                    object value = parameters["@command"];

                    displayInsertUpdateDelete(value);

                }

                else if (result == 0)

                {

                    MessageBox.Show("No records were affected.");

                }



                cmd.Dispose();

            }

            catch (MySqlException ex)

            {

                MessageBox.Show("Error!!\n" + ex.Message,

                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            catch (Exception ex)

            {

                MessageBox.Show("An error occurred: " + ex.Message);

            }

            finally

            {

                conn.Close();

                conn.Dispose();

            }

        }


        public static void displayData(DataGridView dataGridView)

        {

            using (MySqlConnection conn = GetConnection())

            {

                try

                {

                    MySqlDataAdapter da = new MySqlDataAdapter(

                        "SELECT * FROM personal_info", conn);



                    DataTable dt = new DataTable();



                    da.Fill(dt);



                    dataGridView.DataSource = dt;

                }

                catch (MySqlException ex)

                {

                    MessageBox.Show("Error!!\n" + ex.Message,

                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                catch (Exception ex)

                {

                    MessageBox.Show("An error occurred: " + ex.Message);

                }

                finally

                {

                    //Close the Connection

                    conn.Close();

                }

            }

        }


        public static PersonalInfo searchAndGetPersonalInfo(

            string query,

            Dictionary<string, object> parameters)

        {

            PersonalInfo personalInfo = null;



            using (MySqlConnection conn = GetConnection())

            {

                using (MySqlCommand cmd = new MySqlCommand(query, conn))

                {

                    try

                    {

                        // Add parameters to the command

                        foreach (var param in parameters)

                        {

                            cmd.Parameters.AddWithValue(param.Key, param.Value);

                        }



                        using (MySqlDataReader reader = cmd.ExecuteReader())

                        {

                            if (reader.Read())

                            {

                                // Create a new PersonalInfo object

                                personalInfo = new PersonalInfo

                                {

                                    Id = Convert.ToInt32(reader["ID"]),

                                    FirstName = reader["FirstName"].ToString(),

                                    MiddleName = reader["MiddleName"].ToString(),

                                    LastName = reader["LastName"].ToString(),

                                    Birthdate = reader["Birthdate"] != DBNull.Value

                                        ? Convert.ToDateTime(reader["Birthdate"])

                                        : DateTime.MinValue

                                };

                            }

                        }

                    }

                    catch (MySqlException ex)

                    {

                        MessageBox.Show("Error!!\n" + ex.Message,

                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }

            }



            return personalInfo;

        }





        public static void displayInsertUpdateDelete(object value)

        {

            if (value is string command)

            {

                string message = null;



                // StringComparison.OrdinalIgnoreCase

                // accepts "insert", "INSERT", "Insert", etc.

                if (command.Equals("insert",

                    StringComparison.OrdinalIgnoreCase))

                {

                    message = "Data Inserted!";

                }

                else if (command.Equals("update",

                    StringComparison.OrdinalIgnoreCase))

                {

                    message = "Data Updated!";

                }

                else if (command.Equals("delete", StringComparison.OrdinalIgnoreCase))

                {
                    message = "Data Deleted!";
                }



                if (message != null)

                {

                    MessageBox.Show(message,

                        "Information",

                        MessageBoxButtons.OK,

                        MessageBoxIcon.Information);

                }

            }

        }

        // Get Max Educ ID
        public static void getMaxEducID()
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open) return;

                    string sql = "SELECT MAX(educ_id) FROM educ_attianment";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                        {
                            Instantiate.recordChecker.MaxEducID = 1; // Or starting format like 2024001
                        }
                        else
                        {
                            Instantiate.recordChecker.MaxEducID = Convert.ToInt32(result) + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error (Educ ID): " + ex.Message);
            }
        }

        // Get Max Job ID
        public static void getMaxJobID()
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    if (conn.State != System.Data.ConnectionState.Open) return;

                    string sql = "SELECT MAX(job_id) FROM job_info";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                        {
                            Instantiate.recordChecker.MaxJobID = 1;
                        }
                        else
                        {
                            Instantiate.recordChecker.MaxJobID = Convert.ToInt32(result) + 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error (Job ID): " + ex.Message);
            }
        }


        // Fetch Education and Combined Full Name by ID (Using LEFT JOIN)
        public static void displayPersonalInfoWithEducation(DataGridView dataGridView, int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    string query = @"SELECT 
                                e.educ_id AS 'Education ID', 
                                p.ID AS 'Personal ID', 
                                TRIM(CONCAT(p.FirstName, ' ', IFNULL(p.MiddleName, ''), ' ', p.LastName)) AS 'Full Name', 
                                e.school AS 'School', 
                                e.degree AS 'Degree', 
                                e.year_graduated AS 'Year Graduated'
                             FROM personal_info p
                             LEFT JOIN educ_attianment e ON p.ID = e.personal_id
                             WHERE p.ID = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView.DataSource = dt;

                            if (dt.Rows.Count == 0)
                            {
                                dataGridView.DataSource = null;
                                MessageBox.Show("No record found with ID: " + id, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading records: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Fetch Job Info and Combined Full Name by ID (Using LEFT JOIN)
        public static void displayPersonalInfoWithJob(DataGridView dataGridView, int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    string query = @"SELECT 
                                j.job_id AS 'Job ID', 
                                p.ID AS 'Personal ID', 
                                TRIM(CONCAT(p.FirstName, ' ', IFNULL(p.MiddleName, ''), ' ', p.LastName)) AS 'Full Name', 
                                j.position AS 'Position', 
                                j.date_hired AS 'Date Hired', 
                                j.date_end AS 'Date End', 
                                j.status AS 'Status'
                             FROM personal_info p
                             LEFT JOIN job_info j ON p.ID = j.personal_id
                             WHERE p.ID = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView.DataSource = dt;

                            if (dt.Rows.Count == 0)
                            {
                                dataGridView.DataSource = null;
                                MessageBox.Show("No record found with ID: " + id, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading records: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }

}