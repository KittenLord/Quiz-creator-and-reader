using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizCreator
{
    public partial class AddOrModifyForm : Form
    {
        public AddOrModifyForm(QuizQuestion quiz)
        {
            InitializeComponent();

            isModify = true;

            this.quiz = quiz;

            questionBox.Enabled = false;


            questionBox.Text = quiz.Question;
            correctAnswerBox.Text = quiz.CorrectAnswer;
            PreAnswrsBox.Checked = quiz.HasPreAnswers;

            preAnswer1.Text = quiz.PreAnswers[0];
            preAnswer2.Text = quiz.PreAnswers[1];
            preAnswer3.Text = quiz.PreAnswers[2];
            preAnswer4.Text = quiz.PreAnswers[3];
            preAnswer5.Text = quiz.PreAnswers[4];
            preAnswer6.Text = quiz.PreAnswers[5];
        }

        bool isModify = false;
        QuizQuestion quiz;

        public AddOrModifyForm()
        {
            InitializeComponent();
        }

        private void PreAnswrsBox_CheckedChanged(object sender, EventArgs e)
        {
            label3.Enabled = PreAnswrsBox.Checked;
            label4.Enabled = PreAnswrsBox.Checked;
            label5.Enabled = PreAnswrsBox.Checked;

            label6.Enabled = PreAnswrsBox.Checked;
            label7.Enabled = PreAnswrsBox.Checked;
            label8.Enabled = PreAnswrsBox.Checked;


            preAnswer1.Enabled = PreAnswrsBox.Checked;
            preAnswer2.Enabled = PreAnswrsBox.Checked;
            preAnswer3.Enabled = PreAnswrsBox.Checked;

            preAnswer4.Enabled = PreAnswrsBox.Checked;
            preAnswer5.Enabled = PreAnswrsBox.Checked;
            preAnswer6.Enabled = PreAnswrsBox.Checked;
        }

        private void saveQuestionBtn_Click(object sender, EventArgs e)
        {
            if(questionBox.Text == "" || correctAnswerBox.Text == "")
            {
                MessageBox.Show("Not all fields are filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(PreAnswrsBox.Checked && (preAnswer1.Text == "" || preAnswer2.Text == "" || preAnswer3.Text == "" || preAnswer4.Text == "" || preAnswer5.Text == "" || preAnswer6.Text == ""))
            {
                MessageBox.Show("Not all fields are filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!isModify)
            {
                if (!PreAnswrsBox.Checked)
                {
                    MainForm.Quizzes.Add(new QuizQuestion(questionBox.Text, correctAnswerBox.Text));
                }
                else
                {
                    MainForm.Quizzes.Add(new QuizQuestion(questionBox.Text, correctAnswerBox.Text, new string[] { preAnswer1.Text, preAnswer2.Text, preAnswer3.Text, preAnswer4.Text, preAnswer5.Text, preAnswer6.Text }));
                }

                MessageBox.Show("You added new question to your quiz!");
            }
            else
            {

                if (questionBox.Text == "" || correctAnswerBox.Text == "")
                {
                    MessageBox.Show("Not all fields are filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (PreAnswrsBox.Checked && (preAnswer1.Text == "" || preAnswer2.Text == "" || preAnswer3.Text == "" || preAnswer4.Text == "" || preAnswer5.Text == "" || preAnswer6.Text == ""))
                {
                    MessageBox.Show("Not all fields are filled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int index = MainForm.Quizzes.IndexOf(quiz);

                if (!PreAnswrsBox.Checked)
                {
                    MainForm.Quizzes[index] = new QuizQuestion(questionBox.Text, correctAnswerBox.Text, quiz.QueuePlace);
                }
                else
                {
                    MainForm.Quizzes[index] = new QuizQuestion(questionBox.Text, correctAnswerBox.Text, new string[] { preAnswer1.Text, preAnswer2.Text, preAnswer3.Text, preAnswer4.Text, preAnswer5.Text, preAnswer6.Text }, quiz.QueuePlace);
                }

                MessageBox.Show("You modified question");
            }

            Close();
        }
    }
}
