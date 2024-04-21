using System;
using System.Linq;
using System.Windows.Forms;

namespace spschool
{
    public partial class Form2 : Form
    {
        Sportsman _st;
        public Form2(Sportsman st, Form1 mf, bool is_edit = false)
        {
            InitializeComponent();
            _st = st;

            if (is_edit)
            {
                textBoxCode.Text = st.Code.ToString();
                textBoxFIO.Text = st.FIO;
                textBoxSport.Text = st.Sport;
                numericUpDownTeam.Value = st.Team;
                dateTimePicker1.Value = st.BrDate;
                textBoxTitle.Text = st.Title;
            }
            else
            {
                if (mf.sportsmanList.bd.Count() > 0)
                {
                    textBoxCode.Text =
                            (mf.sportsmanList.bd.Max(x => x.Code) + 1).ToString();
                }
                else
                {
                    textBoxCode.Text = "0";
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                $"Сохранить элемент с кодом: {textBoxCode.Text} ?",
                "Сохранение элемента элемента.",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _st.Code = Convert.ToInt32(textBoxCode.Text);
                _st.FIO = textBoxFIO.Text;
                _st.Sport = textBoxSport.Text;
                _st.Team = (int)numericUpDownTeam.Value;
                _st.BrDate = dateTimePicker1.Value;
                _st.Title = textBoxTitle.Text;
                DialogResult = DialogResult.OK;

                Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
