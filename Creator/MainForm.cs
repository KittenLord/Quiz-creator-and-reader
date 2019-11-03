using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encrypter;

namespace QuizCreator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            quizList.Click += (s, e) =>
            {
                quizList.Items.Clear();
                quizList.Items.AddRange(QuizQuestion.Questions.ToArray());
            };
        }

        public static List<QuizQuestion> Quizzes = new List<QuizQuestion>();


        private void newBtn_Click(object sender, EventArgs e)
        {
            new AddOrModifyForm().Show();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (Quizzes.Count <= 0) return;

            if (quizList.SelectedItem == null) return;

            string select = quizList.SelectedItem as string;

            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete this question?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                Quizzes.Remove(Quizzes.Find(p => p.Question == select));
                QuizQuestion.Questions.Remove(select);

                quizList.Items.Clear();
                quizList.Items.AddRange(QuizQuestion.Questions.ToArray());
            }

        }

        private void infoBtn_Click(object sender, EventArgs e)
        {
            if (Quizzes.Count <= 0) return;

            if (quizList.SelectedItem == null) return;

            string select = quizList.SelectedItem as string;

            var q = Quizzes.Find(p => p.Question == select);

            MessageBox.Show($"Question: \"{q.Question}\"\tAnswer: \"{q.CorrectAnswer}\"\nQueue: '{q.QueuePlace}'\tHas pre-answers: {q.HasPreAnswers.ToString()}\n\nAnswer 1: {q.PreAnswers[0]}\tAnswer 2: {q.PreAnswers[1]}\tAnswer 3: {q.PreAnswers[2]}\nAnswer 4: {q.PreAnswers[3]}\tAnswer 5: {q.PreAnswers[4]}\tAnswer 6: {q.PreAnswers[5]}");
        }

        private void modifyBtn_Click(object sender, EventArgs e)
        {
            if (Quizzes.Count <= 0) return;

            if (quizList.SelectedItem == null) return;

            string select = quizList.SelectedItem as string;

            new AddOrModifyForm(Quizzes.Find(p => p.Question == quizList.SelectedItem as string)).Show();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if(fileNameBox.Text == "")
            {
                MessageBox.Show("Insert file name");
                return;
            }

            string quiz = "";

            foreach (var q in Quizzes)
            {
                quiz += $"<quiz>\n\t<place>{q.QueuePlace}</place>\n\t<question>{q.Question}</question>\n\t<corranswer>{q.CorrectAnswer}</corranswer>\n\t<haspreanswer>{q.HasPreAnswers}</haspreanswer>\n\t<preanswers>{q.PreAnswers[0]};{q.PreAnswers[1]};{q.PreAnswers[2]};{q.PreAnswers[3]};{q.PreAnswers[4]};{q.PreAnswers[5]}</preanswers>\n</quiz>\n<end>\n";
            }

            string name = $"{fileNameBox.Text}.txt";

            if (encryptBox.Checked)
            {
                quiz = quiz.Encrypt();
                name = $"{fileNameBox.Text}_ENC.txt";
            }

            FolderBrowserDialog f = new FolderBrowserDialog();

            string path = "";

            if(f.ShowDialog() == DialogResult.OK)
            {
                path = f.SelectedPath;
            }

            File.WriteAllText($@"{path}\{name}", quiz);
        }
    }

    public class QuizQuestion
    {
        public static List<string> Questions = new List<string>();

        public static int LastQueue = 0;

        public readonly int QueuePlace;
        public readonly string Question;
        public readonly string CorrectAnswer;

        public readonly bool HasPreAnswers;

        public readonly string[] PreAnswers;

        public QuizQuestion(string question, string correctAnswer)
        {
            Question = question;
            CorrectAnswer = correctAnswer;

            QueuePlace = LastQueue;
            LastQueue++;

            HasPreAnswers = false;

            Questions.Add(question);

            PreAnswers = new string[] { "--", "--", "--", "--", "--", "--" };
        }

        public QuizQuestion(string question, string correctAnswer, string[] preAnswers)
        {
            Question = question;
            CorrectAnswer = correctAnswer;
            PreAnswers = preAnswers;

            QueuePlace = LastQueue;
            LastQueue++;

            HasPreAnswers = true;

            Questions.Add(question);
        }

        public QuizQuestion(string question, string correctAnswer, int place)
        {
            Question = question;
            CorrectAnswer = correctAnswer;

            QueuePlace = place;

            HasPreAnswers = false;

            PreAnswers = new string[] { "--", "--", "--", "--", "--", "--" };
        }

        public QuizQuestion(string question, string correctAnswer, string[] preAnswers, int place)
        {
            Question = question;
            CorrectAnswer = correctAnswer;
            PreAnswers = preAnswers;

            QueuePlace = place;

            HasPreAnswers = true;
        }
    }
}
