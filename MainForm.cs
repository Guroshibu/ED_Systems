using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

using System.Data.SQLite;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.IO;

namespace ED_Systems
{
    public partial class MainForm : Form
    {
        //для подключения и работы с БД
        private SQLiteConnection dbConnect;
        private SQLiteCommand sqliteCmd;
        //для MySQL
        private MySqlConnection mysqlConnect;
        private MySqlCommand mysqlCmd;
        //
        //текущие координаты
        private Double currX;
        private Double currY;
        private Double currZ;
        //таблицы
        private DataTable dtSystems;
        private DataTable dtPlanets;
        private DataTable dtRings;
        private DataTable dtMaterials;
        private DataTable dtSignals;
        private DataColumn column;

        private bool loadingData;
        private bool useLocalBase;
        private int systemsRowIndex = 0;
        private int planetsRowIndex = 0;
        private int ringsRowIndex = 0;

        public MainForm()
        {
            InitializeComponent();

            dgvSystems.AutoGenerateColumns = false;
            dgvPlanets.AutoGenerateColumns = false;
            dgvRAW.AutoGenerateColumns = false;
            dgvSignals.AutoGenerateColumns = false;
            dgvRings.AutoGenerateColumns = false;
        }
        private void ConnectDB()
        {
            try
            {
                dbConnect = new SQLiteConnection("Data Source=data.db;Version=3;");
                dbConnect.Open();
                sqliteCmd = new SQLiteCommand();
                sqliteCmd.Connection = dbConnect;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("SQLite Error:\r\n" + ex.Message);
            }
        }
        private void DisconnectDB()
        {
            try
            {
                dbConnect.Close();
            }
            catch { };
        }
        private void ConnectMySQL()
        {
            // Connection String.
            string connString = "Server=" + Properties.Settings.Default.MySqlHost + 
                                ";Database=" + Properties.Settings.Default.MySqlDb +
                                ";port=" + Properties.Settings.Default.MySqlPort + 
                                ";User Id=" + Properties.Settings.Default.MySqlUser + 
                                ";password=" + Properties.Settings.Default.MySqlPwd;
            try
            {
                mysqlConnect = new MySqlConnection(connString);
                mysqlConnect.Open();
                mysqlCmd = new MySqlCommand();
                mysqlCmd.Connection = mysqlConnect;
                if (Properties.Settings.Default.MaxDistanse > 1000)
                {
                    Properties.Settings.Default.MaxDistanse = 1000;
                    Properties.Settings.Default.Save();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("MySQL Error:\r\n" + ex.Message);
                UseLocalBase();
                Application.DoEvents();
            }
        }
        private void DisconnectMySQL()
        {
            try
            {
                mysqlConnect.Close();
                mysqlConnect.Dispose();
            }
            catch { };
        }
        //SqLite запросы
        private DataTable SqliteSelect(SQLiteCommand cmd)
        {
            DataTable dt = new DataTable();
            SQLiteDataAdapter adapter;
            try
            {
                adapter = new SQLiteDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("SQLite Error:\r\n" + ex.Message + "\r\n " + cmd.CommandText);
            }
            return dt;
        }
        private void SqliteInsert(SQLiteCommand cmd)
        {
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("SQLite Error:\r\n" + ex.Message + "\r\n " + cmd.CommandText);
            }
        }
        private void SqliteUpdate(SQLiteCommand cmd)
        {
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("SQLite Error:\r\n" + ex.Message + "\r\n " + cmd.CommandText);
            }
        }
        private void SqliteDelete(SQLiteCommand cmd)
        {
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("SQLite Error:\r\n" + ex.Message + "\r\n " + cmd.CommandText);
            }
        }
        private long SqliteCount(SQLiteCommand cmd)
        {
            object count = 0;
            try
            {
                count = cmd.ExecuteScalar();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("SQLite Error:\r\n" + ex.Message + "\r\n " + cmd.CommandText);
            }
            return Convert.ToInt64(count);
        }
        //MySql запросы
        private DataTable MySqlSelect(MySqlCommand cmd)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter;
            try
            {
                adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\r\n " + cmd.CommandText);
                UseLocalBase();
            }
            return dt;
        }
        private void MySqlInsert(MySqlCommand cmd)
        {
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error:\r\n" + ex.Message + "\r\n " + cmd.CommandText);
                UseLocalBase();
            }
        }
        private void MySqlUpdate(MySqlCommand cmd)
        {
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error:\r\n" + ex.Message + "\r\n " + cmd.CommandText);
                UseLocalBase();
            }
        }
        private void MySqlDelete(MySqlCommand cmd)
        {
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error:\r\n" + ex.Message + "\r\n " + cmd.CommandText);
                UseLocalBase();
            }
        }
        private long MySqlCount(MySqlCommand cmd)
        {
            object count = 0;
            try
            {
                count = cmd.ExecuteScalar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error:\r\n" + ex.Message + "\r\n " + cmd.CommandText);
                UseLocalBase();
            }
            return Convert.ToInt64(count);
        }
        private void UseLocalBase()
        {
            useLocalBase = true;
            useLocalBaseToolStripMenuItem.Checked = useLocalBase;
            useLocalBaseToolStripMenuItem.BackColor = Color.LightGreen;
        }
        private void LoadSystems(string filter)
        {
            double dx;
            double dy;
            double dz;
            double radius = Properties.Settings.Default.MaxDistanse;

            DataTable dt;

            loadingData = true;


            if(!useLocalBase) ConnectMySQL();

            if (filter != "" && rbRaw.Checked)
            {
                if (!useLocalBase)
                {
                    mysqlCmd.CommandText = @"SELECT DISTINCT t1.*
                                            FROM Systems t1, Materials t2
                                            WHERE t1.SysAddr=t2.SysAddr AND t2.Name = @Name AND
                                                  SQRT(POW((t1.StarPosX - @currX),2) + 
                                                       POW((t1.StarPosY - @currY),2) +
                                                       POW((t1.StarPosZ - @currZ),2)) < @radius";
                    mysqlCmd.Parameters.Clear();
                    mysqlCmd.Parameters.Add("@currX", MySqlDbType.Double).Value = currX;
                    mysqlCmd.Parameters.Add("@currY", MySqlDbType.Double).Value = currY;
                    mysqlCmd.Parameters.Add("@currZ", MySqlDbType.Double).Value = currZ;
                    mysqlCmd.Parameters.Add("@radius", MySqlDbType.Double).Value = radius;
                    mysqlCmd.Parameters.Add("@Name", MySqlDbType.String).Value = filter;
                    dtSystems = MySqlSelect(mysqlCmd);
                }
                else
                {
                    sqliteCmd.CommandText = @"SELECT DISTINCT t1.*
                                            FROM Systems t1, Materials t2
                                            WHERE t1.SysAddr=t2.SysAddr AND t2.Name = @Name AND
                                                  (t1.StarPosX - @currX) * (t1.StarPosX - @currX) + 
                                                  (t1.StarPosY - @currY) * (t1.StarPosY - @currY) +
		                                          (t1.StarPosZ - @currZ) * (t1.StarPosZ - @currZ) < @radius * @radius";
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@currX", DbType.Double).Value = currX;
                    sqliteCmd.Parameters.Add("@currY", DbType.Double).Value = currY;
                    sqliteCmd.Parameters.Add("@currZ", DbType.Double).Value = currZ;
                    sqliteCmd.Parameters.Add("@radius", DbType.Double).Value = radius;
                    sqliteCmd.Parameters.Add("@Name", DbType.String).Value = filter;
                    dtSystems = SqliteSelect(sqliteCmd);
                }
            }
            else if (filter != "" && rbSignals.Checked)
            {
                if (!useLocalBase)
                {
                    mysqlCmd.CommandText = @"SELECT DISTINCT t1.*
                                            FROM Systems t1, Signals t2
                                            WHERE t1.SysAddr=t2.SysAddr AND t2.Type = @Type AND
                                                  SQRT(POW((t1.StarPosX - @currX),2) + 
                                                       POW((t1.StarPosY - @currY),2) +
                                                       POW((t1.StarPosZ - @currZ),2)) < @radius";
                    mysqlCmd.Parameters.Clear();
                    mysqlCmd.Parameters.Add("@currX", MySqlDbType.Double).Value = currX;
                    mysqlCmd.Parameters.Add("@currY", MySqlDbType.Double).Value = currY;
                    mysqlCmd.Parameters.Add("@currZ", MySqlDbType.Double).Value = currZ;
                    mysqlCmd.Parameters.Add("@radius", MySqlDbType.Double).Value = radius;
                    mysqlCmd.Parameters.Add("@Type", MySqlDbType.String).Value = filter;
                    dtSystems = MySqlSelect(mysqlCmd);
                }
                else
                {
                    sqliteCmd.CommandText = @"SELECT DISTINCT t1.*
                                            FROM Systems t1, Signals t2
                                            WHERE t1.SysAddr=t2.SysAddr AND t2.Type = @Type AND
                                                  (t1.StarPosX - @currX) * (t1.StarPosX - @currX) + 
                                                  (t1.StarPosY - @currY) * (t1.StarPosY - @currY) +
		                                          (t1.StarPosZ - @currZ) * (t1.StarPosZ - @currZ) < @radius * @radius";
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@currX", DbType.Double).Value = currX;
                    sqliteCmd.Parameters.Add("@currY", DbType.Double).Value = currY;
                    sqliteCmd.Parameters.Add("@currZ", DbType.Double).Value = currZ;
                    sqliteCmd.Parameters.Add("@radius", DbType.Double).Value = radius;
                    sqliteCmd.Parameters.Add("@Type", DbType.String).Value = filter;
                    dtSystems = SqliteSelect(sqliteCmd);
                }
            }
            else
            {
                if (!useLocalBase)
                {
                    mysqlCmd.CommandText = @"SELECT *
                                            FROM Systems
                                            WHERE SQRT(POW((StarPosX - @currX),2) + 
                                                       POW((StarPosY - @currY),2) +
                                                       POW((StarPosZ - @currZ),2)) < @radius";
                    mysqlCmd.Parameters.Clear();
                    mysqlCmd.Parameters.Add("@currX", MySqlDbType.Double).Value = currX;
                    mysqlCmd.Parameters.Add("@currY", MySqlDbType.Double).Value = currY;
                    mysqlCmd.Parameters.Add("@currZ", MySqlDbType.Double).Value = currZ;
                    mysqlCmd.Parameters.Add("@radius", MySqlDbType.Double).Value = radius;

                    dtSystems = MySqlSelect(mysqlCmd);
                }
                else
                {
                    sqliteCmd.CommandText = @"SELECT *
                                            FROM Systems
                                            WHERE (StarPosX - @currX) * (StarPosX - @currX) + 
                                                  (StarPosY - @currY) * (StarPosY - @currY) +
		                                          (StarPosZ - @currZ) * (StarPosZ - @currZ) < @radius * @radius";
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@currX", DbType.Double).Value = currX;
                    sqliteCmd.Parameters.Add("@currY", DbType.Double).Value = currY;
                    sqliteCmd.Parameters.Add("@currZ", DbType.Double).Value = currZ;
                    sqliteCmd.Parameters.Add("@radius", DbType.Double).Value = radius;
                    dtSystems = SqliteSelect(sqliteCmd);
                }
            }
            //колонки локальных данных
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Double"),
                ColumnName = "Distance",
                AutoIncrement = false,
                Caption = "",
                ReadOnly = false,
                Unique = false
            };
            dtSystems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "LastVisit",
                AutoIncrement = false,
                Caption = "",
                ReadOnly = false,
                Unique = false
            };
            dtSystems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Comment",
                AutoIncrement = false,
                Caption = "",
                ReadOnly = false,
                Unique = false
            };
            dtSystems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "HasImg",
                AutoIncrement = false,
                Caption = "",
                ReadOnly = false,
                Unique = false
            };
            dtSystems.Columns.Add(column);

            if (dtSystems.Rows.Count == 0) return;

            prbUpload.Minimum = 1;
            prbUpload.Maximum = dtSystems.Rows.Count;
            prbUpload.Value = 1;
            prbUpload.Step = 1;
            prbUpload.Visible = true;

            foreach (DataRow row in dtSystems.Rows)
            {
                //расчет расстояния
                dx = Convert.ToDouble(row["StarPosX"]) - currX;
                dy = Convert.ToDouble(row["StarPosY"]) - currY;
                dz = Convert.ToDouble(row["StarPosZ"]) - currZ;

                row["Distance"] = Math.Round(Math.Sqrt(dx * dx + dy * dy + dz * dz), 2);
                //заполнение локальными данными
                sqliteCmd.CommandText = @"SELECT *
                                          FROM SystemsLocal
                                          WHERE SysAddr = @SysAddr";
                {
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = Convert.ToInt64(row["SysAddr"]);
                }
                dt = SqliteSelect(sqliteCmd);
                if (dt.Rows.Count == 0)
                {
                    row["LastVisit"] = "";
                    row["Comment"] = "";
                    row["HasImg"] = 0;
                }
                else
                {
                    row["LastVisit"] = dt.Rows[0]["LastVisit"].ToString();
                    row["Comment"] = dt.Rows[0]["Comment"].ToString();
                    row["HasImg"] = dt.Rows[0]["HasImg"].ToString();
                }

                prbUpload.PerformStep();
                Application.DoEvents();
            }
            prbUpload.Visible = false;

            dgvSystems.DataSource = dtSystems;

            dgvSystems.Sort(dgvSystems.Columns["sysDistance"], ListSortDirection.Ascending);
            dgvSystems.CurrentCell = dgvSystems.Rows[0].Cells["sysStarSystem"];

            dgvSystems.Update();

            lblSystems.Text = "Systems (" + dtSystems.Rows.Count.ToString() + "), Max distance " + radius.ToString() + " ly";
            loadingData = false;

            LoadPlanets(Convert.ToUInt64(dgvSystems.CurrentRow.Cells["sysSystemAddress"].Value), filter);
            if (!useLocalBase) DisconnectMySQL();

            
        }
        private void LoadPlanets(UInt64 sysAddr, string filter)
        {
            DataTable dt;
            UInt64 planetID;

            if (filter != "" && rbRaw.Checked)
            {
                if (!useLocalBase)
                {
                    mysqlCmd.CommandText = @"SELECT t1.*
                                            FROM Planets t1, Materials t2
                                            WHERE t1.SysAddr = @SysAddr AND
                                                  t1.SysAddr = t2.SysAddr AND
                                                  t1.PlanetID = t2.PlanetID AND
                                                  t2.Name = @Name";
                    {
                        mysqlCmd.Parameters.Clear();
                        mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = sysAddr;
                        mysqlCmd.Parameters.Add("@Name", MySqlDbType.String).Value = filter;
                    }
                    dtPlanets = MySqlSelect(mysqlCmd);
                }
                else
                {
                    sqliteCmd.CommandText = @"SELECT t1.*
                                            FROM Planets t1, Materials t2
                                            WHERE t1.SysAddr = @SysAddr AND
                                                  t1.SysAddr = t2.SysAddr AND
                                                  t1.PlanetID = t2.PlanetID AND
                                                  t2.Name = @Name";
                    {
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                        sqliteCmd.Parameters.Add("@Name", DbType.String).Value = filter;
                    }
                    dtPlanets = SqliteSelect(sqliteCmd);
                }
            }
            else if (filter != "" && rbSignals.Checked)
            {
                if (!useLocalBase)
                {
                    mysqlCmd.CommandText = @"SELECT t1.*
                                            FROM Planets t1, Signals t2
                                            WHERE t1.SysAddr = @SysAddr AND
                                                  t1.SysAddr = t2.SysAddr AND
                                                  t1.PlanetID = t2.PlanetID AND
                                                  t2.Type = @Type";
                    {
                        mysqlCmd.Parameters.Clear();
                        mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = sysAddr;
                        mysqlCmd.Parameters.Add("@Type", MySqlDbType.String).Value = filter;
                    }
                    dtPlanets = MySqlSelect(mysqlCmd);
                }
                else
                {
                    sqliteCmd.CommandText = @"SELECT t1.*
                                            FROM Planets t1, Signals t2
                                            WHERE t1.SysAddr = @SysAddr AND
                                                  t1.SysAddr = t2.SysAddr AND
                                                  t1.PlanetID = t2.PlanetID AND
                                                  t2.Type = @Type";
                    {
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                        sqliteCmd.Parameters.Add("@Type", DbType.String).Value = filter;
                    }
                    dtPlanets = SqliteSelect(sqliteCmd);
                }
            }
            else
            {
                if (!useLocalBase)
                {
                    mysqlCmd.CommandText = @"SELECT *
                                            FROM Planets
                                            WHERE SysAddr = @SysAddr";
                    {
                        mysqlCmd.Parameters.Clear();
                        mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = sysAddr;
                    }
                    dtPlanets = MySqlSelect(mysqlCmd);
                }
                else
                {
                    sqliteCmd.CommandText = @"SELECT *
                                            FROM Planets
                                            WHERE SysAddr = @SysAddr";
                    {
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                    }
                    dtPlanets = SqliteSelect(sqliteCmd);
                }
            }
            //заполнение локальных данных
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "HasImg",
                AutoIncrement = false,
                Caption = "",
                ReadOnly = false,
                Unique = false
            };
            dtPlanets.Columns.Add(column);
            foreach (DataRow row in dtPlanets.Rows)
            {
                sqliteCmd.CommandText = @"SELECT *
                                            FROM PlanetsLocal
                                            WHERE SysAddr = @SysAddr AND
                                                  PlanetID = @PlanetID";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = Convert.ToUInt64(row["PlanetID"]);
                dt = SqliteSelect(sqliteCmd);
                if (dt.Rows.Count == 0)
                {
                    row["HasImg"] = 0;
                }
                else
                {
                    row["HasImg"] = dt.Rows[0]["HasImg"].ToString();
                }
            }

            dgvPlanets.DataSource = dtPlanets;

            dgvPlanets.Sort(dgvPlanets.Columns["plDistance"], ListSortDirection.Ascending);
            dgvPlanets.CurrentCell = dgvPlanets.Rows[0].Cells["plPlanetName"];
            dgvPlanets.Update();

            lblPlanets.Text = "Planets (" + dtPlanets.Rows.Count.ToString() + ")";

            if (dtPlanets.Rows.Count == 0)
            {
                sysAddr = 0;
                planetID = 0;
            }
            else
            {
                sysAddr = Convert.ToUInt64(dgvPlanets.CurrentRow.Cells["plSystemAddress"].Value);
                planetID = Convert.ToUInt64(dgvPlanets.CurrentRow.Cells["plPlanetID"].Value);
            }
            LoadMaterials(sysAddr, planetID);
            LoadRings(sysAddr, planetID);
        }
        private void LoadMaterials(UInt64 sysAddr, UInt64 planetID)
        {
            DataTable dt;


            if (!useLocalBase)
            {
                mysqlCmd.CommandText = @"SELECT *
                                       FROM Materials
                                       WHERE SysAddr = @SysAddr AND
                                             PlanetID = @PlanetID";
                mysqlCmd.Parameters.Clear();
                mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = sysAddr;
                mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                dtMaterials = MySqlSelect(mysqlCmd);
            }
            else
            {
                sqliteCmd.CommandText = @"SELECT *
                                       FROM Materials
                                       WHERE SysAddr = @SysAddr AND
                                             PlanetID = @PlanetID";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = planetID;
                dtMaterials = SqliteSelect(sqliteCmd);
            }
            //добавляем локализацию
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "NameLocalised",
                AutoIncrement = false,
                Caption = "",
                ReadOnly = false,
                Unique = false
            };
            dtMaterials.Columns.Add(column);
            foreach (DataRow row in dtMaterials.Rows)
            {
                sqliteCmd.CommandText = @"SELECT NameLocalised
                                       FROM Localise
                                       WHERE Name = @Name";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@Name", DbType.String).Value = row["Name"].ToString();
                dt = SqliteSelect(sqliteCmd);
                row["NameLocalised"] = dt.Rows[0]["NameLocalised"].ToString();
            }

            dgvRAW.DataSource = dtMaterials;

            dgvRAW.Sort(dgvRAW.Columns["rawName"], ListSortDirection.Ascending);
            dgvRAW.CurrentCell = dgvRAW.Rows[0].Cells["rawName"];
            dgvRAW.Update();

        }
        private void LoadRings(UInt64 sysAddr, UInt64 planetID)
        {
            string ringName;

            if (!useLocalBase)
            {
                mysqlCmd.CommandText = @"SELECT *
                                       FROM Rings
                                       WHERE SysAddr = @SysAddr AND
                                             PlanetID = @PlanetID";
                mysqlCmd.Parameters.Clear();
                mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = sysAddr;
                mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                dtRings = MySqlSelect(mysqlCmd);
            }
            else
            {
                sqliteCmd.CommandText = @"SELECT *
                                       FROM Rings
                                       WHERE SysAddr = @SysAddr AND
                                             PlanetID = @PlanetID";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = planetID;
                dtRings = SqliteSelect(sqliteCmd);
            }

  
            dgvRings.DataSource = dtRings;
            dgvRings.Sort(dgvRings.Columns["rgRingName"], ListSortDirection.Ascending);
            dgvRings.CurrentCell = dgvRings.Rows[0].Cells["rgRingName"];
            dgvRings.Update();

            if (dtRings.Rows.Count == 0)
            {
                sysAddr = 0;
                planetID = 0;
                ringName = "";
            }
            else
            {
                sysAddr = Convert.ToUInt64(dgvRings.CurrentRow.Cells["rgSystemAddress"].Value);
                planetID = Convert.ToUInt64(dgvRings.CurrentRow.Cells["rgPlanetID"].Value);
                ringName = dgvRings.CurrentRow.Cells["rgRingName"].Value.ToString();
            }

            LoadSignals(sysAddr, planetID, ringName);
        }
        private void LoadSignals(UInt64 sysAddr, UInt64 planetID, string ringName)
        {
            DataTable dt;

            ringName = ringName.Trim();

            if (!useLocalBase)
            {
                mysqlCmd.CommandText = @"SELECT *
                                       FROM Signals
                                       WHERE SysAddr = @SysAddr AND
                                             PlanetID = @PlanetID AND
                                             RingName = @RingName";
                mysqlCmd.Parameters.Clear();
                mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = sysAddr;
                mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                mysqlCmd.Parameters.Add("@RingName", MySqlDbType.String).Value = ringName;
                dtSignals = MySqlSelect(mysqlCmd);
            }
            else
            {
                sqliteCmd.CommandText = @"SELECT *
                                       FROM Signals
                                       WHERE SysAddr = @SysAddr AND
                                             PlanetID = @PlanetID AND
                                             RingName = @RingName";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = planetID;
                sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
                dtSignals = SqliteSelect(sqliteCmd);
            }
            //заполнение локальных данных
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Comment",
                AutoIncrement = false,
                Caption = "",
                ReadOnly = false,
                Unique = false
            };
            dtSignals.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "HasImg",
                AutoIncrement = false,
                Caption = "",
                ReadOnly = false,
                Unique = false
            };
            dtSignals.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "NameLocalised",
                AutoIncrement = false,
                Caption = "",
                ReadOnly = false,
                Unique = false
            };
            dtSignals.Columns.Add(column);
            foreach (DataRow row in dtSignals.Rows)
            {
                sqliteCmd.CommandText = @"SELECT *
                                       FROM SignalsLocal
                                       WHERE SysAddr = @SysAddr AND
                                             PlanetID = @PlanetID AND
                                             RingName = @RingName AND
                                             Type = @Type";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = planetID;
                sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
                sqliteCmd.Parameters.Add("@Type", DbType.String).Value = row["Type"].ToString();
                dt = SqliteSelect(sqliteCmd);
                row["Comment"] = dt.Rows[0]["Comment"];
                row["HasImg"] = dt.Rows[0]["HasImg"];
                //добавляем локализацию
                sqliteCmd.CommandText = @"SELECT NameLocalised
                                       FROM Localise
                                       WHERE Name = @Name";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@Name", DbType.String).Value = row["Type"].ToString();
                dt = SqliteSelect(sqliteCmd);
                row["NameLocalised"] = dt.Rows[0]["NameLocalised"].ToString();

            }

            dgvSignals.DataSource = dtSignals;
            dgvSignals.Sort(dgvSignals.Columns["sigType"], ListSortDirection.Ascending);
            dgvSignals.CurrentCell = dgvSignals.Rows[0].Cells["sigType"];
            dgvSignals.Update();

        }
        private void GetLogData(string path)
        {
            FSDJump fsdJump = new FSDJump();
            CarrierJump carrierJump = new CarrierJump();
            Scan scan = new Scan();
            SAASignalsFound saaSignals = new SAASignalsFound();

            DataTable dt = new DataTable();

            UInt64 planetID;
            UInt64 oldPlanetID;
            string systemName;
            string planetName;
            int landable;
            string ringName;

            int linesCount;

            List<string> fileLines = new List<string>();

            if (!File.Exists(path)) return;

            FileInfo log = new FileInfo(path);
            StreamReader stream = new StreamReader(log.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            while (!stream.EndOfStream)
            {
                fileLines.Add(stream.ReadLine());
            }
            stream.Close();
            log = null;
            linesCount = fileLines.Count;

            prbUpload.Visible = true;
            prbUpload.Minimum = 1;
            prbUpload.Maximum = linesCount;
            prbUpload.Value = 1;
            prbUpload.Step = 1;

            loadingData = true;
            if (!useLocalBase) ConnectMySQL();
            foreach (string line in fileLines)
            {
                //"event":"FSDJump"
                if (line.IndexOf("\"event\":\"FSDJump\"") >= 0)
                {
                    fsdJump = JsonConvert.DeserializeObject<FSDJump>(line);

                    //удаленная база
                    if (!useLocalBase)
                    {
                        mysqlCmd.CommandText = @"SELECT COUNT(*)
                                                 FROM Systems
                                                 WHERE SysAddr = @SysAddr";
                        mysqlCmd.Parameters.Clear();
                        mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = fsdJump.SystemAddress;

                        if (MySqlCount(mysqlCmd) == 0)
                        {
                            //если не найден добавляем
                            mysqlCmd.CommandText = @"INSERT INTO Systems (SysAddr, SystemName, StarPosX, StarPosY, StarPosZ)
                                                    VALUES (@SysAddr, @SystemName, @StarPosX, @StarPosY, @StarPosZ)";
                            mysqlCmd.Parameters.Clear();
                            mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = fsdJump.SystemAddress;
                            mysqlCmd.Parameters.Add("@SystemName", MySqlDbType.String).Value = fsdJump.StarSystem;
                            mysqlCmd.Parameters.Add("@StarPosX", MySqlDbType.Double).Value = fsdJump.StarPos[0];
                            mysqlCmd.Parameters.Add("@StarPosY", MySqlDbType.Double).Value = fsdJump.StarPos[1];
                            mysqlCmd.Parameters.Add("@StarPosZ", MySqlDbType.Double).Value = fsdJump.StarPos[2];
                            MySqlInsert(mysqlCmd);
                        }
                    }
                    //локальная база
                    sqliteCmd.CommandText = @"SELECT COUNT(*)
                                                 FROM Systems
                                                 WHERE SysAddr = @SysAddr";
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = fsdJump.SystemAddress;

                    if (SqliteCount(sqliteCmd) == 0)
                    {
                        //если не найден добавляем
                        sqliteCmd.CommandText = @"INSERT INTO Systems (SysAddr, SystemName, StarPosX, StarPosY, StarPosZ)
                                                    VALUES (@SysAddr, @SystemName, @StarPosX, @StarPosY, @StarPosZ)";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = fsdJump.SystemAddress;
                        sqliteCmd.Parameters.Add("@SystemName", DbType.String).Value = fsdJump.StarSystem;
                        sqliteCmd.Parameters.Add("@StarPosX", DbType.Double).Value = fsdJump.StarPos[0];
                        sqliteCmd.Parameters.Add("@StarPosY", DbType.Double).Value = fsdJump.StarPos[1];
                        sqliteCmd.Parameters.Add("@StarPosZ", DbType.Double).Value = fsdJump.StarPos[2];
                        SqliteInsert(sqliteCmd);
                        //доп. данные (хранятся только локально)
                        sqliteCmd.CommandText = @"INSERT INTO SystemsLocal (SysAddr, LastVisit, Comment, HasImg)
                                                    VALUES (@SysAddr, @LastVisit, @Comment, @HasImg)";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = fsdJump.SystemAddress;
                        sqliteCmd.Parameters.Add("@LastVisit", DbType.String).Value = fsdJump.timestamp.ToString("d");
                        sqliteCmd.Parameters.Add("@Comment", DbType.String).Value = "";
                        sqliteCmd.Parameters.Add("@HasImg", DbType.Int32).Value = 0;
                        SqliteInsert(sqliteCmd);
                    }
                    else
                    {
                        sqliteCmd.CommandText = @"UPDATE SystemsLocal
                                                  SET LastVisit = @LastVisit
                                                  WHERE SysAddr = @SysAddr";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = fsdJump.SystemAddress;
                        sqliteCmd.Parameters.Add("@LastVisit", DbType.String).Value = fsdJump.timestamp.ToString("d");
                        SqliteUpdate(sqliteCmd);
                    }
                    //устанавливаем систему как текущую
                    currX = Convert.ToDouble(fsdJump.StarPos[0]);
                    currY = Convert.ToDouble(fsdJump.StarPos[1]);
                    currZ = Convert.ToDouble(fsdJump.StarPos[2]);
                    lblCurrentSystem.Text = fsdJump.StarSystem;
                    //последняя загруженная система
                    Properties.Settings.Default.LastSystemAddress = fsdJump.SystemAddress;
                    Properties.Settings.Default.LastSystemName = fsdJump.StarSystem;
                    Properties.Settings.Default.Save();
                    //текущая система
                    sqliteCmd.CommandText = @"UPDATE CurrentCoordinates
                            SET System = @System, X = @X, Y = @Y, Z = @Z
                            WHERE rowid = @rowid";
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@System", DbType.String).Value = fsdJump.StarSystem;
                    sqliteCmd.Parameters.Add("@X", DbType.Double).Value = fsdJump.StarPos[0];
                    sqliteCmd.Parameters.Add("@Y", DbType.Double).Value = fsdJump.StarPos[1];
                    sqliteCmd.Parameters.Add("@Z", DbType.Double).Value = fsdJump.StarPos[2];
                    sqliteCmd.Parameters.Add("@rowid", DbType.Int32).Value = 1;
                    SqliteUpdate(sqliteCmd);
                }
                // "event":"CarrierJump"
                if (line.IndexOf("\"event\":\"CarrierJump\"") >= 0)
                {
                    carrierJump = JsonConvert.DeserializeObject<CarrierJump>(line);

                    //удаленная база
                    if (!useLocalBase)
                    {
                        mysqlCmd.CommandText = @"SELECT COUNT(*)
                                                 FROM Systems
                                                 WHERE SysAddr = @SysAddr";
                        mysqlCmd.Parameters.Clear();
                        mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = carrierJump.SystemAddress;

                        if (MySqlCount(mysqlCmd) == 0)
                        {
                            //если не найден добавляем
                            mysqlCmd.CommandText = @"INSERT INTO Systems (SysAddr, SystemName, StarPosX, StarPosY, StarPosZ)
                                                    VALUES (@SysAddr, @SystemName, @StarPosX, @StarPosY, @StarPosZ)";
                            mysqlCmd.Parameters.Clear();
                            mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = carrierJump.SystemAddress;
                            mysqlCmd.Parameters.Add("@SystemName", MySqlDbType.String).Value = carrierJump.StarSystem;
                            mysqlCmd.Parameters.Add("@StarPosX", MySqlDbType.Double).Value = carrierJump.StarPos[0];
                            mysqlCmd.Parameters.Add("@StarPosY", MySqlDbType.Double).Value = carrierJump.StarPos[1];
                            mysqlCmd.Parameters.Add("@StarPosZ", MySqlDbType.Double).Value = carrierJump.StarPos[2];
                            MySqlInsert(mysqlCmd);
                        }
                    }
                    //локальная база
                    sqliteCmd.CommandText = @"SELECT COUNT(*)
                                                 FROM Systems
                                                 WHERE SysAddr = @SysAddr";
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = carrierJump.SystemAddress;

                    if (SqliteCount(sqliteCmd) == 0)
                    {
                        //если не найден добавляем
                        sqliteCmd.CommandText = @"INSERT INTO Systems (SysAddr, SystemName, StarPosX, StarPosY, StarPosZ)
                                                    VALUES (@SysAddr, @SystemName, @StarPosX, @StarPosY, @StarPosZ)";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = carrierJump.SystemAddress;
                        sqliteCmd.Parameters.Add("@SystemName", DbType.String).Value = carrierJump.StarSystem;
                        sqliteCmd.Parameters.Add("@StarPosX", DbType.Double).Value = carrierJump.StarPos[0];
                        sqliteCmd.Parameters.Add("@StarPosY", DbType.Double).Value = carrierJump.StarPos[1];
                        sqliteCmd.Parameters.Add("@StarPosZ", DbType.Double).Value = carrierJump.StarPos[2];
                        SqliteInsert(sqliteCmd);
                        //доп. данные (хранятся только локально)
                        sqliteCmd.CommandText = @"INSERT INTO SystemsLocal (SysAddr, LastVisit, Comment, HasImg)
                                                    VALUES (@SysAddr, @LastVisit, @Comment, @HasImg)";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = carrierJump.SystemAddress;
                        sqliteCmd.Parameters.Add("@LastVisit", DbType.String).Value = carrierJump.timestamp.ToString("d");
                        sqliteCmd.Parameters.Add("@Comment", DbType.String).Value = "";
                        sqliteCmd.Parameters.Add("@HasImg", DbType.Int32).Value = 0;
                        SqliteInsert(sqliteCmd);
                    }
                    else
                    {
                        sqliteCmd.CommandText = @"UPDATE SystemsLocal
                                                  SET LastVisit = @LastVisit
                                                  WHERE SysAddr = @SysAddr";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = carrierJump.SystemAddress;
                        sqliteCmd.Parameters.Add("@LastVisit", DbType.String).Value = carrierJump.timestamp.ToString("d");
                        SqliteUpdate(sqliteCmd);
                    }
                    //устанавливаем систему как текущую
                    currX = Convert.ToDouble(carrierJump.StarPos[0]);
                    currY = Convert.ToDouble(carrierJump.StarPos[1]);
                    currZ = Convert.ToDouble(carrierJump.StarPos[2]);
                    lblCurrentSystem.Text = carrierJump.StarSystem;
                    //последняя загруженная система
                    Properties.Settings.Default.LastSystemAddress = carrierJump.SystemAddress;
                    Properties.Settings.Default.LastSystemName = carrierJump.StarSystem;
                    Properties.Settings.Default.Save();
                    //текущая система
                    sqliteCmd.CommandText = @"UPDATE CurrentCoordinates
                            SET System = @System, X = @X, Y = @Y, Z = @Z
                            WHERE rowid = @rowid";
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@System", DbType.String).Value = carrierJump.StarSystem;
                    sqliteCmd.Parameters.Add("@X", DbType.Double).Value = carrierJump.StarPos[0];
                    sqliteCmd.Parameters.Add("@Y", DbType.Double).Value = carrierJump.StarPos[1];
                    sqliteCmd.Parameters.Add("@Z", DbType.Double).Value = carrierJump.StarPos[2];
                    sqliteCmd.Parameters.Add("@rowid", DbType.Int32).Value = 1;
                    SqliteUpdate(sqliteCmd);
                }

                //"event":"Scan"
                if (line.IndexOf("\"event\":\"Scan\"") >= 0)
                {
                    scan = JsonConvert.DeserializeObject<Scan>(line);
        
                    if (scan.PlanetClass == "") continue; //это не планета

                    if(scan.SystemAddress == 0 || scan.StarSystem == "")
                    {
                        //если нет адреса или названия системы то берем из последнего прыжка
                        scan.SystemAddress = Properties.Settings.Default.LastSystemAddress;
                        scan.StarSystem = Properties.Settings.Default.LastSystemName;
                    }

                    planetName = scan.BodyName.Replace(scan.StarSystem, "").Trim();
                    planetID = scan.BodyID;
                    landable = scan.Landable ? 1 : 0;
                    scan.PlanetClass = scan.PlanetClass.Replace("Sudarsky", "").Trim();
                    scan.PlanetClass = scan.PlanetClass.Substring(0, 1).ToUpper() + scan.PlanetClass.Substring(1);
                    if(scan.Volcanism.Length > 1)
                        scan.Volcanism = scan.Volcanism.Substring(0, 1).ToUpper() + scan.Volcanism.Substring(1);
                    //удаленная база
                    if (!useLocalBase)
                    {
                        mysqlCmd.CommandText = @"SELECT COUNT(*)
                                                 FROM Planets
                                                 WHERE SysAddr = @SysAddr AND
                                                       (PlanetID = @PlanetID OR PlanetName = @PlanetName)";
                        mysqlCmd.Parameters.Clear();
                        mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = scan.SystemAddress;
                        mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                        mysqlCmd.Parameters.Add("@PlanetName", MySqlDbType.String).Value = planetName;

                        if (MySqlCount(mysqlCmd) == 0)
                        {
                            //если не найден добавляем
                            mysqlCmd.CommandText = @"INSERT INTO Planets (SysAddr, PlanetID, PlanetName, Distance, Class, Volcanism, Landable, Biological, Geological, Reserve)
                                                    VALUES (@SysAddr, @PlanetID, @PlanetName, @Distance, @Class, @Volcanism, @Landable, @Biological, @Geological, @Reserve)";
                            mysqlCmd.Parameters.Clear();
                            mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = scan.SystemAddress;
                            mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                            mysqlCmd.Parameters.Add("@PlanetName", MySqlDbType.String).Value = planetName;
                            mysqlCmd.Parameters.Add("@Distance", MySqlDbType.Float).Value = scan.DistanceFromArrivalLS;
                            mysqlCmd.Parameters.Add("@Class", MySqlDbType.String).Value = scan.PlanetClass;
                            mysqlCmd.Parameters.Add("@Volcanism", MySqlDbType.String).Value = scan.Volcanism;
                            mysqlCmd.Parameters.Add("@Landable", MySqlDbType.Byte).Value = Convert.ToByte(scan.Landable);
                            mysqlCmd.Parameters.Add("@Biological", MySqlDbType.Byte).Value = 0;
                            mysqlCmd.Parameters.Add("@Geological", MySqlDbType.Byte).Value = 0;
                            mysqlCmd.Parameters.Add("@Reserve", MySqlDbType.String).Value = scan.ReserveLevel;
                            MySqlInsert(mysqlCmd);
                        }
                        else
                        {
                            //проверка ID
                            mysqlCmd.CommandText = @"SELECT PlanetID
                                                 FROM Planets
                                                 WHERE SysAddr = @SysAddr AND
                                                       PlanetName = @PlanetName";
                            mysqlCmd.Parameters.Clear();
                            mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = scan.SystemAddress;
                            mysqlCmd.Parameters.Add("@PlanetName", MySqlDbType.String).Value = planetName;
                            dt = MySqlSelect(mysqlCmd);
                            if(dt.Rows.Count > 0 && Convert.ToUInt64(dt.Rows[0]["PlanetID"]) != planetID)
                            {
                                //обновляем ID
                                oldPlanetID = Convert.ToUInt64(dt.Rows[0]["PlanetID"]);
                                //Planets
                                {
                                    mysqlCmd.CommandText = @"UPDATE Planets
                                                    SET PlanetID = @PlanetID
                                                    WHERE SysAddr = @SysAddr AND
                                                          PlanetID = @OldPlanetID";
                                    mysqlCmd.Parameters.Clear();
                                    mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = scan.SystemAddress;
                                    mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                                    mysqlCmd.Parameters.Add("@OldPlanetID", MySqlDbType.UInt64).Value = oldPlanetID;
                                    MySqlUpdate(mysqlCmd);
                                }
                                //Rings
                                {
                                    mysqlCmd.CommandText = @"UPDATE Rings
                                                    SET PlanetID = @PlanetID
                                                    WHERE SysAddr = @SysAddr AND
                                                          PlanetID = @OldPlanetID";
                                    mysqlCmd.Parameters.Clear();
                                    mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = scan.SystemAddress;
                                    mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                                    mysqlCmd.Parameters.Add("@OldPlanetID", MySqlDbType.UInt64).Value = oldPlanetID;
                                    MySqlUpdate(mysqlCmd);
                                }
                            }
                            //обновляем
                            mysqlCmd.CommandText = @"UPDATE Planets
                                                    SET Distance = @Distance,
                                                        Class = @Class,
                                                        Volcanism = @Volcanism,
                                                        Landable = @Landable,
                                                        Reserve = @Reserve
                                                    WHERE SysAddr = @SysAddr AND
                                                          PlanetID = @PlanetID";
                            mysqlCmd.Parameters.Clear();
                            mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = scan.SystemAddress;
                            mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                            mysqlCmd.Parameters.Add("@Distance", MySqlDbType.Double).Value = scan.DistanceFromArrivalLS;
                            mysqlCmd.Parameters.Add("@Class", MySqlDbType.String).Value = scan.PlanetClass;
                            mysqlCmd.Parameters.Add("@Volcanism", MySqlDbType.String).Value = scan.Volcanism;
                            mysqlCmd.Parameters.Add("@Landable", MySqlDbType.Byte).Value = Convert.ToByte(scan.Landable);
                            mysqlCmd.Parameters.Add("@Reserve", MySqlDbType.String).Value = scan.ReserveLevel;
                            MySqlUpdate(mysqlCmd);
                        }
                    }
                    //локальная база
                    sqliteCmd.CommandText = @"SELECT COUNT(*)
                                                 FROM Planets
                                                 WHERE SysAddr = @SysAddr AND
                                                       (PlanetID = @PlanetID OR PlanetName = @PlanetName)";
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = scan.SystemAddress;
                    sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = planetID;
                    sqliteCmd.Parameters.Add("@PlanetName", DbType.String).Value = planetName;
                    
                    if (SqliteCount(sqliteCmd) == 0)
                    {
                        //если не найден добавляем
                        sqliteCmd.CommandText = @"INSERT INTO Planets (SysAddr, PlanetID, PlanetName, Distance, Class, Volcanism, Landable, Biological, Geological, Reserve)
                                                    VALUES (@SysAddr, @PlanetID, @PlanetName, @Distance, @Class, @Volcanism, @Landable, @Biological, @Geological, @Reserve)";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = scan.SystemAddress;
                        sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = planetID;
                        sqliteCmd.Parameters.Add("@PlanetName", DbType.String).Value = planetName;
                        sqliteCmd.Parameters.Add("@Distance", DbType.Double).Value = scan.DistanceFromArrivalLS;
                        sqliteCmd.Parameters.Add("@Class", DbType.String).Value = scan.PlanetClass;
                        sqliteCmd.Parameters.Add("@Volcanism", DbType.String).Value = scan.Volcanism;
                        sqliteCmd.Parameters.Add("@Landable", DbType.Int32).Value = Convert.ToByte(scan.Landable);
                        sqliteCmd.Parameters.Add("@Biological", DbType.Int32).Value = 0;
                        sqliteCmd.Parameters.Add("@Geological", DbType.Int32).Value = 0;
                        sqliteCmd.Parameters.Add("@Reserve", DbType.String).Value = scan.ReserveLevel;
                        SqliteInsert(sqliteCmd);
                        //доп. данные (хранятся только локально)
                        sqliteCmd.CommandText = @"INSERT INTO PlanetsLocal (SysAddr, PlanetID, HasImg)
                                                    VALUES (@SysAddr, @PlanetID, @HasImg)";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = scan.SystemAddress;
                        sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = planetID;
                        sqliteCmd.Parameters.Add("@HasImg", DbType.Int32).Value = 0;
                        SqliteInsert(sqliteCmd);
                    }
                    else
                    {
                        //проверка ID
                        sqliteCmd.CommandText = @"SELECT PlanetID
                                                 FROM Planets
                                                 WHERE SysAddr = @SysAddr AND
                                                       PlanetName = @PlanetName";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = scan.SystemAddress;
                        sqliteCmd.Parameters.Add("@PlanetName", DbType.String).Value = planetName;
                        dt = SqliteSelect(sqliteCmd);
                        if (dt.Rows.Count > 0 && Convert.ToUInt64(dt.Rows[0]["PlanetID"]) != planetID)
                        {
                            //обновляем ID
                            oldPlanetID = Convert.ToUInt64(dt.Rows[0]["PlanetID"]);
                            //Planets
                            {
                                sqliteCmd.CommandText = @"UPDATE Planets
                                                    SET PlanetID = @PlanetID
                                                    WHERE SysAddr = @SysAddr AND
                                                          PlanetID = @OldPlanetID";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = scan.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = planetID;
                                sqliteCmd.Parameters.Add("@OldPlanetID", DbType.UInt64).Value = oldPlanetID;
                                SqliteUpdate(sqliteCmd);
                            }
                            //PlanetsLocal
                            {
                                sqliteCmd.CommandText = @"UPDATE PlanetsLocal
                                                    SET PlanetID = @PlanetID
                                                    WHERE SysAddr = @SysAddr AND
                                                          PlanetID = @OldPlanetID";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = scan.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = planetID;
                                sqliteCmd.Parameters.Add("@OldPlanetID", DbType.UInt64).Value = oldPlanetID;
                                SqliteUpdate(sqliteCmd);
                            }
                            //PlanetsImg
                            {
                                sqliteCmd.CommandText = @"UPDATE PlanetsImg
                                                    SET PlanetID = @PlanetID
                                                    WHERE SysAddr = @SysAddr AND
                                                          PlanetID = @OldPlanetID";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = scan.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = planetID;
                                sqliteCmd.Parameters.Add("@OldPlanetID", DbType.UInt64).Value = oldPlanetID;
                                SqliteUpdate(sqliteCmd);
                            }
                            //Rings
                            {
                                sqliteCmd.CommandText = @"UPDATE Rings
                                                    SET PlanetID = @PlanetID
                                                    WHERE SysAddr = @SysAddr AND
                                                          PlanetID = @OldPlanetID";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = scan.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = planetID;
                                sqliteCmd.Parameters.Add("@OldPlanetID", DbType.UInt64).Value = oldPlanetID;
                                SqliteUpdate(sqliteCmd);
                            }
                        }

                        //иначе обновляем
                        sqliteCmd.CommandText = @"UPDATE Planets
                                                    SET Distance = @Distance,
                                                        Class = @Class,
                                                        Volcanism = @Volcanism,
                                                        Landable = @Landable,
                                                        Reserve = @Reserve
                                                    WHERE SysAddr = @SysAddr AND
                                                          PlanetID = @PlanetID";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = scan.SystemAddress;
                        sqliteCmd.Parameters.Add("@PlanetID", DbType.String).Value = planetID;
                        sqliteCmd.Parameters.Add("@Distance", DbType.Double).Value = scan.DistanceFromArrivalLS;
                        sqliteCmd.Parameters.Add("@Class", DbType.String).Value = scan.PlanetClass;
                        sqliteCmd.Parameters.Add("@Volcanism", DbType.String).Value = scan.Volcanism;
                        sqliteCmd.Parameters.Add("@Landable", DbType.Int32).Value = Convert.ToByte(scan.Landable);
                        sqliteCmd.Parameters.Add("@Reserve", DbType.String).Value = scan.ReserveLevel;
                        SqliteUpdate(sqliteCmd);
                        
                    }
                    //заполнение Materials
                    if (scan.Materials != null)
                    {
                        foreach (Material elem in scan.Materials)
                        {
                            //удаленная база
                            if (!useLocalBase)
                            {
                                mysqlCmd.CommandText = @"SELECT COUNT(*)
                                                 FROM Materials
                                                 WHERE SysAddr = @SysAddr AND
                                                       PlanetID = @PlanetID AND
                                                       Name = @Name";
                                mysqlCmd.Parameters.Clear();
                                mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = scan.SystemAddress;
                                mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                                mysqlCmd.Parameters.Add("@Name", MySqlDbType.String).Value = elem.Name;

                                if (MySqlCount(mysqlCmd) == 0)
                                {
                                    //если не найден добавляем
                                    mysqlCmd.CommandText = @"INSERT INTO Materials (SysAddr, PlanetID, Name, Percent)
                                                            VALUES (@SysAddr, @PlanetID, @Name, @Percent)";
                                    mysqlCmd.Parameters.Clear();
                                    mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = scan.SystemAddress;
                                    mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                                    mysqlCmd.Parameters.Add("@Name", MySqlDbType.String).Value = elem.Name;
                                    mysqlCmd.Parameters.Add("@Percent", MySqlDbType.Double).Value = elem.Percent;
                                    MySqlInsert(mysqlCmd);
                                }
                                else
                                {
                                    //иначе обновляем
                                    mysqlCmd.CommandText = @"UPDATE Materials
                                                            SET Percent = @Percent
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID AND
                                                                  Name = @Name";
                                    mysqlCmd.Parameters.Clear();
                                    mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = scan.SystemAddress;
                                    mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                                    mysqlCmd.Parameters.Add("@Name", MySqlDbType.String).Value = elem.Name;
                                    mysqlCmd.Parameters.Add("@Percent", MySqlDbType.Double).Value = elem.Percent;
                                    MySqlUpdate(mysqlCmd);
                                }
                            }
                            //локальная база
                            if (elem.Name_Localised == null) elem.Name_Localised = elem.Name;

                            sqliteCmd.CommandText = @"SELECT COUNT(*)
                                                      FROM Materials
                                                      WHERE SysAddr = @SysAddr AND
                                                            PlanetID = @PlanetID AND
                                                            Name = @Name";
                            sqliteCmd.Parameters.Clear();
                            sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = scan.SystemAddress;
                            sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = planetID;
                            sqliteCmd.Parameters.Add("@Name", DbType.String).Value = elem.Name;

                            if (SqliteCount(sqliteCmd) == 0)
                            {
                                //если не найден добавляем
                                sqliteCmd.CommandText = @"INSERT INTO Materials (SysAddr, PlanetID, Name, Percent)
                                                            VALUES (@SysAddr, @PlanetID, @Name, @Percent)";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = scan.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = planetID;
                                sqliteCmd.Parameters.Add("@Name", DbType.String).Value = elem.Name;
                                sqliteCmd.Parameters.Add("@Percent", DbType.Double).Value = elem.Percent;
                                SqliteInsert(sqliteCmd);
                            }
                            else
                            {
                                //иначе обновляем
                                sqliteCmd.CommandText = @"UPDATE Materials
                                                            SET Percent = @Percent
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID AND
                                                                  Name = @Name";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = scan.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = planetID;
                                sqliteCmd.Parameters.Add("@Name", DbType.String).Value = elem.Name;
                                sqliteCmd.Parameters.Add("@Percent", DbType.Double).Value = elem.Percent;
                                SqliteUpdate(sqliteCmd);
                            }
                            //добавление локализации
                            sqliteCmd.CommandText = @"SELECT COUNT(*)
                                                      FROM Localise
                                                      WHERE Name = @Name";

                            sqliteCmd.Parameters.Clear();
                            sqliteCmd.Parameters.Add("@Name", DbType.String).Value = elem.Name;

                            if (SqliteCount(sqliteCmd) == 0)
                            {
                                //если не найден добавляем
                                sqliteCmd.CommandText = @"INSERT INTO Localise (IsRAW, Name, NameLocalised)
                                                        VALUES (@IsRAW, @Name, @NameLocalised)";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@IsRAW", DbType.Int32).Value = 1;
                                sqliteCmd.Parameters.Add("@Name", DbType.String).Value = elem.Name;
                                sqliteCmd.Parameters.Add("@NameLocalised", DbType.String).Value = elem.Name_Localised;
                                SqliteInsert(sqliteCmd);
                            }
                            else
                            {
                                //иначе обновляем
                                sqliteCmd.CommandText = @"UPDATE Localise
                                                         SET NameLocalised = @NameLocalised
                                                         WHERE Name = @Name";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@Name", DbType.String).Value = elem.Name;
                                sqliteCmd.Parameters.Add("@NameLocalised", DbType.String).Value = elem.Name_Localised;
                                SqliteUpdate(sqliteCmd);
                            }
                        }
                    }
                    //заполняем Rings
                    if (scan.Rings != null)
                    {
                        foreach (Ring elem in scan.Rings)
                        {

                            ringName = elem.Name.Replace(scan.BodyName, "");
                            elem.RingClass = elem.RingClass.Replace("eRingClass_", "");

                            //удаленная база
                            if (!useLocalBase)
                            {
                                mysqlCmd.CommandText = @"SELECT COUNT(*)
                                                        FROM Rings
                                                        WHERE SysAddr = @SysAddr  AND 
                                                              PlanetID = @PlanetID AND
                                                              RingName = @RingName";
                                mysqlCmd.Parameters.Clear();
                                mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = scan.SystemAddress;
                                mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                                mysqlCmd.Parameters.Add("@RingName", MySqlDbType.String).Value = ringName;

                                if (MySqlCount(mysqlCmd) == 0)
                                {
                                    //если не найден добавляем
                                    mysqlCmd.CommandText = @"INSERT INTO Rings (SysAddr, PlanetID, RingName, Class)
                                                            VALUES (@SysAddr, @PlanetID, @RingName, @Class)";
                                    mysqlCmd.Parameters.Clear();
                                    mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = scan.SystemAddress;
                                    mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                                    mysqlCmd.Parameters.Add("@RingName", MySqlDbType.String).Value = ringName;
                                    mysqlCmd.Parameters.Add("@Class", MySqlDbType.String).Value = elem.RingClass;
                                    MySqlInsert(mysqlCmd);
                                }
                                else
                                {
                                    //иначе обновляем
                                    mysqlCmd.CommandText = @"UPDATE Rings
                                                            SET Class = @Class
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID AND
                                                                  RingName = @RingName";
                                    mysqlCmd.Parameters.Clear();
                                    mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = scan.SystemAddress;
                                    mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                                    mysqlCmd.Parameters.Add("@RingName", MySqlDbType.String).Value = ringName;
                                    mysqlCmd.Parameters.Add("@Class", MySqlDbType.String).Value = elem.RingClass;
                                    MySqlUpdate(mysqlCmd);
                                }
                            }
                            //локальная база
                            sqliteCmd.CommandText = @"SELECT COUNT(*)
                                                        FROM Rings
                                                        WHERE SysAddr = @SysAddr  AND 
                                                              PlanetID = @PlanetID AND
                                                              RingName = @RingName";
                            sqliteCmd.Parameters.Clear();
                            sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = scan.SystemAddress;
                            sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = planetID;
                            sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;

                            if (SqliteCount(sqliteCmd) == 0)
                            {
                                //если не найден добавляем
                                sqliteCmd.CommandText = @"INSERT INTO Rings (SysAddr, PlanetID, RingName, Class)
                                                            VALUES (@SysAddr, @PlanetID, @RingName, @Class)";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = scan.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = planetID;
                                sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
                                sqliteCmd.Parameters.Add("@Class", DbType.String).Value = elem.RingClass;
                                SqliteInsert(sqliteCmd);
                            }
                            else
                            {
                                //иначе обновляем
                                sqliteCmd.CommandText = @"UPDATE Rings
                                                            SET Class = @Class
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID AND
                                                                  RingName = @RingName";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = scan.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = planetID;
                                sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
                                sqliteCmd.Parameters.Add("@Class", DbType.String).Value = elem.RingClass;
                                SqliteUpdate(sqliteCmd);
                            }
                        }
                    }
                }
                //"event":"SAASignalsFound"
                if(line.IndexOf("\"event\":\"SAASignalsFound\"") >= 0)
                {
                    saaSignals = JsonConvert.DeserializeObject<SAASignalsFound>(line);

                    //получение названия системы и планеты по адресу
                    if (!useLocalBase)
                    {
                        mysqlCmd.CommandText = @"SELECT SystemName
                                                 FROM Systems
                                                 WHERE SysAddr = @SysAddr";
                        mysqlCmd.Parameters.Clear();
                        mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = saaSignals.SystemAddress;
                        dt = MySqlSelect(mysqlCmd);
                        systemName = dt.Rows[0]["SystemName"].ToString();

                        mysqlCmd.CommandText = @"SELECT PlanetName
                                                 FROM Planets
                                                 WHERE SysAddr = @SysAddr AND PlanetID = @PlanetID";
                        mysqlCmd.Parameters.Clear();
                        mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = saaSignals.SystemAddress;
                        mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = saaSignals.BodyID;
                        dt = MySqlSelect(mysqlCmd);
                        if (dt.Rows.Count == 0)
                        {
                            //по id не нашлось, может загружено из EDSM
                            //поищем по названию
                            planetName = saaSignals.BodyName;
                            planetName = saaSignals.BodyName.Replace(systemName, "").Trim();
                            if (planetName.EndsWith("Ring", System.StringComparison.OrdinalIgnoreCase))
                            {
                                planetName = planetName.Substring(0, planetName.Length - 6).Trim();
                            }
                            //имя опредилили, ищем
                            mysqlCmd.CommandText = @"SELECT PlanetID
                                                 FROM Planets
                                                 WHERE SysAddr = @SysAddr AND PlanetName = @PlanetName";
                            mysqlCmd.Parameters.Clear();
                            mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = saaSignals.SystemAddress;
                            mysqlCmd.Parameters.Add("@PlanetName", MySqlDbType.String).Value = planetName;
                            dt = MySqlSelect(mysqlCmd);
                            if (dt.Rows.Count == 0) continue; //ничего не нашли
                            //id из EDSM, меняем
                            saaSignals.BodyID = Convert.ToUInt64(dt.Rows[0]["PlanetID"]);
                        }
                        else
                        {
                            planetName = dt.Rows[0]["PlanetName"].ToString();
                        }

                        //название планеты + название кольца
                        ringName = saaSignals.BodyName.Replace(systemName, "").Trim();
                        //название кольца
                        ringName = ringName.Replace(planetName, "").Trim();
                    }
                    else
                    {
                        sqliteCmd.CommandText = @"SELECT SystemName
                                                 FROM Systems
                                                 WHERE SysAddr = @SysAddr";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = saaSignals.SystemAddress;
                        dt = SqliteSelect(sqliteCmd);
                        systemName = dt.Rows[0]["SystemName"].ToString();

                        sqliteCmd.CommandText = @"SELECT PlanetName
                                                 FROM Planets
                                                 WHERE SysAddr = @SysAddr AND PlanetID = @PlanetID";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = saaSignals.SystemAddress;
                        sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = saaSignals.BodyID;
                        dt = SqliteSelect(sqliteCmd);
                        if (dt.Rows.Count == 0)
                        {
                            //по id не нашлось, может загружено из EDSM
                            //поищем по названию
                            planetName = saaSignals.BodyName;
                            planetName = saaSignals.BodyName.Replace(systemName, "").Trim();
                            if (planetName.EndsWith("Ring", System.StringComparison.OrdinalIgnoreCase))
                            {
                                planetName = planetName.Substring(0, planetName.Length - 6).Trim();
                            }
                            //имя опредилили, ищем
                            sqliteCmd.CommandText = @"SELECT PlanetID
                                                 FROM Planets
                                                 WHERE SysAddr = @SysAddr AND PlanetName = @PlanetName";
                            sqliteCmd.Parameters.Clear();
                            sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = saaSignals.SystemAddress;
                            sqliteCmd.Parameters.Add("@PlanetName", DbType.String).Value = planetName;
                            dt = SqliteSelect(sqliteCmd);
                            if (dt.Rows.Count == 0) continue; //ничего не нашли
                            //id из EDSM, меняем
                            saaSignals.BodyID = Convert.ToUInt64(dt.Rows[0]["PlanetID"]);
                        }
                        else
                        {
                            planetName = dt.Rows[0]["PlanetName"].ToString();
                        }

                        //название планеты + название кольца
                        ringName = saaSignals.BodyName.Replace(systemName, "").Trim();
                        //название кольца
                        ringName = ringName.Replace(planetName, "").Trim();
                    }
                    //добавляем отсутствующие
                    foreach (Signal elem in saaSignals.Signals)
                    {
                        if(elem.Type == "$SAA_SignalType_Biological;" ||
                            elem.Type == "$SAA_SignalType_Geological;" ||
                            elem.Type == "$SAA_SignalType_Other;" ||
                            elem.Type == "$SAA_SignalType_Thargoid;" ||
                            elem.Type == "$SAA_SignalType_Guardian;" ||
                            elem.Type == "$SAA_SignalType_Human;")
                        {
                            //это сигналы с поверхности планеты
                            //добавляем в таблицу планет
                            if (elem.Type == "$SAA_SignalType_Biological;")
                            {
                                //удаленная база
                                if (!useLocalBase)
                                {
                                    mysqlCmd.CommandText = @"UPDATE Planets
                                                            SET Biological = @Biological
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID";
                                    mysqlCmd.Parameters.Clear();
                                    mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = saaSignals.SystemAddress;
                                    mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = saaSignals.BodyID;
                                    mysqlCmd.Parameters.Add("@Biological", MySqlDbType.Int32).Value = elem.Count;
                                    MySqlUpdate(mysqlCmd);
                                }
                                //локальная база
                                sqliteCmd.CommandText = @"UPDATE Planets
                                                            SET Biological = @Biological
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = saaSignals.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.String).Value = saaSignals.BodyID;
                                sqliteCmd.Parameters.Add("@Biological", DbType.Int32).Value = elem.Count;
                                SqliteUpdate(sqliteCmd);
                            }
                            if (elem.Type == "$SAA_SignalType_Geological;")
                            {
                                //удаленная база
                                if (!useLocalBase)
                                {
                                    mysqlCmd.CommandText = @"UPDATE Planets
                                                            SET Geological = @Geological
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID";
                                    mysqlCmd.Parameters.Clear();
                                    mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = saaSignals.SystemAddress;
                                    mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = saaSignals.BodyID;
                                    mysqlCmd.Parameters.Add("@Geological", MySqlDbType.Int32).Value = elem.Count;
                                    MySqlUpdate(mysqlCmd);
                                }
                                //локальная база
                                sqliteCmd.CommandText = @"UPDATE Planets
                                                            SET Geological = @Geological
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = saaSignals.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = saaSignals.BodyID;
                                sqliteCmd.Parameters.Add("@Geological", DbType.Int32).Value = elem.Count;
                                SqliteUpdate(sqliteCmd);
                            }
                            if (elem.Type == "$SAA_SignalType_Guardian;")
                            {
                                //удаленная база
                                if (!useLocalBase)
                                {
                                    mysqlCmd.CommandText = @"UPDATE Planets
                                                            SET Guardian = @Guardian
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID";
                                    mysqlCmd.Parameters.Clear();
                                    mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = saaSignals.SystemAddress;
                                    mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = saaSignals.BodyID;
                                    mysqlCmd.Parameters.Add("@Guardian", MySqlDbType.Int32).Value = elem.Count;
                                    MySqlUpdate(mysqlCmd);
                                }
                                //локальная база
                                sqliteCmd.CommandText = @"UPDATE Planets
                                                            SET Guardian = @Guardian
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = saaSignals.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = saaSignals.BodyID;
                                sqliteCmd.Parameters.Add("@Guardian", DbType.Int32).Value = elem.Count;
                                SqliteUpdate(sqliteCmd);
                            }
                            if (elem.Type == "$SAA_SignalType_Human;")
                            {
                                //удаленная база
                                if (!useLocalBase)
                                {
                                    mysqlCmd.CommandText = @"UPDATE Planets
                                                            SET Human = @Human
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID";
                                    mysqlCmd.Parameters.Clear();
                                    mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = saaSignals.SystemAddress;
                                    mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = saaSignals.BodyID;
                                    mysqlCmd.Parameters.Add("@Human", MySqlDbType.Int32).Value = elem.Count;
                                    MySqlUpdate(mysqlCmd);
                                }
                                //локальная база
                                sqliteCmd.CommandText = @"UPDATE Planets
                                                            SET Human = @Human
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = saaSignals.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = saaSignals.BodyID;
                                sqliteCmd.Parameters.Add("@Human", DbType.Int32).Value = elem.Count;
                                SqliteUpdate(sqliteCmd);
                            }
                            if (elem.Type == "$SAA_SignalType_Thargoid;")
                            {
                                //удаленная база
                                if (!useLocalBase)
                                {
                                    mysqlCmd.CommandText = @"UPDATE Planets
                                                            SET Thargoid = @Thargoid
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID";
                                    mysqlCmd.Parameters.Clear();
                                    mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = saaSignals.SystemAddress;
                                    mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = saaSignals.BodyID;
                                    mysqlCmd.Parameters.Add("@Thargoid", MySqlDbType.Int32).Value = elem.Count;
                                    MySqlUpdate(mysqlCmd);
                                }
                                //локальная база
                                sqliteCmd.CommandText = @"UPDATE Planets
                                                            SET Thargoid = @Thargoid
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = saaSignals.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = saaSignals.BodyID;
                                sqliteCmd.Parameters.Add("@Thargoid", DbType.Int32).Value = elem.Count;
                                SqliteUpdate(sqliteCmd);
                            }
                            if (elem.Type == "$SAA_SignalType_Other;")
                            {
                                //удаленная база
                                if (!useLocalBase)
                                {
                                    mysqlCmd.CommandText = @"UPDATE Planets
                                                            SET Other = @Other
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID";
                                    mysqlCmd.Parameters.Clear();
                                    mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = saaSignals.SystemAddress;
                                    mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = saaSignals.BodyID;
                                    mysqlCmd.Parameters.Add("@Other", MySqlDbType.Int32).Value = elem.Count;
                                    MySqlUpdate(mysqlCmd);
                                }
                                //локальная база
                                sqliteCmd.CommandText = @"UPDATE Planets
                                                            SET Other = @Other
                                                            WHERE SysAddr = @SysAddr AND
                                                                  PlanetID = @PlanetID";
                                sqliteCmd.Parameters.Clear();
                                sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = saaSignals.SystemAddress;
                                sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = saaSignals.BodyID;
                                sqliteCmd.Parameters.Add("@Other", DbType.Int32).Value = elem.Count;
                                SqliteUpdate(sqliteCmd);
                            }
                            continue;
                        }
                        //это сигналы колец
                        if (elem.Type_Localised == null) elem.Type_Localised = elem.Type;

                        //удаленная база
                        if (!useLocalBase)
                        {
                            mysqlCmd.CommandText = @"SELECT COUNT(*)
                                                    FROM Signals
                                                    WHERE SysAddr = @SysAddr AND
                                                          PlanetID = @PlanetID AND
                                                          RingName = @RingName AND
                                                          Type = @Type";
                            mysqlCmd.Parameters.Clear();
                            mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = saaSignals.SystemAddress;
                            mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = saaSignals.BodyID;
                            mysqlCmd.Parameters.Add("@RingName", MySqlDbType.String).Value = ringName;
                            mysqlCmd.Parameters.Add("@Type", MySqlDbType.String).Value = elem.Type;

                            if (MySqlCount(mysqlCmd) == 0)
                            {
                                //если не найден добавляем
                                mysqlCmd.CommandText = @"INSERT INTO Signals (SysAddr, PlanetID, RingName, Type, Count)
                                         VALUES (@SysAddr, @PlanetID, @RingName, @Type, @Count)";
                                mysqlCmd.Parameters.Clear();
                                mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = saaSignals.SystemAddress;
                                mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = saaSignals.BodyID;
                                mysqlCmd.Parameters.Add("@RingName", MySqlDbType.String).Value = ringName;
                                mysqlCmd.Parameters.Add("@Type", MySqlDbType.String).Value = elem.Type;
                                mysqlCmd.Parameters.Add("@Count", MySqlDbType.Int32).Value = elem.Count;

                                MySqlInsert(mysqlCmd);
                            }
                            else
                            {
                                //иначе обновляем
                                mysqlCmd.CommandText = @"UPDATE Signals
                                                        SET Count = @Count
                                                        WHERE SysAddr = @SysAddr AND
                                                              PlanetID = @PlanetID AND
                                                              RingName = @RingName AND
                                                              Type = @Type";
                                mysqlCmd.Parameters.Clear();
                                mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = saaSignals.SystemAddress;
                                mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = saaSignals.BodyID;
                                mysqlCmd.Parameters.Add("@RingName", MySqlDbType.String).Value = ringName;
                                mysqlCmd.Parameters.Add("@Type", MySqlDbType.String).Value = elem.Type;
                                mysqlCmd.Parameters.Add("@Count", MySqlDbType.Int32).Value = elem.Count;

                                MySqlUpdate(mysqlCmd);
                            }

                        }
                        //локальная база
                        sqliteCmd.CommandText = @"SELECT COUNT(*)
                                                    FROM Signals
                                                    WHERE SysAddr = @SysAddr AND
                                                          PlanetID = @PlanetID AND
                                                          RingName = @RingName AND
                                                          Type = @Type";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = saaSignals.SystemAddress;
                        sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = saaSignals.BodyID;
                        sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
                        sqliteCmd.Parameters.Add("@Type", DbType.String).Value = elem.Type;

                        if (SqliteCount(sqliteCmd) == 0)
                        {
                            //если не найден добавляем
                            sqliteCmd.CommandText = @"INSERT INTO Signals (SysAddr, PlanetID, RingName, Type, Count)
                                         VALUES (@SysAddr, @PlanetID, @RingName, @Type, @Count)";
                            sqliteCmd.Parameters.Clear();
                            sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = saaSignals.SystemAddress;
                            sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = saaSignals.BodyID;
                            sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
                            sqliteCmd.Parameters.Add("@Type", DbType.String).Value = elem.Type;
                            sqliteCmd.Parameters.Add("@Count", DbType.Int32).Value = elem.Count;

                            SqliteInsert(sqliteCmd);

                            sqliteCmd.CommandText = @"INSERT INTO SignalsLocal (SysAddr, PlanetID, RingName, Type, Comment, HasImg)
                                         VALUES (@SysAddr, @PlanetID, @RingName, @Type, @Comment, @HasImg)";
                            sqliteCmd.Parameters.Clear();
                            sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = saaSignals.SystemAddress;
                            sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = saaSignals.BodyID;
                            sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
                            sqliteCmd.Parameters.Add("@Type", DbType.String).Value = elem.Type;
                            sqliteCmd.Parameters.Add("@Comment", DbType.String).Value = "";
                            sqliteCmd.Parameters.Add("@HasImg", DbType.Int32).Value = 0;

                            SqliteInsert(sqliteCmd);
                        }
                        else
                        {
                            //иначе обновляем
                            sqliteCmd.CommandText = @"UPDATE Signals
                                                        SET Count = @Count
                                                        WHERE SysAddr = @SysAddr AND
                                                              PlanetID = @PlanetID AND
                                                              RingName = @RingName AND
                                                              Type = @Type";
                            sqliteCmd.Parameters.Clear();
                            sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = saaSignals.SystemAddress;
                            sqliteCmd.Parameters.Add("@PlanetID", DbType.Int64).Value = saaSignals.BodyID;
                            sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
                            sqliteCmd.Parameters.Add("@Type", DbType.String).Value = elem.Type;
                            sqliteCmd.Parameters.Add("@Count", DbType.Int32).Value = elem.Count;

                            SqliteUpdate(sqliteCmd);

                        }
                        //добавление локализации
                        sqliteCmd.CommandText = @"SELECT COUNT(*)
                                                      FROM Localise
                                                      WHERE Name = @Name";
                        sqliteCmd.Parameters.Clear();
                        sqliteCmd.Parameters.Add("@Name", DbType.String).Value = elem.Type;

                        if (SqliteCount(sqliteCmd) == 0)
                        {
                            //если не найден добавляем
                            sqliteCmd.CommandText = @"INSERT INTO Localise (IsRAW, Name, NameLocalised)
                                                        VALUES (@IsRAW, @Name, @NameLocalised)";
                            sqliteCmd.Parameters.Clear();
                            sqliteCmd.Parameters.Add("@IsRAW", DbType.Int32).Value = 0;
                            sqliteCmd.Parameters.Add("@Name", DbType.String).Value = elem.Type;
                            sqliteCmd.Parameters.Add("@NameLocalised", DbType.String).Value = elem.Type_Localised;
                            SqliteInsert(sqliteCmd);
                        }
                        else
                        {
                            //иначе обновляем
                            sqliteCmd.CommandText = @"UPDATE Localise
                                                         SET NameLocalised = @NameLocalised
                                                         WHERE Name = @Name";
                            sqliteCmd.Parameters.Clear();
                            sqliteCmd.Parameters.Add("@Name", DbType.String).Value = elem.Type;
                            sqliteCmd.Parameters.Add("@NameLocalised", DbType.String).Value = elem.Type_Localised;
                            SqliteUpdate(sqliteCmd);
                        }
                    }
                }
                prbUpload.PerformStep();
                Application.DoEvents();
            }
            if (!useLocalBase) DisconnectMySQL();

            prbUpload.Visible = false;
            if (rbRaw.Checked)
            {
                FillMaterialsFilter();
            }
            if (rbSignals.Checked)
            {
                FillSignalsFilter();
            }
            loadingData = false;
            ApplyFilter();
        }
        private void FillMaterialsFilter()
        {
            DataTable dt = new DataTable();

            sqliteCmd.CommandText = @"SELECT Name, NameLocalised
                                       FROM Localise
                                       WHERE IsRAW = @IsRAW";
            sqliteCmd.Parameters.Clear();
            sqliteCmd.Parameters.Add("@IsRAW", DbType.Int32).Value = 1;
            dt = SqliteSelect(sqliteCmd);

            cbxFilter.Items.Clear();
            cbxFilter.Items.Add("");
            foreach (DataRow row in dt.Rows)
            {
                cbxFilter.Items.Add(row["Name"].ToString() + " (" + row["NameLocalised"].ToString() + ")");
            }
        }
        private void FillSignalsFilter()
        {
            DataTable dt = new DataTable();

            sqliteCmd.CommandText = @"SELECT Name, NameLocalised
                                       FROM Localise
                                       WHERE IsRAW = @IsRAW";
            sqliteCmd.Parameters.Clear();
            sqliteCmd.Parameters.Add("@IsRAW", DbType.Int32).Value = 0;
            dt = SqliteSelect(sqliteCmd);

            cbxFilter.Items.Clear();
            cbxFilter.Items.Add("");
            foreach (DataRow row in dt.Rows)
            {
                cbxFilter.Items.Add(row["Name"].ToString() + " (" + row["NameLocalised"].ToString() + ")");
            }
        }
        private void ShowEmptySystems()
        {
            DataTable dt;

            double dx;
            double dy;
            double dz;

            sqliteCmd.CommandText = @"SELECT *
                       FROM Systems
                       WHERE SysAddr NOT IN (SELECT SysAddr FROM Planets)";
            dtSystems = SqliteSelect(sqliteCmd);
            //колонки локальных данных
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Double"),
                ColumnName = "Distance",
                AutoIncrement = false,
                Caption = "",
                ReadOnly = true,
                Unique = false
            };
            dtSystems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "LastVisit",
                AutoIncrement = false,
                Caption = "",
                ReadOnly = true,
                Unique = false
            };
            dtSystems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.String"),
                ColumnName = "Comment",
                AutoIncrement = false,
                Caption = "",
                ReadOnly = true,
                Unique = false
            };
            dtSystems.Columns.Add(column);
            column = new DataColumn
            {
                DataType = System.Type.GetType("System.Int32"),
                ColumnName = "HasImg",
                AutoIncrement = false,
                Caption = "",
                ReadOnly = true,
                Unique = false
            };

            prbUpload.Minimum = 1;
            prbUpload.Maximum = dtSystems.Rows.Count;
            prbUpload.Value = 1;
            prbUpload.Step = 1;
            prbUpload.Visible = true;

            foreach (DataRow row in dtSystems.Rows)
            {
                //расчет расстояния
                dx = Convert.ToDouble(row["StarPosX"]) - currX;
                dy = Convert.ToDouble(row["StarPosY"]) - currY;
                dz = Convert.ToDouble(row["StarPosZ"]) - currZ;

                row["Distance"] = Math.Round(Math.Sqrt(dx * dx + dy * dy + dz * dz), 2);
                //заполнение локальными данными
                sqliteCmd.CommandText = @"SELECT *
                                          FROM SystemsLocal
                                          WHERE SysAddr = @SysAddr";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = Convert.ToInt64(row["SysAddr"]);
                dt = SqliteSelect(sqliteCmd);
                row["LastVisit"] = dt.Rows[0]["LastVisit"].ToString();
                row["Comment"] = dt.Rows[0]["Comment"].ToString();
                row["HasImg"] = dt.Rows[0]["HasImg"].ToString();

                prbUpload.PerformStep();
                Application.DoEvents();
            }
            prbUpload.Visible = false;

            dgvSystems.DataSource = dtSystems;

            dgvSystems.Update();
            lblSystems.Text = "Systems (" + dtSystems.Rows.Count.ToString() + ")";
        }
        private void DeleteEmptySystems()
        {
            sqliteCmd.CommandText = @"DELETE
                                    FROM Systems
                                    WHERE SysAddr NOT IN (SELECT SysAddr FROM Planets)";
            sqliteCmd.Parameters.Clear();
            SqliteDelete(sqliteCmd);
            LoadSystems("");
        }
        private DateTime FromTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
        private double ToTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
        private void ApplyFilter()
        {
            char ch = '(';
            int ind;
            string select = "";

            if (cbxFilter.SelectedItem != null && cbxFilter.SelectedItem.ToString() != "")
            {
                ind = cbxFilter.SelectedItem.ToString().IndexOf(ch);
                select = cbxFilter.SelectedItem.ToString().Substring(0, ind).Trim();
            }
            LoadSystems(select);
        }

        //события формы
        private void MainForm_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
           
            lblLogFolder.Text = "Log folder: " + Properties.Settings.Default.LogPath +
                    " [Last uploaded log: " + Properties.Settings.Default.LastLog + "]";
            useLocalBase = Properties.Settings.Default.UseLocalBase;
            
            //проверка подключения к mysql серверу
            if (!useLocalBase) ConnectMySQL();
            if (!useLocalBase) DisconnectMySQL(); //подключение успешно. отключаемся

            useLocalBaseToolStripMenuItem.Checked = useLocalBase;
            showEmptySystemsToolStripMenuItem.Enabled = useLocalBase;
            deleteEmptySystemsToolStripMenuItem.Enabled = useLocalBase;
            if (useLocalBase)
            {
                useLocalBaseToolStripMenuItem.BackColor = Color.LightGreen;
            }
            else
            {
                useLocalBaseToolStripMenuItem.BackColor = SystemColors.ControlDark;
            }
            //подключение к базе
            ConnectDB();
            //получение текущих координат
            sqliteCmd.CommandText = @"SELECT *
                                      FROM CurrentCoordinates
                                      WHERE rowid = @rowid";
            sqliteCmd.Parameters.Clear();
            sqliteCmd.Parameters.Add("@rowid", DbType.Int32).Value = 1;
            dt = SqliteSelect(sqliteCmd);
            foreach (DataRow row in dt.Rows)
            {
                currX = Convert.ToDouble(row["X"]);
                currY = Convert.ToDouble(row["Y"]);
                currZ = Convert.ToDouble(row["Z"]);
                this.lblCurrentSystem.Text = row["System"].ToString();
            }
            if (rbRaw.Checked) FillMaterialsFilter();
           
            if (rbSignals.Checked) FillSignalsFilter();
            
            LoadSystems("");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //отключение базы
            DisconnectDB();
            Properties.Settings.Default.UseLocalBase = useLocalBase;
            Properties.Settings.Default.Save();
        }

        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.LogPath = fbd.SelectedPath;
                Properties.Settings.Default.Save();
                lblLogFolder.Text = "Log folder: " + Properties.Settings.Default.LogPath;
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Filter = "Journal.xxxxxxxxxxxx.xx.log|*.log";
            opf.Title = "Select log file";
            opf.InitialDirectory = Properties.Settings.Default.LogPath;
            if (opf.ShowDialog() == DialogResult.OK)
            {
                GetLogData(opf.FileName);
                Properties.Settings.Default.LastLog = opf.SafeFileName;
                Properties.Settings.Default.LastLogPath = opf.FileName;
                Properties.Settings.Default.Save();
                lblLogFolder.Text = "Log folder: " + Properties.Settings.Default.LogPath +
                    " [Last uploaded log: " + Properties.Settings.Default.LastLog + "]";
            }
        }
  
        private void tbxSystemFilter_TextChanged(object sender, EventArgs e)
        {
            if (rbSystems.Checked)
            {
                (dgvSystems.DataSource as DataTable).DefaultView.RowFilter =
                    String.Format("SystemName like '{0}%'", tbxSystemFilter.Text);
            }
            if(rbComment.Checked)
            {
                (dgvSystems.DataSource as DataTable).DefaultView.RowFilter =
                    String.Format("Comment like '%{0}%'", tbxSystemFilter.Text);
            }
        }

        private void cbxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void rbRaw_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRaw.Checked)
            {
                FillMaterialsFilter();
            }
        }

        private void rbSignals_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSignals.Checked)
            {
                FillSignalsFilter();
            }
        }

        private void tsmiSystemsCopy_Click(object sender, EventArgs e)
        {
            string copy;
            copy = dgvSystems.CurrentCell.Value.ToString();
            Clipboard.SetText(copy);
        }

        private void dgvSystems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string comment = dgvSystems.CurrentRow.Cells["sysComment"].Value.ToString();
            if (dgvSystems.CurrentRow.IsNewRow) return;
            UInt64 sysAddr = Convert.ToUInt64(dgvSystems.CurrentRow.Cells["sysSystemAddress"].Value);

            sqliteCmd.CommandText = @"UPDATE SystemsLocal
                                                  SET Comment = @Comment
                                                  WHERE SysAddr = @SysAddr";
            sqliteCmd.Parameters.Clear();
            sqliteCmd.Parameters.Add("@Comment", DbType.String).Value = comment;
            sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
            SqliteUpdate(sqliteCmd);
        }

        private void dgvSignals_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string comment = dgvSignals.CurrentRow.Cells["sigComment"].Value.ToString();
            if (dgvSignals.CurrentRow.IsNewRow) return;
            UInt64 sysAddr = Convert.ToUInt64(dgvSystems.CurrentRow.Cells["sigSystemAddress"].Value);

            sqliteCmd.CommandText = @"UPDATE SignalsLocal
                                                  SET Comment = @Comment
                                                  WHERE SysAddr = @SysAddr";
            sqliteCmd.Parameters.Clear();
            sqliteCmd.Parameters.Add("@Comment", DbType.String).Value = comment;
            sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
            SqliteUpdate(sqliteCmd);
        }

        private void resetSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cbxFilter.SelectedIndex = 0;
            tbxSystemFilter.Text = "";
        }

        private void showEmptySystemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowEmptySystems();
        }

        private void deleteEmptySystemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Delete all systems without planets?",
                                                  "Delete empty systems",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);
            if (result == DialogResult.Yes) DeleteEmptySystems();
        }

        private void dgvSystems_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (loadingData) return;
            if (systemsRowIndex == dgvSystems.CurrentRow.Index) return;
            if (dgvSystems.CurrentRow.IsNewRow) return;

            UInt64 sysAddr = 0;
            char ch = '(';
            int ind;
            string filter = "";

            systemsRowIndex = dgvSystems.CurrentRow.Index;
            sysAddr = Convert.ToUInt64(dgvSystems.CurrentRow.Cells["sysSystemAddress"].Value);

            if (cbxFilter.SelectedItem != null && cbxFilter.SelectedItem.ToString() != "")
            {
                ind = cbxFilter.SelectedItem.ToString().IndexOf(ch);
                filter = cbxFilter.SelectedItem.ToString().Substring(0, ind).Trim();
            }
            if (sysAddr == 0) return;
            if (!useLocalBase) ConnectMySQL();
            LoadPlanets(sysAddr, filter);
            if (!useLocalBase) DisconnectMySQL();
        }

        private void dgvPlanets_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (loadingData) return;
            if (planetsRowIndex == dgvPlanets.CurrentRow.Index) return;
            if (dgvPlanets.CurrentRow.IsNewRow) return;

            UInt64 sysAddr = 0;
            UInt64 planetID;

            planetsRowIndex = dgvPlanets.CurrentRow.Index;
            sysAddr = Convert.ToUInt64(dgvPlanets.CurrentRow.Cells["plSystemAddress"].Value);
            planetID = Convert.ToUInt64(dgvPlanets.CurrentRow.Cells["plPlanetID"].Value);
            if (sysAddr == 0 || planetID == 0) return;
            if (!useLocalBase) ConnectMySQL();
            LoadMaterials(sysAddr, planetID);
            LoadRings(sysAddr, planetID);
            if (!useLocalBase) DisconnectMySQL();
        }

        private void dgvRings_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (loadingData) return;
            if (ringsRowIndex == dgvRings.CurrentRow.Index) return;
            if (dgvRings.CurrentRow.IsNewRow) return;

            UInt64 sysAddr = 0;
            UInt64 planetID;
            string ringName;

            ringsRowIndex = dgvRings.CurrentRow.Index;
            sysAddr = Convert.ToUInt64(dgvRings.CurrentRow.Cells["rgSystemAddress"].Value);
            planetID = Convert.ToUInt64(dgvRings.CurrentRow.Cells["rgPlanetID"].Value);
            ringName = dgvRings.CurrentRow.Cells["rgRingName"].Value.ToString();
            if (sysAddr == 0 || planetID == 0 || ringName == "") return;
            if (!useLocalBase) ConnectMySQL();
            LoadSignals(sysAddr, planetID, ringName);
            if (!useLocalBase) DisconnectMySQL();
        }

        private void getEDSMDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EDSMInfo edsm = new EDSMInfo();
            string edsmRequest;
            string sysName;
            UInt64 sysAddr;
            string planetName;
            UInt64 planetID;
            int landable;
            string ringName;

            if (dgvSystems.CurrentRow.IsNewRow) return;
            sysName = dgvSystems.CurrentRow.Cells["sysStarSystem"].Value.ToString();
            //sysName = sysName.Replace("'", "`");
            sysName = WebUtility.UrlEncode(sysName);

            edsmRequest = "https://www.edsm.net/api-system-v1/bodies?systemName=" + sysName;

            WebRequest request = WebRequest.Create(edsmRequest);
            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;

            FormMessage formMessage = new FormMessage();
            formMessage.message = "Loading data from EDSM";
            formMessage.Show();
            Application.DoEvents();
            // Get the response.
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if(response.StatusCode != HttpStatusCode.OK)
            {
                formMessage.Close();
                MessageBox.Show("Error: " + response.StatusDescription);
                response.Close();
                return;
            }
            
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
            //обновляем данные по системе
            edsm = JsonConvert.DeserializeObject<EDSMInfo>(responseFromServer);
            sysAddr = Convert.ToUInt64(dgvSystems.CurrentRow.Cells["sysSystemAddress"].Value);
            sysName = dgvSystems.CurrentRow.Cells["sysStarSystem"].Value.ToString();

            if (!useLocalBase) ConnectMySQL();

            foreach (EDSMBody body in edsm.bodies)
            {
                if (body.type == "Star") continue;
                planetName = body.name.Replace(sysName, "").Trim();
                planetID = body.id;
                landable = (bool)body.isLandable ? 1 : 0;
                //удаленная база
                if (!useLocalBase)
                {
                    mysqlCmd.CommandText = @"SELECT COUNT(*)
                                                 FROM Planets
                                                 WHERE SysAddr = @SysAddr AND
                                                       PlanetName = @PlanetName";
                    mysqlCmd.Parameters.Clear();
                    mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.Int64).Value = sysAddr;
                    mysqlCmd.Parameters.Add("@PlanetName", MySqlDbType.String).Value = planetName;

                    if (MySqlCount(mysqlCmd) == 0)
                    {
                        mysqlCmd.CommandText = @"INSERT INTO Planets (SysAddr, PlanetID, PlanetName, Distance, Class, Volcanism, Landable, Biological, Geological, Reserve)
                                                    VALUES (@SysAddr, @PlanetID, @PlanetName, @Distance, @Class, @Volcanism, @Landable, @Biological, @Geological, @Reserve)";
                        mysqlCmd.Parameters.Clear();
                        mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = sysAddr;
                        mysqlCmd.Parameters.Add("@PlanetID", MySqlDbType.UInt64).Value = planetID;
                        mysqlCmd.Parameters.Add("@PlanetName", MySqlDbType.String).Value = planetName;
                        mysqlCmd.Parameters.Add("@Distance", MySqlDbType.Float).Value = body.distanceToArrival;
                        mysqlCmd.Parameters.Add("@Class", MySqlDbType.String).Value = body.subType;
                        mysqlCmd.Parameters.Add("@Volcanism", MySqlDbType.String).Value = (body.volcanismType == "No volcanism" ? "" : body.volcanismType);
                        mysqlCmd.Parameters.Add("@Landable", MySqlDbType.Byte).Value = Convert.ToByte(landable);
                        mysqlCmd.Parameters.Add("@Biological", MySqlDbType.Byte).Value = 0;
                        mysqlCmd.Parameters.Add("@Geological", MySqlDbType.Byte).Value = 0;
                        mysqlCmd.Parameters.Add("@Reserve", MySqlDbType.String).Value = "";
                        MySqlInsert(mysqlCmd);

                        if (body.rings != null)
                        {
                            foreach (EDSMRing ring in body.rings)
                            {
                                ringName = ring.name.Replace(body.name, "").Trim().ToString();
                                mysqlCmd.CommandText = @"INSERT INTO Rings (SysAddr, planetID, RingName, Class)
                                                            VALUES (@SysAddr, @planetID, @RingName, @Class)";
                                mysqlCmd.Parameters.Clear();
                                mysqlCmd.Parameters.Add("@SysAddr", MySqlDbType.UInt64).Value = sysAddr;
                                mysqlCmd.Parameters.Add("@planetID", MySqlDbType.UInt64).Value = planetID;
                                mysqlCmd.Parameters.Add("@RingName", MySqlDbType.String).Value = ringName;
                                mysqlCmd.Parameters.Add("@Class", MySqlDbType.String).Value = ring.type;
                                MySqlInsert(mysqlCmd);
                            }
                        }
                    }
                }
                //локальная база
                sqliteCmd.CommandText = @"SELECT COUNT(*)
                                                 FROM Planets
                                                 WHERE SysAddr = @SysAddr AND
                                                       PlanetName = @PlanetName";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@PlanetName", DbType.String).Value = planetName;

                if (SqliteCount(sqliteCmd) == 0)
                {
                    //если не найден добавляем
                    sqliteCmd.CommandText = @"INSERT INTO Planets (SysAddr, PlanetID, PlanetName, Distance, Class, Volcanism, Landable, Biological, Geological, Reserve)
                                                    VALUES (@SysAddr, @PlanetID, @PlanetName, @Distance, @Class, @Volcanism, @Landable, @Biological, @Geological, @Reserve)";
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@SysAddr", DbType.Int64).Value = sysAddr;
                    sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = planetID;
                    sqliteCmd.Parameters.Add("@PlanetName", DbType.String).Value = planetName;
                    sqliteCmd.Parameters.Add("@Distance", DbType.Double).Value = body.distanceToArrival;
                    sqliteCmd.Parameters.Add("@Class", DbType.String).Value = body.subType;
                    sqliteCmd.Parameters.Add("@Volcanism", DbType.String).Value = (body.volcanismType == "No volcanism" ? "" : body.volcanismType);
                    sqliteCmd.Parameters.Add("@Landable", DbType.Int32).Value = Convert.ToByte(landable);
                    sqliteCmd.Parameters.Add("@Biological", DbType.Int32).Value = 0;
                    sqliteCmd.Parameters.Add("@Geological", DbType.Int32).Value = 0;
                    sqliteCmd.Parameters.Add("@Reserve", DbType.String).Value = "";
                    SqliteInsert(sqliteCmd);
                    //доп. данные (хранятся только локально)
                    sqliteCmd.CommandText = @"INSERT INTO PlanetsLocal (SysAddr, PlanetID, HasImg)
                                                    VALUES (@SysAddr, @PlanetID, @HasImg)";
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                    sqliteCmd.Parameters.Add("@PlanetID", DbType.UInt64).Value = planetID;
                    sqliteCmd.Parameters.Add("@HasImg", DbType.Int32).Value = 0;
                    SqliteInsert(sqliteCmd);

                    if (body.rings != null)
                    {
                        foreach (EDSMRing ring in body.rings)
                        {
                            ringName = ring.name.Replace(body.name, "").Trim().ToString();
                            sqliteCmd.CommandText = @"INSERT INTO Rings (SysAddr, planetID, RingName, Class)
                                                            VALUES (@SysAddr, @planetID, @RingName, @Class)";
                            sqliteCmd.Parameters.Clear();
                            sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                            sqliteCmd.Parameters.Add("@planetID", DbType.UInt64).Value = planetID;
                            sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
                            sqliteCmd.Parameters.Add("@Class", DbType.String).Value = ring.type;
                            SqliteInsert(sqliteCmd);
                        }
                    }
                }
            }

            LoadPlanets(sysAddr, "");
            if (!useLocalBase) DisconnectMySQL();

            formMessage.Close();
        }

        private void lastVisitToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            dgvSystems.Columns["sysLastVisit"].Visible = lastVisitToolStripMenuItem.Checked;
            dgvSystems.Update();
        }

        private void setAsCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currX = Convert.ToDouble(dgvSystems.CurrentRow.Cells["sysStarPosX"].Value);
            currY = Convert.ToDouble(dgvSystems.CurrentRow.Cells["sysStarPosY"].Value);
            currZ = Convert.ToDouble(dgvSystems.CurrentRow.Cells["sysStarPosZ"].Value);

            sqliteCmd.CommandText = @"UPDATE CurrentCoordinates
                                    SET System = @System,
                                        X = @X,
                                        Y = @Y,
                                        Z = @Z
                                    WHERE rowid = @rowid";
            sqliteCmd.Parameters.Clear();
            sqliteCmd.Parameters.Add("@System", DbType.String).Value = dgvSystems.CurrentRow.Cells["sysStarSystem"].Value.ToString();
            sqliteCmd.Parameters.Add("@X", DbType.Double).Value = currX;
            sqliteCmd.Parameters.Add("@Y", DbType.Double).Value = currY;
            sqliteCmd.Parameters.Add("@Z", DbType.Double).Value = currZ;
            sqliteCmd.Parameters.Add("@rowid", DbType.Int32).Value = 1;
            SqliteUpdate(sqliteCmd);

             lblCurrentSystem.Text = dgvSystems.CurrentRow.Cells["sysStarSystem"].Value.ToString();
             ApplyFilter();
        }

        private void universalCartographicPricesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> imgFiles = new List<string>();
            FormImage formImg = new FormImage();

            imgFiles.Add(@"images\explore.jpg");
            formImg.imgFiles = imgFiles;
            formImg.Show();
        }

        private void useLocalBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            useLocalBase = useLocalBaseToolStripMenuItem.Checked;
            showEmptySystemsToolStripMenuItem.Enabled = useLocalBase;
            deleteEmptySystemsToolStripMenuItem.Enabled = useLocalBase;
            if (useLocalBase)
            {
                useLocalBaseToolStripMenuItem.BackColor = Color.LightGreen;
            }
            else
            {
                useLocalBaseToolStripMenuItem.BackColor = SystemColors.ControlDark;
                if(Properties.Settings.Default.MaxDistanse > 1000)
                {
                    Properties.Settings.Default.MaxDistanse = 1000;
                    Properties.Settings.Default.Save();
                    ApplyFilter();
                }
            }
        }

        private void addImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            
            opf.Title = "Select image";
            opf.Filter = "Image file|*.bmp;*.png;*.jpg";
            if (opf.ShowDialog() != DialogResult.OK) return;

            File.Copy(opf.FileName, @"userimages\" + opf.SafeFileName, true);

            string imgFile = @"userimages\" + opf.SafeFileName;
            UInt64 sysAddr = Convert.ToUInt64(dgvSystems.CurrentRow.Cells["sysSystemAddress"].Value);

            sqliteCmd.CommandText = @"SELECT COUNT(*)
                            FROM SystemsImg
                            WHERE SysAddr = @SysAddr AND Path = @Path";
            sqliteCmd.Parameters.Clear();
            sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
            sqliteCmd.Parameters.Add("@Path", DbType.String).Value = imgFile;
            if (SqliteCount(sqliteCmd) == 0)
            {
                //если не найден добавляем
                sqliteCmd.CommandText = @"INSERT INTO SystemsImg (SysAddr, Path)
                                                        VALUES (@SysAddr, @Path)";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@Path", DbType.String).Value = imgFile;
                SqliteInsert(sqliteCmd);
                //общее количество картинок
                sqliteCmd.CommandText = @"SELECT COUNT(*)
                            FROM SystemsImg
                            WHERE SysAddr = @SysAddr";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                long cou = SqliteCount(sqliteCmd);

                dgvSystems.CurrentRow.Cells["sysHasImg"].ReadOnly = false;
                dgvSystems.CurrentRow.Cells["sysHasImg"].Value = cou;
                dgvSystems.CurrentRow.Cells["sysHasImg"].ReadOnly = true;

                sqliteCmd.CommandText = @"UPDATE SystemsLocal
                                                  SET HasImg = @HasImg
                                                  WHERE SysAddr = @SysAddr";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@HasImg", DbType.UInt32).Value = cou;
                SqliteUpdate(sqliteCmd);
            }
        }
        private void showImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dt;
            UInt64 sysAddr = Convert.ToUInt64(dgvSystems.CurrentRow.Cells["sysSystemAddress"].Value);

            sqliteCmd.CommandText = @"SELECT Path
                            FROM SystemsImg
                            WHERE SysAddr = @SysAddr";
            sqliteCmd.Parameters.Clear();
            sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
            dt = SqliteSelect(sqliteCmd);
            long cou = dt.Rows.Count;
            if (cou == 0) return;

            List<string> imgFiles = new List<string>();
            FormImage formImg = new FormImage();
            foreach(DataRow row in dt.Rows)
            {
                imgFiles.Add(row["Path"].ToString());
            }
            formImg.imgFiles = imgFiles;
            formImg.ShowDialog();
            //удаление неправильных путей к картинкам
            List<string> brokenPaths = formImg.brokenPaths;
            if (brokenPaths != null)
            {
                dgvSystems.CurrentRow.Cells["sysHasImg"].ReadOnly = false;
                foreach (string elem in brokenPaths)
                {
                    sqliteCmd.CommandText = @"DELETE
                            FROM SystemsImg
                            WHERE Path = @Path";
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@Path", DbType.String).Value = elem;
                    SqliteDelete(sqliteCmd);
                    dgvSystems.CurrentRow.Cells["sysHasImg"].Value = --cou;
                }
                dgvSystems.CurrentRow.Cells["sysHasImg"].ReadOnly = true;
                sqliteCmd.CommandText = @"UPDATE SystemsLocal
                                                  SET HasImg = @HasImg
                                                  WHERE SysAddr = @SysAddr";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@HasImg", DbType.UInt32).Value = cou;
                SqliteUpdate(sqliteCmd);
            }
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.DonateAddr);
        }

        private void setMaxDistanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMaxDistanse setDist = new FormMaxDistanse();
            setDist.maxDist = Properties.Settings.Default.MaxDistanse;
            setDist.useLocalBase = useLocalBase;
            setDist.ShowDialog();
            Properties.Settings.Default.MaxDistanse = Convert.ToDouble(setDist.maxDist);
            Properties.Settings.Default.Save();
            tbxSystemFilter.Text = "";
            ApplyFilter();
        }

        private void lblLogFolder_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.LastLogPath == "") return;
            GetLogData(Properties.Settings.Default.LastLogPath);
        }

        private void toolStripMenuHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.HelpAddr);
        }

        private void addImageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Title = "Select image";
            opf.Filter = "Image file|*.bmp;*.png;*.jpg";
            if (opf.ShowDialog() != DialogResult.OK) return;

            File.Copy(opf.FileName, @"userimages\" + opf.SafeFileName, true);

            string imgFile = @"userimages\" + opf.SafeFileName;
            UInt64 sysAddr = Convert.ToUInt64(dgvPlanets.CurrentRow.Cells["plSystemAddress"].Value);
            UInt64 planetID = Convert.ToUInt64(dgvPlanets.CurrentRow.Cells["plPlanetID"].Value);

            sqliteCmd.CommandText = @"SELECT COUNT(*)
                            FROM PlanetsImg
                            WHERE SysAddr = @SysAddr AND PlanetID = @planetID AND Path = @Path";
            sqliteCmd.Parameters.Clear();
            sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
            sqliteCmd.Parameters.Add("@planetID", DbType.UInt64).Value = planetID;
            sqliteCmd.Parameters.Add("@Path", DbType.String).Value = imgFile;

            if (SqliteCount(sqliteCmd) == 0)
            {
                //если не найден добавляем
                sqliteCmd.CommandText = @"INSERT INTO PlanetsImg (SysAddr, PlanetID, Path)
                                                        VALUES (@SysAddr, @PlanetID, @Path)";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@planetID", DbType.UInt64).Value = planetID;
                sqliteCmd.Parameters.Add("@Path", DbType.String).Value = imgFile;
                SqliteInsert(sqliteCmd);
                //общее количество картинок
                sqliteCmd.CommandText = @"SELECT COUNT(*)
                            FROM PlanetsImg
                            WHERE SysAddr = @SysAddr AND PlanetID = @planetID";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@planetID", DbType.UInt64).Value = planetID;
                long cou = SqliteCount(sqliteCmd);

                dgvPlanets.CurrentRow.Cells["plHasImg"].ReadOnly = false;
                dgvPlanets.CurrentRow.Cells["plHasImg"].Value = cou;
                dgvPlanets.CurrentRow.Cells["plHasImg"].ReadOnly = true;

                sqliteCmd.CommandText = @"UPDATE PlanetsLocal
                                                  SET HasImg = @HasImg
                                                  WHERE SysAddr = @SysAddr AND PlanetID = @planetID";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@planetID", DbType.UInt64).Value = planetID;
                sqliteCmd.Parameters.Add("@HasImg", DbType.UInt32).Value = cou;
                SqliteUpdate(sqliteCmd);
            }
        }

        private void showImagesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataTable dt;
            UInt64 sysAddr = Convert.ToUInt64(dgvPlanets.CurrentRow.Cells["plSystemAddress"].Value);
            UInt64 planetID = Convert.ToUInt64(dgvPlanets.CurrentRow.Cells["plPlanetID"].Value);

            sqliteCmd.CommandText = @"SELECT Path
                            FROM PlanetsImg
                            WHERE SysAddr = @SysAddr AND PlanetID = @planetID";
            sqliteCmd.Parameters.Clear();
            sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
            sqliteCmd.Parameters.Add("@planetID", DbType.UInt64).Value = planetID;
            dt = SqliteSelect(sqliteCmd);
            long cou = dt.Rows.Count;
            if (cou == 0) return;

            List<string> imgFiles = new List<string>();
            FormImage formImg = new FormImage();
            foreach (DataRow row in dt.Rows)
            {
                imgFiles.Add(row["Path"].ToString());
            }
            formImg.imgFiles = imgFiles;
            formImg.ShowDialog();
            //удаление неправильных путей к картинкам
            List<string> brokenPaths = formImg.brokenPaths;
            if (brokenPaths != null)
            {
                dgvPlanets.CurrentRow.Cells["plHasImg"].ReadOnly = false;
                foreach (string elem in brokenPaths)
                {
                    sqliteCmd.CommandText = @"DELETE
                            FROM PlanetsImg
                            WHERE Path = @Path";
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@Path", DbType.String).Value = elem;
                    SqliteDelete(sqliteCmd);
                    dgvPlanets.CurrentRow.Cells["plHasImg"].Value = --cou;
                }
                dgvPlanets.CurrentRow.Cells["plHasImg"].ReadOnly = true;
                sqliteCmd.CommandText = @"UPDATE PlanetsLocal
                                                  SET HasImg = @HasImg
                                                  WHERE SysAddr = @SysAddr AND PlanetID = @planetID";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@planetID", DbType.UInt64).Value = planetID;
                sqliteCmd.Parameters.Add("@HasImg", DbType.UInt32).Value = cou;
                SqliteUpdate(sqliteCmd);
            }
        }

        private void addImageToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();

            opf.Title = "Select image";
            opf.Filter = "Image file|*.bmp;*.png;*.jpg";
            if (opf.ShowDialog() != DialogResult.OK) return;

            File.Copy(opf.FileName, @"userimages\" + opf.SafeFileName, true);

            string imgFile = @"userimages\" + opf.SafeFileName;
            UInt64 sysAddr = Convert.ToUInt64(dgvSignals.CurrentRow.Cells["sigSystemAddress"].Value);
            UInt64 planetID = Convert.ToUInt64(dgvSignals.CurrentRow.Cells["sigPlanetID"].Value);
            string ringName = dgvSignals.CurrentRow.Cells["sigRing"].Value.ToString().Trim();
            string type = dgvSignals.CurrentRow.Cells["sigType"].Value.ToString().Trim();

            sqliteCmd.CommandText = @"SELECT COUNT(*)
                            FROM SignalsImg
                            WHERE SysAddr = @SysAddr AND PlanetID = @planetID AND 
                                  RingName = @RingName AND Type = @Type AND Path = @Path";
            sqliteCmd.Parameters.Clear();
            sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
            sqliteCmd.Parameters.Add("@planetID", DbType.UInt64).Value = planetID;
            sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
            sqliteCmd.Parameters.Add("@Type", DbType.String).Value = type;
            sqliteCmd.Parameters.Add("@Path", DbType.String).Value = imgFile;

            if (SqliteCount(sqliteCmd) == 0)
            {
                //если не найден добавляем
                sqliteCmd.CommandText = @"INSERT INTO SignalsImg (SysAddr, PlanetID, RingName, Type, Path)
                                                        VALUES (@SysAddr, @PlanetID, @RingName, @Type, @Path)";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@planetID", DbType.UInt64).Value = planetID;
                sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
                sqliteCmd.Parameters.Add("@Type", DbType.String).Value = type;
                sqliteCmd.Parameters.Add("@Path", DbType.String).Value = imgFile;
                SqliteInsert(sqliteCmd);
                //общее количество картинок
                sqliteCmd.CommandText = @"SELECT COUNT(*)
                            FROM SignalsImg
                            WHERE SysAddr = @SysAddr AND PlanetID = @planetID AND 
                                  RingName = @RingName AND Type = @Type";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@planetID", DbType.UInt64).Value = planetID;
                sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
                sqliteCmd.Parameters.Add("@Type", DbType.String).Value = type;
                long cou = SqliteCount(sqliteCmd);

                dgvSignals.CurrentRow.Cells["sigHasImg"].ReadOnly = false;
                dgvSignals.CurrentRow.Cells["sigHasImg"].Value = cou;
                dgvSignals.CurrentRow.Cells["sigHasImg"].ReadOnly = true;

                sqliteCmd.CommandText = @"UPDATE SignalsLocal
                                                  SET HasImg = @HasImg
                                                  WHERE SysAddr = @SysAddr AND PlanetID = @planetID AND 
                                                        RingName = @RingName AND Type = @Type";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@planetID", DbType.UInt64).Value = planetID;
                sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
                sqliteCmd.Parameters.Add("@Type", DbType.String).Value = type;
                sqliteCmd.Parameters.Add("@HasImg", DbType.UInt32).Value = cou;
                SqliteUpdate(sqliteCmd);
            }
        }

        private void showImagesToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataTable dt;
            UInt64 sysAddr = Convert.ToUInt64(dgvSignals.CurrentRow.Cells["sigSystemAddress"].Value);
            UInt64 planetID = Convert.ToUInt64(dgvSignals.CurrentRow.Cells["sigPlanetID"].Value);
            string ringName = dgvSignals.CurrentRow.Cells["sigRing"].Value.ToString().Trim();
            string type = dgvSignals.CurrentRow.Cells["sigType"].Value.ToString().Trim();

            sqliteCmd.CommandText = @"SELECT Path
                            FROM SignalsImg
                            WHERE SysAddr = @SysAddr AND PlanetID = @planetID AND 
                                  RingName = @RingName AND Type = @Type";
            sqliteCmd.Parameters.Clear();
            sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
            sqliteCmd.Parameters.Add("@planetID", DbType.UInt64).Value = planetID;
            sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
            sqliteCmd.Parameters.Add("@Type", DbType.String).Value = type;
            dt = SqliteSelect(sqliteCmd);
            long cou = dt.Rows.Count;
            if (cou == 0) return;

            List<string> imgFiles = new List<string>();
            FormImage formImg = new FormImage();
            foreach (DataRow row in dt.Rows)
            {
                imgFiles.Add(row["Path"].ToString());
            }
            formImg.imgFiles = imgFiles;
            formImg.ShowDialog();
            //удаление неправильных путей к картинкам
            List<string> brokenPaths = formImg.brokenPaths;
            if (brokenPaths != null)
            {
                dgvSignals.CurrentRow.Cells["sigHasImg"].ReadOnly = false;
                foreach (string elem in brokenPaths)
                {
                    sqliteCmd.CommandText = @"DELETE
                            FROM SignalsImg
                            WHERE Path = @Path";
                    sqliteCmd.Parameters.Clear();
                    sqliteCmd.Parameters.Add("@Path", DbType.String).Value = elem;
                    SqliteDelete(sqliteCmd);
                    dgvSignals.CurrentRow.Cells["sigHasImg"].Value = --cou;
                }
                dgvSignals.CurrentRow.Cells["sigHasImg"].ReadOnly = true;
                sqliteCmd.CommandText = @"UPDATE SignalsLocal
                                                  SET HasImg = @HasImg
                                                  WHERE SysAddr = @SysAddr AND PlanetID = @planetID AND 
                                                        RingName = @RingName AND Type = @Type";
                sqliteCmd.Parameters.Clear();
                sqliteCmd.Parameters.Add("@SysAddr", DbType.UInt64).Value = sysAddr;
                sqliteCmd.Parameters.Add("@planetID", DbType.UInt64).Value = planetID;
                sqliteCmd.Parameters.Add("@RingName", DbType.String).Value = ringName;
                sqliteCmd.Parameters.Add("@Type", DbType.String).Value = type;
                sqliteCmd.Parameters.Add("@HasImg", DbType.UInt32).Value = cou;
                SqliteUpdate(sqliteCmd);
            }
        }
    }
}
