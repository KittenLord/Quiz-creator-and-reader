using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizReader
{
    public partial class QuestionForm : Form
    {
        public QuestionForm()
        {
            InitializeComponent();

            Run();
        }

        public async void Run()
        {
            for (int i = 0; i < Handler.Quizzes.Count; i++)
            {
                SetQuiz(Handler.Quizzes[i]);

                while (!Answered) { await Task.Delay(100); }

                if (Handler.Quizzes[i].CorrectAnswer == answer)
                {
                    MessageBox.Show("Correct");
                    score += 10;
                }
                else
                    MessageBox.Show("Wrong");

                Answered = false;
                answer = "";
            }

            Close();

            MessageBox.Show($"You have got {score} points out of {Handler.Quizzes.Count * 10} possible!");
        }

        bool Answered = false;

        string answer;

        int score = 0;

        public void SetQuiz(QuizQuestion quiz)
        {
            questionLabel.Text = quiz.Question;

            preAnswer1.Text = quiz.PreAnswers[0];
            preAnswer2.Text = quiz.PreAnswers[1];
            preAnswer3.Text = quiz.PreAnswers[2];
            preAnswer4.Text = quiz.PreAnswers[3];
            preAnswer5.Text = quiz.PreAnswers[4];
            preAnswer6.Text = quiz.PreAnswers[5];

            answerBox.Text = "";

            if (!quiz.HasPreAnswers)
            {
                preAnswer1.Enabled = false;
                preAnswer2.Enabled = false;
                preAnswer3.Enabled = false;
                preAnswer4.Enabled = false;
                preAnswer5.Enabled = false;
                preAnswer6.Enabled = false;

                submitBtn.Enabled = true;
                answerBox.Enabled = true;
            }
            else
            {
                preAnswer1.Enabled = true;
                preAnswer2.Enabled = true;
                preAnswer3.Enabled = true;
                preAnswer4.Enabled = true;
                preAnswer5.Enabled = true;
                preAnswer6.Enabled = true;

                submitBtn.Enabled = false;
                answerBox.Enabled = false;
            }
        }
        private void submitBtn_Click(object sender, EventArgs e)
        {
            Answered = true;
            answer = answerBox.Text;
        }
        private void preAnswer1_Click(object sender, EventArgs e)
        {
            Answered = true;
            answer = preAnswer1.Text;
        }
        private void preAnswer2_Click(object sender, EventArgs e)
        {
            Answered = true;
            answer = preAnswer2.Text;
        }
        private void preAnswer3_Click(object sender, EventArgs e)
        {
            Answered = true;
            answer = preAnswer3.Text;
        }
        private void preAnswer4_Click(object sender, EventArgs e)
        {
            Answered = true;
            answer = preAnswer4.Text;
        }
        private void preAnswer5_Click(object sender, EventArgs e)
        {
            Answered = true;
            answer = preAnswer5.Text;
        }
        private void preAnswer6_Click(object sender, EventArgs e)
        {
            Answered = true;
            answer = preAnswer6.Text;
        }
    }
}
