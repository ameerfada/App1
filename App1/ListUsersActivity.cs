using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using App1;
using System.Linq;
using System.Text;
using static Android.Views.ViewGroup;

namespace Project
{
    [Activity(Label = "list_users")]
    public class ListUsersActivity : Activity
    {
        TableLayout listUsers;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.list_users);
            // Create your application here
            var FullFilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "users_data.txt");
            listUsers = (TableLayout)FindViewById(Resource.Id.users);
            Button home_btn = FindViewById<Button>(Resource.Id.btn_home);
            home_btn.Click += Btn_Back_Home;
            using (System.IO.StreamReader sr = new StreamReader(FullFilePath, Encoding.GetEncoding("iso-8859-1")))
            {
                string line;
                int i = 2;
                int userId = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] user = line.Split(new char[] { ';' }, StringSplitOptions.None);

                    TableRow nameTR = new TableRow(this);
                    TableRow ageTR = new TableRow(this);
                    TableRow cityTR = new TableRow(this);
                    TableRow professionTR = new TableRow(this);
                    TableRow isMarriedTR = new TableRow(this);
                    TableRow hasDiplomaTR = new TableRow(this);
                    TableRow currentSubjectsTR = new TableRow(this);
                    TableRow editButtonTR = new TableRow(this);
                    TableRow deleteButtonTR = new TableRow(this);

                    TextView nameLabel = new TextView(this);
                    TextView ageLabel = new TextView(this);
                    TextView cityLabel = new TextView(this);
                    TextView professionLabel = new TextView(this);
                    TextView isMarriedLabel = new TextView(this);
                    TextView hasDiplomaLabel = new TextView(this);
                    TextView currentSubjectsLabel = new TextView(this);

                    TextView name = new TextView(this);
                    TextView age = new TextView(this);
                    TextView city = new TextView(this);
                    TextView profession = new TextView(this);
                    TextView isMarried = new TextView(this);
                    TextView hasDiploma = new TextView(this);
                    TextView currentSubjects = new TextView(this);

                    

                    Button edit_btn = new Button(this);
                    edit_btn.Text = "Edit";
                    edit_btn.Id = userId;
                    edit_btn.Gravity = GravityFlags.Center;

                    Button delete_btn = new Button(this);
                    delete_btn.Text = "Delete";
                    delete_btn.Id = userId;
                    delete_btn.Gravity = GravityFlags.Center;
                  
                    edit_btn.Click += Btn_Edit_Click;
                    delete_btn.Click += Btn_delete_Click;

                    TextView delimiter = new TextView(this);
                    delimiter.SetHeight(3);
                    delimiter.SetBackgroundColor(Color.Black);

                    nameLabel.Text = "Name:";
                    nameLabel.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);
                    nameLabel.SetTextColor(Color.Brown);
                    nameLabel.SetPadding(4,4,4,4);
                    ageLabel.Text = "Age:";
                    ageLabel.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);
                    ageLabel.SetTextColor(Color.Brown);
                    ageLabel.SetPadding(4, 4, 4, 4);
                    cityLabel.Text = "City:";
                    cityLabel.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);
                    cityLabel.SetTextColor(Color.Brown);
                    cityLabel.SetPadding(4, 4, 4, 4);
                    professionLabel.Text = "Profession:";
                    professionLabel.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);
                    professionLabel.SetTextColor(Color.Brown);
                    professionLabel.SetPadding(4, 4, 4, 4);
                    isMarriedLabel.Text = "IsMarried:";
                    isMarriedLabel.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);
                    isMarriedLabel.SetTextColor(Color.Brown);
                    isMarriedLabel.SetPadding(4, 4, 4, 4);
                    hasDiplomaLabel.Text = "HasDiploma:";
                    hasDiplomaLabel.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);
                    hasDiplomaLabel.SetTextColor(Color.Brown);
                    hasDiplomaLabel.SetPadding(4, 4, 4, 4);
                    currentSubjectsLabel.Text = "currentSubjects:";
                    currentSubjectsLabel.SetTypeface(Typeface.Serif, TypefaceStyle.Bold);
                    currentSubjectsLabel.SetTextColor(Color.Brown);
                    currentSubjectsLabel.SetPadding(4, 4, 4, 4);

                    name.Text = user[0];
                    name.SetPadding(4, 4, 4, 4);
                    age.Text = user[1];
                    age.SetPadding(4, 4, 4, 4);
                    city.Text = user[2];
                    city.SetPadding(4, 4, 4, 4);
                    profession.Text = user[3];
                    profession.SetPadding(4, 4, 4, 4);
                    isMarried.Text = user[4];
                    isMarried.SetPadding(4, 4, 4, 4);
                    hasDiploma.Text = user[5];
                    hasDiploma.SetPadding(4, 4, 4, 4);
                    currentSubjects.Text = user[6];
                    currentSubjects.SetPadding(4, 4, 4, 4);

                    nameTR.AddView(nameLabel);
                    nameTR.AddView(name);
                    ageTR.AddView(ageLabel);
                    ageTR.AddView(age);
                    cityTR.AddView(cityLabel);
                    cityTR.AddView(city);
                    professionTR.AddView(professionLabel);
                    professionTR.AddView(profession);
                    isMarriedTR.AddView(isMarriedLabel);
                    isMarriedTR.AddView(isMarried);
                    hasDiplomaTR.AddView(hasDiplomaLabel);
                    hasDiplomaTR.AddView(hasDiploma);
                    currentSubjectsTR.AddView(currentSubjectsLabel);
                    currentSubjectsTR.AddView(currentSubjects);
                    editButtonTR.AddView(edit_btn);
                    deleteButtonTR.AddView(delete_btn);

                    listUsers.AddView(nameTR, i);
                    listUsers.AddView(ageTR, i + 1);
                    listUsers.AddView(cityTR, i + 2);
                    listUsers.AddView(professionTR, i + 3);
                    listUsers.AddView(isMarriedTR, i + 4);
                    listUsers.AddView(hasDiplomaTR, i + 5);
                    listUsers.AddView(currentSubjectsTR, i + 6);
                    listUsers.AddView(editButtonTR, i + 7);
                    listUsers.AddView(deleteButtonTR, i + 8);
                    listUsers.AddView(delimiter, i + 9);
                    i+=10;
                    userId++;
                }
            }
      
        }

        private void Btn_Back_Home(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
        private void Btn_Edit_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(EditUserActivity));
            Button btn = (Button)sender;
            string id = btn.Id.ToString();
            intent.PutExtra("userId", id);
            StartActivity(intent);
        }

        private void Btn_delete_Click(object sender, System.EventArgs e)
        {
            string tempFile = System.IO.Path.GetTempFileName();
            var FullFilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "users_data.txt");
            using (var sr = new StreamReader(FullFilePath))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;
                int lineNumber = 1;
                Button btn = (Button)sender;
                int userId = btn.Id;
                while ((line = sr.ReadLine()) != null)
                {
                    if (lineNumber != userId)
                        sw.WriteLine(line);
                    lineNumber++;
                }
            }

            File.Delete(FullFilePath);
            File.Move(tempFile, FullFilePath);
            Intent intent = new Intent(this, typeof(ListUsersActivity));
            StartActivity(intent);
            }
    }
}