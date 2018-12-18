using System;
using System.IO;
using System.Windows.Forms;
using dz_8.Data;

namespace dz_8
{
    public partial class MainForm : Form
    {
        private GameInfo _GameInfo = new GameInfo();

        public MainForm()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // создаём диалог для открытия файла
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Сохранение файла";
            dialog.RestoreDirectory = true;
            dialog.Filter = "Файлы xml (*.xml)|*.xml|Все файлы (*.*)|*.*";

            var result = dialog.ShowDialog();
            if (result != DialogResult.OK) return;
            long length = new System.IO.FileInfo(dialog.FileName).Length;
            if (length > 10000)
            {
                MessageBox.Show("Превышен лимит!", "Alert!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            if (!File.Exists(dialog.FileName))
            {
                MessageBox.Show("Такого файла нет!", "Alert!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _GameInfo = GameInfo.Load(dialog.FileName);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // сохранение файла
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Сохранение файла";
            dialog.RestoreDirectory = true;         // Восстанавливать предыдущий путь
            dialog.Filter = "Файлы xml (*.xml)|*.xml|Все файлы (*.*)|*.*";
            dialog.FileName = "Game.xml";

            var result = dialog.ShowDialog();
            if (result != DialogResult.OK) return;

            var file_name = dialog.FileName;
            _GameInfo.Save(file_name);
        }

        private void NumberValueChanged(object sender, EventArgs e)
        {
            //счетчик двигаем
            NumericUpDown numeric_updown = (NumericUpDown)sender;
            var question_number = (int)(numeric_updown.Value - 1);

            if (question_number < 0 || question_number >= _GameInfo.Questions.Count)
                return;

            var question = _GameInfo.Questions[question_number];
            QuestionBox.Text = question.Text;
            TrueBox.Checked = question.IsTrue;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            // добавляем новый вопрос
            Questions new_question = new Questions();
            _GameInfo.Questions.Add(new_question);
            NumberOfQuestion.Value = _GameInfo.Questions.Count;
            QuestionBox.Text = "";
            TrueBox.Checked = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // сохраняем вопрос
            int question_number = (int)(NumberOfQuestion.Value - 1);
            if (question_number < 0 || question_number >= _GameInfo.Questions.Count)
                return;
            Questions question = _GameInfo.Questions[question_number];
            question.Text = QuestionBox.Text;
            question.IsTrue = TrueBox.Checked;
        }

        private void DellButton_Click(object sender, EventArgs e)
        {
            // удаляем вопрос
            DialogResult result = MessageBox.Show("Удаляем вопрос?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int question_number = (int)(NumberOfQuestion.Value - 1);
                if (question_number < 0 || question_number >= _GameInfo.Questions.Count)
                    return;
                _GameInfo.Dell(question_number);
                QuestionBox.Text = "";
            }
            if (result == DialogResult.No)
            {
                return;
            }

        }

        private void СоздатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _GameInfo = new GameInfo();
            UpdateInterface();
        }

        private void UpdateInterface()
        {
            // Надо обновить состояние интерфейса
        }
    }
}
