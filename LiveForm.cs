
using MathNet.Numerics;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Tls;
using Security;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.IO;

using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

using System.Runtime.InteropServices;
using System.Security.Cryptography;

using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Forms;
//using WSPR_Live;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static WSPR_Live.LiveForm;

namespace WSPR_Live
{
    public partial class LiveForm : Form
    {



        public string originalcall = "G4GCI";
        private string[] cells = new string[22];
        private string Callsign = "G4GCI";
        private bool databaseError = false;

        int timespan = 10; //timespan x minutes
        int liveLimit = 1000;
        int maxrows = 2500;
        bool update = false;
        string dateformat = "yyyy-MM-dd";

        int OpSystem = 0; //default to Windows
        string slash = "\\"; //default to Windows

        int startCount = 0;
        int startCountMax = 5; //300; //<5 mins

        string db_server = "127.0.0.1";
        string db_user = "admin";
        string db_pass = "wspr";
        string vers = "";


        string headerline = "";

        bool owncall = true;    //use own call not other call

        MessageClass Msg = new MessageClass();

        public bool stopUrl = false;
        private static readonly object _lock = new object();

        public LiveForm()
        {

            InitializeComponent();

            //dataGridView1.Font = new System.Drawing.Font("Consolas", 9); // Set font to Arial with size 12
        }

        private async void LiveForm_Load(object sender, EventArgs e)
        {
            System.Version version = Assembly.GetExecutingAssembly().GetName().Version;
            vers = "0.1.12";


            int index = PlistBox.TopIndex;
            string text = PlistBox.Items[index].ToString();
            PlistBox.SelectedIndex = index;

            callFiltertextBox.CharacterCasing = CharacterCasing.Upper;
            calltextBox.CharacterCasing = CharacterCasing.Upper;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                OpSystem = 0; //Windows
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                OpSystem = 1; //Linux
                slash = "/"; //Linux uses forward slash

            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                OpSystem = 2; //MacOS
                slash = "/"; //MacOS uses forward slash

            }
            else if (OperatingSystem.IsAndroid())
            {
                OpSystem = 3; //Android
                slash = "/"; //Android uses forward slash

            }
            db_server = "127.0.0.1";
            db_user = "admin";
            getUserandPassword();
            set_header(Callsign, db_server, db_user, db_pass);
            await Task.Delay(2000);
            int min = 30;
            await get_results(Callsign, "", db_server, db_user, db_pass, min, owncall);


        }
        public void set_header(string call, string serverName, string db_user, string db_pass)
        {
            headerline = "Received transmissions for: " + call + "                WSPR Scheduler Live  V." + vers + "    GNU GPLv3 License"; ;
            this.Text = headerline;
            originalcall = call;

            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm";
            //dateTimePicker1.ShowUpDown = true;
            bandlistBox.SelectedIndex = 0;

        }

        public struct RX_data
        {
            public Int64 id;
            public DateTime time;
            public Int16 band;
            public string rx_sign;
            public float rx_lat;
            public float rx_lon;
            public string rx_loc;
            public string tx_sign;
            public float tx_lat;
            public float tx_lon;
            public string tx_loc;
            public int distance;
            public int azimuth;
            public int rx_azimuth;
            public int frequency;
            public Int16 power;
            public Int16 snr;
            public Int16 drift;
            public string version;
            public Int16 code;
        }
        RX_data RX = new RX_data();



        DataTable RXtable = new DataTable();

        private async void testDB()
        {
            bool reply = await SQL_Get();
            if (!reply)
            {
                Msg.TMessageBox("Connection error", "", 1500);
            }
            else
            {
                Msg.TMessageBox("Connection OK", "", 1500);
            }

        }

        private async Task getUserandPassword()
        {
            string key = "wsproundtheworld";
            Encryption enc = new Encryption();

            string encryptedpassword;
            string content = "";
            string dbcall = "";
            bool ok = false;

            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string filepath = homeDirectory;
            //string content = "db_user: " + user + " db_pass: " + passwordhash;

            if (Path.Exists(filepath))
            {

                if (filepath.EndsWith(slash))
                {
                    slash = "";
                }
                filepath = filepath + slash + "DBcredential";
                if (System.IO.File.Exists(filepath))
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(filepath))
                        {
                            content = reader.ReadLine();
                            dbcall = reader.ReadLine();
                            reader.Close();
                        }
                        if (content != null || content != "")
                        {
                            if (content.Contains("db_pass:"))
                            {
                                encryptedpassword = content.Substring(content.IndexOf("db_pass: ") + "db_pass: ".Length);
                                string password = enc.Decrypt(encryptedpassword, key);
                                if (password.Length > 0 && password != null)
                                {
                                    db_pass = password;


                                    ok = true;
                                }
                            }
                            if (dbcall != null && dbcall != "")
                            {
                                if (dbcall.Contains("db_call:"))
                                {
                                    dbcall = dbcall.Replace("db_call: ", "").Trim();
                                    if (dbcall != null && dbcall != "")
                                    {
                                        Callsign = dbcall;
                                    }
                                }
                            }
                        }

                        if (!ok)
                        {
                            Msg.TMessageBox("Unable to read database credentials", "", 1000);

                        }
                    }
                    catch (Exception ex)
                    {
                        Msg.TMessageBox("Unable to read database credentials", "", 1000);

                    }
                }
            }

        }

        private void clearRX()
        {
            RX.id = 0;
            RX.time = DateTime.MinValue;
            RX.band = 0;
            RX.rx_sign = "";
            RX.rx_lat = 0;
            RX.rx_lon = 0;
            RX.rx_loc = "";
            RX.tx_sign = "";
            RX.tx_lat = 0;
            RX.tx_lon = 0;
            RX.tx_loc = "";
            RX.distance = 0;
            RX.azimuth = 0;
            RX.rx_azimuth = 0;
            RX.frequency = 0;
            RX.power = 0;
            RX.snr = 0;
            RX.drift = 0;
            RX.version = "";
            RX.code = 0;
        }
        public async Task process_data(string data)
        {
            clearRX();
            try
            {
                string[] R = data.Split('\t');
                Int64.TryParse(R[0].Trim(), out RX.id);

                RX.time = Convert.ToDateTime(R[1]);

                Int16.TryParse(R[2], out RX.band);

                RX.rx_sign = R[3];

                float.TryParse(R[4], out RX.rx_lat);

                float.TryParse(R[5], out RX.rx_lon);
                RX.rx_loc = R[6];
                RX.tx_sign = R[7];
                float.TryParse(R[8], out RX.tx_lat);
                float.TryParse(R[9], out RX.tx_lon);
                RX.tx_loc = R[10];
                Int32.TryParse(R[11], out RX.distance);
                Int32.TryParse(R[12], out RX.azimuth);
                Int32.TryParse(R[13], out RX.rx_azimuth);
                Int32.TryParse(R[14], out RX.frequency);
                Int16.TryParse(R[15], out RX.power);

                Int16.TryParse(R[16], out RX.snr);
                Int16.TryParse(R[17], out RX.drift);

                RX.version = R[18];
                Int16.TryParse(R[19], out RX.code);
            }
            catch
            {

            }

        }

        private string convert_to_miles(int km)
        {
            try
            {
                int m = 0;
                double miles = 0;
                miles = km * 0.621371;
                m = Convert.ToInt32(miles);
                return m.ToString();
            }
            catch
            {
                return "0";
            }
        }

        private void updatebutton_Click(object sender, EventArgs e)
        {
            updateResults();
        }
        private async void updateResults()
        {
            //MessageForm nForm = new MessageForm();
            Msg.TMessageBox("Please wait - retrieving local data ....", "", 30000);
            await show_results();
            //nForm.Dispose();
        }

        public async Task<bool> SQL_Get()
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetStringAsync("http://db1.wspr.live/?query=SELECT%20version()");
                if (response != "" || response != null)
                {

                    MessageBox.Show("SQL version: " + response + " - successful GET");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Error connecting");
                return false;
            }
        }
        public async Task<bool> checkSQL()
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetStringAsync("http://db1.wspr.live/?query=SELECT%20version()");
                if (response != "" || response != null)
                {
                    return true;

                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        public async Task get_results(string call, string freq, string server, string db_user, string db_pass, int timespan, bool owncall)
        {
            //note: band not currently used

            bool isUnlocked = false;
            bool found = false;
            int tries = 0;
            if (updatecheckBox.Checked)
            {
                Msg.TMessageBox("Updates disabled", "", 2000);
                return;
            }

            if (stopUrl)
            {
                return;
            }
            if (!await checkSQL())
            {
                return;
            }
            //MessageForm nForm = new MessageForm();

            while (!isUnlocked)
            {
                //timespan eg. last 5 minutes, limit eg. 500 - no. of entries to retrieve
                Msg.TMessageBox("Please wait - retrieving data ....", "", 30000);
                try
                {

                    int band = 0;

                    //note livelimit  is 1000 - max number of entries to extract from wspr.live database
                    using var client = new HttpClient();

                    string baseUrl = "http://db1.wspr.live/";
                    string sqlQuery = $"SELECT * FROM wspr.rx WHERE tx_sign LIKE '%{call}%' AND time >= subtractMinutes(now(), {timespan}) AND time <= subtractMinutes(now(), 2) LIMIT {liveLimit}";
                    string encodedQuery = Uri.EscapeDataString(sqlQuery);
                    string requestUrl = $"{baseUrl}?query={encodedQuery}";

                    using var stream = await client.GetStreamAsync(requestUrl);
                    using var reader = new StreamReader(stream);

                    string line = "";


                    while ((line = reader.ReadLine()) != null || !reader.EndOfStream)
                    {
                        if (line != null && line != "")
                        {
                            if (!found)
                            {
                                if (!owncall)
                                {
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                                }
                            }
                            found = true;
                            await process_data(line);

                            if (owncall)
                            {
                                await Save_Received(server, db_user, db_pass);
                            }
                            else //if other call then just fill grid
                            {
                                fill_cells();
                            }
                        }

                    }


                    isUnlocked = true;


                    await Task.Delay(1000);

                    if (owncall)
                    {
                        await show_results();
                    }
                    else
                    {
                        dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);  //order by date
                    }

                }
                catch
                {
                    if (tries > 3)
                    {
                        isUnlocked = true;
                    }
                    Thread.Sleep(800);
                    tries++;

                }

            }
            //nForm.Dispose();

        }




        private async Task show_results() // read back from the reported table to populate the datagridview
        {
            try
            {


                int rows = table_count();
                if (rows > 0)
                {
                    await find_received(rows);
                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);  //order by date                    
                }

            }
            catch
            {

            }

        }

        private int table_count()
        {
            int count = 0;
            string connectionString = "server=" + db_server + ";user id=" + db_user + ";password=" + db_pass + ";database=wspr_rx";
            var connection = new MySqlConnection(connectionString);
            try
            {
                //string connectionString = "Server=server;Port=3306;Database=wspr;User ID=user;Password=pass;";

                using (connection)
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SELECT COUNT(*) FROM reported", connection))
                    {
                        count = Convert.ToInt32(command.ExecuteScalar());
                    }
                    connection.Close();
                }
                return count;

            }
            catch
            {
                connection.Close();
                return 0;
            }
        }


        private async Task<bool> find_received(int tablecount) //find a slot row for display in grid from the database corresponding to the date/time from the slot        
        {
            DataTable Slots = new DataTable();
            //DateTime d = new DateTime();
            int i = 0;
            bool found = false;
            string myConnectionString = "server=" + db_server + ";user id=" + db_user + ";password=" + db_pass + ";database=wspr_rx";
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            if (!databaseError)
            {
                lock (_lock)
                {
                    try
                    {


                        connection.Open();

                        MySqlCommand command = connection.CreateCommand();

                        //SELECT* FROM your_table ORDER BY your_date_column DESC LIMIT 500;

                        command.CommandText = "SELECT * FROM reported ORDER BY time DESC LIMIT " + maxrows;
                        MySqlDataReader Reader;
                        Reader = command.ExecuteReader();

                        while (Reader.Read())
                        {


                            if (i < maxrows - 1 && i < tablecount - 1)    //only show first maxrows rows, or to length of reported table
                            {
                                if (!found)
                                {
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);
                                }
                                found = true;
                                clearRX();
                                RX.rx_sign = "";
                                RX.time = (DateTime)Reader["time"];
                                RX.band = (Int16)Reader["band"];
                                RX.rx_sign = (string)Reader["rx_sign"];
                                RX.rx_loc = (string)Reader["rx_loc"];
                                RX.tx_sign = (string)Reader["tx_sign"];
                                RX.tx_loc = (string)Reader["tx_loc"];
                                RX.distance = (int)Reader["distance"];
                                RX.azimuth = (int)Reader["azimuth"];
                                RX.frequency = (int)Reader["frequency"];
                                RX.power = (Int16)Reader["power"];
                                RX.snr = (Int16)Reader["snr"];
                                RX.drift = (Int16)Reader["drift"];
                                RX.version = (string)Reader["version"];
                                if (RX.rx_sign != "" && RX.rx_sign != null)
                                {
                                    fill_cells();
                                }

                                i++;
                            }
                            else
                            {
                                break;
                            }

                        }
                        Reader.Close();
                        connection.Close();
                        databaseError = false;


                    }
                    catch
                    {

                        //databaseError = true; //stop wasting time trying to connect if database error - ignore for present
                        found = false;
                        connection.Close();

                    }
                }
            }
            return found;
        }

        private void fill_cells()
        {
            //clearcells();
            try
            {
                cells[0] = RX.time.ToString("yyyy-MM-dd HH:mm"); //time
                cells[1] = RX.tx_sign; //tx sign
                double f = Convert.ToDouble(RX.frequency);
                f = f / 1000000;
                string formattedF = f.ToString("F6");
                cells[2] = formattedF; //freq
                string snr = Convert.ToString(RX.snr);
                if (RX.snr > 0)
                {
                    snr = "+" + snr;
                }
                cells[3] = snr;  //snr
                cells[4] = RX.drift.ToString();  //drift
                cells[5] = RX.tx_loc;  //tx loc
                cells[6] = RX.power.ToString();   //power dBm
                cells[7] = RX.rx_sign;  //reporter
                cells[8] = RX.rx_loc;    //rx loc

                cells[9] = RX.distance.ToString();   //km
                int km = Convert.ToInt32(RX.distance);    //miles
                cells[10] = convert_to_miles(km);
                cells[11] = RX.azimuth.ToString();
                cells[12] = RX.version;   //version
            }
            catch
            {

            }
            update_grid(); //add this row to the datagridview
        }


        private void filter_results(bool version, string ver)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);  //order by date
            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;
            //dt = dt.AddHours(-2);
            string from = dt1.ToString("yyyy-MM-dd HH:mm:00");
            string to = dt2.ToString("yyyy-MM-dd HH:mm:00");
            int rows = table_count();
            int band = get_band(bandlistBox.SelectedIndex);
            if (rows > 0)
            {
                find_selected(from, to, band, rows, version, ver);

            }

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);  //order by date
        }



        private int get_band(int bandno)
        {
            int b = -2; //all
            switch (bandno)
            {
                case 0:
                    b = -2; //all
                    break;
                case 1:
                    b = -1; //lf
                    break;
                case 2:
                    b = 0;  //mf
                    break;
                case 3:
                    b = 1;  //1.8
                    break;
                case 4:
                    b = 3;
                    break;
                case 5:
                    b = 5;
                    break;
                case 6:
                    b = 7;
                    break;
                case 7:
                    b = 10;
                    break;
                case 8:
                    b = 13;
                    break;
                case 9:
                    b = 14;
                    break;
                case 10:
                    b = 18;
                    break;
                case 11:
                    b = 21;
                    break;
                case 12:
                    b = 24;
                    break;
                case 13:
                    b = 28;
                    break;
                case 14:
                    b = 40;
                    break;
                case 15:
                    b = 50;
                    break;
                case 16:
                    b = 70;
                    break;
                case 17:
                    b = 144;
                    break;
                case 18:
                    b = 432;
                    break;
                case 19:
                    b = 1296;
                    break;
                default:
                    b = -2; //all
                    break;
            }
            return b;
        }

        private void clearcells()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = "";
            }
        }
        private bool find_selected(string time1, string time2, int band, int tablecount, bool version, string ver) //find a slot row for display in grid from the database corresponding to the date/time from the slot
        {
            DataTable Slots = new DataTable();
            //DateTime d = new DateTime();
            int i = 0;
            bool found = false;
            string myConnectionString = "server=" + db_server + ";user id=" + db_user + ";password=" + db_pass + ";database=wspr_rx";

            string bandstr = "";
            string q = "";
            if (band == -2) //all bands
            {
                bandstr = "-1";
                q = ">=";
            }
            else
            {
                bandstr = band.ToString();
                q = "=";
            }
            string callstr = "";
            string fromstr = "";
            string tostr = "";
            if (!databaseError)
            {
                MySqlConnection connection = new MySqlConnection(myConnectionString);
                try
                {


                    connection.Open();

                    MySqlCommand command = connection.CreateCommand();

                    if (callFiltertextBox.Text.Trim() != "")
                    {
                        if (callFiltertextBox.Text.Contains("*"))
                        {
                            callFiltertextBox.Text = callFiltertextBox.Text.Replace("*", "");
                        }
                        callstr = " AND rx_sign LIKE '" + callFiltertextBox.Text.Trim() + "%' ";
                    }
                    fromstr = DFromtextBox.Text.Trim();
                    tostr = DTotextBox.Text.Trim();
                    if (fromstr != "")
                    {
                        if (!kmcheckBox.Checked)
                        {
                            Double k = Convert.ToInt32(fromstr);
                            k = k * 1.609;
                            int K = (int)k;
                            fromstr = K.ToString();
                        }
                        fromstr = " AND distance >= " + fromstr + " ";
                    }
                    if (tostr != "")
                    {
                        if (!kmcheckBox.Checked)
                        {
                            Double k = Convert.ToInt32(tostr);
                            k = k * 1.609;
                            int K = (int)k;
                            tostr = K.ToString();
                        }
                        tostr = " AND distance <= " + tostr + " ";
                    }
                    //command.CommandText = "SELECT * FROM reported ORDER BY time WHERE time >= '" + time1 + "' AND time <= '" + time2 + "' AND band = '" + bandstr + "' DESC LIMIT " + maxrows;
                    if (version)
                    {
                        command.CommandText = "SELECT * FROM reported WHERE version = '" + ver + "'";
                    }
                    else if (datecheckBox.Checked)
                    {
                        command.CommandText = "SELECT * FROM reported WHERE time >= '" + time1 + "' AND time <= '" + time2 + "' AND band " + q + " '" + bandstr + "' " + callstr + fromstr + tostr + " ORDER BY time DESC LIMIT " + maxrows;
                    }
                    else
                    {
                        command.CommandText = "SELECT * FROM reported WHERE band " + q + " " + bandstr + " " + callstr + fromstr + tostr + " ORDER BY time DESC LIMIT " + maxrows;
                    }



                    MySqlDataReader Reader;
                    Reader = command.ExecuteReader();


                    while (Reader.Read())
                    {
                        found = true;

                        if (i < maxrows && i < tablecount)   //only show first maxrows rows, or to length of reported table
                        {
                            clearRX();
                            clearcells();
                            try
                            {
                                RX.rx_sign = "";
                                RX.time = (DateTime)Reader["time"];
                                RX.band = (Int16)Reader["band"];
                                RX.rx_sign = (string)Reader["rx_sign"];
                                RX.rx_loc = (string)Reader["rx_loc"];
                                RX.tx_sign = (string)Reader["tx_sign"];
                                RX.tx_loc = (string)Reader["tx_loc"];
                                RX.distance = (int)Reader["distance"];
                                RX.azimuth = (int)Reader["azimuth"];
                                RX.frequency = (int)Reader["frequency"];
                                RX.power = (Int16)Reader["power"];
                                RX.snr = (Int16)Reader["snr"];
                                RX.drift = (Int16)Reader["drift"];
                                RX.version = (string)Reader["version"];
                            }
                            catch
                            {
                            }

                            if (RX.rx_sign != "" && RX.rx_sign != null)
                            {
                                try
                                {
                                    cells[0] = RX.time.ToString("yyyy-MM-dd HH:mm"); //time
                                    cells[1] = RX.tx_sign; //tx sign
                                    double f = Convert.ToDouble(RX.frequency);
                                    f = f / 1000000;
                                    string formattedF = f.ToString("F6");
                                    cells[2] = formattedF; //freq
                                    string snr = Convert.ToString(RX.snr);
                                    if (RX.snr > 0)
                                    {
                                        snr = "+" + snr;
                                    }
                                    cells[3] = snr;  //snr
                                    cells[4] = RX.drift.ToString();  //drift
                                    cells[5] = RX.tx_loc;  //tx loc
                                    cells[6] = RX.power.ToString();   //power dBm
                                    cells[7] = RX.rx_sign;  //reporter
                                    cells[8] = RX.rx_loc;    //rx loc

                                    cells[9] = RX.distance.ToString();   //km
                                    int km = Convert.ToInt32(RX.distance);    //miles
                                    cells[10] = convert_to_miles(km);
                                    cells[11] = RX.azimuth.ToString();
                                    cells[12] = RX.version;   //version
                                    update_grid(); //add this row to the datagridview
                                }
                                catch
                                {
                                }
                            }
                            i++;
                        }
                        else
                        {
                            break;
                        }

                    }
                    Reader.Close();
                    connection.Close();
                    databaseError = false;

                }
                catch
                {

                    //databaseError = true; //stop wasting time trying to connect if database error - ignore for present
                    found = false;
                    connection.Close();

                }
            }
            return found;
        }
        private void update_grid() //add rows to the datagridview
        {

            DataGridViewRow row = new DataGridViewRow();
            try
            {
                row.CreateCells(dataGridView1);
                for (int i = 0; i < 13; i++)
                {

                    row.Cells[i].Value = cells[i];
                }

                dataGridView1.Rows.Add(row);
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.AllowUserToAddRows = false;
                }
            }
            catch
            {

            }
        }
        public async Task Save_Received(string serverName, string db_user, string db_pass)
        {

            DateTime date = new DateTime();

            string myConnectionString = "server=" + serverName + ";user id=" + db_user + ";password=" + db_pass + ";database=wspr_rx";
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = myConnectionString;
            MySqlCommand command = connection.CreateCommand();

            lock (_lock)
            {
                try
                {

                    command.CommandText = "INSERT IGNORE INTO reported(id,time,band,rx_sign,rx_lat,rx_lon,rx_loc,tx_sign,tx_lat,tx_lon,tx_loc,distance,azimuth,rx_azimuth,frequency,power,snr,drift,version,code) ";
                    command.CommandText += "VALUES(@id,@time,@band,@rx_sign,@rx_lat,@rx_lon,@rx_loc,@tx_sign,@tx_lat,@tx_lon,@tx_loc,@distance,@azimuth,@rx_azimuth,@frequency,@power,@snr,@drift,@version,@code)";
                    connection.Open();

                    //TimeSpan time = Convert.ToDateTime(cells[1]);
                    command.Parameters.AddWithValue("@id", RX.id);
                    command.Parameters.AddWithValue("@time", RX.time);
                    command.Parameters.AddWithValue("@band", RX.band);
                    command.Parameters.AddWithValue("@rx_sign", RX.rx_sign);
                    command.Parameters.AddWithValue("@rx_lat", RX.rx_lat);
                    command.Parameters.AddWithValue("@rx_lon", RX.rx_lon);
                    command.Parameters.AddWithValue("@rx_loc", RX.rx_loc);
                    command.Parameters.AddWithValue("@tx_sign", RX.tx_sign);
                    command.Parameters.AddWithValue("@tx_lat", RX.tx_lat);
                    command.Parameters.AddWithValue("@tx_lon", RX.tx_lon);
                    command.Parameters.AddWithValue("@tx_loc", RX.tx_loc);
                    command.Parameters.AddWithValue("@distance", RX.distance);
                    command.Parameters.AddWithValue("@azimuth", RX.azimuth);
                    command.Parameters.AddWithValue("@rx_azimuth", RX.rx_azimuth);
                    command.Parameters.AddWithValue("@frequency", RX.frequency);
                    command.Parameters.AddWithValue("@power", RX.power);
                    command.Parameters.AddWithValue("@snr", RX.snr);
                    command.Parameters.AddWithValue("@drift", RX.drift);
                    command.Parameters.AddWithValue("@version", RX.version);
                    command.Parameters.AddWithValue("@code", RX.code);
                    command.ExecuteNonQuery();

                    connection.Close();

                }
                catch
                {         //if row already exists then try updating it in database
                    connection.Close();
                }
            }

        }



        private void filterbutton_Click(object sender, EventArgs e)
        {
            //MessageForm nForm = new MessageForm();
            Msg.TMessageBox("Please wait ....", "", 30000);
            if (filterbutton.Text == "Apply")
            {
                filter_results(false, "");
                //filterbutton.Text = "Clear";
            }
            else
            {
                show_results();
                // filterbutton.Text = "Apply";
            }
            //nForm.Dispose();
        }

        private void Clearbutton_Click(object sender, EventArgs e)
        {
            //MessageForm nForm = new MessageForm();
            Msg.TMessageBox("Please wait ....", "", 30000);

            show_results();

            //nForm.Dispose();
        }



        private void DFromtextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Allow only letters, digits, and basic punctuation
                if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !".,-_ ".Contains(e.KeyChar))
                {
                    e.Handled = true; // Block the character
                    return;
                }
                int maxDistance = 20000; //35km short of max
                string maxstr = "20000";
                if (!kmcheckBox.Checked)
                {
                    maxstr = convert_to_miles(maxDistance);
                }
                int maxD = 2000;
                Int32.TryParse(maxstr, out maxD);

                int t;
                if (e.KeyChar == 45) //no minus allowed
                {
                    e.Handled = true;
                    return;
                }
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) //will allow offset from -10 to +210 for adjustment
                {
                    e.Handled = true;
                }

                // Allow only letters, digits, and basic punctuation
                if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !".,-_ ".Contains(e.KeyChar))
                {
                    e.Handled = true; // Block the character
                }
                else
                {
                    int.TryParse(DFromtextBox.Text + e.KeyChar, out t);
                    if (t < 0 || t > maxD)
                    {
                        MessageBox.Show("Error: range 0-" + maxD, "");
                        e.Handled = true;
                    }
                }
            }
            catch
            {
            }
        }

        private void kmcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!kmcheckBox.Checked)
            {
                Ulabel.Text = "mls";
                Dlabel.Text = "0 - 12,453";
            }
            else
            {
                Ulabel.Text = "km";
                Dlabel.Text = "0 - 20,035";
            }
        }

        private void DTotextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Allow only letters, digits, and basic punctuation
                if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !".,-_ ".Contains(e.KeyChar))
                {
                    e.Handled = true; // Block the character
                    return;
                }
                int maxDistance = 20035; //half circumference of earth
                string maxstr = "20035";
                if (!kmcheckBox.Checked)
                {
                    maxstr = convert_to_miles(maxDistance);
                }
                int maxD = 2000;
                Int32.TryParse(maxstr, out maxD);
                int t;
                if (e.KeyChar == 45) //no minus allowed
                {
                    e.Handled = true;
                    return;
                }
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) //will also accept - offset up to -10 and +ve up to 210
                {
                    e.Handled = true;
                }
                else
                {
                    int.TryParse(DTotextBox.Text + e.KeyChar, out t);
                    if (t < 0 || t > maxD)
                    {
                        MessageBox.Show("Error: range 10-" + maxD, "");
                        e.Handled = true;
                    }
                }
            }
            catch
            {

            }
        }

        private void DFromtextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void callFiltertextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only letters, digits, and basic punctuation
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !".,-_*".Contains(e.KeyChar))
            {
                e.Handled = true; // Block the character
            }
        }

        private async void Nowbutton_Click(object sender, EventArgs e)
        {
            int min = 30;
            int index = PlistBox.TopIndex;
            string text = PlistBox.Items[index].ToString();
           
                min = findPeriod();            
            updateNow(min, true);
        }
        private async Task updateNow(int min, bool wait)
        {
            if (updatecheckBox.Checked)
            {
                Msg.TMessageBox("Updates disabled", "", 2000);
                return;
            }
            try
            {

                string url = "http://db1.wspr.live";
                if (stopUrl)
                {
                    Msg.TMessageBox("Internet is disabled", "", 2000);
                    return;
                }

                if (!await checkSQL())
                {
                    Msg.TMessageBox("Cannot connect to wspr.live ...", "Error connecting", 3500);
                    return;
                }
                string freq = "";
                if (!timer1.Enabled)
                {

                    await get_results(Callsign, freq, db_server, db_user, db_pass, min, owncall);


                    PlistBox.SelectedIndex = 0;
                }
                else
                {

                    return;
                }
                if (wait && !timer1.Enabled)
                {
                    timer1.Interval = 120000;
                    timer1.Enabled = true;
                    timer1.Start(); //prevent multiple presses within 2 minutes
                    Nowbutton.Text = "Wait ...";
                }
            }
            catch
            {

            }
        }

        private int findPeriod() //find period in minutes
        {
            try
            {              
                int index = PlistBox.TopIndex;
                string s = PlistBox.Items[index].ToString();
                if (s != "" && s != null)
                {                 

                    int i = 10;
                    switch (index)
                    {
                        case 0:
                            i = 10;
                            break;
                        case 1:
                            i = 20;
                            break;
                        case 2:
                            i = 30;
                            break;
                        case 3:
                            i = 60;
                            break;
                        case 4:
                            i = 180;
                            break;
                        case 5:
                            i = 360;
                            break;
                        case 6:
                            i = 720;
                            break;
                        case 7:
                            i = 1440;
                            break;
                        default:
                            i = 10;
                            break;
                    }
                    return i;
                }
                else
                {
                    return 10;
                }
            }
            catch
            {
                return 10;
            }

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            Nowbutton.Text = "Update now";
            timer1.Stop();
            timer1.Enabled = false;

        }



        private void PlistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PlistBox.SelectedIndex > -1)
            {
                Plabel.Text = PlistBox.SelectedItem.ToString();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                updatePassandCall();
            }
            catch
            {

            }
        }
        private async void updatePassandCall()
        {
            try
            {
                string freq = "";
                startCount++;
                startCountMax = 4;
                int min = 20;
                int index = PlistBox.TopIndex;
                string text = PlistBox.Items[index].ToString();
              
                    min = findPeriod();
                
                if (startCount > startCountMax)  //X minutes
                {
                    startCount = 0;
                    get_results(Callsign, freq, db_server, db_user, db_pass, min, owncall);

                }

                await getUserandPassword();
            }
            catch { }

        }

        private void testDBbutton_Click(object sender, EventArgs e)
        {
            testDB();
        }

        private void calltextBox_TextChanged(object sender, EventArgs e)
        {
            if (calltextBox.Text == "")
            {
                Callsign = originalcall;
            }
            else
            {
                Callsign = calltextBox.Text.Trim().ToUpper();
            }
            this.Text = headerline.Replace(originalcall, Callsign);
        }

        private void othercheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (othercheckBox.Checked)
            {
                calltextBox.Enabled = true;

                owncall = false;
                filterbutton.Visible = false;
                Clearbutton.Visible = false;
                if (calltextBox.Text.Trim() != "")
                {
                    Callsign = calltextBox.Text.ToUpper();
                    this.Text = headerline.Replace(originalcall, Callsign);
                }
            }
            else
            {

                calltextBox.Enabled = false;
                owncall = true;
                Callsign = originalcall;
                filterbutton.Visible = true;
                Clearbutton.Visible = true;
                this.Text = headerline.Replace(originalcall, Callsign);
                Nowbutton.Text = "Update now";
                timer1.Stop();
                timer1.Enabled = false;
            }
        }

        private void delbutton_Click(object sender, EventArgs e)
        {
            if (calltextBox.Text.Trim() == "")
            {
                return;
            }
            var res = Msg.ynMessageBox("Remove all records for " + calltextBox.Text + " from database (Y/N)?", "Confirm removal");
            if (res == DialogResult.Yes)
            {
                //delete_received(db_server, db_user, db_pass);

            }
        }

        private void updatecheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (updatecheckBox.Checked)
            {
                disabledlabel.Visible = true;
            }
            else
            {
                disabledlabel.Visible = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                if (e.ColumnIndex >= 0)
                {
                    int colIndex = e.ColumnIndex;
                    string text = dataGridView1.Rows[rowIndex].Cells[colIndex].Value?.ToString();
                    if (colIndex == 12) //version column
                    {
                        var res = Msg.ynMessageBox("Search by version (Y/N)?", "Version");
                        if (res == DialogResult.Yes)
                        {
                            Msg.TMessageBox("Please wait ....", "", 30000);
                            filter_results(true, text.Trim());

                        }
                    }
                }
            }

        }

    }
}
