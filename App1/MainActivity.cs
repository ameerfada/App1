using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.IO;
using System.Text;
using App1;


namespace Project
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Button btn_list = FindViewById<Button>(Resource.Id.btn_list);
            Button btn_add = FindViewById<Button>(Resource.Id.btn_add);
            btn_list.Click += Btn_list_Click;
            btn_add.Click += Btn_add_Click;
            var FullFilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "users_data.txt");
            System.IO.StreamReader sr;
            if (File.Exists(FullFilePath))
                sr = new StreamReader(FullFilePath, Encoding.GetEncoding("iso-8859-1"));
            else
                sr = new StreamReader(File.Create(FullFilePath), Encoding.GetEncoding("iso-8859-1"));
            using (sr)
            {
                string line = "";
                string userMostSubjects = "";
                string usersOver30 = "";
                int maxSubjects = 0;
                int usersFromBudapest = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] user = line.Split(new char[] { ';' }, StringSplitOptions.None);
                    string[] subjects = user[6].Split(new char[] { ',' }, StringSplitOptions.None);
                    if(subjects.Length > maxSubjects)
                    {
                        maxSubjects = subjects.Length;
                        userMostSubjects = user[0];
                    }
                    if(Int32.Parse(user[1]) > 30)
                    {
                        usersOver30 += user[0] + ",";
                    }
                    if(user[2] == "Budapest")
                    {
                        usersFromBudapest++;
                    }
                }

                TextView userMostSubjectsET = FindViewById<TextView>(Resource.Id.user_most_subjects);
                TextView usersOver30ET = FindViewById<TextView>(Resource.Id.users_over_30);
                TextView usersFromBudapestET = FindViewById<TextView>(Resource.Id.users_from_budapest);

                userMostSubjectsET.Text = userMostSubjects;
                usersOver30ET.Text = usersOver30.TrimEnd(',');
                usersFromBudapestET.Text = usersFromBudapest.ToString();

            }
        }

        private void Btn_list_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ListUsersActivity));
            StartActivity(intent);
        }

        private void Btn_add_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AddUserActivity));
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}