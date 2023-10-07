using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.ListViewItem;

namespace app_vertical_calender
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> _dateMap = new Dictionary<string, string>();
        SaveData _save;
        DateTime _startDate;
        bool _isLoading;
        public Form1()
        {
            InitializeComponent();
            _save =  XMLClass.Load<SaveData>();
            this.ControlBox = false;
            this.Text = "";
            this.KeyPreview = true;
            this.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            };
            var uimode = _save.UIMode;
            this.ForeColor = uimode == "light" ? Color.FromArgb(47, 54, 64) : Color.FromArgb(245, 246, 250);
            this.BackColor = uimode == "light" ? Color.FromArgb(245, 246, 250) : Color.FromArgb(47, 54, 64);

            c_listbox_calender.Columns.Add("", -2, HorizontalAlignment.Left);
            c_listbox_calender.Columns.Add("", -2, HorizontalAlignment.Left);
            c_listbox_calender.HeaderStyle = ColumnHeaderStyle.None;
            c_listbox_calender.FullRowSelect = true;
            c_listbox_calender.Select();
            c_listbox_calender.DoubleClick += c_listbox_calender_DoubleClick;
            c_listbox_calender.ItemSelectionChanged += c_listbox_calender_ItemSelectionChanged;
            c_listbox_calender.MouseDown += c_listbox_MouseDown;
            c_listbox_calender.MouseMove += c_listbox_MouseMove;
            c_listbox_calender.ForeColor = uimode == "light" ? Color.FromArgb(47, 54, 64) : Color.FromArgb(245, 246, 250);
            c_listbox_calender.BackColor = uimode == "light" ? Color.FromArgb(245, 246, 250) : Color.FromArgb(47, 54, 64);


            const int start_num = -60;
            _startDate = DateTime.Today.AddDays(start_num);
            for (int i = 0; i < 120; i++)
            {
                var date = _startDate.AddDays(i);
                var item = new ListViewItem
                {
                    Text = date.ToString("MM/dd(ddd)")
                };
                c_listbox_calender.Items.Add(item);
                item.SubItems.Add(new ListViewSubItem
                {
                    Text = _save.Get(date)
                });
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            c_listbox_calender.Columns[1].Width = Width - c_listbox_calender.Columns[0].Width - 40;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Bounds = Properties.Settings.Default.Bounds;
            var item = c_listbox_calender.Items[60];
            item.Selected = true;
            item.ForeColor = ColorFromHSV(0, .5, 1);
            c_listbox_calender.TopItem = (item);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Bounds = Bounds;
            Properties.Settings.Default.Save();
            XMLClass.Save(_save);
        }

        private void c_listbox_calender_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }

        private void c_listbox_calender_DoubleClick(object sender, EventArgs e)
        {
            var selected = c_listbox_calender.SelectedItems;
            if (selected != null)
            {
                var form = new Form2();
                form.textBox1.Text = selected[0].SubItems[1].Text;
                form.ShowDialog();
                if (form.IsOK)
                {
                    selected[0].SubItems[1].Text = form.textBox1.Text;
                    _save.Add(_startDate.AddDays(selected[0].Index), form.textBox1.Text);
                    XMLClass.Save(_save);
                }
                form.Dispose();
            }
        }

        Point mousePoint;
        private void c_listbox_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                mousePoint = new Point(e.X, e.Y);
            }
        }

        private void c_listbox_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;
            }
        }

        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public class SaveData
        {
            public string UIMode = "dark";
            public List<Item> Items = new List<Item>();
            public void Add(DateTime key, string value)
            {
                var akey = key.ToOADate().ToString();
                var index = Items.FindIndex(e => e.Key == akey);
                if (-1 == index)
                    Items.Add(new Item { Key = akey, Value = value });
                else
                    Items[index].Value = value;
            }
            public string Get(DateTime key)
            {
                var akey = key.ToOADate().ToString();
                var index = Items.FindIndex(e => e.Key == akey);
                return -1 == index ? "" : Items[index].Value;
            }
            public class Item
            {
                public string Key;
                public string Value;
            }
        }

        public static class XMLClass
        {
            static readonly string path = "save.txt";
            public static string Save<T>(T control)
            {
                var writer = new StringWriter(); // 出力先のWriterを定義
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, control);
                var xml = writer.ToString();
                File.WriteAllText(path, xml);
                Console.WriteLine(xml);
                return xml;
            }

            public static T Load<T>() where T : new()
            {
                if (!File.Exists(path))
                    return new T();
                var xml = File.ReadAllText(path);
                var serializer = new XmlSerializer(typeof(T));
                var deserializedBook = (T)serializer.Deserialize(new StringReader(xml));
                return deserializedBook;
            }
        }

    }
}
