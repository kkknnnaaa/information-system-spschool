using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace spschool
{
    public partial class Form1 : Form
    {
        int indCol = 1;
        string nameField = "Code";
        string filtrTeamName = "Все команды";
        SortOrder typeSort = SortOrder.Ascending;

        public ListSportsmanJson sportsmanList = new ListSportsmanJson();

        public Sportsman currentSt = null;

        public BindingSource bindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();


            string fileName = "sport1.json";
            sportsmanList = new ListSportsmanJson();

            string errorText = sportsmanList.ReadFromFile(fileName);

            if (!String.IsNullOrEmpty(errorText))
            {
                MessageBox.Show(errorText, "Ошибка чтения файла",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                return;
            }
            else
            {
                RefreshGrid();
                Update_TeamChart();
            }


        }
        public void Update_TeamChart()
        {
            var group_sportsman = sportsmanList.bd.GroupBy(s => s.Team)
                                            .Select(group => new { Team = group.Key, Count = group.Count() })
                                            .OrderBy(s => s.Team);
            var x = group_sportsman.Select(s => s.Team).ToArray();
            var y = group_sportsman.Select(s => s.Count).ToArray();
            TeamChart.Series[0].Points.DataBindXY(x, y);
        }
        

        public void RefreshGrid()
        {
            dataGridView1.DataSource = null;

            switch (nameField)
            {
                case "Code":
                    if (typeSort == SortOrder.Ascending)
                        sportsmanList.bd = sportsmanList.bd.OrderBy(x => x.Code).ToList();
                    else
                        sportsmanList.bd = sportsmanList.bd.OrderByDescending(x => x.Code).ToList();
                    break;
                case "FIO":
                    if (typeSort == SortOrder.Ascending)
                        sportsmanList.bd = sportsmanList.bd.OrderBy(x => x.FIO).ToList();
                    else
                        sportsmanList.bd = sportsmanList.bd.OrderByDescending(x => x.FIO).ToList();
                    break;
                case "Sport":
                    if (typeSort == SortOrder.Ascending)
                        sportsmanList.bd = sportsmanList.bd.OrderBy(x => x.Sport).ToList();
                    else
                        sportsmanList.bd = sportsmanList.bd.OrderByDescending(x => x.Sport).ToList();
                    break;
                case "Team":
                    if (typeSort == SortOrder.Ascending)
                        sportsmanList.bd = sportsmanList.bd.OrderBy(x => x.Team).ToList();
                    else
                        sportsmanList.bd = sportsmanList.bd.OrderByDescending(x => x.Team).ToList();
                    break;
                case "BrDate":
                    if (typeSort == SortOrder.Ascending)
                        sportsmanList.bd = sportsmanList.bd.OrderBy(x => x.BrDate).ToList();
                    else
                        sportsmanList.bd = sportsmanList.bd.OrderByDescending(x => x.BrDate).ToList();
                    break;
                case "Age":
                    if (typeSort == SortOrder.Ascending)
                        sportsmanList.bd = sportsmanList.bd.OrderBy(x => x.Age).ToList();
                    else
                        sportsmanList.bd = sportsmanList.bd.OrderByDescending(x => x.Age).ToList();
                    break;
                case "Title":
                    if (typeSort == SortOrder.Ascending)
                        sportsmanList.bd = sportsmanList.bd.OrderBy(x => x.Title).ToList();
                    else
                        sportsmanList.bd = sportsmanList.bd.OrderByDescending(x => x.Title).ToList();
                    break;
            }

            if (filtrTeamName == "Все команды")
            {
                bindingSource.DataSource = sportsmanList.bd;
            }
            else
            {
                bindingSource.DataSource = sportsmanList.bd.Where(team => team.Team == Convert.ToInt32(filtrTeamName)).ToList();
            }
            
            dataGridView1.DataSource = bindingSource;
            dataGridView1.AutoGenerateColumns = false;
            bindingSource.Position = bindingSource.IndexOf(currentSt);
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[indCol].HeaderCell.SortGlyphDirection =
                    typeSort;
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            indCol = e.ColumnIndex;
            nameField = dataGridView1.Columns[indCol].Name;
            typeSort = dataGridView1.Columns[indCol].HeaderCell.SortGlyphDirection;
            if (typeSort == SortOrder.None)
                typeSort = SortOrder.Ascending;
            if (typeSort == SortOrder.Ascending)
                typeSort = SortOrder.Descending;
            else
                typeSort = SortOrder.Ascending;
            dataGridView1.Columns[indCol].HeaderCell.SortGlyphDirection =
                typeSort;
            RefreshGrid();
            Update_TeamChart();
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            currentSt = new Sportsman();

            Form2 addEdit = new Form2(currentSt, this);
            addEdit.ShowDialog();
            if (addEdit.DialogResult == DialogResult.OK)
            {
                sportsmanList.bd.Add(currentSt);
                RefreshGrid();
                Update_TeamChart();
            }
        }
        private void toolStripButtonChange_Click(object sender, EventArgs e)
        {
            currentSt = (Sportsman)bindingSource.Current;

            Form2 addEdit = new Form2(currentSt, this, true);
            addEdit.ShowDialog();
            if (addEdit.DialogResult == DialogResult.OK)
            {
                RefreshGrid();
                Update_TeamChart();
            }
        }
        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            currentSt = (Sportsman)bindingSource.Current;
            DialogResult result = MessageBox.Show(
                $"Удалить элемент с кодом: {currentSt.Code} ?",
                "Удаление элемента.",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                sportsmanList.bd.Remove(currentSt);
                RefreshGrid();
                Update_TeamChart();
            }
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            if (sportsmanList.bd.Count() != 0)
            {
                string fileName = "sport1.json";
                string errorText = sportsmanList.WriteToFile(fileName);
                if (!String.IsNullOrEmpty(errorText))
                {
                    MessageBox.Show(errorText, "Ошибка записи файла",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Сохранение прошла успешно", "Информация",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            }
        }

        private void toolStripComboBoxTeam_Click(object sender, EventArgs e)
        {
            var group_sportsman = sportsmanList.bd.GroupBy(s => s.Team)
                                            .Select(group => new { Team = group.Key})
                                            .OrderBy(s => s.Team);
            toolStripComboBoxTeam.Items.Clear();
            toolStripComboBoxTeam.Items.Add("Все команды");

            foreach (var team in group_sportsman)
            {
                toolStripComboBoxTeam.Items.Add(team.Team);
            }
        }

        private void toolStripComboBoxTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            filtrTeamName = toolStripComboBoxTeam.SelectedItem.ToString();
            RefreshGrid();
        }
    }
}
