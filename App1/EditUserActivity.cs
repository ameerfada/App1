using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using App1;


namespace Project
{
    [Activity(Label = "EditUserActivity")]
    public class EditUserActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.edit_user);
            // Create your application here
            EditText name = FindViewById<EditText>(Resource.Id.name);
            EditText age = FindViewById<EditText>(Resource.Id.age);
            EditText city = FindViewById<EditText>(Resource.Id.city);
            EditText profession = FindViewById<EditText>(Resource.Id.profession);
            EditText currentSubjects = FindViewById<EditText>(Resource.Id.currentSubjects);
            CheckBox isMarried = FindViewById<CheckBox>(Resource.Id.isMarried);
            CheckBox hasDiploma = FindViewById<CheckBox>(Resource.Id.hasDiploma);

            var FullFilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "users_data.txt");
            using (System.IO.StreamReader sr = new StreamReader(FullFilePath, Encoding.GetEncoding("iso-8859-1")))
            {
                String line;
                int lineNumber = 1;
                int userId = Int32.Parse(Intent.GetStringExtra("userId"));
                while ((line = sr.ReadLine()) != null)
                {
                    if (lineNumber == userId)
                    {
                        string[] user = line.Split(new char[] { ';' }, StringSplitOptions.None);
                        name.Text = user[0];
                        age.Text = user[1];
                        city.Text = user[2];
                        profession.Text = user[3];
                        isMarried.Checked = user[4] == "true" ? true : false;
                        hasDiploma.Checked = user[5] == "true" ? true : false;
                        currentSubjects.Text = user[6];
                        break;
                    }
                    lineNumber++;
                }
            }

            Button btn_save = FindViewById<Button>(Resource.Id.btn_save);
            btn_save.Click += Btn_save_Click;
        }
        private void Btn_save_Click(object sender, System.EventArgs e)
        {
            Boolean dataIsValid = true;

            EditText name = FindViewById<EditText>(Resource.Id.name);
            TextView name_error = FindViewById<TextView>(Resource.Id.name_error);
            if (name.Text != "")
            {
                String[] words = name.Text.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                Boolean nameIsValid = true;
                foreach (var word in words)
                {
                    if (!char.IsUpper(word[0]) || !word.Substring(1).All(char.IsLower))
                    {
                        nameIsValid = false;
                    }
                }
                if (!nameIsValid)
                {
                    dataIsValid = false;
                    ViewCompat.SetBackgroundTintList(name, Android.Content.Res.ColorStateList.ValueOf(Color.Red));
                    name_error.Text = "Name property must always start with a capital letter, also must have capital letters after each white-space! Any other letter must not be capital";
                }
                else
                {
                    ViewCompat.SetBackgroundTintList(name, Android.Content.Res.ColorStateList.ValueOf(Color.Green));
                    name_error.Text = "";
                }

            }
            else
            {
                dataIsValid = false;
                ViewCompat.SetBackgroundTintList(name, Android.Content.Res.ColorStateList.ValueOf(Color.Red));
                name_error.Text = "Name  cannot be empty";
            }

            EditText age = FindViewById<EditText>(Resource.Id.age);
            TextView age_error = FindViewById<TextView>(Resource.Id.age_error);
            if (age.Text != "")
            {
                if (!(Enumerable.Range(0, 100).Contains(Int32.Parse(age.Text))))
                {
                    dataIsValid = false;
                    ViewCompat.SetBackgroundTintList(age, Android.Content.Res.ColorStateList.ValueOf(Color.Red));
                    age_error.Text = "Age must be between 0-99";
                }
                else
                {
                    ViewCompat.SetBackgroundTintList(age, Android.Content.Res.ColorStateList.ValueOf(Color.Green));
                    age_error.Text = "";
                }
            }
            else
            {
                dataIsValid = false;
                ViewCompat.SetBackgroundTintList(age, Android.Content.Res.ColorStateList.ValueOf(Color.Red));
                age_error.Text = "Age cannot be empty";
            }

            EditText city = FindViewById<EditText>(Resource.Id.city);
            TextView city_error = FindViewById<TextView>(Resource.Id.city_error);
            if (city.Text != "")
            {
                if (!char.IsUpper(city.Text[0]))
                {
                    dataIsValid = false;
                    ViewCompat.SetBackgroundTintList(city, Android.Content.Res.ColorStateList.ValueOf(Color.Red));
                    city_error.Text = "City must start with Capital letter";
                }
                else
                {
                    ViewCompat.SetBackgroundTintList(city, Android.Content.Res.ColorStateList.ValueOf(Color.Green));
                    city_error.Text = "";
                }
            }
            else
            {
                dataIsValid = false;
                ViewCompat.SetBackgroundTintList(city, Android.Content.Res.ColorStateList.ValueOf(Color.Red));
                city_error.Text = "City cannot be empty";
            }

            EditText profession = FindViewById<EditText>(Resource.Id.profession);
            TextView profession_error = FindViewById<TextView>(Resource.Id.profession_error);
            if (profession.Text == "")
            {
                dataIsValid = false;
                ViewCompat.SetBackgroundTintList(profession, Android.Content.Res.ColorStateList.ValueOf(Color.Red));
                profession_error.Text = "Profession cannot be empty";
            }
            else
            {
                ViewCompat.SetBackgroundTintList(profession, Android.Content.Res.ColorStateList.ValueOf(Color.Green));
                profession_error.Text = "";
            }

            EditText currentSubjects = FindViewById<EditText>(Resource.Id.currentSubjects);
            if (currentSubjects.Text == "")
            {
                dataIsValid = false;
                ViewCompat.SetBackgroundTintList(currentSubjects, Android.Content.Res.ColorStateList.ValueOf(Color.Red));
                TextView currentSubjects_error = FindViewById<TextView>(Resource.Id.currentSubjects_error);
                currentSubjects_error.Text = "Current Subjects cannot be empty. It must be a list separated with ','";
            }
            else
            {
                ViewCompat.SetBackgroundTintList(currentSubjects, Android.Content.Res.ColorStateList.ValueOf(Color.Green));
                TextView currentSubjects_error = FindViewById<TextView>(Resource.Id.currentSubjects_error);
                currentSubjects_error.Text = "";
            }

            if (dataIsValid)
            {
                var FullFilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "users_data.txt");
                int userId = Int32.Parse(Intent.GetStringExtra("userId"));
                string[] lines = File.ReadAllLines(FullFilePath);
                CheckBox isMarried = FindViewById<CheckBox>(Resource.Id.isMarried);
                CheckBox hasDiploma = FindViewById<CheckBox>(Resource.Id.hasDiploma);
                string user_data = name.Text + ";" + age.Text + ";" + city.Text + ";" + profession.Text + ";" + isMarried.Checked.ToString().ToLower() + ";" + hasDiploma.Checked.ToString().ToLower() + ";" + currentSubjects.Text;
                lines[userId - 1] = user_data;
                File.WriteAllLines(FullFilePath, lines);
                Intent intent = new Intent(this, typeof(ListUsersActivity));
                StartActivity(intent);
            }
        }
    }
}