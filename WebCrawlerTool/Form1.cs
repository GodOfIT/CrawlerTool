using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebCrawlerTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://fsd.hanu.vn/quantri");


        }
        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);

            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').Replace(' ', '-').Replace('A', 'a').Replace('Q', 'q').Replace('U', 'u').Replace('W', 'w').Replace('E', 'e').Replace('R', 'r').Replace('T', 't').Replace('Y', 'y').Replace('I', 'i').Replace('O', 'o').Replace('P', 'p').Replace('S', 's').Replace('D', 'd').Replace('F', 'f').Replace('G', 'g').Replace('H', 'h').Replace('J', 'j').Replace('K', 'k').Replace('L', 'l').Replace('Z', 'z').Replace('X', 'x').Replace('C', 'c').Replace('V', 'v').Replace('B', 'b').Replace('N', 'n').Replace('M', 'm');
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }



        private void button2_Click(object sender, EventArgs e)
        {
            HtmlWeb htmlweb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8
            };
            string s = textBox1.Text + textBox7.Text;

            HtmlAgilityPack.HtmlDocument doc = htmlweb.Load("http://web.hanu.vn/fsd/mod/forum/discuss.php?d=2801");
            var items = doc.DocumentNode.SelectNodes("//tr[@class='header']").ToList();
            var Icontent = doc.DocumentNode.SelectSingleNode("//td[@class='content']");
            var neededitems = new List<object>();
            foreach (var item in items)
            {
                var header = item.SelectSingleNode(".//td[contains(@class,'topic starter')]/div[contains(@class,'subject')]").InnerText;
                object obj1 = new { header };
                neededitems.Add(obj1);
                textBox5.Text = header.ToString();
            }
            var neededcontent = new List<object>();
            string st1 = "";
            var htmlcontent = Icontent.SelectSingleNode(".//div[contains(@class,'posting')]/div[contains(@class,'posting')]");

            var content = htmlcontent.InnerText;
            object obj = new { content };
            neededcontent.Add(obj);
            st1 = content.ToString();
            textBox6.Text = content.ToString();

            webBrowser1.Document.GetElementById("jform_title").InnerText = textBox5.Text;
            webBrowser1.Document.GetElementById("jform_alias").InnerText = convertToUnSign3(textBox5.Text) + "lllllllllll";
            HtmlWindow frame = webBrowser1.Document.GetElementById("jform_articletext_ifr").Document.Window.Frames["jform_articletext_ifr"];
            HtmlElement body = frame.Document.GetElementById("tinymce");
            body.InnerHtml = htmlcontent.InnerHtml;
           // body.InnerText = st1;

            //string ss = "article.save";
            //object[] tt = new object[1];
            //tt[0] = (object)ss;
            //webBrowser1.Document.InvokeScript("submitbutton", tt);
            //webBrowser1.Navigate("http://fsd.hanu.vn/administrator/index.php?option=com_content&task=article.add");


        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://fsd.hanu.vn/administrator/index.php?option=com_content&task=article.add");

        }

        private void button1_Click(object sender, EventArgs e)
        {

            webBrowser1.Document.GetElementById("mod-login-username").InnerText = "ducbm";

            webBrowser1.Document.GetElementById("mod-login-password").InnerText = "ducbm";

            webBrowser1.Document.GetElementById("form-login").InvokeMember("submit");

            webBrowser1.Navigate("http://fsd.hanu.vn/administrator/index.php?option=com_content&task=article.add");
            button2.Enabled = true;
        }


    }
}
