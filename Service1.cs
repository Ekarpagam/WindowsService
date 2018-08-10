using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Data.SqlClient;


namespace DemoWindoserviceBlog
{
    public partial class Service1 : ServiceBase
    {
        // public string strcon = @"Data Source=SPRITLE\SQLEXPRESS;Initial Catalog=LIDS;Integrated Security=True";
        public string strcon = @"Data Source = SPRITLE\SQLEXPRESS;Initial Catalog = LIDS; Integrated Security=SSPI";
        System.Timers.Timer timer1;
        int count;
        public Service1()
        {
            InitializeComponent();
            timer1 = new System.Timers.Timer();
        }
        protected override void OnStart(string[] args)
        {
            //System.Diagnostics.Debugger.Launch(); 
            timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
            timer1.Interval = 10000;
            timer1.Enabled = true;
            timer1.Start();
        }
        protected override void OnStop()
        {
            timer1.Enabled = false;
        }
        public void nxttable()
        {
            tblenmae = action = "";
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            //  SqlCommand cmd11 = new SqlCommand("select * FROM inmatetopupTrigger where ([Servstatus] !='P' or [Servstatus] is null or Servstatus='') order by Audit_Timestamp", con);

            SqlCommand cmd11 = new SqlCommand("select (SELECT  (select * FROM inmatetopupTrigger where ([Servstatus] !='P' or [Servstatus] is null or Servstatus='') FOR XML auto,root ('Inmate_Credits'),elements,type) for xml path('credits')) as Name", con);
            SqlDataReader dr1 = cmd11.ExecuteReader();
            if (dr1.Read())
            {
                //action = dr1["Audit_Action"].ToString();
                //time1 = dr1["Audit_Timestamp"].ToString();
                //id = Convert.ToInt32(dr1["id"].ToString());
                //tblenmae = dr1["TableName"].ToString();

                //created_at1 = dr1["created_at"].ToString();
                //updated_at1 = dr1["updated_at"].ToString();
                //vending_machine_id1 = dr1["vending_machine_id"].ToString();
                //credit_period_id1 = dr1["credit_period_id"].ToString();
                //topup_credit1 = dr1["topup_credit"].ToString();
                //user_id1 = dr1["user_id"].ToString();
                //date_time1 = dr1["date_time"].ToString();
                //source1 = dr1["source"].ToString();
                //uid1 = dr1["uid"].ToString();
                //inmate_id1 = dr1["inmate_id"].ToString();
                //transaction_id1 = dr1["transaction_id"].ToString();
                //old_credit1 = dr1["old_credit"].ToString();

                //alltble = " inmate_id-" + inmate_id1 + " transaction_id-" + transaction_id1 + " vending_machine_id-" + vending_machine_id1 + " credit_period_id-" + credit_period_id1 + " topup_credit-" + topup_credit1 + " user_id" + user_id1 + " date_time-" + date_time1 + " source-" + source1 + " uid-" + uid1 + " old_credit-" + old_credit1 + " created_at-" + created_at1 + " updated_at-" + updated_at1;
                //process = count + " " + " Time - " + time1 + " " + id + "  Action- " + action +  alltble;

                action = dr1["Name"].ToString();
                process = action;

                if (action != "<credits/>")
                {

                    FileStream fs = new FileStream(@"K:\TestServiceLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(process);
                    sw.Flush();
                    sw.Close();
                }
            }
            dr1.Close();

            SqlCommand cmd1 = new SqlCommand("update inmatetopupTrigger set [Servstatus]='P' where [Servstatus] !='" + "P" + "'or [Servstatus] is null or Servstatus='' ", con);
            cmd1.ExecuteNonQuery();

            con.Close();
        }
        public string created_at1, updated_at1, vending_machine_id1, credit_period_id1, topup_credit1, user_id1, date_time1, source1, uid1, inmate_id1, transaction_id1, old_credit1;
        public string action, time, process, tblenmae, alltble, action1, time1, tblenmae1;
        public int id, id1;
        public string credits, inmate_id, credit_period_id, inmate_credit_upload_log_id, week1_total_hrs, hourly_job_rate, fort_nightly_allowance, week1_allowance;
        public string eligible_canteen, created_at, updated_at, week2_total_hrs, week2_allowance, working_shift, two_weekly_allowance, work_shop, location;
        public string today;
        private void timer1_Elapsed(object sender, EventArgs e)
        {
            try
            {
                today = DateTime.Now.ToString("dd-MM-yyyy");
               /// DateTime.Now.ToString("yyyy-MM-dd");
                created_at1 = updated_at1 = vending_machine_id1 = credit_period_id1 = topup_credit1 = user_id1 = date_time1 = source1 = uid1 = inmate_id1 = transaction_id1 = old_credit1 = "";
                action = ""; time = ""; id = 0;
                tblenmae = credits = inmate_id = credit_period_id = inmate_credit_upload_log_id = week1_total_hrs = hourly_job_rate = fort_nightly_allowance = "";
                week1_allowance = eligible_canteen = created_at = updated_at = week2_total_hrs = week2_allowance = working_shift = two_weekly_allowance = work_shop = location = "";
                //StringBuilder sql = new StringBuilder();
                SqlConnection con = new SqlConnection(strcon);
                con.Open();
                //    SqlCommand cmd = new SqlCommand("select * FROM inmate_credits_TriggerLog where ([Servstatus] !='P' or [Servstatus] is null or Servstatus='') order by Audit_Timestamp", con);
                SqlCommand cmd = new SqlCommand("select (SELECT  (select * FROM inmate_credits_TriggerLog where ([Servstatus] !='P' or [Servstatus] is null or Servstatus='') FOR XML auto,root ('Inmate_Credits'),elements,type) for xml path('credits')) as Detail", con);
                //SqlCommand cmd = new SqlCommand("LogFileTrigger", con);
                //  cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //action = dr["Audit_Action"].ToString();
                    //time = dr["Audit_Timestamp"].ToString();
                    //id = Convert.ToInt32(dr["id"].ToString());
                    //tblenmae = dr["TableName"].ToString();

                    //credits = dr["credits"].ToString();
                    //inmate_id = dr["inmate_id"].ToString();
                    //credit_period_id = dr["credit_period_id"].ToString();
                    //inmate_credit_upload_log_id = dr["inmate_credit_upload_log_id"].ToString();
                    //week1_total_hrs = dr["week1_total_hrs"].ToString();
                    //hourly_job_rate = dr["hourly_job_rate"].ToString();
                    //fort_nightly_allowance = dr["fort_nightly_allowance"].ToString();
                    //week1_allowance = dr["week1_allowance"].ToString();
                    //eligible_canteen = dr["eligible_canteen"].ToString();

                    //created_at = dr["created_at"].ToString();
                    //updated_at = dr["updated_at"].ToString();
                    //week2_total_hrs = dr["week2_total_hrs"].ToString();

                    //week2_allowance = dr["week2_allowance"].ToString();
                    //working_shift = dr["working_shift"].ToString();
                    //two_weekly_allowance = dr["two_weekly_allowance"].ToString();
                    //work_shop = dr["work_shop"].ToString();
                    //location = dr["location"].ToString();

                    //alltble = " credits-" + credits + " inmate_id-" + inmate_id + " credit_period_id-" + credit_period_id + " inmate_credit_upload_log_id-" + inmate_credit_upload_log_id + " week1_total_hrs-" + week1_total_hrs + " hourly_job_rate-" + hourly_job_rate + " fort_nightly_allowance-" + fort_nightly_allowance + " week1_allowance-" + week1_allowance + " eligible_canteen-" + eligible_canteen + " created_at-" + created_at + " updated_at-" + updated_at + " week2_total_hrs-" + week2_total_hrs + " week2_allowance-" + week2_allowance + " working_shift-" + working_shift + " two_weekly_allowance-" + two_weekly_allowance + " work_shop-" + work_shop + " location-" + location;
                    //process = count + " " + " Time - " + time + " " + id + "  Action- " + action +  alltble;

                    action = dr["Detail"].ToString();
                    if (action != "<credits/>")
                    {

                        process = action;

                        FileStream fs = new FileStream(@"K:\TestServiceLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.BaseStream.Seek(0, SeekOrigin.End);
                        sw.WriteLine(process);
                        sw.Flush();
                        sw.Close();
                    }
                    else
                    {

                        dr.Close();
                        nxttable();
                    }
                }
                else
                {
                    dr.Close();
                    nxttable();
                }
                dr.Close();
                //   SqlCommand cmd1 = new SqlCommand("update " + tblenmae + " set [Servstatus]='P' where [id]='" + id + "' and [Audit_Action]='" + action + "'", con);
                SqlCommand cmd1 = new SqlCommand("update inmate_credits_TriggerLog set [Servstatus]='P' where [Servstatus] !='" + "P" + "'or [Servstatus] is null or Servstatus='' ", con);
                cmd1.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
            //    process = count + " No Updation ";
            //    //count++;
            //    FileStream fs = new FileStream(@"K:\TestServiceLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //    StreamWriter sw = new StreamWriter(fs);
            //    sw.BaseStream.Seek(0, SeekOrigin.End);
            //  sw.WriteLine(ex.ToString()); 
            ////   sw.WriteLine(process);
            //    sw.Flush();
            //    sw.Close();
            }

            finally
            {
                timer1.Enabled = true;
                count++;
            }
        }
    }
}


