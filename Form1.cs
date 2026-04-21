using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
namespace MyCalculator;
public enum ButtonType
{
    Number,
    Operator,
    Function,
    Equal
}
public partial class Form1 : Form
{
    private double result = 0;
    private string operation = "";
    private TextBox txtDisplay;
    private bool isPerformed = false;

    private void applyModernButtonStyle(Button btn, ButtonType type)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.Font = new Font("Segoe UI", 14, FontStyle.Bold, GraphicsUnit.Point);
        btn.Cursor = Cursors.Hand;//Chuyển con trỏ chuột thành hình bản tay

        switch (type)
        {
           case ButtonType.Number:
                btn.BackColor = Color.White;//Màu nền
                btn.ForeColor = Color.Black;//Màu chữ
                break;
            case ButtonType.Operator:
                btn.BackColor = Color.FromArgb(230, 230, 230);
                btn.ForeColor = Color.FromArgb(0, 120, 215);
                break;
            case ButtonType.Function:
                btn.BackColor = Color.FromArgb(255, 185, 0);
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 12, FontStyle.Bold, GraphicsUnit.Point);
                break;
            case ButtonType.Equal:
                btn.BackColor = Color.FromArgb(0, 120, 215);
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 16, FontStyle.Bold, GraphicsUnit.Point);
                break;



        }
    }
    public void equalClick(object? sender, EventArgs e)
    {
        
        double num2 = double.Parse(txtDisplay.Text);
        switch (operation)
        {
            case "+":
                result = result + num2;
                break;
            case "-":
                result = result - num2;
                break;
            case "*":
                result = result * num2;
                break;
            case "/":
                if(num2 != 0)
                {
                    result = result / num2;
                }
                else
                {
                    MessageBox.Show("Cannot divide by zero!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    result = 0;
                }
                break;
            default:
                MessageBox.Show("Invalid operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;

    

        }
        txtDisplay.Text = result.ToString();
    }
    public void operatorClick(object? sender, EventArgs e)
    { 
        if(result != 0)
        {
            equalClick(sender, e);
        }
        Button btn = (Button)sender!;
        result = double.Parse(txtDisplay.Text); 
        isPerformed = true;
        operation = btn.Text;
    }
    public void numberClick(object? sender, EventArgs e)
    {
        Button btn = (Button)sender!;
        if(txtDisplay.Text == "0" || isPerformed)
        {
            txtDisplay.Clear();
            isPerformed = false;
        }
        txtDisplay.Text += btn.Text;
    }
    public void CreateButton(string text, int x, int y, EventHandler onclick)
    {

        Button btn = new Button
        {
            Text = text,
            Location = new Point(x, y),
            Size = new Size(70, 40),
            BackColor = Color.FromArgb(200, 200, 200),
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 12, FontStyle.Bold, GraphicsUnit.Point)
        };
        applyModernButtonStyle(btn, text switch
        {
            "+" or "-" or "*" or "/" => ButtonType.Operator,
            "AC" or "DEL" or "ANS" => ButtonType.Function,
            "=" => ButtonType.Equal,
            _ => ButtonType.Number
        });
        this.Controls.Add(btn);
        btn.Click += onclick;
    }
    public void ac_btnClick(object? sender, EventArgs e)
    {
        txtDisplay.Text = "0";
        result = 0;
        operation = "";
        isPerformed = false;

        MessageBox.Show("Calculator reset!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    public Form1()
    {

        InitializeComponent();
        this.Text = "My Calculator";
        this.Size = new Size(400, 500);
        this.BackColor = Color.FromArgb(240, 240, 240);
        this.Font = new Font("Segoe UI", 12, FontStyle.Regular, GraphicsUnit.Point);
        this.StartPosition = FormStartPosition.CenterScreen;// Khởi tạo vị trí của form

        txtDisplay = new TextBox{
            BackColor = Color.FromArgb(255, 255, 255),
            ForeColor = Color.FromArgb(0, 0, 0),
            Location = new Point(20, 20),
            Size = new Size(340, 30),
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Right,
            Font = new Font("Segoe UI", 14, FontStyle.Bold, GraphicsUnit.Point)
        };
        this.Controls.Add(txtDisplay);

        for (int i = 0; i < 10; i++)
        {
            int x = 40 + (i % 3) * 80;
            int y = 120 + (i / 3) * 50;
            CreateButton(i.ToString(), x, y, numberClick);
            

        }
        CreateButton("+", 280, 120, operatorClick);
        CreateButton("-", 280, 170, operatorClick);
        CreateButton("*", 280, 220, operatorClick);
        CreateButton("/", 280, 270, operatorClick);
        CreateButton("=", 280, 320, equalClick);

        //Nút delete để xóa
        CreateButton("DEL", 40, 320, (s, e) =>
        {
            txtDisplay.Text = "";
            result = 0;
            operation = "";
            isPerformed = false;
        });
        //Nút dấu chấm phẩy
        CreateButton(".", 120, 270, (s, e) =>
        {
            if (!txtDisplay.Text.Contains("."))
            {
                txtDisplay.Text += ".";
            }
        });

        //Nút AC
        CreateButton("AC", 120, 320, ac_btnClick);
        //Nút ans để hiển thị kết quả trước đó
        CreateButton("ANS", 200, 270, (s, e) =>
        {
            txtDisplay.Text = result.ToString();
        });
    }
}
