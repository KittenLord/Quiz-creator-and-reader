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

namespace QuizReader
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();


        }

        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();

            string path = "";

            if (file.ShowDialog() == DialogResult.OK)
            {
                path = file.FileName;
            }
            else
                return;

            string content = File.ReadAllText(path);

            if (!path.EndsWith(".txt"))
            {
                MessageBox.Show("Wrong file selected");
                return;
            }

            if (path.EndsWith("_ENC.txt"))
            {
                content = content.Decrypt();
            }

            List<string> encryptedQuestions = content.Split(new string[] { "<end>" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            //encryptedQuestions.Remove(encryptedQuestions.ElementAt(encryptedQuestions.Count - 1));

            List<QuizQuestion> quizzes = new List<QuizQuestion>();

            for(int i = 0; i < encryptedQuestions.Count; i++)
            {
                string line = encryptedQuestions[i].FindSurroundedBy("<place>", "</place>");
                int num;

                if (line == "0") 
                    num = 0;
                if (line == "")
                    continue;
                else
                    num = Convert.ToInt32(line);

                quizzes.Add(new QuizQuestion(encryptedQuestions[i].FindSurroundedBy("<question>", "</question>"), encryptedQuestions[i].FindSurroundedBy("<corranswer>", "</corranswer>"), encryptedQuestions[i].FindSurroundedBy("<haspreanswer>", "</haspreanswer>") == "True" ? true : false, encryptedQuestions[i].FindSurroundedBy("<preanswers>", "</preanswers>").Split(';'), num));
            }

            Handler.Quizzes = quizzes;

            new QuestionForm().Show();
        }
    }

    public class QuizQuestion
    {
        public readonly int QueuePlace;
        public readonly string Question;
        public readonly string CorrectAnswer;

        public readonly bool HasPreAnswers;

        public readonly string[] PreAnswers;
        public QuizQuestion(string question, string correctAnswer, bool haspreanswers, string[] preAnswers, int place)
        {
            Question = question;
            CorrectAnswer = correctAnswer;
            PreAnswers = preAnswers;

            QueuePlace = place;

            HasPreAnswers = haspreanswers;
        }
    }

    public static class Handler
    {
        public static List<QuizQuestion> Quizzes;
    }

    public static class Ext
    {
        public static string FindSurroundedBy(this string finder, string startBorder, string endBorder)
        {
            if (!finder.Contains(startBorder) || !finder.Contains(endBorder)) return "";

            bool found = false;
            bool ended = false;

            string content = "";

            for (int i = 0; i < finder.Length; i++)
            {
                if (finder[i] == endBorder[0] && found)
                {
                    string end = "";
                    int j;
                    for (j = i; j < i + endBorder.Length; j++)
                    {
                        end += finder[j];
                    }
                    if (end == endBorder)
                    {
                        ended = true;
                        i = j;
                        return content;
                    }
                }

                if (finder[i] == startBorder[0] && !found)
                {
                    string start = "";
                    int j;
                    for (j = i; j < i + startBorder.Length; j++)
                    {
                        start += finder[j];
                    }
                    if (start == startBorder)
                    {
                        found = true;
                        i = j;
                    }
                }

                if (found && !ended)
                {
                    content += finder[i];
                }
            }

            return content;
        }
    }
}
